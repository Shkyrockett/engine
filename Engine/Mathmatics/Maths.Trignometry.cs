// <copyright file="Maths.Trignometry.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

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
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double Roll, double Pitch, double Yaw) QuaternionToEulerAngles(double x, double y, double z, double w)
        {
            var halfPi = PI / 2;
            var test = x * y + z * w;
            (double Roll, double Pitch, double Yaw) quat = (0, 0, 0);
            if (test > 0.499d)
            { // singularitY at north pole
                quat.Yaw = 2d * Atan2(x, w);
                quat.Roll = halfPi;
                quat.Pitch = 0d;
            }
            else if (test < -0.499d)
            { // singularitY at south pole
                quat.Yaw = -2d * Atan2(x, w);
                quat.Roll = -halfPi;
                quat.Pitch = 0d;
            }
            else
            {
                var sqX = x * x;
                var sqY = y * y;
                var sqZ = z * z;
                quat.Yaw = Atan2(2d * y * w - 2d * x * z, 1d - 2d * sqY - 2d * sqZ);
                quat.Roll = Asin(2d * test);
                quat.Pitch = Atan2(2d * x * w - 2d * y * z, 1d - 2d * sqX - 2d * sqZ);
            }

            if (quat.Pitch <= Epsilon)
                quat.Pitch = 0d;
            if (quat.Yaw <= Epsilon)
                quat.Yaw = 0d;
            if (quat.Roll <= Epsilon)
                quat.Roll = 0d;
            return quat;
        }

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
            var value = angle - (Tau * Math.Round(angle * InverseTau));
            return value < 0 ? value + Tau : value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NormalizeRadian(double angle)
        {
            var value = (angle + PI) % (Tau);
            value += value > 0d ? -PI : PI;
            return value;
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
            var value = angle % Tau;
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
            var value = angle - (Tau * Math.Round(angle * InverseTau));
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
        //[DebuggerStepThrough]
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
        //[DebuggerStepThrough]
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </acknowledgment>
        //[DebuggerStepThrough]
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteAngle(
            double x1, double y1,
            double x2, double y2)
        {
            // Find the angle of point a and point b.
            var test = -Angle(x1, y1, x2, y2) % PI;
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://james-ramsden.com/angle-between-two-vectors/
        /// </acknowledgment>
        //[DebuggerStepThrough]
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://james-ramsden.com/angle-between-two-vectors/
        /// </acknowledgment>
        //[DebuggerStepThrough]
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Based on the answer by flup at: http://stackoverflow.com/questions/17762077/how-to-find-the-point-on-ellipse-given-the-angle
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipticalPolarAngle(double angle, double rx, double ry)
        {
            // Wrap the angle between -2PI and 2PI.
            var theta = angle % Tau;

            // Find the elliptical t that matches the circular angle.
            if (Math.Abs(theta) == Right || Math.Abs(theta) == Pau)
                return angle;
            else if (theta > Right && theta < Pau)
                return Atan(rx * Tan(theta) / ry) + PI;
            else if (theta < -Right && theta > -Pau)
                return Atan(rx * Tan(theta) / ry) - PI;
            else
                return Atan(rx * Tan(theta) / ry);
        }

        /// <summary>
        /// Return a "correction" angle that converts a subtended angle to a parametric angle for an
        /// ellipse with radii a and b.
        /// </summary>
        /// <param name="subtended"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Code ported from: https://www.khanacademy.org/computer-programming/e/6221186997551104
        /// Math from: http://mathworld.wolfram.com/Ellipse-LineIntersection.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SubtendedToParametric(double subtended, double a, double b)
        {
            if (a == b)
                return 0;  /* circle needs no correction */

            var rx = Cos(subtended);  /* ray from the origin */
            var ry = Sin(subtended);
            var e = (a * b) / Sqrt(a * a * ry * ry + b * b * rx * rx);
            var ex = e * rx;  /* where ray intersects ellipse */
            var ey = e * ry;
            var parametric = Atan2(a * ey, b * ex);
            subtended = Atan2(ry, rx);  /* Normalized! */
            return parametric - subtended;
        }

        #region Reflect

        /// <summary>
        /// Calculates the reflection of a point off a line segment
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="axisX"></param>
        /// <param name="axisY"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) Reflect(double x1, double y1, double x2, double y2, double axisX, double axisY)
        {
            var (i, j) = Delta(x1, y1, x2, y2);
            var magnatude = 0.5d * DotProduct(i, j, i, j);
            var reflection = CrossProduct(i, j, CrossProduct(x2, y2, x1, y1), DotProduct(axisX, axisY, i, j));
            return ((magnatude * reflection - axisX),
                    (magnatude * reflection - axisY));
        }

        #endregion

        #region Rotate Point

        /// <summary>
        /// Rotate a point around the world origin.
        /// </summary>
        /// <param name="x">The x component of the point to rotate.</param>
        /// <param name="y">The y component of the point to rotate.</param>
        /// <param name="angle">The angle to rotate in pi radians.</param>
        /// <returns>A point rotated about the origin by the specified pi radian angle.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) RotatePoint2D(double x, double y, double angle)
            => RotatePoint2D(x, y, 0, 0, angle);

        /// <summary>
        /// Rotate a point around a fulcrum point.
        /// </summary>
        /// <param name="x">The x component of the point to rotate.</param>
        /// <param name="y">The y component of the point to rotate.</param>
        /// <param name="cx">The x component of the fulcrum point to rotate the point around.</param>
        /// <param name="cy">The y component of the fulcrum point to rotate the point around.</param>
        /// <param name="angle">The angle to rotate the point in pi radians.</param>
        /// <returns>A point rotated about the fulcrum point by the specified pi radian angle.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) RotatePoint2D(double x, double y, double cx, double cy, double angle)
        {
            var deltaX = x - cx;
            var deltaY = y - cy;
            var angleCos = Cos(angle);
            var angleSin = Sin(angle);
            return ((cx + (deltaX * angleCos - deltaY * angleSin)),
                    (cy + (deltaX * angleSin + deltaY * angleCos)));
        }

        #endregion

        #region Slope

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
        //[DebuggerStepThrough]
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(
            double x1, double y1,
            double x2, double y2)
            => (Math.Abs(x1 - x2) < Epsilon) ? SlopeMax : ((y2 - y1) / (x2 - x1));

        #endregion

        #region Unit

        /// <summary>
        /// Unit of a 2D Vector.
        /// </summary>
        /// <param name="i">The i component of the Vector to Unitize.</param>
        /// <param name="j">The j component of the Vector to Unitize.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) Unit(double i, double j)
            => Scale2D(i, j, 1 / Sqrt(((i * i) + (j * j))));

        /// <summary>
        /// Unit of a 3D Vector.
        /// </summary>
        /// <param name="i">The i component of the Vector to Unitize.</param>
        /// <param name="j">The j component of the Vector to Unitize.</param>
        /// <param name="k">The k component of the Vector to Unitize.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K) Unit(double i, double j, double k)
            => Scale3D(i, j, k, 1 / Sqrt(((i * i) + (j * j) + (k * k))));

        /// <summary>
        /// Unit of a 4D Vector.
        /// </summary>
        /// <param name="i">The i component of the Vector to Unitize.</param>
        /// <param name="j">The j component of the Vector to Unitize.</param>
        /// <param name="k">The k component of the Vector to Unitize.</param>
        /// <param name="l">The l component of the Vector to Unitize.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K, double L) Unit(double i, double j, double k, double l)
            => Scale4D(i, j, k, l, 1 / Sqrt(((i * i) + (j * j) + (k * k) + (l * l))));

        #endregion

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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Abs(double i, double j, double k)
            => Magnitude(i, j, k);

        /// <summary>
        /// Derived math functions equivalent Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Secant(double value)
            => (value % PI == Right)
            && (value % PI == -Right)
            ? (1 / Cos(value)) : 0;

        /// <summary>
        /// Derived math functions equivalent  Co-secant
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cosecant(double Value)
            => (Value % PI == 0)
            && (Value % PI == PI)
            ? (1 / Sin(Value)) : 0;

        /// <summary>
        /// Derived math functions equivalent Cotangent
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cotangent(double Value)
            => (Value % PI == 0)
            && (Value % PI == PI)
            ? (1 / Tan(Value)) : 0;

        /// <summary>
        /// Derived math functions equivalent Inverse Sine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseSine(double value)
        {
            if (value == 1)
                return Right;
            if (value == -1)
                return -Right;
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseCosecant(double value)
        {
            if (value == 1)
                return Right;
            if (value == -1)
                return -Right;
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicCosine(double value)
            => ((Exp(value) + Exp((value * -1))) * 0.5d);

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Tangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HTan(X)</remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
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
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LogarithmTobaseN(double value, double numberBase)
            => (numberBase == 1) ? (Log(value) / Log(numberBase)) : 0;

        #endregion
    }
}
