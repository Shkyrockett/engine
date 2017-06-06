// <copyright file="PointFExtentions.cs" company="Shkyrockett" >
//     Copyright (c) 2013 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine.Imaging;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class PointFExtentions
    {
        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="pointA">First Point.</param>
        /// <param name="pointB">Second Point.</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteAngle(this PointF pointA, PointF pointB)
            => Maths.AbsoluteAngle(pointA.X, pointA.Y, pointB.X, pointB.Y);

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, int addend)
            => new PointF(point.X + addend, point.Y + addend);

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, float addend)
            => new PointF(point.X + addend, point.Y + addend);

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, double addend)
            => new PointF((float)(point.X + addend), (float)(point.Y + addend));

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, Size addend)
            => new PointF(point.X + addend.Width, point.Y + addend.Height);

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, SizeF addend)
            => new PointF(point.X + addend.Width, point.Y + addend.Height);

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(this PointF point, Vector2D addend)
            => new PointF((float)(point.X + addend.I), (float)(point.Y + addend.J));

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
            => Maths.Angle(PointA.X, PointA.Y, PointB.X, PointB.Y);

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
            => Maths.CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);

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
            => Maths.CrossProduct(valueA.X, valueA.Y, valueB.X, valueB.Y);

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
            => Maths.CrossProduct(valueA.X, valueA.Y, valueB.I, valueB.J);

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
            => value2.Subtract(value1);

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
            => Measurements.Distance(a.X, a.Y, b.X, b.Y);

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
            => Measurements.Distance(point.X, point.Y, value.X, value.Y);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="value">Starting Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this PointF value)
            => Maths.DotProduct(value.X, value.Y, value.X, value.Y);

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
        public static double DotProduct(this PointF point, PointF value)
            => Maths.DotProduct(point.X, point.Y, value.X, value.Y);

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
            => Maths.DotProduct(point.X, point.Y, vector.I, vector.J);

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, int factor)
            => new PointF((point.X * factor), (point.Y * factor));

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, float factor)
            => new PointF((point.X * factor), (point.Y * factor));

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, double factor)
            => new PointF((float)(point.X * factor), (float)(point.Y * factor));

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, Point factors)
            => new PointF((point.X * factors.X), (point.Y * factors.Y));

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, PointF factors)
            => new PointF((point.X * factors.X), (point.Y * factors.Y));

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF size, Size factor)
            => new PointF(size.X * factor.Width, size.Y * factor.Height);

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, SizeF factor)
            => new PointF(point.X * factor.Width, point.Y * factor.Height);

        /// <summary>
        /// Inflates a <see cref="PointF"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Inflate(this PointF point, Vector2D factors)
            => new PointF((float)(point.X * factors.I), (float)(point.Y * factors.J));

        /// <summary>
        /// Normalize Two Points
        /// </summary>
        /// <param name="point">First Point</param>
        /// <param name="value">Second Point</param>
        /// <returns>The Normal of two Points</returns>
        /// <remarks></remarks>
        public static PointF Normalize(this PointF point, SizeF value)
            => new Point2D(Maths.Normalize2D(point.X, point.Y, value.Width, value.Height)).ToPointF();

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
            var QC12 = new Vector2D(
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
            var QC12 = new Vector2D(
                segment.B.CrossProduct(segment.A),
                axis.DotProduct(SegmentVectorDelta)
                );
            double QC3 = 0.5F * SegmentVectorDelta.DotProduct(SegmentVectorDelta);
            return new PointF(
                (float)(QC3 * SegmentVectorDelta.CrossProduct(QC12) - axis.X),
                (float)(QC3 * SegmentVectorDelta.CrossProduct(QC12) - axis.Y)
                );
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
            var result = new Matrix();

            // We need to go counter-clockwise.
            result.RotateAt((float)-angle.ToDegrees(), center);

            return result;
        }

        /// <summary>
        /// Rotate a point around the world origin.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="angle">The angle to rotate in pi radians.</param>
        /// <returns>A point rotated about the origin by the specified pi radian angle.</returns>
        public static PointF RotatePoint(this PointF point, double angle)
            => RotatePoint(point, PointF.Empty, angle);

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

            var angleCos = Cos(angle);
            var angleSin = Sin(angle);

            return new PointF(
                (float)(axis.X + (deltaX * angleCos - deltaY * angleSin)),
                (float)(axis.Y + (deltaX * angleSin + deltaY * angleCos))
            );
        }

        /// <summary>
        /// Rotate a series of points around the world origin.
        /// </summary>
        /// <param name="points">The array of points to rotate.</param>
        /// <param name="angle">The angle to rotate the points in pi radians.</param>
        public static void RotatePoints(this PointF[] points, double angle)
        {
            for (var i = 0; i < points.Length; i++)
                points[i] = RotatePoint(points[i], angle);
        }

        /// <summary>
        /// Rotate a series of points around a fulcrum point.
        /// </summary>
        /// <param name="points">The array of points to rotate.</param>
        /// <param name="fulcrum">The point to rotate all other points around.</param>
        /// <param name="angle">The angle to rotate the points in pi radians.</param>
        public static void RotatePoints(this PointF[] points, PointF fulcrum, double angle)
        {
            for (var i = 0; i < points.Length; i++)
                points[i] = RotatePoint(points[i], fulcrum, angle);
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
            => new PointF((x * factor), (y * factor));

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Scale(this PointF point, int factor)
            => Scale(point.X, point.Y, factor);

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Scale(this PointF point, float factor)
            => Scale(point.X, point.Y, factor);

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
        /// Calculates the Slope of two points.
        /// </summary>
        /// <param name="PointA">Starting Point</param>
        /// <param name="PointB">Ending Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>The slope is calculated with Slope = (YB - YA) / (XB - XA) or rise over run</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(this PointF PointA, PointF PointB)
            => Maths.Slope(PointA.X, PointA.Y, PointB.X, PointB.Y);

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static PointF Subtract(this PointF point, int value)
            => new PointF(point.X - value, point.Y - value);

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static PointF Subtract(this PointF point, float value)
            => new PointF(point.X - value, point.Y - value);

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static PointF Subtract(this PointF point, double value)
            => new PointF((float)(point.X - value), (float)(point.Y - value));

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static Vector2D Subtract(this PointF point, Point value)
            => new PointF(point.X - value.X, point.Y - value.Y);

        /// <summary>
        /// Subtracts a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="value">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        public static Vector2D Subtract(this PointF point, PointF value)
            => new PointF(point.X - value.X, point.Y - value.Y);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this PointF point, Size value)
            => new PointF(point.X - value.Width, point.Y - value.Height);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this PointF point, SizeF value)
            => new PointF((point.X - value.Width), (point.Y - value.Height));

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this PointF point, Vector2D Value)
            => new PointF((float)(point.X - Value.I), (float)(point.Y - Value.J));

        /// <summary>
        /// Unit of a Point
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Unit(this PointF value)
            => value.Scale((float)(1 / Sqrt((value.X * value.X) + (value.Y * value.Y))));
    }
}
