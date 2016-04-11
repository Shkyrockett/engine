// <copyright file="PrimitivesExtensions.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

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
        public static double AbsoluteAngle(this Point pointA, Point pointB)
        {
            // Find the angle of point a and point b. 
            double test = -Angle(pointA, pointB) % Math.PI;

            // This should only loop once using the modulus of pi.
            while (test < 0)
            {
                test += Math.PI;
            }

            return test;
        }

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="pointA">First Point.</param>
        /// <param name="pointB">Second Point.</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        public static double AbsoluteAngle(this PointF pointA, PointF pointB)
        {
            // Find the angle of point a and point b. 
            double test = -Angle(pointA, pointB) % Math.PI;

            // This should only loop once using the modulus of pi.
            while (test < 0)
            {
                test += Math.PI;
            }

            return test;
        }

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="segment">Line segment.</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        public static double AbsoluteAngle(this LineSegment segment)
        {
            // Find the angle of point a and point b. 
            double test = -Angle(segment.A, segment.B) % Math.PI;

            // This should only loop once using the modulus of pi.
            while (test < 0)
            {
                test += Math.PI;
            }

            return test;
        }
        #endregion

        #region Add
        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, int addendum)
        {
            return new Point(point.X + addendum, point.Y + addendum);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, float addendum)
        {
            return new Point((int)(point.X + addendum), (int)(point.Y + addendum));
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, double addendum)
        {
            return new Point((int)(point.X + addendum), (int)(point.Y + addendum));
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, Point addendum)
        {
            return new Point(point.X + addendum.X, point.Y + addendum.Y);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, PointF addendum)
        {
            return new Point((int)(point.X + addendum.X), (int)(point.Y + addendum.Y));
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, Size addendum)
        {
            return new Point(point.X + addendum.Width, point.Y + addendum.Height);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, SizeF addendum)
        {
            return new Point((int)(point.X + addendum.Width), (int)(point.Y + addendum.Height));
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, Vector addendum)
        {
            return new Point(point.X + addendum.X, point.Y + addendum.Y);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(this Point point, VectorF addendum)
        {
            return new Point((int)(point.X + addendum.X), (int)(point.Y + addendum.Y));
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, int addendum)
        {
            return new PointF(point.X + addendum, point.Y + addendum);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, float addendum)
        {
            return new PointF(point.X + addendum, point.Y + addendum);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, double addendum)
        {
            return new PointF((float)(point.X + addendum), (float)(point.Y + addendum));
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Add(this PointF point, PointF addendum)
        {
            return new VectorF(point.X + addendum.X, point.Y + addendum.Y);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, Size addendum)
        {
            return new PointF(point.X + addendum.Width, point.Y + addendum.Height);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, SizeF addendum)
        {
            return new PointF(point.X + addendum.Width, point.Y + addendum.Height);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, Vector addendum)
        {
            return new PointF(point.X + addendum.X, point.Y + addendum.Y);
        }

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, VectorF addendum)
        {
            return new PointF(point.X + addendum.X, point.Y + addendum.Y);
        }

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Add(this Vector vector, int value)
        {
            return new Vector((vector.X + value), (vector.Y + value));
        }

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Add(this Vector vector, float value)
        {
            return new Vector((int)(vector.X + value), (int)(vector.Y + value));
        }

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Add(this Vector vector, double value)
        {
            return new Vector((int)(vector.X + value), (int)(vector.Y + value));
        }

        /// <summary>
        /// Add Vector2D
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Add(this Vector vector, Point value)
        {
            return new Vector(vector.X + value.X, vector.Y + value.Y);
        }

        /// <summary>
        /// Add Vector2D
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Add(this Vector vector, PointF value)
        {
            return new Vector((int)(vector.X + value.X), (int)(vector.Y + value.Y));
        }

        /// <summary>
        /// Add Vector2D
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Add(this Vector vector, Vector value)
        {
            return new Vector((int)(vector.X + value.X), (int)(vector.Y + value.Y));
        }

        /// <summary>
        /// Add Vector2D
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Add(this Vector vector, VectorF value)
        {
            return new Vector((int)(vector.X + value.X), (int)(vector.Y + value.Y));
        }

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Add(this VectorF vector, int value)
        {
            return new VectorF(vector.X + value, vector.Y + value);
        }

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Add(this VectorF vector, float value)
        {
            return new VectorF(vector.X + value, vector.Y + value);
        }

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Add(this VectorF vector, double value)
        {
            return new VectorF((float)(vector.X + value), (float)(vector.Y + value));
        }

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this VectorF vector, Point value)
        {
            return new PointF(vector.X + value.X, vector.Y + value.Y);
        }

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this VectorF vector, PointF value)
        {
            return new PointF(vector.X + value.X, vector.Y + value.Y);
        }

        /// <summary>
        /// Add Vector2D
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Add(this VectorF vector, VectorF value)
        {
            return new VectorF(vector.X + value.X, vector.Y + value.Y);
        }

        /// <summary>
        /// Add Vector2D
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Add(this VectorF vector, Vector value)
        {
            return new VectorF(vector.X + value.X, vector.Y + value.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment Add(this LineSegment segment, LineSegment value)
        {
            return new LineSegment(
                segment.Points[0].X + value.Points[0].X,
                segment.Points[0].Y + value.Points[0].Y,
                segment.Points[1].X + value.Points[1].X,
                segment.Points[1].Y + value.Points[1].Y);
        }
        #endregion

        #region Angle
        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="X1">Horizontal Component of Point Starting Point</param>
        /// <param name="Y1">Vertical Component of Point Starting Point</param>
        /// <param name="X2">Horizontal Component of Ending Point</param>
        /// <param name="Y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(int X1, int Y1, int X2, int Y2)
        {
            return Math.Atan2((Y1 - Y2), (X1 - X2));
        }

        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="X1">Horizontal Component of Point Starting Point</param>
        /// <param name="Y1">Vertical Component of Point Starting Point</param>
        /// <param name="X2">Horizontal Component of Ending Point</param>
        /// <param name="Y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(float X1, float Y1, float X2, float Y2)
        {
            return Math.Atan2((Y1 - Y2), (X1 - X2));
        }

        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="X1">Horizontal Component of Point Starting Point</param>
        /// <param name="Y1">Vertical Component of Point Starting Point</param>
        /// <param name="X2">Horizontal Component of Ending Point</param>
        /// <param name="Y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(double X1, double Y1, double X2, double Y2)
        {
            return Math.Atan2((Y1 - Y2), (X1 - X2));
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
            return Angle(PointA.X, PointA.Y, PointB.X, PointB.Y);
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
            return Angle(PointA.X, PointA.Y, PointB.X, PointB.Y);
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
            return Angle(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);
        }
        #endregion

        #region Cross Product
        /// <summary>
        /// Cross Product of two points.
        /// </summary>
        /// <param name="xA">First Point X component.</param>
        /// <param name="yA">First Point Y component.</param>
        /// <param name="xB">Second Point X component.</param>
        /// <param name="yB">Second Point Y component.</param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks>Note that AB · BC = |AB| * |BC| * Cos(theta).</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct(int xA, int yA, int xB, int yB)
        {
            return (xA * yB) - (yA * xB);
        }

        /// <summary>
        /// Cross Product of two points.
        /// </summary>
        /// <param name="xA">First Point X component.</param>
        /// <param name="yA">First Point Y component.</param>
        /// <param name="xB">Second Point X component.</param>
        /// <param name="yB">Second Point Y component.</param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks>Note that AB · BC = |AB| * |BC| * Cos(theta).</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct(float xA, float yA, float xB, float yB)
        {
            return (xA * yB) - (yA * xB);
        }

        /// <summary>
        /// Cross Product of two points.
        /// </summary>
        /// <param name="xA">First Point X component.</param>
        /// <param name="yA">First Point Y component.</param>
        /// <param name="xB">Second Point X component.</param>
        /// <param name="yB">Second Point Y component.</param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks>Note that AB · BC = |AB| * |BC| * Cos(theta).</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct(double xA, double yA, double xB, double yB)
        {
            return (xA * yB) - (yA * xB);
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
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
        public static double CrossProduct(this Point point, Vector value)
        {
            return CrossProduct(point.X, point.Y, value.X, value.Y);
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
        public static double CrossProduct(this Point point, VectorF value)
        {
            return CrossProduct(point.X, point.Y, value.X, value.Y);
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
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
        public static double CrossProduct(this PointF valueA, Vector valueB)
        {
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
        public static double CrossProduct(this PointF valueA, VectorF valueB)
        {
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
        public static double CrossProduct(this Vector valueA, Point valueB)
        {
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
        public static double CrossProduct(this Vector valueA, PointF valueB)
        {
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
        public static double CrossProduct(this Vector valueA, Vector valueB)
        {
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
        public static double CrossProduct(this Vector valueA, VectorF valueB)
        {
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
        public static double CrossProduct(this VectorF valueA, Point valueB)
        {
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
        public static double CrossProduct(this VectorF valueA, PointF valueB)
        {
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
        public static double CrossProduct(this VectorF valueA, Vector valueB)
        {
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
        public static double CrossProduct(this VectorF valueA, VectorF valueB)
        {
            return CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);
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
        public static Vector Delta(this Point value1, Point value2)
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
        public static VectorF Delta(this PointF value1, PointF value2)
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
        public static Vector Delta(this Vector vector, Vector value)
        {
            return value - vector;
        }

        /// <summary>
        /// Finds the Delta of two Vectors
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value">Second Point</param>
        /// <returns>Returns the Difference Between PointA and PointB</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Delta(this VectorF vector, VectorF value)
        {
            return value - vector;
        }

        #endregion

        #region Distance
        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="xA">First Point X component.</param>
        /// <param name="yA">First Point Y component.</param>
        /// <param name="xB">Second Point X component.</param>
        /// <param name="yB">Second Point Y component.</param>
        /// <returns>The distance between two points.</returns>
        /// <remarks>
        /// Source: http://www.vcskicks.com/code-snippet/distance-formula.php.
        /// Pythagorean theorem c^2 = a^2 + b^2
        /// thus c = square root(a^2 + b^2)
        /// </remarks>
        public static double DistanceV1(int xA, int yA, int xB, int yB)
        {
            double a = (xA - xB);
            double b = (yA - yB);
            return Math.Sqrt(a * a + b * b);
        }

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="xA">First Point X component.</param>
        /// <param name="yA">First Point Y component.</param>
        /// <param name="xB">Second Point X component.</param>
        /// <param name="yB">Second Point Y component.</param>
        /// <returns>The distance between two points.</returns>
        /// <remarks>
        /// Source: http://www.vcskicks.com/code-snippet/distance-formula.php.
        /// Pythagorean theorem c^2 = a^2 + b^2
        /// thus c = square root(a^2 + b^2)
        /// </remarks>
        public static double DistanceV1(float xA, float yA, float xB, float yB)
        {
            double a = (xA - xB);
            double b = (yA - yB);
            return Math.Sqrt(a * a + b * b);
        }

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="xA">First Point X component.</param>
        /// <param name="yA">First Point Y component.</param>
        /// <param name="xB">Second Point X component.</param>
        /// <param name="yB">Second Point Y component.</param>
        /// <returns>The distance between two points.</returns>
        /// <remarks>
        /// Source: http://www.vcskicks.com/code-snippet/distance-formula.php.
        /// Pythagorean theorem c^2 = a^2 + b^2
        /// thus c = square root(a^2 + b^2)
        /// </remarks>
        public static double DistanceV1(double xA, double yA, double xB, double yB)
        {
            double a = (xA - xB);
            double b = (yA - yB);
            return Math.Sqrt(a * a + b * b);
        }

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="xA">First Point X component.</param>
        /// <param name="yA">First Point Y component.</param>
        /// <param name="xB">Second Point X component.</param>
        /// <param name="yB">Second Point Y component.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double DistanceV2(double xA, double yA, double xB, double yB)
        {
            return Modulus(xA - xB, yA - yB);
        }

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Distance(this Point point, Point value)
        {
            return DistanceV1(point.X, point.Y, value.X, value.Y);
        }

        /// <summary>
        /// Length of a Vector
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Distance(this PointF point, PointF value)
        {
            return DistanceV1(point.X, point.Y, value.X, value.Y);
        }

        /// <summary>
        /// Length of a Vector
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Distance(this Vector value1, Vector value2)
        {
            return DistanceV1(value1.X, value1.Y, value2.X, value2.Y);
        }

        /// <summary>
        /// Length of a Vector
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Distance(this VectorF value1, VectorF value2)
        {
            return DistanceV1(value1.X, value1.Y, value2.X, value2.Y);
        }

        /// <summary>
        /// Length of a Segment
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Distance(this LineSegment segment)
        {
            return DistanceV1(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);
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
        public static double Divide(this Vector Value1, Vector Value2)
        {
            return Value1.DotProduct(Value2.Invert());
        }

        /// <summary>
        /// Divide Point
        /// </summary>
        /// <param name="Value1">First Point</param>
        /// <param name="Value2">Second Point</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Divide(this VectorF Value1, VectorF Value2)
        {
            return Value1.DotProduct(Value2.Invert());
        }
        #endregion

        #region Dot Product
        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="xA">First Point X component.</param>
        /// <param name="yA">First Point Y component.</param>
        /// <param name="xB">Second Point X component.</param>
        /// <param name="yB">Second Point Y component.</param>
        /// <returns>The Dot Product.</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(int xA, int yA, int xB, int yB)
        {
            return ((xA * xB) + (yA * yB));
        }

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="xA">First Point X component.</param>
        /// <param name="yA">First Point Y component.</param>
        /// <param name="xB">Second Point X component.</param>
        /// <param name="yB">Second Point Y component.</param>
        /// <returns>The Dot Product.</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(float xA, float yA, float xB, float yB)
        {
            return ((xA * xB) + (yA * yB));
        }

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="xA">First Point X component.</param>
        /// <param name="yA">First Point Y component.</param>
        /// <param name="xB">Second Point X component.</param>
        /// <param name="yB">Second Point Y component.</param>
        /// <returns>The Dot Product.</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(double xA, double yA, double xB, double yB)
        {
            return ((xA * xB) + (yA * yB));
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
            return DotProduct(value.X, value.Y, value.X, value.Y);
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
            return DotProduct(value.X, value.Y, value.X, value.Y);
        }

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="value">Starting Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Vector value)
        {
            return DotProduct(value.X, value.Y, value.X, value.Y);
        }

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="value">Starting Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this VectorF value)
        {
            return DotProduct(value.X, value.Y, value.X, value.Y);
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
            return DotProduct(point.X, point.Y, value.X, value.Y);
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
            return DotProduct(point.X, point.Y, value.X, value.Y);
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
        public static double DotProduct(this Point point, Vector vector)
        {
            return DotProduct(point.X, point.Y, vector.X, vector.Y);
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
        public static double DotProduct(this Point point, VectorF vector)
        {
            return DotProduct(point.X, point.Y, vector.X, vector.Y);
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
            return DotProduct(point.X, point.Y, value.X, value.Y);
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
            return DotProduct(point.X, point.Y, value.X, value.Y);
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
        public static double DotProduct(this PointF point, Vector vector)
        {
            return DotProduct(point.X, point.Y, vector.X, vector.Y);
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
        public static double DotProduct(this PointF point, VectorF vector)
        {
            return DotProduct(point.X, point.Y, vector.X, vector.Y);
        }

        /// <summary>
        /// Determines the dot product of two 2D vectors.
        /// </summary>
        /// <param name="vector">First Point.</param>
        /// <param name="value">Second Point.</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Vector vector, Point value)
        {
            return DotProduct(vector.X, vector.Y, value.X, value.Y);
        }

        /// <summary>
        /// Determines the dot product of two 2D vectors.
        /// </summary>
        /// <param name="vector">First Point.</param>
        /// <param name="value">Second Point.</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Vector vector, PointF value)
        {
            return DotProduct(vector.X, vector.Y, value.X, value.Y);
        }

        /// <summary>
        /// Determines the dot product of two 2D vectors.
        /// </summary>
        /// <param name="vector">First Point.</param>
        /// <param name="value">Second Point.</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Vector vector, Vector value)
        {
            return DotProduct(vector.X, vector.Y, value.X, value.Y);
        }

        /// <summary>
        /// Determines the dot product of two 2D vectors.
        /// </summary>
        /// <param name="vector">First Point.</param>
        /// <param name="value">Second Point.</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Vector vector, VectorF value)
        {
            return DotProduct(vector.X, vector.Y, value.X, value.Y);
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
        public static double DotProduct(this VectorF vector, Point value)
        {
            return DotProduct(vector.X, vector.Y, value.X, value.Y);
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
        public static double DotProduct(this VectorF vector, PointF value)
        {
            return DotProduct(vector.X, vector.Y, value.X, value.Y);
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
        public static double DotProduct(this VectorF vector, Vector value)
        {
            return DotProduct(vector.X, vector.Y, value.X, value.Y);
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
        public static double DotProduct(this VectorF vector, VectorF value)
        {
            return DotProduct(vector.X, vector.Y, value.X, value.Y);
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
        public static Point Inflate(this Point point, Vector factors)
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
        public static Point Inflate(this Point point, VectorF factors)
        {
            return new Point((int)(point.X * factors.X), (int)(point.Y * factors.Y));
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
        public static PointF Inflate(this PointF point, Vector factors)
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
        public static PointF Inflate(this PointF point, VectorF factors)
        {
            return new PointF((point.X * factors.X), (point.Y * factors.Y));
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
        public static Size Inflate(this Size size, Vector factor)
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
        public static Size Inflate(this Size size, VectorF factor)
        {
            return new Size((int)(size.Width * factor.X), (int)(size.Height * factor.Y));
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
        public static SizeF Inflate(this SizeF size, Vector factor)
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
        public static SizeF Inflate(this SizeF size, VectorF factor)
        {
            return new SizeF(size.Width * factor.X, size.Height * factor.Y);
        }

        /// <summary>
        /// Inflates a <see cref="Vector"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Inflate(this Vector point, int factor)
        {
            return new Vector((point.X * factor), (point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Vector"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Inflate(this Vector point, float factor)
        {
            return new Vector((int)(point.X * factor), (int)(point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Vector"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Inflate(this Vector point, double factor)
        {
            return new Vector((int)(point.X * factor), (int)(point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Vector"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Inflate(this Vector point, Point factors)
        {
            return new Vector((point.X * factors.X), (point.Y * factors.Y));
        }

        /// <summary>
        /// Inflates a <see cref="Vector"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Inflate(this Vector point, PointF factors)
        {
            return new Vector((int)(point.X * factors.X), (int)(point.Y * factors.Y));
        }

        /// <summary>
        /// Inflates a <see cref="Vector"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Vector"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="Vector"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Inflate(this Vector size, Size factor)
        {
            return new Vector(size.X * factor.Width, size.Y * factor.Height);
        }

        /// <summary>
        /// Inflates a <see cref="Vector"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="Vector"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Inflate(this Vector point, SizeF factor)
        {
            return new Vector((int)(point.X * factor.Width), (int)(point.Y * factor.Height));
        }

        /// <summary>
        /// Inflates a <see cref="Vector"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Inflate(this Vector point, Vector factors)
        {
            return new Vector((point.X * factors.X), (point.Y * factors.Y));
        }

        /// <summary>
        /// Inflates a <see cref="Vector"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Inflate(this Vector point, VectorF factors)
        {
            return new Vector((int)(point.X * factors.X), (int)(point.Y * factors.Y));
        }

        /// <summary>
        /// Inflates a <see cref="VectorF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="VectorF"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="VectorF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Inflate(this VectorF point, int factor)
        {
            return new VectorF((point.X * factor), (point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="VectorF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="VectorF"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="VectorF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Inflate(this VectorF point, float factor)
        {
            return new VectorF((point.X * factor), (point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="VectorF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="VectorF"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="VectorF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Inflate(this VectorF point, double factor)
        {
            return new VectorF((float)(point.X * factor), (float)(point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="VectorF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="VectorF"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="VectorF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Inflate(this VectorF point, Point factors)
        {
            return new VectorF((point.X * factors.X), (point.Y * factors.Y));
        }

        /// <summary>
        /// Inflates a <see cref="VectorF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="VectorF"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="VectorF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Inflate(this VectorF point, PointF factors)
        {
            return new VectorF((point.X * factors.X), (point.Y * factors.Y));
        }

        /// <summary>
        /// Inflates a <see cref="VectorF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="VectorF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="VectorF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Inflate(this VectorF size, Size factor)
        {
            return new VectorF(size.X * factor.Width, size.Y * factor.Height);
        }

        /// <summary>
        /// Inflates a <see cref="VectorF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="VectorF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="VectorF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Inflate(this VectorF point, SizeF factor)
        {
            return new VectorF(point.X * factor.Width, point.Y * factor.Height);
        }

        /// <summary>
        /// Inflates a <see cref="VectorF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="VectorF"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="VectorF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Inflate(this VectorF point, Vector factors)
        {
            return new VectorF((point.X * factors.X), (point.Y * factors.Y));
        }

        /// <summary>
        /// Inflates a <see cref="VectorF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="VectorF"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="VectorF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Inflate(this VectorF point, VectorF factors)
        {
            return new VectorF((point.X * factors.X), (point.Y * factors.Y));
        }
        #endregion

        #region Invert
        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Invert(int x, int y)
        {
            return new Vector((1 / x), (1 / y));
        }

        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Invert(float x, float y)
        {
            return new VectorF((1 / x), (1 / y));
        }

        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Invert(double x, double y)
        {
            return new VectorF((1 / x), (1 / y));
        }

        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Invert(this Point value)
        {
            return Invert(value.X, value.Y);
        }

        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Invert(this PointF value)
        {
            return Invert(value.X, value.Y);
        }

        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Invert(this Vector value)
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
        public static VectorF Invert(this VectorF value)
        {
            return Invert(value.X, value.Y);
        }
        #endregion

        #region Length
        /// <summary>
        /// Returns the Length of a lineSeg.
        /// </summary>
        /// <param name="xA">Horizontal Component of Point Starting Point</param>
        /// <param name="yA">Vertical Component of Point Starting Point</param>
        /// <param name="xB">Horizontal Component of Ending Point</param>
        /// <param name="yB">Vertical Component of Ending Point</param>
        /// <returns>Returns the Length of a lineSeg.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(int xA, int yA, int xB, int yB)
        {
            return Math.Sqrt(Math.Pow(xB - xA, 2) + Math.Pow(yB - yA, 2));
        }

        /// <summary>
        /// Returns the Length of a lineSeg.
        /// </summary>
        /// <param name="xA">Horizontal Component of Point Starting Point</param>
        /// <param name="yA">Vertical Component of Point Starting Point</param>
        /// <param name="xB">Horizontal Component of Ending Point</param>
        /// <param name="yB">Vertical Component of Ending Point</param>
        /// <returns>Returns the Length of a lineSeg.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(float xA, float yA, float xB, float yB)
        {
            return Math.Sqrt(Math.Pow(xB - xA, 2) + Math.Pow(yB - yA, 2));
        }

        /// <summary>
        /// Returns the Length of a lineSeg.
        /// </summary>
        /// <param name="xA">Horizontal Component of Point Starting Point</param>
        /// <param name="yA">Vertical Component of Point Starting Point</param>
        /// <param name="xB">Horizontal Component of Ending Point</param>
        /// <param name="yB">Vertical Component of Ending Point</param>
        /// <returns>Returns the Length of a lineSeg.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(double xA, double yA, double xB, double yB)
        {
            return Math.Sqrt(Math.Pow(xB - xA, 2) + Math.Pow(yB - yA, 2));
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
            return Length(point.X, point.Y, value.X, value.Y);
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
            return Length(point.X, point.Y, value.X, value.Y);
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
        public static double Length(this VectorF vector, VectorF value)
        {
            return Length(vector.X, vector.Y, value.X, value.Y);
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
            return Length(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);
        }
        #endregion

        #region Modulus
        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulus(int x, int y)
        {
            return Math.Pow((x * x) + (y * y), 0.5F);
        }

        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulus(float x, float y)
        {
            return Math.Pow((x * x) + (y * y), 0.5F);
        }

        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulus(double x, double y)
        {
            return Math.Pow((x * x) + (y * y), 0.5F);
        }

        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulus(this Vector value)
        {
            return Modulus(value.X, value.Y);
        }

        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulus(this VectorF value)
        {
            return Modulus(value.X, value.Y);
        }
        #endregion

        #region Multiply
        /// <summary>
        /// Multiply: Point * Matrix
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerStepThrough]
        public static PointF Multiply(this PointF point, MatrixF matrix)
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
            return point.Scale((float)((1 / Math.Sqrt(((point.X * value.Width) + (point.Y * value.Height))))));
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
        public static PointF[] Offset(this PointF point, PointF value, float distance)
        {
            VectorF UnitVectorAB = new VectorF(point, value);
            VectorF PerpendicularAB = UnitVectorAB.Perpendicular().Scale(0.5).Scale(distance);
            PointF[] Out = new PointF[] { point + PerpendicularAB, value + PerpendicularAB };
            if ((distance <= 0)) Array.Reverse(Out);
            return Out;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static LineSegment Offset(this LineSegment segment, float distance)
        {
            VectorF UnitVectorAB = new VectorF(segment.A, segment.B);
            VectorF PerpendicularAB = UnitVectorAB.Perpendicular().Scale(0.5).Scale(distance);
            LineSegment Out = new LineSegment(segment.A + PerpendicularAB, segment.B + PerpendicularAB);
            if ((distance <= 0)) Out.Reverse();
            return Out;
        }
        #endregion

        #region Perpendicular Vector
        /// <summary>
        /// Perpendicular of a Vector.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Perpendicular(int x, int y)
        {
            return new Vector(y * -1, x);
        }

        /// <summary>
        /// Perpendicular of a Vector.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Perpendicular(float x, float y)
        {
            return new VectorF(y * -1, x);
        }

        /// <summary>
        /// Perpendicular of a Vector.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Perpendicular(double x, double y)
        {
            return new VectorF(y * -1, x);
        }

        /// <summary>
        /// Perpendicular of a Vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Perpendicular(this Vector vector)
        {
            return Perpendicular(vector.X, vector.Y);
        }

        /// <summary>
        /// Perpendicular of a Vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorF Perpendicular(this VectorF vector)
        {
            return Perpendicular(vector.X, vector.Y);
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
            VectorF SegmentVectorDelta = point.Delta(value);
            VectorF QC12 = new VectorF(
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
            VectorF SegmentVectorDelta = segment.A.Delta(segment.B);
            VectorF QC12 = new VectorF(
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
            Array.Reverse(segment.Points);
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

            double angleCos = Math.Cos(angle);
            double angleSin = Math.Sin(angle);

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

            double angleCos = Math.Cos(angle);
            double angleSin = Math.Sin(angle);

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
        public static PointF Scale(double x, double y, double factor)
        {
            return new PointF((float)(x * factor), (float)(y * factor));
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

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Scale(this PointF point, double factor)
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
        public static Vector Scale(this Vector value, int factor)
        {
            return Scale(value.X, value.Y, factor);
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
        public static Vector Scale(this Vector value, float factor)
        {
            return Scale(value.X, value.Y, factor);
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
        public static Vector Scale(this Vector value, double factor)
        {
            return Scale(value.X, value.Y, factor);
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
        public static VectorF Scale(this VectorF value, int factor)
        {
            return Scale(value.X, value.Y, factor);
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
        public static VectorF Scale(this VectorF value, float factor)
        {
            return Scale(value.X, value.Y, factor);
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
        public static VectorF Scale(this VectorF value, double factor)
        {
            return Scale(value.X, value.Y, factor);
        }
        #endregion

        #region Slope
        /// <summary>
        /// Returns the slope angle of a line.
        /// </summary>
        /// <param name="X1">Horizontal Component of Point Starting Point</param>
        /// <param name="Y1">Vertical Component of Point Starting Point</param>
        /// <param name="X2">Horizontal Component of Ending Point</param>
        /// <param name="Y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks></remarks>
        public static double Slope(ref int X1, ref int Y1, ref int X2, ref int Y2)
        {
            //  Check to see if the Line is Vertical. 
            //  The original Version was: If (Line.A.X - Line.B.X) = 0 Then
            if ((X1 == X2))
            {
                //  Line is Vertical return something close to infinity (Close to 
                //  the largest value allowed for the data type).
                return MathExtensions.SlopeMax;
            }
            else
            {
                return ((Y2 - Y1) / (X2 - X1));
            }
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
        public static double Slope(ref float X1, ref float Y1, ref float X2, ref float Y2)
        {
            //  Check to see if the Line is Vertical. 
            //  The original Version was: If (Line.A.X - Line.B.X) = 0 Then
            if ((X1 == X2))
            {
                //  Vertical line check.
                //  Line is Vertical return something close to infinity (Close to 
                //  the largest value allowed for the data type).
                return MathExtensions.SlopeMax;
            }
            else
            {
                return ((Y2 - Y1)
                            / (X2 - X1));
            }
        }

        /// <summary>
        /// Calculates the Slope of two points.
        /// </summary>
        /// <param name="PointA">Starting Point</param>
        /// <param name="PointB">Ending Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>The slope is calculated with Slope = (YB - YA) / (XB - XA) or rise over run</remarks>
        public static double Slope(this Point PointA, Point PointB)
        {
            //  If the line is vertical, return something close to infinity 
            //  (Close to the largest value allowed for the data type).
            if ((PointA.X == PointB.X))
            {
                return MathExtensions.SlopeMax;
            }
            //  Otherwise calculate and return the slope.
            return ((PointB.Y - PointA.Y) / (PointB.X - PointA.X));
        }

        /// <summary>
        /// Calculates the Slope of two points.
        /// </summary>
        /// <param name="PointA">Starting Point</param>
        /// <param name="PointB">Ending Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>The slope is calculated with Slope = (YB - YA) / (XB - XA) or rise over run</remarks>
        public static double Slope(this PointF PointA, PointF PointB)
        {
            //  Check to see if the Line is Vertical. 
            //  The original Version was: If (Line.A.X - Line.B.X) = 0 Then
            if ((PointA.X == PointB.X))
            {
                //  If the line is vertical, return something close to infinity 
                //  (Close to the largest value allowed for the data type).
                return MathExtensions.SlopeMax;
            }

            //  Otherwise calculate and return the slope.
            return ((PointB.Y - PointA.Y) / (PointB.X - PointA.X));
        }

        /// <summary>
        /// Calculates the Slope of a vector.
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>The slope is calculated with Slope = Y / X or rise over run</remarks>
        public static double Slope(this Vector Point)
        {
            //  If the line is vertical, return something close to infinity 
            //  (Close to the largest value allowed for the data type).
            if ((Point.X == 0))
            {
                return MathExtensions.SlopeMax;
            }

            //  Otherwise calculate and return the slope.
            return (Point.Y / Point.X);
        }

        /// <summary>
        /// Calculates the Slope of a vector.
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>The slope is calculated with Slope = Y / X or rise over run</remarks>
        public static double Slope(this VectorF Point)
        {
            //  If the line is vertical, return something close to infinity 
            //  (Close to the largest value allowed for the data type).
            if ((Point.X == 0))
            {
                return MathExtensions.SlopeMax;
            }

            //  Otherwise calculate and return the slope.
            return (Point.Y / Point.X);
        }

        /// <summary>
        /// Returns the slope angle of a line.
        /// </summary>
        /// <param name="Line">Line to get length of</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks></remarks>
        public static double Slope(this LineSegment Line)
        {
            //  Check to see if the Line is Vertical. 
            //  The original Version was: If (Line.A.X - Line.B.X) = 0 Then
            if ((Line.A.X == Line.B.X))
            {
                //  Line is Vertical return something close to infinity (Close to 
                //  the largest value allowed for the data type).
                return MathExtensions.SlopeMax;
            }

            //  Otherwise calculate and return the slope.
            return ((Line.B.Y - Line.A.Y) / (Line.B.X - Line.A.X));
        }

        #endregion

        #region Subtract
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
        /// Subtracts a <see cref="Point"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static Vector Subtract(this Point point, Point value)
        {
            return new Vector(point.X - value.X, point.Y - value.Y);
        }

        /// <summary>
        /// Subtracts a <see cref="Point"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static Vector Subtract(this Point point, PointF value)
        {
            return new Vector((int)(point.X - value.X), (int)(point.Y - value.Y));
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
        public static Point Subtract(this Point point, Vector value)
        {
            return new Point(point.X - value.X, point.Y - value.Y);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point Subtract(this Point point, VectorF value)
        {
            return new Point((int)(point.X - value.X), (int)(point.Y - value.Y));
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
        public static VectorF Subtract(this PointF point, Point value)
        {
            return new PointF(point.X - value.X, point.Y - value.Y);
        }

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static VectorF Subtract(this PointF point, PointF value)
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
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this PointF point, Vector value)
        {
            return new PointF(point.X - value.X, point.Y - value.Y);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this PointF point, VectorF Value)
        {
            return new PointF(point.X - Value.X, point.Y - Value.Y);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector Subtract(this Vector vector, Point value)
        {
            return new Vector(vector.X - vector.X, vector.Y - vector.Y);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector Subtract(this Vector vector, PointF Value)
        {
            return new Vector((int)(vector.X - Value.X), (int)(vector.Y - Value.Y));
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector Subtract(this Vector point, Size value)
        {
            return new Vector(point.X - value.Width, point.Y - value.Height);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector Subtract(this Vector point, SizeF value)
        {
            return new Vector((int)(point.X - value.Width), (int)(point.Y - value.Height));
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector Subtract(this Vector vector, Vector Value)
        {
            return new Vector(vector.X - Value.X, vector.Y - Value.Y);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector Subtract(this Vector vector, VectorF Value)
        {
            return new Vector((int)(vector.X - Value.X), (int)(vector.Y - Value.Y));
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this VectorF vector, Point value)
        {
            return new PointF(vector.X - vector.X, vector.Y - vector.Y);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this VectorF vector, PointF Value)
        {
            return new PointF(vector.X - Value.X, vector.Y - Value.Y);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Subtract(this VectorF point, Size value)
        {
            return new VectorF(point.X - value.Width, point.Y - value.Height);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Subtract(this VectorF point, SizeF value)
        {
            return new VectorF((point.X - value.Width), (point.Y - value.Height));
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Subtract(this VectorF vector, Vector Value)
        {
            return new VectorF((vector.X - Value.X), (vector.Y - Value.Y));
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Subtract(this VectorF vector, VectorF Value)
        {
            return new VectorF(vector.X - Value.X, vector.Y - Value.Y);
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
            return value.Scale((float)(1 / Math.Sqrt((value.X * value.X) + (value.Y * value.Y))));
        }

        /// <summary>
        /// Unit of a Point
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Unit(this PointF value)
        {
            return value.Scale((float)(1 / Math.Sqrt((value.X * value.X) + (value.Y * value.Y))));
        }

        /// <summary>
        /// Unit of a Point
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size Unit(this Size value)
        {
            return value.Inflate((float)(1 / Math.Sqrt((value.Width * value.Width) + (value.Height * value.Height))));
        }

        /// <summary>
        /// Unit of a Point
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static SizeF Unit(this SizeF value)
        {
            return value.Inflate((float)(1 / Math.Sqrt((value.Width * value.Width) + (value.Height * value.Height))));
        }

        /// <summary>
        /// Unit of a Vector
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector Unit(this Vector value)
        {
            return value.Scale((int)(1 / Math.Sqrt(((value.X * value.X) + (value.Y * value.Y)))));
        }

        /// <summary>
        /// Unit of a Vector
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Unit(this VectorF value)
        {
            return value.Scale((float)(1 / Math.Sqrt(((value.X * value.X) + (value.Y * value.Y)))));
        }

        #endregion
    }
}
