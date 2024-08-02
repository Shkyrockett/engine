// <copyright file="MathematicalConstants.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Numerics;
using static Engine.Operations;

namespace Engine;

/// <summary>
/// The Mathematics constants class.
/// </summary>
public static partial class Floats<T>
    where T : IFloatingPointIeee754<T>
{
    #region Epsilons, Minimums, Maximums
    /// <summary>
    /// The horizontal Value: T.NegativeInfinity.
    /// </summary>
    public static readonly T horizontal = T.NegativeInfinity;

    /// <summary>
    /// The tolerance Value: 1e-6d.
    /// </summary>
    public static readonly T Tolerance = T.CreateChecked(1e-6);

    /// <summary>
    /// The accuracy Value: T.CreateChecked(15).
    /// </summary>
    public static readonly T Accuracy = T.CreateChecked(15);

    /// <summary>
    /// Error difference for line intersection tests.
    /// </summary>
    public static readonly T Epsilon = T.CreateChecked(5.684341886080801536e-12);

    /// <summary>
    /// Smallest such that 1.0 + <see cref="Epsilon"/> != 1.0
    /// </summary>
    public const double DoubleEpsilon = 2.2204460492503131e-016d;

    /// <summary>
    /// The T round limit.
    /// </summary>
    public const double DoubleRoundLimit = 1E+16;

    /// <summary>
    /// Smallest such that 1.0 + <see cref="FloatEpsilon"/> != 1.0
    /// </summary>
    public const float FloatEpsilon = 1.192092896e-07f;

    /// <summary>
    /// Number close to zero, where float.MinValue is -float.MaxValue
    /// </summary>
    public const float FloatMin = 1.175494351e-38f;

    /// <summary>
    /// The near zero epsilon Value: 1E-20.
    /// </summary>
    public static readonly T NearZeroEpsilon = T.CreateChecked(1E-20);

    /// <summary>
    /// SlopeMax is a large value "close to infinity" (Close to the largest value allowed for the data
    /// type). Used in the Slope of a LineSeg
    /// </summary>

    public static readonly T SlopeMax = T.CreateChecked(9223372036854775807);

    /// <summary>
    /// The default arc tolerance Value: 0.25.
    /// </summary>
    public static readonly T DefaultArcTolerance = T.CreateChecked(0.25);

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

    /// <summary>
    /// The negative one
    /// </summary>
    public static readonly T NegativeOne = T.NegativeOne;

    /// <summary>
    /// Negative zero.
    /// </summary>
    /// <remarks>
    /// <para>Might be useful with Atan2
    /// http://www.charlespetzold.com/blog/2008/09/180741.html</para>
    /// </remarks>
    public static readonly T NegativeZero = T.NegativeZero;

    /// <summary>
    /// Negative infinity.
    /// </summary>
    public static readonly T NegativeInfinity = T.NegativeInfinity;

    /// <summary>
    /// Positive infinity.
    /// </summary>
    public static readonly T PositiveInfinity = T.PositiveInfinity;

    #region Fractions
    /// <summary>
    /// The one sixty fourth Value: T.One / T.CreateChecked(64).
    /// </summary>
    public static readonly T OneSixtyfourth = T.One / T.CreateChecked(64);

    /// <summary>
    /// The one thirty second Value: T.One / T.CreateChecked(32).
    /// </summary>
    public static readonly T OneThirtysecond = T.One / T.CreateChecked(32);

    /// <summary>
    /// The three sixty fourth Value: T.CreateChecked(3) / T.CreateChecked(64).
    /// </summary>
    public static readonly T ThreeSixtyfourth = Integers<T>.Three / T.CreateChecked(64);

    /// <summary>
    /// The one sixteenth Value: T.One / T.CreateChecked(16).
    /// </summary>
    public static readonly T OneSixteenth = T.One / T.CreateChecked(16);

    /// <summary>
    /// The five sixty fourth Value: T.CreateChecked(5) / T.CreateChecked(64).
    /// </summary>
    public static readonly T FiveSixtyfourth = T.CreateChecked(5) / T.CreateChecked(64);

    /// <summary>
    /// The three thirty second Value: Three / T.CreateChecked(32).
    /// </summary>
    public static readonly T ThreeThirtysecond = Integers<T>.Three / T.CreateChecked(32);

    /// <summary>
    /// The seven sixty fourth Value: T.CreateChecked(7) / T.CreateChecked(64).
    /// </summary>
    public static readonly T SevenSixtyfourth = T.CreateChecked(7) / T.CreateChecked(64);

    /// <summary>
    /// The one eighth Value: T.One / T.CreateChecked(8).
    /// </summary>
    public static readonly T OneEighth = T.One / T.CreateChecked(8);

    /// <summary>
    /// The nine sixty fourth Value: T.CreateChecked(9) / T.CreateChecked(64).
    /// </summary>
    public static readonly T NineSixtyfourth = T.CreateChecked(9) / T.CreateChecked(64);

    /// <summary>
    /// The five thirty second Value: T.CreateChecked(5) / T.CreateChecked(32).
    /// </summary>
    public static readonly T FiveThirtysecond = T.CreateChecked(5) / T.CreateChecked(32);

    /// <summary>
    /// The eleven sixty fourth Value: T.CreateChecked(11) / T.CreateChecked(64).
    /// </summary>
    public static readonly T ElevenSixtyfourth = T.CreateChecked(11) / T.CreateChecked(64);

    /// <summary>
    /// The three sixteenth Value: T.CreateChecked(3) / T.CreateChecked(16).
    /// </summary>
    public static readonly T ThreeSixteenth = Integers<T>.Three / T.CreateChecked(16);

    /// <summary>
    /// The thirteen sixty fourth Value: T.CreateChecked(13) / T.CreateChecked(64).
    /// </summary>
    public static readonly T ThirteenSixtyfourth = T.CreateChecked(13) / T.CreateChecked(64);

    /// <summary>
    /// The seven thirty second Value: T.CreateChecked(7) / T.CreateChecked(32).
    /// </summary>
    public static readonly T SevenThirtysecond = T.CreateChecked(7) / T.CreateChecked(32);

    /// <summary>
    /// The fifteen sixty fourth Value: T.CreateChecked(15) / T.CreateChecked(64).
    /// </summary>
    public static readonly T FifteenSixtyfourth = T.CreateChecked(15) / T.CreateChecked(64);

    /// <summary>
    /// The one quarter Value: T.One / T.CreateChecked(4).
    /// </summary>
    public static readonly T OneQuarter = T.One / T.CreateChecked(4);

    /// <summary>
    /// The seventeen sixty fourth Value: T.CreateChecked(17) / T.CreateChecked(64).
    /// </summary>
    public static readonly T SeventeenSixtyfourth = T.CreateChecked(17) / T.CreateChecked(64);

    /// <summary>
    /// The nine thirty second Value: T.CreateChecked(9) / T.CreateChecked(32).
    /// </summary>
    public static readonly T NineThirtysecond = T.CreateChecked(9) / T.CreateChecked(32);

    /// <summary>
    /// The nineteen sixty fourth Value: T.CreateChecked(19) / T.CreateChecked(64).
    /// </summary>
    public static readonly T NineteenSixtyfourth = T.CreateChecked(19) / T.CreateChecked(64);

    /// <summary>
    /// The five sixteenth Value: T.CreateChecked(5) / T.CreateChecked(16).
    /// </summary>
    public static readonly T FiveSixteenth = T.CreateChecked(5) / T.CreateChecked(16);

    /// <summary>
    /// The twenty one sixty fourth Value: T.CreateChecked(21) / T.CreateChecked(64).
    /// </summary>
    public static readonly T TwentyoneSixtyfourth = T.CreateChecked(21) / T.CreateChecked(64);

    /// <summary>
    /// The one third Value: T.One / T.CreateChecked(3).
    /// </summary>
    public static readonly T OneThird = T.One / Integers<T>.Three;

    /// <summary>
    /// The eleven thirty second Value: T.CreateChecked(11) / T.CreateChecked(32).
    /// </summary>
    public static readonly T ElevenThirtysecond = T.CreateChecked(11) / T.CreateChecked(32);

    /// <summary>
    /// The twenty three sixty fourth Value: T.CreateChecked(23) / T.CreateChecked(64).
    /// </summary>
    public static readonly T TwentythreeSixtyfourth = T.CreateChecked(23) / T.CreateChecked(64);

    /// <summary>
    /// The three eighths Value: T.CreateChecked(3) / T.CreateChecked(8).
    /// </summary>
    public static readonly T ThreeEighths = Integers<T>.Three / T.CreateChecked(8);

    /// <summary>
    /// The twenty five sixty fourth Value: T.CreateChecked(25) / T.CreateChecked(64).
    /// </summary>
    public static readonly T TwentyfiveSixtyfourth = T.CreateChecked(25) / T.CreateChecked(64);

    /// <summary>
    /// The thirteen thirty second Value: T.CreateChecked(13) / T.CreateChecked(32).
    /// </summary>
    public static readonly T ThirteenThirtysecond = T.CreateChecked(13) / T.CreateChecked(32);

    /// <summary>
    /// The twenty seven sixty fourth Value: T.CreateChecked(27) / T.CreateChecked(64).
    /// </summary>
    public static readonly T TwentysevenSixtyfourth = T.CreateChecked(27) / T.CreateChecked(64);

    /// <summary>
    /// The seven sixteenth Value: T.CreateChecked(7) / T.CreateChecked(16).
    /// </summary>
    public static readonly T SevenSixteenth = T.CreateChecked(7) / T.CreateChecked(16);

    /// <summary>
    /// The twenty nine sixty fourth Value: T.CreateChecked(29) / T.CreateChecked(64).
    /// </summary>
    public static readonly T TwentynineSixtyfourth = T.CreateChecked(29) / T.CreateChecked(64);

    /// <summary>
    /// The fifteen thirty second Value: T.CreateChecked(15) / T.CreateChecked(32).
    /// </summary>
    public static readonly T FifteenThirtysecond = T.CreateChecked(15) / T.CreateChecked(32);

    /// <summary>
    /// The thirty one sixty fourth Value: T.CreateChecked(31) / T.CreateChecked(64).
    /// </summary>
    public static readonly T ThirtyoneSixtyfourth = T.CreateChecked(31) / T.CreateChecked(64);

    /// <summary>
    /// The one half Value: 1 / 2.
    /// </summary>
    public static readonly T OneHalf = T.One / T.CreateChecked(2);

    /// <summary>
    /// The thirty three sixty fourth Value: T.CreateChecked(33) / T.CreateChecked(64).
    /// </summary>
    public static readonly T ThirtythreeSixtyfourth = T.CreateChecked(33) / T.CreateChecked(64);

    /// <summary>
    /// The seventeen thirty second Value: T.CreateChecked(17) / T.CreateChecked(32).
    /// </summary>
    public static readonly T SeventeenThirtysecond = T.CreateChecked(17) / T.CreateChecked(32);

    /// <summary>
    /// The thirty five sixty fourth Value: T.CreateChecked(35) / T.CreateChecked(64).
    /// </summary>
    public static readonly T ThirtyfiveSixtyfourth = T.CreateChecked(35) / T.CreateChecked(64);

    /// <summary>
    /// The nine sixteenth Value: T.CreateChecked(9) / T.CreateChecked(16).
    /// </summary>
    public static readonly T NineSixteenth = T.CreateChecked(9) / T.CreateChecked(16);

    /// <summary>
    /// The thirty seven sixty fourth Value: T.CreateChecked(37) / T.CreateChecked(64).
    /// </summary>
    public static readonly T ThirtysevenSixtyfourth = T.CreateChecked(37) / T.CreateChecked(64);

    /// <summary>
    /// The nineteen thirty second Value: T.CreateChecked(19) / T.CreateChecked(32).
    /// </summary>
    public static readonly T NineteenThirtysecond = T.CreateChecked(19) / T.CreateChecked(32);

    /// <summary>
    /// The thirty nine sixty fourth Value: T.CreateChecked(39) / T.CreateChecked(64).
    /// </summary>
    public static readonly T ThirtynineSixtyfourth = T.CreateChecked(39) / T.CreateChecked(64);

    /// <summary>
    /// The five eighths Value: T.CreateChecked(5) / T.CreateChecked(8).
    /// </summary>
    public static readonly T FiveEighths = T.CreateChecked(5) / T.CreateChecked(8);

    /// <summary>
    /// The forty one sixty fourth Value: T.CreateChecked(41) / T.CreateChecked(64).
    /// </summary>
    public static readonly T FortyoneSixtyfourth = T.CreateChecked(41) / T.CreateChecked(64);

    /// <summary>
    /// The twenty one thirty second Value: T.CreateChecked(21) / T.CreateChecked(32).
    /// </summary>
    public static readonly T TwentyoneThirtysecond = T.CreateChecked(21) / T.CreateChecked(32);

    /// <summary>
    /// The two thirds Value: T.CreateChecked(2) / T.CreateChecked(3).
    /// </summary>
    public static readonly T TwoThirds = T.CreateChecked(2) / Integers<T>.Three;

    /// <summary>
    /// The forty three sixty fourth Value: T.CreateChecked(43) / T.CreateChecked(64).
    /// </summary>
    public static readonly T FortythreeSixtyfourth = T.CreateChecked(43) / T.CreateChecked(64);

    /// <summary>
    /// The eleven sixteenth Value: T.CreateChecked(11) / T.CreateChecked(16).
    /// </summary>
    public static readonly T ElevenSixteenth = T.CreateChecked(11) / T.CreateChecked(16);

    /// <summary>
    /// The forty five sixty fourth Value: T.CreateChecked(45) / T.CreateChecked(64).
    /// </summary>
    public static readonly T FortyfiveSixtyfourth = T.CreateChecked(45) / T.CreateChecked(64);

    /// <summary>
    /// The twenty three thirty second Value: T.CreateChecked(23) / T.CreateChecked(32).
    /// </summary>
    public static readonly T TwentythreeThirtysecond = T.CreateChecked(23) / T.CreateChecked(32);

    /// <summary>
    /// The forty seven sixty fourth Value: T.CreateChecked(47) / T.CreateChecked(64).
    /// </summary>
    public static readonly T FortysevenSixtyfourth = T.CreateChecked(47) / T.CreateChecked(64);

    /// <summary>
    /// The three quarters Value: T.CreateChecked(3) / T.CreateChecked(4).
    /// </summary>
    public static readonly T ThreeQuarters = Integers<T>.Three / T.CreateChecked(4);

    /// <summary>
    /// The forty nine sixty fourth Value: T.CreateChecked(49) / T.CreateChecked(64).
    /// </summary>
    public static readonly T FortynineSixtyfourth = T.CreateChecked(49) / T.CreateChecked(64);

    /// <summary>
    /// The twenty five thirty second Value: T.CreateChecked(25) / T.CreateChecked(32).
    /// </summary>
    public static readonly T TwentyfiveThirtysecond = T.CreateChecked(25) / T.CreateChecked(32);

    /// <summary>
    /// The fifty one sixty fourth Value: T.CreateChecked(51) / T.CreateChecked(64).
    /// </summary>
    public static readonly T FiftyoneSixtyfourth = T.CreateChecked(51) / T.CreateChecked(64);

    /// <summary>
    /// The thirteen sixteenth Value: T.CreateChecked(13) / T.CreateChecked(16).
    /// </summary>
    public static readonly T ThirteenSixteenth = T.CreateChecked(13) / T.CreateChecked(16);

    /// <summary>
    /// The fifty three sixty fourth Value: T.CreateChecked(53) / T.CreateChecked(64).
    /// </summary>
    public static readonly T FiftythreeSixtyfourth = T.CreateChecked(53) / T.CreateChecked(64);

    /// <summary>
    /// The twenty seven thirty second Value: T.CreateChecked(27) / T.CreateChecked(32).
    /// </summary>
    public static readonly T TwentysevenThirtysecond = T.CreateChecked(27) / T.CreateChecked(32);

    /// <summary>
    /// The fifty five sixty fourth Value: T.CreateChecked(55) / T.CreateChecked(64).
    /// </summary>
    public static readonly T FiftyfiveSixtyfourth = T.CreateChecked(55) / T.CreateChecked(64);

    /// <summary>
    /// The seven eighths Value: T.CreateChecked(7) / T.CreateChecked(8).
    /// </summary>
    public static readonly T SevenEighths = T.CreateChecked(7) / T.CreateChecked(8);

    /// <summary>
    /// The fifty seven sixty fourth Value: T.CreateChecked(57) / T.CreateChecked(64).
    /// </summary>
    public static readonly T FiftysevenSixtyfourth = T.CreateChecked(57) / T.CreateChecked(64);

    /// <summary>
    /// The twenty nine thirty second Value: T.CreateChecked(29) / T.CreateChecked(32).
    /// </summary>
    public static readonly T TwentynineThirtysecond = T.CreateChecked(29) / T.CreateChecked(32);

    /// <summary>
    /// The fifty nine sixty fourth Value: T.CreateChecked(59) / T.CreateChecked(64).
    /// </summary>
    public static readonly T FiftynineSixtyfourth = T.CreateChecked(59) / T.CreateChecked(64);

    /// <summary>
    /// The fifteen sixteenth Value: T.CreateChecked(15) / T.CreateChecked(16).
    /// </summary>
    public static readonly T FifteenSixteenth = T.CreateChecked(15) / T.CreateChecked(16);

    /// <summary>
    /// The sixty one sixty fourth Value: T.CreateChecked(61) / T.CreateChecked(64).
    /// </summary>
    public static readonly T SixtyoneSixtyfourth = T.CreateChecked(61) / T.CreateChecked(64);

    /// <summary>
    /// The thirty one thirty second Value: T.CreateChecked(31) / T.CreateChecked(32).
    /// </summary>
    public static readonly T ThirtyoneThirtysecond = T.CreateChecked(31) / T.CreateChecked(32);

    /// <summary>
    /// The sixty three sixty fourth Value: T.CreateChecked(63) / T.CreateChecked(64).
    /// </summary>
    public static readonly T SixtythreeSixtyfourth = T.CreateChecked(63) / T.CreateChecked(64);

    /// <summary>
    /// The one twenty seventh Value: 1 / 27.
    /// </summary>
    public static readonly T OneTwentySeventh = T.One / T.CreateChecked(27);

    /// <summary>
    /// The one sixth Value: 1 / 6.
    /// </summary>
    public static readonly T OneSixth = T.One / Integers<T>.Six;
    #endregion Fractions

    /// <summary>
    /// Represents the golden ratio as specified by the constant, φ (phi).
    /// </summary>
    /// <value>≈1.61803...</value>
    public static readonly T Phi = (T.One + T.Sqrt(T.CreateChecked(5))) / T.CreateChecked(2); // 1.6180339887498948482045868343656d;

    /// <summary>
    ///  Represents the plastic constant as specified by the constant, ρ.
    /// </summary>
    /// <value>≈1.32471...</value>
    public static readonly T Rho = Root((T.One / T.CreateChecked(2)) + (OneSixth * T.Sqrt(T.CreateChecked(23) / Integers<T>.Three)), Integers<T>.Three) + Root((T.One / T.CreateChecked(2)) - (OneSixth * T.Sqrt(T.CreateChecked(23) / Integers<T>.Three)), Integers<T>.Three);

    #region Pi Derivations
    /// <summary>
    /// Represents the inverse of Pi, or the quotient of one over pi.
    /// </summary>
    public static readonly T InversePi = T.One / T.Pi; // 0.31830988618379067153776752674503d;

    /// <summary>
    /// Represents the inverse of Tau, or the quotient of one over 2 pi.
    /// </summary>
    public static readonly T InverseTau = T.One / T.Tau; // 0.15915494309189533576888376337251d;

    /// <summary>
    /// Represents the value of the T inverse of Pi, or the quotient of two over pi.
    /// </summary>
    public static readonly T Inverse2OverPi = Integers<T>.Two / T.Pi; // 0.63661977236758134307553505349006d;

    /// <summary>
    /// Represents the ratio of the radius of a circle to the first sixteenth of that circle.
    /// One sixteenth Tau or a eighth Pi.
    /// </summary>
    /// <remarks><para>T.Pi / 8</para></remarks>
    public static readonly T EighthPi = T.Pi / T.CreateChecked(8); // 0.39269908169872415480783042290994d;

    /// <summary>
    /// Represents the ratio of the radius of a circle to the first eighth of that circle.
    /// One eighth Tau or a quarter Pi. A 45 degree angle.
    /// </summary>
    /// <remarks><para>T.Pi / 4</para></remarks>
    public static readonly T Quart = T.Pi / T.CreateChecked(4); // 0.78539816339744830961566084581988d;

    /// <summary>
    /// Represents the ratio of the radius of a circle to the first quarter of that circle.
    /// One quarter Tau or half Pi. A right angle in mathematics.
    /// </summary>
    /// <remarks><para>T.Pi / 2</para></remarks>
    public static readonly T Hau = T.Pi / T.CreateChecked(2); // 1.5707963267948966192313216916398d;

    /// <summary>
    /// Represents the ratio of the circumference of a circle to its diameter, specified
    /// by the constant, π (Pi).
    /// One half Tau or One Pi.
    /// </summary>
    /// <value>≈3.1415926535897931...</value>
    public static readonly T Pi = T.Pi; // 3.1415926535897932384626433832795d;

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
    public static readonly T Pau = T.CreateChecked(1.5) * T.Pi; // 4.7123889803846898576939650749193d;

    /// <summary>
    /// The nearest value to 0 Cosine can produce for a right angle.
    /// </summary>
    public static readonly T CosineZeroEpsilon = T.Cos(Hau); //6.123233995736766E-17;

    /// <summary>
    /// The nearest value to 0 Cosine can produce for a reverse right angle.
    /// </summary>
    public static readonly T CosineNegativeZeroEpsilon = T.Cos(Pau);

    /// <summary>
    /// Represents the ratio of the circumference of a circle to its radius, specified
    /// by the proposed constant, τ (Tau).
    /// One Tau or two Pi.
    /// </summary>
    /// <value>≈6.28318...</value>
    public static readonly T Tau = T.Tau;//2d * T.Pi; // 6.283185307179586476925286766559d;

    /// <summary>
    /// One Radian.
    /// </summary>
    /// <remarks><para>T.Pi / 180</para></remarks>
    public static readonly T Radian = T.Pi / T.CreateChecked(180); // 0.01745329251994329576923690768489d;

    /// <summary>
    /// One half radian.
    /// </summary>
    public static readonly T HalfRadian = T.Pi / T.CreateChecked(90);

    /// <summary>
    /// One degree.
    /// </summary>
    /// <remarks><para>180 / T.Pi</para></remarks>
    public static readonly T Degree = T.CreateChecked(180) / T.Pi; // 57.295779513082320876798154814105d;
    #endregion Pi Derivations

    #region Sine Cosine of Regular Angles.
    /// <summary>
    /// The cosine of 0.
    /// </summary>
    public static readonly T Cos0 = T.Cos(T.Zero);

    /// <summary>
    /// The cosine of T.Pi.
    /// </summary>
    public static readonly T CosHalfPi = T.Cos(Hau);

    /// <summary>
    /// The cosine of Pi.
    /// </summary>
    public static readonly T CosPi = T.Cos(T.Pi);

    /// <summary>
    /// The cosine of Pau.
    /// </summary>
    public static readonly T CosPau = T.Cos(Pau);

    /// <summary>
    /// The sine of 0.
    /// </summary>
    public static readonly T Sin0 = T.Sin(T.Zero);

    /// <summary>
    /// The sine of half Pi.
    /// </summary>
    public static readonly T SinHalfPi = T.Sin(Hau);

    /// <summary>
    /// The sine of Pi.
    /// </summary>
    public static readonly T SinPi = T.Sin(Pi);

    /// <summary>
    /// The sine of Pau.
    /// </summary>
    public static readonly T SinPau = T.Sin(Pau);
    #endregion

    #region Roots
    /// <summary>
    /// Represents the inverse square root of 2.
    /// </summary>
    public static readonly T InvSqrt2 = T.One / T.Sqrt(Integers<T>.Two); // 0.70710678118654752440084436210485d;

    /// <summary>
    /// Represents the T inverse square root of Pi.
    /// </summary>
    public static readonly T Inv2SqrtPi = Integers<T>.Two / T.Sqrt(T.Pi); // 1.1283791670955125738961589031215d;

    /// <summary>
    /// Represents the constant value of the square root of 2.
    /// </summary>
    /// <value>≈1.41421...</value>
    public static readonly T Sqrt2 = T.Sqrt(Integers<T>.Two); // 1.4142135623730950488016887242097d;

    /// <summary>
    /// Represents the constant value of the square root of 3.
    /// </summary>
    /// <value>≈1.73205...</value>
    public static readonly T Sqrt3 = T.Sqrt(Integers<T>.Three); // 1.7320508075688772935274463415059d;

    /// <summary>
    /// Represents the constant value of the square root of 5.
    /// </summary>
    /// <value>≈2.23606...</value>
    public static readonly T Sqrt5 = T.Sqrt(Integers<T>.Five); // 2.2360679774997896964091736687313d;

    /// <summary>
    /// Represents the constant value of the cube root of 2.
    /// </summary>
    /// <value></value>
    public static readonly T Cbrt2 = T.Cbrt(Integers<T>.Two);

    /// <summary>
    /// Represents the constant value of the cube root of 3.
    /// </summary>
    /// <value></value>
    public static readonly T Cbrt3 = T.Cbrt(Integers<T>.Three);

    /// <summary>
    /// Represents the constant value of the cube root of 5.
    /// </summary>
    /// <value></value>
    public static readonly T Cbrt5 = T.Cbrt(Integers<T>.Five);
    #endregion Roots

    #region Color Constants
    /// <summary>
    /// The lower limit for percentages.
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public static readonly T PercentMin = T.Zero;

    /// <summary>
    /// The upper limit for percentages.
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public static readonly T PercentMax = T.One;

    /// <summary>
    /// The lower limit for I in YIQ.
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public static readonly T YIQMinI = T.CreateChecked(-0.5957);

    /// <summary>
    /// The upper limit for I in YIQ.
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public static readonly T YIQMaxI = T.CreateChecked(0.5957);

    /// <summary>
    /// The lower limit for Q in YIQ.
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public static readonly T YIQMinQ = T.CreateChecked(-0.5226);

    /// <summary>
    /// The upper limit for Q in YIQ.
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public static readonly T YIQMaxQ = T.CreateChecked(0.5226);

    /// <summary>
    /// The lower limit for U in YUV.
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public static readonly T YUVMinU = T.CreateChecked(-0.436);

    /// <summary>
    /// The upper limit for U in YUV.
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public static readonly T YUVMaxU = T.CreateChecked(0.436);

    /// <summary>
    /// The lower limit for V in YUV.
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public static readonly T YUVMinV = T.CreateChecked(-0.615);

    /// <summary>
    /// The upper limit for V in YUV.
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public static readonly T YUVMaxV = T.CreateChecked(0.615);
    #endregion Color Constants

    #region Logarithms
    /// <summary>
    ///
    /// </summary>
    public static readonly T E = T.E; // 2.7182818284590452353602874713527d;

    /// <summary>
    /// The base 2 natural log of e.
    /// </summary>
    public static readonly T Log2E = T.CreateChecked(1.44269504088896340736);

    /// <summary>
    /// The base 10 natural log of e.
    /// </summary>
    public static readonly T Log10E = T.CreateChecked(0.434294481903251827651); // 0.43429448190325182765112891891661d;

    /// <summary>
    /// The base 2 natural log.
    /// </summary>
    public static readonly T LN2 = T.CreateChecked(0.693147180559945309417);

    /// <summary>
    /// The base 10 natural log.
    /// </summary>
    public static readonly T LN10 = T.CreateChecked(2.30258509299404568402);

    /// <summary>
    /// The Log of Two.
    /// </summary>
    public static readonly T LogTwo = T.Log(Integers<T>.Two);

    /// <summary>
    /// The Log of Ten.
    /// </summary>
    public static readonly T LogTen = T.Log(Integers<T>.Ten);

    /// <summary>
    /// The inverse of the log of two.
    /// </summary>
    public static readonly T InverseLogTwo = T.One / LogTwo;
    #endregion Logarithms

    #region Matrices
    /// <summary>
    /// The identity matrix 2x2.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2,
        T m2x1, T m2x2
        ) IdentityMatrix2x2 =
        (T.One, T.Zero,
        T.Zero, T.One);

    /// <summary>
    /// The identity matrix 3x3.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3
        ) IdentityMatrix3x3 =
        (T.One, T.Zero, T.Zero,
        T.Zero, T.One, T.Zero,
        T.Zero, T.Zero, T.One);

    /// <summary>
    /// The identity matrix 4x4.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4
        ) IdentityMatrix4x4 =
        (T.One, T.Zero, T.Zero, T.Zero,
        T.Zero, T.One, T.Zero, T.Zero,
        T.Zero, T.Zero, T.One, T.Zero,
        T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>
    /// The identity matrix 5x5.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5
        ) IdentityMatrix5x5 =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero,
        T.Zero, T.One, T.Zero, T.Zero, T.Zero,
        T.Zero, T.Zero, T.One, T.Zero, T.Zero,
        T.Zero, T.Zero, T.Zero, T.One, T.Zero,
        T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>
    /// The identity matrix 6x6.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6
        ) IdentityMatrix6x6 =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero,
        T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero,
        T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero,
        T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero,
        T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>
    /// The identity matrix 7x7.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7
        ) IdentityMatrix7x7 =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>
    /// The identity matrix 8x8.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7, T m1x8,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7, T m2x8,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7, T m3x8,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7, T m4x8,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7, T m5x8,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7, T m6x8,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7, T m7x8,
        T m8x1, T m8x2, T m8x3, T m8x4, T m8x5, T m8x6, T m8x7, T m8x8
        ) IdentityMatrix8x8 =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>
    /// The identity matrix 9x9.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7, T m1x8, T m1x9,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7, T m2x8, T m2x9,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7, T m3x8, T m3x9,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7, T m4x8, T m4x9,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7, T m5x8, T m5x9,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7, T m6x8, T m6x9,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7, T m7x8, T m7x9,
        T m8x1, T m8x2, T m8x3, T m8x4, T m8x5, T m8x6, T m8x7, T m8x8, T m8x9,
        T m9x1, T m9x2, T m9x3, T m9x4, T m9x5, T m9x6, T m9x7, T m9x8, T m9x9
        ) IdentityMatrix9x9 =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>
    /// The identity matrix 10x10.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7, T m1x8, T m1x9, T m1x10,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7, T m2x8, T m2x9, T m2x10,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7, T m3x8, T m3x9, T m3x10,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7, T m4x8, T m4x9, T m4x10,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7, T m5x8, T m5x9, T m5x10,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7, T m6x8, T m6x9, T m6x10,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7, T m7x8, T m7x9, T m7x10,
        T m8x1, T m8x2, T m8x3, T m8x4, T m8x5, T m8x6, T m8x7, T m8x8, T m8x9, T m8x10,
        T m9x1, T m9x2, T m9x3, T m9x4, T m9x5, T m9x6, T m9x7, T m9x8, T m9x9, T m9x10,
        T m10x1, T m10x2, T m10x3, T m10x4, T m10x5, T m10x6, T m10x7, T m10x8, T m10x9, T m10x10
        ) IdentityMatrix10x10 =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>
    /// The identity matrix 11x11.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7, T m1x8, T m1x9, T m1x10, T m1x11,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7, T m2x8, T m2x9, T m2x10, T m2x11,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7, T m3x8, T m3x9, T m3x10, T m3x11,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7, T m4x8, T m4x9, T m4x10, T m4x11,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7, T m5x8, T m5x9, T m5x10, T m5x11,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7, T m6x8, T m6x9, T m6x10, T m6x11,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7, T m7x8, T m7x9, T m7x10, T m7x11,
        T m8x1, T m8x2, T m8x3, T m8x4, T m8x5, T m8x6, T m8x7, T m8x8, T m8x9, T m8x10, T m8x11,
        T m9x1, T m9x2, T m9x3, T m9x4, T m9x5, T m9x6, T m9x7, T m9x8, T m9x9, T m9x10, T m9x11,
        T m10x1, T m10x2, T m10x3, T m10x4, T m10x5, T m10x6, T m10x7, T m10x8, T m10x9, T m10x10, T m10x11,
        T m11x1, T m11x2, T m11x3, T m11x4, T m11x5, T m11x6, T m11x7, T m11x8, T m11x9, T m11x10, T m11x11
        ) IdentityMatrix11x11 =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One, T.Zero,
         T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.One);

    /// <summary>
    /// The cubic Hermite Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4
        ) CubicHermiteBernsteinBasisMatrix =
        (T.One, T.Zero, T.Zero, T.Zero,
        T.Zero, T.One, T.Zero, T.Zero,
        -Integers<T>.Three, -Integers<T>.Two, Integers<T>.Three, T.NegativeOne,
        Integers<T>.Two, T.One, Integers<T>.NegativeTwo, T.One);

    /// <summary>
    /// The linear Bezier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2,
        T m2x1, T m2x2
        ) LinearBezierBernsteinBasisMatrix =
        (T.One, T.Zero,
        T.NegativeOne, T.One);

    /// <summary>
    /// The inverse linear Bezier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2,
        T m2x1, T m2x2
        ) InverseLinearBezierBernsteinBasisMatrix =
        (T.One, T.Zero,
        T.One, T.One);

    /// <summary>
    /// The quadratic Bezier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3
        ) QuadraticBezierBernsteinBasisMatrix =
        (T.One, T.Zero, T.Zero,
        Integers<T>.NegativeTwo, Integers<T>.Two, T.Zero,
        T.One, -Integers<T>.Two, T.One);

    /// <summary>
    /// The inverse quadratic Bezier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3
        ) InverseQuadraticBezierBernsteinBasisMatrix =
        (T.One, T.Zero, T.Zero,
        T.One, OneHalf, T.Zero,
        T.One, T.One, T.One);

    /// <summary>
    /// The cubic Bezier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4
        ) CubicBezierBernsteinBasisMatrix =
        (T.One, T.Zero, T.Zero, T.Zero,
        -Integers<T>.Three, Integers<T>.Three, T.Zero, T.Zero,
        Integers<T>.Three, -Integers<T>.Six, Integers<T>.Three, T.Zero,
        T.NegativeOne, Integers<T>.Three, -Integers<T>.Three, T.One);

    /// <summary>
    /// The inverse cubic Bezier Bernstein basis matrix.
    /// </summary>
    public static (
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4
        ) InverseCubicBezierBernsteinBasisMatrix =
        (T.One, T.Zero, T.Zero, T.Zero,
        T.One, OneThird, T.Zero, T.Zero,
        T.One, TwoThirds, OneThird, T.Zero,
        T.One, T.One, T.One, T.One);

    /// <summary>
    /// The quartic Bezier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5
        ) QuarticBezierBernsteinBasisMatrix =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero,
        -Integers<T>.Four, Integers<T>.Four, T.Zero, T.Zero, T.Zero,
        Integers<T>.Six, -Integers<T>.Twelve, Integers<T>.Six, T.Zero, T.Zero,
        -Integers<T>.Four, Integers<T>.Twelve, -Integers<T>.Twelve, Integers<T>.Four, T.Zero,
        T.One, -Integers<T>.Four, Integers<T>.Six, -Integers<T>.Four, T.One);

    /// <summary>
    /// The inverse quartic Bezier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5
        ) InverseQuarticBezierBernsteinBasisMatrix =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero,
        T.One, OneQuarter, T.Zero, T.Zero, T.Zero,
        T.One, ThreeQuarters, TwoThirds, T.Zero, T.Zero,
        T.One, ThreeQuarters, ThreeQuarters, OneQuarter, T.Zero,
        T.One, T.One, T.One, T.One, T.One);

    /// <summary>
    /// The quintic Bezier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6
        ) QuinticBezierBernsteinBasisMatrix =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        -Integers<T>.Five, Integers<T>.Five, T.Zero, T.Zero, T.Zero, T.Zero,
        Integers<T>.Ten, -Integers<T>.Twenty, Integers<T>.Ten, T.Zero, T.Zero, T.Zero,
        -Integers<T>.Ten, T.CreateChecked(30), -T.CreateChecked(30), Integers<T>.Ten, T.Zero, T.Zero,
        Integers<T>.Five, -Integers<T>.Twenty, T.CreateChecked(30), -Integers<T>.Twenty, Integers<T>.Five, T.Zero,
        T.NegativeOne, Integers<T>.Five, -Integers<T>.Ten, Integers<T>.Ten, -Integers<T>.Five, T.One);

    /// <summary>
    /// The sextic Bezier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7
        ) SexticBezierBernsteinBasisMatrix =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        -Integers<T>.Six, Integers<T>.Six, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        T.CreateChecked(15), -T.CreateChecked(30), T.CreateChecked(15), T.Zero, T.Zero, T.Zero, T.Zero,
        -Integers<T>.Twenty, T.CreateChecked(60), -T.CreateChecked(60), T.CreateChecked(20), T.Zero, T.Zero, T.Zero,
        T.CreateChecked(15), -T.CreateChecked(60), T.CreateChecked(90), -T.CreateChecked(60), T.CreateChecked(15), T.Zero, T.Zero,
        -Integers<T>.Six, T.CreateChecked(30), -T.CreateChecked(60), T.CreateChecked(60), -T.CreateChecked(30), Integers<T>.Six, T.Zero,
        T.One, -Integers<T>.Six, T.CreateChecked(15), -Integers<T>.Twenty, T.CreateChecked(15), -Integers<T>.Six, T.One);

    /// <summary>
    /// The septic Bezier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7, T m1x8,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7, T m2x8,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7, T m3x8,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7, T m4x8,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7, T m5x8,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7, T m6x8,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7, T m7x8,
        T m8x1, T m8x2, T m8x3, T m8x4, T m8x5, T m8x6, T m8x7, T m8x8
        ) SepticBezierBernsteinBasisMatrix =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        -Integers<T>.Seven, Integers<T>.Seven, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        T.CreateChecked(21), -T.CreateChecked(42), T.CreateChecked(21), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        -T.CreateChecked(35), T.CreateChecked(105), -T.CreateChecked(105), T.CreateChecked(35), T.Zero, T.Zero, T.Zero, T.Zero,
        T.CreateChecked(35), -T.CreateChecked(140), T.CreateChecked(210), -T.CreateChecked(140), T.CreateChecked(35), T.Zero, T.Zero, T.Zero,
        -T.CreateChecked(21), T.CreateChecked(105), -T.CreateChecked(210), T.CreateChecked(210), -T.CreateChecked(105), T.CreateChecked(21), T.Zero, T.Zero,
        Integers<T>.Seven, -T.CreateChecked(42), T.CreateChecked(105), -T.CreateChecked(140), T.CreateChecked(105), -T.CreateChecked(42), Integers<T>.Seven, T.Zero,
        T.NegativeOne, Integers<T>.Seven, -T.CreateChecked(21), T.CreateChecked(35), -T.CreateChecked(35), T.CreateChecked(21), -Integers<T>.Seven, T.One);

    /// <summary>
    /// The octic Bezier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7, T m1x8, T m1x9,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7, T m2x8, T m2x9,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7, T m3x8, T m3x9,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7, T m4x8, T m4x9,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7, T m5x8, T m5x9,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7, T m6x8, T m6x9,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7, T m7x8, T m7x9,
        T m8x1, T m8x2, T m8x3, T m8x4, T m8x5, T m8x6, T m8x7, T m8x8, T m8x9,
        T m9x1, T m9x2, T m9x3, T m9x4, T m9x5, T m9x6, T m9x7, T m9x8, T m9x9
        ) OcticBezierBernsteinBasisMatrix =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        -Integers<T>.Eight, Integers<T>.Eight, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        T.CreateChecked(28), -T.CreateChecked(56), T.CreateChecked(28), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        -T.CreateChecked(56), T.CreateChecked(168), -T.CreateChecked(168), T.CreateChecked(56), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        T.CreateChecked(70), -T.CreateChecked(280), T.CreateChecked(420), -T.CreateChecked(280), T.CreateChecked(70), T.Zero, T.Zero, T.Zero, T.Zero,
        -T.CreateChecked(56), T.CreateChecked(280), -T.CreateChecked(560), T.CreateChecked(560), -T.CreateChecked(280), T.CreateChecked(56), T.Zero, T.Zero, T.Zero,
        T.CreateChecked(28), -T.CreateChecked(168), T.CreateChecked(420), -T.CreateChecked(560), T.CreateChecked(420), -T.CreateChecked(168), T.CreateChecked(28), T.Zero, T.Zero,
        -Integers<T>.Eight, T.CreateChecked(56), -T.CreateChecked(168), T.CreateChecked(280), -T.CreateChecked(280), T.CreateChecked(168), -T.CreateChecked(56), Integers<T>.Eight, T.Zero,
        T.One, -Integers<T>.Eight, T.CreateChecked(28), -T.CreateChecked(56), T.CreateChecked(70), -T.CreateChecked(56), T.CreateChecked(28), -Integers<T>.Eight, T.One);

    /// <summary>
    /// The nonic Bezier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7, T m1x8, T m1x9, T m1x10,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7, T m2x8, T m2x9, T m2x10,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7, T m3x8, T m3x9, T m3x10,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7, T m4x8, T m4x9, T m4x10,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7, T m5x8, T m5x9, T m5x10,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7, T m6x8, T m6x9, T m6x10,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7, T m7x8, T m7x9, T m7x10,
        T m8x1, T m8x2, T m8x3, T m8x4, T m8x5, T m8x6, T m8x7, T m8x8, T m8x9, T m8x10,
        T m9x1, T m9x2, T m9x3, T m9x4, T m9x5, T m9x6, T m9x7, T m9x8, T m9x9, T m9x10,
        T m10x1, T m10x2, T m10x3, T m10x4, T m10x5, T m10x6, T m10x7, T m10x8, T m10x9, T m10x10
        ) NonicBezierBernsteinBasisMatrix =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        -Integers<T>.Nine, Integers<T>.Nine, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        T.CreateChecked(36), -T.CreateChecked(72), T.CreateChecked(36), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        -T.CreateChecked(84), T.CreateChecked(252), -T.CreateChecked(252), T.CreateChecked(84), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        T.CreateChecked(126), -T.CreateChecked(504), T.CreateChecked(756), -T.CreateChecked(504), T.CreateChecked(126), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        -T.CreateChecked(126), T.CreateChecked(630), -T.CreateChecked(1260), T.CreateChecked(1260), -T.CreateChecked(630), T.CreateChecked(126), T.Zero, T.Zero, T.Zero, T.Zero,
        T.CreateChecked(84), -T.CreateChecked(504), T.CreateChecked(1260), -T.CreateChecked(1680), T.CreateChecked(1260), -T.CreateChecked(504), T.CreateChecked(84), T.Zero, T.Zero, T.Zero,
        -T.CreateChecked(36), T.CreateChecked(252), -T.CreateChecked(756), T.CreateChecked(1260), -T.CreateChecked(1260), T.CreateChecked(756), -T.CreateChecked(252), T.CreateChecked(72), T.Zero, T.Zero,
        Integers<T>.Nine, -T.CreateChecked(72), T.CreateChecked(252), -T.CreateChecked(504), T.CreateChecked(630), -T.CreateChecked(504), T.CreateChecked(252), -T.CreateChecked(72), Integers<T>.Nine, T.Zero,
        T.NegativeOne, Integers<T>.Nine, -T.CreateChecked(36), T.CreateChecked(84), -T.CreateChecked(126), T.CreateChecked(12), -T.CreateChecked(84), T.CreateChecked(36), Integers<T>.Nine, T.One);

    /// <summary>
    /// The decic Bezier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7, T m1x8, T m1x9, T m1x10, T m1x11,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7, T m2x8, T m2x9, T m2x10, T m2x11,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7, T m3x8, T m3x9, T m3x10, T m3x11,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7, T m4x8, T m4x9, T m4x10, T m4x11,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7, T m5x8, T m5x9, T m5x10, T m5x11,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7, T m6x8, T m6x9, T m6x10, T m6x11,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7, T m7x8, T m7x9, T m7x10, T m7x11,
        T m8x1, T m8x2, T m8x3, T m8x4, T m8x5, T m8x6, T m8x7, T m8x8, T m8x9, T m8x10, T m8x11,
        T m9x1, T m9x2, T m9x3, T m9x4, T m9x5, T m9x6, T m9x7, T m9x8, T m9x9, T m9x10, T m9x11,
        T m10x1, T m10x2, T m10x3, T m10x4, T m10x5, T m10x6, T m10x7, T m10x8, T m10x9, T m10x10, T m10x11,
        T m11x1, T m11x2, T m11x3, T m11x4, T m11x5, T m11x6, T m11x7, T m11x8, T m11x9, T m11x10, T m11x11
        ) DecicBezierBernsteinBasisMatrix =
        (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        -Integers<T>.Ten, Integers<T>.Ten, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        T.CreateChecked(45), -T.CreateChecked(90), T.CreateChecked(45), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        -T.CreateChecked(120), T.CreateChecked(360), -T.CreateChecked(360), T.CreateChecked(120), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        T.CreateChecked(210), -T.CreateChecked(840), T.CreateChecked(1260), -T.CreateChecked(840), T.CreateChecked(210), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        -T.CreateChecked(252), T.CreateChecked(1260), -T.CreateChecked(2520), T.CreateChecked(2520), -T.CreateChecked(1260), T.CreateChecked(252), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
        T.CreateChecked(210), -T.CreateChecked(1260), T.CreateChecked(3150), -T.CreateChecked(4200), T.CreateChecked(3150), -T.CreateChecked(1260), T.CreateChecked(210), T.Zero, T.Zero, T.Zero, T.Zero,
        -T.CreateChecked(120), T.CreateChecked(840), -T.CreateChecked(2520), T.CreateChecked(4200), -T.CreateChecked(4200), T.CreateChecked(2520), -T.CreateChecked(840), T.CreateChecked(120), T.Zero, T.Zero, T.Zero,
        T.CreateChecked(45), -T.CreateChecked(360), T.CreateChecked(1260), -T.CreateChecked(2520), T.CreateChecked(3150), -T.CreateChecked(2520), T.CreateChecked(1260), -T.CreateChecked(360), T.CreateChecked(45), T.Zero, T.Zero,
        -Integers<T>.Ten, T.CreateChecked(90), -T.CreateChecked(360), T.CreateChecked(840), -T.CreateChecked(1260), T.CreateChecked(1260), -T.CreateChecked(840), T.CreateChecked(360), -T.CreateChecked(90), Integers<T>.Ten, T.Zero,
        T.One, -Integers<T>.Ten, T.CreateChecked(45), T.CreateChecked(-120), T.CreateChecked(210), T.CreateChecked(-252), T.CreateChecked(210), T.CreateChecked(-120), T.CreateChecked(45), -Integers<T>.Ten, T.One);
    #endregion Matrices

    #region Gauss Tables
    /// <summary>
    /// Gauss abscissa table
    /// </summary>
    /// <acknowledgment>
    /// https://code.google.com/archive/p/degrafa/source/default/source
    /// </acknowledgment>
    public static readonly T[] abscissa = new T[]
    {
        // N=2
        T.CreateChecked(-0.5773502692),
        T.CreateChecked( 0.5773502692),
        // N=3
        T.CreateChecked(-0.7745966692),
        T.CreateChecked( 0.7745966692),
        T.CreateChecked( 0.0000000000),
        // N=4
        T.CreateChecked(-0.8611363116),
        T.CreateChecked( 0.8611363116),
        T.CreateChecked(-0.3399810436),
        T.CreateChecked( 0.3399810436),
        // N=5
        T.CreateChecked(-0.9061798459),
        T.CreateChecked( 0.9061798459),
        T.CreateChecked(-0.5384693101),
        T.CreateChecked( 0.5384693101),
        T.CreateChecked( 0.0000000000),
        // N=6
        T.CreateChecked(-0.9324695142),
        T.CreateChecked( 0.9324695142),
        T.CreateChecked(-0.6612093865),
        T.CreateChecked( 0.6612093865),
        T.CreateChecked(-0.2386191861),
        T.CreateChecked( 0.2386191861),
        // N=7
        T.CreateChecked(-0.9491079123),
        T.CreateChecked( 0.9491079123),
        T.CreateChecked(-0.7415311856),
        T.CreateChecked( 0.7415311856),
        T.CreateChecked(-0.4058451514),
        T.CreateChecked( 0.4058451514),
        T.CreateChecked( 0.0000000000),
        // N=8
        T.CreateChecked(-0.9602898565),
        T.CreateChecked( 0.9602898565),
        T.CreateChecked(-0.7966664774),
        T.CreateChecked( 0.7966664774),
        T.CreateChecked(-0.5255324099),
        T.CreateChecked( 0.5255324099),
        T.CreateChecked(-0.1834346425),
        T.CreateChecked( 0.1834346425)
    };

    /// <summary>
    /// Gauss weight table
    /// </summary>
    /// <acknowledgment>
    /// https://code.google.com/archive/p/degrafa/source/default/source
    /// </acknowledgment>
    public static readonly T[] weight = new T[]
    {
        // N=2
        T.CreateChecked(1.0000000000),
        T.CreateChecked(1.0000000000),
        // N=3      
        T.CreateChecked(0.5555555556),
        T.CreateChecked(0.5555555556),
        T.CreateChecked(0.8888888888),
        // N=4      
        T.CreateChecked(0.3478548451),
        T.CreateChecked(0.3478548451),
        T.CreateChecked(0.6521451549),
        T.CreateChecked(0.6521451549),
        // N=5      
        T.CreateChecked(0.2369268851),
        T.CreateChecked(0.2369268851),
        T.CreateChecked(0.4786286705),
        T.CreateChecked(0.4786286705),
        T.CreateChecked(0.5688888888),
        // N=6      
        T.CreateChecked(0.1713244924),
        T.CreateChecked(0.1713244924),
        T.CreateChecked(0.3607615730),
        T.CreateChecked(0.3607615730),
        T.CreateChecked(0.4679139346),
        T.CreateChecked(0.4679139346),
        // N=7      
        T.CreateChecked(0.1294849662),
        T.CreateChecked(0.1294849662),
        T.CreateChecked(0.2797053915),
        T.CreateChecked(0.2797053915),
        T.CreateChecked(0.3818300505),
        T.CreateChecked(0.3818300505),
        T.CreateChecked(0.4179591837),
        // N=8      
        T.CreateChecked(0.1012285363),
        T.CreateChecked(0.1012285363),
        T.CreateChecked(0.2223810345),
        T.CreateChecked(0.2223810345),
        T.CreateChecked(0.3137066459),
        T.CreateChecked(0.3137066459),
        T.CreateChecked(0.3626837834),
        T.CreateChecked(0.3626837834)
    };
    #endregion Gauss Tables
}
