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
using System.Globalization;
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
        /// Number close to zero, where float.MinValue is -float.MaxValue
        /// </summary>
        public const float FloatMin = 1.175494351e-38f;

        /// <summary>
        /// SlopeMax is a large value "close to infinity" (Close to the largest value allowed for the data 
        /// type). Used in the Slope of a LineSeg
        /// </summary>
        /// <remarks></remarks>
        public const double SlopeMax = 9223372036854775807d;

        /// <summary>
        /// One Tau or double Pi.
        /// </summary>
        public const double Tau = 2d * Math.PI;

        /// <summary>
        /// Math.PI * 2
        /// </summary>
        /// <remarks></remarks>
        public const double DoublePi = Tau;

        /// <summary>
        /// One half Tau or One Pi.
        /// </summary>
        public const double Pi = Math.PI;

        /// <summary>
        /// One half Tau or One Pi.
        /// </summary>
        public const double HalfTau = Math.PI;

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
        /// Represents the golden ratio.
        /// </summary>
        public static readonly double Golden = Phi;

        /// <summary>
        /// Represents the golden ratio by formula.
        /// </summary>
        public static readonly double GoldenRatio = Phi;

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
        public const double E = Math.E;

        /// <summary>
        /// Represents the square root of 2.
        /// </summary>
        public static readonly double Sqrt2 = Math.Sqrt(2);

        /// <summary>
        /// Represents the square root of 3.
        /// </summary>
        public static readonly double SQRT3 = Math.Sqrt(3);

        /// <summary>
        /// Represents the golden ratio.
        /// </summary>
        public static readonly double Phi = (1d + Math.Sqrt(5)) / 2d; //1.61803398874989484820458683436;

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
        private static Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        #region Geometric Methods

        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="aX">Horizontal Component of Point Starting Point</param>
        /// <param name="aY">Vertical Component of Point Starting Point</param>
        /// <param name="bX">Horizontal Component of Ending Point</param>
        /// <param name="bY">Vertical Component of Ending Point</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(double aX, double aY, double bX, double bY)
        {
            return Math.Atan2((aY - bY), (aX - bX));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <returns>
        /// Return the angle ABC.
        /// Return a value between PI and -PI.
        /// Note that the value is the opposite of what you might
        /// expect because Y coordinates increase downward.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleVector(double aX, double aY, double bX, double bY, double cX, double cY)
        {
            // Calculate the angle.
            return Math.Atan2(CrossProductVector(aX, aY, bX, bY, cX, cY), DotProductVector(aX, aY, bX, bY, cX, cY));
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
        public static double AbsoluteAngle(double aX, double aY, double bX, double bY)
        {
            // Find the angle of point a and point b. 
            double test = -Maths.Angle(aX, aY, bX, bY) % Math.PI;
            return test < 0 ? test += Math.PI : test;
        }

        /// <summary>
        /// Cross Product of two points.
        /// </summary>
        /// <param name="aX">First Point X component.</param>
        /// <param name="aY">First Point Y component.</param>
        /// <param name="bX">Second Point X component.</param>
        /// <param name="bY">Second Point Y component.</param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks>Note that AB · BC = |AB| * |BC| * Cos(theta).</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct(double aX, double aY, double bX, double bY)
        {
            return (aX * bY) - (aY * bX);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProductVector(double aX, double aY, double bX, double bY, double cX, double cY)
        {
            // Calculate the Z coordinate of the cross product.
            return ((aX - bX) * (cY - bY) - (aY - bY) * (cX - bX));
        }

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="aX">First Point X component.</param>
        /// <param name="aY">First Point Y component.</param>
        /// <param name="bX">Second Point X component.</param>
        /// <param name="bY">Second Point Y component.</param>
        /// <returns>The Dot Product.</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(double aX, double aY, double bX, double bY)
        {
            return ((aX * bX) + (aY * bY));
        }

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="aX">First Point X component.</param>
        /// <param name="aY">First Point Y component.</param>
        /// <param name="aZ">First Point Z component.</param>
        /// <param name="bX">Second Point X component.</param>
        /// <param name="bY">Second Point Y component.</param>
        /// <param name="bZ">Second Point Z component.</param>
        /// <returns>The Dot Product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(double aX, double aY, double aZ, double bX, double bY, double bZ)
        {
            return ((aX * bX) + (aY * bY) + (aZ * bZ));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <returns>
        /// Return the dot product AB · BC.
        /// </returns>
        /// <remarks>
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </remarks>
        public static double DotProductVector(double aX, double aY, double bX, double bY, double cX, double cY)
        {
            // Get the vectors' coordinates.
            double BAx = aX - bX;
            double BAy = aY - bY;
            double BCx = cX - bX;
            double BCy = cY - bY;

            // Calculate the dot product.
            return (BAx * BCx + BAy * BCy);
        }

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="aX">First X component.</param>
        /// <param name="aY">First Y component.</param>
        /// <param name="bX">Second X component.</param>
        /// <param name="bY">Second Y component.</param>
        /// <returns>The distance between two points.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(double aX, double aY, double bX, double bY)
        {
            return Math.Sqrt((bX - aX) * (bX - aX) + (bY - aY) * (bY - aY));
        }

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="aX">First X component.</param>
        /// <param name="aY">First Y component.</param>
        /// <param name="aZ">First Z component.</param>
        /// <param name="bX">Second X component.</param>
        /// <param name="bY">Second Y component.</param>
        /// <param name="bZ">Second Z component.</param>
        /// <returns>The distance between two points.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(double aX, double aY, double aZ, double bX, double bY, double bZ)
        {
            return Math.Sqrt((bX - aX) * (bX - aX) + (bY - aY) * (bY - aY) + (bZ - aZ) * (bZ - aZ));
        }

        /// <summary>
        /// Calculates the Slope of a vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Returns the slope angle of a vector.</returns>
        /// <remarks>The slope is calculated with Slope = Y / X or rise over run</remarks>
        public static double Slope(double i, double j)
        {
            //  If the line is vertical, return something close to infinity 
            //  (Close to the largest value allowed for the data type).
            if ((i == 0)) return SlopeMax;
            //  Otherwise calculate and return the slope.
            return (j / i);
        }

        /// <summary>
        /// Returns the slope angle of a line.
        /// </summary>
        /// <param name="X1">Horizontal Component of Point Starting Point</param>
        /// <param name="Y1">Vertical Component of Point Starting Point</param>
        /// <param name="X2">Horizontal Component of Ending Point</param>
        /// <param name="Y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks></remarks>
        public static double Slope(double X1, double Y1, double X2, double Y2)
        {
            //  Vertical line check.
            //  Check to see if the Line is Vertical. 
            //  Line is Vertical return something close to infinity (Close to 
            //  the largest value allowed for the data type).
            //  The original Version was: If (Line.A.X - Line.B.X) = 0 Then
            if ((X1 == X2)) return SlopeMax;
            //  Otherwise calculate and return the slope.
            return ((Y2 - Y1) / (X2 - X1));
        }

        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulus(double i, double j)
        {
            return Math.Sqrt((i * i) + (j * j));
        }

        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulus(double i, double j, double k)
        {
            return Math.Sqrt((i * i) + (j * j) + (k * k));
        }

        #endregion

        #region Array Math

        /// <summary>
        /// Returns the average value of a numeric array.
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        /// <remarks>Note: Uses Following Sum Function as well.</remarks>
        public static double Average(this double[] Values)
        {
            return (Sum(Values) / Values.Length);
        }

        /// <summary>
        /// Find the sum of an array of Numbers
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Sum(this double[] Values)
        {
            double Retval = 0;
            for (int i = 0; i < Values.Length; i++)
            {
                Retval += Values[i];
            }

            return Retval;
        }

        #endregion

        #region Derived Equivalent Math Functions
        // Derived equivalent Math Functions The following is a list of non-intrinsic math functions that can be derived from the intrinsic math functions:

        /// <summary>
        /// Cube root equivalent of the sqrt function. (note that there are actually
        /// three roots: one real, two complex, and we don't care about the latter):
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/26823024/cubic-bezier-reverse-getpoint-equation-float-for-vector-vector-for-float?answertab=active#tab-top</remarks>
        public static double Crt(double value)
        {
            return value < 0 ? -Math.Pow(-value, 1 / 3) : Math.Pow(value, 1 / 3);
        }

        /// <summary>
        /// Angle with tangent opp/hyp
        /// </summary>
        /// <param name="opposite"></param>
        /// <param name="adjacent"></param>
        /// <returns>Return the angle with tangent opp/hyp. The returned value is between PI and -PI.</returns>
        /// <remarks></remarks>
        public static double Atan2(double opposite, double adjacent)
        {
            //double angle;
            ////  Get the basic angle.
            //angle = (Math.PI / 2);
            //angle = (Math.Abs(Math.Atan((Opposite / Adjacent))));
            //return angle;
            return Math.Atan2(opposite, adjacent);
        }

        /// <summary>
        /// Returns the Angle of two deltas.
        /// </summary>
        /// <param name="DeltaA">Delta Angle 1</param>
        /// <param name="DeltaB">Delta Angle 2</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        public static double _Atan2(double DeltaA, double DeltaB)
        {
            if (((DeltaA == 0) && (DeltaB == 0))) return 0;
            double Value = Math.Asin(DeltaA / Math.Sqrt(DeltaA * DeltaA + DeltaB * DeltaB));
            if ((DeltaB < 0)) Value = (Math.PI - Value);
            if ((Value < 0)) Value = (Value + (2 * Math.PI));
            return Value;
        }

        /// <summary>
        /// Derived math functions equivalent Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Secant(double value)
        {
            if (((value % Pi != HalfPi) && (value % Pi != -HalfPi)))
            {
                return (1 / Math.Cos(value));
            }

            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent  Co-secant
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Cosecant(double Value)
        {
            if (((Value % Pi != 0) && (Value % Pi != Pi)))
            {
                return (1 / Math.Sin(Value));
            }

            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent Cotangent
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Cotangent(double Value)
        {
            if (((Value % Pi != 0) && (Value % Pi != Pi)))
            {
                return (1 / Math.Tan(Value));
            }

            return 0;
        }

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
        /// <remarks></remarks>
        public static double InverseCotangent(double value)
        {
            //  Arc-co-tan(X) 
            return (Math.Atan(value) + (2 * Math.Atan(1)));
        }

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Sine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double HyperbolicSine(double value)
        {
            //  HSin(X) 
            return ((Math.Exp(value) - Math.Exp((value * -1))) * 0.5d);
        }

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Cosine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double HyperbolicCosine(double value)
        {
            //  HCos(X) 
            return ((Math.Exp(value) + Math.Exp((value * -1))) * 0.5d);
        }

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Tangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double HyperbolicTangent(double value)
        {
            //  HTan(X) 
            return ((Math.Exp(value) - Math.Exp((value * -1))) / (Math.Exp(value) + Math.Exp((value * -1))));
        }

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double HyperbolicSecant(double value)
        {
            //  HSec(X) 
            return (0.5d * (Math.Exp(value) + Math.Exp((value * -1))));
        }

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Co-secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double HyperbolicCosecant(double value)
        {
            //  HCosec(X)
            return (0.5d * (Math.Exp(value) - Math.Exp((value * -1))));
        }

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Cotangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double HyperbolicCotangent(double value)
        {
            //  HCotan(X) 
            return ((Math.Exp(value) + Math.Exp((value * -1))) / (Math.Exp(value) - Math.Exp((value * -1))));
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Sine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double InverseHyperbolicSine(double value)
        {
            //  HArcsin(X) 
            return Math.Log((value + Math.Sqrt(((value * value) + 1))));
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Cosine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double InverseHyperbolicCosine(double value)
        {
            //  HArccos(X) 
            return Math.Log((value + Math.Sqrt(((value * value) - 1))));
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Tangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double InverseHyperbolicTangent(double value)
        {
            //  HArctan(X) 
            return (Math.Log(((1 + value) / (1 - value))) * 0.5d);
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double InverseHyperbolicSecant(double value)
        {
            //  HArcsec(X) 
            return Math.Log(((Math.Sqrt((((value * value) * -1) + 1)) + 1) / value));
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Co-secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double InverseHyperbolicCosecant(double value)
        {
            //  HArccosec(X) 
            return Math.Log((((Math.Sign(value) * Math.Sqrt(((value * value) + 1))) + 1) / value));
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Cotangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double InverseHyperbolicCotangent(double value)
        {
            //  HArccotan(X)
            return (Math.Log(((value + 1) / (value - 1))) * 0.5d);
        }

        /// <summary>
        /// Derived math functions equivalent Base N Logarithm
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numberBase"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double LogarithmTobaseN(double value, double numberBase)
        {
            //  LogN(X) 
            // Return Log(Value) / Log(NumberBase)
            if ((numberBase != 1))
            {
                return (Math.Log(value) / Math.Log(numberBase));
            }

            return 0;
        }

        #endregion

        #region Conversion Extensions

        /// <summary>
        /// Imitation of Excel's Mod Operator
        /// </summary>
        /// <param name="valueA">Source parameter</param>
        /// <param name="valueB">Destination parameter</param>
        /// <returns>Returns the same Modulus Result that Excel returns.</returns>
        /// <remarks>Created after finding out Excel returns a different value for the Mod Operator than .Net</remarks>
        public static double Modulo(this double valueA, double valueB)
        {
            double temp = valueA;
            return ((temp %= valueB) < 0) ? temp + valueB : temp;
        }

        /// <summary>
        /// Convert Degrees to Radians.
        /// </summary>
        /// <param name="degrees">Angle in Degrees.</param>
        /// <returns>Angle in Radians.</returns>
        /// <remarks></remarks>
        /// <optimisation>This code has been optimized for speed by removing division from each call</optimisation>
        public static double ToRadians(this double degrees)
        {
            return degrees * Radien;
        }

        /// <summary>
        /// Convert Radians to Degrees.
        /// </summary>
        /// <param name="radiens">Angle in Radians.</param>
        /// <returns>Angle in Degrees.</returns>
        /// <remarks></remarks>
        /// <optimisation>This code has been optimized for speed by removing division from each call</optimisation>
        public static double ToDegrees(this double radiens)
        {
            return radiens * Degree;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int RoundToInt(this float val)
        {
            return (0 < val) ? (int)(val + 0.5) : (int)(val - 0.5);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int RoundToInt(this double val)
        {
            return (0 < val) ? (int)(val + 0.5) : (int)(val - 0.5);
        }

        /// <summary>
        /// Round a value to the nearest multiple of a number.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="multiple">The multiple to round to.</param>
        /// <returns>Returns a value rounded to an interval of the multiple.</returns>
        /// <remarks></remarks>
        public static double RoundToMultiple(this double value, double multiple)
        {
            // Convert.ToInt32 does the correct rounding that Math.Round does not do.
            return Convert.ToInt32(value / multiple) * multiple;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static float ToFloat(string text)
        {
            return float.Parse(text, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static float ToFloat(string text, IFormatProvider provider)
        {
            return float.Parse(text, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static double ToDouble(string text)
        {
            return double.Parse(text, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static double ToDouble(string text, IFormatProvider provider)
        {
            return double.Parse(text, provider);
        }

        #endregion

        #region Comparisons

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
        public static bool AreClose(this float value1, float value2)
        {
            //in case they are Infinities (then epsilon check does not work)
            if (value1 == value2) return true;
            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            float eps = (float)((Math.Abs(value1) + Math.Abs(value2) + 10.0) * FloatEpsilon);
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
        public static bool AreClose(this double value1, double value2)
        {
            //in case they are Infinities (then epsilon check does not work)
            if (value1 == value2) return true;
            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            double eps = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * Maths.DoubleEpsilon;
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
        public static bool LessThan(this float value1, float value2)
        {
            return (value1 < value2) && !AreClose(value1, value2);
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
        public static bool LessThan(this double value1, double value2)
        {
            return (value1 < value2) && !AreClose(value1, value2);
        }

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
        public static bool GreaterThan(this float value1, float value2)
        {
            return (value1 > value2) && !AreClose(value1, value2);
        }

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
        public static bool GreaterThan(this double value1, double value2)
        {
            return (value1 > value2) && !AreClose(value1, value2);
        }

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
        public static bool LessThanOrClose(this float value1, float value2)
        {
            return (value1 < value2) || AreClose(value1, value2);
        }

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
        public static bool LessThanOrClose(this double value1, double value2)
        {
            return (value1 < value2) || AreClose(value1, value2);
        }

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
        public static bool GreaterThanOrClose(this float value1, float value2)
        {
            return (value1 > value2) || AreClose(value1, value2);
        }

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
        public static bool GreaterThanOrClose(this double value1, double value2)
        {
            return (value1 > value2) || AreClose(value1, value2);
        }

        /// <summary>
        /// IsZero - Returns whether or not the double is "close" to 0.  Same as AreClose(double, 0),
        /// but this is faster.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value"> The double to compare to 0. </param>
        public static bool IsZero(this float value)
        {
            return Math.Abs(value) < 10.0 * FloatEpsilon;
        }

        /// <summary>
        /// IsZero - Returns whether or not the double is "close" to 0.  Same as AreClose(double, 0),
        /// but this is faster.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value"> The double to compare to 0. </param>
        public static bool IsZero(this double value)
        {
            return Math.Abs(value) < 10.0 * DoubleEpsilon;
        }

        /// <summary>
        /// IsOne - Returns whether or not the double is "close" to 1.  Same as AreClose(double, 1),
        /// but this is faster.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value"> The double to compare to 1. </param>
        public static bool IsOne(this float value)
        {
            return Math.Abs(value - 1.0) < 10.0 * FloatEpsilon;
        }

        /// <summary>
        /// IsOne - Returns whether or not the double is "close" to 1.  Same as AreClose(double, 1),
        /// but this is faster.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value"> The double to compare to 1. </param>
        public static bool IsOne(this double value)
        {
            return Math.Abs(value - 1.0) < 10.0 * DoubleEpsilon;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsBetweenZeroAndOne(this float val)
        {
            return (GreaterThanOrClose(val, 0) && LessThanOrClose(val, 1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsBetweenZeroAndOne(this double val)
        {
            return (GreaterThanOrClose(val, 0) && LessThanOrClose(val, 1));
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Lower"></param>
        /// <param name="Upper"></param>
        /// <returns></returns>
        public static double Random(this double Lower, double Upper)
        {
            return ((rnd.Next() * ((Upper - Lower) + 1)) + Lower);
        }
    }
}
