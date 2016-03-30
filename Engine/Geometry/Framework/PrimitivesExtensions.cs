// <copyright file="PrimitivesExtensions.cs" company="Shkyrockett">
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
using System.ComponentModel;
using System.Drawing;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public static class PrimitivesExtensions
    {
        /// <summary>
        /// Adds a <see cref="PointF"/> by a value.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> to inflate.</param>
        /// <param name="addendum">The factor to inflate the <see cref="PointF"/>.</param>
        /// <returns>Returns a <see cref="PointF"/> structure inflated by the factor provided.</returns>
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
        public static PointF Add(this VectorF vector, PointF value)
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
        public static VectorF Add(this VectorF vector, double value)
        {
            return new VectorF((float)(vector.X + value), (float)(vector.Y + value));
        }

        /// <summary>
        /// Add Vector2D
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
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
        public static VectorF Add(this VectorF vector, Vector value)
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
        public static VectorF Add(this Vector vector, VectorF value)
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
        public static LineSegment Add(this LineSegment segment, LineSegment value)
        {
            return new LineSegment(
                segment.Points[0].X + value.Points[0].X,
                segment.Points[0].Y + value.Points[0].Y,
                segment.Points[1].X + value.Points[1].X,
                segment.Points[1].Y + value.Points[1].Y);
        }

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
            double test = -GetAngle(pointA, pointB) % Math.PI;

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
            double test = -GetAngle(pointA, pointB) % Math.PI;

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
            double test = -GetAngle(segment.A, segment.B) % Math.PI;

            // This should only loop once using the modulus of pi.
            while (test < 0)
            {
                test += Math.PI;
            }

            return test;
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
        public static double Angle(int X1, int Y1, int X2, int Y2)
        {
            return Angle((X1 - X2), (Y1 - Y2));
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
        public static double Angle(float X1, float Y1, float X2, float Y2)
        {
            return Angle((X1 - X2), (Y1 - Y2));
        }

        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="PointA">Starting Point</param>
        /// <param name="PointB">Ending Point</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        public static double Angle(this Point PointA, Point PointB)
        {
            return Angle((PointA.X - PointB.X), (PointA.Y - PointB.Y));
        }

        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="PointA">Starting Point</param>
        /// <param name="PointB">Ending Point</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        public static double Angle(this PointF PointA, PointF PointB)
        {
            return Angle((PointA.X - PointB.X), (PointA.Y - PointB.Y));
        }

        /// <summary>
        /// Returns the Angle of a line segment.
        /// </summary>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        public static double Angle(this LineSegment segment)
        {
            VectorF Delta = segment.A.Delta(segment.B);
            return Angle(Delta.X, Delta.Y);
        }

        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="DeltaA">Delta Angle 1</param>
        /// <param name="DeltaB">Delta Angle 2</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        public static double Angle(double DeltaA, double DeltaB)
        {
            if (((DeltaA == 0) && (DeltaB == 0))) return 0;
            double Value = Math.Asin(DeltaA / Math.Sqrt(DeltaA * DeltaA + DeltaB * DeltaB));
            if ((DeltaB < 0)) Value = (Math.PI - Value);
            if ((Value < 0)) Value = (Value + (2 * Math.PI));
            return Value;
        }

        /// <summary>
        /// Cross Product of a corner
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks>Note that AB · BC = |AB| * |BC| * Cos(theta).</remarks>
        public static double CrossProduct(this Point point, Vector value)
        {
            return (point.X * value.Y) - (point.Y * value.X);
        }

        /// <summary>
        /// Cross Product of a corner
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks>Note that AB · BC = |AB| * |BC| * Cos(theta).</remarks>
        public static double CrossProduct(this Point point, VectorF value)
        {
            return (point.X * value.Y) - (point.Y * value.X);
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
        public static double CrossProduct(this PointF valueA, Vector valueB)
        {
            return (valueA.X * valueB.Y) - (valueA.Y * valueB.X);
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
        public static double CrossProduct(this PointF valueA, VectorF valueB)
        {
            return (valueA.X * valueB.Y) - (valueA.Y * valueB.X);
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
        public static double CrossProduct(this Point valueA, Point valueB)
        {
            return (valueA.X * valueB.Y) - (valueA.Y * valueB.X);
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
        public static double CrossProduct(this PointF valueA, PointF valueB)
        {
            return (valueA.X * valueB.Y) - (valueA.Y * valueB.X);
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
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>
        /// Return the cross product AB x BC.=((a)->x*(b)->y-(a)->y*(b)->x)
        /// </returns>
        /// <remarks>Graphics Gems IV, page 139.</remarks>
        public static double CrossProduct(this VectorF value1, VectorF value2)
        {
            return value1.CrossProduct(value2);
        }

        /// <summary>
        /// Finds the Delta of two Points
        /// </summary>
        /// <param name="value1">First Point</param>
        /// <param name="value2">Second Point</param>
        /// <returns>Returns the Difference Between PointA and PointB</returns>
        /// <remarks></remarks>
        public static VectorF Delta(this PointF value1, PointF value2)
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
        public static Vector Delta(this Point value1, Point value2)
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
        public static VectorF Delta(this VectorF vector, VectorF value)
        {
            return value - vector;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>Source: http://www.vcskicks.com/code-snippet/distance-formula.php. </remarks>
        public static double Distance(this Point point, Point value)
        {
            // Pythagorean theorem c^2 = a^2 + b^2
            // thus c = square root(a^2 + b^2)
            double a = (value.X - point.X);
            double b = (value.Y - point.Y);

            return Math.Sqrt(a * a + b * b);
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
            return (point.Subtract(value)).Modulus();
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
            return (value1 - value2).Modulus();
        }

        /// <summary>
        /// Length of a Segment
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Distance(this LineSegment segment)
        {
            return segment.A.Subtract(segment.B).Modulus();
        }

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="value">Starting Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product "�" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        public static double DotProduct(this Vector value)
        {
            return ((value.X * value.X) + (value.Y * value.Y));
        }

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="value">Starting Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product "�" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        public static double DotProduct(this VectorF value)
        {
            return ((value.X * value.X) + (value.Y * value.Y));
        }

        /// <summary>
        /// Determines the dot product of two 2D vectors
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value">Second Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        public static double DotProduct(this Point point, Vector value)
        {
            return ((point.X * value.X) + (point.X * value.Y));
        }

        /// <summary>
        /// Determines the dot product of two 2D vectors
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value">Second Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        public static double DotProduct(this Point point, VectorF value)
        {
            return ((point.X * value.X) + (point.X * value.Y));
        }

        /// <summary>
        /// Finds the Dot Product of two Points 
        /// </summary>
        /// <param name="point">First Point</param>
        /// <param name="value">Second Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>
        /// The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2
        /// </remarks>
        public static double DotProduct(this PointF point, Vector value)
        {
            return ((point.Y * value.X) + (point.Y * value.Y));
        }

        /// <summary>
        /// Finds the Dot Product of two Points 
        /// </summary>
        /// <param name="point">First Point</param>
        /// <param name="value">Second Point</param>
        /// <returns>Dot Product</returns>
        /// <remarks>
        /// The dot product is calculated with DotProduct = X ^ 2 + Y ^ 2
        /// </remarks>
        public static double DotProduct(this PointF point, VectorF value)
        {
            return ((point.Y * value.X) + (point.Y * value.Y));
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
        public static double DotProduct(this PointF point, PointF value)
        {
            return ((point.Y * value.X) + (point.Y * value.Y));
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
        public static double DotProduct(this Point point, Point value)
        {
            return ((point.Y * value.X) + (point.Y * value.Y));
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
        public static double DotProduct(this VectorF vector, VectorF value)
        {
            return vector.DotProduct(value);
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
        public static double DotProduct(this VectorF vector, PointF value)
        {
            return vector.DotProduct(value);
        }

        /// <summary>
        /// Find the angle of two points.
        /// </summary>
        /// <param name="point">First Point.</param>
        /// <param name="value">Second Point.</param>
        /// <returns>Returns a value between PI and -PI representing the angle, in radians, of two points relative to world coordinates.</returns>
        /// <remarks></remarks>
        public static double GetAngle(this Point point, Point value)
        {
            return Math.Atan2((value.Y - point.Y), (value.X - point.X));
        }

        /// <summary>
        /// Find the angle of two points.
        /// </summary>
        /// <param name="point">First Point.</param>
        /// <param name="value">Second Point.</param>
        /// <returns>Returns a value between PI and -PI representing the angle, in radians, of two points relative to world coordinates.</returns>
        /// <remarks></remarks>
        public static double GetAngle(this PointF point, PointF value)
        {
            return Math.Atan2((value.Y - point.Y), (value.X - point.X));
        }

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        public static Size Inflate(this Size size, float factor)
        {
            return new Size((int)(size.Width * factor), (int)(size.Height * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        public static Size Inflate(this Size size, Size factor)
        {
            return new Size((int)(size.Width * factor.Width), (int)(size.Height * factor.Height));
        }

        /// <summary>
        /// Inflates a <see cref="SizeF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        public static SizeF Inflate(this SizeF size, float factor)
        {
            return new SizeF(size.Width * factor, size.Height * factor);
        }

        /// <summary>
        /// Inflates a <see cref="SizeF"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
        public static SizeF Inflate(this SizeF size, SizeF factor)
        {
            return new SizeF(size.Width * factor.Width, size.Height * factor.Height);
        }

        /// <summary>
        /// Inverts a Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector Invert(this Vector value)
        {
            return new Vector((1 / value.X), (1 / value.Y));
        }

        /// <summary>
        /// Inverts a Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Invert(this VectorF value)
        {
            return new VectorF((1 / value.X), (1 / value.Y));
        }

        /// <summary>
        /// Returns the Length of a lineSeg.
        /// </summary>
        /// <param name="X1">Horizontal Component of Point Starting Point</param>
        /// <param name="Y1">Vertical Component of Point Starting Point</param>
        /// <param name="X2">Horizontal Component of Ending Point</param>
        /// <param name="Y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Length of a lineSeg.</returns>
        /// <remarks></remarks>
        public static double Length(int X1, int Y1, int X2, int Y2)
        {
            return Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
        }

        /// <summary>
        /// Returns the Length of a lineSeg.
        /// </summary>
        /// <param name="X1">Horizontal Component of Point Starting Point</param>
        /// <param name="Y1">Vertical Component of Point Starting Point</param>
        /// <param name="X2">Horizontal Component of Ending Point</param>
        /// <param name="Y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Length of a lineSeg.</returns>
        /// <remarks></remarks>
        public static double Length(float X1, float Y1, float X2, float Y2)
        {
            return Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
        }

        /// <summary>
        /// Calculates the Length between two points.
        /// </summary>
        /// <param name="point">Starting Point.</param>
        /// <param name="value">Ending Point.</param>
        /// <returns>Returns the length of a line segment between two points.</returns>
        /// <remarks>The Length is calculated as AC = SquarRoot(AB^2 + BC^2) </remarks>
        public static double Length(this Point point, Point value)
        {
            return Math.Sqrt(Math.Pow((value.X - point.X), 2) + Math.Pow((value.Y - point.Y), 2));
        }

        /// <summary>
        /// Calculates the Length between two points.
        /// </summary>
        /// <param name="point">Starting Point.</param>
        /// <param name="value">Ending Point.</param>
        /// <returns>Returns the length of a line segment between two points.</returns>
        /// <remarks>The Length is calculated as AC = SquarRoot(AB^2 + BC^2) </remarks>
        public static double Length(this PointF point, PointF value)
        {
            return Math.Sqrt(Math.Pow(value.X - point.X, 2) + Math.Pow(value.Y - point.Y, 2));
        }

        /// <summary>
        /// Finds the length of a 2D vector
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value"> Point</param>
        /// <returns>The Length between two Points</returns>
        /// <remarks>The Length is calculated as AC = SquarRoot(AB^2 + BC^2) </remarks>
        public static double Length(this VectorF vector, VectorF value)
        {
            return (float)(Math.Sqrt(Math.Pow(value.X - vector.X, 2) + Math.Pow(value.Y - vector.Y, 2)));
        }

        /// <summary>
        /// Finds the Length between two points
        /// </summary>
        /// <param name="segment">line segment</param>
        /// <returns>The Length between two Points</returns>
        /// <remarks>The Length is calculated as AC = SquarRoot(AB^2 + BC^2) </remarks>
        public static double Length(this LineSegment segment)
        {
            return Math.Sqrt(Math.Pow(segment.B.X - segment.A.X, 2) + Math.Pow(segment.B.Y - segment.A.Y, 2));
        }

        /// <summary>
        /// Modulus of a Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Modulus(this Vector point)
        {
            return Math.Pow((point.X * point.X) + (point.Y * point.Y), 0.5F);
        }

        /// <summary>
        /// Modulus of a Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Modulus(this VectorF value)
        {
            return Math.Pow((value.X * value.X) + (value.Y * value.Y), 0.5F);
        }

        /// <summary>
        /// Multiply: Point * Matrix
        /// </summary>
        public static PointF Multiply(this PointF point, MatrixF matrix)
        {
            return matrix.Transform(point);
        }

        /// <summary>
        /// Multiply Vector
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="value">Second Vector</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Multiply(this VectorF vector, PointF value)
        {
            return new VectorF(vector.X * value.X, vector.Y * value.Y);
        }

        /// <summary>
        /// Multiply Point
        /// </summary>
        /// <param name="value1">First Point</param>
        /// <param name="value2">Second Point</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Multiply(this PointF value1, PointF value2)
        {
            return new VectorF((value1.X * value2.X), (value1.Y * value2.Y));
        }

        /// <summary>
        /// Multiply Vector
        /// </summary>
        /// <param name="value1">First Point</param>
        /// <param name="value2">Second Point</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Multiply(this VectorF value1, VectorF value2)
        {
            return value1.Multiply(value2);
        }

        /// <summary>
        /// Normalize Two Points
        /// </summary>
        /// <param name="point">First Point</param>
        /// <param name="value">Second Point</param>
        /// <returns>The Normal of two Points</returns>
        /// <remarks></remarks>
        public static PointF Normalise(this PointF point, SizeF value)
        {
            return point.Scale((float)((1 / Math.Sqrt(((point.X * value.Width) + (point.Y * value.Height))))));
        }

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
            PointF[] Out = new PointF[] { (PointF)(point + PerpendicularAB), (PointF)(value + PerpendicularAB) };
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
            LineSegment Out = new LineSegment((PointF)(segment.A + PerpendicularAB), (PointF)(segment.B + PerpendicularAB));
            if ((distance <= 0)) Out.Reverse();
            return Out;
        }

        /// <summary>
        /// Perpendicular of a Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        public static Vector Perpendicular(this Vector vector)
        {
            return new Vector(vector.Y * -1, vector.X);
        }

        /// <summary>
        /// Perpendicular of a Vector
        /// </summary>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use X = -Y, Y = X</remarks>
        public static VectorF Perpendicular(this VectorF vector)
        {
            return new VectorF(vector.Y * -1, vector.X);
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        public static void Reverse(this LineSegment segment)
        {
            Array.Reverse(segment.Points);
        }

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

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        public static Point Scale(this Point point, float factor)
        {
            return new Point((int)(point.X * factor), (int)(point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        public static PointF Scale(this PointF point, float factor)
        {
            return new PointF((float)(point.X * factor), (float)(point.Y * factor));
        }

        /// <summary>
        /// Inflates a <see cref="Point"/> by a given factor.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Point"/>.</param>
        /// <returns>Returns a <see cref="Point"/> structure inflated by the factor provided.</returns>
        public static PointF Scale(this PointF point, double factor)
        {
            return new PointF((float)(point.X * factor), (float)(point.Y * factor));
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static VectorF Scale(this VectorF value, double factor)
        {
            return new VectorF((float)(value.X * factor), (float)(value.Y * factor));
        }

        /// <summary>
        /// Scale a Point
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="vector">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static PointF Scale(this PointF point, Vector vector)
        {
            return new PointF((float)(point.X * vector.X), (float)(point.Y * vector.Y));
        }

        /// <summary>
        /// Scale a Point
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to inflate.</param>
        /// <param name="vector">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static PointF Scale(this PointF point, VectorF vector)
        {
            return new PointF((float)(point.X * vector.X), (float)(point.Y * vector.Y));
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
        public static PointF Subtract(this PointF point, float value)
        {
            return new PointF(point.X - value, point.Y - value);
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
        public static PointF Subtract(this Point point, VectorF value)
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
        /// <param name="vector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static PointF Subtract(this Vector vector, Point value)
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
        public static PointF Subtract(this Vector vector, PointF Value)
        {
            return new PointF(vector.X - Value.X, vector.Y - Value.Y);
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
            return new VectorF(vector.X - Value.X, vector.Y - Value.Y);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static VectorF Subtract(this Vector vector, VectorF Value)
        {
            return new VectorF(vector.X - Value.X, vector.Y - Value.Y);
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
        public static VectorF Unit(this VectorF value)
        {
            return value.Scale((float)(1 / Math.Sqrt(((value.X * value.X) + (value.Y * value.Y)))));
        }
    }
}
