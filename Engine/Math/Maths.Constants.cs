// <copyright file="Maths.Constants.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Maths
    {
        #region Epsilons, Minimums, Maximums

        /// <summary>
        /// Smallest such that 1.0 + <see cref="DoubleEpsilon"/> != 1.0
        /// </summary>
        public const double DoubleEpsilon = 2.2204460492503131e-016d;

        /// <summary>
        /// Smallest such that 1.0 + <see cref="FloatEpsilon"/> != 1.0
        /// </summary>
        public const float FloatEpsilon = 1.192092896e-07f;

        ///// <summary>
        ///// float precision significant decimal
        ///// </summary>
        //const double FloatEpsilon = 0.000001d;

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

        #endregion

        #region Pi Derivations

        /// <summary>
        /// Represents the inverse of Pi, or the quotient of one over pi.
        /// </summary>
        public const double InversePi = 1d / PI; // 0.318309886183790671538d;

        /// <summary>
        /// Represents the value of the double inverse of Pi, or the quotient of two over pi.
        /// </summary>
        public const double Inverse2OverPi = 2d / PI; // 0.636619772367581343076;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the first eighth of that circle.
        /// One sixteenth Tau or a eighth Pi.
        /// </summary>
        /// <remarks>PI / 8</remarks>
        public const double EighthPi = 0.125d * PI;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the first eighth of that circle.
        /// One eighth Tau or a quarter Pi.
        /// </summary>
        /// <remarks>PI / 4</remarks>
        public const double Quart = 0.25d * PI; // 0.785398163397448309616d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the first quarter of that circle.
        /// One quarter Tau or half Pi.
        /// </summary>
        /// <remarks>PI / 2</remarks>
        public const double HalfPi = 0.5d * PI; // 1.57079632679489661923d;

        ///// <summary>
        ///// Represents the ratio of the circumference of a circle to its diameter, specified
        ///// by the constant, π (Pi).
        ///// One half Tau or One Pi.
        ///// </summary>
        ///// <value>≈3.1415926535897931...</value>
        //public const double PI = Math.PI; // 3.14159265358979323846d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the third quarter of that circle.
        /// Three quarter tau, or one and a half pi.
        /// </summary>
        /// <remarks>
        /// Three quarter tau, or one and a half pi are just too long and awkward.
        /// Randal Munro's "compromise" works well enough for a name: http://xkcd.com/1292/
        /// </remarks>
        public const double Pau = 1.5d * PI;

        /// <summary>
        /// Represents the ratio of the circumference of a circle to its radius, specified
        /// by the proposed constant, τ (Tau).
        /// One Tau or two Pi.
        /// </summary>
        /// <value>≈6.28318...</value>
        public const double Tau = 2d * PI; // 6.28318530717958647693;

        /// <summary>
        /// One Radian.
        /// </summary>
        /// <remarks>PI / 180</remarks>
        public const double Radien = PI / 180d;

        /// <summary>
        /// One degree.
        /// </summary>
        /// <remarks>180 / PI</remarks>
        public const double Degree = 180d / PI;

        #endregion

        /// <summary>
        /// Represents the golden ratio as specified by the constant, φ (phi).
        /// </summary>
        /// <value>≈1.61803...</value>
        public static readonly double Phi = (1d + Sqrt(5)) / 2d; //1.61803398874989484820458683436;

        /// <summary>
        ///  Represents the plastic constant as specified by the constant, ρ.
        /// </summary>
        /// <value>≈1.32471...</value>
        public static readonly double Rho = Root(0.5 + (1.00 / 6.00 * Sqrt(23.00 / 3.00)), 3.00) + Root(0.50 - (1.00 / 6.00 * Sqrt(23.00 / 3.00)), 3.00);

        #region Roots

        /// <summary>
        /// 
        /// </summary>
        public static readonly double InvSqrt2 = 1 / Sqrt(2); // 0.707106781186547524401;

        /// <summary>
        /// 
        /// </summary>
        public static readonly double Inv2SqrtPi = 2 / Sqrt(PI); // 1.12837916709551257390;

        /// <summary>
        /// Represents the constant value of the square root of 2.
        /// </summary>
        /// <value>≈1.41421...</value>
        public static readonly double Sqrt2 = Sqrt(2); // 1.41421356237309504880;

        /// <summary>
        /// Represents the constant value of the square root of 3.
        /// </summary>
        /// <value>≈1.73205...</value>
        public static readonly double Sqrt3 = Sqrt(3);

        /// <summary>
        /// Represents the constant value of the square root of 5.
        /// </summary>
        /// <value>≈2.23606...</value>
        public static readonly double Sqrt5 = Sqrt(5);

        #endregion

        #region Logarithms 

        ///// <summary>
        ///// 
        ///// </summary>
        //public const double E = Math.E;// 2.71828182845904523536d;

        /// <summary>
        /// The base 2 natural log of e.
        /// </summary>
        public const double Log2E = 1.44269504088896340736d;

        /// <summary>
        /// The base 10 natural log of e.
        /// </summary>
        public const double Log10E = 0.434294481903251827651d;

        /// <summary>
        /// The base 2 natural log.
        /// </summary>
        public const double LN2 = 0.693147180559945309417d;

        /// <summary>
        /// The base 10 natural log.
        /// </summary>
        public const double LN10 = 2.30258509299404568402d;

        #endregion

        #region Gauss Tables

        /// <summary>
        /// Gauss abscissa table
        /// </summary>
        /// <remarks>https://code.google.com/archive/p/degrafa/source/default/source</remarks>
        public static List<double> abscissa = new List<double>
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
        public static List<double> weight = new List<double>
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
    }
}
