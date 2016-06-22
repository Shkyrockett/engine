// <copyright file="Primitives.cs" >
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Maths;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public static class Primitives
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
        public static double AbsoluteAngle(this Point2D pointA, Point2D pointB)
            => Maths.AbsoluteAngle(pointA.X, pointA.Y, pointB.X, pointB.Y);

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="segment">Line segment.</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteAngle(this LineSegment segment)
            => Maths.AbsoluteAngle(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);

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
            => new Point2D(point.X + addend, point.Y + addend);

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
            => new Vector2D(point.X + addend.X, point.Y + addend.Y);

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
            => new Point2D(point.X + addend.Width, point.Y + addend.Height);

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
            => new Point2D(point.X + addend.I, point.Y + addend.J);

        /// <summary>
        /// Adds a <see cref="Size2D"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="Size2D"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Add(this Size2D point, double addend)
            => new Size2D(point.Width + addend, point.Height + addend);

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
            => new Size2D(point.Width + addend.X, point.Height + addend.Y);

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
            => new Size2D(point.Width + addend.Width, point.Height + addend.Height);

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
            => new Size2D(point.Width + addend.I, point.Height + addend.J);

        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addend">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(this PointF point, PointF addend)
            => new Vector2D(point.X + addend.X, point.Y + addend.Y);

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
            => new Vector2D(vector.I + addend, vector.J + addend);

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
            => new Vector2D(vector.I + addend, vector.J + addend);

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
            => new Vector2D((float)(vector.I + addend), (float)(vector.J + addend));

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
            => new Point2D(vector.I + addend.X, vector.J + addend.Y);

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
            => new Point2D(vector.I + addend.X, vector.J + addend.Y);

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
            => new Vector2D(vector.I + addend.I, vector.J + addend.J);

        /// <summary>
        /// Add VectorF
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Add(this Vector3D vector, double addend)
            => new Vector3D((vector.I + addend), (vector.J + addend), (vector.K + addend));

        /// <summary>
        /// Add Vector3D
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Add(this Vector3D vector, Point3D addend)
            => new Point3D(vector.I + addend.X, vector.J + addend.Y, vector.K + addend.Z);

        /// <summary>
        /// Add Vector2D
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Add(this Vector3D vector, Vector3D addend)
            => new Vector3D(vector.I + addend.I, vector.J + addend.J, vector.K + addend.K);

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
            => new LineSegment(
            segment.Points[0].X + addend.Points[0].X,
            segment.Points[0].Y + addend.Points[0].Y,
            segment.Points[1].X + addend.Points[1].X,
            segment.Points[1].Y + addend.Points[1].Y);

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
            => Maths.Angle(PointA.X, PointA.Y, PointB.X, PointB.Y);

        /// <summary>
        /// Returns the Angle of a line segment.
        /// </summary>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(this LineSegment segment)
            => Maths.Angle(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);

        #endregion

        #region Center

        /// <summary>
        /// Extension method to find the center point of a rectangle.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle2D"/> of which you want the center.</param>
        /// <returns>A <see cref="Point2D"/> representing the center point of the <see cref="RectangleF"/>.</returns>
        /// <remarks>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.</remarks>
        public static Point2D Center(this Rectangle2D rectangle)
            => new Point2D((0.5f * rectangle.Width) + rectangle.X, (0.5f * rectangle.Height) + rectangle.Y);

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
        public static double CrossProduct(this Vector2D valueA, Point valueB)
            => Maths.CrossProduct(valueA.I, valueA.J, valueB.X, valueB.Y);

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
            => Maths.CrossProduct(valueA.I, valueA.J, valueB.X, valueB.Y);

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
        public static Vector2D Delta(this Point2D value1, Point2D value2)
            => value2.Subtract(value1);

        /// <summary>
        /// Finds the Delta of two Sizes
        /// </summary>
        /// <param name="size">First Point</param>
        /// <param name="value">Second Point</param>
        /// <returns>Returns the Difference Between PointA and PointB</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Delta(this Size2D size, Size2D value)
            => value - size;

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
            => value - vector;

        #endregion

        #region Distance/Length

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="p1">First point.</param>
        /// <param name="p2">Second point.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(Tuple<double, double> p1, Tuple<double, double> p2)
            => Maths.Distance(p1.Item1, p1.Item2, p2.Item1, p2.Item2);

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="p1">First point.</param>
        /// <param name="p2">Second point.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point2D p1, Point2D p2)
            => Maths.Distance(p1.X, p1.Y, p2.X, p2.Y);

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
            => Maths.Distance(a.I, a.J, b.I, b.J);

        /// <summary>
        /// Length of a Segment
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this LineSegment segment)
            => Maths.Distance(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);

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
            => Maths.Distance(x1, y1, x2, y2);

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
            => Maths.Distance(x1, y1, x2, y2);

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
            => Maths.Distance(x1, y1, x2, y2);

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
            => Maths.Distance(point.X, point.Y, value.X, value.Y);

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
            => Maths.Distance(vector.I, vector.J, value.I, value.J);

        /// <summary>
        /// Finds the Length between two points
        /// </summary>
        /// <param name="segment">line segment</param>
        /// <returns>The Length between two Points</returns>
        /// <remarks>The Length is calculated as AC = SquarRoot(AB^2 + BC^2) </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this LineSegment segment)
            => Maths.Distance(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);

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
            => Value1.DotProduct(Value2.Invert());

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
            => Maths.DotProduct(vector.I, vector.J, value.X, value.Y);

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
            => Maths.DotProduct(vector.I, vector.J, value.X, value.Y);

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

        #endregion

        #region Inflate

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
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
        /// Inflates a <see cref="SizeF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Inflate(this Size2D size, Size2D factor)
            => new Size2D(size.Width * factor.Width, size.Height * factor.Height);

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
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
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, int factor)
            => new Vector2D((point.I * factor), (point.J * factor));

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, float factor)
            => new Vector2D((point.I * factor), (point.J * factor));

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, double factor)
            => new Vector2D((float)(point.I * factor), (float)(point.J * factor));

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, Point factors)
            => new Vector2D((point.I * factors.X), (point.J * factors.Y));

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, PointF factors)
            => new Vector2D((point.I * factors.X), (point.J * factors.Y));

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D size, Size factor)
            => new Vector2D(size.I * factor.Width, size.J * factor.Height);

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, SizeF factor)
            => new Vector2D(point.I * factor.Width, point.J * factor.Height);

        /// <summary>
        /// Inflates a <see cref="Vector2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Vector2D"/> to inflate.</param>
        /// <param name="factors">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Vector2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Inflate(this Vector2D point, Vector2D factors)
            => new Vector2D((point.I * factors.I), (point.J * factors.J));

        #endregion

        #region Invert

        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Invert(float x, float y)
            => new Vector2D((1 / x), (1 / y));

        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Invert(double x, double y)
            => new Vector2D((1 / x), (1 / y));

        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Invert(this PointF value)
            => Invert(value.X, value.Y);

        /// <summary>
        /// Inverts a Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Invert(this Vector2D value)
            => Invert(value.I, value.J);

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
            => Maths.Modulus(value.I, value.J);

        #endregion

        #region Multiply

        /// <summary>
        /// Multiply: Point * Matrix
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerStepThrough]
        public static Point2D Multiply(this Point2D point, Matrix2D matrix)
            => matrix.Transform(point);

        #endregion

        #region Normalize

        /// <summary>
        /// Normalize Two Points
        /// </summary>
        /// <param name="point">First Point</param>
        /// <param name="value">Second Point</param>
        /// <returns>The Normal of two Points</returns>
        /// <remarks></remarks>
        public static Point2D Normalize(this Point2D point, Size2D value)
            => new Point2D(Maths.Normalize(point.X, point.Y, value.Width, value.Height));

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
            return new List<Point2D> { new Point2D(offset.Item1, offset.Item2), new Point2D(offset.Item3, offset.Item4) };
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
            => new LineSegment(Maths.OffsetSegment(point.X, point.Y, value.X, value.Y, distance));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment Offset(this LineSegment segment, double distance)
            => new LineSegment(Maths.OffsetSegment(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y, distance));

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
            => new Vector2D(PerpendicularClockwise(i, j));

        /// <summary>
        /// Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Perpendicular(double i, double j)
            => new Vector2D(PerpendicularClockwise(i, j));

        /// <summary>
        /// Perpendicular of a Vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Perpendicular(this Vector2D vector)
            => new Vector2D(PerpendicularClockwise(vector.I, vector.J));

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
        public static Point2D Reflect(this Point2D point, Point2D value, Point2D axis)
        {
            Vector2D SegmentVectorDelta = point.Delta(value);
            var QC12 = new Vector2D(
                value.CrossProduct(point),
                axis.DotProduct((Point2D)SegmentVectorDelta)
                );
            double QC3 = 0.5F * SegmentVectorDelta.DotProduct(SegmentVectorDelta);
            return new Point2D(
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
        public static Point2D Reflect(this LineSegment segment, Point2D axis)
        {
            Vector2D SegmentVectorDelta = segment.A.Delta(segment.B);
            var QC12 = new Vector2D(
                segment.B.CrossProduct(segment.A),
                axis.DotProduct((Point2D)SegmentVectorDelta)
                );
            double QC3 = 0.5F * SegmentVectorDelta.DotProduct(SegmentVectorDelta);
            return new Point2D(
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
        public static Matrix2D RotateAroundPoint(this Point2D center, double angle)
        {
            // Translate the point to the origin.
            var result = new Matrix2D();

            // We need to go counter-clockwise.
            result.RotateAt((float)-angle.ToDegrees(), center.X, center.Y);

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
        public static Point2D RotatePoint(this Point2D point, double angle)
            => RotatePoint(point, Point2D.Empty, angle);

        /// <summary>
        /// Rotate a point around a fulcrum point.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="axis">The fulcrum point to rotate the point around.</param>
        /// <param name="angle">The angle to rotate the point in pi radians.</param>
        /// <returns>A point rotated about the fulcrum point by the specified pi radian angle.</returns>
        public static Point2D RotatePoint(this Point2D point, Point2D axis, double angle)
        {
            double deltaX = point.X - axis.X;
            double deltaY = point.Y - axis.Y;

            double angleCos = Cos(angle);
            double angleSin = Sin(angle);

            return new Point2D(
                (axis.X + (deltaX * angleCos - deltaY * angleSin)),
                (axis.Y + (deltaX * angleSin + deltaY * angleCos))
            );
        }

        #endregion

        #region Rotate Points

        /// <summary>
        /// Rotate a series of points around the world origin.
        /// </summary>
        /// <param name="points">The array of points to rotate.</param>
        /// <param name="angle">The angle to rotate the points in pi radians.</param>
        public static void RotatePoints(this Point2D[] points, double angle)
        {
            for (int i = 0; i < points.Length; i++)
                points[i] = RotatePoint(points[i], angle);
        }

        /// <summary>
        /// Rotate a series of points around a fulcrum point.
        /// </summary>
        /// <param name="points">The array of points to rotate.</param>
        /// <param name="fulcrum">The point to rotate all other points around.</param>
        /// <param name="angle">The angle to rotate the points in pi radians.</param>
        public static void RotatePoints(this Point2D[] points, Point2D fulcrum, double angle)
        {
            for (int i = 0; i < points.Length; i++)
                points[i] = RotatePoint(points[i], fulcrum, angle);
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
        public static Point2D Scale(double x, double y, double factor)
            => new Point2D((x * factor), (y * factor));

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="x">The x value to inflate.</param>
        /// <param name="y">The y value to inflate.</param>
        /// <param name="z">The z value to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Scale(double x, double y, double z, double factor)
            => new Point3D((x * factor), (y * factor), (z * factor));

        /// <summary>
        /// Inflates a <see cref="Point2D"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point2D"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="double"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Scale(this Point2D point, double factor)
            => Scale(point.X, point.Y, factor);

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
            => Scale(value.I, value.J, factor);

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
            => Scale(value.I, value.J, factor);

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
            => Scale(value.I, value.J, factor);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Scale(this Vector3D value, double factor)
            => Scale(value.I, value.J, value.K, factor);
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
        public static double Slope(this Point2D PointA, Point2D PointB)
            => Maths.Slope(PointA.X, PointA.Y, PointB.X, PointB.Y);

        /// <summary>
        /// Calculates the Slope of a vector.
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>The slope is calculated with Slope = Y / X or rise over run</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(this Vector2D Point)
            => Maths.Slope(Point.I, Point.J);

        /// <summary>
        /// Returns the slope angle of a line.
        /// </summary>
        /// <param name="Line">Line to get length of</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(this LineSegment Line)
            => Maths.Slope(Line.A.X, Line.A.Y, Line.B.X, Line.B.Y);

        #endregion

        #region Subtract

        /// <summary>
        /// Subtracts a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="vaule">The <see cref="Point2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure reduced by the amount provided.</returns>
        public static Point2D Subtract(this Point2D vaule, double subend)
            => new Point2D(vaule.X - subend, vaule.Y - subend);

        /// <summary>
        /// Subtracts a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="value">The <see cref="Point2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure reduced by the amount provided.</returns>
        public static Vector2D Subtract(this Point2D value, Point2D subend)
            => new Vector2D(value.X - subend.X, value.Y - subend.Y);

        /// <summary>
        /// Subtracts a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="value">The <see cref="Point2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure reduced by the amount provided.</returns>
        public static Point2D Subtract(this Point2D value, Size2D subend)
            => new Point2D(value.X - subend.Width, value.Y - subend.Height);

        /// <summary>
        /// Subtracts a <see cref="Point2D"/> by a value.
        /// </summary>
        /// <param name="value">The <see cref="Point2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Vector2D"/>.</param>
        /// <returns>Returns a <see cref="Point2D"/> structure reduced by the amount provided.</returns>
        public static Point2D Subtract(this Point2D value, Vector2D subend)
            => new Point2D(value.X - subend.I, value.Y - subend.J);

        /// <summary>
        /// Subtracts a <see cref="Size2D"/> by a value.
        /// </summary>
        /// <param name="vaule">The <see cref="Size2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure reduced by the amount provided.</returns>
        public static Size2D Subtract(this Size2D vaule, double subend)
            => new Size2D(vaule.Width - subend, vaule.Height - subend);

        /// <summary>
        /// Subtracts a <see cref="Size2D"/> by a value.
        /// </summary>
        /// <param name="value">The <see cref="Size2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure reduced by the amount provided.</returns>
        public static Size2D Subtract(this Size2D value, Point2D subend)
            => new Size2D(value.Width - subend.X, value.Height - subend.Y);

        /// <summary>
        /// Subtracts a <see cref="Size2D"/> by a value.
        /// </summary>
        /// <param name="value">The <see cref="Size2D"/> to reduce.</param>
        /// <param name="subend">The amount to reduce the <see cref="Size2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure reduced by the amount provided.</returns>
        public static Size2D Subtract(this Size2D value, Size2D subend)
            => new Size2D(value.Width - subend.Width, value.Height - subend.Height);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D Subtract(this Vector2D vector, double value)
            => new Vector2D(vector.I - value, vector.J - value);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this Vector2D vector, Point value)
            => new PointF((float)(vector.I - value.X), (float)(vector.J - value.Y));

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D Subtract(this Vector2D vector, Point2D value)
            => new Point2D((vector.I - value.X), (vector.J - value.Y));

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this Vector2D vector, PointF Value)
            => new PointF((float)(vector.I - Value.X), (float)(vector.J - Value.Y));

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D Subtract(this Vector2D point, Size value)
            => new Vector2D(point.I - value.Width, point.J - value.Height);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D Subtract(this Vector2D point, SizeF value)
            => new Vector2D((point.I - value.Width), (point.J - value.Height));

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D Subtract(this Vector2D vector, Vector2D Value)
            => new Vector2D(vector.I - Value.I, vector.J - Value.J);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector3D Subtract(this Vector3D vector, double value)
            => new Vector3D(vector.I - value, vector.J - value, vector.K - value);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point3D Subtract(this Vector3D vector, Point3D value)
            => new Point3D((vector.I - value.X), (vector.J - value.Y), (vector.K - value.Z));

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector3D Subtract(this Vector3D vector, Vector3D Value)
            => new Vector3D(vector.I - Value.I, vector.J - Value.J, vector.K - Value.K);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static LineSegment Subtract(this LineSegment segment, LineSegment value)
            => new LineSegment(
            segment.Points[0].X - value.Points[0].X,
            segment.Points[0].Y - value.Points[0].Y,
            segment.Points[1].X - value.Points[1].X,
            segment.Points[1].Y - value.Points[1].Y);

        #endregion

        #region Unit

        /// <summary>
        /// Unit of a Vector
        /// </summary>
        /// <param name="value">The Vector to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D Unit(this Vector2D value)
            => value.Scale(1 / Sqrt(((value.I * value.I) + (value.J * value.J))));

        /// <summary>
        /// Unit of a Vector
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector3D Unit(this Vector3D value)
            => value.Scale(1 / Sqrt(((value.I * value.I) + (value.J * value.J))));

        #endregion
    }
}
