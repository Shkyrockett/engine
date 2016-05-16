// <copyright file="Maths.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Engine.Geometry
{
    /// <summary>
    /// Extended Math processing library.
    /// </summary>
    public static class Maths
    {
        #region Constants

        /// <summary>
        /// Smallest such that 1.0+DBL_EPSILON != 1.0
        /// </summary>
        public const double DoubleEpsilon = 2.2204460492503131e-016d;

        /// <summary>
        /// Smallest such that 1.0+FLT_EPSILON != 1.0
        /// </summary>
        public const float FloatEpsilon = 1.192092896e-07f;

        /// <summary>
        /// 
        /// </summary>
        public const double NearZeroEpsilon = 1E-20;

        /// <summary>
        /// Number close to zero, where float.MinValue is -float.MaxValue
        /// </summary>
        public const float FloatMin = 1.175494351e-38f;

        /// <summary>
        /// SlopeMax is a large value "close to infinity" (Close to the largest value allowed for the data 
        /// type). Used in the Slope of a LineSeg
        /// </summary>
        /// <remarks></remarks>
        public const double SlopeMax = 9223372036854775807d;
        //public const double SlopeMax = double.PositiveInfinity;

        /// <summary>
        /// 
        /// </summary>
        public static double DoubleRoundLimit = 1E+16;

        /// <summary>
        /// Represents the ratio of the circumference of a circle to its radius, specified
        /// by the proposed constant, τ.
        /// One Tau or double Pi.
        /// </summary>
        /// <value>≈6.28318...</value>
        public const double Tau = 2d * Math.PI;

        /// <summary>
        /// Represents the ratio of the circumference of a circle to its radius, specified
        /// by the proposed constant, τ.
        /// One Tau or double Pi.
        /// </summary>
        /// <value>≈6.28318...</value>
        public static readonly double Τ = Tau;

        /// <summary>
        /// Math.PI * 2
        /// </summary>
        /// <remarks></remarks>
        public const double DoublePi = Tau;

        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified
        /// by the constant, π.
        /// One half Tau or One Pi.
        /// </summary>
        /// <value>≈3.1415926535897931...</value>
        public const double Pi = Math.PI;

        /// <summary>
        /// 
        /// </summary>
        public const double Π = Pi;

        /// <summary>
        /// One half Tau or One Pi.
        /// </summary>
        public const double HalfTau = Math.PI;

        /// <summary>
        /// 
        /// </summary>
        public const double ThreeQuarterTau = Math.PI * 6d / 4d;

        /// <summary>
        /// 
        /// </summary>
        public const double OneAndAHalfPi = ThreeQuarterTau;

        /// <summary>
        /// One quarter Tau or half Pi.
        /// </summary>
        public const double QuarterTau = 0.5d * Math.PI;

        /// <summary>
        /// Math.PI / 2
        /// </summary>
        /// <remarks></remarks>
        public const double HalfPi = QuarterTau;

        /// <summary>
        /// One eighth Tau or a quarter Pi.
        /// </summary>
        public const double EighthTau = 0.25d * Math.PI;

        /// <summary>
        /// Math.PI / 4
        /// </summary>
        /// <remarks></remarks>
        public const double QuarterPi = EighthTau;

        /// <summary>
        /// One sixteenth Tau or a eighth Pi.
        /// </summary>
        public const double SixteenthTau = 0.125d * Math.PI;

        /// <summary>
        /// Math.PI / 8
        /// </summary>
        /// <remarks></remarks>
        public const double EighthPi = SixteenthTau;

        /// <summary>
        /// Represents the golden ratio as specified by the constant, φ.
        /// </summary>
        /// <value>≈1.61803...</value>
        public static readonly double Phi = (1d + Math.Sqrt(5)) / 2d; //1.61803398874989484820458683436;

        /// <summary>
        /// 
        /// </summary>
        public static readonly double Φ = Phi;

        /// <summary>
        /// Represents the golden ratio.
        /// </summary>
        public static readonly double Golden = Phi;

        /// <summary>
        /// Represents the golden ratio by formula.
        /// </summary>
        public static readonly double GoldenRatio = Phi;

        /// <summary>
        ///  Represents the plastic constant as specified by the constant, ρ.
        /// </summary>
        /// <value>≈1.32471...</value>
        public static readonly double Rho = Root(0.5 + (1.00 / 6.00 * Math.Sqrt(23.00 / 3.00)), 3.00) + Root(0.50 - (1.00 / 6.00 * Math.Sqrt(23.00 / 3.00)), 3.00);

        /// <summary>
        /// 
        /// </summary>
        public static readonly double Ρ = Rho;

        /// <summary>
        /// One Radian.
        /// </summary>
        public const double Radien = Math.PI / 180d;

        /// <summary>
        /// Math.PI / 180
        /// </summary>
        /// <remarks></remarks>
        /// <optimisation>This code has been optimized for Accuracy</optimisation>
        public const double PiOneEightyth = Radien;

        /// <summary>
        /// One degree.
        /// </summary>
        public const double Degree = 180d / Math.PI;

        /// <summary>
        /// 180 / Math.PI
        /// </summary>
        /// <remarks></remarks>
        public const double OneEightythPi = Degree;

        /// <summary>
        /// Represents the natural logarithmic base, specified by the constant e. 
        /// </summary>
        /// <value>3.1415926535897931</value>
        public const double E = Math.E;

        /// <summary>
        /// Represents the constant value of the square root of 2.
        /// </summary>
        /// <value>≈1.41421...</value>
        public static readonly double Sqrt2 = Math.Sqrt(2);

        /// <summary>
        /// Represents the constant value of the square root of 3.
        /// </summary>
        /// <value>≈1.73205...</value>
        public static readonly double Sqrt3 = Math.Sqrt(3);

        /// <summary>
        /// Represents the constant value of the square root of 5.
        /// </summary>
        /// <value>≈2.23606...</value>
        public static readonly double Sqrt5 = Math.Sqrt(5);

        /// <summary>
        /// The natural log of e.
        /// </summary>
        public const double Log10E = 0.434294481903251827651d;

        #endregion

        #region Gauss Tables
        /// <summary>
        /// Gauss abscissa table
        /// </summary>
        /// <remarks>https://code.google.com/archive/p/degrafa/source/default/source</remarks>
        public static List<double> abscissa = new List<double>()
            {
                // N=2
                -0.5773502692,
                0.5773502692,
                // N=3
                -0.7745966692,
                0.7745966692,
                0,
                // N=4
                -0.8611363116,
                0.8611363116,
                -0.3399810436,
                0.3399810436,
                // N=5
                -0.9061798459,
                0.9061798459,
                -0.5384693101,
                0.5384693101,
                0.0000000000,
                // N=6
                -0.9324695142,
                0.9324695142,
                -0.6612093865,
                0.6612093865,
                -0.2386191861,
                0.2386191861,
                // N=7
                -0.9491079123,
                0.9491079123,
                -0.7415311856,
                0.7415311856,
                -0.4058451514,
                0.4058451514,
                0.0000000000,
                // N=8
                -0.9602898565,
                0.9602898565,
                -0.7966664774,
                0.7966664774,
                -0.5255324099,
                0.5255324099,
                -0.1834346425,
                0.1834346425
            };

        /// <summary>
        /// Gauss weight table
        /// </summary>
        /// <remarks>https://code.google.com/archive/p/degrafa/source/default/source</remarks>
        public static List<double> weight = new List<double>()
            {
                // N=2
                1,
                1,
                // N=3
                0.5555555556,
                0.5555555556,
                0.8888888888,
                // N=4
                0.3478548451,
                0.3478548451,
                0.6521451549,
                0.6521451549,
                // N=5
                0.2369268851,
                0.2369268851,
                0.4786286705,
                0.4786286705,
                0.5688888888,
                // N=6
                0.1713244924,
                0.1713244924,
                0.3607615730,
                0.3607615730,
                0.4679139346,
                0.4679139346,
                // N=7
                0.1294849662,
                0.1294849662,
                0.2797053915,
                0.2797053915,
                0.3818300505,
                0.3818300505,
                0.4179591837,
                // N=8
                0.1012285363,
                0.1012285363,
                0.2223810345,
                0.2223810345,
                0.3137066459,
                0.3137066459,
                0.3626837834,
                0.3626837834
            };
        #endregion

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
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(
            double x1, double y1,
            double x2, double y2)
            => Math.Atan2((y1 - y2), (x1 - x2));

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
        public static double Angle(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (x1 == x2 && y1 == y2 && z1 == z2) ? 0 : Math.Acos(Math.Min(1.0d, DotProduct(Normalize(x1, y1, z1), Normalize(x2, y2, z2))));

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
        public static double AngleVector(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
            => Math.Atan2(CrossProduct3Point(x1, y1, x2, y2, x3, y3), DotProduct3Point(x1, y1, x2, y2, x3, y3));

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        public static double AbsoluteAngle(
            double x1, double y1,
            double x2, double y2)
        {
            // Find the angle of point a and point b. 
            double test = -Maths.Angle(x1, y1, x2, y2) % Math.PI;
            return test < 0 ? test += Math.PI : test;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleBetween(
            double uX, double uY,
            double vX, double vY)
            => Math.Acos((uX * vX + uY * vY) / Math.Sqrt((uX * uX + uY * uY) * (vX * vX + vY * vY)));

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleBetween(
            double uX, double uY, double uZ,
            double vX, double vY, double vZ)
            => Math.Acos((uX * vX + uY * vY + uZ * vZ) / Math.Sqrt((uX * uX + uY * uY + uZ * uZ) * (vX * vX + vY * vY + vZ * vZ)));

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
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <returns></returns>
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct3Point(
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
        /// Return the dot product AB · BC.
        /// </returns>
        /// <remarks>
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct3Point(
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(
            double x1, double y1,
            double x2, double y2)
            => Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1));

        /// <summary>
        /// The square of the distance between two points.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
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
        /// <param name="x2_">The x component of the first point on the line.</param>
        /// <param name="y2_">The y component of the first point on the line.</param>
        /// <param name="x3_">The x component of the second point on the line.</param>
        /// <param name="y3_">The y component of the second point on the line.</param>
        /// <returns></returns>
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(double i, double j)
            => i == 0 ? SlopeMax : (j / i);

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
        public static double Slope(
            double x1, double y1,
            double x2, double y2)
            => (x1 == x2) ? SlopeMax : ((y2 - y1) / (x2 - x1));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Magnitude(double i, double j)
            => Math.Sqrt((i * i) + (j * j));

        /// <summary>
        /// Magnitude of a three dimensional Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Magnitude(double i, double j, double k)
            => Math.Sqrt((i * i) + (j * j) + (k * k));

        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks></remarks>
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double> Normalize(double i, double j)
            => new Tuple<double, double>(
                i / Math.Sqrt((i * i) + (j * j)),
                j / Math.Sqrt((i * i) + (j * j))
                );

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> Normalize(double i, double j, double k)
            => new Tuple<double, double, double>(
                i / Math.Sqrt((i * i) + (j * j) + (k * k)),
                j / Math.Sqrt((i * i) + (j * j) + (k * k)),
                k / Math.Sqrt((i * i) + (j * j) + (k * k))
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double> Normalize(
            double x1, double y1,
            double x2, double y2)
            => new Tuple<double, double>(
                x1 / Math.Sqrt(((x1 * x2) + (y1 * y2))),
                y1 / Math.Sqrt(((x1 * x2) + (y1 * y2)))
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> Normalize(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => new Tuple<double, double, double>(
                x1 / Math.Sqrt((x1 * x2) + (y1 * y2) + (z1 * z2)),
                y1 / Math.Sqrt((x1 * x2) + (y1 * y2) + (z1 * z2)),
                z1 / Math.Sqrt((x1 * x2) + (y1 * y2) + (z1 * z2))
                );

        /// <summary>
        /// Find the Clockwise Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use I = -J, J = I</remarks>
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double> PerpendicularCounterClockwise(double i, double j)
            => new Tuple<double, double>(j, -i);

        /// <summary>
        /// Find the determinant of a 2 by 2 matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <remarks>https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(
            double a, double b,
            double c, double d)
            => ((a * d)
              - (b * c)
            );

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
        /// Find the inverse of the determinant of a 2 by 2 matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
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
        public static Tuple<double, double, double> Reflection(
            double i1, double j1, double k1,
            double i2, double j2, double k2)
        {
            // if v2 has a right angle to vector, return -vector and stop
            if (Math.Abs(Math.Abs(Angle(i1, j1, k1, i2, j2, k2)) - Math.PI / 2) < double.Epsilon)
            {
                return new Tuple<double, double, double>(-i1, -j1, -k1);
            }

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> RotateX(double x1, double y1, double z1, double rad)
            => new Tuple<double, double, double>(
                x1,
                (y1 * Math.Cos(rad)) - (z1 * Math.Sin(rad)),
                (y1 * Math.Sin(rad)) + (z1 * Math.Cos(rad))
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> RotateY(double x1, double y1, double z1, double rad)
            => new Tuple<double, double, double>(
                (z1 * Math.Sin(rad)) + (x1 * Math.Cos(rad)),
                y1,
                (z1 * Math.Cos(rad)) - (x1 * Math.Sin(rad))
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> RotateZ(double x1, double y1, double z1, double rad)
            => new Tuple<double, double, double>(
                (x1 * Math.Cos(rad)) - (y1 * Math.Sin(rad)),
                (x1 * Math.Sin(rad)) + (y1 * Math.Cos(rad)),
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> RotateX(double x1, double y1, double z1, double yOff, double zOff, double rad)
            => new Tuple<double, double, double>(
                x1,
                (y1 * Math.Cos(rad)) - (z1 * Math.Sin(rad)) + (yOff * (1 - Math.Cos(rad)) + zOff * Math.Sin(rad)),
                (y1 * Math.Sin(rad)) + (z1 * Math.Cos(rad)) + (zOff * (1 - Math.Cos(rad)) - yOff * Math.Sin(rad))
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> RotateY(double x1, double y1, double z1, double xOff, double zOff, double rad)
            => new Tuple<double, double, double>(
                (z1 * Math.Sin(rad)) + (x1 * Math.Cos(rad)) + (xOff * (1 - Math.Cos(rad)) - zOff * Math.Sin(rad)),
                y1,
                (z1 * Math.Cos(rad)) - (x1 * Math.Sin(rad)) + (zOff * (1 - Math.Cos(rad)) + xOff * Math.Sin(rad))
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> RotateZ(double x1, double y1, double z1, double xOff, double yOff, double rad)
            => new Tuple<double, double, double>(
                (x1 * Math.Cos(rad)) - (y1 * Math.Sin(rad)) + (xOff * (1 - Math.Cos(rad)) + yOff * Math.Sin(rad)),
                (x1 * Math.Sin(rad)) + (y1 * Math.Cos(rad)) + (yOff * (1 - Math.Cos(rad)) - xOff * Math.Sin(rad)),
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(double i1, double j1)
            => Magnitude(i1, j1) == 1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="j1"></param>
        /// <param name="k1"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(double i1, double j1, double k1)
            => Magnitude(i1, j1, k1) == 1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="theta"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double> Interpolate(
            double x1, double y1,
            double x2, double y2,
            double theta)
            => new Tuple<double, double>(
                x1 * (1 - theta) + x2 * theta,
                y1 * (1 - theta) + y2 * theta
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
        /// <param name="theta"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> Interpolate(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double theta)
            => new Tuple<double, double, double>(
                x1 * (1 - theta) + x2 * theta,
                y1 * (1 - theta) + y2 * theta,
                z1 * (1 - theta) + z2 * theta
                );

        #endregion

        #region Array Math

        /// <summary>
        /// Returns the average value of a numeric array.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks>Note: Uses Following Sum Function as well.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Average(this double[] values)
            => (values.Sum() / values.Length);

        /// <summary>
        /// Returns the average value of a numeric array.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks>Note: Uses Following Sum Function as well.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Average(this List<double> values)
            => (values.Sum() / values.Count);

        /// <summary>
        /// Returns the average value of a numeric array.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks>Note: Uses Following Sum Function as well.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Average(this IEnumerable<double> values)
            => values.Sum() / values.Count();

        /// <summary>
        /// Find the sum of an array of Numbers
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum(double[] values)
            => values.Sum();

        /// <summary>
        /// Find the sum of an array of Numbers
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum(List<double> values)
            => values.Sum();

        /// <summary>
        /// Find the sum of an array of Numbers
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum(IEnumerable<double> values)
            => values.Sum();

        #endregion

        #region Derived Equivalent Math Functions

        // Derived equivalent Math Functions The following is a list of non-intrinsic math functions that can be derived from the intrinsic math functions:

        /// <summary>
        /// Returns the specified root a specified number.
        /// </summary>
        /// <param name="x">A double-precision floating-point number to find the specified root of.</param>
        /// <param name="y">A double-precision floating-point number that specifies a root.</param>
        /// <returns>The y root of the number x.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Root(double x, double y)
            => (x < 0 && y % 2 == 1) ? -Math.Pow(-x, (1d / y)) : Math.Pow(x, (1d / y));

        /// <summary>
        /// Cube root equivalent of the sqrt function. (note that there are actually
        /// three roots: one real, two complex, and we don't care about the latter):
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/26823024/cubic-bezier-reverse-getpoint-equation-float-for-vector-vector-for-float?answertab=active#tab-top</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Crt(double value)
            => value < 0 ? -Math.Pow(-value, 1d / 3d) : Math.Pow(value, 1d / 3d);

        /// <summary>
        /// Angle with tangent opp/hyp
        /// </summary>
        /// <param name="opposite"></param>
        /// <param name="adjacent"></param>
        /// <returns>Return the angle with tangent opp/hyp. The returned value is between PI and -PI.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Atan2(double opposite, double adjacent)
            => Math.Atan2(opposite, adjacent);

        /// <summary>
        /// Returns the Angle of two deltas.
        /// </summary>
        /// <param name="opposite">Delta Angle 1</param>
        /// <param name="adjacent">Delta Angle 2</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        public static double _Atan2(double opposite, double adjacent)
        {
            if (((opposite == 0) && (adjacent == 0))) return 0;
            double Value = Math.Asin(opposite / Math.Sqrt(opposite * opposite + adjacent * adjacent));
            if ((adjacent < 0)) Value = (Math.PI - Value);
            if ((Value < 0)) Value = (Value + (2 * Math.PI));
            return Value;
        }

        /// <summary>
        /// Derived math functions equivalent Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Secant(double value)
            => ((value % Pi != HalfPi) && (value % Pi != -HalfPi)) ? (1 / Math.Cos(value)) : 0;

        /// <summary>
        /// Derived math functions equivalent  Co-secant
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cosecant(double Value)
            => ((Value % Pi != 0) && (Value % Pi != Pi)) ? (1 / Math.Sin(Value)) : 0;

        /// <summary>
        /// Derived math functions equivalent Cotangent
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cotangent(double Value)
            => ((Value % Pi != 0) && (Value % Pi != Pi)) ? (1 / Math.Tan(Value)) : 0;

        /// <summary>
        /// Derived math functions equivalent Inverse Sine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double InverseSine(double value)
        {
            //  Arc-sin(X) 
            // Return Atan(Value / Sqrt(-Value * Value + 1))
            if (value == 1) return HalfPi;
            else if (value == -1) return -HalfPi;
            else
            {
                if ((Math.Abs(value) < 1))
                {
                    return Math.Atan(value / Math.Sqrt(-value * value + 1));
                }
            }

            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Cosine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double InverseCosine(double value)
        {
            //  Arc-cos(X) 
            // Return Atan(-Value / Sqrt(-Value * Value + 1)) + 2 * Atan(1)
            if (value == 1) return 0;
            else if (value == -1) return Pi;
            else
            {
                if ((Math.Abs(value) < 1))
                {
                    return Math.Atan(-value / Math.Sqrt(-value * value + 1)) + 2 * Math.Atan(1);
                }
            }

            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double InverseSecant(double value)
        {
            //  Arc-sec(X) 
            // Return Atan(Value / Sqrt(Value * Value - 1)) + Sign((Value) - 1) * (2 * Atan(1))
            if (value == 1) return 0;
            else if (value == -1) return Math.PI;
            else
            {
                if ((Math.Abs(value) < 1))
                {
                    return Math.Atan(value / Math.Sqrt(value * value - 1)) + Math.Sign((value) - 1) * (2 * Math.Atan(1));
                }
            }

            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Co-secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double InverseCosecant(double value)
        {
            //  Arc-co-sec(X) 
            // Return Atan(Value / Sqrt(Value * Value - 1)) + (Sign(Value) - 1) * (2 * Atan(1))
            if (value == 1) return HalfPi;
            else if (value == -1) return -HalfPi;
            else
            {
                if ((Math.Abs(value) < 1))
                {
                    return Math.Atan(value / Math.Sqrt(value * value - 1)) + (Math.Sign(value) - 1) * (2 * Math.Atan(1));
                }
            }
            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Cotangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>Arc-co-tan(X)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseCotangent(double value)
            => (Math.Atan(value) + (2 * Math.Atan(1)));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Sine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HSin(X)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicSine(double value)
            => ((Math.Exp(value) - Math.Exp((value * -1))) * 0.5d);

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Cosine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HCos(X)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicCosine(double value)
            => ((Math.Exp(value) + Math.Exp((value * -1))) * 0.5d);

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Tangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HTan(X)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicTangent(double value)
            => ((Math.Exp(value) - Math.Exp((value * -1))) / (Math.Exp(value) + Math.Exp((value * -1))));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HSec(X)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicSecant(double value)
            => (0.5d * (Math.Exp(value) + Math.Exp((value * -1))));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Co-secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HCosec(X)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicCosecant(double value)
            => (0.5d * (Math.Exp(value) - Math.Exp((value * -1))));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Cotangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HCotan(X) </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicCotangent(double value)
            => ((Math.Exp(value) + Math.Exp((value * -1))) / (Math.Exp(value) - Math.Exp((value * -1))));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Sine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArcsin(X)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicSine(double value)
            => Math.Log((value + Math.Sqrt(((value * value) + 1))));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Cosine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArccos(X)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicCosine(double value)
            => Math.Log((value + Math.Sqrt(((value * value) - 1))));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Tangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArctan(X)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicTangent(double value)
            => (Math.Log(((1 + value) / (1 - value))) * 0.5d);

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArcsec(X) </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicSecant(double value)
            => Math.Log(((Math.Sqrt((((value * value) * -1) + 1)) + 1) / value));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Co-secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArccosec(X)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicCosecant(double value)
            => Math.Log((((Math.Sign(value) * Math.Sqrt(((value * value) + 1))) + 1) / value));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Cotangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArccotan(X)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicCotangent(double value)
            => (Math.Log(((value + 1) / (value - 1))) * 0.5d);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LogarithmTobaseN(double value, double numberBase)
            => (numberBase != 1) ? (Math.Log(value) / Math.Log(numberBase)) : 0;

        #endregion

        #region Conversion Extensions

        /// <summary>
        /// Find the absolute positive value of a radian angle.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns>The absolute positive angle in radians.</returns>
        /// <remarks></remarks>
        public static double AbsoluteAngle(this double angle)
        {
            double test = angle % Maths.Tau;
            return test < 0 ? test + Maths.Tau : test;
        }

        /// <summary>
        /// Imitation of Excel's Mod Operator
        /// </summary>
        /// <param name="valueA">Source parameter</param>
        /// <param name="valueB">Destination parameter</param>
        /// <returns>Returns the same Modulus Result that Excel returns.</returns>
        /// <remarks>Created after finding out Excel returns a different value for the Mod Operator than .Net</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulo(this double valueA, double valueB)
            => ((valueA %= valueB) < 0) ? valueA + valueB : valueA;

        /// <summary>
        /// Convert Degrees to Radians.
        /// </summary>
        /// <param name="degrees">Angle in Degrees.</param>
        /// <returns>Angle in Radians.</returns>
        /// <remarks></remarks>
        /// <optimisation>This code has been optimized for speed by removing division from each call</optimisation>
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
        /// <optimisation>This code has been optimized for speed by removing division from each call</optimisation>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToDegrees(this double radiens)
            => radiens * Degree;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RoundToInt(this float val)
            => (0 < val) ? (int)(val + 0.5) : (int)(val - 0.5);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RoundToInt(this double val)
            => (0 < val) ? (int)(val + 0.5) : (int)(val - 0.5);

        /// <summary>
        /// Round a value to the nearest multiple of a number.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="multiple">The multiple to round to.</param>
        /// <returns>Returns a value rounded to an interval of the multiple.</returns>
        /// <remarks>Convert.ToInt32 does the correct rounding that Math.Round does not do.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RoundToMultiple(this double value, double multiple)
            => Convert.ToInt32(value / multiple) * multiple;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToFloat(string text)
            => float.Parse(text, CultureInfo.InvariantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToFloat(string text, IFormatProvider provider)
            => float.Parse(text, provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToDouble(string text)
            => double.Parse(text, CultureInfo.InvariantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToDouble(string text, IFormatProvider provider)
            => double.Parse(text, provider);

        #endregion

        #region Comparisons

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="epsilonSqrd"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreClose(double aX, double aY, double bX, double bY, double epsilonSqrd)
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
        public static bool AreClose(this float value1, float value2, float epsilon = FloatEpsilon)
        {
            // in case they are Infinities (then epsilon check does not work)
            if (value1 == value2) return true;
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
        public static bool AreClose(this double value1, double value2, double epsilon = DoubleEpsilon)
        {
            // in case they are Infinities (then epsilon check does not work)
            if (value1 == value2) return true;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrClose(this double value1, double value2)
            => (value1 > value2) || AreClose(value1, value2);

        /// <summary>
        /// 
        /// </summary>
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero(this double value, double epsilon = DoubleEpsilon)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne(this double value, double epsilon = DoubleEpsilon)
            => Math.Abs(value - 1d) < 10d * epsilon;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBetweenZeroAndOne(this float val)
            => (GreaterThanOrClose(val, 0f) && LessThanOrClose(val, 1));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBetweenZeroAndOne(this double val)
            => (GreaterThanOrClose(val, 0d) && LessThanOrClose(val, 1));

        #endregion
    }
}
