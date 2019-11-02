// <copyright file="Mathematics.Constants.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using static System.Math;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The Mathematics class.
    /// </summary>
    public static partial class Mathematics
    {
        #region Epsilons, Minimums, Maximums
        /// <summary>
        /// The horizontal Value: double.NegativeInfinity.
        /// </summary>
        public const double horizontal = double.NegativeInfinity;

        /// <summary>
        /// Negative zero.
        /// </summary>
        /// <remarks>
        /// <para>Might be useful with Atan2
        /// http://www.charlespetzold.com/blog/2008/09/180741.html</para>
        /// </remarks>
        public const double NegativeZero = -0d;//1 / double.NegativeInfinity;

        /// <summary>
        /// The tolerance Value: 1e-6d.
        /// </summary>
        public const double Tolerance = 1e-6d;

        /// <summary>
        /// The accuracy Value: 15d.
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

        /// <summary>
        /// The nearest value to 0 Cosine can produce for a right angle.
        /// </summary>
        public static readonly double CosineZeroEpsilon = Cos(HalfPi); //6.123233995736766E-17;

        /// <summary>
        /// The nearest value to 0 Cosine can produce for a reverse right angle.
        /// </summary>
        public static readonly double CosineNegitiveZeroEpsilon = Cos(Pau);

        /// <summary>
        /// The near zero epsilon Value: 1E-20.
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

        public const double SlopeMax = 9223372036854775807d;

        /// <summary>
        /// The double round limit.
        /// </summary>
        public static readonly double DoubleRoundLimit = 1E+16;

        /// <summary>
        /// The default arc tolerance Value: 0.25.
        /// </summary>
        public const double DefaultArcTolerance = 0.25;

        /// <summary>
        /// The lo range32 Value: 0x7FFF.
        /// </summary>
        public const int LoRange32 = 0x7FFF;

        /// <summary>
        /// The hi range32 Value: 0x7FFF.
        /// </summary>
        public const int HiRange32 = 0x7FFF;

        /// <summary>
        /// The lo range64 Value: 0x3FFFFFFF.
        /// </summary>
        public const long LoRange64 = 0x3FFFFFFF;

        /// <summary>
        /// The hi range64 Value: 0x3FFFFFFFFFFFFFFFL.
        /// </summary>
        public const long HiRange64 = 0x3FFFFFFFFFFFFFFFL;
        #endregion Epsilons, Minimums, Maximums

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
        public const double Inverse2OverPi = 2d / PI; // 0.63661977236758134307553505349006d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the first sixteenth of that circle.
        /// One sixteenth Tau or a eighth Pi.
        /// </summary>
        /// <remarks><para>PI / 8</para></remarks>
        public const double EighthPi = 0.125d * PI; // 0.39269908169872415480783042290994d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the first eighth of that circle.
        /// One eighth Tau or a quarter Pi. A 45 degree angle.
        /// </summary>
        /// <remarks><para>PI / 4</para></remarks>
        public const double Quart = 0.25d * PI; // 0.78539816339744830961566084581988d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the first quarter of that circle.
        /// One quarter Tau or half Pi. A right angle in mathematics.
        /// </summary>
        /// <remarks><para>PI / 2</para></remarks>
        public const double HalfPi = 0.5d * PI; // 1.5707963267948966192313216916398d;

        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified
        /// by the constant, π (Pi).
        /// One half Tau or One Pi.
        /// </summary>
        /// <value>≈3.1415926535897931...</value>
        public const double Pi = PI; // 3.1415926535897932384626433832795d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the third quarter of that circle.
        /// Three quarter tau, or one and a half pi.
        /// </summary>
        /// <remarks>
        /// <para>Three quarter tau, or one and a half pi are just too long and awkward.
        /// Randal Munro's joke "compromise" works well enough for a name: http://xkcd.com/1292/</para>
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
        /// <remarks><para>PI / 180</para></remarks>
        public const double Radian = PI / 180d; // 0.01745329251994329576923690768489d;

        /// <summary>
        /// One degree.
        /// </summary>
        /// <remarks><para>180 / PI</para></remarks>
        public const double Degree = 180d / PI; // 57.295779513082320876798154814105d;
        #endregion Pi Derivations

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

        /// <summary>
        /// The cosine of 0.
        /// </summary>
        public static readonly double Cos0 = Cos(0);

        /// <summary>
        /// The cosine of PI.
        /// </summary>
        public static readonly double CosHalfPi = Cos(HalfPi);

        /// <summary>
        /// The cosine of Pi.
        /// </summary>
        public static readonly double CosPi = Cos(PI);

        /// <summary>
        /// The cosine of Pau.
        /// </summary>
        public static readonly double CosPau = Cos(Pau);

        /// <summary>
        /// The sine of 0.
        /// </summary>
        public static readonly double Sin0 = Sin(0);

        /// <summary>
        /// The sine of half Pi.
        /// </summary>
        public static readonly double SinHalfPi = Sin(HalfPi);

        /// <summary>
        /// The sine of Pi.
        /// </summary>
        public static readonly double SinPi = Sin(Pi);

        /// <summary>
        /// The sine of Pau.
        /// </summary>
        public static readonly double SinPau = Sin(Pau);

        #region Roots
        /// <summary>
        /// Represents the inverse square root of 2.
        /// </summary>
        public static readonly double InvSqrt2 = 1d / Sqrt(2d); // 0.70710678118654752440084436210485d;

        /// <summary>
        /// Represents the double inverse square root of Pi.
        /// </summary>
        public static readonly double Inv2SqrtPi = 2d / Sqrt(PI); // 1.1283791670955125738961589031215d;

        /// <summary>
        /// Represents the constant value of the square root of 2.
        /// </summary>
        /// <value>≈1.41421...</value>
        public static readonly double Sqrt2 = Sqrt(2d); // 1.4142135623730950488016887242097d;

        /// <summary>
        /// Represents the constant value of the square root of 3.
        /// </summary>
        /// <value>≈1.73205...</value>
        public static readonly double Sqrt3 = Sqrt(3d); // 1.7320508075688772935274463415059d;

        /// <summary>
        /// Represents the constant value of the square root of 5.
        /// </summary>
        /// <value>≈2.23606...</value>
        public static readonly double Sqrt5 = Sqrt(5d); // 2.2360679774997896964091736687313d;
        #endregion Roots

        #region Fractions
        /// <summary>
        /// The zero Value: 0.
        /// </summary>
        public const double Zero = 0d;

        /// <summary>
        /// The one sixty fourth Value: 1d / 64d.
        /// </summary>
        public const double OneSixtyfourth = 1d / 64d;

        /// <summary>
        /// The one thirty second Value: 1d / 32d.
        /// </summary>
        public const double OneThirtysecond = 1d / 32d;

        /// <summary>
        /// The three sixty fourth Value: 3d / 64d.
        /// </summary>
        public const double ThreeSixtyfourth = 3d / 64d;

        /// <summary>
        /// The one sixteenth Value: 1d / 16d.
        /// </summary>
        public const double OneSixteenth = 1d / 16d;

        /// <summary>
        /// The five sixty fourth Value: 5d / 64d.
        /// </summary>
        public const double FiveSixtyfourth = 5d / 64d;

        /// <summary>
        /// The three thirty second Value: 3d / 32d.
        /// </summary>
        public const double ThreeThirtysecond = 3d / 32d;

        /// <summary>
        /// The seven sixty fourth Value: 7d / 64d.
        /// </summary>
        public const double SevenSixtyfourth = 7d / 64d;

        /// <summary>
        /// The one eighth Value: 1d / 8d.
        /// </summary>
        public const double OneEighth = 1d / 8d;

        /// <summary>
        /// The nine sixty fourth Value: 9d / 64d.
        /// </summary>
        public const double NineSixtyfourth = 9d / 64d;

        /// <summary>
        /// The five thirty second Value: 5d / 32d.
        /// </summary>
        public const double FiveThirtysecond = 5d / 32d;

        /// <summary>
        /// The eleven sixty fourth Value: 11d / 64d.
        /// </summary>
        public const double ElevenSixtyfourth = 11d / 64d;

        /// <summary>
        /// The three sixteenth Value: 3d / 16d.
        /// </summary>
        public const double ThreeSixteenth = 3d / 16d;

        /// <summary>
        /// The thirteen sixty fourth Value: 13d / 64d.
        /// </summary>
        public const double ThirteenSixtyfourth = 13d / 64d;

        /// <summary>
        /// The seven thirty second Value: 7d / 32d.
        /// </summary>
        public const double SevenThirtysecond = 7d / 32d;

        /// <summary>
        /// The fifteen sixty fourth Value: 15d / 64d.
        /// </summary>
        public const double FifteenSixtyfourth = 15d / 64d;

        /// <summary>
        /// The one quarter Value: 1d / 4d.
        /// </summary>
        public const double OneQuarter = 1d / 4d;

        /// <summary>
        /// The seventeen sixty fourth Value: 17d / 64d.
        /// </summary>
        public const double SeventeenSixtyfourth = 17d / 64d;

        /// <summary>
        /// The nine thirty second Value: 9d / 32d.
        /// </summary>
        public const double NineThirtysecond = 9d / 32d;

        /// <summary>
        /// The nineteen sixty fourth Value: 19d / 64d.
        /// </summary>
        public const double NineteenSixtyfourth = 19d / 64d;

        /// <summary>
        /// The five sixteenth Value: 5d / 16d.
        /// </summary>
        public const double FiveSixteenth = 5d / 16d;

        /// <summary>
        /// The twenty one sixty fourth Value: 21d / 64d.
        /// </summary>
        public const double TwentyoneSixtyfourth = 21d / 64d;

        /// <summary>
        /// The one third Value: 1d / 3d.
        /// </summary>
        public const double OneThird = 1d / 3d;

        /// <summary>
        /// The eleven thirty second Value: 11d / 32d.
        /// </summary>
        public const double ElevenThirtysecond = 11d / 32d;

        /// <summary>
        /// The twenty three sixty fourth Value: 23d / 64d.
        /// </summary>
        public const double TwentythreeSixtyfourth = 23d / 64d;

        /// <summary>
        /// The three eighths Value: 3d / 8d.
        /// </summary>
        public const double ThreeEighths = 3d / 8d;

        /// <summary>
        /// The twenty five sixty fourth Value: 25d / 64d.
        /// </summary>
        public const double TwentyfiveSixtyfourth = 25d / 64d;

        /// <summary>
        /// The thirteen thirty second Value: 13d / 32d.
        /// </summary>
        public const double ThirteenThirtysecond = 13d / 32d;

        /// <summary>
        /// The twenty seven sixty fourth Value: 27d / 64d.
        /// </summary>
        public const double TwentysevenSixtyfourth = 27d / 64d;

        /// <summary>
        /// The seven sixteenth Value: 7d / 16d.
        /// </summary>
        public const double SevenSixteenth = 7d / 16d;

        /// <summary>
        /// The twenty nine sixty fourth Value: 29d / 64d.
        /// </summary>
        public const double TwentynineSixtyfourth = 29d / 64d;

        /// <summary>
        /// The fifteen thirty second Value: 15d / 32d.
        /// </summary>
        public const double FifteenThirtysecond = 15d / 32d;

        /// <summary>
        /// The thirty one sixty fourth Value: 31d / 64d.
        /// </summary>
        public const double ThirtyoneSixtyfourth = 31d / 64d;

        /// <summary>
        /// The one half Value: 1d * 0.5d.
        /// </summary>
        public const double OneHalf = 1d * 0.5d;

        /// <summary>
        /// The thirty three sixty fourth Value: 33d / 64d.
        /// </summary>
        public const double ThirtythreeSixtyfourth = 33d / 64d;

        /// <summary>
        /// The seventeen thirty second Value: 17d / 32d.
        /// </summary>
        public const double SeventeenThirtysecond = 17d / 32d;

        /// <summary>
        /// The thirty five sixty fourth Value: 35d / 64d.
        /// </summary>
        public const double ThirtyfiveSixtyfourth = 35d / 64d;

        /// <summary>
        /// The nine sixteenth Value: 9d / 16d.
        /// </summary>
        public const double NineSixteenth = 9d / 16d;

        /// <summary>
        /// The thirty seven sixty fourth Value: 37d / 64d.
        /// </summary>
        public const double ThirtysevenSixtyfourth = 37d / 64d;

        /// <summary>
        /// The nineteen thirty second Value: 19d / 32d.
        /// </summary>
        public const double NineteenThirtysecond = 19d / 32d;

        /// <summary>
        /// The thirty nine sixty fourth Value: 39d / 64d.
        /// </summary>
        public const double ThirtynineSixtyfourth = 39d / 64d;

        /// <summary>
        /// The five eighths Value: 5d / 8d.
        /// </summary>
        public const double FiveEighths = 5d / 8d;

        /// <summary>
        /// The forty one sixty fourth Value: 41d / 64d.
        /// </summary>
        public const double FortyoneSixtyfourth = 41d / 64d;

        /// <summary>
        /// The twenty one thirty second Value: 21d / 32d.
        /// </summary>
        public const double TwentyoneThirtysecond = 21d / 32d;

        /// <summary>
        /// The two thirds Value: 2d / 3d.
        /// </summary>
        public const double TwoThirds = 2d / 3d;

        /// <summary>
        /// The forty three sixty fourth Value: 43d / 64d.
        /// </summary>
        public const double FortythreeSixtyfourth = 43d / 64d;

        /// <summary>
        /// The eleven sixteenth Value: 11d / 16d.
        /// </summary>
        public const double ElevenSixteenth = 11d / 16d;

        /// <summary>
        /// The forty five sixty fourth Value: 45d / 64d.
        /// </summary>
        public const double FortyfiveSixtyfourth = 45d / 64d;

        /// <summary>
        /// The twenty three thirty second Value: 23d / 32d.
        /// </summary>
        public const double TwentythreeThirtysecond = 23d / 32d;

        /// <summary>
        /// The forty seven sixty fourth Value: 47d / 64d.
        /// </summary>
        public const double FortysevenSixtyfourth = 47d / 64d;

        /// <summary>
        /// The three quarters Value: 3d / 4d.
        /// </summary>
        public const double ThreeQuarters = 3d / 4d;

        /// <summary>
        /// The forty nine sixty fourth Value: 49d / 64d.
        /// </summary>
        public const double FortynineSixtyfourth = 49d / 64d;

        /// <summary>
        /// The twenty five thirty second Value: 25d / 32d.
        /// </summary>
        public const double TwentyfiveThirtysecond = 25d / 32d;

        /// <summary>
        /// The fifty one sixty fourth Value: 51d / 64d.
        /// </summary>
        public const double FiftyoneSixtyfourth = 51d / 64d;

        /// <summary>
        /// The thirteen sixteenth Value: 13d / 16d.
        /// </summary>
        public const double ThirteenSixteenth = 13d / 16d;

        /// <summary>
        /// The fifty three sixty fourth Value: 53d / 64d.
        /// </summary>
        public const double FiftythreeSixtyfourth = 53d / 64d;

        /// <summary>
        /// The twenty seven thirty second Value: 27d / 32d.
        /// </summary>
        public const double TwentysevenThirtysecond = 27d / 32d;

        /// <summary>
        /// The fifty five sixty fourth Value: 55d / 64d.
        /// </summary>
        public const double FiftyfiveSixtyfourth = 55d / 64d;

        /// <summary>
        /// The seven eighths Value: 7d / 8d.
        /// </summary>
        public const double SevenEighths = 7d / 8d;

        /// <summary>
        /// The fifty seven sixty fourth Value: 57d / 64d.
        /// </summary>
        public const double FiftysevenSixtyfourth = 57d / 64d;

        /// <summary>
        /// The twenty nine thirty second Value: 29d / 32d.
        /// </summary>
        public const double TwentynineThirtysecond = 29d / 32d;

        /// <summary>
        /// The fifty nine sixty fourth Value: 59d / 64d.
        /// </summary>
        public const double FiftynineSixtyfourth = 59d / 64d;

        /// <summary>
        /// The fifteen sixteenth Value: 15d / 16d.
        /// </summary>
        public const double FifteenSixteenth = 15d / 16d;

        /// <summary>
        /// The sixty one sixty fourth Value: 61d / 64d.
        /// </summary>
        public const double SixtyoneSixtyfourth = 61d / 64d;

        /// <summary>
        /// The thirty one thirty second Value: 31d / 32d.
        /// </summary>
        public const double ThirtyoneThirtysecond = 31d / 32d;

        /// <summary>
        /// The sixty three sixty fourth Value: 63d / 64d.
        /// </summary>
        public const double SixtythreeSixtyfourth = 63d / 64d;

        /// <summary>
        /// The one Value: 1d.
        /// </summary>
        public const double One = 1d;

        /// <summary>
        /// The one twenty seventh Value: 1 / 27.
        /// </summary>
        public const double OneTwentySeventh = 1d / 27d;
        #endregion Fractions

        #region Color Constants
        /// <summary>
        /// The lower limit for percentages.
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const double PercentMin = 0d;

        /// <summary>
        /// The upper limit for percentages.
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const double PercentMax = 1d;

        /// <summary>
        /// The lower limit for H.
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const double HueMin = 0d;

        /// <summary>
        /// The upper limit for H.
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const double HueMax = 360d;

        /// <summary>
        /// The lower limit for R, G, B (integer version).
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const byte RGBMin = 0;

        /// <summary>
        /// The upper limit for R, G, B (integer version).
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const byte RGBMax = 255;

        /// <summary>
        /// The lower limit for R, G, B (integer version).
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const byte CMYKMin = 0;

        /// <summary>
        /// The upper limit for R, G, B (integer version).
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const byte CMYKMax = 100;

        /// <summary>
        /// The lower limit for I in YIQ.
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const double YIQMinI = -0.5957d;

        /// <summary>
        /// The upper limit for I in YIQ.
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const double YIQMaxI = 0.5957d;

        /// <summary>
        /// The lower limit for Q in YIQ.
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const double YIQMinQ = -0.5226d;

        /// <summary>
        /// The upper limit for Q in YIQ.
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const double YIQMaxQ = 0.5226d;

        /// <summary>
        /// The lower limit for U in YUV.
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const double YUVMinU = -0.436d;

        /// <summary>
        /// The upper limit for U in YUV.
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const double YUVMaxU = 0.436d;

        /// <summary>
        /// The lower limit for V in YUV.
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const double YUVMinV = -0.615d;

        /// <summary>
        /// The upper limit for V in YUV.
        /// </summary>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        public const double YUVMaxV = 0.615d;
        #endregion Color Constants

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
        public static readonly double LogTwo = Log(2d);

        /// <summary>
        /// The Log of Ten.
        /// </summary>
        public static readonly double LogTen = Log(10d);

        /// <summary>
        /// The inverse of the log of two.
        /// </summary>
        public static readonly double InverseLogTwo = 1d / LogTwo;
        #endregion Logarithms

        #region Matrices
        /// <summary>
        /// The identity matrix 2x2.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2,
            double m2x1, double m2x2
            ) IdentityMatrix2x2 = 
            (1d, 0d,
            0d, 1d);

        /// <summary>
        /// The identity matrix 3x3.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3
            ) IdentityMatrix3x3 = 
            (1d, 0d, 0d,
            0d, 1d, 0d,
            0d, 0d, 1d);

        /// <summary>
        /// The identity matrix 4x4.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4
            ) IdentityMatrix4x4 = 
            (1d, 0d, 0d, 0d,
            0d, 1d, 0d, 0d,
            0d, 0d, 1d, 0d,
            0d, 0d, 0d, 1d);

        /// <summary>
        /// The identity matrix 5x5.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) IdentityMatrix5x5 = 
            (1d, 0d, 0d, 0d, 0d,
            0d, 1d, 0d, 0d, 0d,
            0d, 0d, 1d, 0d, 0d,
            0d, 0d, 0d, 1d, 0d,
            0d, 0d, 0d, 0d, 1d);

        /// <summary>
        /// The identity matrix 6x6.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6
            ) IdentityMatrix6x6 = 
            (1d, 0d, 0d, 0d, 0d, 0d,
            0d, 1d, 0d, 0d, 0d, 0d,
            0d, 0d, 1d, 0d, 0d, 0d,
            0d, 0d, 0d, 1d, 0d, 0d,
            0d, 0d, 0d, 0d, 1d, 0d,
            0d, 0d, 0d, 0d, 0d, 1d);

        /// <summary>
        /// The identity matrix 7x7.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7
            ) IdentityMatrix7x7 = 
            (1d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 1d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 1d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 1d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 1d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 1d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 1d);

        /// <summary>
        /// The identity matrix 8x8.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7, double m1x8,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7, double m2x8,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7, double m3x8,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7, double m4x8,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7, double m5x8,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7, double m6x8,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7, double m7x8,
            double m8x1, double m8x2, double m8x3, double m8x4, double m8x5, double m8x6, double m8x7, double m8x8
            ) IdentityMatrix8x8 = 
            (1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 1d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 1d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 1d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 1d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 1d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 1d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 0d, 1d);

        /// <summary>
        /// The identity matrix 9x9.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7, double m1x8, double m1x9,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7, double m2x8, double m2x9,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7, double m3x8, double m3x9,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7, double m4x8, double m4x9,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7, double m5x8, double m5x9,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7, double m6x8, double m6x9,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7, double m7x8, double m7x9,
            double m8x1, double m8x2, double m8x3, double m8x4, double m8x5, double m8x6, double m8x7, double m8x8, double m8x9,
            double m9x1, double m9x2, double m9x3, double m9x4, double m9x5, double m9x6, double m9x7, double m9x8, double m9x9
            ) IdentityMatrix9x9 = 
            (1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 1d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 1d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 1d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 1d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 1d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 0d, 1d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 1d);

        /// <summary>
        /// The identity matrix 10x10.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7, double m1x8, double m1x9, double m1x10,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7, double m2x8, double m2x9, double m2x10,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7, double m3x8, double m3x9, double m3x10,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7, double m4x8, double m4x9, double m4x10,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7, double m5x8, double m5x9, double m5x10,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7, double m6x8, double m6x9, double m6x10,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7, double m7x8, double m7x9, double m7x10,
            double m8x1, double m8x2, double m8x3, double m8x4, double m8x5, double m8x6, double m8x7, double m8x8, double m8x9, double m8x10,
            double m9x1, double m9x2, double m9x3, double m9x4, double m9x5, double m9x6, double m9x7, double m9x8, double m9x9, double m9x10,
            double m10x1, double m10x2, double m10x3, double m10x4, double m10x5, double m10x6, double m10x7, double m10x8, double m10x9, double m10x10
            ) IdentityMatrix10x10 = 
            (1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 1d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 1d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 1d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 1d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 0d, 1d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 1d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 1d);

        /// <summary>
        /// The identity matrix 11x11.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7, double m1x8, double m1x9, double m1x10, double m1x11,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7, double m2x8, double m2x9, double m2x10, double m2x11,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7, double m3x8, double m3x9, double m3x10, double m3x11,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7, double m4x8, double m4x9, double m4x10, double m4x11,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7, double m5x8, double m5x9, double m5x10, double m5x11,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7, double m6x8, double m6x9, double m6x10, double m6x11,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7, double m7x8, double m7x9, double m7x10, double m7x11,
            double m8x1, double m8x2, double m8x3, double m8x4, double m8x5, double m8x6, double m8x7, double m8x8, double m8x9, double m8x10, double m8x11,
            double m9x1, double m9x2, double m9x3, double m9x4, double m9x5, double m9x6, double m9x7, double m9x8, double m9x9, double m9x10, double m9x11,
            double m10x1, double m10x2, double m10x3, double m10x4, double m10x5, double m10x6, double m10x7, double m10x8, double m10x9, double m10x10, double m10x11,
            double m11x1, double m11x2, double m11x3, double m11x4, double m11x5, double m11x6, double m11x7, double m11x8, double m11x9, double m11x10, double m11x11
            ) IdentityMatrix11x11 = 
            (1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 1d, 0d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 1d, 0d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 1d, 0d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 0d, 1d, 0d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 1d, 0d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 1d, 0d,
             0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 1d);

        /// <summary>
        /// The cubic Hermite Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4
            ) CubicHermiteBernsteinBasisMatrix = 
            (1, 0, 0, 0,
            0, 1, 0, 0,
            -3, -2, 3, -1,
            2, 1, -2, 1);

        /// <summary>
        /// The linear Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2,
            double m2x1, double m2x2
            ) LinearBezierBernsteinBasisMatrix =
            (1d, 0d,
            -1d, 1d);

        /// <summary>
        /// The inverse linear Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2,
            double m2x1, double m2x2
            ) InverseLinearBezierBernsteinBasisMatrix =
            (1d, 0d,
            1d, 1d);

        /// <summary>
        /// The quadratic Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3
            ) QuadraticBezierBernsteinBasisMatrix =
            (1d, 0d, 0d,
            -2d, 2d, 0d,
            1d, -2d, 1d);

        /// <summary>
        /// The inverse quadratic Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3
            ) InverseQuadraticBezierBernsteinBasisMatrix =
            (1d, 0d, 0d,
            1d, OneHalf, 0d,
            1d, 1d, 1d);

        /// <summary>
        /// The cubic Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4
            ) CubicBezierBernsteinBasisMatrix =
            (1d, 0d, 0d, 0d,
            -3d, 3d, 0d, 0d,
            3d, -6d, 3d, 0d,
            -1d, 3d, -3d, 1d);

        /// <summary>
        /// The inverse cubic Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4
            ) InverseCubicBezierBernsteinBasisMatrix =
            (1d, 0d, 0d, 0d,
            1d, OneThird, 0d, 0d,
            1d, TwoThirds, OneThird, 0d,
            1d, 1d, 1d, 1d);

        /// <summary>
        /// The quartic Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) QuarticBezierBernsteinBasisMatrix =
            (1d, 0d, 0d, 0d, 0d,
            -4d, 4d, 0d, 0d, 0d,
            6d, -12d, 6d, 0d, 0d,
            -4d, 12d, -12d, 4d, 0d,
            1d, -4d, 6d, -4d, 1d);

        /// <summary>
        /// The inverse quartic Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) InverseQuarticBezierBernsteinBasisMatrix =
            (1d, 0d, 0d, 0d, 0d,
            1d, OneQuarter, 0d, 0d, 0d,
            1d, ThreeQuarters, TwoThirds, 0d, 0d,
            1d, ThreeQuarters, ThreeQuarters, OneQuarter, 0d,
            1d, 1d, 1d, 1d, 1d);

        /// <summary>
        /// The quintic Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6
            ) QuinticBezierBernsteinBasisMatrix =
            (1d, 0d, 0d, 0d, 0d, 0d,
            -5d, 5d, 0d, 0d, 0d, 0d,
            10d, -20d, 10d, 0d, 0d, 0d,
            -10d, 30d, -30d, 10d, 0d, 0d,
            5d, -20d, 30d, -20d, 5d, 0d,
            -1d, 5d, -10d, 10d, -5d, 1d);

        /// <summary>
        /// The sextic Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7
            ) SexticBezierBernsteinBasisMatrix =
            (1d, 0d, 0d, 0d, 0d, 0d, 0d,
            -6d, 6d, 0d, 0d, 0d, 0d, 0d,
            15d, -30d, 15d, 0d, 0d, 0d, 0d,
            -20d, 60d, -60d, 20d, 0d, 0d, 0d,
            15d, -60d, 90d, -60d, 15d, 0d, 0d,
            -6d, 30d, -60d, 60d, -30d, 6d, 0d,
            1d, -6d, 15d, -20d, 15d, -6d, 1d);

        /// <summary>
        /// The septic Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7, double m1x8,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7, double m2x8,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7, double m3x8,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7, double m4x8,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7, double m5x8,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7, double m6x8,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7, double m7x8,
            double m8x1, double m8x2, double m8x3, double m8x4, double m8x5, double m8x6, double m8x7, double m8x8
            ) SepticBezierBernsteinBasisMatrix =
            (1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
            -7d, 7d, 0d, 0d, 0d, 0d, 0d, 0d,
            21d, -42d, 21d, 0d, 0d, 0d, 0d, 0d,
            -35d, 105d, -105d, 35d, 0d, 0d, 0d, 0d,
            35d, -140d, 210d, -140d, 35d, 0d, 0d, 0d,
            -21d, 105d, -210d, 210d, -105d, 21d, 0d, 0d,
            7d, -42d, 105d, -140d, 105d, -42d, 7d, 0d,
            -1d, 7d, -21d, 35d, -35d, 21d, -7d, 1d);

        /// <summary>
        /// The octic Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7, double m1x8, double m1x9,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7, double m2x8, double m2x9,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7, double m3x8, double m3x9,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7, double m4x8, double m4x9,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7, double m5x8, double m5x9,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7, double m6x8, double m6x9,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7, double m7x8, double m7x9,
            double m8x1, double m8x2, double m8x3, double m8x4, double m8x5, double m8x6, double m8x7, double m8x8, double m8x9,
            double m9x1, double m9x2, double m9x3, double m9x4, double m9x5, double m9x6, double m9x7, double m9x8, double m9x9
            ) OcticBezierBernsteinBasisMatrix =
            (1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
            -8d, 8d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
            28d, -56d, 28d, 0d, 0d, 0d, 0d, 0d, 0d,
            -56d, 168d, -168d, 56d, 0d, 0d, 0d, 0d, 0d,
            70d, -280d, 420d, -280d, 70d, 0d, 0d, 0d, 0d,
            -56d, 280d, -560d, 560d, -280d, 56d, 0d, 0d, 0d,
            28d, -168d, 420d, -560d, 420d, -168d, 28d, 0d, 0d,
            -8d, 56d, -168d, 280d, -280d, 168d, -56d, 8d, 0d,
            1d, -8d, 28d, -56d, 70d, -56d, 28d, -8d, 1d);

        /// <summary>
        /// The nonic Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7, double m1x8, double m1x9, double m1x10,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7, double m2x8, double m2x9, double m2x10,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7, double m3x8, double m3x9, double m3x10,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7, double m4x8, double m4x9, double m4x10,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7, double m5x8, double m5x9, double m5x10,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7, double m6x8, double m6x9, double m6x10,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7, double m7x8, double m7x9, double m7x10,
            double m8x1, double m8x2, double m8x3, double m8x4, double m8x5, double m8x6, double m8x7, double m8x8, double m8x9, double m8x10,
            double m9x1, double m9x2, double m9x3, double m9x4, double m9x5, double m9x6, double m9x7, double m9x8, double m9x9, double m9x10,
            double m10x1, double m10x2, double m10x3, double m10x4, double m10x5, double m10x6, double m10x7, double m10x8, double m10x9, double m10x10
            ) NonicBezierBernsteinBasisMatrix =
            (1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
            -9d, 9d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
            36d, -72d, 36d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
            -84d, 252d, -252d, 84d, 0d, 0d, 0d, 0d, 0d, 0d,
            126d, -504d, 756d, -504d, 126d, 0d, 0d, 0d, 0d, 0d,
            -126d, 630d, -1260d, 1260d, -630d, 126d, 0d, 0d, 0d, 0d,
            84d, -504d, 1260d, -1680d, 1260d, -504d, 84d, 0d, 0d, 0d,
            -36d, 252d, -756d, 1260d, -1260d, 756d, -252d, 72d, 0d, 0d,
            9d, -72d, 252d, -504d, 630d, -504d, 252d, -72d, 9d, 0d,
            -1d, 9d, -36d, 84d, -126d, 126, -84d, 36, 9d, 1d);

        /// <summary>
        /// The decic Bezier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7, double m1x8, double m1x9, double m1x10, double m1x11,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7, double m2x8, double m2x9, double m2x10, double m2x11,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7, double m3x8, double m3x9, double m3x10, double m3x11,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7, double m4x8, double m4x9, double m4x10, double m4x11,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7, double m5x8, double m5x9, double m5x10, double m5x11,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7, double m6x8, double m6x9, double m6x10, double m6x11,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7, double m7x8, double m7x9, double m7x10, double m7x11,
            double m8x1, double m8x2, double m8x3, double m8x4, double m8x5, double m8x6, double m8x7, double m8x8, double m8x9, double m8x10, double m8x11,
            double m9x1, double m9x2, double m9x3, double m9x4, double m9x5, double m9x6, double m9x7, double m9x8, double m9x9, double m9x10, double m9x11,
            double m10x1, double m10x2, double m10x3, double m10x4, double m10x5, double m10x6, double m10x7, double m10x8, double m10x9, double m10x10, double m10x11,
            double m11x1, double m11x2, double m11x3, double m11x4, double m11x5, double m11x6, double m11x7, double m11x8, double m11x9, double m11x10, double m11x11
            ) DecicBezierBernsteinBasisMatrix =
            (1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
            -10d, 10d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
            45d, -90d, 45d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
            -120d, 360d, -360d, 120d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
            210d, -840d, 1260d, -840d, 210d, 0d, 0d, 0d, 0d, 0d, 0d,
            -252d, 1260d, -2520d, 2520d, -1260d, 252d, 0d, 0d, 0d, 0d, 0d,
            210d, -1260d, 3150d, -4200d, 3150d, -1260d, 210d, 0d, 0d, 0d, 0d,
            -120d, 840d, -2520d, 4200d, -4200d, 2520d, -840d, 120d, 0d, 0d, 0d,
            45d, -360d, 1260d, -2520d, 3150d, -2520d, 1260d, -360d, 45d, 0d, 0d,
            -10d, 90d, -360d, 840d, -1260d, 1260, -840d, 360, 90d, 10d, 0d,
            1d, -10d, 45d, -120d, 210d, -252d, 210d, -120d, 45d, -10d, 1d);
        #endregion Matrices

        #region Gauss Tables
        /// <summary>
        /// Gauss abscissa table
        /// </summary>
        /// <acknowledgment>
        /// https://code.google.com/archive/p/degrafa/source/default/source
        /// </acknowledgment>
        public static readonly double[] abscissa = new double[]
        {
            // N=2
            -0.5773502692d,
                0.5773502692d,
            // N=3
            -0.7745966692d,
                0.7745966692d,
                0.0000000000d,
            // N=4
            -0.8611363116d,
                0.8611363116d,
            -0.3399810436d,
                0.3399810436d,
            // N=5
            -0.9061798459d,
                0.9061798459d,
            -0.5384693101d,
                0.5384693101d,
                0.0000000000d,
            // N=6
            -0.9324695142d,
                0.9324695142d,
            -0.6612093865d,
                0.6612093865d,
            -0.2386191861d,
                0.2386191861d,
            // N=7
            -0.9491079123d,
                0.9491079123d,
            -0.7415311856d,
                0.7415311856d,
            -0.4058451514d,
                0.4058451514d,
                0.0000000000d,
            // N=8
            -0.9602898565d,
                0.9602898565d,
            -0.7966664774d,
                0.7966664774d,
            -0.5255324099d,
                0.5255324099d,
            -0.1834346425d,
                0.1834346425d
        };

        /// <summary>
        /// Gauss weight table
        /// </summary>
        /// <acknowledgment>
        /// https://code.google.com/archive/p/degrafa/source/default/source
        /// </acknowledgment>
        public static readonly double[] weight = new double[]
        {
            // N=2
            1.0000000000d,
            1.0000000000d,
            // N=3
            0.5555555556d,
            0.5555555556d,
            0.8888888888d,
            // N=4
            0.3478548451d,
            0.3478548451d,
            0.6521451549d,
            0.6521451549d,
            // N=5
            0.2369268851d,
            0.2369268851d,
            0.4786286705d,
            0.4786286705d,
            0.5688888888d,
            // N=6
            0.1713244924d,
            0.1713244924d,
            0.3607615730d,
            0.3607615730d,
            0.4679139346d,
            0.4679139346d,
            // N=7
            0.1294849662d,
            0.1294849662d,
            0.2797053915d,
            0.2797053915d,
            0.3818300505d,
            0.3818300505d,
            0.4179591837d,
            // N=8
            0.1012285363d,
            0.1012285363d,
            0.2223810345d,
            0.2223810345d,
            0.3137066459d,
            0.3137066459d,
            0.3626837834d,
            0.3626837834d
        };
        #endregion Gauss Tables
    }
}
