// <copyright file="Primitives.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Maths;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// The primitives class.
    /// </summary>
    public static class Primitives
    {
        // ToDo: Add Tuple Math here.
        
        /// <summary>
        /// Checks if two vectors are equal within a small bounded error.
        /// </summary>
        /// <param name="p0">First vector to compare.</param>
        /// <param name="p1">Second vector to compare.</param>
        /// <returns>True if the vectors are almost equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsOrClose(Point2D p0, Point2D p1)
            => p0.SquareDistance(p1) < Epsilon;

        /// <summary>
        /// Checks if two vectors are equal within a small bounded error.
        /// </summary>
        /// <param name="v0">First vector to compare.</param>
        /// <param name="v1">Second vector to compare.</param>
        /// <returns>True if the vectors are almost equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsOrClose(Vector2D v0, Vector2D v1)
            => v0.SquareDistance(v1) < Epsilon;

        #region Absolute Angle
        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="pointA">First Point.</param>
        /// <param name="pointB">Second Point.</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteAngle(this Point2D pointA, Point2D pointB)
            => Maths.AbsoluteAngle(pointA.X, pointA.Y, pointB.X, pointB.Y);

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="segment">Line segment.</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteAngle(this LineSegment segment)
            => Maths.AbsoluteAngle(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);
        #endregion Absolute Angle

        #region Add
        /// <summary>
        /// Adds a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="augend">The <see cref="Point2D"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Add(this Point2D augend, double addend)
            => Add2D(augend.X, augend.Y, addend);

        /// <summary>
        /// Adds a <see cref="Point2D"/> to a <see cref="Point2D"/> by value.
        /// </summary>
        /// <param name="augend">The <see cref="Point2D"/> to add to.</param>
        /// <param name="addend">The <see cref="Point2D"/> to add with.</param>
        /// <returns>
        /// Returns a <see cref="Point2D"/> structure enlarged by the amount of the other <see cref="Point2D"/> structure.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(this Point2D augend, Point2D addend)
            => Add2D(augend.X, augend.Y, addend.X, addend.Y);

        /// <summary>
        /// Adds a <see cref="Size2D"/> to a <see cref="Point2D"/> by value.
        /// </summary>
        /// <param name="augend">The <see cref="Point2D"/> to add to.</param>
        /// <param name="addend">The <see cref="Size2D"/> to add with.</param>
        /// <returns>
        /// Returns a <see cref="Point2D"/> structure enlarged by the amount of the <see cref="Size2D"/> structure.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Add(this Point2D augend, Size2D addend)
            => Add2D(augend.X, augend.Y, addend.Width, addend.Height);

        /// <summary>
        /// Adds a <see cref="Vector2D"/> to a <see cref="Point2D"/> by value.
        /// </summary>
        /// <param name="augend">The <see cref="Point2D"/> to add to.</param>
        /// <param name="addend">The <see cref="Vector2D"/> to add with.</param>
        /// <returns>
        /// Returns a <see cref="Point2D"/> structure enlarged by the amount of the <see cref="Size2D"/> structure.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Add(this Point2D augend, Vector2D addend)
            => Add2D(augend.X, augend.Y, addend.I, addend.J);

        /// <summary>
        /// Adds a <see cref="Size2D"/> by a value.
        /// </summary>
        /// <param name="augend">The <see cref="Size2D"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Add(this Size2D augend, double addend)
            => Add2D(augend.Width, augend.Height, addend);

        /// <summary>
        /// Adds a <see cref="Point2D"/> to a <see cref="Size2D"/> by value.
        /// </summary>
        /// <param name="augend">The <see cref="Size2D"/> to add to.</param>
        /// <param name="addend">The <see cref="Point2D"/> to add with.</param>
        /// <returns>
        /// Returns a <see cref="Size2D"/> structure enlarged by the amount of the <see cref="Point2D"/> structure.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Add(this Size2D augend, Point2D addend)
            => Add2D(augend.Width, augend.Height, addend.X, addend.Y);

        /// <summary>
        /// Adds a <see cref="Size2D"/> to a <see cref="Size2D"/> by value.
        /// </summary>
        /// <param name="augend">The <see cref="Size2D"/> to add to.</param>
        /// <param name="addend">The <see cref="Size2D"/> to add with.</param>
        /// <returns>
        /// Returns a <see cref="Size2D"/> structure enlarged by the amount of the other <see cref="Size2D"/> structure.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Add(this Size2D augend, Size2D addend)
            => Add2D(augend.Width, augend.Height, addend.Width, addend.Height);

        /// <summary>
        /// Adds a <see cref="Vector2D"/> to a <see cref="Size2D"/> by value.
        /// </summary>
        /// <param name="augend">The <see cref="Size2D"/> to add to.</param>
        /// <param name="addend">The <see cref="Vector2D"/> to add with.</param>
        /// <returns>
        /// Returns a <see cref="Size2D"/> structure enlarged by the amount of the <see cref="Point2D"/> structure.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Add(this Size2D augend, Vector2D addend)
            => Add2D(augend.Width, augend.Height, addend.I, addend.J);

        ///// <summary>
        ///// Adds a <see cref="PointF"/> by a value.
        ///// </summary>
        ///// <param name="augend">The <see cref="PointF"/> to inflate.</param>
        ///// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        ///// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector2D Add(this PointF augend, PointF addend)
        //    => Add2D(augend.X, augend.Y, addend.X, addend.Y);

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(this Vector2D augend, int addend)
            => Add2D(augend.I, augend.J, addend, addend);

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(this Vector2D augend, float addend)
            => Add2D(augend.I, augend.J, addend);

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(this Vector2D augend, double addend)
            => Add2D(augend.I, augend.J, addend);

        ///// <summary>
        ///// Add VectorF
        ///// </summary>
        ///// <param name="augend"></param>
        ///// <param name="addend"></param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Point2D Add(this Vector2D augend, Point addend)
        //    => Add2D(augend.I, augend.J, addend.X, addend.Y);

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Add(this Vector2D augend, Point2D addend)
            => Add2D(augend.I, augend.J, addend.X, addend.Y);

        /// <summary>
        /// Add Vector2D
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(this Vector2D augend, Vector2D addend)
            => Add2D(augend.I, augend.J, addend.I, addend.J);

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Add(this Vector3D augend, double addend)
            => Add3D(augend.I, augend.J, augend.K, addend);

        /// <summary>
        /// Add Vector3D
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Add(this Vector3D augend, Point3D addend)
            => Add3D(augend.I, augend.J, augend.K, addend.X, addend.Y, addend.Z);

        /// <summary>
        /// Add Vector3D
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Add(this Vector3D augend, Vector3D addend)
            => Add3D(augend.I, augend.J, augend.K, addend.I, addend.J, addend.K);

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="Vector4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Add(this Vector4D augend, double addend)
            => Add4D(augend.I, augend.J, augend.K, augend.L, addend);

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="Vector4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Add(this Vector4D augend, Vector4D addend)
            => Add4D(augend.I, augend.J, augend.K, augend.L, addend.I, addend.J, addend.K, addend.L);

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Add(this Quaternion4D augend, double addend)
            => Add4D(augend.X, augend.Y, augend.Z, augend.W, addend);

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Add(this Quaternion4D augend, Quaternion4D addend)
            => Add4D(augend.X, augend.Y, augend.Z, augend.W, addend.X, addend.Y, addend.Z, addend.W);

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="LineSegment"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment Add(this LineSegment augend, double addend)
            => Add4D(augend.AX, augend.AY, augend.BX, augend.BY, addend);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Add(this Matrix2x2D augend, Matrix2x2D addend)
            => Add2x2x2x2(
                augend.M0x0, augend.M0x1,
                augend.M1x0, augend.M1x1,
                addend.M0x0, addend.M0x1,
                addend.M1x0, addend.M1x1);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Add(this Matrix3x3D augend, Matrix3x3D addend)
            => Add3x3x3x3(
                augend.M0x0, augend.M0x1, augend.M0x2,
                augend.M1x0, augend.M1x1, augend.M1x2,
                augend.M2x0, augend.M2x1, augend.M2x2,
                addend.M0x0, addend.M0x1, addend.M0x2,
                addend.M1x0, addend.M1x1, addend.M1x2,
                addend.M2x0, addend.M2x1, addend.M2x2);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Add(this Matrix4x4D augend, Matrix4x4D addend)
            => Add4x4x4x4(
                augend.M0x0, augend.M0x1, augend.M0x2, augend.M0x3,
                augend.M1x0, augend.M1x1, augend.M1x2, augend.M1x3,
                augend.M2x0, augend.M2x1, augend.M2x2, augend.M2x3,
                augend.M3x0, augend.M3x1, augend.M3x2, augend.M3x3,
                addend.M0x0, addend.M0x1, addend.M0x2, addend.M0x3,
                addend.M1x0, addend.M1x1, addend.M1x2, addend.M1x3,
                addend.M2x0, addend.M2x1, addend.M2x2, addend.M2x3,
                addend.M3x0, addend.M3x1, addend.M3x2, addend.M3x3);

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="LineSegment"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment Add(this LineSegment augend, LineSegment addend)
            => Add4D(augend.AX, augend.AY, augend.BX, augend.BY, addend.AX, addend.AY, addend.BX, addend.BY);

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="Transform2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform2D Add(this Transform2D augend, Transform2D addend)
            => new Transform2D(augend.X + addend.X, augend.Y + addend.Y, augend.SkewX + addend.SkewX, augend.SkewY + addend.SkewY, augend.ScaleX * addend.ScaleX, augend.ScaleY * addend.ScaleY);
        #endregion Add

        #region Adjoint
        /// <summary>
        /// The adjoint.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="Matrix2x2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Adjoint(this Matrix2x2D source)
            => new Matrix2x2D(
                source.M1x1,
                -source.M0x1,
                -source.M1x0,
                source.M0x0);

        /// <summary>
        /// The adjoint.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Adjoint(this Matrix3x3D source)
            => new Matrix3x3D(
                (source.M1x1 * source.M2x2) - (source.M1x2 * source.M2x1),
                -((source.M0x1 * source.M2x2) - (source.M0x2 * source.M2x1)),
                (source.M0x1 * source.M1x2) - (source.M0x2 * source.M1x1),
                -((source.M1x0 * source.M2x2) - (source.M1x2 * source.M2x0)),
                (source.M0x0 * source.M2x2) - (source.M0x2 * source.M2x0),
                -((source.M0x0 * source.M1x2) - (source.M0x2 * source.M1x0)),
                (source.M1x0 * source.M2x1) - (source.M1x1 * source.M2x0),
                -((source.M0x0 * source.M2x1) - (source.M0x1 * source.M2x0)),
                (source.M0x0 * source.M1x1) - (source.M0x1 * source.M1x0));

        /// <summary>
        /// Used to generate the adjoint of this matrix.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>The adjoint matrix of the current instance.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Adjoint(this Matrix4x4D source)
        {
            var m22m33m32m23 = (source.M2x2 * source.M3x3) - (source.M3x2 * source.M2x3);
            var m21m33m31m23 = (source.M2x1 * source.M3x3) - (source.M3x1 * source.M2x3);
            var m21m32m31m22 = (source.M2x1 * source.M3x2) - (source.M3x1 * source.M2x2);

            var m12m33m32m13 = (source.M1x2 * source.M3x3) - (source.M3x2 * source.M1x3);
            var m11m33m31m13 = (source.M1x1 * source.M3x3) - (source.M3x1 * source.M1x3);
            var m11m32m31m12 = (source.M1x1 * source.M3x2) - (source.M3x1 * source.M1x2);

            var m12m23m22m13 = (source.M1x2 * source.M2x3) - (source.M2x2 * source.M1x3);
            var m11m23m21m13 = (source.M1x1 * source.M2x3) - (source.M2x1 * source.M1x3);
            var m11m22m21m12 = (source.M1x1 * source.M2x2) - (source.M2x1 * source.M1x2);

            var m20m33m30m23 = (source.M2x0 * source.M3x3) - (source.M3x0 * source.M2x3);
            var m20m32m30m22 = (source.M2x0 * source.M3x2) - (source.M3x0 * source.M2x2);
            var m10m33m30m13 = (source.M1x0 * source.M3x3) - (source.M3x0 * source.M1x3);

            var m10m32m30m12 = (source.M1x0 * source.M3x2) - (source.M3x0 * source.M1x2);
            var m10m23m20m13 = (source.M1x0 * source.M2x3) - (source.M2x0 * source.M1x3);
            var m10m22m20m12 = (source.M1x0 * source.M2x2) - (source.M2x0 * source.M1x2);

            var m20m31m30m21 = (source.M2x0 * source.M3x1) - (source.M3x0 * source.M2x1);
            var m10m31m30m11 = (source.M1x0 * source.M3x1) - (source.M3x0 * source.M1x1);
            var m10m21m20m11 = (source.M1x0 * source.M2x1) - (source.M2x0 * source.M1x1);

            return new Matrix4x4D(
                (source.M1x1 * m22m33m32m23) - (source.M1x2 * m21m33m31m23) + (source.M1x3 * m21m32m31m22),
                -((source.M0x1 * m22m33m32m23) - (source.M0x2 * m21m33m31m23) + (source.M0x3 * m21m32m31m22)),
                (source.M0x1 * m12m33m32m13) - (source.M0x2 * m11m33m31m13) + (source.M0x3 * m11m32m31m12),
                -((source.M0x1 * m12m23m22m13) - (source.M0x2 * m11m23m21m13) + (source.M0x3 * m11m22m21m12)),
                -((source.M1x0 * m22m33m32m23) - (source.M1x2 * m20m33m30m23) + (source.M1x3 * m20m32m30m22)),
                (source.M0x0 * m22m33m32m23) - (source.M0x2 * m20m33m30m23) + (source.M0x3 * m20m32m30m22),
                -((source.M0x0 * m12m33m32m13) - (source.M0x2 * m10m33m30m13) + (source.M0x3 * m10m32m30m12)),
                (source.M0x0 * m12m23m22m13) - (source.M0x2 * m10m23m20m13) + (source.M0x3 * m10m22m20m12),
                (source.M1x0 * m21m33m31m23) - (source.M1x1 * m20m33m30m23) + (source.M1x3 * m20m31m30m21),
                -((source.M0x0 * m21m33m31m23) - (source.M0x1 * m20m33m30m23) + (source.M0x3 * m20m31m30m21)),
                (source.M0x0 * m11m33m31m13) - (source.M0x1 * m10m33m30m13) + (source.M0x3 * m20m31m30m21),
                -((source.M0x0 * m11m23m21m13) - (source.M0x1 * m10m23m20m13) + (source.M0x3 * m10m21m20m11)),
                -((source.M1x0 * m21m32m31m22) - (source.M1x1 * m20m32m30m22) + (source.M1x2 * m20m31m30m21)),
                (source.M0x0 * m21m32m31m22) - (source.M0x1 * m20m32m30m22) + (source.M0x2 * m20m31m30m21),
                -((source.M0x0 * m11m32m31m12) - (source.M0x1 * m10m32m30m12) + (source.M0x2 * m10m31m30m11)),
                (source.M0x0 * m11m22m21m12) - (source.M0x1 * m10m22m20m12) + (source.M0x2 * m10m21m20m11));
        }
        #endregion Adjoint

        #region Angle
        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="PointA">Starting Point</param>
        /// <param name="PointB">Ending Point</param>
        /// <returns>Returns the Angle of a line.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(this Point2D PointA, Point2D PointB)
            => Maths.Angle(PointA.X, PointA.Y, PointB.X, PointB.Y);

        /// <summary>
        /// Returns the Angle of a line segment.
        /// </summary>
        /// <returns>Returns the Angle of a line.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(this LineSegment segment)
            => Maths.Angle(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);
        #endregion Angle

        #region Append
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="T:int[]"/>.</returns>
        public static int[] Add(this int[] array, int t)
        {
            ArrayUtilities.Add(ref array, t);
            return array;
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="T:float[]"/>.</returns>
        public static float[] Add(this float[] array, float t)
        {
            ArrayUtilities.Add(ref array, t);
            return array;
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="T:double[]"/>.</returns>
        public static double[] Add(this double[] array, double t)
        {
            ArrayUtilities.Add(ref array, t);
            return array;
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="T:Point2D[]"/>.</returns>
        public static Point2D[] Add(this Point2D[] array, Point2D t)
        {
            ArrayUtilities.Add(ref array, t);
            return array;
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="T:Point3D[]"/>.</returns>
        public static Point3D[] Add(this Point3D[] array, Point3D t)
        {
            ArrayUtilities.Add(ref array, t);
            return array;
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="T:Vector2D[]"/>.</returns>
        public static Vector2D[] Add(this Vector2D[] array, Vector2D t)
        {
            ArrayUtilities.Add(ref array, t);
            return array;
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="T:Vector3D[]"/>.</returns>
        public static Vector3D[] Add(this Vector3D[] array, Vector3D t)
        {
            ArrayUtilities.Add(ref array, t);
            return array;
        }
        #endregion Append

        #region Center
        /// <summary>
        /// Extension method to find the center point of a rectangle.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle2D"/> of which you want the center.</param>
        /// <returns>A <see cref="Point2D"/> representing the center point of the <see cref="Rectangle2D"/>.</returns>
        /// <remarks>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Center(this Rectangle2D rectangle)
            => new Point2D((0.5d * rectangle.Width) + rectangle.X, (0.5d * rectangle.Height) + rectangle.Y);
        #endregion Center

        #region Cofactor
        /// <summary>
        /// The cofactor.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="Matrix2x2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Cofactor(this Matrix2x2D source)
            => new Matrix2x2D(
                -source.M1x1,
                source.M0x1,
                source.M1x0,
                -source.M0x0);

        /// <summary>
        /// The cofactor.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Cofactor(this Matrix3x3D source)
            => new Matrix3x3D(
                -((source.M1x1 * source.M2x2) - (source.M1x2 * source.M2x1)),
                (source.M0x1 * source.M2x2) - (source.M0x2 * source.M2x1),
                -((source.M0x1 * source.M1x2) - (source.M0x2 * source.M1x1)),
                (source.M1x0 * source.M2x2) - (source.M1x2 * source.M2x0),
                -((source.M0x0 * source.M2x2) - (source.M0x2 * source.M2x0)),
                (source.M0x0 * source.M1x2) - (source.M0x2 * source.M1x0),
                -((source.M1x0 * source.M2x1) - (source.M1x1 * source.M2x0)),
                (source.M0x0 * source.M2x1) - (source.M0x1 * source.M2x0),
                -((source.M0x0 * source.M1x1) - (source.M0x1 * source.M1x0)));

        /// <summary>
        /// The cofactor.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="Matrix4x4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Cofactor(this Matrix4x4D source)
        {
            var m22m33m32m23 = (source.M2x2 * source.M3x3) - (source.M3x2 * source.M2x3);
            var m21m33m31m23 = (source.M2x1 * source.M3x3) - (source.M3x1 * source.M2x3);
            var m21m32m31m22 = (source.M2x1 * source.M3x2) - (source.M3x1 * source.M2x2);
            var m12m33m32m13 = (source.M1x2 * source.M3x3) - (source.M3x2 * source.M1x3);

            var m11m33m31m13 = (source.M1x1 * source.M3x3) - (source.M3x1 * source.M1x3);
            var m11m32m31m12 = (source.M1x1 * source.M3x2) - (source.M3x1 * source.M1x2);
            var m12m23m22m13 = (source.M1x2 * source.M2x3) - (source.M2x2 * source.M1x3);
            var m11m23m21m13 = (source.M1x1 * source.M2x3) - (source.M2x1 * source.M1x3);

            var m11m22m21m12 = (source.M1x1 * source.M2x2) - (source.M2x1 * source.M1x2);
            var m20m33m30m23 = (source.M2x0 * source.M3x3) - (source.M3x0 * source.M2x3);
            var m20m32m30m22 = (source.M2x0 * source.M3x2) - (source.M3x0 * source.M2x2);
            var m10m33m30m13 = (source.M1x0 * source.M3x3) - (source.M3x0 * source.M1x3);

            var m10m32m30m12 = (source.M1x0 * source.M3x2) - (source.M3x0 * source.M1x2);
            var m10m23m20m13 = (source.M1x0 * source.M2x3) - (source.M2x0 * source.M1x3);
            var m10m22m20m12 = (source.M1x0 * source.M2x2) - (source.M2x0 * source.M1x2);
            var m20m31m30m21 = (source.M2x0 * source.M3x1) - (source.M3x0 * source.M2x1);

            var m10m31m30m11 = (source.M1x0 * source.M3x1) - (source.M3x0 * source.M1x1);
            var m10m21m20m11 = (source.M1x0 * source.M2x1) - (source.M2x0 * source.M1x1);

            return new Matrix4x4D(
                -((source.M1x1 * m22m33m32m23) - (source.M1x2 * m21m33m31m23) + (source.M1x3 * m21m32m31m22)),
                (source.M0x1 * m22m33m32m23) - (source.M0x2 * m21m33m31m23) + (source.M0x3 * m21m32m31m22),
                -((source.M0x1 * m12m33m32m13) - (source.M0x2 * m11m33m31m13) + (source.M0x3 * m11m32m31m12)),
                (source.M0x1 * m12m23m22m13) - (source.M0x2 * m11m23m21m13) + (source.M0x3 * m11m22m21m12),
                (source.M1x0 * m22m33m32m23) - (source.M1x2 * m20m33m30m23) + (source.M1x3 * m20m32m30m22),
                -((source.M0x0 * m22m33m32m23) - (source.M0x2 * m20m33m30m23) + (source.M0x3 * m20m32m30m22)),
                (source.M0x0 * m12m33m32m13) - (source.M0x2 * m10m33m30m13) + (source.M0x3 * m10m32m30m12),
                -((source.M0x0 * m12m23m22m13) - (source.M0x2 * m10m23m20m13) + (source.M0x3 * m10m22m20m12)),
                -((source.M1x0 * m21m33m31m23) - (source.M1x1 * m20m33m30m23) + (source.M1x3 * m20m31m30m21)),
                (source.M0x0 * m21m33m31m23) - (source.M0x1 * m20m33m30m23) + (source.M0x3 * m20m31m30m21),
                -((source.M0x0 * m11m33m31m13) - (source.M0x1 * m10m33m30m13) + (source.M0x3 * m20m31m30m21)),
                (source.M0x0 * m11m23m21m13) - (source.M0x1 * m10m23m20m13) + (source.M0x3 * m10m21m20m11),
                (source.M1x0 * m21m32m31m22) - (source.M1x1 * m20m32m30m22) + (source.M1x2 * m20m31m30m21),
                -((source.M0x0 * m21m32m31m22) - (source.M0x1 * m20m32m30m22) + (source.M0x2 * m20m31m30m21)),
                (source.M0x0 * m11m32m31m12) - (source.M0x1 * m10m32m30m12) + (source.M0x2 * m10m31m30m11),
                -((source.M0x0 * m11m22m21m12) - (source.M0x1 * m10m22m20m12) + (source.M0x2 * m10m21m20m11)));

        }
        #endregion Cofactor

        #region Concatenate
        /// <summary>
        /// The concatenate.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Concatenate(this Quaternion4D a, Quaternion4D b)
            => new Quaternion4D(
                (b.X * a.W) + (a.X * b.W) + ((b.Y * a.Z) - (b.Z * a.Y)),
                (b.Y * a.W) + (a.Y * b.W) + ((b.Z * a.X) - (b.X * a.Z)),
                (b.Z * a.W) + (a.Z * b.W) + ((b.X * a.Y) - (b.Y * a.X)),
                (b.W * a.W) - ((b.X * a.X) + (b.Y * a.Y) + (b.Z * a.Z)));
        #endregion Concatenate

        #region Conjugate
        /// <summary>
        /// The conjugate.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Conjugate(this Quaternion4D value)
            => new Quaternion4D(-value.X, -value.Y, -value.Z, value.W);
        #endregion Conjugate

        #region Convert
        /// <summary>
        /// Gets a 3x3 rotation matrix from this Quaternion.
        /// </summary>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D ToRotationMatrix(this Quaternion4D quaternion)
            => new Matrix3x3D(
                1d - (2d * ((quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z))),
                (2d * quaternion.Y * quaternion.X) - (2d * quaternion.Z * quaternion.W),
                (2d * quaternion.Z * quaternion.X) + (2d * quaternion.Y * quaternion.W),
                (2d * quaternion.Y * quaternion.X) + (2d * quaternion.Z * quaternion.W),
                1d - (2d * ((quaternion.X * quaternion.X) + (quaternion.Z * quaternion.Z))),
                (2d * quaternion.Z * quaternion.Y) - (2d * quaternion.X * quaternion.W),
                (2d * quaternion.Z * quaternion.X) - (2d * quaternion.Y * quaternion.W),
                (2d * quaternion.Z * quaternion.Y) + (2d * quaternion.X * quaternion.W),
                1d - (2d * ((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y))));

        /// <summary>
        /// Gets a 4x4 matrix from this Quaternion.
        /// </summary>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        /// <remarks>
        /// source -> http://content.gpwiki.org/index.php/OpenGL:Tutorials:Using_Quaternions_to_represent_rotation#Quaternion_to_Matrix
        /// </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D ToMatrix(this Quaternion4D quaternion)
            => new Matrix4x4D(
                1d - (2d * ((quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z))),
                2d * ((quaternion.X * quaternion.Y) - (quaternion.W * quaternion.Z)),
                2d * ((quaternion.X * quaternion.Z) + (quaternion.W * quaternion.Y)),
                0d,
                2d * ((quaternion.X * quaternion.Y) + (quaternion.W * quaternion.Z)),
                1d - (2d * ((quaternion.X * quaternion.X) + (quaternion.Z * quaternion.Z))),
                2d * ((quaternion.Y * quaternion.Z) - (quaternion.W * quaternion.X)),
                0d,
                2d * ((quaternion.X * quaternion.Z) - (quaternion.W * quaternion.Y)),
                2d * ((quaternion.Y * quaternion.Z) + (quaternion.W * quaternion.X)),
                1d - (2d * ((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y))),
                0d,
                2d * ((quaternion.X * quaternion.Z) - (quaternion.W * quaternion.Y)),
                2d * ((quaternion.Y * quaternion.Z) + (quaternion.W * quaternion.X)),
                1d - (2d * ((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y))),
                0d);

        /// <summary>
        /// The to axis.
        /// </summary>
        /// <param name="quaternion">The quaternion.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector3D XAxis, Vector3D YAxis, Vector3D ZAxis) ToAxis(this Quaternion4D quaternion)
        {
            var rotation = ToRotationMatrix(quaternion);
            return (rotation.Cx, rotation.Cy, rotation.Cz);
        }

        /// <summary>
        /// The quaternion representing the rotation is
        /// q = cos(A/2)+sin(A/2)*(X*i+Y*j+Z*k)
        /// </summary>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double Angle, Vector3D Axis) ToAngleAxis(this Quaternion4D quaternion)
        {
            var sqrLength = (quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z);
            if (sqrLength == 0d)
            {
                return (0d, new Vector3D(1d, 0d, 0d));
            }

            var inverseLength = InverseSqrt(sqrLength);
            return (2d * Acos(quaternion.W), new Vector3D(quaternion.X * inverseLength, quaternion.Y * inverseLength, quaternion.Z * inverseLength));
        }

        /// <summary>
        /// The to Euler angles.
        /// </summary>
        /// <param name="quaternion">The quaternion.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        public static Orientation3D ToEulerAngles(this Quaternion4D quaternion)
            => QuaternionToEulerAngles(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
        #endregion Convert

        #region Cross Product
        /// <summary>
        /// Cross Product a Perpendicular dot product of two vectors.
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="valueA">Starting Point</param>
        /// <param name="valueB">Ending Point</param>
        /// <returns>
        /// Return the cross product AB x BC.=((a)->x*(b)->y-(a)->y*(b)->x)
        /// </returns>
        /// <remarks>Graphics Gems IV, page 139.</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct(this Point2D valueA, Point2D valueB)
            => Maths.CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);

        ///// <summary>
        ///// Cross Product a Perpendicular dot product of two vectors.
        ///// The cross product is a vector perpendicular to AB
        ///// and BC having length |AB| * |BC| * Sin(theta) and
        ///// with direction given by the right-hand rule.
        ///// For two vectors in the X-Y plane, the result is a
        ///// vector with X and Y components 0 so the Z component
        ///// gives the vector's length and direction.
        ///// </summary>
        ///// <param name="valueA"></param>
        ///// <param name="valueB"></param>
        ///// <returns>
        ///// Return the cross product AB x BC.=((a)->x*(b)->y-(a)->y*(b)->x)
        ///// </returns>
        ///// <remarks>Graphics Gems IV, page 139.</remarks>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static double CrossProduct(this Vector2D valueA, Point valueB)
        //    => Maths.CrossProduct(valueA.I, valueA.J, valueB.X, valueB.Y);

        ///// <summary>
        ///// Cross Product a Perpendicular dot product of two vectors.
        ///// The cross product is a vector perpendicular to AB
        ///// and BC having length |AB| * |BC| * Sin(theta) and
        ///// with direction given by the right-hand rule.
        ///// For two vectors in the X-Y plane, the result is a
        ///// vector with X and Y components 0 so the Z component
        ///// gives the vector's length and direction.
        ///// </summary>
        ///// <param name="valueA"></param>
        ///// <param name="valueB"></param>
        ///// <returns>
        ///// Return the cross product AB x BC.=((a)->x*(b)->y-(a)->y*(b)->x)
        ///// </returns>
        ///// <remarks>Graphics Gems IV, page 139.</remarks>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static double CrossProduct(this Vector2D valueA, PointF valueB)
        //    => Maths.CrossProduct(valueA.I, valueA.J, valueB.X, valueB.Y);

        /// <summary>
        /// Cross Product a Perpendicular dot product of two vectors.
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="valueA"></param>
        /// <param name="valueB"></param>
        /// <returns>
        /// Return the cross product AB x BC.=((a)->x*(b)->y-(a)->y*(b)->x)
        /// </returns>
        /// <remarks>Graphics Gems IV, page 139.</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct(this Vector2D valueA, Vector2D valueB)
            => Maths.CrossProduct(valueA.I, valueA.J, valueB.I, valueB.J);
        #endregion Cross Product

        #region Delta
        /// <summary>
        /// Finds the Delta of two Points
        /// </summary>
        /// <param name="value1">First Point</param>
        /// <param name="value2">Second Point</param>
        /// <returns>Returns the Difference Between PointA and PointB</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Delta(this Point2D value1, Point2D value2)
            => Maths.Delta(value1.X, value1.Y, value2.X, value2.Y);

        /// <summary>
        /// Finds the Delta of two Sizes
        /// </summary>
        /// <param name="size">First Point</param>
        /// <param name="value">Second Point</param>
        /// <returns>Returns the Difference Between PointA and PointB</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Delta(this Size2D size, Size2D value)
            => Maths.Delta(size.Width, size.Height, value.Width, value.Height);

        /// <summary>
        /// Finds the Delta of two Vectors
        /// </summary>
        /// <param name="vector">First Point</param>
        /// <param name="value">Second Point</param>
        /// <returns>Returns the Difference Between PointA and PointB</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Delta(this Vector2D vector, Vector2D value)
            => Maths.Delta(vector.I, vector.J, value.I, value.J);
        #endregion Delta

        #region Determinant
        /// <summary>
        /// The determinant.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(this Matrix2x2D source)
            => Maths.Determinant(
                source.M0x0, source.M0x1,
                source.M1x0, source.M1x1);

        /// <summary>
        /// The determinant.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(this Matrix3x3D source)
            => Maths.Determinant(
                source.M0x0, source.M0x1, source.M0x2,
                source.M1x0, source.M1x1, source.M1x2,
                source.M2x0, source.M2x1, source.M2x2);

        /// <summary>
        /// The determinant.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(this Matrix4x4D source)
            => Maths.Determinant(
                source.M0x0, source.M0x1, source.M0x2, source.M0x3,
                source.M1x0, source.M1x1, source.M1x2, source.M1x3,
                source.M2x0, source.M2x1, source.M2x2, source.M2x3,
                source.M3x0, source.M3x1, source.M3x2, source.M3x3);
        #endregion Determinant

        #region Divide
        /// <summary>
        /// The divide.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Divide(this Point2D divisor, double dividend)
            => Divide2D1D(divisor.X, divisor.Y, dividend);

        /// <summary>
        /// The divide.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Divide(this Size2D divisor, double dividend)
            => Divide2D1D(divisor.Width, divisor.Height, dividend);

        /// <summary>
        /// The divide.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns>The <see cref="Vector2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Divide(this Vector2D divisor, double dividend)
            => Divide2D1D(divisor.I, divisor.J, dividend);

        /// <summary>
        /// The divide.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns>The <see cref="Vector3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Divide(this Vector3D divisor, double dividend)
            => Divide3D1D(divisor.I, divisor.J, divisor.K, dividend);

        /// <summary>
        /// The divide.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns>The <see cref="Vector4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Divide(this Vector4D divisor, double dividend)
            => Divide4D1D(divisor.I, divisor.J, divisor.K, divisor.L, dividend);

        /// <summary>
        /// The divide.
        /// </summary>
        /// <param name="quaternion1">The quaternion1.</param>
        /// <param name="quaternion2">The quaternion2.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Divide(this Quaternion4D quaternion1, Quaternion4D quaternion2)
        {
            var x = quaternion1.X;
            var y = quaternion1.Y;
            var z = quaternion1.Z;
            var w = quaternion1.W;
            var num14 = (quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y) + (quaternion2.Z * quaternion2.Z) + (quaternion2.W * quaternion2.W);
            var num5 = 1f / num14;
            var num4 = -quaternion2.X * num5;
            var num3 = -quaternion2.Y * num5;
            var num2 = -quaternion2.Z * num5;
            var num = quaternion2.W * num5;
            var num13 = (y * num2) - (z * num3);
            var num12 = (z * num4) - (x * num2);
            var num11 = (x * num3) - (y * num4);
            var num10 = (x * num4) + (y * num3) + (z * num2);
            return new Quaternion4D(
                (x * num) + (num4 * w) + num13,
                (y * num) + (num3 * w) + num12,
                (z * num) + (num2 * w) + num11,
                (w * num) - num10);
        }
        #endregion Divide

        #region Dot Product
        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="value">Starting Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Point2D value)
            => Maths.DotProduct(value.X, value.Y, value.X, value.Y);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="value">Starting Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Vector2D value)
            => Maths.DotProduct(value.I, value.J, value.I, value.J);

        /// <summary>
        /// Finds the Dot Product (scalar or inner product) of two Points.
        /// </summary>
        /// <param name="point">Starting Point</param>
        /// <param name="value">Ending Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>
        /// The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Point2D point, Point2D value)
            => Maths.DotProduct(point.X, point.Y, value.X, value.Y);

        /// <summary>
        /// Finds the Dot Product (scalar or inner product) of two Points.
        /// </summary>
        /// <param name="point">Starting Point</param>
        /// <param name="value">Ending Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>
        /// The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Point2D point, Vector2D value)
            => Maths.DotProduct(point.X, point.Y, value.I, value.J);

        ///// <summary>
        ///// Determines the dot product of two 2D vectors
        ///// </summary>
        ///// <param name="vector">First Point</param>
        ///// <param name="value">Second Point</param>
        ///// <returns>Dot Product</returns>
        ///// <remarks>
        ///// The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2
        ///// </remarks>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static double DotProduct(this Vector2D vector, Point value)
        //    => Maths.DotProduct(vector.I, vector.J, value.X, value.Y);

        ///// <summary>
        ///// Determines the dot product of two 2D vectors
        ///// </summary>
        ///// <param name="vector">First Point</param>
        ///// <param name="value">Second Point</param>
        ///// <returns>Dot Product</returns>
        ///// <remarks>
        ///// The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2
        ///// </remarks>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static double DotProduct(this Vector2D vector, PointF value)
        //    => Maths.DotProduct(vector.I, vector.J, value.X, value.Y);

        /// <summary>
        /// Determines the dot product of two 2D vectors
        /// </summary>
        /// <param name="vector">First Point</param>
        /// <param name="value">Second Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>
        /// The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Vector2D vector, Vector2D value)
            => Maths.DotProduct(vector.I, vector.J, value.I, value.J);

        /// <summary>
        /// The dot product.
        /// </summary>
        /// <param name="quaternion1">The quaternion1.</param>
        /// <param name="quaternion2">The quaternion2.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Quaternion4D quaternion1, Quaternion4D quaternion2)
            => (quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y) + (quaternion1.Z * quaternion2.Z) + (quaternion1.W * quaternion2.W);
        #endregion Dot Product

        #region Exponent
        /// <summary>
        /// Calculates the Exponent of a Quaternion.
        /// </summary>
        /// <param name="source">ToDo: describe source parameter on Exponent</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Exponent(Quaternion4D source)
        {
            // If q = A*(X*i+Y*j+Z*k) Where (X,Y,Z) is unit length, then
            // eXp(q) = cos(A)+sin(A)*(X*i+Y*j+Z*k).  If sin(A) is near Zero,
            // use eXp(q) = cos(A)+A*(X*i+Y*j+Z*k) since A/sin(A) has limit 1.

            var angle = Sqrt((source.X * source.X) + (source.Y * source.Y) + (source.Z * source.Z));
            var sin = Sin(angle);

            // start off With a Zero Quaternion
            var returnvalue = Quaternion4D.Empty;

            returnvalue.W = Cos(angle);

            if (Abs(sin) >= Epsilon)
            {
                var coeff = sin / angle;

                returnvalue.X = coeff * source.X;
                returnvalue.Y = coeff * source.Y;
                returnvalue.Z = coeff * source.Z;
            }
            else
            {
                returnvalue.X = source.X;
                returnvalue.Y = source.Y;
                returnvalue.Z = source.Z;
            }

            return returnvalue;
        }
        #endregion Exponent

        #region Equals
        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(this Point2D point1, Point2D point2)
            => point1.X == point2.X && point1.Y == point2.Y;

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(this Vector2D point1, Vector2D point2)
            => point1.I == point2.I && point1.J == point2.J;
        #endregion Equals

        #region Greater Than
        /// <summary>
        /// The greater than.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan(this Point2D point1, Point2D point2)
            => point1.X > point2.X && point1.Y > point2.Y;

        /// <summary>
        /// The greater than.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan(this Vector2D point1, Vector2D point2)
            => point1.I > point2.I && point1.J > point2.J;
        #endregion Greater Than

        #region Greater Than or Equal To
        /// <summary>
        /// The greater than or equal.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrEqual(this (double X, double Y) point1, (double X, double Y) point2)
            => point1.X >= point2.X && point1.Y >= point2.Y;

        /// <summary>
        /// The greater than or equal.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrEqual(this Point2D point1, Point2D point2)
            => point1.X >= point2.X && point1.Y >= point2.Y;

        /// <summary>
        /// The greater than o equal.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOEqual(this Vector2D point1, Vector2D point2)
            => point1.I >= point2.I && point1.J >= point2.J;
        #endregion Greater Than or Equal To

        #region Inflate
        /// <summary>
        /// Inflates a <see cref="Point2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point2D"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Inflate(this Point2D point, Vector2D factors)
            => new Point2D(point.X * factors.I, point.Y * factors.J);

        /// <summary>
        /// Inflates a <see cref="Size2D"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size2D"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Inflate(this Size2D size, double factor)
            => new Size2D(size.Width * factor, size.Height * factor);

        /// <summary>
        /// Inflates a <see cref="Size2D"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size2D"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Inflate(this Size2D size, Size2D factor)
            => new Size2D(size.Width * factor.Width, size.Height * factor.Height);

        /// <summary>
        /// Inflates a <see cref="Size2D"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size2D"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="Vector2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Inflate(this Size2D size, Vector2D factor)
            => new Size2D(size.Width * factor.I, size.Height * factor.J);

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, int factor)
            => new Vector2D(point.I * factor, point.J * factor);

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, float factor)
            => new Vector2D(point.I * factor, point.J * factor);

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, double factor)
            => new Vector2D(point.I * factor, point.J * factor);

        ///// <summary>
        ///// Inflates a <see cref="Vector2D"/> by a given factor.
        ///// </summary>
        ///// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        ///// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        ///// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector2D Inflate(this Vector2D point, Point factors)
        //    => new Vector2D(point.I * factors.X, point.J * factors.Y);

        ///// <summary>
        ///// Inflates a <see cref="Vector2D"/> by a given factor.
        ///// </summary>
        ///// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        ///// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        ///// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector2D Inflate(this Vector2D point, PointF factors)
        //    => new Vector2D(point.I * factors.X, point.J * factors.Y);

        ///// <summary>
        ///// Inflates a <see cref="Vector2D"/> by a given factor.
        ///// </summary>
        ///// <param name="size">The <see cref="Vector2D"/> to inflate.</param>
        ///// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        ///// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector2D Inflate(this Vector2D size, Size factor)
        //    => new Vector2D(size.I * factor.Width, size.J * factor.Height);

        ///// <summary>
        ///// Inflates a <see cref="Vector2D"/> by a given factor.
        ///// </summary>
        ///// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        ///// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        ///// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector2D Inflate(this Vector2D point, SizeF factor)
        //    => new Vector2D(point.I * factor.Width, point.J * factor.Height);

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, Vector2D factors)
            => new Vector2D(point.I * factors.I, point.J * factors.J);
        #endregion Inflate

        #region Invert
        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Invert(float x, float y)
            => new Vector2D(1 / x, 1 / y);

        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Invert(double x, double y)
            => new Vector2D(1d / x, 1d / y);

        ///// <summary>
        ///// Inverts a Vector.
        ///// </summary>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector2D Invert(this PointF value)
        //    => Invert(value.X, value.Y);

        /// <summary>
        /// Inverts a Vector
        /// </summary>
        /// <param name="value">ToDo: describe value parameter on Invert</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Invert(this Vector2D value)
            => Invert(value.I, value.J);

        /// <summary>
        /// The invert.
        /// </summary>
        /// <param name="quaternion">The quaternion.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Invert(Quaternion4D quaternion)
        {
            var normal = (quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z) + (quaternion.W * quaternion.W);
            if (normal == 0d)
            {
                return Quaternion4D.Empty;
            }

            var inverseNormal = 1f / normal;
            return new Quaternion4D(
                -quaternion.X * inverseNormal,
                -quaternion.Y * inverseNormal,
                -quaternion.Z * inverseNormal,
                quaternion.W * inverseNormal);
        }

        /// <summary>
        /// The invert.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="Matrix2x2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Invert(this Matrix2x2D source)
        {
            var m11 = source.M1x1;
            var detInv = 1 / ((source.M0x0 * m11) - (source.M0x1 * source.M1x0));
            return new Matrix2x2D(
                detInv * m11,
                detInv * -source.M0x1,
                detInv * -source.M1x0,
                detInv * source.M0x0);
        }

        /// <summary>
        /// The invert.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Invert(this Matrix3x3D source)
        {
            var m11m22m12m21 = (source.M1x1 * source.M2x2) - (source.M1x2 * source.M2x1);
            var m10m22m12m20 = (source.M1x0 * source.M2x2) - (source.M1x2 * source.M2x0);
            var m10m21m11m20 = (source.M1x0 * source.M2x1) - (source.M1x1 * source.M2x0);
            var detInv = 1 / ((source.M0x0 * m11m22m12m21) - (source.M0x1 * m10m22m12m20) + (source.M0x2 * m10m21m11m20));
            return new Matrix3x3D(
                detInv * m11m22m12m21,
                detInv * (-((source.M0x1 * source.M2x2) - (source.M0x2 * source.M2x1))),
                detInv * ((source.M0x1 * source.M1x2) - (source.M0x2 * source.M1x1)),
                detInv * (-m10m22m12m20),
                detInv * ((source.M0x0 * source.M2x2) - (source.M0x2 * source.M2x0)),
                detInv * (-((source.M0x0 * source.M1x2) - (source.M0x2 * source.M1x0))),
                detInv * m10m21m11m20,
                detInv * (-((source.M0x0 * source.M2x1) - (source.M0x1 * source.M2x0))),
                detInv * ((source.M0x0 * source.M1x1) - (source.M0x1 * source.M1x0)));
        }

        /// <summary>
        /// The invert.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="Matrix4x4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Invert(this Matrix4x4D source)
        {
            var m22m33m32m23 = (source.M2x2 * source.M3x3) - (source.M3x2 * source.M2x3);
            var m21m33m31m23 = (source.M2x1 * source.M3x3) - (source.M3x1 * source.M2x3);
            var m21m32m31m22 = (source.M2x1 * source.M3x2) - (source.M3x1 * source.M2x2);

            var m12m33m32m13 = (source.M1x2 * source.M3x3) - (source.M3x2 * source.M1x3);
            var m11m33m31m13 = (source.M1x1 * source.M3x3) - (source.M3x1 * source.M1x3);
            var m11m32m31m12 = (source.M1x1 * source.M3x2) - (source.M3x1 * source.M1x2);

            var m12m23m22m13 = (source.M1x2 * source.M2x3) - (source.M2x2 * source.M1x3);
            var m11m23m21m13 = (source.M1x1 * source.M2x3) - (source.M2x1 * source.M1x3);
            var m11m22m21m12 = (source.M1x1 * source.M2x2) - (source.M2x1 * source.M1x2);

            var m20m33m30m23 = (source.M2x0 * source.M3x3) - (source.M3x0 * source.M2x3);
            var m20m32m30m22 = (source.M2x0 * source.M3x2) - (source.M3x0 * source.M2x2);
            var m10m33m30m13 = (source.M1x0 * source.M3x3) - (source.M3x0 * source.M1x3);

            var m10m32m30m12 = (source.M1x0 * source.M3x2) - (source.M3x0 * source.M1x2);
            var m10m23m20m13 = (source.M1x0 * source.M2x3) - (source.M2x0 * source.M1x3);
            var m10m22m20m12 = (source.M1x0 * source.M2x2) - (source.M2x0 * source.M1x2);

            var m20m31m30m21 = (source.M2x0 * source.M3x1) - (source.M3x0 * source.M2x1);
            var m10m31m30m11 = (source.M1x0 * source.M3x1) - (source.M3x0 * source.M1x1);
            var m10m21m20m11 = (source.M1x0 * source.M2x1) - (source.M2x0 * source.M1x1);

            var detInv = 1 /
            ((source.M0x0 * ((source.M1x1 * m22m33m32m23) - (source.M1x2 * m21m33m31m23) + (source.M1x3 * m21m32m31m22))) -
            (source.M0x1 * ((source.M1x0 * m22m33m32m23) - (source.M1x2 * m20m33m30m23) + (source.M1x3 * m20m32m30m22))) +
            (source.M0x2 * ((source.M1x0 * m21m33m31m23) - (source.M1x1 * m20m33m30m23) + (source.M1x3 * m20m31m30m21))) -
            (source.M0x3 * ((source.M1x0 * m21m32m31m22) - (source.M1x1 * m20m32m30m22) + (source.M1x2 * m20m31m30m21))));

            return new Matrix4x4D(
                detInv * ((source.M1x1 * m22m33m32m23) - (source.M1x2 * m21m33m31m23) + (source.M1x3 * m21m32m31m22)),
                detInv * (-((source.M0x1 * m22m33m32m23) - (source.M0x2 * m21m33m31m23) + (source.M0x3 * m21m32m31m22))),
                detInv * ((source.M0x1 * m12m33m32m13) - (source.M0x2 * m11m33m31m13) + (source.M0x3 * m11m32m31m12)),
                detInv * (-((source.M0x1 * m12m23m22m13) - (source.M0x2 * m11m23m21m13) + (source.M0x3 * m11m22m21m12))),
                detInv * (-((source.M1x0 * m22m33m32m23) - (source.M1x2 * m20m33m30m23) + (source.M1x3 * m20m32m30m22))),
                detInv * ((source.M0x0 * m22m33m32m23) - (source.M0x2 * m20m33m30m23) + (source.M0x3 * m20m32m30m22)),
                detInv * (-((source.M0x0 * m12m33m32m13) - (source.M0x2 * m10m33m30m13) + (source.M0x3 * m10m32m30m12))),
                detInv * ((source.M0x0 * m12m23m22m13) - (source.M0x2 * m10m23m20m13) + (source.M0x3 * m10m22m20m12)),
                detInv * ((source.M1x0 * m21m33m31m23) - (source.M1x1 * m20m33m30m23) + (source.M1x3 * m20m31m30m21)),
                detInv * (-((source.M0x0 * m21m33m31m23) - (source.M0x1 * m20m33m30m23) + (source.M0x3 * m20m31m30m21))),
                detInv * ((source.M0x0 * m11m33m31m13) - (source.M0x1 * m10m33m30m13) + (source.M0x3 * m20m31m30m21)),
                detInv * (-((source.M0x0 * m11m23m21m13) - (source.M0x1 * m10m23m20m13) + (source.M0x3 * m10m21m20m11))),
                detInv * (-((source.M1x0 * m21m32m31m22) - (source.M1x1 * m20m32m30m22) + (source.M1x2 * m20m31m30m21))),
                detInv * ((source.M0x0 * m21m32m31m22) - (source.M0x1 * m20m32m30m22) + (source.M0x2 * m20m31m30m21)),
                detInv * (-((source.M0x0 * m11m32m31m12) - (source.M0x1 * m10m32m30m12) + (source.M0x2 * m10m31m30m11))),
                detInv * ((source.M0x0 * m11m22m21m12) - (source.M0x1 * m10m22m20m12) + (source.M0x2 * m10m21m20m11)));
        }
        #endregion Invert

        #region Less Than
        /// <summary>
        /// The less than.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan(this Point2D point1, Point2D point2)
            => point1.X < point2.X && point1.Y < point2.Y;

        /// <summary>
        /// The less than.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan(this Vector2D point1, Vector2D point2)
            => point1.I < point2.I && point1.J < point2.J;
        #endregion Less Than

        #region Less Than or Equal To
        /// <summary>
        /// The less than or equal.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrEqual(this (double X, double Y) point1, (double X, double Y) point2)
            => point1.X <= point2.X && point1.Y <= point2.Y;

        /// <summary>
        /// The less than or equal.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrEqual(this Point2D point1, Point2D point2)
            => point1.X <= point2.X && point1.Y <= point2.Y;

        /// <summary>
        /// The less than or equal.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrEqual(this Vector2D point1, Vector2D point2)
            => point1.I <= point2.I && point1.J <= point2.J;
        #endregion Less Than or Equal To

        #region Linear Interpolation
        /// <summary>
        /// The lerp.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Lerp(this Point2D point1, Point2D point2, double t)
            => new Point2D(point1.X + ((point2.X - point1.X) * t), point1.Y + ((point2.Y - point1.Y) * t));

        /// <summary>
        /// The lerp.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Vector2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Lerp(this Vector2D point1, Vector2D point2, double t)
            => new Vector2D(point1.I + ((point2.I - point1.I) * t), point1.J + ((point2.J - point1.J) * t));

        /// <summary>
        /// The lerp.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Lerp(this Size2D point1, Size2D point2, double t)
            => new Size2D(point1.Width + ((point2.Width - point1.Width) * t), point1.Height + ((point2.Height - point1.Height) * t));

        /// <summary>
        /// The lerp.
        /// </summary>
        /// <param name="quaternion1">The quaternion1.</param>
        /// <param name="quaternion2">The quaternion2.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Lerp(this Quaternion4D quaternion1, Quaternion4D quaternion2, double amount)
        {
            var num = amount;
            var num2 = 1f - num;
            var quaternion = new Quaternion4D();
            var num5 = (quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y) + (quaternion1.Z * quaternion2.Z) + (quaternion1.W * quaternion2.W);
            if (num5 >= 0f)
            {
                quaternion.X = (num2 * quaternion1.X) + (num * quaternion2.X);
                quaternion.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
                quaternion.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
                quaternion.W = (num2 * quaternion1.W) + (num * quaternion2.W);
            }
            else
            {
                quaternion.X = (num2 * quaternion1.X) - (num * quaternion2.X);
                quaternion.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
                quaternion.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
                quaternion.W = (num2 * quaternion1.W) - (num * quaternion2.W);
            }
            var num4 = (quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z) + (quaternion.W * quaternion.W);
            var num3 = 1f / Sqrt(num4);
            quaternion.X *= num3;
            quaternion.Y *= num3;
            quaternion.Z *= num3;
            quaternion.W *= num3;
            return quaternion;
        }
        #endregion Linear Interpolation

        #region Log
        /// <summary>
        /// Calculates the logarithm of a Quaternion.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Log(this Quaternion4D source)
        {
            // BLACKBOX: Learn this
            // If q = cos(A)+sin(A)*(X*i+Y*j+Z*k) Where (X,Y,Z) is unit length, then
            // log(q) = A*(X*i+Y*j+Z*k).  If sin(A) is near Zero, use log(q) =
            // sin(A)*(X*i+Y*j+Z*k) since sin(A)/A has limit 1.

            // start off With a Zero Quaternion
            var returnvalue = Quaternion4D.Empty;

            if (Abs(source.W) < 1d)
            {
                var angle = Acos(source.W);
                var sin = Sin(angle);

                if (Abs(sin) >= Epsilon)
                {
                    var coeff = angle / sin;
                    returnvalue.X = coeff * source.X;
                    returnvalue.Y = coeff * source.Y;
                    returnvalue.Z = coeff * source.Z;
                }
                else
                {
                    returnvalue.X = source.X;
                    returnvalue.Y = source.Y;
                    returnvalue.Z = source.Z;
                }
            }

            return returnvalue;
        }
        #endregion Log

        #region Max
        /// <summary>
        /// The max.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Max(this Point2D point1, Point2D point2)
            => new Point2D(Math.Max(point1.X, point2.X), Math.Max(point1.Y, point2.Y));

        /// <summary>
        /// The max.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="Vector2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Max(this Vector2D point1, Vector2D point2)
            => new Vector2D(Math.Max(point1.I, point2.I), Math.Max(point1.J, point2.J));
        #endregion Max

        #region Min
        /// <summary>
        /// The min.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Min(this Point2D point1, Point2D point2)
            => new Point2D(Math.Min(point1.X, point2.X), Math.Min(point1.Y, point2.Y));

        /// <summary>
        /// The min.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>The <see cref="Vector2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Min(this Vector2D point1, Vector2D point2)
            => new Vector2D(Math.Min(point1.I, point2.I), Math.Min(point1.J, point2.J));
        #endregion Min

        #region Modulus
        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulus(this Vector2D value)
            => Maths.Modulus(value.I, value.J);
        #endregion Modulus

        #region Multiply
        /// <summary>
        /// Multiply: Point * Matrix
        /// </summary>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Multiply(this Point2D point, Matrix3x2D matrix)
            => matrix.Transform(point);

        /// <summary>
        /// Used to multiply (concatenate) two Matrix2x2s.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Multiply(this Matrix2x2D left, Matrix2x2D right)
            => new Matrix2x2D(
            (left.M0x0 * right.M0x0) + (left.M0x1 * right.M1x0),
            (left.M0x0 * right.M0x1) + (left.M0x1 * right.M1x1),
            (left.M1x0 * right.M0x0) + (left.M1x1 * right.M1x0),
            (left.M1x0 * right.M0x1) + (left.M1x1 * right.M1x1));

        /// <summary>
        /// Used to multiply (concatenate) two Matrix3x3s.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Multiply(this Matrix3x3D left, Matrix2x2D right)
            => new Matrix3x3D(
                (left.M0x0 * right.M0x0) + (left.M0x1 * right.M1x0),
                (left.M0x0 * right.M0x1) + (left.M0x1 * right.M1x1),
                left.M0x2,
                (left.M1x0 * right.M0x0) + (left.M1x1 * right.M1x0),
                (left.M1x0 * right.M0x1) + (left.M1x1 * right.M1x1),
                left.M1x2,
                (left.M2x0 * right.M0x0) + (left.M2x1 * right.M1x0),
                (left.M2x0 * right.M0x1) + (left.M2x1 * right.M1x1),
                left.M2x2);

        /// <summary>
        /// Used to multiply (concatenate) two Matrix3x3s.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Multiply(this Matrix2x2D left, Matrix3x3D right)
            => new Matrix3x3D(
                (left.M0x0 * right.M0x0) + (left.M0x1 * right.M1x0),
                (left.M0x0 * right.M0x1) + (left.M0x1 * right.M1x1),
                (left.M0x0 * right.M0x2) + (left.M0x1 * right.M1x2),
                (left.M1x0 * right.M0x0) + (left.M1x1 * right.M1x0),
                (left.M1x0 * right.M0x1) + (left.M1x1 * right.M1x1),
                (left.M1x0 * right.M0x2) + (left.M1x1 * right.M1x2),
                right.M2x0,
                right.M2x1,
                right.M2x2);

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix3x3D"/>s.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Multiply(this Matrix3x3D left, Matrix3x3D right)
            => new Matrix3x3D(
                (left.M0x0 * right.M0x0) + (left.M0x1 * right.M1x0) + (left.M0x2 * right.M2x0),
                (left.M0x0 * right.M0x1) + (left.M0x1 * right.M1x1) + (left.M0x2 * right.M2x1),
                (left.M0x0 * right.M0x2) + (left.M0x1 * right.M1x2) + (left.M0x2 * right.M2x2),
                (left.M1x0 * right.M0x0) + (left.M1x1 * right.M1x0) + (left.M1x2 * right.M2x0),
                (left.M1x0 * right.M0x1) + (left.M1x1 * right.M1x1) + (left.M1x2 * right.M2x1),
                (left.M1x0 * right.M0x2) + (left.M1x1 * right.M1x2) + (left.M1x2 * right.M2x2),
                (left.M2x0 * right.M0x0) + (left.M2x1 * right.M1x0) + (left.M2x2 * right.M2x0),
                (left.M2x0 * right.M0x1) + (left.M2x1 * right.M1x1) + (left.M2x2 * right.M2x1),
                (left.M2x0 * right.M0x2) + (left.M2x1 * right.M1x2) + (left.M2x2 * right.M2x2));

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D"/>s.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Multiply(this Matrix2x2D left, Matrix4x4D right)
            => Multiply2x2x4x4(
                left.M0x0, left.M0x1,
                left.M1x0, left.M1x1,
                right.M0x0, right.M0x1, right.M0x2, right.M0x3,
                right.M1x0, right.M1x1, right.M1x2, right.M1x3,
                right.M2x0, right.M2x1, right.M2x2, right.M2x3,
                right.M3x0, right.M3x1, right.M3x2, right.M3x3);

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D"/>s.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Multiply(this Matrix3x3D left, Matrix4x4D right)
            => Multiply3x3x4x4(
                left.M0x0, left.M0x1, left.M0x2,
                left.M1x0, left.M1x1, left.M1x2,
                left.M2x0, left.M2x1, left.M2x2,
                right.M0x0, right.M0x1, right.M0x2, right.M0x3,
                right.M1x0, right.M1x1, right.M1x2, right.M1x3,
                right.M2x0, right.M2x1, right.M2x2, right.M2x3,
                right.M3x0, right.M3x1, right.M3x2, right.M3x3);

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D"/>s.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Multiply(this Matrix4x4D left, Matrix2x2D right)
            => Multiply4x4x2x2(
                left.M0x0, left.M0x1, left.M0x2, left.M0x3,
                left.M1x0, left.M1x1, left.M1x2, left.M1x3,
                left.M2x0, left.M2x1, left.M2x2, left.M2x3,
                left.M3x0, left.M3x1, left.M3x2, left.M3x3,
                right.M0x0, right.M0x1,
                right.M1x0, right.M1x1);

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D"/>s.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Multiply(this Matrix4x4D left, Matrix3x3D right)
            => Multiply4x4x3x3(
                left.M0x0, left.M0x1, left.M0x2, left.M0x3,
                left.M1x0, left.M1x1, left.M1x2, left.M1x3,
                left.M2x0, left.M2x1, left.M2x2, left.M2x3,
                left.M3x0, left.M3x1, left.M3x2, left.M3x3,
                right.M0x0, right.M0x1, right.M0x2,
                right.M1x0, right.M1x1, right.M1x2,
                right.M2x0, right.M2x1, right.M2x2);

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D"/>s.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Multiply(this Matrix4x4D left, Matrix4x4D right)
            => Multiply4x4x4x4(
                left.M0x0, left.M0x1, left.M0x2, left.M0x3,
                left.M1x0, left.M1x1, left.M1x2, left.M1x3,
                left.M2x0, left.M2x1, left.M2x2, left.M2x3,
                left.M3x0, left.M3x1, left.M3x2, left.M3x3,
                right.M0x0, right.M0x1, right.M0x2, right.M0x3,
                right.M1x0, right.M1x1, right.M1x2, right.M1x3,
                right.M2x0, right.M2x1, right.M2x2, right.M2x3,
                right.M3x0, right.M3x1, right.M3x2, right.M3x3);

        /// <summary>
        /// The multiply.
        /// </summary>
        /// <param name="quaternion1">The quaternion1.</param>
        /// <param name="quaternion2">The quaternion2.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Multiply(this Quaternion4D quaternion1, Quaternion4D quaternion2)
        {
            var x = quaternion1.X;
            var y = quaternion1.Y;
            var z = quaternion1.Z;
            var w = quaternion1.W;
            var num4 = quaternion2.X;
            var num3 = quaternion2.Y;
            var num2 = quaternion2.Z;
            var num = quaternion2.W;
            var num12 = (y * num2) - (z * num3);
            var num11 = (z * num4) - (x * num2);
            var num10 = (x * num3) - (y * num4);
            var num9 = (x * num4) + (y * num3) + (z * num2);
            return new Quaternion4D(
                (x * num) + (num4 * w) + num12,
                (y * num) + (num3 * w) + num11,
                (z * num) + (num2 * w) + num10,
                (w * num) - num9);
        }
        #endregion Multiply

        #region Negate
        /// <summary>
        ///	Negates a <see cref="Matrix2x2D"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Negate(this Matrix2x2D source)
            => Maths.Negate(
                source.M0x0, source.M0x1,
                source.M1x0, source.M1x1);

        /// <summary>
        ///	Negates a <see cref="Matrix3x3D"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Negate(this Matrix3x3D source)
            => Maths.Negate(
                source.M0x0, source.M0x1, source.M0x2,
                source.M1x0, source.M1x1, source.M1x2,
                source.M2x0, source.M2x1, source.M2x2);

        /// <summary>
        ///	Negates a <see cref="Matrix4x4D"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Negate(this Matrix4x4D source)
            => Maths.Negate(
                source.M0x0, source.M0x1, source.M0x2, source.M0x3,
                source.M1x0, source.M1x1, source.M1x2, source.M1x3,
                source.M2x0, source.M2x1, source.M2x2, source.M2x3,
                source.M3x0, source.M3x1, source.M3x2, source.M3x3);

        /// <summary>
        /// The negate.
        /// </summary>
        /// <param name="quaternion">The quaternion.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Negate(Quaternion4D quaternion)
            => new Quaternion4D(
                -quaternion.X,
                -quaternion.Y,
                -quaternion.Z,
                -quaternion.W);
        #endregion Negate

        #region Normalize
        /// <summary>
        /// Normalize Two Points
        /// </summary>
        /// <param name="point">First Point</param>
        /// <param name="value">Second Point</param>
        /// <returns>The Normal of two Points</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Normalize(this Point2D point, Size2D value)
            => Normalize2D(point.X, point.Y, value.Width, value.Height);

        /// <summary>
        /// This returns the Normalized Vector2D that is passed. This is also known as a Unit Vector.
        /// </summary>
        /// <param name="source">The Vector3D to be Normalized.</param>
        /// <returns>The Normalized Vector2D. (Unit Vector)</returns>
        /// <remarks><seealso href="http://en.wikipedia.org/wiki/Vector_%28spatial%29#Unit_vector"/></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Normalize(this Vector2D source)
            => Normalize2D(source.I, source.J);

        /// <summary>
        /// This returns the Normalized Vector3D that is passed. This is also known as a Unit Vector.
        /// </summary>
        /// <param name="source">The Vector3D to be Normalized.</param>
        /// <returns>The Normalized Vector3D. (Unit Vector)</returns>
        /// <remarks><seealso href="http://en.wikipedia.org/wiki/Vector_%28spatial%29#Unit_vector"/></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Normalize(this Vector3D source)
            => Normalize3D(source.I, source.J, source.K);

        /// <summary>
        /// This returns the Normalized Vector3D that is passed. This is also known as a Unit Vector.
        /// </summary>
        /// <param name="source">The Vector3D to be Normalized.</param>
        /// <returns>The Normalized Vector3D. (Unit Vector)</returns>
        /// <remarks><seealso href="http://en.wikipedia.org/wiki/Vector_%28spatial%29#Unit_vector"/></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Normalize(this Vector4D source)
            => Normalize4D(source.I, source.J, source.K, source.L);

        /// <summary>
        /// The normalize.
        /// </summary>
        /// <param name="quaternion">The quaternion.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Normalize(this Quaternion4D quaternion)
        {
            var num2 = (quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z) + (quaternion.W * quaternion.W);
            var num = 1f / Sqrt(num2);
            return new Quaternion4D(
                quaternion.X * num,
                quaternion.Y * num,
                quaternion.Z * num,
                quaternion.W * num);
        }
        #endregion Normalize

        #region Perpendicular Vector
        /// <summary>
        /// Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Perpendicular(float i, float j)
            => PerpendicularClockwise(i, j);

        /// <summary>
        /// Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Perpendicular(double i, double j)
            => PerpendicularClockwise(i, j);

        /// <summary>
        /// Perpendicular of a Vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Perpendicular(this Vector2D vector)
            => PerpendicularClockwise(vector.I, vector.J);
        #endregion Perpendicular Vector

        #region Reflect
        /// <summary>
        /// Calculates the reflection of a point off a line segment
        /// </summary>
        /// <param name="point">First point of line segment</param>
        /// <param name="value">Second point of line segment</param>
        /// <param name="axis">Point to Reflect</param>
        /// <returns></returns>

        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Reflect(this Point2D point, Point2D value, Point2D axis)
            //{
            //    Vector2D SegmentVectorDelta = point.Delta(value);
            //    var QC12 = new Vector2D(
            //        value.CrossProduct(point),
            //        axis.DotProduct((Point2D)SegmentVectorDelta)
            //        );
            //    double QC3 = 0.5F * SegmentVectorDelta.DotProduct(SegmentVectorDelta);
            //    return new Point2D(
            //        (float)(QC3 * SegmentVectorDelta.CrossProduct(QC12) - axis.X),
            //        (float)(QC3 * SegmentVectorDelta.CrossProduct(QC12) - axis.Y)
            //        );
            //}
            => Maths.Reflect(point.X, point.Y, value.X, value.Y, axis.X, axis.Y);

        /// <summary>
        /// Calculates the reflection of a point off a line segment
        /// </summary>
        /// <param name="segment">The line segment</param>
        /// <param name="axis">Point to reflect about</param>
        /// <returns></returns>

        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Reflect(this LineSegment segment, Point2D axis)
            //{
            //    Vector2D SegmentVectorDelta = segment.A.Delta(segment.B);
            //    var QC12 = new Vector2D(
            //        segment.B.CrossProduct(segment.A),
            //        axis.DotProduct((Point2D)SegmentVectorDelta)
            //        );
            //    double QC3 = 0.5F * SegmentVectorDelta.DotProduct(SegmentVectorDelta);
            //    return new Point2D(
            //        (float)(QC3 * SegmentVectorDelta.CrossProduct(QC12) - axis.X),
            //        (float)(QC3 * SegmentVectorDelta.CrossProduct(QC12) - axis.Y)
            //        );
            //}
            => Maths.Reflect(segment.AX, segment.AY, segment.BX, segment.BY, axis.X, axis.Y);
        #endregion Reflect

        #region Remove At
        /// <summary>
        /// Remove the at.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="T:double[]"/>.</returns>
        public static double[] RemoveAt(this double[] array, int index)
        {
            ArrayUtilities.RemoveAt(ref array, index);
            return array;
        }

        /// <summary>
        /// Remove the at.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="T:Point2D[]"/>.</returns>
        public static Point2D[] RemoveAt(this Point2D[] array, int index)
        {
            ArrayUtilities.RemoveAt(ref array, index);
            return array;
        }

        /// <summary>
        /// Remove the at.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="T:Point3D[]"/>.</returns>
        public static Point3D[] RemoveAt(this Point3D[] array, int index)
        {
            ArrayUtilities.RemoveAt(ref array, index);
            return array;
        }

        /// <summary>
        /// Remove the at.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="T:Vector2D[]"/>.</returns>
        public static Vector2D[] RemoveAt(this Vector2D[] array, int index)
        {
            ArrayUtilities.RemoveAt(ref array, index);
            return array;
        }

        /// <summary>
        /// Remove the at.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="T:Vector3D[]"/>.</returns>
        public static Vector3D[] RemoveAt(this Vector3D[] array, int index)
        {
            ArrayUtilities.RemoveAt(ref array, index);
            return array;
        }
        #endregion Remove At

        #region Reverse
        //[DebuggerStepThrough]
        /// <summary>
        /// The reverse.
        /// </summary>
        /// <param name="segment">The segment.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reverse(this LineSegment segment)
            => segment.Points.Reverse();
        #endregion Reverse

        #region Rotate Around Point
        /// <summary>
        /// Creates a matrix to rotate an object around a particular point.
        /// </summary>
        /// <param name="angle">The angle to rotate in radians.</param>
        /// <param name="center">The point around which to rotate.</param>
        /// <returns>Return a rotation matrix to rotate around a point.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x2D RotateAroundPoint(this Point2D center, double angle)
        {
            // Translate the point to the origin.
            var result = new Matrix3x2D();

            // We need to go counter-clockwise.
            result.RotateAt((float)-angle.ToDegrees(), center.X, center.Y);

            return result;
        }
        #endregion Rotate Around Point

        #region Rotate Point
        /// <summary>
        /// Rotate a point around the world origin.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="angle">The angle to rotate in pi radians.</param>
        /// <returns>A point rotated about the origin by the specified pi radian angle.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D RotatePoint(this Point2D point, double angle)
            => RotatePoint2D(point.X, point.Y, 0, 0, angle);

        /// <summary>
        /// Rotate a point around a fulcrum point.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="axis">The fulcrum point to rotate the point around.</param>
        /// <param name="angle">The angle to rotate the point in pi radians.</param>
        /// <returns>A point rotated about the fulcrum point by the specified pi radian angle.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D RotatePoint(this Point2D point, Point2D axis, double angle)
            => RotatePoint2D(point.X, point.Y, axis.X, axis.Y, angle);
        #endregion Rotate Point

        #region Rotate Points
        /// <summary>
        /// Rotate a series of points around the world origin.
        /// </summary>
        /// <param name="points">The array of points to rotate.</param>
        /// <param name="angle">The angle to rotate the points in pi radians.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RotatePoints(this Point2D[] points, double angle)
        {
            for (var i = 0; i < points.Length; i++)
            {
                points[i] = RotatePoint(points[i], angle);
            }
        }

        /// <summary>
        /// Rotate a series of points around a fulcrum point.
        /// </summary>
        /// <param name="points">The array of points to rotate.</param>
        /// <param name="fulcrum">The point to rotate all other points around.</param>
        /// <param name="angle">The angle to rotate the points in pi radians.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RotatePoints(this Point2D[] points, Point2D fulcrum, double angle)
        {
            for (var i = 0; i < points.Length; i++)
            {
                points[i] = RotatePoint(points[i], fulcrum, angle);
            }
        }
        #endregion Rotate Points

        #region Scale
        /// <summary>
        /// Inflates a <see cref="Point2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point2D"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="double"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Scale(this Point2D point, double factor)
            => Scale2D(point.X, point.Y, factor);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Scale(this Vector2D value, int factor)
            => Scale2D(value.I, value.J, factor);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Scale(this Vector2D value, float factor)
            => Scale2D(value.I, value.J, factor);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Scale(this Vector2D value, double factor)
            => Scale2D(value.I, value.J, factor);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Scale(this Vector3D value, double factor)
            => Scale3D(value.I, value.J, value.K, factor);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Scale(this Vector4D value, double factor)
            => Scale4D(value.I, value.J, value.K, value.L, factor);

        /// <summary>
        /// The scale.
        /// </summary>
        /// <param name="quaternion1">The quaternion1.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        public static Quaternion4D Scale(this Quaternion4D quaternion1, double scalar)
            => new Quaternion4D(
                quaternion1.X * scalar,
                quaternion1.Y * scalar,
                quaternion1.Z * scalar,
                quaternion1.W * scalar);

        /// <summary>
        /// Used to multiply a Matrix2x2 object by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Scale(this Matrix2x2D left, double scalar)
            => new Matrix2x2D(
            left.M0x0 * scalar,
            left.M0x1 * scalar,
            left.M1x0 * scalar,
            left.M1x1 * scalar);

        /// <summary>
        /// Used to multiply a Matrix3x3 object by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Scale(this Matrix3x3D left, double scalar)
            => new Matrix3x3D(
                left.M0x0 * scalar,
                left.M0x1 * scalar,
                left.M0x2 * scalar,
                left.M1x0 * scalar,
                left.M1x1 * scalar,
                left.M1x2 * scalar,
                left.M2x0 * scalar,
                left.M2x1 * scalar,
                left.M2x2 * scalar);

        /// <summary>
        /// Used to multiply a Matrix4x4 object by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Scale(this Matrix4x4D left, double scalar)
            => Scale4x4(
                left.M0x0, left.M0x1, left.M0x2, left.M0x3,
                left.M1x0, left.M1x1, left.M1x2, left.M1x3,
                left.M2x0, left.M2x1, left.M2x2, left.M2x3,
                left.M3x0, left.M3x1, left.M3x2, left.M3x3,
                scalar);
        #endregion Scale

        #region Slerp
        //[DebuggerStepThrough]
        /// <summary>
        /// The slerp.
        /// </summary>
        /// <param name="quaternion1">The quaternion1.</param>
        /// <param name="quaternion2">The quaternion2.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Slerp(this Quaternion4D quaternion1, Quaternion4D quaternion2, double amount)
        {
            double num2;
            double num3;
            var num = amount;
            var num4 = (quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y) + (quaternion1.Z * quaternion2.Z) + (quaternion1.W * quaternion2.W);
            var flag = false;
            if (num4 < 0d)
            {
                flag = true;
                num4 = -num4;
            }
            if (num4 > 0.999999d)
            {
                num3 = 1d - num;
                num2 = flag ? -num : num;
            }
            else
            {
                var num5 = Acos(num4);
                var num6 = 1d / Sin(num5);
                num3 = Sin((1d - num) * num5) * num6;
                num2 = flag ? -Sin(num * num5) * num6 : Sin(num * num5) * num6;
            }
            return new Quaternion4D(
                (num3 * quaternion1.X) + (num2 * quaternion2.X),
                (num3 * quaternion1.Y) + (num2 * quaternion2.Y),
                (num3 * quaternion1.Z) + (num2 * quaternion2.Z),
                (num3 * quaternion1.W) + (num2 * quaternion2.W));
        }
        #endregion Slerp

        #region Slope
        /// <summary>
        /// Calculates the Slope of two points.
        /// </summary>
        /// <param name="PointA">Starting Point</param>
        /// <param name="PointB">Ending Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>The slope is calculated with Slope = (YB - YA) / (XB - XA) or rise over run</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(this Point2D PointA, Point2D PointB)
            => Maths.Slope(PointA.X, PointA.Y, PointB.X, PointB.Y);

        /// <summary>
        /// Calculates the Slope of a vector.
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>The slope is calculated with Slope = Y / X or rise over run</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(this Vector2D Point)
            => Maths.Slope(Point.I, Point.J);

        /// <summary>
        /// Returns the slope angle of a line.
        /// </summary>
        /// <param name="Line">Line to get length of</param>
        /// <returns>Returns the slope angle of a line.</returns>

        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(this LineSegment Line)
            => Maths.Slope(Line.A.X, Line.A.Y, Line.B.X, Line.B.Y);
        #endregion Slope

        #region Subtract
        /// <summary>
        /// Subtracts a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="minuend">The <see cref="Point2D"/> to reduce.</param>
        /// <param name="subtrahend">The amount to reduce the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure reduced by the amount provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Subtract(this Point2D minuend, double subtrahend)
            => Subtract2D(minuend.X, minuend.Y, subtrahend);

        /// <summary>
        /// Subtracts a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="minuend">The <see cref="Point2D"/> to reduce.</param>
        /// <param name="subtrahend">The amount to reduce the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure reduced by the amount provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Subtract(this Point2D minuend, Point2D subtrahend)
            => Subtract2D(minuend.X, minuend.Y, subtrahend.X, subtrahend.Y);

        /// <summary>
        /// Subtracts a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="minuend">The <see cref="Point2D"/> to reduce.</param>
        /// <param name="subtrahend">The amount to reduce the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure reduced by the amount provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Subtract(this Point2D minuend, Size2D subtrahend)
            => Subtract2D(minuend.X, minuend.Y, subtrahend.Width, subtrahend.Height);

        /// <summary>
        /// Subtracts a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="minuend">The <see cref="Point2D"/> to reduce.</param>
        /// <param name="subtrahend">The amount to reduce the <see cref="Vector2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure reduced by the amount provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Subtract(this Point2D minuend, Vector2D subtrahend)
            => Subtract2D(minuend.X, minuend.Y, subtrahend.I, subtrahend.J);

        /// <summary>
        /// Subtracts a <see cref="Size2D"/> by a value.
        /// </summary>
        /// <param name="minuend">The <see cref="Size2D"/> to reduce.</param>
        /// <param name="subtrahend">The amount to reduce the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure reduced by the amount provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Subtract(this Size2D minuend, double subtrahend)
            => Subtract2D(minuend.Width, minuend.Height, subtrahend, subtrahend);

        /// <summary>
        /// Subtracts a <see cref="Size2D"/> by a value.
        /// </summary>
        /// <param name="minuend">The <see cref="Size2D"/> to reduce.</param>
        /// <param name="subtrahend">The amount to reduce the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure reduced by the amount provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Subtract(this Size2D minuend, Point2D subtrahend)
            => Subtract2D(minuend.Width, minuend.Height, subtrahend.X, subtrahend.Y);

        /// <summary>
        /// Subtracts a <see cref="Size2D"/> by a value.
        /// </summary>
        /// <param name="minuend">The <see cref="Size2D"/> to reduce.</param>
        /// <param name="subtrahend">The amount to reduce the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure reduced by the amount provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Subtract(this Size2D minuend, Size2D subtrahend)
            => Subtract2D(minuend.Width, minuend.Height, subtrahend.Width, subtrahend.Height);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahendX"></param>
        /// <param name="subtrahendY"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Subtract(this Vector2D minuend, double subtrahendX, double subtrahendY)
            => Subtract2D(minuend.I, minuend.J, subtrahendX, subtrahendY);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Subtract(this Vector2D minuend, double subtrahend)
            => Subtract2D(minuend.I, minuend.J, subtrahend);

        ///// <summary>
        ///// Subtract Points
        ///// </summary>
        ///// <param name="minuend"></param>
        ///// <param name="subtrahend"></param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static PointF Subtract(this Vector2D minuend, Point subtrahend)
        //    => new PointF((float)(minuend.I - subtrahend.X), (float)(minuend.J - subtrahend.Y));

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Subtract(this Vector2D minuend, Point2D subtrahend)
            => Subtract2D(minuend.I, minuend.J, subtrahend.X, subtrahend.Y);

        ///// <summary>
        ///// Subtract Points
        ///// </summary>
        ///// <param name="minuend"></param>
        ///// <param name="subtrahend"></param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static PointF Subtract(this Vector2D minuend, PointF subtrahend)
        //    => new PointF((float)(minuend.I - subtrahend.X), (float)(minuend.J - subtrahend.Y));

        ///// <summary>
        ///// Subtract Points
        ///// </summary>
        ///// <param name="minuend"></param>
        ///// <param name="subtrahend"></param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector2D Subtract(this Vector2D minuend, Size subtrahend)
        //    => Subtract2D(minuend.I, minuend.J, subtrahend.Width, subtrahend.Height);

        ///// <summary>
        ///// Subtract Points
        ///// </summary>
        ///// <param name="minuend"></param>
        ///// <param name="subtrahend"></param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector2D Subtract(this Vector2D minuend, SizeF subtrahend)
        //    => Subtract2D(minuend.I, minuend.J, subtrahend.Width, subtrahend.Height);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Subtract(this Vector2D minuend, Vector2D subtrahend)
            => Subtract2D(minuend.I, minuend.J, subtrahend.I, subtrahend.J);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Subtract(this Vector3D minuend, double subtrahend)
            => Subtract3D(minuend.I, minuend.J, minuend.K, subtrahend);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Subtract(this Vector3D minuend, Point3D subtrahend)
            => Subtract3D(minuend.I, minuend.J, minuend.K, subtrahend.X, subtrahend.Y, subtrahend.Z);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Subtract(this Vector3D minuend, Vector3D subtrahend)
            => Subtract3D(minuend.I, minuend.J, minuend.K, subtrahend.I, subtrahend.J, subtrahend.K);

        /// <summary>
        /// The subtract.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns>The <see cref="Vector4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Subtract(this Vector4D minuend, double subtrahend)
            => Subtract4D(minuend.I, minuend.J, minuend.K, minuend.L, subtrahend);

        /// <summary>
        /// The subtract.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns>The <see cref="Vector4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Subtract(this Vector4D minuend, Vector4D subtrahend)
            => Subtract4D(minuend.I, minuend.J, minuend.K, minuend.L, subtrahend.I, subtrahend.J, subtrahend.K, subtrahend.L);

        /// <summary>
        /// The subtract.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Subtract(this Quaternion4D minuend, double subtrahend)
            => Subtract4D(minuend.X, minuend.Y, minuend.Z, minuend.W, subtrahend);

        /// <summary>
        /// The subtract.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Subtract(this Quaternion4D minuend, Quaternion4D subtrahend)
            => Subtract4D(minuend.X, minuend.Y, minuend.Z, minuend.W, subtrahend.X, subtrahend.Y, subtrahend.Z, subtrahend.W);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Subtract(this Matrix2x2D minuend, Matrix2x2D subtrahend)
            => Subtract2x2x2x2(
                minuend.M0x0, minuend.M0x1,
                minuend.M1x0, minuend.M1x1,
                subtrahend.M0x0, subtrahend.M0x1,
                subtrahend.M1x0, subtrahend.M1x1);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Subtract(this Matrix3x3D minuend, Matrix3x3D subtrahend)
            => Subtract3x3x3x3(
                minuend.M0x0, minuend.M0x1, minuend.M0x2,
                minuend.M1x0, minuend.M1x1, minuend.M1x2,
                minuend.M2x0, minuend.M2x1, minuend.M2x2,
                subtrahend.M0x0, subtrahend.M0x1, subtrahend.M0x2,
                subtrahend.M1x0, subtrahend.M1x1, subtrahend.M1x2,
                subtrahend.M2x0, subtrahend.M2x1, subtrahend.M2x2);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subtrahend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Subtract(this Matrix4x4D minuend, Matrix4x4D subtrahend)
            => Subtract4x4x4x4(
                minuend.M0x0, minuend.M0x1, minuend.M0x2, minuend.M0x3,
                minuend.M1x0, minuend.M1x1, minuend.M1x2, minuend.M1x3,
                minuend.M2x0, minuend.M2x1, minuend.M2x2, minuend.M2x3,
                minuend.M3x0, minuend.M3x1, minuend.M3x2, minuend.M3x3,
                subtrahend.M0x0, subtrahend.M0x1, subtrahend.M0x2, subtrahend.M0x3,
                subtrahend.M1x0, subtrahend.M1x1, subtrahend.M1x2, subtrahend.M1x3,
                subtrahend.M2x0, subtrahend.M2x1, subtrahend.M2x2, subtrahend.M2x3,
                subtrahend.M3x0, subtrahend.M3x1, subtrahend.M3x2, subtrahend.M3x3);

        /// <summary>
        /// The subtract.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns>The <see cref="LineSegment"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment Subtract(this LineSegment minuend, double subtrahend)
            => Subtract4D(minuend.AX, minuend.AY, minuend.BX, minuend.BY, subtrahend);

        /// <summary>
        /// The subtract.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns>The <see cref="LineSegment"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment Subtract(this LineSegment minuend, LineSegment subtrahend)
            => Subtract4D(minuend.AX, minuend.AY, minuend.BX, minuend.BY, subtrahend.AX, subtrahend.AY, subtrahend.BX, subtrahend.BY);

        /// <summary>
        /// The subtract.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns>The <see cref="Transform2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform2D Subtract(this Transform2D minuend, Transform2D subtrahend)
            => new Transform2D(minuend.X - subtrahend.X, minuend.Y - subtrahend.Y, NormalizeRadian(minuend.SkewX - subtrahend.SkewX), NormalizeRadian(minuend.SkewY - subtrahend.SkewY), minuend.ScaleX / subtrahend.ScaleX, minuend.ScaleY / subtrahend.ScaleY);
        #endregion Subtract

        #region Transpose
        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>A transposed Matrix.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Transpose(this Matrix2x2D source)
            => new Matrix2x2D(
                source.M0x0,
                source.M1x0,
                source.M0x1,
                source.M1x1);

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>A transposed Matrix.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Transpose(this Matrix3x3D source)
            => new Matrix3x3D(
                source.M0x0,
                source.M1x0,
                source.M2x0,
                source.M0x1,
                source.M1x1,
                source.M2x1,
                source.M0x2,
                source.M1x2,
                source.M2x2);

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>A transposed Matrix.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Transpose(this Matrix4x4D source)
            => new Matrix4x4D(
                source.M0x0,
                source.M1x0,
                source.M2x0,
                source.M3x0,
                source.M0x1,
                source.M1x1,
                source.M2x1,
                source.M3x1,
                source.M0x2,
                source.M1x2,
                source.M2x2,
                source.M3x2,
                source.M0x3,
                source.M1x3,
                source.M2x3,
                source.M3x3);
        #endregion Transpose

        #region Unit
        /// <summary>
        /// Unit of a Vector
        /// </summary>
        /// <param name="value">The Vector to Unitize</param>
        /// <returns></returns>

        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Unit(this Vector2D value)
            => Maths.Unit(value.I, value.J);

        /// <summary>
        /// Unit of a Vector
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>

        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Unit(this Vector3D value)
            => Maths.Unit(value.I, value.J, value.K);

        /// <summary>
        /// Unit of a Vector
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>

        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Unit(this Vector4D value)
            => Maths.Unit(value.I, value.J, value.K, value.L);
        #endregion Unit

        /// <summary>
        /// The cross product.
        /// </summary>
        /// <param name="O">The O.</param>
        /// <param name="A">The A.</param>
        /// <param name="B">The B.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks>
        /// http://jwezorek.com/2017/09/basic-convex-hull-in-c/
        /// https://en.wikibooks.org/wiki/Algorithm_Implementation/Geometry/Convex_hull/Monotone_chain
        /// </remarks>
        public static double CrossProduct(Point2D O, Point2D A, Point2D B)
            => ((A.X - O.X) * (B.Y - O.Y)) - ((A.Y - O.Y) * (B.X - O.X));

        /// <summary>
        /// Get the convex hull.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        /// <remarks>
        /// http://jwezorek.com/2017/09/basic-convex-hull-in-c/
        /// https://en.wikibooks.org/wiki/Algorithm_Implementation/Geometry/Convex_hull/Monotone_chain
        /// </remarks>
        public static List<Point2D> GetConvexHull(this List<Point2D> points)
        {
            if (points is null)
            {
                return null;
            }

            if (points.Count <= 1)
            {
                return points;
            }

            var n = points.Count;
            var k = 0;
            var H = new List<Point2D>(new Point2D[2 * n]);

            points.Sort((a, b) =>
                 a.X == b.X ? a.Y.CompareTo(b.Y) : a.X.CompareTo(b.X));

            // Build lower hull
            for (var i = 0; i < n; ++i)
            {
                while (k >= 2 && CrossProduct(H[k - 2], H[k - 1], points[i]) <= 0)
                {
                    k--;
                }

                H[k++] = points[i];
            }

            // Build upper hull
            for (int i = n - 2, t = k + 1; i >= 0; i--)
            {
                while (k >= t && CrossProduct(H[k - 2], H[k - 1], points[i]) <= 0)
                {
                    k--;
                }

                H[k++] = points[i];
            }

            return H.Take(k - 1).ToList();
        }
    }
}
