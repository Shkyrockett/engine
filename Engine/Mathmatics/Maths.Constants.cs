// <copyright file="Maths.Constants.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

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
        /// 
        /// </summary>
        /// <remarks>
        /// Might be useful with Atan2
        /// http://www.charlespetzold.com/blog/2008/09/180741.html
        /// </remarks>
        public const double NegativeZero = -0d;//1 / double.NegativeInfinity;

        /// <summary>
        /// 
        /// </summary>
        public const double Tolerance = 1e-6d;

        /// <summary>
        /// 
        /// </summary>
        public const double Accuracy = 15d;

        /// <summary>
        /// Error difference for line intersection tests.
        /// </summary>
        public const double Epsilon = 5.684341886080801536e-12d;

        /// <summary>
        /// Smallest such that 1.0 + <see cref="Epsilon"/> != 1.0
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

        /// <summary>
        /// 
        /// </summary>
        public const double DefaultArcTolerance = 0.25;

        /// <summary>
        /// 
        /// </summary>
        public const int LoRange32 = 0x7FFF;

        /// <summary>
        /// 
        /// </summary>
        public const int HiRange32 = 0x7FFF;

        /// <summary>
        /// 
        /// </summary>
        public const long LoRange64 = 0x3FFFFFFF;

        /// <summary>
        /// 
        /// </summary>
        public const long HiRange64 = 0x3FFFFFFFFFFFFFFFL;

        #endregion

        #region Pi Derivations

        /// <summary>
        /// Represents the inverse of Pi, or the quotient of one over pi.
        /// </summary>
        public const double InversePi = 1d / PI; // 0.31830988618379067153776752674503d;

        /// <summary>
        /// Represents the inverse of Tau, or the quotient of one over 2 pi.
        /// </summary>
        public const double InverseTau = 1d / Tau; // 0.15915494309189533576888376337251d;

        /// <summary>
        /// Represents the value of the double inverse of Pi, or the quotient of two over pi.
        /// </summary>
        public const double Inverse2OverPi = 2d / PI; // 3.1415926535897932384626433832795d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the first eighth of that circle.
        /// One sixteenth Tau or a eighth Pi.
        /// </summary>
        /// <remarks>PI / 8</remarks>
        public const double EighthPi = 0.125d * PI; // 0.39269908169872415480783042290994d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the first eighth of that circle.
        /// One eighth Tau or a quarter Pi. A 45 degree angle.
        /// </summary>
        /// <remarks>PI / 4</remarks>
        public const double Quart = 0.25d * PI; // 0.78539816339744830961566084581988d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the first quarter of that circle.
        /// One quarter Tau or half Pi. A right angle in mathematics.
        /// </summary>
        /// <remarks>PI / 2</remarks>
        public const double Right = 0.5d * PI; // 1.5707963267948966192313216916398d;

        ///// <summary>
        ///// Represents the ratio of the circumference of a circle to its diameter, specified
        ///// by the constant, π (Pi).
        ///// One half Tau or One Pi.
        ///// </summary>
        ///// <value>≈3.1415926535897931...</value>
        //public const double Pi = Math.PI; // 3.1415926535897932384626433832795d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the third quarter of that circle.
        /// Three quarter tau, or one and a half pi.
        /// </summary>
        /// <remarks>
        /// Three quarter tau, or one and a half pi are just too long and awkward.
        /// Randal Munro's joke "compromise" works well enough for a name: http://xkcd.com/1292/
        /// </remarks>
        /// <acknowledgment>
        /// Randal Munro http://xkcd.com/1292/ 
        /// </acknowledgment>
        public const double Pau = 1.5d * PI; // 4.7123889803846898576939650749193d;

        /// <summary>
        /// Represents the ratio of the circumference of a circle to its radius, specified
        /// by the proposed constant, τ (Tau).
        /// One Tau or two Pi.
        /// </summary>
        /// <value>≈6.28318...</value>
        public const double Tau = 2d * PI; // 6.283185307179586476925286766559d;

        /// <summary>
        /// One Radian.
        /// </summary>
        /// <remarks>PI / 180</remarks>
        public const double Radian = PI / 180d; // 0.01745329251994329576923690768489d;

        /// <summary>
        /// One degree.
        /// </summary>
        /// <remarks>180 / PI</remarks>
        public const double Degree = 180d / PI; // 57.295779513082320876798154814105d;

        #endregion

        /// <summary>
        /// Represents the golden ratio as specified by the constant, φ (phi).
        /// </summary>
        /// <value>≈1.61803...</value>
        public static readonly double Phi = (1d + Sqrt(5)) * 0.5d; // 1.6180339887498948482045868343656d;

        /// <summary>
        ///  Represents the plastic constant as specified by the constant, ρ.
        /// </summary>
        /// <value>≈1.32471...</value>
        public static readonly double Rho = Root(0.5d + (1d / 6d * Sqrt(23d / 3d)), 3d) + Root(0.5d - (1d / 6d * Sqrt(23d / 3d)), 3d);

        #region Roots

        /// <summary>
        /// Represents the inverse square root of 2.
        /// </summary>
        public static readonly double InvSqrt2 = 1 / Sqrt(2); // 0.70710678118654752440084436210485d;

        /// <summary>
        /// Represents the double inverse square root of Pi.
        /// </summary>
        public static readonly double Inv2SqrtPi = 2 / Sqrt(PI); // 1.1283791670955125738961589031215d;

        /// <summary>
        /// Represents the constant value of the square root of 2.
        /// </summary>
        /// <value>≈1.41421...</value>
        public static readonly double Sqrt2 = Sqrt(2); // 1.4142135623730950488016887242097d;

        /// <summary>
        /// Represents the constant value of the square root of 3.
        /// </summary>
        /// <value>≈1.73205...</value>
        public static readonly double Sqrt3 = Sqrt(3); // 1.7320508075688772935274463415059d;

        /// <summary>
        /// Represents the constant value of the square root of 5.
        /// </summary>
        /// <value>≈2.23606...</value>
        public static readonly double Sqrt5 = Sqrt(5); // 2.2360679774997896964091736687313d;

        #endregion

        #region Fractions

        /// <summary>
        /// 
        /// </summary>
        public const double Zero = 0;

        /// <summary>
        /// 
        /// </summary>
        public const double OneSixtyfourth = 1d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double OneThirtysecond = 1d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThreeSixtyfourth = 3d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double OneSixteenth = 1d / 16d;

        /// <summary>
        /// 
        /// </summary>
        public const double FiveSixtyfourth = 5d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThreeThirtysecond = 3d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double SevenSixtyfourth = 7d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double OneEighth = 1d / 8d;

        /// <summary>
        /// 
        /// </summary>
        public const double NineSixtyfourth = 9d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double FiveThirtysecond = 5d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double ElevenSixtyfourth = 11d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThreeSixteenth = 3d / 16d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThirteenSixtyfourth = 13d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double SevenThirtysecond = 7d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double FifteenSixtyfourth = 15d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double OneQuarter = 1d / 4d;

        /// <summary>
        /// 
        /// </summary>
        public const double SeventeenSixtyfourth = 17d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double NineThirtysecond = 9d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double NineteenSixtyfourth = 19d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double FiveSixteenth = 5d / 16d;

        /// <summary>
        /// 
        /// </summary>
        public const double TwentyoneSixtyfourth = 21d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double OneThird = 1d / 3d;

        /// <summary>
        /// 
        /// </summary>
        public const double ElevenThirtysecond = 11d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double TwentythreeSixtyfourth = 23d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThreeEighths = 3d / 8d;

        /// <summary>
        /// 
        /// </summary>
        public const double TwentyfiveSixtyfourth = 25d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThirteenThirtysecond = 13d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double TwentysevenSixtyfourth = 27d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double SevenSixteenth = 7d / 16d;

        /// <summary>
        /// 
        /// </summary>
        public const double TwentynineSixtyfourth = 29d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double FifteenThirtysecond = 15d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThirtyoneSixtyfourth = 31d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double OneHalf = 1d * 0.5d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThirtythreeSixtyfourth = 33d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double SeventeenThirtysecond = 17d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThirtyfiveSixtyfourth = 35d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double NineSixteenth = 9d / 16d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThirtysevenSixtyfourth = 37d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double NineteenThirtysecond = 19d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThirtynineSixtyfourth = 39d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double FiveEighths = 5d / 8d;

        /// <summary>
        /// 
        /// </summary>
        public const double FourtyoneSixtyfourth = 41d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double TwentyoneThirtysecond = 21d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double TwoThirds = 2d / 3d;

        /// <summary>
        /// 
        /// </summary>
        public const double FourtythreeSixtyfourth = 43d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double ElevenSixteenth = 11d / 16d;

        /// <summary>
        /// 
        /// </summary>
        public const double FourtyfiveSixtyfourth = 45d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double TwentythreeThirtysecond = 23d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double FourtysevenSixtyfourth = 47d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThreeQuarters = 3d / 4d;

        /// <summary>
        /// 
        /// </summary>
        public const double FourtynineSixtyfourth = 49d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double TwentyfiveThirtysecond = 25d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double FiftyoneSixtyfourth = 51d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThirteenSixteenth = 13d / 16d;

        /// <summary>
        /// 
        /// </summary>
        public const double FiftythreeSixtyfourth = 53d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double TwentysevenThirtysecond = 27d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double FiftyfiveSixtyfourth = 55d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double SevenEighths = 7d / 8d;

        /// <summary>
        /// 
        /// </summary>
        public const double FiftysevenSixtyfourth = 57d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double TwentynineThirtysecond = 29d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double FiftynineSixtyfourth = 59d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double FifteenSixteenth = 15d / 16d;

        /// <summary>
        /// 
        /// </summary>
        public const double SixtyoneSixtyfourth = 61d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double ThirtyoneThirtysecond = 31d / 32d;

        /// <summary>
        /// 
        /// </summary>
        public const double SixtythreeSixtyfourth = 63d / 64d;

        /// <summary>
        /// 
        /// </summary>
        public const double One = 1d;

        /// <summary>
        /// 
        /// </summary>
        public const double OneTwentySeventh = 1 / 27;

        #endregion

        #region Logarithms

        ///// <summary>
        /////
        ///// </summary>
        //public const double E = Math.E; // 2.7182818284590452353602874713527d;

        /// <summary>
        /// The base 2 natural log of e.
        /// </summary>
        public const double Log2E = 1.44269504088896340736d;

        /// <summary>
        /// The base 10 natural log of e.
        /// </summary>
        public const double Log10E = 0.434294481903251827651d; // 0.43429448190325182765112891891661d;

        /// <summary>
        /// The base 2 natural log.
        /// </summary>
        public const double LN2 = 0.693147180559945309417d;

        /// <summary>
        /// The base 10 natural log.
        /// </summary>
        public const double LN10 = 2.30258509299404568402d;

        /// <summary>
        /// The Log of Two.
        /// </summary>
        public static readonly double LogTwo = Log(2);

        /// <summary>
        /// The Log of Ten.
        /// </summary>
        public static readonly double LogTen = Log(10);

        /// <summary>
        /// The inverse of the log of two.
        /// </summary>
        public static readonly double InverseLogTwo = 1 / LogTwo;

        #endregion

        #region Gauss Tables

        /// <summary>
        /// Gauss abscissa table
        /// </summary>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://code.google.com/archive/p/degrafa/source/default/source
        /// </acknowledgment>
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://code.google.com/archive/p/degrafa/source/default/source
        /// </acknowledgment>
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
