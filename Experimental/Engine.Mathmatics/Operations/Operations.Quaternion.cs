// <copyright file="Operations.Quaternion.cs" company="Shkyrockett" >
//    Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Mathematics;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Operations
    {
        #region Multiply
        /// <summary>
        /// Multiply Two Quaternions.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="w1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="w2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) MultiplyQuaternions(double x1, double y1, double z1, double w1, double x2, double y2, double z2, double w2)
            => ((x1 * w2) + (x2 * w1) + ((y1 * z2) - (z1 * y2)),
                (y1 * w2) + (y2 * w1) + ((z1 * x2) - (x1 * z2)),
                (z1 * w2) + (z2 * w1) + ((x1 * y2) - (y1 * x2)),
                (w1 * w2) - ((x1 * x2) + (y1 * y2) + (z1 * z2)));
        #endregion Multiply

        #region Divide
        /// <summary>
        /// Divide Two Quaternions.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="w1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="w2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) DivideQuaternions(double x1, double y1, double z1, double w1, double x2, double y2, double z2, double w2)
        {
            var num14 = (x2 * x2) + (y2 * y2) + (z2 * z2) + (w2 * w2);
            var num5 = 1d / num14;
            var num4 = -x2 * num5;
            var num3 = -y2 * num5;
            var num2 = -z2 * num5;
            var num = w2 * num5;
            var num13 = (y1 * num2) - (z1 * num3);
            var num12 = (z1 * num4) - (x1 * num2);
            var num11 = (x1 * num3) - (y1 * num4);
            var num10 = (x1 * num4) + (y1 * num3) + (z1 * num2);
            return (
                (x1 * num) + (num4 * w1) + num13,
                (y1 * num) + (num3 * w1) + num12,
                (z1 * num) + (num2 * w1) + num11,
                (w1 * num) - num10);
        }
        #endregion Divide

        #region Factories
        /// <summary>
        /// The from rotation matrix.
        /// </summary>
        /// <param name="m0x0"></param>
        /// <param name="m0x1"></param>
        /// <param name="m0x2"></param>
        /// <param name="m1x0"></param>
        /// <param name="m1x1"></param>
        /// <param name="m1x2"></param>
        /// <param name="m2x0"></param>
        /// <param name="m2x1"></param>
        /// <param name="m2x2"></param>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) QuaternionFromRotationMatrix(
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2)
        {
            var trace = m0x0 + m1x1 + m2x2;
            (double X, double Y, double Z, double W) quaternion = (0d, 0d, 0d, 0d);
            if (trace > 0d)
            {
                var root = Sqrt(trace + 1d);
                quaternion.W = root * 0.5d;
                root = 0.5d / root;
                quaternion.X = (m1x2 - m2x1) * root;
                quaternion.Y = (m2x0 - m0x2) * root;
                quaternion.Z = (m0x1 - m1x0) * root;
                return quaternion;
            }
            if ((m0x0 >= m1x1) && (m0x0 >= m2x2))
            {
                var root = Sqrt(1f + m0x0 - m1x1 - m2x2);
                var w = 0.5d / root;
                quaternion.X = 0.5d * root;
                quaternion.Y = (m0x1 + m1x0) * w;
                quaternion.Z = (m0x2 + m2x0) * w;
                quaternion.W = (m1x2 - m2x1) * w;
                return quaternion;
            }
            if (m1x1 > m2x2)
            {
                var root = Sqrt(1f + m1x1 - m0x0 - m2x2);
                var w = 0.5d / root;
                quaternion.X = (m1x0 + m0x1) * w;
                quaternion.Y = 0.5d * root;
                quaternion.Z = (m2x1 + m1x2) * w;
                quaternion.W = (m2x0 - m0x2) * w;
                return quaternion;
            }
            var sqrt = Sqrt(1f + m2x2 - m0x0 - m1x1);
            var ww = 0.5d / sqrt;
            quaternion.X = (m2x0 + m0x2) * ww;
            quaternion.Y = (m2x1 + m1x2) * ww;
            quaternion.Z = 0.5d * sqrt;
            quaternion.W = (m0x1 - m1x0) * ww;
            return quaternion;
        }

        /// <summary>
        /// The from axis angle.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="angle">The angle.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) QuaternionFromAxisAngle(double i, double j, double k, double angle)
        {
            var halfAngle = angle * 0.5d;
            var sin = Sin(halfAngle);
            var cos = Cos(halfAngle);
            return (i * sin, j * sin, k * sin, cos);
        }

        /// <summary>
        /// The from Euler angles.
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <param name="pitch">The pitch.</param>
        /// <param name="yaw">The yaw.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) QuaternionFromEulerAngles(double roll, double pitch, double yaw)
        {
            var halfRoll = roll * 0.5d;
            var rollSin = Sin(halfRoll);
            var rollCos = Cos(halfRoll);
            var halfPitch = pitch * 0.5d;
            var pitchSin = Sin(halfPitch);
            var pitchCos = Cos(halfPitch);
            var halfYaw = yaw * 0.5d;
            var yawSin = Sin(halfYaw);
            var yawCos = Cos(halfYaw);
            return (
                (yawCos * pitchSin * rollCos) + (yawSin * pitchCos * rollSin),
                (yawSin * pitchCos * rollCos) - (yawCos * pitchSin * rollSin),
                (yawCos * pitchCos * rollSin) - (yawSin * pitchSin * rollCos),
                (yawCos * pitchCos * rollCos) + (yawSin * pitchSin * rollSin));
        }
        #endregion Factories

        #region Conjugate
        /// <summary>
        /// The conjugate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) Conjugate(double x, double y, double z, double w) => (x, -y, -z, w);
        #endregion Conjugate

        #region Conversion
        /// <summary>
        /// The quaternion to Euler angles.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="w">The w.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double Roll, double Pitch, double Yaw) QuaternionToEulerAngles(double x, double y, double z, double w)
        {
            var test = (x * y) + (z * w);
            var quat = (Yaw: 0d, Roll: 0d, Pitch: 0d);
            if (test > 0.499d)
            {
                // singularitY at north pole
                quat.Yaw = 2d * Atan2(x, w);
                quat.Roll = HalfPi;
                quat.Pitch = 0d;
            }
            else if (test < -0.499d)
            {
                // singularitY at south pole
                quat.Yaw = -2d * Atan2(x, w);
                quat.Roll = -HalfPi;
                quat.Pitch = 0d;
            }
            else
            {
                var sqX = x * x;
                var sqY = y * y;
                var sqZ = z * z;
                quat.Yaw = Atan2((2d * y * w) - (2d * x * z), 1d - (2d * sqY) - (2d * sqZ));
                quat.Roll = Asin(2d * test);
                quat.Pitch = Atan2((2d * x * w) - (2d * y * z), 1d - (2d * sqX) - (2d * sqZ));
            }

            if (quat.Pitch <= Epsilon)
            {
                quat.Pitch = 0d;
            }

            if (quat.Yaw <= Epsilon)
            {
                quat.Yaw = 0d;
            }

            if (quat.Roll <= Epsilon)
            {
                quat.Roll = 0d;
            }

            return quat;
        }

        /// <summary>
        /// Gets a 3x3 rotation matrix from this Quaternion.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) QuaternionToRotationMatrix(double x, double y, double z, double w) => (
                1d - (2d * ((y * y) + (z * z))),
                (2d * y * x) - (2d * z * w),
                (2d * z * x) + (2d * y * w),
                (2d * y * x) + (2d * z * w),
                1d - (2d * ((x * x) + (z * z))),
                (2d * z * y) - (2d * x * w),
                (2d * z * x) - (2d * y * w),
                (2d * z * y) + (2d * x * w),
                1d - (2d * ((x * x) + (y * y))));

        /// <summary>
        /// Gets a 4x4 matrix from this Quaternion.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        /// <remarks>
        /// source -> http://content.gpwiki.org/index.php/OpenGL:Tutorials:Using_Quaternions_to_represent_rotation#Quaternion_to_Matrix
        /// </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) QuaternionToMatrix(double x, double y, double z, double w) => (
                           1d - (2d * ((y * y) + (z * z))),
                           2d * ((x * y) - (w * z)),
                           2d * ((x * z) + (w * y)),
                           0d,
                           2d * ((x * y) + (w * z)),
                           1d - (2d * ((x * x) + (z * z))),
                           2d * ((y * z) - (w * x)),
                           0d,
                           2d * ((x * z) - (w * y)),
                           2d * ((y * z) + (w * x)),
                           1d - (2d * ((x * x) + (y * y))),
                           0d,
                           2d * ((x * z) - (w * y)),
                           2d * ((y * z) + (w * x)),
                           1d - (2d * ((x * x) + (y * y))),
                           0d);

        /// <summary>
        /// The to axis.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        /// <param name="quaternion">The quaternion.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double XAxisX, double XAxisY, double XAxisZ,
            double YAxisX, double YAxisY, double YAxisZ,
            double ZAxisX, double ZAxisY, double ZAxisZ
            ) QuaternionToAxis(double x, double y, double z, double w)
        {
            var (m0x0, m0x1, m0x2, m1x0, m1x1, m1x2, m2x0, m2x1, m2x2) = QuaternionToRotationMatrix(x, y, z, w);
            return (m0x0, m1x0, m2x0, m0x1, m1x1, m2x1, m0x2, m1x2, m2x2);
        }

        /// <summary>
        /// The quaternion representing the rotation is
        /// q = cos(A/2)+sin(A/2)*(X*i+Y*j+Z*k)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double Angle, double X, double Y, double Z) QuaternionToAngleAxis(double x, double y, double z, double w)
        {
            var sqrLength = (x * x) + (y * y) + (z * z);
            if (sqrLength == 0d)
            {
                return (0d, 1d, 0d, 0d);
            }

            var unit = 1d / Sqrt(sqrLength);
            return (2d * Acos(w), x * unit, y * unit, z * unit);
        }
        #endregion Conversion
    }
}
