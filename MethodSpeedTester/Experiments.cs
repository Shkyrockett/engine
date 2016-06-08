// <copyright file="Experiments.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine;
using Engine.Geometry;
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
        /// Set of tests to run testing methods that calculate the absolute angle of Two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(AbsoluteAngleTests))]
        public static List<SpeedTester> AbsoluteAngleTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => AbsoluteAngle0(0, 0, 1, 1),
                $"{nameof(Experiments.AbsoluteAngle0)}(0, 0, 1, 1)"),
                 new SpeedTester(() => AbsoluteAngle1(0, 0, 1, 1),
                $"{nameof(Experiments.AbsoluteAngle1)}(0, 0,, 1, 1)"),
           };
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
        /// Set of tests to run testing methods that calculate the angle between Two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(AngleBetweenTests))]
        public static List<SpeedTester> AngleBetweenTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => AngleBetween(0, 0, 1, 1),
                $"{nameof(Experiments.AngleBetween)}(0, 0, 1, 1)"),
           };
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

        #endregion

        #region Angle Between Two 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle between Two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(AngleBetween3DTests))]
        public static List<SpeedTester> AngleBetween3DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => AngleBetween(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.AngleBetween)}(0, 0, 0, 1, 1, 1)"),
           };
        }

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
        /// Set of tests to run testing methods that calculate the angle of Two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(Angle2DTests))]
        public static List<SpeedTester> Angle2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => Angle(0, 0, 1, 1),
                $"{nameof(Experiments.Angle)}(0, 0, 1, 1)"),
           };
        }

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
        /// Set of tests to run testing methods that calculate the angle of Two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(Angle3DTests))]
        public static List<SpeedTester> Angle3DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => Angle(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.Angle)}(0, 0, 0, 1, 1, 1)"),
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
        /// Set of tests to run testing methods that calculate the complex product of Two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(ComplexProduct2DTests))]
        public static List<SpeedTester> ComplexProduct2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => ComplexProduct(0, 0, 1, 1),
                $"{nameof(Experiments.ComplexProduct)}(0, 0, 1, 1)"),
           };
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
        public static Tuple<double, double> ComplexProduct(
            double x0, double y0,
            double x1, double y1)
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

        #region Closest Point on line

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line</remarks>
        private static Point2D ClosestPointOnLineSegmentMvG(Point2D a, Point2D b, Point2D p)
        {
            // Vector A->B
            Point2D diffAB = new Point2D(a.X - b.X, a.Y - b.Y);

            double det = a.Y * b.X - a.X * b.Y;

            double dot = diffAB.X * p.X + diffAB.Y * p.Y;

            Point2D val = new Point2D(dot * diffAB.X + det * diffAB.Y, dot * diffAB.Y - det * diffAB.X);

            double magnitude = diffAB.X * diffAB.X + diffAB.Y * diffAB.Y;

            double inverseDist = 1 / magnitude;

            return new Point2D(val.X * inverseDist, val.Y * inverseDist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line</remarks>
        private static Point2D ClosestPointOnLineSegmentDarienPardinas(Point2D a, Point2D b, Point2D p)
        {
            // Vector A->P 
            Point2D diffAP = new Point2D(p.X - a.X, p.Y - a.Y);

            // Vector A->B
            Point2D diffAB = new Point2D(b.X - a.X, b.Y - a.Y);

            double dotAB = diffAB.X * diffAB.X + diffAB.Y * diffAB.Y;

            // The dot product of diffAP and diffAB
            double dotABAP = diffAP.X * diffAB.X + diffAP.Y * diffAB.Y;

            //  # The normalized "distance" from a to the closest point
            double dist = dotABAP / dotAB;

            if (dist < 0)
            {
                return a;
            }
            else if (dist > dotABAP)
            {
                return b;
            }
            else
            {
                return new Point2D(a.X + diffAB.X * dist, a.Y + diffAB.Y * dist);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line</remarks>
        private static Point2D ClosestPointOnLineDarienPardinas(Point2D a, Point2D b, Point2D p)
        {
            // Vector A->P 
            Point2D diffAP = new Point2D(p.X - a.X, p.Y - a.Y);

            // Vector A->B
            Point2D diffAB = new Point2D(b.X - a.X, b.Y - a.Y);

            double dotAB = diffAB.X * diffAB.X + diffAB.Y * diffAB.Y;

            // The dot product of diffAP and diffAB
            double dotABAP = diffAP.X * diffAB.X + diffAP.Y * diffAB.Y;

            // The normalized "distance" from a to the closest point
            double dist = dotABAP / dotAB;

            return new Point2D(a.X + diffAB.X * dist, a.Y + diffAB.Y * dist);
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

        #region Cubic Bezier Length approximations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <returns></returns>
        /// <remarks>http://steve.hollasch.net/cgindex/curves/cbezarclen.html</remarks>
        private static double CubicBezierArcLength(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
        {
            Point2D k1 = (Point2D)(-p1 + 3 * (p2 - p3) + p4);
            Point2D k2 = 3 * (p1 + p3) - 6 * p2;
            Point2D k3 = (Point2D)(3 * (p2 - p1));
            Point2D k4 = p1;

            double q1 = 9.0 * (Sqrt(Abs(k1.X)) + Sqrt((Abs(k1.Y))));
            double q2 = 12.0 * (k1.X * k2.X + k1.Y * k2.Y);
            double q3 = 3.0 * (k1.X * k3.X + k1.Y * k3.Y) + 4.0 * (Sqrt(Abs(k2.X)) + Sqrt(Abs(k2.Y)));
            double q4 = 4.0 * (k2.X * k3.X + k2.Y * k3.Y);
            double q5 = Sqrt(Abs(k3.X)) + Sqrt(Abs(k3.Y));

            // Approximation algorithm based on Simpson. 
            double a = 0;
            double b = 1;
            int n_limit = 1024;
            double TOLERANCE = 0.001;

            int n = 1;

            double multiplier = (b - a) / 6.0;
            double endsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a) + CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, b);
            double interval = (b - a) / 2.0;
            double asum = 0;
            double bsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a + interval);
            double est1 = multiplier * (endsum + 2 * asum + 4 * bsum);
            double est0 = 2 * est1;

            while (n < n_limit && (Abs(est1) > 0 && Abs((est1 - est0) / est1) > TOLERANCE))
            {
                n *= 2;
                multiplier /= 2;
                interval /= 2;
                asum += bsum;
                bsum = 0;
                est0 = est1;
                double interval_div_2n = interval / (2.0 * n);

                for (int i = 1; i < 2 * n; i += 2)
                {
                    double t = a + i * interval_div_2n;
                    bsum += CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, t);
                }

                est1 = multiplier * (endsum + 2 * asum + 4 * bsum);
            }

            return est1 * 10;
        }

        /// <summary>
        /// Bezier Arc Length Function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="q1"></param>
        /// <param name="q2"></param>
        /// <param name="q3"></param>
        /// <param name="q4"></param>
        /// <param name="q5"></param>
        /// <returns></returns>
        /// <remarks>http://steve.hollasch.net/cgindex/curves/cbezarclen.html</remarks>
        private static double CubicBezierArcLengthHelper(ref double q1, ref double q2, ref double q3, ref double q4, ref double q5, double t)
        {
            double result = q5 + t * (q4 + t * (q3 + t * (q2 + t * q1)));
            result = Sqrt(Abs(result));
            return result;
        }

        /// <summary>
        /// Approximate length of the Bezier curve which starts at "start" and
        /// is defined by "c". According to Computing the Arc Length of Cubic Bezier Curves
        /// there is no closed form integral for it.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        /// <remarks>http://www.lemoda.net/maths/bezier-length/index.html</remarks>
        private static double CubicBezierLength(Point2D p1, Point2D p2, Point2D p3, Point2D p4, int steps = 10)
        {
            double t;
            Point2D dot;
            Point2D previous_dot = new Point2D();
            double length = 0.0;
            for (int i = 0; i <= steps; i++)
            {
                t = (double)i / steps;
                dot = new Point2D(Interpolaters.CubicBezier(p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y, p4.X, p4.Y, t));
                if (i > 0)
                {
                    double x_diff = dot.X - previous_dot.X;
                    double y_diff = dot.Y - previous_dot.Y;
                    length += Sqrt(x_diff * x_diff + y_diff * y_diff);
                }
                previous_dot = dot;
            }
            return length;
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

        #region Cubic Bezier and Line Intersections

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

        #region Ellipse Perimeter Length

        /// <summary>
        /// This approximation is within about 5% of the true value, so long as a is not more than 3 times longer than b (in other words, the ellipse is not too "squashed"):
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeter1(double a, double b)
        {
            return 2 * PI * (Sqrt(0.5 * ((b * b) + (a * a))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://ellipse-circumference.blogspot.com/
        /// </remarks>
        private static double EllipsePerimeter2(double a, double b)
        {
            double h = (((b - a) * (b - a)) / ((b + a) * (b + a)));
            double H2 = 4 - 3 * h;
            double d = ((11 * PI / (44 - 14 * PI)) + 24100) - 24100 * h;
            return PI * (b + a) * (1 + (3 * h) / (10 + Pow(H2, 0.5)) + (1.5 * Pow(h, 6) - .5 * Pow(h, 12)) / d);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterKepler(double a, double b)
        {
            return 2 * PI * (Sqrt(a * b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterSipos(double a, double b)
        {
            return 2 * PI * (((a + b) * (a + b)) / ((Sqrt(a) + Sqrt(a)) * (Sqrt(b) + Sqrt(b))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterNaive(double a, double b)
        {
            return PI * (a + b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterPeano(double a, double b)
        {
            return PI * ((3 * (a + b) / 2) - Sqrt(a * b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterEuler(double a, double b)
        {
            return 2 * PI * Sqrt(((a * a) + (b * b)) / 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterAlmkvist(double a, double b)
        {
            return 2 * PI
                * ((2 * Pow(a + b, 2) - Pow(Sqrt(a) - Sqrt(b), 4))
                / (Pow(Sqrt(a) - Sqrt(b), 2) + (2 * Sqrt(2 * (a + b)) * Pow(a * b, (1 / 4)))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterQuadratic(double a, double b)
        {
            return (PI / 2) * Sqrt((6) * (a * a + b * b) + (4 * a * b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterMuir(double a, double b)
        {
            return 2 * PI * Pow((Pow(a, 3 / 2) + Pow(b, 3 / 2)) / 2, 2 / 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterLindner(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * Sqrt(1 + (h / 8));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipseCircumference05.html
        /// </remarks>
        private static double EllipsePerimeterSykoraRiveraCantrellsParticularlyFruitful(double a, double b)
        {
            return 4 * ((PI * a * b) + ((a - b) * (a - b))) / (a + b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeterYNOT(double a, double b)
        {
            double s = Log(2, E) / Log(PI / 2, E);
            return 4 * Pow(Pow(a, s) + Pow(b, s), 1 / s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeterCombinedPadé(double a, double b)
        {
            double d1 = (PI / 4) * (19 / 15) - 1;
            double d2 = (PI / 4) * (80 / 63) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return PI * (a + b) * (p * ((64 + 16 * h)
                / (64 - h * h))
                + (1 - p) * ((16 + 3 * h) / (16 - h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeterCombinedPadé2(double a, double b)
        {
            double d1 = (PI / 4) * (81 / 64) - 1;
            double d2 = (PI / 4) * (19 / 15) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return PI * (a + b) * (p * ((16 - 3 * h)
                / (16 - h))
                + (1 - p) * Pow(1 + (h) / 8, 2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeterJacobsenWaadelandHudsonLipka(double a, double b)
        {
            double d1 = (PI / 4) * (61 / 48) - 1;
            double d2 = (PI / 4) * (187 / 147) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return PI * (a + b) * (p * ((256 - 48 * h - 21 * h * h)
                / (256 - 112 * h + 3 * h * h))
                + (1 - p) * ((64 - 3 * h * h) / (64 - 16 * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeter2_3JacobsenWaadeland(double a, double b)
        {
            double d1 = (PI / 4) * (61 / 48) - 1;
            double d2 = (PI / 4) * (187 / 147) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return PI * (a + b) * (p * ((3072 - 1280 * h - 252 * h * h + 33 * h * h * h)
                / (3072 - 2048 * h + 212 * h * h))
                + (1 - p) * ((256 - 48 * h - 21 * h * h) / (256 - 112 * h + 3 * h * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeter3_3_3_2(double a, double b)
        {
            double d1 = (PI / 4) * (61 / 48) - 1;
            double d2 = (PI / 4) * (187 / 147) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return PI * (a + b) * (p * ((135168 - 85760 * h - 5568 * h * h + 3867 * h * h * h)
                / (135168 - 119552 * h + 22208 * h * h - 345 * h * h * h))
                + (1 - p) * ((3072 - 1280 * h - 252 * h * h + 33 * h * h * h)
                / (3072 - 2048 * h + 212 * h * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterRamanujan(double a, double b)
        {
            return PI * (3 * (a + b) - Sqrt((3 * a + b) * (a + 3 * b)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterSelmer(double a, double b)
        {
            return (PI / 4) * ((6 + .5 * (Pow(a - b, 2) * Pow(a - b, 2) / Pow(a + b, 2) * Pow(a + b, 2))) * (a + b) - Sqrt(2 * (a * a + 3 * a * b + b * b)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterRamanujan2(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * (1 + ((3 * h) / (10 + Sqrt(4 - 3 * h))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterPadéSelmer(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * ((16 + (3 * h)) / (16 - h));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterPadéMichon(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * ((64 + (16 * h)) / (64 - (h * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterPadéHudsonLipkaBronshtein(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * ((64 + (3 * h * h)) / (64 - (16 * h)));
        }

        /// <summary>
        /// Not correct.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterCombinedPadéHudsonLipkaMichon(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * ((64 + (3 * h * h)) / (64 - (16 * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterPadéJacobsenWaadeland(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * ((256 - (48 * h) - (21 * h * h)) / (256 - (112 * h) + 3 * h * h));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterPadé3_2(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * ((3072 - (1280 * h) - (252 * h * h) + (33 * h * h * h)) / (3072 - (2048 * h) + 212 * h * h));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterPadé3_3(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) *
                ((135168 - (85760 * h) - (5568 * h * h) + (3867 * h * h * h))
                / (135168 - (119552 * h) + (22208 * h * h) - (345 * h * h * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterOptimizedPeano(double a, double b)
        {
            double p = 1.32;
            return 2 * PI * (p * ((a + b) / 2) + (1 - p) * Sqrt(a * b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterOptimizedQuadratic1(double a, double b)
        {
            double w = 0.7966106;
            return 2 * PI * Sqrt(w * ((a * a + b * b) / 2) + (1 - w) * a * b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterOptimizedQuadratic2(double a, double b)
        {
            return PI * Sqrt(2 * (a * a + b * b) + (a - b) * (a - b) / 2.458338);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterOptimizedRamanujan1(double a, double b)
        {
            double p = 3.0273;
            double w = 3;
            return 2 * PI * (p * ((a + b) / 2) + (1 - p) * Sqrt((a + w * b) * (w * a + b)) / (1 + w));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterBartolomeuMichon(double a, double b)
        {
            return a == b ? 2 * PI * a : PI * ((a - b) / Atan((a - b) / (a + b)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterCantrell2(double a, double b)
        {
            double p = 0.410117;
            double w = 74;
            return 4 * (a + b) - ((8 - 2 * PI) * a * b) /
                (p * (a + b) + (1 - 2 * p) * (Sqrt((a + w * b) * (w * a + b)) / (1 + w)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterTakakazuSeki(double a, double b)
        {
            return 2 * Sqrt(PI * PI * a * b + 4 * (a - b) * (a - b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterLockwood(double a, double b)
        {
            return 4 * (((b * b) / a) * Atan(a / b) + ((a * a) / b) * Atan(b / a));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterBartolomeu(double a, double b)
        {
            double t = (PI / 4) * ((a - b) / b);
            return PI * Sqrt(2 * (a * a + b * b)) * (Sin(t) / t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterRivera1(double a, double b)
        {
            return 4 * a + 2 * (PI - 2) * a * Pow(b / a, 1.456);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterRivera2(double a, double b)
        {
            return 4 * ((PI * a * b + (a - b) * (a - b)) / (a + b)) - (89 / 146) * Pow((b * Sqrt(a) - a * Sqrt(b)) / (a + b), 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterCantrell(double a, double b)
        {
            double s = Log(2) / Log(2 / (4 - PI));
            return 4 * (a + b) - ((2 * (4 - PI) * a * b) / Pow(Pow(a, s) + Pow(b, s), 1 / s));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterSykora(double a, double b)
        {
            return 4 * (((PI * a * b + (a - b) * (a - b))) / (a + b)) - 0.5 * ((a * b) / (a + b)) * (((a - b) * (a - b)) / (PI * a * b + (a + b) * (a + b)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterCantrellRamanujan(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * (1 + ((3 * h) / (10 + Sqrt(4 - 3 * h))) + ((4 / PI) - ((14) / (11))) * Pow(h, 12));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeterK13(double a, double b)
        {
            return PI * (((a + b) / 2) + Sqrt((a * a + b * b) / 2));
        }

        /// <summary>
        /// This one is not as good with a circle. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://ellipse-circumference2.blogspot.com/2011/12/accurate-online-ellipse-circumference.html</remarks>
        private static double EllipsePerimeterThomasBlankenhorn1(double a, double b)
        {
            double X1 = a;
            double X2 = b;
            double HMX = Max(X1, X2);
            double HMN = Min(X1, X2);
            double H1 = HMN / HMX;
            return 2 * PI * HMX * ((2 / PI) + 0.0000122 * Pow(H1, 0.6125) - 0.0021973 * Pow(H1, 1.225) + 0.919315 * Pow(H1, 1.8375) - 1.0359227 * Pow(H1, 2.45) + 0.861913 * Pow(H1, 3.0625) - 0.7274398 * Pow(H1, 3.675) + 0.6352295 * Pow(H1, 4.2875) - 0.436051 * Pow(H1, 4.9) + 0.1818904 * Pow(H1, 5.5125) - 0.0333691 * Pow(H1, 6.125));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://ellipse-circumference3.blogspot.com/
        /// </remarks>
        private static double EllipsePerimeterThomasBlankenhorn8(double a, double b)
        {
            double X1 = a;
            double X2 = b;
            double HMX = Max(X1, X2);
            double HMN = Min(X1, X2);
            double H1 = HMN / HMX;
            return HMX * (4 + (3929 * Pow(H1, 1.5) + 1639157 * Pow(H1, 2) + 19407215 * Pow(H1, 2.5) + 24302653 * Pow(H1, 3) + 12892432 * Pow(H1, 3.5)) / (86251 + 1924742 * Pow(H1, 0.5) + 6612384 * Pow(H1, 1) + 7291509 * Pow(H1, 1.5) + 6436977 * Pow(H1, 2) + 3158719 * Pow(H1, 2.5)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox06.html
        /// </remarks>
        private static double EllipsePerimeterCantrell2006(double a, double b)
        {
            double p = 3.982901;
            double q = 66.71674;
            double s = 18.31287;
            double t = 23.39728;
            double r = 4 * ((4 - PI) * (4 * s + t + 16) - (4 * p + q));
            return 4 * (a + b)
                - ((a * b) / (a + b))
                * ((p * (a + b) * (a + b) + q * a * b + r * ((a * b) / (a + b)) * ((a * b) / (a + b)))
                / ((a + b) * (a + b) + s * a * b + t * ((a * b) / (a + b)) * ((a * b) / (a + b))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox06.html
        /// </remarks>
        private static double EllipsePerimeterAhmadi2006(double a, double b)
        {
            double c1 = PI - 3;
            double c2 = PI;
            double c3 = 0.5;
            double c4 = (PI + 1) / 2;
            double c5 = 4;
            double k = 1 - ((c1 * a * b) / ((a * a + b * b) + c2 * Sqrt(c3 * a * b * a * b + a * b * Sqrt(a * b * (c4 * (a * a + b * b) + c5 * a * b)))));
            return 4 * ((PI * a * b + k * (a - b) * (a - b)) / (a + b));
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

        #region Intersection of Conic Section with Line segment
        // http://csharphelper.com/blog/2014/11/see-where-a-line-intersects-a-conic-section-in-c/
        #endregion

        #region Intersection of Conic Section with Conic Section
        // http://csharphelper.com/blog/2014/11/see-where-two-conic-sections-intersect-in-c/
        #endregion

        #region Intersection of Ellipse and Ellipse

        /// <summary>
        /// Finds Intersection of two Ellipse'
        /// </summary>
        /// <param name="ellipseA"></param>
        /// <param name="ellipseB"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static LineSegment Intersect(Ellipse ellipseA, Ellipse ellipseB)
        {
            double d = (ellipseB.Center.X * ellipseB.Center.X - ellipseA.Center.X * ellipseA.Center.X - ellipseB.MajorRadius * ellipseB.MajorRadius - Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2) + ellipseA.MajorRadius * ellipseA.MajorRadius);
            double a = (Pow(2 * ellipseA.Center.X - 2 * ellipseB.Center.X, 2) + 4 * Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2));
            double b = (2 * d * (2 * ellipseA.Center.X - 2 * ellipseB.Center.X) - 8 * ellipseB.Center.X * Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2));
            double C = (4 * ellipseB.Center.X * ellipseB.Center.X * Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2) + d * d - 4 * Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2) * ellipseB.MajorRadius * ellipseB.MajorRadius);
            double XA = ((-b + Sqrt(b * b - 4 * a * C)) / (2 * a));
            double XB = ((-b - Sqrt(b * b - 4 * a * C)) / (2 * a));
            double YA = (Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Pow(XA - ellipseA.Center.X, 2)) + ellipseA.Center.Y);
            double YB = (-Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Pow(XA - ellipseA.Center.X, 2)) + ellipseA.Center.Y);
            double YC = (Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Pow(XB - ellipseA.Center.X, 2)) + ellipseA.Center.Y);
            double YD = (-Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Pow(XB - ellipseA.Center.X, 2)) + ellipseA.Center.Y);
            double E = ((XA - ellipseB.Center.X) + Pow(YA - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius);
            double F = ((XA - ellipseB.Center.X) + Pow(YB - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius);
            double G = ((XB - ellipseB.Center.X) + Pow(YC - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius);
            double H = ((XB - ellipseB.Center.X) + Pow(YD - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius);
            if (Abs(F) < Abs(E)) YA = YB;
            if (Abs(H) < Abs(G)) YC = YD;
            if (ellipseA.Center.Y == ellipseB.Center.Y) YC = 2 * ellipseA.Center.Y - YA;
            return new LineSegment(XA, YA, XB, YC);
        }

        /// <summary>
        /// 
        /// </summary>
        public class EllipseIntersectStuff
        {
            internal bool GotEllipse1 = false, GotEllipse2 = false;
            private Rectangle2D Ellipse1 = new Rectangle2D();
            private Rectangle2D Ellipse2 = new Rectangle2D();

            // Equations that define the ellipses.
            internal double Dx1 = 0;
            internal double Dy1 = 0;
            internal double Dx2 = 0;
            internal double Dy2 = 0;

            internal double Rx1 = 0;
            internal double Ry1 = 0;
            internal double Rx2 = 0;
            internal double Ry2 = 0;

            internal double A1 = 0;
            internal double B1 = 0;
            internal double C1 = 0;
            internal double D1 = 0;
            internal double E1 = 0;
            internal double F1 = 0;
            internal double A2 = 0;
            internal double B2 = 0;
            internal double C2 = 0;
            internal double D2 = 0;
            internal double E2 = 0;
            internal double F2 = 0;

            // The points of intersection.
            internal List<Point2D> Roots = new List<Point2D>();
            internal List<double> RootSign1 = new List<double>();
            internal List<double> RootSign2 = new List<double>();
            internal List<Point2D> PointsOfIntersection = new List<Point2D>();

            // Difference function tangent lines.
            internal double TangentX = 0;
            internal List<Point2D> TangentCenters = null;
            internal List<Point2D> TangentP1 = null;
            internal List<Point2D> TangentP2 = null;
        }

        private const double small = 0.1f;

        /// <summary>
        /// Find the points of intersection.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="eis"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static void FindPointsOfIntersectionNewtonsMethod(double xmin, double xmax, EllipseIntersectStuff eis)
        {
            eis.Roots = new List<Point2D>();
            eis.RootSign1 = new List<double>();
            eis.RootSign2 = new List<double>();

            if (!eis.GotEllipse1 || !eis.GotEllipse2) return;

            // Find roots for each of the difference equations.
            double[] signs = { +1f, -1f };
            foreach (double sign1 in signs)
            {
                foreach (double sign2 in signs)
                {
                    List<Point2D> points = FindRootsUsingNewtonsMethod(
                        xmin, xmax,
                        eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, sign1,
                        eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, sign2);
                    if (points.Count > 0)
                    {
                        eis.Roots.AddRange(points);
                        for (int i = 0; i < points.Count; i++)
                        {
                            eis.RootSign1.Add(sign1);
                            eis.RootSign2.Add(sign2);
                        }
                    }
                }
            }

            // Find corresponding points of intersection.
            eis.PointsOfIntersection = new List<Point2D>();
            for (int i = 0; i < eis.Roots.Count; i++)
            {
                double y1 = G1(eis.Roots[i].X, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, eis.RootSign1[i]);
                double y2 = G1(eis.Roots[i].X, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, eis.RootSign2[i]);
                eis.PointsOfIntersection.Add(new Point2D(eis.Roots[i].X, y1));

                // Validation.
                const double small = 0.001f;
                Debug.Assert(Abs(y1 - y2) < small);
            }
        }

        /// <summary>
        /// Find the points of intersection.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="eis"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-4/</remarks>
        private static void FindPointsOfIntersectionUsingBinaryDivision(double xmin, double xmax, EllipseIntersectStuff eis)
        {
            eis.Roots = new List<Point2D>();
            eis.RootSign1 = new List<double>();
            eis.RootSign2 = new List<double>();

            if (!eis.GotEllipse1 || !eis.GotEllipse2) return;

            // Find roots for each of the difference equations.
            double[] signs = { +1f, -1f };
            foreach (double sign1 in signs)
            {
                foreach (double sign2 in signs)
                {
                    List<Point2D> points = FindRootsUsingBinaryDivision(
                        xmin, xmax,
                        eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, sign1,
                        eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, sign2);
                    if (points.Count > 0)
                    {
                        eis.Roots.AddRange(points);
                        for (int i = 0; i < points.Count; i++)
                        {
                            eis.RootSign1.Add(sign1);
                            eis.RootSign2.Add(sign2);
                        }
                    }
                }
            }

            // Find corresponding points of intersection.
            eis.PointsOfIntersection = new List<Point2D>();
            for (int i = 0; i < eis.Roots.Count; i++)
            {
                double y1 = G1(eis.Roots[i].X, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, eis.RootSign1[i]);
                double y2 = G1(eis.Roots[i].X, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, eis.RootSign2[i]);
                eis.PointsOfIntersection.Add(new Point2D(eis.Roots[i].X, y1));

                // Validation.
                Debug.Assert(Abs(y1 - y2) < small);
            }
        }

        /// <summary>
        /// Find roots by using Newton's method.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static List<Point2D> FindRootsUsingNewtonsMethod(double xmin, double xmax,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            List<Point2D> roots = new List<Point2D>();
            const int num_tests = 1000;
            double delta_x = (xmax - xmin) / (num_tests - 1);

            // Loop over the possible x values looking for roots.
            double x0 = xmin;
            double x, y;
            for (int i = 0; i < num_tests; i++)
            {
                // Try to find a root at this position.
                UseNewtonsMethod(x0, out x, out y,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);

                // See if we have already found this root.
                if (IsNumber(y))
                {
                    bool is_new = true;
                    foreach (Point2D pt in roots)
                    {
                        const double small = 0.001f;
                        if (Abs(pt.X - x) < small)
                        {
                            is_new = false;
                            break;
                        }
                    }

                    // If this is a new point, save it.
                    if (is_new)
                    {
                        roots.Add(new Point2D(x, y));

                        // If we've found two roots, we won't find any more.
                        if (roots.Count > 1) return roots;
                    }
                }

                x0 += delta_x;
            }

            return roots;
        }

        /// <summary>
        /// Find roots by using binary division.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-4/</remarks>
        private static List<Point2D> FindRootsUsingBinaryDivision(double xmin, double xmax,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            List<Point2D> roots = new List<Point2D>();
            const int num_tests = 10;
            double delta_x = (xmax - xmin) / (num_tests - 1);

            // Loop over the possible x values looking for roots.
            double x0 = xmin;
            double x, y;
            for (int i = 0; i < num_tests; i++)
            {
                // Try to find a root in this range.
                UseBinaryDivision(x0, delta_x, out x, out y,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);

                // See if we have already found this root.
                if (IsNumber(y))
                {
                    bool is_new = true;
                    foreach (Point2D pt in roots)
                    {
                        if (Abs(pt.X - x) < small)
                        {
                            is_new = false;
                            break;
                        }
                    }

                    // If this is a new point, save it.
                    if (is_new)
                    {
                        roots.Add(new Point2D(x, y));

                        // If we've found two roots, we won't find any more.
                        if (roots.Count > 1) return roots;
                    }
                }

                x0 += delta_x;
            }

            return roots;
        }

        /// <summary>
        /// Find a root by using Newton's method.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static void UseNewtonsMethod(double x0, out double x, out double y,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            const double cutoff = 0.0000001f;
            const double tiny = 0.00001f;
            const int max_iterations = 100;
            double epsilon;
            int iterations = 0;

            do
            {
                // Display this guess x0.
                iterations++;

                // Make sure x0 isn't on a flat spot.
                double g_prime = GPrime(x0,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);
                while (Abs(g_prime) < tiny)
                {
                    x0 += tiny;
                    g_prime = GPrime(x0,
                        A1, B1, C1, D1, E1, F1, sign1,
                        A2, B2, C2, D2, E2, F2, sign2);
                }

                // Calculate the next estimate for x0.
                double g = G(x0,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);
                epsilon = -g / g_prime;
                x0 += epsilon;
            } while ((Abs(epsilon) > cutoff) && (iterations < max_iterations));

            x = x0;
            y = G(x0,
                A1, B1, C1, D1, E1, F1, sign1,
                A2, B2, C2, D2, E2, F2, sign2);
            //Console.WriteLine("G1(" + x + ") = " + y +
            //    ", Epsilon: " + epsilon +
            //    ", Iterations: " + iterations);
        }

        /// <summary>
        /// Find a root by using binary division.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="delta_x"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-4/</remarks>
        private static void UseBinaryDivision(double x0, double delta_x,
            out double x, out double y,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            const int num_trials = 200;
            const int sgn_nan = -2;

            // Get G(x) for the bounds.
            double xmin = x0;
            double g_xmin = G(xmin,
                A1, B1, C1, D1, E1, F1, sign1,
                A2, B2, C2, D2, E2, F2, sign2);
            if (Abs(g_xmin) < small)
            {
                x = xmin;
                y = g_xmin;
                return;
            }

            double xmax = xmin + delta_x;
            double g_xmax = G(xmax,
                A1, B1, C1, D1, E1, F1, sign1,
                A2, B2, C2, D2, E2, F2, sign2);
            if (Abs(g_xmax) < small)
            {
                x = xmax;
                y = g_xmax;
                return;
            }

            // Get the sign of the values.
            int sgn_min, sgn_max;
            if (IsNumber(g_xmin)) sgn_min = Sign(g_xmin);
            else sgn_min = sgn_nan;
            if (IsNumber(g_xmax)) sgn_max = Sign(g_xmax);
            else sgn_max = sgn_nan;

            // If the two values have the same sign,
            // then there is no root here.
            if (sgn_min == sgn_max)
            {
                x = 1;
                y = double.NaN;
                return;
            }

            // Use binary division to find the point of intersection.
            double xmid = 0, g_xmid = 0;
            int sgn_mid = 0;
            for (int i = 0; i < num_trials; i++)
            {
                // Get values for the midpoint.
                xmid = (xmin + xmax) / 2;
                g_xmid = G(xmid,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);
                if (IsNumber(g_xmid)) sgn_mid = Sign(g_xmid);
                else sgn_mid = sgn_nan;

                // If sgn_mid is 0, gxmid is 0 so this is the root.
                if (sgn_mid == 0) break;

                // See which half contains the root.
                if (sgn_mid == sgn_min)
                {
                    // The min and mid values have the same sign.
                    // Search the right half.
                    xmin = xmid;
                    g_xmin = g_xmid;
                }
                else if (sgn_mid == sgn_max)
                {
                    // The max and mid values have the same sign.
                    // Search the left half.
                    xmax = xmid;
                    g_xmax = g_xmid;
                }
                else
                {
                    // The three values have different signs.
                    // Assume min or max is NaN.
                    if (sgn_min == sgn_nan)
                    {
                        // Value g_xmin is NaN. Use the right half.
                        xmin = xmid;
                        g_xmin = g_xmid;
                    }
                    else if (sgn_max == sgn_nan)
                    {
                        // Value g_xmax is NaN. Use the right half.
                        xmax = xmid;
                        g_xmax = g_xmid;
                    }
                    else
                    {
                        // This is a weird case. Just trap it.
                        throw new InvalidOperationException(
                            "Unexpected difference curve. " +
                            "Cannot find a root between X = " +
                            xmin + " and X = " + xmax);
                    }
                }
            }

            if (IsNumber(g_xmid) && (Abs(g_xmid) < small))
            {
                x = xmid;
                y = g_xmid;
            }
            else if (IsNumber(g_xmin) && (Abs(g_xmin) < small))
            {
                x = xmin;
                y = g_xmin;
            }
            else if (IsNumber(g_xmax) && (Abs(g_xmax) < small))
            {
                x = xmax;
                y = g_xmax;
            }
            else
            {
                x = xmid;
                y = double.NaN;
            }
        }

        /// <summary>
        /// Get an ellipse's points from its equation.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="E"></param>
        /// <param name="F"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static List<Point2D> GetPointsFromEquation(double xmin, double xmax,
            double A, double B, double C, double D, double E, double F)
        {
            List<Point2D> points = new List<Point2D>();
            for (double x = xmin; x <= xmax; x++)
            {
                double y = G1(A, B, C, D, E, F, x, +1f);
                if (IsNumber(y)) points.Add(new Point2D(x, y));
            }
            for (double x = xmax; x >= xmin; x--)
            {
                double y = G1(A, B, C, D, E, F, x, -1f);
                if (IsNumber(y)) points.Add(new Point2D(x, y));
            }
            return points;
        }

        /// <summary>
        /// Get points representing the difference between the two ellipses' equations. 
        /// </summary>
        /// <param name="xmin1"></param>
        /// <param name="xmax1"></param>
        /// <param name="xmin2"></param>
        /// <param name="xmax2"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static List<List<Point2D>> GetDifferencePoints(
            double xmin1, double xmax1,
            double xmin2, double xmax2,
            double A1, double B1, double C1, double D1, double E1, double F1,
            double A2, double B2, double C2, double D2, double E2, double F2)
        {
            double xmin = Min(xmin1, xmin2);
            double xmax = Max(xmax1, xmax2);
            List<List<Point2D>> result = new List<List<Point2D>>();

            double[] signs = { -1f, +1f };
            foreach (double sign1 in signs)
            {
                foreach (double sign2 in signs)
                {
                    List<Point2D> points = new List<Point2D>();
                    result.Add(points);
                    for (double x = xmin; x <= xmax; x++)
                    {
                        double y1 = G1(A1, B1, C1, D1, E1, F1, x, sign1);
                        if (IsNumber(y1))
                        {
                            double y2 = G1(A2, B2, C2, D2, E2, F2, x, sign2);
                            if (IsNumber(y2)) points.Add(new Point2D(x, y1 - y2));
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Find tangents to the difference functions.
        /// </summary>
        /// <param name="eis"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static void FindDifferenceTangents(EllipseIntersectStuff eis)
        {
            eis.TangentCenters = new List<Point2D>();
            eis.TangentP1 = new List<Point2D>();
            eis.TangentP2 = new List<Point2D>();

            if (!eis.GotEllipse1 || !eis.GotEllipse2) return;

            const double tangent_length = 50;

            //++
            double tangent_y = G(eis.TangentX,
                eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f,
                eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
            if (IsNumber(tangent_y))
            {
                double slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f) -
                    G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
                if (IsNumber(slope))
                {
                    double delta_x = Sqrt(
                        tangent_length * tangent_length / (1 + slope * slope)) / 2;
                    eis.TangentCenters.Add(new Point2D(eis.TangentX, tangent_y));
                    eis.TangentP1.Add(new Point2D(eis.TangentX - delta_x, tangent_y - slope * delta_x));
                    eis.TangentP2.Add(new Point2D(eis.TangentX + delta_x, tangent_y + slope * delta_x));
                }
            }

            //+-
            tangent_y = G(eis.TangentX,
                eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f,
                eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
            if (IsNumber(tangent_y))
            {
                double slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f) -
                    G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
                if (IsNumber(slope))
                {
                    double delta_x = Sqrt(
                        tangent_length * tangent_length / (1 + slope * slope)) / 2;
                    eis.TangentCenters.Add(new Point2D(eis.TangentX, tangent_y));
                    eis.TangentP1.Add(new Point2D(eis.TangentX - delta_x, tangent_y - slope * delta_x));
                    eis.TangentP2.Add(new Point2D(eis.TangentX + delta_x, tangent_y + slope * delta_x));
                }
            }

            //-+
            tangent_y = G(eis.TangentX,
                eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f,
                eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
            if (IsNumber(tangent_y))
            {
                double slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f) -
                    G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
                if (IsNumber(slope))
                {
                    double delta_x = Sqrt(
                        tangent_length * tangent_length / (1 + slope * slope)) / 2;
                    eis.TangentCenters.Add(new Point2D(eis.TangentX, tangent_y));
                    eis.TangentP1.Add(new Point2D(eis.TangentX - delta_x, tangent_y - slope * delta_x));
                    eis.TangentP2.Add(new Point2D(eis.TangentX + delta_x, tangent_y + slope * delta_x));
                }
            }

            //--
            tangent_y = G(eis.TangentX,
                eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f,
                eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
            if (IsNumber(tangent_y))
            {
                double slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f) -
                    G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
                if (IsNumber(slope))
                {
                    double delta_x = Sqrt(
                        tangent_length * tangent_length / (1 + slope * slope)) / 2;
                    eis.TangentCenters.Add(new Point2D(eis.TangentX, tangent_y));
                    eis.TangentP1.Add(new Point2D(eis.TangentX - delta_x, tangent_y - slope * delta_x));
                    eis.TangentP2.Add(new Point2D(eis.TangentX + delta_x, tangent_y + slope * delta_x));
                }
            }
        }

        /// <summary>
        /// Get the equation for this ellipse.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="Dx"></param>
        /// <param name="Dy"></param>
        /// <param name="Rx"></param>
        /// <param name="Ry"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="E"></param>
        /// <param name="F"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static void GetEllipseFormula(Rectangle2D rect,
            out double Dx, out double Dy, out double Rx, out double Ry,
            out double A, out double B, out double C, out double D,
            out double E, out double F)
        {
            Dx = rect.X + rect.Width / 2f;
            Dy = rect.Y + rect.Height / 2f;
            Rx = rect.Width / 2f;
            Ry = rect.Height / 2f;

            A = 1f / Rx / Rx;
            B = 0;
            C = 1f / Ry / Ry;
            D = -2f * Dx / Rx / Rx;
            E = -2f * Dy / Ry / Ry;
            F = Dx * Dx / Rx / Rx + Dy * Dy / Ry / Ry - 1;

            // Verify the parameters.
            Console.WriteLine();
            double xmid = rect.Left + rect.Width / 2f;
            double ymid = rect.Top + rect.Height / 2f;
            VerifyEquation(A, B, C, D, E, F, rect.Left, ymid);
            VerifyEquation(A, B, C, D, E, F, rect.Right, ymid);
            VerifyEquation(A, B, C, D, E, F, xmid, rect.Top);
            VerifyEquation(A, B, C, D, E, F, xmid, rect.Bottom);
        }

        /// <summary>
        /// Verify that the equation gives a value close to 0 for the given point (x, y).
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="E"></param>
        /// <param name="F"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static void VerifyEquation(double A, double B, double C, double D, double E, double F, double x, double y)
        {
            double total = A * x * x + B * x * y + C * y * y + D * x + E * y + F;
            Console.WriteLine("VerifyEquation (" + x + ", " + y + ") = " + total);
            Debug.Assert(Abs(total) < 0.001f);
        }

        /// <summary>
        /// Calculate G1(x). root_sign is -1 or 1.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="E"></param>
        /// <param name="F"></param>
        /// <param name="root_sign"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static double G1(double x, double A, double B, double C, double D, double E, double F, double root_sign)
        {
            double result = B * x + E;
            result = result * result;
            result = result - 4 * C * (A * x * x + D * x + F);
            result = root_sign * Sqrt(result);
            result = -(B * x + E) + result;
            result = result / 2 / C;

            return result;
        }

        /// <summary>
        /// Calculate G1'(x). root_sign is -1 or 1.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="E"></param>
        /// <param name="F"></param>
        /// <param name="root_sign"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static double G1Prime(double x, double A, double B, double C, double D, double E, double F, double root_sign)
        {
            double numerator = 2 * (B * x + E) * B - 4 * C * (2 * A * x + D);
            double denominator = 2 * Sqrt((B * x + E) * (B * x + E) - 4 * C * (A * x * x + D * x + F));
            double result = -B + root_sign * numerator / denominator;
            result = result / 2 / C;
            return result;
        }

        /// <summary>
        /// Calculate G(x).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static double G(double x,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            return
                G1(x, A1, B1, C1, D1, E1, F1, sign1) -
                G1(x, A2, B2, C2, D2, E2, F2, sign2);
        }

        /// <summary>
        /// Calculate G'(x).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static double GPrime(double x,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            return
                G1Prime(x, A1, B1, C1, D1, E1, F1, sign1) -
                G1Prime(x, A2, B2, C2, D2, E2, F2, sign2);
        }

        /// <summary>
        /// Return true if the number is not infinity or NaN.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static bool IsNumber(double number)
        {
            return !(double.IsNaN(number) || double.IsInfinity(number));
        }

        #endregion

        #region Intersection of Ellipse and line

        /// <summary>
        /// Finds the Intersection of a Ellipse and a Line
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static LineSegment Intersect(Ellipse ellipse, LineSegment line)
        {
            double SlopeA = line.Slope();
            double SlopeB = (line.A.Y - (SlopeA * line.A.X));
            double A = (1 + (SlopeA * SlopeA));
            double B = ((2 * (SlopeA * (SlopeB - ellipse.Center.Y))) - (2 * ellipse.Center.X));
            double C = ((ellipse.Center.X * ellipse.Center.X) + (((SlopeB - ellipse.Center.Y) * (SlopeB - ellipse.Center.X)) - (ellipse.MajorRadius * ellipse.MajorRadius)));
            double XA = ((((B * -1) + Sqrt(((B * B) - (A * C)))) / (2 * A)));
            double XB = ((((B - Sqrt(((B * B) - (A * C)))) * -1) / (2 * A)));
            double YA = ((SlopeA * XA) + SlopeB);
            double YB = ((SlopeA * XB) + SlopeB);
            return new LineSegment(XA, YA, XB, YB);
        }

        #endregion

        #region Intersection of Parabola and Hyperbola
        //http://csharphelper.com/blog/2014/11/see-where-a-parabola-and-hyperbola-intersect-in-c/
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

        #region Line in Polyline

        /// <summary>
        /// This function should be called with the full set of *all* relevant polygons.
        /// (The algorithm automatically knows that enclosed polygons are “no-go” areas.)
        /// Note:  As much as possible, this algorithm tries to return YES when the
        /// test line-segment is exactly on the border of the polygon, particularly
        /// if the test line-segment *is* a side of a polygon.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>
        /// Public-domain code by Darel Rex Finley, 2006.
        /// http://alienryderflex.com/shortest_path/
        /// </remarks>
        public static bool LineInPolygon(Point2D start, Point2D end, Polygon polygon)
        {
            int i;
            int j;
            double sX;
            double sY;
            double eX;
            double eY;
            double rotSX;
            double rotSY;
            double rotEX;
            double rotEY;
            double crossX;

            end.X -= start.X;
            end.Y -= start.Y;
            double dist = Sqrt(end.X * end.X + end.Y * end.Y);
            double theCos = end.X / dist;
            double theSin = end.Y / dist;
            for (i = 0; i < polygon.Points.Count; i++)
            {
                j = i + 1;
                if (j == polygon.Points.Count) j = 0;

                sX = polygon.Points[i].X - start.X;
                sY = polygon.Points[i].Y - start.Y;
                eX = polygon.Points[j].X - start.X;
                eY = polygon.Points[j].Y - start.Y;
                if (sX == 0.0 && sY == 0.0 && eX == end.X && eY == end.Y
                || eX == 0.0 && eY == 0.0 && sX == end.X && sY == end.Y)
                {
                    return true;
                }

                rotSX = sX * theCos + sY * theSin;
                rotSY = sY * theCos - sX * theSin;
                rotEX = eX * theCos + eY * theSin;
                rotEY = eY * theCos - eX * theSin;
                if (rotSY < 0.0 && rotEY > 0.0
                || rotEY < 0.0 && rotSY > 0.0)
                {
                    crossX = rotSX + (rotEX - rotSX) * (0.0 - rotSY) / (rotEY - rotSY);
                    if (crossX >= 0.0 && crossX <= dist) return false;
                }

                if (rotSY == 0.0 && rotEY == 0.0
                && (rotSX >= 0.0 || rotEX >= 0.0)
                && (rotSX <= dist || rotEX <= dist)
                && (rotSX < 0.0 || rotEX < 0.0
                || rotSX > dist || rotEX > dist))
                {
                    return false;
                }
            }

            return polygon.Contains(new Point2D(start.X + end.X / 2.0, start.Y + end.Y / 2.0));
        }

        #endregion

        #region Line in Polyline set

        /// <summary>
        /// This function should be called with the full set of *all* relevant polygons.
        /// (The algorithm automatically knows that enclosed polygons are “no-go” areas.)
        /// Note:  As much as possible, this algorithm tries to return YES when the
        /// test line-segment is exactly on the border of the polygon, particularly
        /// if the test line-segment *is* a side of a polygon.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="allPolys"></param>
        /// <returns></returns>
        /// <remarks>
        /// Public-domain code by Darel Rex Finley, 2006.
        /// http://alienryderflex.com/shortest_path/
        /// </remarks>
        public static bool LineInPolygonSet(PolygonSet allPolys, Point2D start, Point2D end)
        {
            double theCos, theSin, dist, sX, sY, eX, eY, rotSX, rotSY, rotEX, rotEY, crossX;
            int i, j, polyI;

            end.X -= start.X;
            end.Y -= start.Y; dist = Sqrt(end.X * end.X + end.Y * end.Y);
            theCos = end.X / dist;
            theSin = end.Y / dist;

            for (polyI = 0; polyI < allPolys.Count; polyI++)
            {
                for (i = 0; i < allPolys.Polygons[polyI].Points.Count; i++)
                {
                    j = i + 1; if (j == allPolys.Polygons[polyI].Points.Count) j = 0;

                    sX = allPolys.Polygons[polyI].Points[i].X - start.X;
                    sY = allPolys.Polygons[polyI].Points[i].Y - start.Y;
                    eX = allPolys.Polygons[polyI].Points[j].X - start.X;
                    eY = allPolys.Polygons[polyI].Points[j].Y - start.Y;
                    if (sX == 0.0 && sY == 0.0 && eX == end.X && eY == end.Y
                    || eX == 0.0 && eY == 0.0 && sX == end.X && sY == end.Y)
                    {
                        return true;
                    }

                    rotSX = sX * theCos + sY * theSin;
                    rotSY = sY * theCos - sX * theSin;
                    rotEX = eX * theCos + eY * theSin;
                    rotEY = eY * theCos - eX * theSin;
                    if (rotSY < 0.0 && rotEY > 0.0

                    || rotEY < 0.0 && rotSY > 0.0)
                    {
                        crossX = rotSX + (rotEX - rotSX) * (0.0 - rotSY) / (rotEY - rotSY);
                        if (crossX >= 0.0 && crossX <= dist) return false;
                    }

                    if (rotSY == 0.0 && rotEY == 0.0

                    && (rotSX >= 0.0 || rotEX >= 0.0)
                    && (rotSX <= dist || rotEX <= dist)
                    && (rotSX < 0.0 || rotEX < 0.0

                    || rotSX > dist || rotEX > dist))
                    {
                        return false;
                    }
                }
            }

            return allPolys.Contains(new Point2D(start.X + end.X / 2.0, start.Y + end.Y / 2.0));
        }

        #endregion

        #region List Interpolation Points of Cubic Bezier 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> InterpolatePoints(CubicBezier bezier, int count)
        {
            Point2D[] ipoints = new Point2D[count + 1];
            for (int i = 0; i <= count; i += 1)
            {
                double v = (1d / count) * i;
                ipoints[i] = bezier.Interpolate(v);
            }

            return new List<Point2D>(ipoints);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> InterpolateCubicBeizerPoints(CubicBezier bezier, int count)
        {
            return InterpolateCubicBeizerPoints(bezier.A, bezier.B, bezier.C, bezier.D, count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="Precision"></param>
        /// <returns></returns>
        private static List<Point2D> InterpolateCubicBeizerPoints(Point2D a, Point2D b, Point2D c, Point2D d, double Precision)
        {
            Point2D[] BPoints = new Point2D[(int)((1 / Precision) + 2)];
            BPoints[0] = a;
            BPoints[BPoints.Length - 1] = d;
            int Node = 0;
            for (double Index = 0; Index < 1; Index += Precision)
            {
                Node++;
                BPoints[Node] = new Point2D(Interpolaters.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, Index));
            }

            return new List<Point2D>(BPoints);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> ComputeBezierInterpolations(CubicBezier bezier, int count)
        {
            return ComputeBezierInterpolations(bezier.A, bezier.B, bezier.C, bezier.D, count);
        }

        /// <summary>
        ///  ComputeBezier fills an array of Point2D structs with the curve points
        ///  generated from the control points cp. Caller must allocate sufficient memory
        ///  for the result, which is [sizeof(Point2D) * numberOfPoints]
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="numberOfPoints"></param>
        private static List<Point2D> ComputeBezierInterpolations(Point2D a, Point2D b, Point2D c, Point2D d, int numberOfPoints)
        {
            List<Point2D> curve = new List<Point2D>();
            double t = 0;
            double dt = (1.0d / (numberOfPoints - 1));
            for (int i = 0; (i <= numberOfPoints); i++)
            {
                t += dt;
                curve.Add(new Point2D(Interpolaters.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, t)));
            }
            return curve;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> InterpolateCubicBeizerPoints0(CubicBezier bezier, int count)
        {
            return InterpolateCubicBeizerPoints0(bezier.A, bezier.B, bezier.C, bezier.D, count);
        }

        /// <summary>
        /// Function to Plot a Cubic Bezier
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="Precision"></param>
        /// <returns></returns>
        private static List<Point2D> InterpolateCubicBeizerPoints0(Point2D a, Point2D b, Point2D c, Point2D d, double Precision)
        {
            Point2D[] BPoints = new Point2D[(int)((1 / Precision) + 2)];
            BPoints[0] = a;
            BPoints[BPoints.Length - 1] = d;
            int Node = 0;
            for (double Index = 0; (Index <= 1); Index = (Index + Precision))
            {
                Node++;
                BPoints[Node] = new Point2D(Interpolaters.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, Index));
            }

            return new List<Point2D>(BPoints);
        }

        #endregion

        #region List Interpolation points of Quadratic Bezier

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> InterpolateQuadraticBezierPoints(QuadraticBezier bezier, int count)
        {
            Point2D[] ipoints = new Point2D[count + 1];
            for (int i = 0; i <= count; i += 1)
            {
                double v = (1f / count) * i;
                ipoints[i] = bezier.Interpolate(v);
            }

            return new List<Point2D>(ipoints);
        }

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
                new SpeedTester(() => PointInCircle(0, 0, 2, 3, 3),
                $"{nameof(Experiments.PointInCircle)}(0, 0, 2, 3, 3)"),
                new SpeedTester(() => PointInCircleInline(0, 0, 2, 1, 1),
                $"{nameof(Experiments.PointInCircleInline)}(0, 0, 2, 1, 1)"),
                new SpeedTester(() => PointInCircleInline(0, 0, 2, 3, 3),
                $"{nameof(Experiments.PointInCircleInline)}(0, 0, 2, 3, 3)"),
                new SpeedTester(() => PointInCirclePhilcolbourn(0, 0, 2, 1, 1),
                $"{nameof(Experiments.PointInCirclePhilcolbourn)}(0, 0, 2, 1, 1)"),
                new SpeedTester(() => PointInCirclePhilcolbourn(0, 0, 2, 3, 3),
                $"{nameof(Experiments.PointInCirclePhilcolbourn)}(0, 0, 2, 3, 3)"),
                new SpeedTester(() => PointInCircleNPhilcolbourn(0, 0, 2, 1, 1),
                $"{nameof(Experiments.PointInCircleNPhilcolbourn)}(0, 0, 2, 1, 1)"),
                new SpeedTester(() => PointInCircleNPhilcolbourn(0, 0, 2, 3, 3),
                $"{nameof(Experiments.PointInCircleNPhilcolbourn)}(0, 0, 2, 3, 3)"),
                new SpeedTester(() => PointInCircleWilliamMorrison(0, 0, 2, 1, 1),
                $"{nameof(Experiments.PointInCircleWilliamMorrison)}(0, 0, 2, 1, 1)"),
                new SpeedTester(() => PointInCircleWilliamMorrison(0, 0, 2, 3, 3),
                $"{nameof(Experiments.PointInCircleWilliamMorrison)}(0, 0, 2, 3, 3)"),
                new SpeedTester(() => PointInCircleX(0, 0, 2, 1, 1),
                $"{nameof(Experiments.PointInCircleX)}(0, 0, 2, 1, 1)"),
                new SpeedTester(() => PointInCircleX(0, 0, 2, 3, 3),
                $"{nameof(Experiments.PointInCircleX)}(0, 0, 2, 3, 3)"),
            };
        }

        /// <summary>
        /// Find out if a Point is in a Circle. 
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static InsideOutside PointInCircle(
            double centerX, double centerY,
            double radius,
            double x, double y)
        {
            double distance = Distance2D_0(centerX, centerY, x, y);
            return (radius >= distance) ? ((radius == distance) ? InsideOutside.Boundary : InsideOutside.Inside) : InsideOutside.Outside;
        }

        /// <summary>
        /// Find out if a Point is in a Circle. 
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static InsideOutside PointInCircleInline(
            double centerX, double centerY,
            double radius,
            double x, double y)
        {
            double distance = Sqrt((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY));
            return (radius >= distance) ? ((radius == distance) ? InsideOutside.Boundary : InsideOutside.Inside) : InsideOutside.Outside;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/481144/equation-for-testing-if-a-point-is-inside-a-circle</remarks>
        public static InsideOutside PointInCirclePhilcolbourn(
            double centerX,
            double centerY,
            double radius,
            double x,
            double y)
        {
            double dx = Abs(x - centerX);
            if (dx > radius) return InsideOutside.Outside;
            double dy = Abs(y - centerY);
            if (dy > radius) return InsideOutside.Outside;
            //if (dx + dy <= radius) return InsideOutside.Inside;
            double distanceSquared = dx * dx + dy * dy;
            double radiusSquared = radius * radius;
            return (radiusSquared >= distanceSquared) ? ((radiusSquared == distanceSquared) ? InsideOutside.Boundary : InsideOutside.Inside) : InsideOutside.Outside;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/481144/equation-for-testing-if-a-point-is-inside-a-circle</remarks>
        public static InsideOutside PointInCircleNPhilcolbourn(
            double centerX,
            double centerY,
            double radius,
            double x,
            double y)
        {
            double dx = Abs(x - centerX);
            double dy = Abs(y - centerY);
            double distanceSquared = dx * dx + dy * dy;
            double radiusSquared = radius * radius;
            return (radiusSquared >= distanceSquared) ? ((radiusSquared == distanceSquared) ? InsideOutside.Boundary : InsideOutside.Inside) : InsideOutside.Outside;
        }

        /// <summary>
        /// test if coordinate (x, y) is within a radius from coordinate (centerX, centerY)
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/481144/equation-for-testing-if-a-point-is-inside-a-circle</remarks>
        public static InsideOutside PointInCircleWilliamMorrison(
            double centerX,
            double centerY,
            double radius,
            double x,
            double y)
        {
            if (x >= centerX - radius && x <= centerX + radius &&
                y >= centerY - radius && y <= centerY + radius)
            {
                double dx = centerX - x;
                double dy = centerY - y;
                dx *= dx;
                dy *= dy;
                double distanceSquared = dx + dy;
                double radiusSquared = radius * radius;
                return (radiusSquared >= distanceSquared) ? ((radiusSquared == distanceSquared) ? InsideOutside.Boundary : InsideOutside.Inside) : InsideOutside.Outside;
            }
            return InsideOutside.Outside;
        }

        /// <summary>
        /// test if coordinate (x, y) is within a radius from coordinate (centerX, centerY)
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static InsideOutside PointInCircleX(
            double centerX,
            double centerY,
            double radius,
            double x,
            double y)
        {
            if (x >= centerX - radius && x <= centerX + radius &&
                y >= centerY - radius && y <= centerY + radius)
            {
                double dx = ((centerX > x) ? (x - centerX) : (centerX - x));
                double dy = ((centerY > y) ? (y - centerY) : (centerY - y));
                if (dx > radius || dy > radius) return InsideOutside.Outside;
                dx *= dx;
                dy *= dy;
                double distanceSquared = dx + dy;
                double radiusSquared = radius * radius;
                return (radiusSquared >= distanceSquared) ? ((radiusSquared == distanceSquared) ? InsideOutside.Boundary : InsideOutside.Inside) : InsideOutside.Outside;
            }
            return InsideOutside.Outside;
        }

        #endregion

        #region Point in Ellipse

        /// <summary>
        /// Checks whether a point is found within the boundaries of an ellipse.
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </remarks>
        [Pure]
        [DebuggerStepThrough]
        public static InsideOutside PointInEllipse(Ellipse ellipse, Point2D point)
        {
            if (ellipse.R1 <= 0d || ellipse.R2 <= 0d) return InsideOutside.Outside;

            double cosT = Cos(-ellipse.Angle);
            double sinT = Sin(-ellipse.Angle);

            double u = point.X - ellipse.Center.X;
            double v = point.Y - ellipse.Center.Y;

            double a = (cosT * u + sinT * v) * (cosT * u + sinT * v);
            double b = (sinT * u - cosT * v) * (sinT * u - cosT * v);

            double d1Squared = 4 * ellipse.R1 * ellipse.R1;
            double d2Squared = 4 * ellipse.R2 * ellipse.R2;

            double normalizedRadius = (a / d1Squared)
                                    + (b / d2Squared);

            return (normalizedRadius <= 1d) ? ((normalizedRadius == 1d) ? InsideOutside.Boundary : InsideOutside.Inside) : InsideOutside.Outside;
        }

        #endregion

        #region Point in Ellipse Unrotated

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool UnrotatedEllipseContainsPoint(Ellipse ellipse, Point2D point)
        {
            if (ellipse.R1 <= 0d || ellipse.R2 <= 0d) return false;

            double u = point.X - ellipse.Center.X;
            double v = point.Y - ellipse.Center.Y;

            double a = u * u;
            double b = u * u;

            double d1Squared = ellipse.R1 * ellipse.R1;
            double d2Squared = ellipse.R2 * ellipse.R2;

            return (a / d1Squared)
                 + (b / d2Squared) <= 1d;
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
                //new SpeedTester(() => PointInPolygonDarelRexFinley(polygon, point),
                //$"{nameof(Experiments.PointInPolygonDarelRexFinley)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonNathanMercer(polygon, point),
                //$"{nameof(Experiments.PointInPolygonNathanMercer)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonLaschaLagidse(polygon, point),
                //$"{nameof(Experiments.PointInPolygonLaschaLagidse)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonPatrickMullen(polygon, point, PatrickMullenValues.Item1, PatrickMullenValues.Item2),
                //$"{nameof(Experiments.PointInPolygonPatrickMullen)}(polygon, {point}, constant, multiple)"),
                //new SpeedTester(() => PointInPolygonMeowNET(polygon, point),
                //$"{nameof(Experiments.PointInPolygonMeowNET)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonAlienRyderFlex(polygon, point),
                //$"{nameof(Experiments.PointInPolygonAlienRyderFlex)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonLaschaLagidse2(polygon, point),
                //$"{nameof(Experiments.PointInPolygonLaschaLagidse2)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonGilKr(polygon, point),
                //$"{nameof(Experiments.PointInPolygonGilKr)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonMKatzWRandolphFranklin(polygon, point),
                //$"{nameof(Experiments.PointInPolygonMKatzWRandolphFranklin)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonRodStephens(polygon, point),
                //$"{nameof(Experiments.PointInPolygonRodStephens)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonSaeedAmiri(polygon, point),
                //$"{nameof(Experiments.PointInPolygonSaeedAmiri)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonKeith(polygon, point),
                //$"{nameof(Experiments.PointInPolygonKeith)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonJerryKnauss(polygon, point),
                //$"{nameof(Experiments.PointInPolygonJerryKnauss)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonJerryKnauss2(polygon, point),
                //$"{nameof(Experiments.PointInPolygonJerryKnauss2)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonPaulBourke(polygon, point),
                //$"{nameof(Experiments.PointInPolygonPaulBourke)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonWRandolphFranklin(polygon, point),
                //$"{nameof(Experiments.PointInPolygonWRandolphFranklin)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonPhilippeReverdy(polygon, point),
                //$"{nameof(Experiments.PointInPolygonPhilippeReverdy)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonBobStein(polygon, point),
                //$"{nameof(Experiments.PointInPolygonBobStein)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonHormannAgathos(polygon, point),
                $"{nameof(Experiments.PointInPolygonHormannAgathos)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonHormannAgathosX(polygon, point),
                $"{nameof(Experiments.PointInPolygonHormannAgathosX)}(polygon, {point})"),
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
                if (((
                    (polygon[j].Y < point.Y)
                    && (point.Y < polygon[i].Y))
                    || ((polygon[i].Y < point.Y)
                    && (point.Y < polygon[j].Y)))
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
                if (
                    polygon[i].Y < point.Y
                    && polygon[j].Y >= point.Y
                    || polygon[j].Y < point.Y
                    && polygon[i].Y >= point.Y)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static InsideOutside PointInPolygonHormannAgathos(List<PointF> polygon, PointF point)
        {
            // returns 0 if false, +1 if true, -1 if pt ON polygon boundary
            // See "The Point in Polygon Problem for Arbitrary Polygons" by Hormann & Agathos
            // http://www.inf.usi.ch/hormann/papers/Hormann.2001.TPI.pdf
            InsideOutside result = InsideOutside.Outside;

            // If the polygon has 2 or fewer points, it is a line or point and has no interior. 
            if (polygon.Count < 3) return InsideOutside.Outside;
            PointF curPoint = polygon[0];
            for (int i = 1; i <= polygon.Count; ++i)
            {
                PointF nextPoint = (i == polygon.Count ? polygon[0] : polygon[i]);
                if (nextPoint.Y == point.Y)
                {
                    if ((nextPoint.X == point.X) || (curPoint.Y == point.Y
                    && ((nextPoint.X > point.X) == (curPoint.X < point.X))))
                    {
                        return InsideOutside.Boundary;
                    }
                }

                if ((curPoint.Y < point.Y) != (nextPoint.Y < point.Y))
                {
                    if (curPoint.X >= point.X)
                    {
                        if (nextPoint.X > point.X)
                        {
                            result = 1 - result;
                        }
                        else
                        {
                            double determinant = (curPoint.X - point.X) * (nextPoint.Y - point.Y) - (nextPoint.X - point.X) * (curPoint.Y - point.Y);
                            if (determinant == 0)
                            {
                                return InsideOutside.Boundary;
                            }
                            else if ((determinant > 0) == (nextPoint.Y > curPoint.Y)) result = 1 - result;
                        }
                    }
                    else
                    {
                        if (nextPoint.X > point.X)
                        {
                            double determinant = (curPoint.X - point.X) * (nextPoint.Y - point.Y) - (nextPoint.X - point.X) * (curPoint.Y - point.Y);
                            if (determinant == 0)
                            {
                                return InsideOutside.Boundary;
                            }
                            else if ((determinant > 0) == (nextPoint.Y > curPoint.Y)) result = 1 - result;
                        }
                    }
                }

                curPoint = nextPoint;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static InsideOutside PointInPolygonHormannAgathosX(List<PointF> polygon, PointF point)
        {
            // returns 0 if false, +1 if true, -1 if pt ON polygon boundary
            // See "The Point in Polygon Problem for Arbitrary Polygons" by Hormann & Agathos
            // http://www.inf.usi.ch/hormann/papers/Hormann.2001.TPI.pdf
            InsideOutside result = InsideOutside.Outside;

            // If the polygon has 2 or fewer points, it is a line or point and has no interior. 
            if (polygon.Count < 3) return InsideOutside.Outside;
            PointF curPoint = polygon[0];
            PointF nextPoint = polygon[1];
            for (int i = 1; i <= polygon.Count; ++i)
            {
                nextPoint = (i == polygon.Count ? polygon[0] : polygon[i]);
                if (nextPoint.Y == point.Y)
                {
                    if ((nextPoint.X == point.X) || (curPoint.Y == point.Y
                    && ((nextPoint.X > point.X) == (curPoint.X < point.X))))
                    {
                        return InsideOutside.Boundary;
                    }
                }

                if ((curPoint.Y < point.Y) != (nextPoint.Y < point.Y))
                {
                    if (curPoint.X >= point.X)
                    {
                        if (nextPoint.X > point.X)
                        {
                            result = 1 - result;
                        }
                        else
                        {
                            double determinant = (curPoint.X - point.X) * (nextPoint.Y - point.Y) - (nextPoint.X - point.X) * (curPoint.Y - point.Y);
                            if (determinant == 0)
                            {
                                return InsideOutside.Boundary;
                            }
                            else if ((determinant > 0) == (nextPoint.Y > curPoint.Y))
                            {
                                result = 1 - result;
                            }
                        }
                    }
                    else
                    {
                        if (nextPoint.X > point.X)
                        {
                            double determinant = (curPoint.X - point.X) * (nextPoint.Y - point.Y) - (nextPoint.X - point.X) * (curPoint.Y - point.Y);
                            if (determinant == 0)
                            {
                                return InsideOutside.Boundary;
                            }
                            else if ((determinant > 0) == (nextPoint.Y > curPoint.Y))
                            {
                                result = 1 - result;
                            }
                        }
                    }
                }

                curPoint = nextPoint;
            }

            return result;
        }

        #endregion

        #region Point in Polygon set

        /// <summary>
        /// This function automatically knows that enclosed polygons are "no-go" areas.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygons"></param>
        /// <returns></returns>
        /// <remarks>
        /// Public-domain code by Darel Rex Finley, 2006.
        /// http://alienryderflex.com/shortest_path/
        /// </remarks>
        public static bool PointInPolygonSet(PolygonSet polygons, Point2D point)
        {
            bool oddNodes = false;
            int j;

            for (int polyI = 0; polyI < polygons.Count; polyI++)
            {
                for (int i = 0; i < polygons.Polygons[polyI].Points.Count; i++)
                {
                    j = i + 1;
                    if (j == polygons.Polygons[polyI].Points.Count) j = 0;
                    if (polygons.Polygons[polyI].Points[i].Y < point.Y
                    && polygons.Polygons[polyI].Points[j].Y >= point.Y
                    || polygons.Polygons[polyI].Points[j].Y < point.Y
                    && polygons.Polygons[polyI].Points[i].Y >= point.Y)
                    {
                        if (polygons.Polygons[polyI].Points[i].X + (point.Y - polygons.Polygons[polyI].Points[i].Y)
                        / (polygons.Polygons[polyI].Points[j].Y - polygons.Polygons[polyI].Points[i].Y)
                        * (polygons.Polygons[polyI].Points[j].X - polygons.Polygons[polyI].Points[i].X) < point.X)
                        {
                            oddNodes = !oddNodes;
                        }
                    }
                }
            }

            return oddNodes;
        }

        /// <summary>
        /// This function automatically knows that enclosed polygons are "no-go" areas.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygons"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool PointInPolygonSetShkyrockett(PolygonSet polygons, Point2D point)
        {
            bool returnValue = false;

            foreach (var poly in polygons.Polygons)
            {
                returnValue = !poly.Points.Contains(point);
            }

            return returnValue;
        }

        #endregion

        #region Point in Rectangle

        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [Pure]
        //[DebuggerStepThrough]
        public static InsideOutside Contains(Rectangle2D rectangle, Point2D point)
        {
            return (rectangle.X <= point.X
                && point.X < rectangle.X + rectangle.Width
                && rectangle.Y <= point.Y
                && point.Y < rectangle.Y + rectangle.Height) ? InsideOutside.Inside : InsideOutside.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [Pure]
        //[DebuggerStepThrough]
        public static InsideOutside Contains2(Rectangle2D rectangle, Point2D point)
        {
            if (((rectangle.X == point.X || rectangle.Bottom == point.X) && ((rectangle.Y <= point.Y) == (rectangle.Bottom >= point.Y)))
             || ((rectangle.Right == point.Y || rectangle.Left == point.Y) && ((rectangle.X <= point.X) == (rectangle.Right >= point.X))))
            {
                return InsideOutside.Boundary;
            }

            return (rectangle.X <= point.X
                && point.X < rectangle.X + rectangle.Width
                && rectangle.Y <= point.Y
                && point.Y < rectangle.Y + rectangle.Height) ? InsideOutside.Inside : InsideOutside.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [Pure]
        //[DebuggerStepThrough]
        public static InsideOutside PointOnRectangleX(Rectangle2D rectangle, Point2D point)
        {
            double top = Sqrt((rectangle.TopRight.X - rectangle.TopLeft.X) * (rectangle.TopRight.X - rectangle.TopLeft.X) + (rectangle.TopRight.Y - rectangle.TopLeft.Y) * (rectangle.TopRight.Y - rectangle.TopLeft.Y));
            double right = Sqrt((rectangle.BottomRight.X - rectangle.TopRight.X) * (rectangle.BottomRight.X - rectangle.TopRight.X) + (rectangle.BottomRight.Y - rectangle.TopRight.Y) * (rectangle.BottomRight.Y - rectangle.TopRight.Y));
            double tlp = (point.X - rectangle.TopLeft.X) * (point.X - rectangle.TopLeft.X) + (point.Y - rectangle.TopLeft.Y) * (point.Y - rectangle.TopLeft.Y);
            double trp = (point.X - rectangle.TopRight.X) * (point.X - rectangle.TopRight.X) + (point.Y - rectangle.TopRight.Y) * (point.Y - rectangle.TopRight.Y);
            double brp = (point.X - rectangle.BottomRight.X) * (point.X - rectangle.BottomRight.X) + (point.Y - rectangle.BottomRight.Y) * (point.Y - rectangle.BottomRight.Y);
            double blp = (point.X - rectangle.BottomLeft.X) * (point.X - rectangle.BottomLeft.X) + (point.Y - rectangle.BottomLeft.Y) * (point.Y - rectangle.BottomLeft.Y);

            if (top == Sqrt(tlp - trp) || right == Sqrt(trp - brp) || top == Sqrt(brp - blp) || right == Sqrt(blp - tlp)) return InsideOutside.Boundary;

            return (rectangle.X <= point.X
                && point.X < rectangle.X + rectangle.Width
                && rectangle.Y <= point.Y
                && point.Y < rectangle.Y + rectangle.Height) ? InsideOutside.Inside : InsideOutside.Outside;
        }

        #endregion

        #region Point Near Ellipse

        /// <summary>
        /// Return True if the point is inside the ellipse
        /// (expanded by distance close_distance vertically
        /// and horizontally).
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="close_distance"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool PointNearEllipse(double px, double py, double x1, double y1, double x2, double y2, double close_distance)
        {
            double a = ((Abs((x2 - x1)) / 2) + close_distance);
            double b = ((Abs((y2 - y1)) / 2) + close_distance);
            px = (px - (x2 + x1) / 2);
            py = (py - (y2 + y1) / 2);
            return ((((px * px) / (a * a)) + (((py * py) / (b * b))) <= 1));
        }

        #endregion

        #region Point on Line Segment

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segmentAX"></param>
        /// <param name="segmentAY"></param>
        /// <param name="segmentBX"></param>
        /// <param name="segmentBY"></param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <returns></returns>
        public static bool PointOnLineSegment(double segmentAX, double segmentAY, double segmentBX, double segmentBY, double pointX, double pointY)
        {
            return ((pointX == segmentAX) && (pointY == segmentAY)) ||
              ((pointX == segmentBX) && (pointY == segmentBY)) ||
              (((pointX > segmentAX) == (pointX < segmentBX)) &&
              ((pointY > segmentAY) == (pointY < segmentBY)) &&
              ((pointX - segmentAX) * (segmentBY - segmentAY) ==
              (segmentBX - segmentAX) * (pointY - segmentAY)));
        }

        #endregion

        #region Points are Close

        /// <summary>
        /// Return True if (x1, y1) is within close_distance vertically and horizontally of (x2, y2).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool AreClose(double x1, double y1, double x2, double y2, double epsilon = Maths.DoubleEpsilon)
        {
            return (Abs(x2 - x1) <= epsilon) && (Abs(y2 - y1) <= epsilon);
        }

        /// <summary>
        /// Compares two points for fuzzy equality.  This function
        /// helps compensate for the fact that double values can 
        /// acquire error when operated upon
        /// </summary>
        /// <param name='point1'>The first point to compare</param>
        /// <param name='point2'>The second point to compare</param>
        /// <returns>Whether or not the two points are equal</returns>
        public static bool AreClose(Point2D point1, Point2D point2, double epsilon = Maths.DoubleEpsilon)
        {
            return Maths.AreClose(point1.X, point2.X, epsilon) &&
            Maths.AreClose(point1.Y, point2.Y, epsilon);
        }

        /// <summary>
        /// Compares two Size instances for fuzzy equality.  This function
        /// helps compensate for the fact that double values can 
        /// acquire error when operated upon
        /// </summary>
        /// <param name='size1'>The first size to compare</param>
        /// <param name='size2'>The second size to compare</param>
        /// <returns>Whether or not the two Size instances are equal</returns>
        public static bool AreClose(Size2D size1, Size2D size2, double epsilon = Maths.DoubleEpsilon)
        {
            return Maths.AreClose(size1.Width, size2.Width, epsilon) &&
                   Maths.AreClose(size1.Height, size2.Height, epsilon);
        }

        /// <summary>
        /// Compares two Vector instances for fuzzy equality.  This function
        /// helps compensate for the fact that double values can 
        /// acquire error when operated upon
        /// </summary>
        /// <param name='vector1'>The first Vector to compare</param>
        /// <param name='vector2'>The second Vector to compare</param>
        /// <returns>Whether or not the two Vector instances are equal</returns>
        public static bool AreClose(Vector2D vector1, Vector2D vector2, double epsilon = Maths.DoubleEpsilon)
        {
            return Maths.AreClose(vector1.I, vector2.I, epsilon) &&
                   Maths.AreClose(vector1.J, vector2.J, epsilon);
        }

        #endregion

        #region Polygon Area

        /// <summary>
        /// Add the areas of the trapezoids defined by the
        /// polygon's edges dropped to the X-axis. When the
        /// program considers a bottom edge of a polygon, the
        /// calculation gives a negative area so the space
        /// between the polygon and the axis is subtracted,
        /// leaving the polygon's area. This method gives odd
        /// results for non-simple polygons.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns>
        /// Return the polygon's area in "square units."
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/calculate-the-area-of-a-polygon-in-c/</remarks>
        public static double PolygonArea(Polygon polygon)
        {
            // Return the absolute value of the signed area.
            // The signed area is negative if the polygon is
            // oriented clockwise.
            return Abs(SignedPolygonArea(polygon));
        }

        /// <summary>
        /// Add the areas of the trapezoids defined by the
        /// polygon's edges dropped to the X-axis. When the
        /// program considers a bottom edge of a polygon, the
        /// calculation gives a negative area so the space
        /// between the polygon and the axis is subtracted,
        /// leaving the polygon's area. This method gives odd
        /// results for non-simple polygons.
        /// The value will be negative if the polygon is
        /// oriented clockwise.
        /// </summary>
        /// <returns>
        /// Return the polygon's area in "square units."
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/calculate-the-area-of-a-polygon-in-c/</remarks>
        public static double SignedPolygonArea(Polygon polygon)
        {
            // Add the first point to the end.
            int num_points = polygon.Points.Count;
            Point2D[] pts = new Point2D[num_points + 1];
            polygon.Points.CopyTo(pts, 0);
            pts[num_points] = polygon.Points[0];

            // Get the areas.
            double area = 0;
            for (int i = 0; i < num_points; i++)
            {
                area +=
                    (pts[i + 1].X - pts[i].X) *
                    (pts[i + 1].Y + pts[i].Y) / 2;
            }

            // Return the result.
            return area;
        }

        #endregion

        #region Polygon Centroid

        /// <summary>
        /// Find the polygon's centroid.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/find-the-centroid-of-a-polygon-in-c/</remarks>
        public static Point2D Centroid(Polygon polygon)
        {
            // Add the first point at the end of the array.
            int num_points = polygon.Points.Count;
            Point2D[] pts = new Point2D[num_points + 1];
            polygon.Points.CopyTo(pts, 0);
            pts[num_points] = polygon.Points[0];

            // Find the centroid.
            double X = 0;
            double Y = 0;
            double second_factor;
            for (int i = 0; i < num_points; i++)
            {
                second_factor =
                    pts[i].X * pts[i + 1].Y -
                    pts[i + 1].X * pts[i].Y;
                X += (pts[i].X + pts[i + 1].X) * second_factor;
                Y += (pts[i].Y + pts[i + 1].Y) * second_factor;
            }

            // Divide by 6 times the polygon's area.
            double polygon_area = PolygonArea(polygon);
            X /= (6 * polygon_area);
            Y /= (6 * polygon_area);

            // If the values are negative, the polygon is
            // oriented counterclockwise so reverse the signs.
            if (X < 0)
            {
                X = -X;
                Y = -Y;
            }

            return new Point2D(X, Y);
        }

        #endregion

        #region Polygon Oriented Clockwise

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns>
        /// Return true if the polygon is oriented clockwise.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        public static bool PolygonIsOrientedClockwise(Polygon polygon)
        {
            return (SignedPolygonArea(polygon) < 0);
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

        #region Rectangles are Close

        /// <summary>
        /// Compares two rectangles for fuzzy equality.  This function
        /// helps compensate for the fact that double values can 
        /// acquire error when operated upon
        /// </summary>
        /// <param name='rect1'>The first rectangle to compare</param>
        /// <param name='rect2'>The second rectangle to compare</param>
        /// <returns>Whether or not the two rectangles are equal</returns>
        public static bool AreClose(Rectangle2D rect1, Rectangle2D rect2, double epsilon = Maths.DoubleEpsilon)
        {
            // If they're both empty, don't bother with the double logic.
            if (rect1.IsEmpty)
            {
                return rect2.IsEmpty;
            }

            // At this point, rect1 isn't empty, so the first thing we can test is
            // rect2.IsEmpty, followed by property-wise compares.
            return (!rect2.IsEmpty) &&
                Maths.AreClose(rect1.X, rect2.X, epsilon) &&
                Maths.AreClose(rect1.Y, rect2.Y, epsilon) &&
                Maths.AreClose(rect1.Height, rect2.Height, epsilon) &&
                Maths.AreClose(rect1.Width, rect2.Width, epsilon);
        }

        #endregion

        #region Rotated Rectangle Bounds

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="fulcrum"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Rectangle2D RotatedRectangleBounds(double x, double y, double width, double height, Point2D fulcrum, double angle)
        {
            double cosAngle = Abs(Cos(angle));
            double sinAngle = Abs(Sin(angle));

            Size2D size = new Size2D(
                (cosAngle * width) + (sinAngle * height),
                (cosAngle * height) + (sinAngle * width)
                );

            Point2D loc = new Point2D(
                fulcrum.X + ((-width / 2) * cosAngle + (-height / 2) * sinAngle),
                fulcrum.Y + ((-width / 2) * sinAngle + (-height / 2) * cosAngle)
                );

            return new Rectangle2D(loc, size);
        }

        #endregion

        #region Rotated Rectangle Points

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="fulcrum"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static List<Point2D> RotatedRectangleCorners(double x, double y, double width, double height, Point2D fulcrum, double angle)
        {
            List<Point2D> points = new List<Point2D>();

            Point2D xaxis = new Point2D(Cos(angle), Sin(angle));
            Point2D yaxis = new Point2D(-Sin(angle), Cos(angle));

            // Apply the rotation transformation and translate to new center.
            points.Add(new Point2D(
                fulcrum.X + ((-width / 2) * xaxis.X + (-height / 2) * xaxis.Y),
                fulcrum.Y + ((-width / 2) * yaxis.X + (-height / 2) * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrum.X + ((width / 2) * xaxis.X + (-height / 2) * xaxis.Y),
                fulcrum.Y + ((width / 2) * yaxis.X + (-height / 2) * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrum.X + ((width / 2) * xaxis.X + (height / 2) * xaxis.Y),
                fulcrum.Y + ((width / 2) * yaxis.X + (height / 2) * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrum.X + ((-width / 2) * xaxis.X + (height / 2) * xaxis.Y),
                fulcrum.Y + ((-width / 2) * yaxis.X + (height / 2) * yaxis.Y)
                ));

            return points;
        }

        #endregion

        #region Round

        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(RoundTests))]
        public static List<SpeedTester> RoundTests()
        {
            double value = 0.5d;
            return new List<SpeedTester>() {
                new SpeedTester(() => RoundAFZ(value),
                $"{nameof(Experiments.RoundAFZ)}({value})"),
                new SpeedTester(() => RoundToEven(value),
                $"{nameof(Experiments.RoundToEven)}({value})"),
                new SpeedTester(() => RoundToInt32(value),
                $"{nameof(Experiments.RoundToInt32)}({value})"),
                new SpeedTester(() => Round(value),
                $"{nameof(Experiments.Round)}({value})"),
                new SpeedTester(() => Truncate(value),
                $"{nameof(Experiments.Truncate)}({value})"),
            };
        }

        /// <summary>
        /// Away from zero rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        [Pure]
        public static double RoundAFZ(double value, int decimals)
        {
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Away from zero rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        [Pure]
        public static double RoundAFZ(double value)
        {
            return Math.Round(value, 0, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// To Even, or Bankers rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        [Pure]
        public static double RoundToEven(double value)
        {
            return Math.Round(value, 0, MidpointRounding.ToEven);
        }

        /// <summary>
        /// To Even, or Bankers rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double RoundToInt32(double value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Away from zero rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Round(double value)
        {
            return value < 0 ? (int)(value - 0.5) : (int)(value + 0.5);
        }

        /// <summary>
        /// Truncate rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Truncate(double value)
        {
            return (int)value;
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

        #region Shortest Path

        /// <summary>
        /// Finds the shortest path from sX,sY to eX,eY that stays within the polygon set.
        /// Note:  To be safe, the solutionX and solutionY arrays should be large enough
        ///  to accommodate all the corners of your polygon set (although it is
        /// unlikely that anywhere near that many elements will ever be needed).
        /// Returns YES if the optimal solution was found, or NO if there is no solution.
        /// If a solution was found, solutionX and solutionY will contain the coordinates
        /// of the intermediate nodes of the path, in order.  (The startpoint and endpoint
        /// are assumed, and will not be included in the solution.)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="polygons"></param>
        /// <returns></returns>
        /// <remarks>
        /// Public-domain code by Darel Rex Finley, 2006.
        /// http://alienryderflex.com/shortest_path/
        /// </remarks>
        public static Polyline ShortestPath(PolygonSet polygons, Point2D start, Point2D end)
        {
            // (larger than total solution dist could ever be)
            double maxLength = double.MaxValue;// 9999999.0;

            List<TestPoint2D> pointList = new List<TestPoint2D>();
            List<Point2D> solution = new List<Point2D>();

            int pointCount, solutionNodes;

            int treeCount, polyI, i, j, bestI = 0, bestJ;
            double bestDist, newDist;

            //  Fail if either the startpoint or endpoint is outside the polygon set.
            if (!polygons.Contains(start)
            || !polygons.Contains(end))
            {
                return null;
            }

            //  If there is a straight-line solution, return with it immediately.
            if (LineInPolygonSet(polygons, start, end))
            {
                return new Polyline(new List<Point2D>() { start, end });
            }

            //  Build a point list that refers to the corners of the
            //  polygons, as well as to the startpoint and endpoint.
            pointList.Add(start);
            pointCount = 1;
            for (polyI = 0; polyI < polygons.Count; polyI++)
            {
                for (i = 0; i < polygons.Polygons[polyI].Points.Count; i++)
                {
                    pointList.Add(polygons.Polygons[polyI].Points[i]);
                    pointCount++;
                }
            }

            pointList.Add(end);
            pointCount++;

            //  Initialize the shortest-path tree to include just the startpoint.
            treeCount = 1;
            pointList[0].TotalDistance = 0.0;

            //  Iteratively grow the shortest-path tree until it reaches the endpoint
            //  -- or until it becomes unable to grow, in which case exit with failure.
            bestJ = 0;
            while (bestJ < pointCount - 1)
            {
                bestDist = maxLength;
                for (i = 0; i < treeCount; i++)
                {
                    for (j = treeCount; j < pointCount; j++)
                    {
                        if (LineInPolygonSet(polygons, (Point2D)pointList[i], (Point2D)pointList[j]))
                        {
                            newDist = pointList[i].TotalDistance + Primitives.Distance((Point2D)pointList[i], (Point2D)pointList[j]);
                            if (newDist < bestDist)
                            {
                                bestDist = newDist; bestI = i; bestJ = j;
                            }
                        }
                    }
                }

                if (bestDist == maxLength) return null;   //  (no solution)
                pointList[bestJ].Previous = bestI;
                pointList[bestJ].TotalDistance = bestDist;

                // Swap
                TestPoint2D temp = pointList[bestJ];
                pointList[bestJ] = pointList[treeCount];
                pointList[treeCount] = temp;

                treeCount++;
            }

            //  Load the solution arrays.
            solution.Add(start);
            solutionNodes = -1;
            i = treeCount - 1;
            while (i > 0)
            {
                i = pointList[i].Previous;
                solutionNodes++;
            }
            j = solutionNodes - 1;
            i = treeCount - 1;
            while (j >= 0)
            {
                i = pointList[i].Previous;
                solution.Insert(1, (Point2D)pointList[i]);
                j--;
            }
            solution.Add(end);

            //  Success.
            return new Polyline(solution);
        }

        /// <summary>
        /// Finds the shortest path from sX,sY to eX,eY that stays within the polygon set.
        /// Note:  To be safe, the solutionX and solutionY arrays should be large enough
        ///  to accommodate all the corners of your polygon set (although it is
        /// unlikely that anywhere near that many elements will ever be needed).
        /// Returns YES if the optimal solution was found, or NO if there is no solution.
        /// If a solution was found, solutionX and solutionY will contain the coordinates
        /// of the intermediate nodes of the path, in order.  (The startpoint and endpoint
        /// are assumed, and will not be included in the solution.)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="polygons"></param>
        /// <returns></returns>
        /// <remarks>
        /// Public-domain code by Darel Rex Finley, 2006.
        /// http://alienryderflex.com/shortest_path/
        /// </remarks>
        public static Polyline ShortestPathX(PolygonSet polygons, Point2D start, Point2D end)
        {
            // (larger than total solution dist could ever be)
            double maxLength = double.MaxValue;

            List<TestPoint2D> pointList = new List<TestPoint2D>();
            List<Point2D> solution = new List<Point2D>();

            int pointCount;
            int solutionNodes;

            int treeCount;
            int bestI = 0;
            int bestJ;
            double bestDist;
            double newDist;

            //  Fail if either the startpoint or endpoint is outside the polygon set.
            if (!polygons.Contains(start)
            || !polygons.Contains(end))
            {
                return null;
            }

            //  If there is a straight-line solution, return with it immediately.
            if (polygons.Contains(start, end))
            {
                return new Polyline(new List<Point2D>() { start, end });
            }

            //  Build a point list that refers to the corners of the
            //  polygons, as well as to the startpoint and endpoint.
            pointList.Add(start);
            pointCount = 1;
            foreach (var poly in polygons.Polygons)
            {
                foreach (var point in poly.Points)
                {
                    pointList.Add(point);
                    pointCount++;
                }
            }

            pointList.Add(end);
            pointCount++;

            //  Initialize the shortest-path tree to include just the startpoint.
            treeCount = 1;
            pointList[0].TotalDistance = 0.0;

            //  Iteratively grow the shortest-path tree until it reaches the endpoint
            //  -- or until it becomes unable to grow, in which case exit with failure.
            bestJ = 0;
            while (bestJ < pointCount - 1)
            {
                bestDist = maxLength;
                for (int ti = 0; ti < treeCount; ti++)
                {
                    for (int tj = treeCount; tj < pointCount; tj++)
                    {
                        if (polygons.Contains((Point2D)pointList[ti], (Point2D)pointList[tj]))
                        {
                            newDist = pointList[ti].TotalDistance + Primitives.Distance((Point2D)pointList[ti], (Point2D)pointList[tj]);
                            if (newDist < bestDist)
                            {
                                bestDist = newDist; bestI = ti; bestJ = tj;
                            }
                        }
                    }
                }

                if (bestDist == maxLength) return null;   //  (no solution)
                pointList[bestJ].Previous = bestI;
                pointList[bestJ].TotalDistance = bestDist;

                // Swap
                TestPoint2D temp = pointList[bestJ];
                pointList[bestJ] = pointList[treeCount];
                pointList[treeCount] = temp;

                treeCount++;
            }

            //  Load the solution arrays.
            solution.Add(start);
            solutionNodes = -1;
            int i = treeCount - 1;
            while (i > 0)
            {
                i = pointList[i].Previous;
                solutionNodes++;
            }
            int j = solutionNodes - 1;
            i = treeCount - 1;
            while (j >= 0)
            {
                i = pointList[i].Previous;
                solution.Insert(1, (Point2D)pointList[i]);
                j--;
            }
            solution.Add(end);

            //  Success.
            return new Polyline(solution);
        }

        #endregion

        #region Values are Close

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
        public static bool AreClose(double value1, double value2, double epsilon = Maths.DoubleEpsilon)
        {
            // in case they are Infinities (then epsilon check does not work)
            if (value1 == value2) return true;
            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            double eps = (Abs(value1) + Abs(value2) + 10d) * epsilon;
            double delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }

        #endregion

        #region Wrap Point on Rectangle Bounds

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="point"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D WrapPointToRectangle(Rectangle2D bounds, Point2D point, ref Point2D reference)
        {
            if ((point.X <= bounds.X))
            {
                reference = (reference - new Size2D(bounds.X, 0));
                return new Point2D((bounds.Width - 2), point.Y);
            }
            if ((point.Y <= bounds.Y))
            {
                reference = (reference - new Size2D(0, bounds.Y));
                return new Point2D(point.X, (bounds.Height - 2));
            }
            if ((point.X >= (bounds.Width - 1)))
            {
                reference = (reference + new Size2D(bounds.Width, 0));
                return new Point2D((bounds.X + 2), point.Y);
            }
            if ((point.Y >= (bounds.Height - 1)))
            {
                reference = (reference + new Size2D(0, bounds.Height));
                return new Point2D(point.X, (bounds.Y + 2));
            }
            return point;
            // 'ToDo: Adjust My_StartPoint when Screen is wrapped
        }

        #endregion
    }
}
