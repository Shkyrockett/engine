// <copyright file="Maths.Trignometry.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine
{
    /// <summary>
    ///
    /// </summary>
    public partial class Maths
    {
        /// <summary>
        /// Convert Degrees to Radians.
        /// </summary>
        /// <param name="degrees">Angle in Degrees.</param>
        /// <returns>Angle in Radians.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToRadians(this double degrees)
            => degrees * Radian;

        /// <summary>
        /// Convert Radians to Degrees.
        /// </summary>
        /// <param name="radiens">Angle in Radians.</param>
        /// <returns>Angle in Degrees.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToDegrees(this double radiens)
            => radiens * Degree;

        /// <summary>
        /// Find the absolute positive value of a radian angle.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns>The absolute positive angle in radians.</returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteAngle(this double angle)
        {
            if (double.IsNaN(angle))
                return angle;
            // ToDo: Need to do some testing to figure out which method is more appropriate.
            //double value = angle % Tau;
            //double value = IEEERemainder(angle, Tau);
            // The active ingredient of the IEEERemainder method is extracted here.
            double value = angle - (Tau * Math.Round(angle * InverseTau));
            return value < 0 ? value + Tau : value;
        }

        /// <summary>
        /// Reduces a given angle to a value between 2π and -2π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double WrapAngleModulus(this double angle)
        {
            if (double.IsNaN(angle))
                return angle;
            double value = angle % Tau;
            return (value <= -PI) ? value + Tau : value - Tau;
        }

        /// <summary>
        /// Reduces a given angle to a value between 2π and -2π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double WrapAngle(this double angle)
        {
            if (double.IsNaN(angle))
                return angle;
            // The IEEERemainder method works better than the % modulus operator in this case, even if it is slower.
            //double value = IEEERemainder(angle, Tau);
            // The active ingredient of the IEEERemainder method is extracted here for performance reasons.
            double value = angle - (Tau * Math.Round(angle * InverseTau));
            return (value <= -PI) ? value + Tau : value - Tau;
        }

        /// <summary>
        /// Imitation of Excel's Mod Operator
        /// </summary>
        /// <param name="valueA">Source parameter</param>
        /// <param name="valueB">Destination parameter</param>
        /// <returns>Returns the same Modulus Result that Excel returns.</returns>
        /// <remarks>Created after finding out Excel returns a different value for the Mod Operator than .Net</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulo(this double valueA, double valueB)
            => ((valueA %= valueB) < 0) ? valueA + valueB : valueA;

        /// <summary>
        ///
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(double i, double j)
            => Atan2(i, -j);

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
            => Atan2((y1 - y2), (x1 - x2));

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
        public static double Angle(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (Math.Abs(x1 - x2) < Epsilon
            && Math.Abs(y1 - y2) < Epsilon
            && Math.Abs(z1 - z2) < Epsilon) ? 0 : Acos(Min(1.0d, DotProduct(Normalize3D(x1, y1, z1), Normalize3D(x2, y2, z2))));

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
            => Atan2(CrossProductVector(x1, y1, x2, y2, x3, y3), DotProductVector(x1, y1, x2, y2, x3, y3));

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteAngle(
            double x1, double y1,
            double x2, double y2)
        {
            // Find the angle of point a and point b.
            double test = -Angle(x1, y1, x2, y2) % PI;
            return test < 0 ? test += PI : test;
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
            => Acos((uX * vX + uY * vY) / Sqrt((uX * uX + uY * uY) * (vX * vX + vY * vY)));

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
            => Acos((uX * vX + uY * vY + uZ * vZ) / Sqrt((uX * uX + uY * uY + uZ * uZ) * (vX * vX + vY * vY + vZ * vZ)));

        /// <summary>
        /// Find the elliptical t that matches the coordinates of a circular angle.
        /// </summary>
        /// <param name="angle">The angle to transform into elliptic angle.</param>
        /// <param name="rx">The first radius of the ellipse.</param>
        /// <param name="ry">The second radius of the ellipse.</param>
        /// <returns></returns>
        /// <remarks>
        /// Based on the answer by flup at:
        /// http://stackoverflow.com/questions/17762077/how-to-find-the-point-on-ellipse-given-the-angle
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipsePolarAngle(double angle, double rx, double ry)
        {
            // Wrap the angle between -2PI and 2PI.
            double theta = angle % Tau;

            // Find the elliptical t that matches the circular angle.
            if (Math.Abs(theta) == HalfPi || Math.Abs(theta) == Pau)
                return angle;
            else if (theta > HalfPi && theta < Pau)
                return Atan(rx * Tan(theta) / ry) + PI;
            else if (theta < -HalfPi && theta > -Pau)
                return Atan(rx * Tan(theta) / ry) - PI;
            else
                return Atan(rx * Tan(theta) / ry);
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
            => Math.Abs(i) < Epsilon ? SlopeMax : (j / i);

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
            => (Math.Abs(x1 - x2) < Epsilon) ? SlopeMax : ((y2 - y1) / (x2 - x1));

        #region Derived Equivalent Math Functions

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

        // Derived equivalent Math Functions The following is a list of non-intrinsic math functions that can be derived from the intrinsic math functions:

        /// <summary>
        /// Derived math functions equivalent Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Secant(double value)
            => ((Math.Abs(value % PI - HalfPi) > Epsilon)
            && (Math.Abs(value % PI - -HalfPi) > Epsilon)) ? (1 / Cos(value)) : 0;

        /// <summary>
        /// Derived math functions equivalent  Co-secant
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cosecant(double Value)
            => ((Math.Abs(Value % PI) > Epsilon)
            && (Math.Abs(Value % PI - PI) > Epsilon)) ? (1 / Sin(Value)) : 0;

        /// <summary>
        /// Derived math functions equivalent Cotangent
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cotangent(double Value)
            => ((Math.Abs(Value % PI) > Epsilon)
            && (Math.Abs(Value % PI - PI) > Epsilon)) ? (1 / Tan(Value)) : 0;

        /// <summary>
        /// Derived math functions equivalent Inverse Sine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseSine(double value)
        {
            if (value == 1)
                return HalfPi;
            if (value == -1)
                return -HalfPi;
            if (Math.Abs(value) < 1)
                // Arc-sin(X)
                return Atan(value / Sqrt(-value * value + 1));
            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Cosine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseCosine(double value)
        {
            if (value == 1)
                return 0;
            if (value == -1)
                return PI;
            if (Math.Abs(value) < 1)
                // Arc-cos(X)
                return Atan(-value / Sqrt(-value * value + 1)) + 2 * Atan(1);
            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseSecant(double value)
        {
            if (value == 1)
                return 0;
            if (value == -1)
                return PI;
            if (Math.Abs(value) < 1)
                // Arc-sec(X)
                return Atan(value / Sqrt(value * value - 1)) + Sin((value) - 1) * (2 * Atan(1));
            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Co-secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseCosecant(double value)
        {
            if (value == 1)
                return HalfPi;
            if (value == -1)
                return -HalfPi;
            if (Math.Abs(value) < 1)
                // Arc-co-sec(X)
                return Atan(value / Sqrt(value * value - 1)) + (Sin(value) - 1) * (2 * Atan(1));
            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Cotangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>Arc-co-tan(X)</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseCotangent(double value)
            => (Atan(value) + (2 * Atan(1)));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Sine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HSin(X)</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicSine(double value)
            => ((Exp(value) - Exp((value * -1))) * 0.5d);

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Cosine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HCos(X)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicCosine(double value)
            => ((Exp(value) + Exp((value * -1))) * 0.5d);

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Tangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HTan(X)</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicTangent(double value)
            => ((Exp(value) - Exp((value * -1))) / (Exp(value) + Exp((value * -1))));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HSec(X)</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicSecant(double value)
            => (0.5d * (Exp(value) + Exp((value * -1))));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Co-secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HCosec(X)</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicCosecant(double value)
            => (0.5d * (Exp(value) - Exp((value * -1))));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Cotangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HCotan(X) </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicCotangent(double value)
            => ((Exp(value) + Exp((value * -1))) / (Exp(value) - Exp((value * -1))));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Sine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArcsin(X)</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicSine(double value)
            => Log((value + Sqrt(((value * value) + 1))));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Cosine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArccos(X)</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicCosine(double value)
            => Log((value + Sqrt(((value * value) - 1))));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Tangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArctan(X)</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicTangent(double value)
            => (Log(((1 + value) / (1 - value))) * 0.5d);

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArcsec(X) </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicSecant(double value)
            => Log(((Sqrt((((value * value) * -1) + 1)) + 1) / value));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Co-secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArccosec(X)</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicCosecant(double value)
            => Log((((Sin(value) * Sqrt(((value * value) + 1))) + 1) / value));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Cotangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArccotan(X)</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicCotangent(double value)
            => (Log(((value + 1) / (value - 1))) * 0.5d);

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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LogarithmTobaseN(double value, double numberBase)
            => (Math.Abs(numberBase - 1) > Epsilon) ? (Log(value) / Log(numberBase)) : 0;

        #endregion
    }
}
