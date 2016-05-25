﻿// <copyright file="PrimitivesExtensions.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public static class PrimitivesExtensions
    {
        #region Absolute Angle

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="pointA">First Point.</param>
        /// <param name="pointB">Second Point.</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteAngle(this Point pointA, Point pointB)
        {
            return Maths.AbsoluteAngle(pointA.X, pointA.Y, pointB.X, pointB.Y);
        }

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="pointA">First Point.</param>
        /// <param name="pointB">Second Point.</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteAngle(this PointF pointA, PointF pointB)
        {
            return Maths.AbsoluteAngle(pointA.X, pointA.Y, pointB.X, pointB.Y);
        }

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="segment">Line segment.</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteAngle(this LineSegment segment)
        {
            return Maths.AbsoluteAngle(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);
        }

        #endregion

        #region Add

        /// <summary>
        /// Adds a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="Point2D"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Add(this Point2D point, double addend)
        {
            return new Point2D(point.X + addend, point.Y + addend);
        }

        /// <summary>
        /// Adds a <see cref="Point2D"/> to a <see cref="Point2D"/> by value.
        /// </summary>
        /// <param name="point">The <see cref="Point2D"/> to add to.</param>
        /// <param name="addend">The <see cref="Point2D"/> to add with.</param>
        /// <returns>
        /// Returns a <see cref="Point2D"/> structure enlarged by the amount of the other <see cref="Point2D"/> structure.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(this Point2D point, Point2D addend)
        {
            return new Vector2D(point.X + addend.X, point.Y + addend.Y);
        }

        /// <summary>
        /// Adds a <see cref="Size2D"/> to a <see cref="Point2D"/> by value.
        /// </summary>
        /// <param name="point">The <see cref="Point2D"/> to add to.</param>
        /// <param name="addend">The <see cref="Size2D"/> to add with.</param>
        /// <returns>
        /// Returns a <see cref="Point2D"/> structure enlarged by the amount of the <see cref="Size2D"/> structure.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Add(this Point2D point, Size2D addend)
        {
            return new Point2D(point.X + addend.Width, point.Y + addend.Height);
        }

        /// <summary>
        /// Adds a <see cref="Vector2D"/> to a <see cref="Point2D"/> by value.
        /// </summary>
        /// <param name="point">The <see cref="Point2D"/> to add to.</param>
        /// <param name="addend">The <see cref="Vector2D"/> to add with.</param>
        /// <returns>
        /// Returns a <see cref="Point2D"/> structure enlarged by the amount of the <see cref="Size2D"/> structure.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Add(this Point2D point, Vector2D addend)
        {
            return new Point2D(point.X + addend.I, point.Y + addend.J);
        }

        /// <summary>
        /// Adds a <see cref="Size2D"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="Size2D"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Add(this Size2D point, double addend)
        {
            return new Size2D(point.Width + addend, point.Height + addend);
        }

        /// <summary>
        /// Adds a <see cref="Point2D"/> to a <see cref="Size2D"/> by value.
        /// </summary>
        /// <param name="point">The <see cref="Size2D"/> to add to.</param>
        /// <param name="addend">The <see cref="Point2D"/> to add with.</param>
        /// <returns>
        /// Returns a <see cref="Size2D"/> structure enlarged by the amount of the <see cref="Point2D"/> structure.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Add(this Size2D point, Point2D addend)
        {
            return new Size2D(point.Width + addend.X, point.Height + addend.Y);
        }

        /// <summary>
        /// Adds a <see cref="Size2D"/> to a <see cref="Size2D"/> by value.
        /// </summary>
        /// <param name="point">The <see cref="Size2D"/> to add to.</param>
        /// <param name="addend">The <see cref="Size2D"/> to add with.</param>
        /// <returns>
        /// Returns a <see cref="Size2D"/> structure enlarged by the amount of the other <see cref="Size2D"/> structure.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Add(this Size2D point, Size2D addend)
        {
            return new Size2D(point.Width + addend.Width, point.Height + addend.Height);
        }

        /// <summary>
        /// Adds a <see cref="Vector2D"/> to a <see cref="Size2D"/> by value.
        /// </summary>
        /// <param name="point">The <see cref="Size2D"/> to add to.</param>
        /// <param name="addend">The <see cref="Vector2D"/> to add with.</param>
        /// <returns>
        /// Returns a <see cref="Size2D"/> structure enlarged by the amount of the <see cref="Point2D"/> structure.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Add(this Size2D point, Vector2D addend)
        {
            return new Size2D(point.Width + addend.I, point.Height + addend.J);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, int addend)
        {
            return new Point(point.X + addend, point.Y + addend);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, float addend)
        {
            return new Point((int)(point.X + addend), (int)(point.Y + addend));
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, double addend)
        {
            return new Point((int)(point.X + addend), (int)(point.Y + addend));
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, Point addend)
        {
            return new Point(point.X + addend.X, point.Y + addend.Y);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, PointF addend)
        {
            return new Point((int)(point.X + addend.X), (int)(point.Y + addend.Y));
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, Size addend)
        {
            return new Point(point.X + addend.Width, point.Y + addend.Height);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, SizeF addend)
        {
            return new Point((int)(point.X + addend.Width), (int)(point.Y + addend.Height));
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, Vector2D addend)
        {
            return new Point((int)(point.X + addend.I), (int)(point.Y + addend.J));
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, int addend)
        {
            return new PointF(point.X + addend, point.Y + addend);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, float addend)
        {
            return new PointF(point.X + addend, point.Y + addend);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, double addend)
        {
            return new PointF((float)(point.X + addend), (float)(point.Y + addend));
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(this PointF point, PointF addend)
        {
            return new Vector2D(point.X + addend.X, point.Y + addend.Y);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, Size addend)
        {
            return new PointF(point.X + addend.Width, point.Y + addend.Height);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, SizeF addend)
        {
            return new PointF(point.X + addend.Width, point.Y + addend.Height);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, Vector2D addend)
        {
            return new PointF((float)(point.X + addend.I), (float)(point.Y + addend.J));
        }

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(this Vector2D vector, int addend)
        {
            return new Vector2D(vector.I + addend, vector.J + addend);
        }

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(this Vector2D vector, float addend)
        {
            return new Vector2D(vector.I + addend, vector.J + addend);
        }

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(this Vector2D vector, double addend)
        {
            return new Vector2D((float)(vector.I + addend), (float)(vector.J + addend));
        }

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Add(this Vector2D vector, Point addend)
        {
            return new Point2D(vector.I + addend.X, vector.J + addend.Y);
        }

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Add(this Vector2D vector, Point2D addend)
        {
            return new Point2D(vector.I + addend.X, vector.J + addend.Y);
        }

        /// <summary>
        /// Add Vector2D
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(this Vector2D vector, Vector2D addend)
        {
            return new Vector2D(vector.I + addend.I, vector.J + addend.J);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment Add(this LineSegment segment, LineSegment addend)
        {
            return new LineSegment(
                segment.Points[0].X + addend.Points[0].X,
                segment.Points[0].Y + addend.Points[0].Y,
                segment.Points[1].X + addend.Points[1].X,
                segment.Points[1].Y + addend.Points[1].Y);
        }

        #endregion

        #region Angle

        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="PointA">Starting Point</param>
        /// <param name="PointB">Ending Point</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(this Point2D PointA, Point2D PointB)
        {
            return Maths.Angle(PointA.X, PointA.Y, PointB.X, PointB.Y);
        }

        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="PointA">Starting Point</param>
        /// <param name="PointB">Ending Point</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(this Point PointA, Point PointB)
        {
            return Maths.Angle(PointA.X, PointA.Y, PointB.X, PointB.Y);
        }

        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="PointA">Starting Point</param>
        /// <param name="PointB">Ending Point</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(this PointF PointA, PointF PointB)
        {
            return Maths.Angle(PointA.X, PointA.Y, PointB.X, PointB.Y);
        }

        /// <summary>
        /// Returns the Angle of a line segment.
        /// </summary>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(this LineSegment segment)
        {
            return Maths.Angle(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);
        }

        #endregion

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
        {
            return Maths.CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
        }

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
        public static double CrossProduct(this Point valueA, Point valueB)
        {
            return Maths.CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
        }

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
        public static double CrossProduct(this Point valueA, PointF valueB)
        {
            return Maths.CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
        }

        /// <summary>
        /// Cross Product of a corner
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks>Note that AB · BC = |AB| * |BC| * Cos(theta).</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct(this Point point, Vector2D value)
        {
            return Maths.CrossProduct(point.X, point.Y, value.I, value.J);
        }

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
        public static double CrossProduct(this PointF valueA, Point valueB)
        {
            return Maths.CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
        }

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
        public static double CrossProduct(this PointF valueA, PointF valueB)
        {
            return Maths.CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
        }

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
        public static double CrossProduct(this PointF valueA, Vector2D valueB)
        {
            return Maths.CrossProduct(valueA.X, valueA.Y, valueB.I, valueB.J);
        }

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
        public static double CrossProduct(this Vector2D valueA, Point valueB)
        {
            return Maths.CrossProduct(valueA.I, valueA.J, valueB.X, valueB.Y);
        }

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
        public static double CrossProduct(this Vector2D valueA, PointF valueB)
        {
            return Maths.CrossProduct(valueA.I, valueA.J, valueB.X, valueB.Y);
        }

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
        {
            return Maths.CrossProduct(valueA.I, valueA.J, valueB.I, valueB.J);
        }

        #endregion

        #region Delta

        /// <summary>
        /// Finds the Delta of two Points
        /// </summary>
        /// <param name="value1">First Point</param>
        /// <param name="value2">Second Point</param>
        /// <returns>Returns the Difference Between PointA and PointB</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Delta(this Point value1, Point value2)
        {
            return value2.Subtract(value1);
        }

        /// <summary>
        /// Finds the Delta of two Points
        /// </summary>
        /// <param name="value1">First Point</param>
        /// <param name="value2">Second Point</param>
        /// <returns>Returns the Difference Between PointA and PointB</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Delta(this PointF value1, PointF value2)
        {
            return value2.Subtract(value1);
        }

        /// <summary>
        /// Finds the Delta of two Points
        /// </summary>
        /// <param name="value1">First Point</param>
        /// <param name="value2">Second Point</param>
        /// <returns>Returns the Difference Between PointA and PointB</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Delta(this Point2D value1, Point2D value2)
        {
            return value2.Subtract(value1);
        }

        /// <summary>
        /// Finds the Delta of two Vectors
        /// </summary>
        /// <param name="vector">First Point</param>
        /// <param name="value">Second Point</param>
        /// <returns>Returns the Difference Between PointA and PointB</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Delta(this Vector2D vector, Vector2D value)
        {
            return value - vector;
        }

        #endregion

        #region Distance/Length

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point2D a, Point2D b)
        {
            return Maths.Distance(a.X, a.Y, b.X, b.Y);
        }

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point a, Point b)
        {
            return Maths.Distance(a.X, a.Y, b.X, b.Y);
        }

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this PointF a, PointF b)
        {
            return Maths.Distance(a.X, a.Y, b.X, b.Y);
        }

        /// <summary>
        /// Length of a Vector
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Vector2D a, Vector2D b)
        {
            return Maths.Distance(a.I, a.J, b.I, b.J);
        }

        /// <summary>
        /// Length of a Segment
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this LineSegment segment)
        {
            return Maths.Distance(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);
        }

        /// <summary>
        /// Returns the Length of a lineSeg.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Length of a lineSeg.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(int x1, int y1, int x2, int y2)
        {
            return Maths.Distance(x1, y1, x2, y2);
        }

        /// <summary>
        /// Returns the Length of a lineSeg.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Length of a lineSeg.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(float x1, float y1, float x2, float y2)
        {
            return Maths.Distance(x1, y1, x2, y2);
        }

        /// <summary>
        /// Returns the Length of a lineSeg.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Length of a lineSeg.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(double x1, double y1, double x2, double y2)
        {
            return Maths.Distance(x1, y1, x2, y2);
        }

        /// <summary>
        /// Calculates the Length between two points.
        /// </summary>
        /// <param name="point">Starting Point.</param>
        /// <param name="value">Ending Point.</param>
        /// <returns>Returns the length of a line segment between two points.</returns>
        /// <remarks>The Length is calculated as AC = SquarRoot(AB^2 + BC^2) </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this Point2D point, Point2D value)
        {
            return Maths.Distance(point.X, point.Y, value.X, value.Y);
        }

        /// <summary>
        /// Calculates the Length between two points.
        /// </summary>
        /// <param name="point">Starting Point.</param>
        /// <param name="value">Ending Point.</param>
        /// <returns>Returns the length of a line segment between two points.</returns>
        /// <remarks>The Length is calculated as AC = SquarRoot(AB^2 + BC^2) </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this Point point, Point value)
        {
            return Maths.Distance(point.X, point.Y, value.X, value.Y);
        }

        /// <summary>
        /// Calculates the Length between two points.
        /// </summary>
        /// <param name="point">Starting Point.</param>
        /// <param name="value">Ending Point.</param>
        /// <returns>Returns the length of a line segment between two points.</returns>
        /// <remarks>The Length is calculated as AC = SquarRoot(AB^2 + BC^2) </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this PointF point, PointF value)
        {
            return Maths.Distance(point.X, point.Y, value.X, value.Y);
        }

        /// <summary>
        /// Finds the length of a 2D vector
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"> Point</param>
        /// <returns>The Length between two Points</returns>
        /// <remarks>The Length is calculated as AC = SquarRoot(AB^2 + BC^2) </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this Vector2D vector, Vector2D value)
        {
            return Maths.Distance(vector.I, vector.J, value.I, value.J);
        }

        /// <summary>
        /// Finds the Length between two points
        /// </summary>
        /// <param name="segment">line segment</param>
        /// <returns>The Length between two Points</returns>
        /// <remarks>The Length is calculated as AC = SquarRoot(AB^2 + BC^2) </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this LineSegment segment)
        {
            return Maths.Distance(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);
        }

        #endregion

        #region Divide
        /// <summary>
        /// Divide Point
        /// </summary>
        /// <param name="Value1">First Point</param>
        /// <param name="Value2">Second Point</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Divide(this Vector2D Value1, Vector2D Value2)
        {
            return Value1.DotProduct(Value2.Invert());
        }
        #endregion

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
        {
            return Maths.DotProduct(value.X, value.Y, value.X, value.Y);
        }

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="value">Starting Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Point value)
        {
            return Maths.DotProduct(value.X, value.Y, value.X, value.Y);
        }

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="value">Starting Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this PointF value)
        {
            return Maths.DotProduct(value.X, value.Y, value.X, value.Y);
        }

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="value">Starting Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Vector2D value)
        {
            return Maths.DotProduct(value.I, value.J, value.I, value.J);
        }

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
        {
            return Maths.DotProduct(point.X, point.Y, value.X, value.Y);
        }

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
        public static double DotProduct(this Point point, Point value)
        {
            return Maths.DotProduct(point.X, point.Y, value.X, value.Y);
        }

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
        public static double DotProduct(this Point point, PointF value)
        {
            return Maths.DotProduct(point.X, point.Y, value.X, value.Y);
        }

        /// <summary>
        /// Determines the dot product of two 2D vectors
        /// </summary>
        /// <param name="point"></param>
        /// <param name="vector">Second Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Point point, Vector2D vector)
        {
            return Maths.DotProduct(point.X, point.Y, vector.I, vector.J);
        }

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
        public static double DotProduct(this PointF point, Point value)
        {
            return Maths.DotProduct(point.X, point.Y, value.X, value.Y);
        }

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
        public static double DotProduct(this PointF point, PointF value)
        {
            return Maths.DotProduct(point.X, point.Y, value.X, value.Y);
        }

        /// <summary>
        /// Finds the Dot Product of two Points 
        /// </summary>
        /// <param name="point">First Point</param>
        /// <param name="vector">Second Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>
        /// The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this PointF point, Vector2D vector)
        {
            return Maths.DotProduct(point.X, point.Y, vector.I, vector.J);
        }

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
        public static double DotProduct(this Vector2D vector, Point value)
        {
            return Maths.DotProduct(vector.I, vector.J, value.X, value.Y);
        }

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
        public static double DotProduct(this Vector2D vector, PointF value)
        {
            return Maths.DotProduct(vector.I, vector.J, value.X, value.Y);
        }

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
        {
            return Maths.DotProduct(vector.I, vector.J, value.I, value.J);
        }

        #endregion

        #region Inflate
        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Inflate(this Point point, int factor)
        {
            return new Point((point.X * factor), (point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Inflate(this Point point, float factor)
        {
            return new Point((int)(point.X * factor), (int)(point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Inflate(this Point point, double factor)
        {
            return new Point((int)(point.X * factor), (int)(point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Inflate(this Point point, Point factors)
        {
            return new Point((point.X * factors.X), (point.Y * factors.Y));
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Inflate(this Point point, PointF factors)
        {
            return new Point((int)(point.X * factors.X), (int)(point.Y * factors.Y));
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Inflate(this Point size, Size factor)
        {
            return new Point(size.X * factor.Width, size.Y * factor.Height);
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Inflate(this Point point, SizeF factor)
        {
            return new Point((int)(point.X * factor.Width), (int)(point.Y * factor.Height));
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Inflate(this Point point, Vector2D factors)
        {
            return new Point((int)(point.X * factors.I), (int)(point.Y * factors.J));
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Inflate(this Point2D point, Vector2D factors)
        {
            return new Point2D(point.X * factors.I, point.Y * factors.J);
        }

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, int factor)
        {
            return new PointF((point.X * factor), (point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, float factor)
        {
            return new PointF((point.X * factor), (point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, double factor)
        {
            return new PointF((float)(point.X * factor), (float)(point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, Point factors)
        {
            return new PointF((point.X * factors.X), (point.Y * factors.Y));
        }

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, PointF factors)
        {
            return new PointF((point.X * factors.X), (point.Y * factors.Y));
        }

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF size, Size factor)
        {
            return new PointF(size.X * factor.Width, size.Y * factor.Height);
        }

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, SizeF factor)
        {
            return new PointF(point.X * factor.Width, point.Y * factor.Height);
        }

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, Vector2D factors)
        {
            return new PointF((float)(point.X * factors.I), (float)(point.Y * factors.J));
        }

        /// <summary>
        /// Inflates a <see cref="Size2D"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size2D"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Inflate(this Size2D size, double factor)
        {
            return new Size2D(size.Width * factor, size.Height * factor);
        }

        /// <summary>
        /// Inflates a <see cref="SizeF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Inflate(this Size2D size, Size2D factor)
        {
            return new Size2D(size.Width * factor.Width, size.Height * factor.Height);
        }

        /// <summary>
        /// Inflates a <see cref="Size2D"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size2D"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Inflate(this Size size, Point2D factor)
        {
            return new Size2D(size.Width * factor.X, size.Height * factor.Y);
        }

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size2D"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="Vector2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Inflate(this Size2D size, Vector2D factor)
        {
            return new Size2D(size.Width * factor.I, size.Height * factor.J);
        }

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, int factor)
        {
            return new Size(size.Width * factor, size.Height * factor);
        }

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, float factor)
        {
            return new Size((int)(size.Width * factor), (int)(size.Height * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, double factor)
        {
            return new Size((int)(size.Width * factor), (int)(size.Height * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, Point factor)
        {
            return new Size(size.Width * factor.X, size.Height * factor.Y);
        }

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, PointF factor)
        {
            return new Size((int)(size.Width * factor.X), (int)(size.Height * factor.Y));
        }

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, Size factor)
        {
            return new Size(size.Width * factor.Width, size.Height * factor.Height);
        }

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, SizeF factor)
        {
            return new Size((int)(size.Width * factor.Width), (int)(size.Height * factor.Height));
        }

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, Vector2D factor)
        {
            return new Size((int)(size.Width * factor.I), (int)(size.Height * factor.J));
        }

        /// <summary>
        /// Inflates a <see cref="SizeF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeF Inflate(this SizeF size, int factor)
        {
            return new SizeF(size.Width * factor, size.Height * factor);
        }

        /// <summary>
        /// Inflates a <see cref="SizeF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeF Inflate(this SizeF size, float factor)
        {
            return new SizeF(size.Width * factor, size.Height * factor);
        }

        /// <summary>
        /// Inflates a <see cref="SizeF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeF Inflate(this SizeF size, double factor)
        {
            return new SizeF((float)(size.Width * factor), (float)(size.Height * factor));
        }

        /// <summary>
        /// Inflates a <see cref="SizeF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeF Inflate(this SizeF size, Point factor)
        {
            return new SizeF(size.Width * factor.X, size.Height * factor.Y);
        }

        /// <summary>
        /// Inflates a <see cref="SizeF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeF Inflate(this SizeF size, PointF factor)
        {
            return new SizeF(size.Width * factor.X, size.Height * factor.Y);
        }

        /// <summary>
        /// Inflates a <see cref="SizeF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeF Inflate(this SizeF size, Size factor)
        {
            return new SizeF(size.Width * factor.Width, size.Height * factor.Height);
        }

        /// <summary>
        /// Inflates a <see cref="SizeF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeF Inflate(this SizeF size, SizeF factor)
        {
            return new SizeF(size.Width * factor.Width, size.Height * factor.Height);
        }

        /// <summary>
        /// Inflates a <see cref="SizeF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeF Inflate(this SizeF size, Vector2D factor)
        {
            return new SizeF((float)(size.Width * factor.I), (float)(size.Height * factor.J));
        }

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, int factor)
        {
            return new Vector2D((point.I * factor), (point.J * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, float factor)
        {
            return new Vector2D((point.I * factor), (point.J * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, double factor)
        {
            return new Vector2D((float)(point.I * factor), (float)(point.J * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, Point factors)
        {
            return new Vector2D((point.I * factors.X), (point.J * factors.Y));
        }

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, PointF factors)
        {
            return new Vector2D((point.I * factors.X), (point.J * factors.Y));
        }

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D size, Size factor)
        {
            return new Vector2D(size.I * factor.Width, size.J * factor.Height);
        }

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, SizeF factor)
        {
            return new Vector2D(point.I * factor.Width, point.J * factor.Height);
        }

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, Vector2D factors)
        {
            return new Vector2D((point.I * factors.I), (point.J * factors.J));
        }
        #endregion

        #region Invert
        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Invert(float x, float y)
        {
            return new Vector2D((1 / x), (1 / y));
        }

        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Invert(double x, double y)
        {
            return new Vector2D((1 / x), (1 / y));
        }

        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Invert(this PointF value)
        {
            return Invert(value.X, value.Y);
        }

        /// <summary>
        /// Inverts a Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Invert(this Vector2D value)
        {
            return Invert(value.I, value.J);
        }
        #endregion

        #region Modulus

        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulus(this Vector2D value)
        {
            return Maths.Modulus(value.I, value.J);
        }

        #endregion

        #region Multiply
        /// <summary>
        /// Multiply: Point * Matrix
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerStepThrough]
        public static Point2D Multiply(this Point2D point, Matrix2D matrix)
        {
            return matrix.Transform(point);
        }
        #endregion

        #region Normalize

        /// <summary>
        /// Normalize Two Points
        /// </summary>
        /// <param name="point">First Point</param>
        /// <param name="value">Second Point</param>
        /// <returns>The Normal of two Points</returns>
        /// <remarks></remarks>
        public static PointF Normalize(this PointF point, SizeF value)
        {
            return new Point2D(Maths.Normalize(point.X, point.Y, value.Width, value.Height)).ToPointF();
        }

        #endregion

        #region Offset

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static List<Point2D> Offset(this Point2D point, Point2D value, double distance)
        {
            Tuple<double, double, double, double> offset = Maths.OffsetSegment(point.X, point.Y, value.X, value.Y, distance);
            return new List<Point2D>() { new Point2D(offset.Item1, offset.Item2), new Point2D(offset.Item3, offset.Item4) };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment OffsetSegment(Point2D point, Point2D value, double distance)
        {
            return new LineSegment(Maths.OffsetSegment(point.X, point.Y, value.X, value.Y, distance));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment Offset(this LineSegment segment, double distance)
        {
            return new LineSegment(Maths.OffsetSegment(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y, distance));
        }

        #endregion

        #region Perpendicular Vector

        /// <summary>
        /// Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Perpendicular(float i, float j)
        {
            return new Vector2D(Maths.PerpendicularClockwise(i, j));
        }

        /// <summary>
        /// Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Perpendicular(double i, double j)
        {
            return new Vector2D(Maths.PerpendicularClockwise(i, j));
        }

        /// <summary>
        /// Perpendicular of a Vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Perpendicular(this Vector2D vector)
        {
            return new Vector2D(Maths.PerpendicularClockwise(vector.I, vector.J));
        }

        #endregion

        #region Reflect
        /// <summary>
        /// Calculates the reflection of a point off a line segment
        /// </summary>
        /// <param name="point">First point of line segment</param>
        /// <param name="value">Second point of line segment</param>
        /// <param name="axis">Point to Reflect</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Reflect(this PointF point, PointF value, PointF axis)
        {
            Vector2D SegmentVectorDelta = point.Delta(value);
            Vector2D QC12 = new Vector2D(
                value.CrossProduct(point),
                axis.DotProduct(SegmentVectorDelta)
                );
            double QC3 = 0.5F * SegmentVectorDelta.DotProduct(SegmentVectorDelta);
            return new PointF(
                (float)(QC3 * SegmentVectorDelta.CrossProduct(QC12) - axis.X),
                (float)(QC3 * SegmentVectorDelta.CrossProduct(QC12) - axis.Y)
                );
        }

        /// <summary>
        /// Calculates the reflection of a point off a line segment
        /// </summary>
        /// <param name="segment">The line segment</param>
        /// <param name="axis">Point to reflect about</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Reflect(this LineSegment segment, PointF axis)
        {
            Vector2D SegmentVectorDelta = segment.A.Delta(segment.B);
            Vector2D QC12 = new Vector2D(
                segment.B.CrossProduct(segment.A),
                axis.DotProduct(SegmentVectorDelta)
                );
            double QC3 = 0.5F * SegmentVectorDelta.DotProduct(SegmentVectorDelta);
            return new PointF(
                (float)(QC3 * SegmentVectorDelta.CrossProduct(QC12) - axis.X),
                (float)(QC3 * SegmentVectorDelta.CrossProduct(QC12) - axis.Y)
                );
        }
        #endregion

        #region Reverse
        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        public static void Reverse(this LineSegment segment)
        {
            segment.Points.Reverse();
        }
        #endregion

        #region Rotate Around Point
        /// <summary>
        /// Creates a matrix to rotate an object around a particular point.  
        /// </summary>
        /// <param name="angle">The angle to rotate in radians.</param>
        /// <param name="center">The point around which to rotate.</param>
        /// <returns>Return a rotation matrix to rotate around a point.</returns>
        public static Matrix RotateAroundPoint(this Point center, double angle)
        {
            // Translate the point to the origin.
            Matrix result = new Matrix();

            // We need to go counter-clockwise.
            result.RotateAt((float)-angle.ToDegrees(), center);

            return result;
        }

        /// <summary>
        /// Creates a matrix to rotate an object around a particular point.  
        /// </summary>
        /// <param name="angle">The angle to rotate in radians.</param>
        /// <param name="center">The point around which to rotate.</param>
        /// <returns>Return a rotation matrix to rotate around a point.</returns>
        public static Matrix RotateAroundPoint(this PointF center, double angle)
        {
            // Translate the point to the origin.
            Matrix result = new Matrix();

            // We need to go counter-clockwise.
            result.RotateAt((float)-angle.ToDegrees(), center);

            return result;
        }
        #endregion

        #region Rotate Point
        /// <summary>
        /// Rotate a point around the world origin.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="angle">The angle to rotate in pi radians.</param>
        /// <returns>A point rotated about the origin by the specified pi radian angle.</returns>
        public static Point RotatePoint(this Point point, double angle)
        {
            return RotatePoint(point, Point.Empty, angle);
        }

        /// <summary>
        /// Rotate a point around the world origin.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="angle">The angle to rotate in pi radians.</param>
        /// <returns>A point rotated about the origin by the specified pi radian angle.</returns>
        public static PointF RotatePoint(this PointF point, double angle)
        {
            return RotatePoint(point, PointF.Empty, angle);
        }

        /// <summary>
        /// Rotate a point around a fulcrum point.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="axis">The fulcrum point to rotate the point around.</param>
        /// <param name="angle">The angle to rotate the point in pi radians.</param>
        /// <returns>A point rotated about the fulcrum point by the specified pi radian angle.</returns>
        public static Point RotatePoint(this Point point, Point axis, double angle)
        {
            double deltaX = point.X - axis.X;
            double deltaY = point.Y - axis.Y;

            double angleCos = Cos(angle);
            double angleSin = Sin(angle);

            return new Point(
                (int)(axis.X + (deltaX * angleCos - deltaY * angleSin)),
                (int)(axis.Y + (deltaX * angleSin + deltaY * angleCos))
            );
        }

        /// <summary>
        /// Rotate a point around a fulcrum point.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="axis">The fulcrum point to rotate the point around.</param>
        /// <param name="angle">The angle to rotate the point in pi radians.</param>
        /// <returns>A point rotated about the fulcrum point by the specified pi radian angle.</returns>
        public static PointF RotatePoint(this PointF point, PointF axis, double angle)
        {
            double deltaX = point.X - axis.X;
            double deltaY = point.Y - axis.Y;

            double angleCos = Cos(angle);
            double angleSin = Sin(angle);

            return new PointF(
                (float)(axis.X + (deltaX * angleCos - deltaY * angleSin)),
                (float)(axis.Y + (deltaX * angleSin + deltaY * angleCos))
            );
        }
        #endregion

        #region Rotate Points
        /// <summary>
        /// Rotate a series of points around the world origin.
        /// </summary>
        /// <param name="points">The array of points to rotate.</param>
        /// <param name="angle">The angle to rotate the points in pi radians.</param>
        public static void RotatePoints(this Point[] points, double angle)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = RotatePoint(points[i], angle);
            }
        }

        /// <summary>
        /// Rotate a series of points around the world origin.
        /// </summary>
        /// <param name="points">The array of points to rotate.</param>
        /// <param name="angle">The angle to rotate the points in pi radians.</param>
        public static void RotatePoints(this PointF[] points, double angle)
        {
            for (int i = 0; i < points.Length; i++)
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
        public static void RotatePoints(this Point[] points, Point fulcrum, double angle)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = RotatePoint(points[i], fulcrum, angle);
            }
        }

        /// <summary>
        /// Rotate a series of points around a fulcrum point.
        /// </summary>
        /// <param name="points">The array of points to rotate.</param>
        /// <param name="fulcrum">The point to rotate all other points around.</param>
        /// <param name="angle">The angle to rotate the points in pi radians.</param>
        public static void RotatePoints(this PointF[] points, PointF fulcrum, double angle)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = RotatePoint(points[i], fulcrum, angle);
            }
        }
        #endregion

        #region Scale
        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="x">The x value to inflate.</param>
        /// <param name="y">The y value to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Scale(int x, int y, int factor)
        {
            return new Point(x * factor, y * factor);
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="x">The x value to inflate.</param>
        /// <param name="y">The y value to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Scale(int x, int y, float factor)
        {
            return new Point((int)(x * factor), (int)(y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="x">The x value to inflate.</param>
        /// <param name="y">The y value to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Scale(int x, int y, double factor)
        {
            return new Point((int)(x * factor), (int)(y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="x">The x value to inflate.</param>
        /// <param name="y">The y value to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Scale(float x, float y, float factor)
        {
            return new PointF((x * factor), (y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="x">The x value to inflate.</param>
        /// <param name="y">The y value to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Scale(double x, double y, double factor)
        {
            return new Point2D((float)(x * factor), (float)(y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Scale(this Point point, int factor)
        {
            return Scale(point.X, point.Y, factor);
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Scale(this Point point, float factor)
        {
            return Scale(point.X, point.Y, factor);
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Scale(this Point point, double factor)
        {
            return Scale(point.X, point.Y, factor);
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Scale(this PointF point, int factor)
        {
            return Scale(point.X, point.Y, factor);
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Scale(this PointF point, float factor)
        {
            return Scale(point.X, point.Y, factor);
        }

        ///// <summary>
        ///// Inflates a <see cref="Point"/> by a given factor.
        ///// </summary>
        ///// <param name="point">The <see cref="Point"/> to inflate.</param>
        ///// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        ///// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static PointF Scale(this PointF point, double factor)
        //{
        //    return Scale(point.X, point.Y, factor);
        //}

        /// <summary>
        /// Inflates a <see cref="Point2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point2D"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="double"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Scale(this Point2D point, double factor)
        {
            return Scale(point.X, point.Y, factor);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Scale(this Vector2D value, int factor)
        {
            return Scale(value.I, value.J, factor);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Scale(this Vector2D value, float factor)
        {
            return Scale(value.I, value.J, factor);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Scale(this Vector2D value, double factor)
        {
            return Scale(value.I, value.J, factor);
        }
        #endregion

        #region Slope

        /// <summary>
        /// Calculates the Slope of two points.
        /// </summary>
        /// <param name="PointA">Starting Point</param>
        /// <param name="PointB">Ending Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>The slope is calculated with Slope = (YB - YA) / (XB - XA) or rise over run</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(this Point PointA, Point PointB)
        {
            return Maths.Slope(PointA.X, PointA.Y, PointB.X, PointB.Y);
        }

        /// <summary>
        /// Calculates the Slope of two points.
        /// </summary>
        /// <param name="PointA">Starting Point</param>
        /// <param name="PointB">Ending Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>The slope is calculated with Slope = (YB - YA) / (XB - XA) or rise over run</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(this PointF PointA, PointF PointB)
        {
            return Maths.Slope(PointA.X, PointA.Y, PointB.X, PointB.Y);
        }

        /// <summary>
        /// Calculates the Slope of two points.
        /// </summary>
        /// <param name="PointA">Starting Point</param>
        /// <param name="PointB">Ending Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>The slope is calculated with Slope = (YB - YA) / (XB - XA) or rise over run</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(this Point2D PointA, Point2D PointB)
        {
            return Maths.Slope(PointA.X, PointA.Y, PointB.X, PointB.Y);
        }

        /// <summary>
        /// Calculates the Slope of a vector.
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>The slope is calculated with Slope = Y / X or rise over run</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(this Vector2D Point)
        {
            return Maths.Slope(Point.I, Point.J);
        }

        /// <summary>
        /// Returns the slope angle of a line.
        /// </summary>
        /// <param name="Line">Line to get length of</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(this LineSegment Line)
        {
            return Maths.Slope(Line.A.X, Line.A.Y, Line.B.X, Line.B.Y);
        }

        #endregion

        #region Subtract

        /// <summary>
        /// Subtracts a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="vaule">The <see cref="Point2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure reduced by the amount provided.</returns>
        public static Point2D Subtract(this Point2D vaule, double subend)
        {
            return new Point2D(vaule.X - subend, vaule.Y - subend);
        }

        /// <summary>
        /// Subtracts a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="value">The <see cref="Point2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure reduced by the amount provided.</returns>
        public static Vector2D Subtract(this Point2D value, Point2D subend)
        {
            return new Vector2D(value.X - subend.X, value.Y - subend.Y);
        }

        /// <summary>
        /// Subtracts a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="value">The <see cref="Point2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure reduced by the amount provided.</returns>
        public static Point2D Subtract(this Point2D value, Size2D subend)
        {
            return new Point2D(value.X - subend.Width, value.Y - subend.Height);
        }

        /// <summary>
        /// Subtracts a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="value">The <see cref="Point2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Vector2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure reduced by the amount provided.</returns>
        public static Point2D Subtract(this Point2D value, Vector2D subend)
        {
            return new Point2D(value.X - subend.I, value.Y - subend.J);
        }

        /// <summary>
        /// Subtracts a <see cref="Size2D"/> by a value.
        /// </summary>
        /// <param name="vaule">The <see cref="Size2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure reduced by the amount provided.</returns>
        public static Size2D Subtract(this Size2D vaule, double subend)
        {
            return new Size2D(vaule.Width - subend, vaule.Height - subend);
        }

        /// <summary>
        /// Subtracts a <see cref="Size2D"/> by a value.
        /// </summary>
        /// <param name="value">The <see cref="Size2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure reduced by the amount provided.</returns>
        public static Size2D Subtract(this Size2D value, Point2D subend)
        {
            return new Size2D(value.Width - subend.X, value.Height - subend.Y);
        }

        /// <summary>
        /// Subtracts a <see cref="Size2D"/> by a value.
        /// </summary>
        /// <param name="value">The <see cref="Size2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure reduced by the amount provided.</returns>
        public static Size2D Subtract(this Size2D value, Size2D subend)
        {
            return new Size2D(value.Width - subend.Width, value.Height - subend.Height);
        }

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static Point Subtract(this Point point, int value)
        {
            return new Point(point.X - value, point.Y - value);
        }

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static Point Subtract(this Point point, float value)
        {
            return new Point((int)(point.X - value), (int)(point.Y - value));
        }

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static Point Subtract(this Point point, double value)
        {
            return new Point((int)(point.X - value), (int)(point.Y - value));
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point Subtract(this Point point, Size value)
        {
            return new Point(point.X - value.Width, point.Y - value.Height);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point Subtract(this Point point, SizeF value)
        {
            return new Point((int)(point.X - value.Width), (int)(point.Y - value.Height));
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point Subtract(this Point point, Vector2D value)
        {
            return new Point((int)(point.X - value.I), (int)(point.Y - value.J));
        }

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static PointF Subtract(this PointF point, int value)
        {
            return new PointF(point.X - value, point.Y - value);
        }

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static PointF Subtract(this PointF point, float value)
        {
            return new PointF(point.X - value, point.Y - value);
        }

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static PointF Subtract(this PointF point, double value)
        {
            return new PointF((float)(point.X - value), (float)(point.Y - value));
        }

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static Vector2D Subtract(this PointF point, Point value)
        {
            return new PointF(point.X - value.X, point.Y - value.Y);
        }

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static Vector2D Subtract(this PointF point, PointF value)
        {
            return new PointF(point.X - value.X, point.Y - value.Y);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this PointF point, Size value)
        {
            return new PointF(point.X - value.Width, point.Y - value.Height);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this PointF point, SizeF value)
        {
            return new PointF((point.X - value.Width), (point.Y - value.Height));
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this PointF point, Vector2D Value)
        {
            return new PointF((float)(point.X - Value.I), (float)(point.Y - Value.J));
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D Subtract(this Vector2D vector, double value)
        {
            return new Vector2D(vector.I - value, vector.J - value);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this Vector2D vector, Point value)
        {
            return new PointF((float)(vector.I - value.X), (float)(vector.J - value.Y));
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D Subtract(this Vector2D vector, Point2D value)
        {
            return new Point2D((vector.I - value.X), (vector.J - value.Y));
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this Vector2D vector, PointF Value)
        {
            return new PointF((float)(vector.I - Value.X), (float)(vector.J - Value.Y));
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D Subtract(this Vector2D point, Size value)
        {
            return new Vector2D(point.I - value.Width, point.J - value.Height);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D Subtract(this Vector2D point, SizeF value)
        {
            return new Vector2D((point.I - value.Width), (point.J - value.Height));
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D Subtract(this Vector2D vector, Vector2D Value)
        {
            return new Vector2D(vector.I - Value.I, vector.J - Value.J);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static LineSegment Subtract(this LineSegment segment, LineSegment value)
        {
            return new LineSegment(
                segment.Points[0].X - value.Points[0].X,
                segment.Points[0].Y - value.Points[0].Y,
                segment.Points[1].X - value.Points[1].X,
                segment.Points[1].Y - value.Points[1].Y);
        }
        #endregion

        #region Unit

        /// <summary>
        /// Unit of a Point
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point Unit(this Point value)
        {
            return value.Scale((float)(1 / Sqrt((value.X * value.X) + (value.Y * value.Y))));
        }

        /// <summary>
        /// Unit of a Point
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Unit(this PointF value)
        {
            return value.Scale((float)(1 / Sqrt((value.X * value.X) + (value.Y * value.Y))));
        }

        /// <summary>
        /// Unit of a Point
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size Unit(this Size value)
        {
            return value.Inflate((float)(1 / Sqrt((value.Width * value.Width) + (value.Height * value.Height))));
        }

        /// <summary>
        /// Unit of a Point
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static SizeF Unit(this SizeF value)
        {
            return value.Inflate((float)(1 / Sqrt((value.Width * value.Width) + (value.Height * value.Height))));
        }

        /// <summary>
        /// Unit of a Vector
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D Unit(this Vector2D value)
        {
            return value.Scale(1 / Sqrt(((value.I * value.I) + (value.J * value.J))));
        }

        #endregion
    }
}
