// <copyright file="Measurements.cs" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary>
//   Methods for calculating measurements of geometric entities.
// </summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The measurements class.
    /// </summary>
    public static class Measurements
    {
        #region Distance Extension Method Overloads
        /// <summary>
        /// Distance between two tuple points.
        /// </summary>
        /// <param name="p1">First point.</param>
        /// <param name="p2">Second point.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this (double X, double Y) p1, (double X, double Y) p2)
            => Distance(p1.X, p1.Y, p2.X, p2.Y);

        /// <summary>
        /// Finds the distance between two points in 2-dimensional euclidean space.
        /// </summary>
        /// <param name="pointA">The first point</param>
        /// <param name="pointB">The second Point</param>
        /// <returns>Returns the length of the distance between the two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point2D pointA, Point2D pointB)
            => Distance(pointA.X, pointA.Y, pointB.X, pointB.Y);

        /// <summary>
        /// Finds the distance between two 2-dimensional vectors.
        /// </summary>
        /// <param name="vectorA">The first vector.</param>
        /// <param name="vectorB">The second vector.</param>
        /// <returns>Returns the length of the distance between the two vectors.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Vector2D vectorA, Vector2D vectorB)
            => Distance(vectorA.I, vectorA.J, vectorB.I, vectorB.J);

        /// <summary>
        /// Finds the distance between two points in 3-dimensional euclidean space.
        /// </summary>
        /// <param name="pointA">The first point.</param>
        /// <param name="pointB">The second point.</param>
        /// <returns>Returns the length of the distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point3D pointA, Point3D pointB)
            => Distance(pointA.X, pointA.Y, pointA.Z, pointB.X, pointB.Y, pointB.Z);

        /// <summary>
        /// Finds the distance between two 3-dimensional vectors.
        /// </summary>
        /// <param name="vectorA">The first vector.</param>
        /// <param name="vectorB">The second vector.</param>
        /// <returns>Returns the length of the distance between two vectors.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Vector3D vectorA, Vector3D vectorB)
            => Distance(vectorA.I, vectorA.J, vectorA.K, vectorB.I, vectorB.J, vectorB.K);

        /// <summary>
        /// Finds the distance between a line segment and a point, but only if the point is in the space perpendicular to the line segment.
        /// </summary>
        /// <param name="segment">The line segment.</param>
        /// <param name="point">The point.</param>
        /// <returns>Returns the length of the shortest distance from the point to the line segment, or null if the point is not within the area of perpendicularity to the line segment.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double? ConstrainedDistance(this LineSegment segment, Point2D point)
            => ConstrainedDistanceLineSegmentPoint(segment.AX, segment.AY, segment.BX, segment.BY, point.X, point.Y);

        /// <summary>
        /// Finds the distance between a line segment and a point, but only if the point is in the space perpendicular to the line segment.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="segment">The line segment.</param>
        /// <returns>Returns the length of the shortest distance from the point to the line segment, or null if the point is not within the area of perpendicularity to the line segment.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double? ConstrainedDistance(this Point2D point, LineSegment segment)
            => ConstrainedDistanceLineSegmentPoint(segment.AX, segment.AY, segment.BX, segment.BY, point.X, point.Y);

        /// <summary>
        /// Finds the shortest distance between a line segment and a point.
        /// </summary>
        /// <param name="segment">The line segment.</param>
        /// <param name="point">The point.</param>
        /// <returns>Returns the length of the shortest between a line segment an a point.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this LineSegment segment, Point2D point)
            => DistanceLineSegmentPoint(segment.AX, segment.AY, segment.BX, segment.BY, point.X, point.Y);

        /// <summary>
        /// Finds the shortest distance between a line segment and a point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="segment">The line segment.</param>
        /// <returns>Returns the length of the shortest between a line segment an a point.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point2D point, LineSegment segment)
            => DistanceLineSegmentPoint(segment.AX, segment.AY, segment.BX, segment.BY, point.X, point.Y);

        /// <summary>
        /// Finds the shortest distance between an infinite line and a point.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="point">The point.</param>
        /// <returns>Returns the length of the distance between a point and a line.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Line line, Point2D point)
            => DistanceLinePoint(line.Location.X, line.Location.Y, line.Direction.I, line.Direction.J, point.X, point.Y);

        /// <summary>
        /// Finds the shortest distance between an infinite line and a point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="line">The line.</param>
        /// <returns>Returns the length of the distance between a point and a line.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point2D point, Line line)
            => DistanceLinePoint(line.Location.X, line.Location.Y, line.Direction.I, line.Direction.J, point.X, point.Y);

        /// <summary>
        /// Finds the shortest distance between a point and a Bézier segment.
        /// </summary>
        /// <param name="bezier">The Bézier segment.</param>
        /// <param name="point">The point.</param>
        /// <returns>Returns the length of the distance between a point and a Bézier segment.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this BezierSegmentX bezier, Point2D point)
            => DistanceTo(bezier.CurveX, bezier.CurveY, point);

        /// <summary>
        /// Finds the shortest distance between a point and a Bézier segment.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="bezier">The Bézier segment.</param>
        /// <returns>Returns the length of the distance between a point and a Bézier segment.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point2D point, BezierSegmentX bezier)
            => DistanceTo(bezier.CurveX, bezier.CurveY, point);

        /// <summary>
        /// Finds the shortest distance between a point and a Quadratic Bézier curve.
        /// </summary>
        /// <param name="bezier">The Quadratic Bézier curve.</param>
        /// <param name="point">The point.</param>
        /// <returns>Returns the length of the shortest distance between a point and a Quadratic Bézier curve.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this QuadraticBezier bezier, Point2D point)
            => DistanceTo(bezier.CurveX, bezier.CurveY, point);

        /// <summary>
        /// Finds the shortest distance between a point and a Quadratic Bézier curve.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="bezier">The Quadratic Bézier curve.</param>
        /// <returns>Returns the length of the shortest distance between a point and a Quadratic Bézier curve.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point2D point, QuadraticBezier bezier)
            => DistanceTo(bezier.CurveX, bezier.CurveY, point);

        /// <summary>
        /// Finds the shortest distance between a point and a Cubic Bézier curve.
        /// </summary>
        /// <param name="bezier">The Cubic Bézier curve.</param>
        /// <param name="point">The point.</param>
        /// <returns>Returns the length of the shortest distance between a point and a Cubic Bézier curve.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this CubicBezier bezier, Point2D point)
            => DistanceTo(bezier.CurveX, bezier.CurveY, point);

        /// <summary>
        /// Finds the shortest distance between a point and a Cubic Bézier curve.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="bezier">The Cubic Bézier curve.</param>
        /// <returns>Returns the length of the shortest distance between a point and a Cubic Bézier curve.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point2D point, CubicBezier bezier)
            => DistanceTo(bezier.CurveX, bezier.CurveY, point);
        #endregion Distance Extension Method Overloads

        #region SquareDistance Extension Method Overloads
        /// <summary>
        /// Finds the square distance between two points.
        /// </summary>
        /// <param name="pointA">The first point.</param>
        /// <param name="pointB">The second point.</param>
        /// <returns>Returns the squared distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistance(this Point2D pointA, Point2D pointB)
            => SquareDistance(pointA.X, pointA.Y, pointB.X, pointB.Y);

        /// <summary>
        /// Finds the square distance between two vectors.
        /// </summary>
        /// <param name="vectorA">The first vector.</param>
        /// <param name="vectorB">The second vector.</param>
        /// <returns>Returns the squared distance between two vectors.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistance(this Vector2D vectorA, Vector2D vectorB)
            => SquareDistance(vectorA.I, vectorA.J, vectorB.I, vectorB.J);
        #endregion SquareDistance Extension Method Overloads

        #region Nearest Extension Method Overloads
        /// <summary>
        /// Finds the nearest point on a point. A redundant method that always returns the first point.
        /// </summary>
        /// <param name="pointA">The first point.</param>
        /// <param name="pointB">The second point.</param>
        /// <returns>Returns the values of the first point.</returns>
        public static (double X, double Y) NearestPoint(this Point2D pointA, Point2D pointB)
            => (pointA.X, pointA.Y);

        /// <summary>
        /// Finds the perpendicular nearest point on a line segment.
        /// </summary>
        /// <param name="segment">The line segment.</param>
        /// <param name="point">The point.</param>
        /// <returns>Returns the nearest point on the line segment perpendicular to a point.</returns>
        public static (double X, double Y) NearestPoint(this LineSegment segment, Point2D point)
            => NearestPointOnLineSegment(segment.AX, segment.AY, segment.BX, segment.BY, point.X, point.Y);

        /// <summary>
        /// Finds the perpendicular nearest point on a line segment.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="segment">The line segment.</param>
        /// <returns>Returns the nearest point on the line segment perpendicular to a point.</returns>
        public static (double X, double Y) NearestPoint(this Point2D point, LineSegment segment)
            => NearestPointOnLineSegment(segment.AX, segment.AY, segment.BX, segment.BY, point.X, point.Y);

        /// <summary>
        /// Finds the nearest point on an elliptical arc.
        /// </summary>
        /// <param name="ellipticalArc">The Elliptical Arc.</param>
        /// <param name="point">The Point.</param>
        /// <returns>Returns the nearest point on an elliptical arc to a point.</returns>
        public static (double X, double Y) NearestPoint(this EllipticalArc ellipticalArc, Point2D point)
            => NearestPointOnEllipticalArc(ellipticalArc.X, ellipticalArc.Y, ellipticalArc.RX, ellipticalArc.RY, ellipticalArc.StartAngle, ellipticalArc.SweepAngle, point.X, point.Y);

        /// <summary>
        /// Finds the nearest t parameter on a Bézier curve segment to a point.
        /// </summary>
        /// <param name="bezier">The Bézier curve.</param>
        /// <param name="point">The Point.</param>
        /// <returns>Returns the t parameter that represents the nearest point on a Bézier curve segment.</returns>
        public static double NearestT(this BezierSegmentX bezier, Point2D point)
            => ClosestParameter(bezier.CurveX, bezier.CurveY, point);

        /// <summary>
        /// Finds the nearest t parameter on a Quadratic Bézier curve segment to a point.
        /// </summary>
        /// <param name="bezier">The Quadratic Bézier curve.</param>
        /// <param name="point">The Point.</param>
        /// <returns>Returns the t parameter that represents the nearest point on a Quadratic Bézier curve segment.</returns>
        public static double NearestT(this QuadraticBezier bezier, Point2D point)
            => ClosestParameter(bezier.CurveX, bezier.CurveY, point);

        /// <summary>
        /// Finds the nearest t parameter on a Cubic Bézier curve segment to a point.
        /// </summary>
        /// <param name="bezier">The Cubic Bézier curve.</param>
        /// <param name="point">The Point.</param>
        /// <returns>Returns the t parameter that represents the nearest point on a Cubic Bézier curve segment.</returns>
        public static double NearestT(this CubicBezier bezier, Point2D point)
            => ClosestParameter(bezier.CurveX, bezier.CurveY, point);
        #endregion Nearest Extension Method Overloads

        #region Length Extension Method Overloads
        /// <summary>
        /// Finds the length of a point.
        /// </summary>
        /// <param name="point">The point</param>
        /// <returns>Returns 0.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this ScreenPoint point)
            => 0;

        /// <summary>
        /// Finds the length or magnitude of a 2-dimensional vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>Returns the length of a vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this Vector2D vector)
            => VectorMagnitude(vector.I, vector.J);

        /// <summary>
        /// Finds the length or magnitude of a 3-dimensional vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>Returns the length of a vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this Vector3D vector)
            => VectorMagnitude(vector.I, vector.J, vector.K);

        /// <summary>
        /// Finds the length or magnitude of a 4-dimensional vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>Returns the length of a vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this Vector4D vector)
            => VectorMagnitude(vector.I, vector.J, vector.K, vector.L);

        /// <summary>
        /// Finds the length of a Quaternion.
        /// </summary>
        /// <param name="quaternion">The Quaternion.</param>
        /// <returns>Returns the length of the Quaternion.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this QuaternionD quaternion)
            => QuaternionMagnitude(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

        /// <summary>
        /// Finds the length of a line segment.
        /// </summary>
        /// <param name="segment">The line segment.</param>
        /// <returns>The distance between the two end points on the line segment.</returns>
        /// <remarks>The Length is calculated as AC = SquarRoot(AB^2 + BC^2) </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this LineSegment segment)
            => Distance(segment.AX, segment.AY, segment.BX, segment.BY);

        /// <summary>
        /// Finds the length of a circular arc.
        /// </summary>
        /// <param name="arc">The arc.</param>
        /// <returns>Returns the arc length of a circular arc.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this CircularArc arc)
            => ArcLength(arc.Radius, arc.SweepAngle);

        /// <summary>
        /// Finds the length of the circumference of a circle.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <returns>Returns the distance around the circumference of a circle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this Circle circle)
            => CircleCircumference(circle.Radius);

        /// <summary>
        /// Finds the length of an elliptical arc segment.
        /// </summary>
        /// <param name="ellipticalArc">The Elliptical Arc.</param>
        /// <returns>Returns the arc length of the elliptical arc segment.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this EllipticalArc ellipticalArc)
            => EllipticalArcLength(ellipticalArc.StartPoint.X, ellipticalArc.StartPoint.Y, ellipticalArc.EndPoint.X, ellipticalArc.EndPoint.Y, ellipticalArc.StartAngle, ellipticalArc.EndAngle);

        /// <summary>
        /// Finds the approximate length of the perimeter of an elliptical arc.
        /// </summary>
        /// <param name="ellipse">The ellipse.</param>
        /// <returns>Returns the length of the circumference of the perimeter of the ellipse.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this Ellipse ellipse)
            => EllipsePerimeter(ellipse.RX, ellipse.RY);

        /// <summary>
        /// Finds the approximate length of a Bézier segment.
        /// </summary>
        /// <param name="bezier">The Bézier segment.</param>
        /// <returns>Returns the approximate length of the Bézier segment.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this BezierSegmentX bezier)
        {
            switch (bezier.Degree)
            {
                case PolynomialDegree.Constant:
                    return 0;
                case PolynomialDegree.Linear:
                    return Distance(bezier[0].X, bezier[0].Y, bezier[1].X, bezier[1].Y);
                case PolynomialDegree.Quadratic:
                    return QuadraticBezierArcLengthByIntegral(bezier[0].X, bezier[0].Y, bezier[1].X, bezier[1].Y, bezier[2].X, bezier[2].Y);
                case PolynomialDegree.Cubic:
                    return CubicBezierArcLength(bezier[0].X, bezier[0].Y, bezier[1].X, bezier[1].Y, bezier[2].X, bezier[2].Y, bezier[3].X, bezier[3].Y);
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// Finds the approximate length of a Quadratic curve.
        /// </summary>
        /// <param name="bezier">The Quadratic Bézier curve.</param>
        /// <returns>Returns the approximate length of a Quadratic Bézier curve.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this QuadraticBezier bezier)
            => QuadraticBezierArcLengthByIntegral(bezier.AX, bezier.AY, bezier.BX, bezier.BY, bezier.CX, bezier.CY);

        /// <summary>
        /// Finds the approximate length of a Cubic Bézier curve.
        /// </summary>
        /// <param name="bezier">The Cubic Bézier curve.</param>
        /// <returns>Returns the approximate length of a Cubic Bézier curve.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this CubicBezier bezier)
            => CubicBezierArcLength(bezier.AX, bezier.AY, bezier.BX, bezier.BY, bezier.CX, bezier.CY, bezier.DX, bezier.DY);
        #endregion Length Extension Method Overloads

        #region Length Squared Extension Method Overloads
        /// <summary>
        /// Finds the square length or magnitude of a 2-dimensional vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>Returns the square length of a vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LengthSquared(this Vector2D vector)
            => VectorMagnitudeSquared(vector.I, vector.J);

        /// <summary>
        /// Finds the square length or magnitude of a 3-dimensional vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>Returns the square length of a vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LengthSquared(this Vector3D vector)
            => VectorMagnitudeSquared(vector.I, vector.J, vector.K);

        /// <summary>
        /// Finds the square length or magnitude of a 4-dimensional vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>Returns the square length of a vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LengthSquared(this Vector4D vector)
            => VectorMagnitudeSquared(vector.I, vector.J, vector.K, vector.L);

        /// <summary>
        /// Finds the square distance of the length of a Quaternion.
        /// </summary>
        /// <param name="quaternion">The Quaternion.</param>
        /// <returns>Returns the length of a Quaternion.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LengthSquared(this QuaternionD quaternion)
            => QuaternionNormal(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

        /// <summary>
        /// Finds the square length of a line segment.
        /// </summary>
        /// <param name="segment">The line segment.</param>
        /// <returns>Returns the square length of the distance between two points of a line segment.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LengthSquared(this LineSegment segment)
            => SquareDistance(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);
        #endregion Length Squared Extension Method Overloads

        #region Bounds Extension Method Overloads
        /// <summary>
        /// Finds the axis aligned bounding box (AABB) rectangle that fully encompasses a line segment.
        /// </summary>
        /// <param name="segment">The line segment.</param>
        /// <returns>Returns a rectangle representing the axis aligned bounding box (AABB) that contains the line segment.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this LineSegment segment)
            => LineSegmentBounds(segment.A, segment.B);

        /// <summary>
        /// Finds the axis aligned bounding box (AABB) rectangle that fully encompasses a circle.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <returns>Returns a rectangle representing the axis aligned bounding box (AABB) that contains the circle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this Circle circle)
            => CircleBounds(circle.X, circle.Y, circle.Radius);

        /// <summary>
        /// Finds the axis aligned bounding box (AABB) rectangle that fully encompasses a circular arc.
        /// </summary>
        /// <param name="arc">The circular arc.</param>
        /// <returns>Returns a rectangle representing the axis aligned bounding box (AABB) that contains the circular arc.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this CircularArc arc)
            => CircularArcBounds(arc.X, arc.Y, arc.Radius, 0, arc.StartAngle, arc.SweepAngle);

        /// <summary>
        /// Finds the axis aligned bounding box (AABB) rectangle that fully encompasses an ellipse.
        /// </summary>
        /// <param name="ellipse">The Ellipse.</param>
        /// <returns>Returns a rectangle representing the axis aligned bounding box (AABB) that contains the ellipse.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this Ellipse ellipse)
            => EllipseBounds(ellipse.X, ellipse.Y, ellipse.RX, ellipse.RY, ellipse.Angle);

        /// <summary>
        /// Finds the axis aligned bounding box (AABB) rectangle that fully encompasses an elliptical arc.
        /// </summary>
        /// <param name="arc">The elliptical arc.</param>
        /// <returns>Returns a rectangle representing the axis aligned bounding box (AABB) that contains the elliptical arc.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this EllipticalArc arc)
            => EllipticalArcBounds(arc.X, arc.Y, arc.RX, arc.RY, arc.Angle, arc.StartAngle, arc.SweepAngle);

        /// <summary>
        /// Finds the axis aligned bounding box (AABB) rectangle that fully encompasses a polygon contour.
        /// </summary>
        /// <param name="contour">The polygon contour.</param>
        /// <returns>Returns a rectangle representing the axis aligned bounding box (AABB) that contains the polygon contour.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this PolygonContour contour)
            => PolygonBounds(contour.Points);

        /// <summary>
        /// Finds the axis aligned bounding box (AABB) rectangle that fully encompasses a polyline.
        /// </summary>
        /// <param name="polyline">The Polyline.</param>
        /// <returns>Returns a rectangle representing the axis aligned bounding box (AABB) that contains the polyline.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this Polyline polyline)
            => PolygonBounds(polyline.Points);

        /// <summary>
        /// Finds the axis aligned bounding box (AABB) rectangle that fully encompasses a Bézier curve segment.
        /// </summary>
        /// <param name="bezier">The Bézier curve segment.</param>
        /// <returns>Returns a rectangle representing the axis aligned bounding box (AABB) that contains the Bézier curve segment.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this BezierSegmentX bezier)
        {
            switch (bezier.Degree)
            {
                case PolynomialDegree.Constant:
                    return Rectangle2D.Empty;
                case PolynomialDegree.Linear:
                    return LineSegmentBounds(bezier[0].X, bezier[0].Y, bezier[1].X, bezier[1].Y);
                case PolynomialDegree.Quadratic:
                    //return QuadraticBezierBounds(bezier[0].X, bezier[0].Y, bezier[1].X, bezier[1].Y, bezier[2].X, bezier[2].Y);
                    return BezierBounds(bezier.CurveX, bezier.CurveY);
                case PolynomialDegree.Cubic:
                    //return CubicBezierBounds(bezier[0].X, bezier[0].Y, bezier[1].X, bezier[1].Y, bezier[2].X, bezier[2].Y, bezier[3].X, bezier[3].Y);
                    return BezierBounds(bezier.CurveX, bezier.CurveY);
                default:
                    return Rectangle2D.Empty;
            }
        }

        /// <summary>
        /// Finds the axis aligned bounding box (AABB) rectangle that fully encompasses a Quadratic Bézier curve.
        /// </summary>
        /// <param name="bezier">The Quadratic Bézier curve.</param>
        /// <returns>Returns a rectangle representing the axis aligned bounding box (AABB) that contains the Quadratic Bézier curve.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this QuadraticBezier bezier)
            //=> QuadraticBezierBounds(bezier.AX, bezier.AY, bezier.BX, bezier.BY, bezier.CX, bezier.CY);
            => BezierBounds(bezier.CurveX, bezier.CurveY);

        /// <summary>
        /// Finds the axis aligned bounding box (AABB) rectangle that fully encompasses a Cubic Bézier curve.
        /// </summary>
        /// <param name="bezier">The Cubic Bézier curve.</param>
        /// <returns>Returns a rectangle representing the axis aligned bounding box (AABB) that contains the Cubic Bézier curve.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this CubicBezier bezier)
            //=> CubicBezierBounds(bezier.AX, bezier.AY, bezier.BX, bezier.BY, bezier.CX, bezier.CY, bezier.DX, bezier.DY);
            => BezierBounds(bezier.CurveX, bezier.CurveY);

        /// <summary>
        /// Finds the axis aligned bounding box (AABB) rectangle that fully encompasses a Polycurve contour path.
        /// </summary>
        /// <param name="path">The polycurve contour path.</param>
        /// <returns>Returns a rectangle representing the axis aligned bounding box (AABB) that contains the Polycurve contour path.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this PolycurveContour path)
            => PolycurveContourBounds(path);
        #endregion Bounds Extension Method Overloads

        #region Area Extension Method Overloads
        /// <summary>
        /// Finds the area of a triangle.
        /// </summary>
        /// <param name="triangle">The triangle.</param>
        /// <returns>Returns the area of the triangle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this Triangle triangle)
            => Abs(SignedTriangleArea(triangle.A.X, triangle.A.Y, triangle.B.X, triangle.B.Y, triangle.C.X, triangle.C.Y));

        /// <summary>
        /// Finds the area of a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>Returns the area of the rectangle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this Rectangle2D rectangle)
            => (rectangle.Height == rectangle.Width) ? SquareArea(rectangle.Width) : RectangleArea(rectangle.Width, rectangle.Height);

        /// <summary>
        /// Finds the area of a polyline.
        /// </summary>
        /// <param name="polyline">The Polyline.</param>
        /// <returns>Returns the area of the polyline.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this Polyline polyline)
            => Abs(SignedPolygonArea(polyline.Points));

        /// <summary>
        /// Finds the area of a polygon contour.
        /// </summary>
        /// <param name="contour">The polygon contour.</param>
        /// <returns>Returns the area of the polygon contour.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this PolygonContour contour)
            => Abs(SignedPolygonArea(contour.Points));

        /// <summary>
        /// Finds the area of a circular arc.
        /// </summary>
        /// <param name="arc">The circular arc.</param>
        /// <returns>Returns the area of a circular arc.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this CircularArc arc)
            => CircularArcSectorArea(arc.Radius, arc.SweepAngle);

        /// <summary>
        /// Finds the area of a circle.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <returns>Returns the area of the circle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this Circle circle)
            => CircleArea(circle.Radius);

        /// <summary>
        /// Finds the area of an elliptical arc.
        /// </summary>
        /// <param name="arc">The elliptical arc.</param>
        /// <returns>Returns the area of a circular arc.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this EllipticalArc arc)
            => EllipticalArcSectorArea(arc.RX, arc.RY, arc.StartAngle, arc.SweepAngle);

        /// <summary>
        /// Finds the area of an ellipse.
        /// </summary>
        /// <param name="ellipse">The ellipse.</param>
        /// <returns>Returns the area of the ellipse.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this Ellipse ellipse)
            => EllipseArea(ellipse.RX, ellipse.RY);
        #endregion Area Extension Method Overloads

        #region Distance Methods
        /// <summary>
        /// Calculates the distance between two points in 2-dimensional euclidean space.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <returns>Returns the distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(
            double x1, double y1,
            double x2, double y2)
            => Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

        /// <summary>
        /// Calculates the distance between two points in 3-dimensional euclidean space.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="z1">First Z component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <param name="z2">Second Z component.</param>
        /// <returns>Returns the distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1));

        /// <summary>
        /// Calculates the distance between two points in 4-dimensional space.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="z1">First Z component.</param>
        /// <param name="w1">First W component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <param name="z2">Second Z component.</param>
        /// <param name="w2">Second W component.</param>
        /// <returns>Returns the distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(
            double x1, double y1, double z1, double w1,
            double x2, double y2, double z2, double w2)
            => Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1) + (w2 - w1) * (w2 - w1));

        /// <summary>
        /// Calculates the distance between a point and a line segment, constrained to the area perpendicular to the line segment.
        /// </summary>
        /// <param name="aX">The x-component of the first point of the line segment.</param>
        /// <param name="aY">The y-component of the first point of the line segment.</param>
        /// <param name="bX">The x-component of the second point of the line segment.</param>
        /// <param name="bY">The y-component of the second point of the line segment.</param>
        /// <param name="pX">The x-component of the point.</param>
        /// <param name="pY">The y-component of the point.</param>
        /// <returns>Returns the distance to the line segment or null if the point is not in the area of perpendicularity.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double? ConstrainedDistanceLineSegmentPoint(
            double aX, double aY, double bX, double bY,
            double pX, double pY)
        {
            // Vector of line segment a->b
            (var ui, var uj) = (bX - aX, bY - aY);

            // Vector a->p
            (var vi, var vj) = (aX - pX, aY - pY);

            // Get the determinant or squared length of the line segment.
            var d = ui * ui + uj * uj;

            // Get the length of the line segment.
            var length = Sqrt(d);

            // Check whether the line is a line or a point.
            if (length == 0) return null;

            // Find the interpolation value.
            var t = -(vi * ui + vj * uj) / d;

            // Check whether the closest point falls on the line segment.
            if (t < 0d || t > 1d) return null;

            // Return the length to the nearest point on the line segment.
            return (length == 0)
                ? Sqrt(vi * vi + vj * vj)
                : Abs(ui * vj - vi * uj) / length;
        }

        /// <summary>
        /// Calculates the distance between a point and a line.
        /// </summary>
        /// <param name="aX">The x-component of the location point of the line.</param>
        /// <param name="aY">The y-component of the location point of the line.</param>
        /// <param name="ai">The i-component of the vector of the line.</param>
        /// <param name="aj">The j-component of the vector of the line.</param>
        /// <param name="pX">The x-component of the test point.</param>
        /// <param name="pY">The y-component of the test point.</param>
        /// <returns>Returns the shortest distance to the line from the point.</returns>
        /// <acknowledgment>
        /// From answer at: http://stackoverflow.com/a/2255848
        /// formula here: http://mathworld.wolfram.com/Point-LineDistance2-Dimensional.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceLinePoint(
            double aX, double aY, double ai, double aj,
            double pX, double pY)
        {
            // Vector a->p
            (var vi, var vj) = (aX - pX, aY - pY);

            // Get the length of the line segment.
            var length = Sqrt(ai * ai + aj * aj);

            // Return the length to the nearest point on the line.
            return (length == 0)
                ? Sqrt(vi * vi + vj * vj)
                : Abs(ai * vj - vi * aj) / length;
        }

        /// <summary>
        /// Calculates the distance between a point and a line segment.
        /// </summary>
        /// <param name="aX">The x-component of the first point of the line segment.</param>
        /// <param name="aY">The y-component of the first point of the line segment.</param>
        /// <param name="bX">The x-component of the second point of the line segment.</param>
        /// <param name="bY">The y-component of the second point of the line segment.</param>
        /// <param name="pX">The x-component of the point.</param>
        /// <param name="pY">The y-component of the point.</param>
        /// <returns>Returns the distance between the line segment and the point.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceLineSegmentPoint(
            double aX, double aY, double bX, double bY,
            double pX, double pY)
        {
            // Vector of line segment a->b
            (var ui, var uj) = (bX - aX, bY - aY);

            // Vector a->p
            (var vi, var vj) = (aX - pX, aY - pY);

            // Vector b->p
            (var wi, var wj) = (bX - pX, bY - pY);

            // Get the determinant or squared length of the line segment.
            var d = ui * ui + uj * uj;

            // Get the length of the line segment.
            var length = Sqrt(d);

            // Find the interpolation value.
            var t = -(vi * ui + vj * uj) / d;

            // Return the distance to the nearest point on the line segment.
            return (t < 0d) /* Check whether the closest point falls between the ends of line segment. */
                ? Sqrt(vi * vi + vj * vj)
                : (t > 1d)
                ? Sqrt(wi * wi + wj * wj)
                : (length == 0)
                ? Sqrt(vi * vi + vj * vj)
                : Abs(ui * vj - vi * uj) / length;
        }
        #endregion Distance Methods

        #region Square Distance Methods
        /// <summary>
        /// Calculates the Magnitude or squared length of a vector.
        /// </summary>
        /// <param name="i">The i parameter.</param>
        /// <param name="j">The j parameter.</param>
        /// <returns>Returns the squared magnitude of the vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitudeSquared(double i, double j)
            => i * i + j * j;

        /// <summary>
        /// Calculates the Magnitude or squared length of a vector.
        /// </summary>
        /// <param name="i">The i parameter.</param>
        /// <param name="j">The j parameter.</param>
        /// <param name="k">The k parameter.</param>
        /// <returns>Returns the squared magnitude of the vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitudeSquared(double i, double j, double k)
            => i * i + j * j + k * k;

        /// <summary>
        /// Calculates the Magnitude or squared length of a vector.
        /// </summary>
        /// <param name="i">The i parameter.</param>
        /// <param name="j">The j parameter.</param>
        /// <param name="k">The k parameter.</param>
        /// <param name="l">The l parameter.</param>
        /// <returns>Returns the squared magnitude of the vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitudeSquared(double i, double j, double k, double l)
            => i * i + j * j + k * k + l * l;

        /// <summary>
        /// Calculates the normal or squared length of a Quaternion.
        /// </summary>
        /// <param name="x">The x component of the Quaternion.</param>
        /// <param name="y">The y component of the Quaternion.</param>
        /// <param name="z">The z component of the Quaternion.</param>
        /// <param name="w">The w component of the Quaternion.</param>
        /// <returns>Returns the normal of the Quaternion.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuaternionNormal(double x, double y, double z, double w)
            => (x * x) + (y * y) + (z * z) + (w * w);

        /// <summary>
        /// Calculates the square of the distance between two 2-dimensional euclidean points.
        /// </summary>
        /// <param name="x1">The x component of the first point.</param>
        /// <param name="y1">The y component of the first point.</param>
        /// <param name="x2">The x component of the second point.</param>
        /// <param name="y2">The y component of the second point.</param>
        /// <returns>Returns the squared length of the distance between two points</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistance(
            double x1, double y1,
            double x2, double y2)
            => (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);

        /// <summary>
        /// Calculates the square of the distance between two 3-dimensional euclidean points.
        /// </summary>
        /// <param name="x1">The x component of the first point.</param>
        /// <param name="y1">The y component of the first point.</param>
        /// <param name="z1">The z component of the first point.</param>
        /// <param name="x2">The x component of the second point.</param>
        /// <param name="y2">The y component of the second point.</param>
        /// <param name="z2">The z component of the second point.</param>
        /// <returns>Returns the squared length of the distance between two points</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistance(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1);

        /// <summary>
        /// Calculates the square of the distance between two 4-dimensional points.
        /// </summary>
        /// <param name="x1">The x component of the first point.</param>
        /// <param name="y1">The y component of the first point.</param>
        /// <param name="z1">The z component of the first point.</param>
        /// <param name="w1">The w component of the first point.</param>
        /// <param name="x2">The x component of the second point.</param>
        /// <param name="y2">The y component of the second point.</param>
        /// <param name="z2">The z component of the second point.</param>
        /// <param name="w2">The w component of the second point.</param>
        /// <returns>Returns the squared length of the distance between two points</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistance(
            double x1, double y1, double z1, double w1,
            double x2, double y2, double z2, double w2)
            => (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1) + (w2 - z1) * (w2 - w1);

        /// <summary>
        /// Calculates the square of the distance of a point from a line.
        /// </summary>
        /// <param name="ax">The x component of the first point on the line.</param>
        /// <param name="ay">The y component of the first point on the line.</param>
        /// <param name="bx">The x component of the second point on the line.</param>
        /// <param name="by">The y component of the second point on the line.</param>
        /// <param name="px">The x component of the Point.</param>
        /// <param name="py">The y component of the Point.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistanceLineSegmentPoint(
            double ax, double ay, double bx, double by,
            double px, double py)
        {
            // Vector of line segment a->b
            (var ui, var uj) = (bx - ax, by - ay);

            // Vector a->p
            (var vi, var vj) = (ax - px, ay - py);

            // Vector b->p
            (var wi, var wj) = (bx - px, by - py);

            var c = ui * vj - vi * uj;

            // Get the determinant or squared length of the line segment.
            var d = ui * ui + uj * uj;

            // Find the interpolation value.
            var t = -(vi * ui + vj * uj) / d;

            // Return the distance to the nearest point on the line segment.
            return (t < 0d) /* Check whether the closest point falls between the ends of line segment. */
                ? (vi * vi + vj * vj)
                : (t > 1d)
                ? (wi * wi + wj * wj)
                : (d == 0)
                ? (vi * vi + vj * vj)
                : c * c / d;
        }

        /// <summary>
        /// Calculates the square of the distance of a point from a line.
        /// </summary>
        /// <param name="lx">The x component of the first point on the line.</param>
        /// <param name="ly">The y component of the first point on the line.</param>
        /// <param name="li">The x component of the second point on the line.</param>
        /// <param name="lj">The y component of the second point on the line.</param>
        /// <param name="px">The x component of the Point.</param>
        /// <param name="py">The y component of the Point.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistanceLinePoint(
            double lx, double ly, double li, double lj,
            double px, double py)
        {
            var c = lj * px + li * py - (lj * lx + li * ly);
            var d = lj * lj + li * li;
            return c * c / d;
        }
        #endregion Square Distance Methods

        #region Nearest Methods
        /// <summary>
        /// Finds the nearest point on a line segment to a point.
        /// </summary>
        /// <param name="aX">The x component of the first point on the line segment.</param>
        /// <param name="aY">The y component of the first point on the line segment.</param>
        /// <param name="bX">The x component of the second point on the line segment.</param>
        /// <param name="bY">The y component of the second point on the line segment.</param>
        /// <param name="pX">The x component of the point.</param>
        /// <param name="pY">The y component of the point.</param>
        /// <returns>Returns the nearest point on a line segment to a point.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) NearestPointOnLineSegment(
            double aX, double aY, double bX, double bY,
            double pX, double pY)
        {
            // Vector of line segment a->b
            (var ui, var uj) = (bX - aX, bY - aY);

            // Vector a->p
            (var vi, var vj) = (pX - aX, pY - aY);

            // Get the determinant or squared length of the line segment.
            var d = ui * ui + uj * uj;

            // The dot product of the u and v vectors.
            var a = vi * ui + vj * uj;

            // Find the interpolation value, the normalized "distance" from a to the closest point
            var t = d == 0 ? a : a / d;

            // Return the nearest point on the line segment.
            return (t < 0d) /* Check whether the closest point falls between the ends of line segment. */
                ? (aX, aY)
                : (t > 1d)
                ? (bX, bY)
                : (aX + ui * t, aY + uj * t);
        }

        /// <summary>
        /// Finds the nearest point on an elliptical arc to a point.
        /// </summary>
        /// <param name="cX">The x-component of the center point of the ellipse.</param>
        /// <param name="cY">The y-component of the center point of the ellipse.</param>
        /// <param name="rX">The x radius of the ellipse.</param>
        /// <param name="rY">The y radius of the ellipse.</param>
        /// <param name="a0">The starting angle of the elliptical arc.</param>
        /// <param name="a1">The ending angle of the elliptical arc.</param>
        /// <param name="pX">The x-component of the point.</param>
        /// <param name="pY">The y-component of the point.</param>
        /// <returns>The nearest point on the elliptical arc to a point.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/36260793/algorithm-for-shortest-distance-from-a-point-to-an-elliptic-arc?rq=1
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) NearestPointOnEllipticalArc(
            double cX, double cY,
            double rX, double rY,
            double a0, double a1,
            double pX, double pY)
        {
            int e;
            double a, x, y;

            while (a0 >= a1)
                // just make sure a0<a1
                a0 -= Tau;

            // 25 sample points in first iteration
            var b0 = a0;
            var b1 = a1;
            var da = (b1 - b0) / 25.0;

            // No best solution yet
            double ll = -1;
            var aa = a0;

            // More recursions means more accurate result
            for (var i = 0; i < 3; i++)
            {
                // Sample arc a=<b0,b1> with step da
                for (e = 1, a = b0; e != 0; a += da)
                {
                    if (a >= b1) { a = b1; e = 0; }

                    // Elliptic arc sampled point
                    x = cX + rX * Cos(a);
                    y = cY - rY * Sin(a); // The y axis is in reverse order therefore - distance^2 to x_in,y_in
                    x -= pX;
                    x *= x;
                    y -= pY;
                    y *= y;
                    var l = x + y;

                    // Remember best solution
                    if ((ll < 0d) || (ll > l))
                    {
                        aa = a;
                        ll = l;
                    }
                }

                // Use just area near found solution aa
                b0 = aa - da; if (b0 < a0) b0 = a0;
                b1 = aa + da; if (b1 > a1) b1 = a1;

                // 10 points per area stop if too small area already
                da = 0.1 * (b1 - b0); if (da < 1e-6) break;
            }

            // Mine y axis is in reverse order therefore -
            return (cX + rX * Cos(aa), cY - rY * Sin(aa));
        }

        /// <summary>
        /// Finds the nearest point on a Cubic Bézier curve.
        /// </summary>
        /// <param name="p0X">The x-component of the starting point of the curve.</param>
        /// <param name="p0Y">The y-component of the starting point of the curve.</param>
        /// <param name="p1X">The x-component of the handle of the curve.</param>
        /// <param name="p1Y">The y-component of the handle of the curve.</param>
        /// <param name="p2X">The x-component of the ending point of the curve.</param>
        /// <param name="p2Y">The y-component of the ending point of the curve.</param>
        /// <param name="pX">The x-component of the point.</param>
        /// <param name="pY">The y-component of the point.</param>
        /// <returns>Returns the nearest point on the Quadratic Bézier curve to a point.</returns>
        //[DebuggerStepThrough]
        [Obsolete(message: "Not yet implemented.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double X, double Y)? NearestPointOnQuadraticBezier(
            double p0Y, double p0X,
            double p1X, double p1Y,
            double p2X, double p2Y,
            double pX, double pY)
            => null;

        /// <summary>
        /// Finds the nearest point on a Cubic Bézier curve.
        /// </summary>
        /// <param name="p0X">The x-component of the starting point of the curve.</param>
        /// <param name="p0Y">The y-component of the starting point of the curve.</param>
        /// <param name="p1X">The x-component of the first handle of the curve.</param>
        /// <param name="p1Y">The y-component of the first handle of the curve.</param>
        /// <param name="p2X">The x-component of the second handle of the curve.</param>
        /// <param name="p2Y">The y-component of the second handle of the curve.</param>
        /// <param name="p3X">The x-component of the ending point of the curve.</param>
        /// <param name="p3Y">The y-component of the ending point of the curve.</param>
        /// <param name="pX">The x-component of the point.</param>
        /// <param name="pY">The y-component of the point.</param>
        /// <returns>Returns the nearest point on the Cubic Bézier curve to a point.</returns>
        //[DebuggerStepThrough]
        [Obsolete(message: "Not yet implemented.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double X, double Y)? NearestPointOnCubicBezier(
            double p0X, double p0Y,
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double pX, double pY)
            => null;
        #endregion Nearest Methods

        #region Length Methods
        /// <summary>
        /// Calculates the magnitude or length of a vector.
        /// </summary>
        /// <param name="i">The i parameter of the vector.</param>
        /// <param name="j">The j parameter of the vector.</param>
        /// <returns>Returns the magnitude of the vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitude(double i, double j)
            => Sqrt(i * i + j * j);

        /// <summary>
        /// Calculates the magnitude or length of a vector.
        /// </summary>
        /// <param name="i">The i parameter of the vector.</param>
        /// <param name="j">The j parameter of the vector.</param>
        /// <param name="k">The k parameter of the vector.</param>
        /// <returns>Returns the magnitude of the vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitude(double i, double j, double k)
            => Sqrt(i * i + j * j + k * k);

        /// <summary>
        /// Calculates the magnitude or length of a vector.
        /// </summary>
        /// <param name="i">The i parameter of the vector.</param>
        /// <param name="j">The j parameter of the vector.</param>
        /// <param name="k">The k parameter of the vector.</param>
        /// <param name="l">The l parameter of the vector.</param>
        /// <returns>Returns the magnitude of the vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitude(double i, double j, double k, double l)
            => Sqrt(i * i + j * j + k * k + l * l);

        /// <summary>
        /// Calculates the length or magnitude of a Quaternion.
        /// </summary>
        /// <param name="x">The x component of the Quaternion.</param>
        /// <param name="y">The y component of the Quaternion.</param>
        /// <param name="z">The z component of the Quaternion.</param>
        /// <param name="w">The w component of the Quaternion.</param>
        /// <returns>Returns the magnitude of the Quaternion.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuaternionMagnitude(double x, double y, double z, double w)
            => Sqrt((x * x) + (y * y) + (z * z) + (w * w));

        /// <summary>
        /// Calculates the arc length of a circular arc.
        /// </summary>
        /// <param name="r">The radius of the arc.</param>
        /// <param name="sweepAngle">The sweep angle of the arc.</param>
        /// <returns>Returns the arc length of the circular arc.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ArcLength(double r, double sweepAngle)
            => 2d * PI * r * -sweepAngle;

        /// <summary>
        /// Calculates the length of the circumference of a circle.
        /// </summary>
        /// <param name="r">The radius of the circle.</param>
        /// <returns>Returns the length of the circumference of the circle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircleCircumference(double r)
            => 2d * r * PI;

        /// <summary>
        /// Calculates the length of the perimeter of an ellipse.
        /// </summary>
        /// <param name="a">The a parameter, or x radius of the ellipse.</param>
        /// <param name="b">The b parameter, or y radius of the ellipse.</param>
        /// <returns>Returns the length of the perimeter of the ellipse.</returns>
        /// <acknowledgment>
        /// http://www.ebyte.it/library/docs/math05a/EllipseCircumference05.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipsePerimeter(double a, double b)
            => 4d * ((PI * a * b) + ((a - b) * (a - b))) / (a + b);

        /// <summary>
        /// Calculates the arc length of an elliptical arc.
        /// </summary>
        /// <param name="startX">The x component of the start point of the elliptical arc.</param>
        /// <param name="startY">The y component of the start point of the elliptical arc.</param>
        /// <param name="endX">The x component of the end point of the elliptical arc.</param>
        /// <param name="endY">The y component of the end point of the elliptical arc.</param>
        /// <param name="startAngle">The start angle of the elliptical arc.</param>
        /// <param name="endAngle">The end angle of the elliptical arc.</param>
        /// <returns>Returns the arc length of the elliptical arc.</returns>
        /// <acknowledgment>
        /// http://www.iosrjournals.org/iosr-jm/papers/Vol3-issue2/B0320813.pdf
        /// http://mathforum.org/kb/servlet/JiveServlet/download/130-2391290-7852023-766514/PERIMETER%20OF%20THE%20ELLIPTICAL%20ARC%20A%20GEOMETRIC%20METHOD.pdf
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipticalArcLength(double startX, double startY, double endX, double endY, double startAngle, double endAngle)
            => /*ChordLength*/Sqrt(Abs(endX - startX) * Abs(endX - startX) + Abs(endY - startY) * Abs(endY - startY))
            / /*Middle Angle*/(2 * Sin(OneHalf * (startAngle - endAngle)))
            * (startAngle - endAngle);

        /// <summary>
        /// Calculates the closed-form solution to elliptic integral for arc length.
        /// </summary>
        /// <param name="ax">The starting x-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="ay">The starting y-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="bx">The middle x-coordinate for the tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="by">The middle y-coordinate for the tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="cx">The closing x-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="cy">The closing y-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns>Returns the arc length of the Quadratic Bézier curve.</returns>
        /// <acknowledgment>
        /// https://algorithmist.wordpress.com/2009/01/05/quadratic-bezier-arc-length/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadraticBezierArcLengthByIntegral(
            double ax, double ay,
            double bx, double by,
            double cx, double cy)
        {
            var _ax = ax - 2d * bx + cx;
            var _ay = ay - 2d * by + cy;
            var _bx = 2d * bx - 2d * ax;
            var _by = 2d * by - 2d * ay;

            var a = 4d * (_ax * _ax + _ay * _ay);
            var b = 4d * (_ax * _bx + _ay * _by);
            var c = _bx * _bx + _by * _by;

            var abc = 2d * Sqrt(a + b + c);
            var a2 = Sqrt(a);
            var a32 = 2d * a * a2;
            var c2 = 2d * Sqrt(c);
            var ba = b / a2;

            return (a32 * abc + a2 * b * (abc - c2) + (4d * c * a - b * b) * Log((2d * a2 + ba + abc) / (ba + c2))) / (4d * a32);
        }

        /// <summary>
        /// Calculates the arc length of a Cubic Bézier curve.
        /// </summary>
        /// <param name="ax">The starting x-coordinate for the <see cref="CubicBezier"/> curve.</param>
        /// <param name="ay">The starting y-coordinate for the <see cref="CubicBezier"/> curve.</param>
        /// <param name="bx">The first x-coordinate for the tangent control node for the <see cref="CubicBezier"/> curve.</param>
        /// <param name="by">The first y-coordinate for the tangent control node for the <see cref="CubicBezier"/> curve.</param>
        /// <param name="cx">The second x-coordinate for the tangent control node for the <see cref="CubicBezier"/> curve.</param>
        /// <param name="cy">The second y-coordinate for the tangent control node for the <see cref="CubicBezier"/> curve.</param>
        /// <param name="dx">The closing x-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="dy">The closing y-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns>Returns the arc length of the Cubic Bézier curve.</returns>
        /// <acknowledgment>
        /// http://steve.hollasch.net/cgindex/curves/cbezarclen.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicBezierArcLength(
            double ax, double ay,
            double bx, double by,
            double cx, double cy,
            double dx, double dy)
        {
            var k1 = new Point2D(-ax + 3d * (bx - cx) + dx, -ay + 3d * (by - cy) + dy);
            var k2 = new Point2D(3d * (ax + cx) - 6d * bx, 3d * (ay + cy) - 6d * by);
            var k3 = new Point2D(3d * (bx - ax), 3d * (by - ax));
            var k4 = new Point2D(ax, ay);

            var q1 = 9d * (Sqrt(Abs(k1.X)) + Sqrt(Abs(k1.Y)));
            var q2 = 12d * (k1.X * k2.X + k1.Y * k2.Y);
            var q3 = 3d * (k1.X * k3.X + k1.Y * k3.Y) + 4d * (Sqrt(Abs(k2.X)) + Sqrt(Abs(k2.Y)));
            var q4 = 4d * (k2.X * k3.X + k2.Y * k3.Y);
            var q5 = Sqrt(Abs(k3.X)) + Sqrt(Abs(k3.Y));

            // Approximation algorithm based on Simpson.
            const double a = 0d;
            const double b = 1d;
            const int n_limit = 1024;
            const double TOLERANCE = 0.001d;

            var n = 1;

            var multiplier = (b - a) / 6d;
            var endsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a) + CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, b);
            var interval = (b - a) * 0.5d;
            var asum = 0d;
            var bsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a + interval);
            var est1 = multiplier * (endsum + 2d * asum + 4d * bsum);
            var est0 = 2d * est1;

            while (n < n_limit && Abs(est1) > 0d && Abs((est1 - est0) / est1) > TOLERANCE)
            {
                n *= 2;
                multiplier /= 2d;
                interval /= 2d;
                asum += bsum;
                bsum = 0d;
                est0 = est1;
                var interval_div_2n = interval / (2d * n);

                for (var i = 1; i < 2 * n; i += 2)
                {
                    var t = a + i * interval_div_2n;
                    bsum += CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, t);
                }

                est1 = multiplier * (endsum + 2d * asum + 4d * bsum);
            }

            return est1 * 10d;
        }

        /// <summary>
        /// Calculates the Bézier Arc Length.
        /// </summary>
        /// <param name="t">The t parameter.</param>
        /// <param name="q1"></param>
        /// <param name="q2"></param>
        /// <param name="q3"></param>
        /// <param name="q4"></param>
        /// <param name="q5"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://steve.hollasch.net/cgindex/curves/cbezarclen.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double CubicBezierArcLengthHelper(
            ref double q1,
            ref double q2,
            ref double q3,
            ref double q4,
            ref double q5,
            double t)
        {
            var result = q5 + t * (q4 + t * (q3 + t * (q2 + t * q1)));
            result = Sqrt(Abs(result));
            return result;
        }

        /// <summary>
        /// Calculates the perimeter of a polygon contour.
        /// </summary>
        /// <param name="points">The points of the polygon contour.</param>
        /// <returns>Returns the length of the perimeter of the polygon contour.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PolygonContourPerimeter(this Point2D[] points)
            => points.Length > 0d ? points.Zip(points.Skip(1), Distance).Sum() + points[0].Distance(points[points.Length - 1]) : 0d;

        /// <summary>
        /// Calculates the perimeter of a polygon contour.
        /// </summary>
        /// <param name="points">The points of the polygon contour.</param>
        /// <returns>Returns the length of the perimeter of the polygon contour.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PolygonContourPerimeter(this IEnumerable<Point2D> points)
            => PolygonContourPerimeter(points as List<Point2D>);

        /// <summary>
        /// Calculates the perimeter of a polygon contour.
        /// </summary>
        /// <param name="points">The points of the polygon contour.</param>
        /// <returns>Returns the length of the perimeter of the polygon contour.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PolygonContourPerimeter(this List<Point2D> points)
            => points.Count > 0d ? points.Zip(points.Skip(1), Distance).Sum() + points[0].Distance(points[points.Count - 1]) : 0d;
        #endregion Length Methods

        #region BoundsMethods
        /// <summary>
        /// Finds the Axis Aligned Bounding Box (AABB) Rectangle of a line segment.
        /// </summary>
        /// <param name="a">The first point of the line segment.</param>
        /// <param name="b">The second point of the line segment.</param>
        /// <returns>Returns the Axis Aligned Bounding Box (AABB) Rectangle that fully encompasses the line segment.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D LineSegmentBounds(Point2D a, Point2D b)
            => new Rectangle2D(a, b);

        /// <summary>
        /// Finds the Axis Aligned Bounding Box (AABB) Rectangle of a line segment.
        /// </summary>
        /// <param name="x0">The x-component of the first point of the line segment.</param>
        /// <param name="y0">The y-component of the first point of the line segment.</param>
        /// <param name="x1">The x-component of the second point of the line segment.</param>
        /// <param name="y1">The y-component of the second point of the line segment.</param>
        /// <returns>Returns the Axis Aligned Bounding Box (AABB) Rectangle that fully encompasses the line segment.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D LineSegmentBounds2(
            double x0, double y0,
            double x1, double y1)
            => new Rectangle2D(new Point2D(x0, y0), new Point2D(x1, y1));

        /// <summary>
        /// Finds the Axis Aligned Bounding Box (AABB) Rectangle of a line segment.
        /// </summary>
        /// <param name="aX">The x-component of the first point of the line segment.</param>
        /// <param name="aY">The y-component of the first point of the line segment.</param>
        /// <param name="bX">The x-component of the second point of the line segment.</param>
        /// <param name="bY">The y-component of the second point of the line segment.</param>
        /// <returns>Returns the Axis Aligned Bounding Box (AABB) Rectangle that fully encompasses the line segment.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D LineSegmentBounds(
            double aX, double aY,
            double bX, double bY)
            => Rectangle2D.FromLTRB
            (
                aX <= bX ? aX : bX,
                aY <= bY ? aY : bY,
                aX >= bX ? aX : bX,
                aY >= bY ? aY : bY
            );

        /// <summary>
        /// Calculate the Axis Aligned Bounding Box (AABB) square boundaries of a circle.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of the Circle.</param>
        /// <returns>Returns A Rectangle that is the Axis Aligned Bounding Box (AABB) of the size and location to envelop the circle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D CircleBounds(
            double cX, double cY,
            double r)
            => Rectangle2D.FromLTRB(cX - r, cY - r, cX + r, cY + r);

        /// <summary>
        /// Calculates the Axis Aligned Bounding Box (AABB) rectangle of a circular arc.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of the Circle.</param>
        /// <param name="angle">The angle of orientation of the ellipse.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>Returns an Axis Aligned Bounding Box (AABB) Rectangle large enough to closely fit the circular arc inside. The close bounding box of a circular arc.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D CircularArcBounds(
            double cX, double cY,
            double r,
            double angle,
            double startAngle, double sweepAngle)
        {
            var start = (angle + startAngle).WrapAngle();
            var end = (start + sweepAngle).WrapAngle();

            var bounds = new Rectangle2D(
                new Point2D(cX + r * Cos(startAngle), cY + r * Sin(startAngle)),
                new Point2D(cX + r * Cos(end), cY + r * Sin(end)));

            // Expand the boundaries if any of the extreme angles fall within the sweep angle.
            if (Intersections.Within(0, start, sweepAngle))
                bounds.Right = cX + r;
            if (Intersections.Within(HalfPi, start, sweepAngle))
                bounds.Bottom = cY + r;
            if (Intersections.Within(PI, start, sweepAngle))
                bounds.Left = cX - r;
            if (Intersections.Within(Pau, start, sweepAngle))
                bounds.Top = cY - r;

            return bounds;
        }

        /// <summary>
        /// Calculates the Axis Aligned Bounding Box (AABB) rectangular external boundaries of a non-rotated ellipse.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <returns>Calculates an Axis Aligned Bounding Box (AABB) Rectangle that is the size and location to envelop an ellipse.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D EllipseBounds(
            double cX, double cY,
            double r1, double r2)
            => new Rectangle2D(cX - r1, cY - r2, r1 * 2, r2 * 2);

        /// <summary>
        /// Calculates the Axis Aligned Bounding Box (AABB) external bounding rectangle of a rotated ellipse.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <returns>Returns a Axis Aligned Bounding Box (AABB) Rectangle that is the size and location to envelop a rotated ellipse.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D EllipseBounds(
            double cX, double cY,
            double r1, double r2,
            double angle)
            => EllipseBounds(cX, cY, r1, r2, Cos(angle), Sin(angle));

        /// <summary>
        /// Calculates the Axis Aligned Bounding Box (AABB) external bounding rectangle of a rotated ellipse.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosAngle">The Cosine component of the Angle of rotation of Ellipse about it's center.</param>
        /// <param name="sinAngle">The Sine component of the Angle of rotation of Ellipse about it's center.</param>
        /// <returns>Returns a Axis Aligned Bounding Box (AABB) Rectangle that is the size and location to envelop a rotated ellipse.</returns>
        /// <acknowledgment>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D EllipseBounds(
            double cX, double cY,
            double r1, double r2,
            double cosAngle, double sinAngle)
        {
            var a = r1 * cosAngle;
            var b = r2 * sinAngle;
            var c = r1 * sinAngle;
            var d = r2 * cosAngle;

            // Get the height and width.
            var width = Sqrt((a * a) + (b * b)) * 2;
            var height = Sqrt((c * c) + (d * d)) * 2;

            // Get the location point.
            var x2 = cX - width * 0.5d;
            var y2 = cY - height * 0.5d;

            // Return the bounding rectangle.
            return new Rectangle2D(x2, y2, width, height);
        }

        /// <summary>
        /// Calculates the Axis Aligned Bounding Box (AABB) close fitting rectangular bounding box of a rotated ellipse elliptical arc.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>The close bounding box of a rotated elliptical arc.</returns>
        /// <acknowledgment>
        /// Helpful hints on how this might be implemented came from:
        /// http://fridrich.blogspot.com/2011/06/bounding-box-of-svg-elliptical-arc.html,
        /// http://bazaar.launchpad.net/~inkscape.dev/inkscape/trunk/view/head:/src/2geom/elliptical-arc.cpp
        /// and http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D EllipticalArcBounds(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle, double sweepAngle)
        {
            if (r1 == 0 && r2 == 0)
                return new Rectangle2D(cX, cX, 0, 0);

            // Get the ellipse rotation transform.
            var cosT = Cos(angle);
            var sinT = Sin(angle);

            // Find the angles of the Cartesian extremes.
            var angles = new double[4] {
                Atan2((r1 - r2) * (r1 + r2) * sinT * cosT, r2 * r2 * sinT * sinT + r1 * r1 * cosT * cosT),
                Atan2(r1 * r1 * sinT * sinT + r2 * r2 * cosT * cosT, (r1 - r2) * (r1 + r2) * sinT * cosT),
                Atan2((r1 - r2) * (r1 + r2) * sinT * cosT, r2 * r2 * sinT * sinT + r1 * r1 * cosT * cosT) + PI,
                Atan2(r1 * r1 * sinT * sinT + r2 * r2 * cosT * cosT, (r1 - r2) * (r1 + r2) * sinT * cosT) + PI };
            //var vectors = new (double X, double Y)[4] {
            //    ((r1 - r2) * (r1 + r2) * sinT * cosT, r2 * r2 * sinT * sinT + r1 * r1 * cosT * cosT),
            //    (r1 * r1 * sinT * sinT + r2 * r2 * cosT * cosT, (r1 - r2) * (r1 + r2) * sinT * cosT),
            //    (-(r1 - r2) * (r1 + r2) * sinT * cosT, -r2 * r2 * sinT * sinT + r1 * r1 * cosT * cosT),
            //    (-r1 * r1 * sinT * sinT + r2 * r2 * cosT * cosT, -(r1 - r2) * (r1 + r2) * sinT * cosT) };

            // Sort the angles so that like sides are consistently at the same index.
            Array.Sort(angles);

            // Get the start and end angles adjusted to polar coordinates.
            var t0 = EllipticalPolarAngle(startAngle, r1, r2);
            var t1 = EllipticalPolarAngle(startAngle + sweepAngle, r1, r2);

            // Interpolate the ratios of height and width of the chord.
            var sinT0 = Sin(t0);
            var cosT0 = Cos(t0);
            var sinT1 = Sin(t1);
            var cosT1 = Cos(t1);

            // Get the end points of the chord.
            var bounds = new Rectangle2D(
                // Apply the rotation transformation and translate to new center.
                new Point2D(
                    cX + (r1 * cosT0 * cosT - r2 * sinT0 * sinT),
                    cY + (r1 * cosT0 * sinT + r2 * sinT0 * cosT)),
                // Apply the rotation transformation and translate to new center.
                new Point2D(
                    cX + (r1 * cosT1 * cosT - r2 * sinT1 * sinT),
                    cY + (r1 * cosT1 * sinT + r2 * sinT1 * cosT)));

            // Find the parent ellipse's horizontal and vertical radii extremes.
            var halfWidth = Sqrt((r1 * r1 * cosT * cosT) + (r2 * r2 * sinT * sinT));
            var halfHeight = Sqrt((r1 * r1 * sinT * sinT) + (r2 * r2 * cosT * cosT));

            // Expand the elliptical boundaries if any of the extreme angles fall within the sweep angle.
            if (Intersections.Within(angles[0], angle + startAngle, sweepAngle))
                bounds.Right = cX + halfWidth;
            if (Intersections.Within(angles[1], angle + startAngle, sweepAngle))
                bounds.Bottom = cY + halfHeight;
            if (Intersections.Within(angles[2], angle + startAngle, sweepAngle))
                bounds.Left = cX - halfWidth;
            if (Intersections.Within(angles[3], angle + startAngle, sweepAngle))
                bounds.Top = cY - halfHeight;

            // Return the points of the Cartesian extremes of the rotated elliptical arc.
            return bounds;
        }

        /// <summary>
        /// Calculate the Axis Aligned Bounding Box (AABB) external bounding rectangle of a rotated rectangle.
        /// </summary>
        /// <param name="height">The height of the rectangle to rotate.</param>
        /// <param name="width">The width of the rectangle to rotate.</param>
        /// <param name="fulcrum">The point at which to rotate the rectangle.</param>
        /// <param name="angle">The angle in radians to rotate the rectangle/</param>
        /// <returns>Returns an Axis Aligned Bounding Box (AABB) Rectangle with the location and height, width bounding the rotated rectangle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D RotatedRectangleBounds(
            double width, double height,
            Point2D fulcrum, double angle)
            => RotatedRectangleBounds(width, height, fulcrum.X, fulcrum.Y, angle);

        /// <summary>
        /// Calculate the Axis Aligned Bounding Box (AABB) external bounding rectangle of a rotated rectangle.
        /// </summary>
        /// <param name="height">The height of the rectangle to rotate.</param>
        /// <param name="fulcrumX"></param>
        /// <param name="fulcrumY"></param>
        /// <param name="width">The width of the rectangle to rotate.</param>
        /// <param name="angle">The angle in radians to rotate the rectangle/</param>
        /// <returns>Returns an Axis Aligned Bounding Box (AABB) Rectangle with the location and height, width bounding the rotated rectangle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D RotatedRectangleBounds(
            double width, double height,
            double fulcrumX, double fulcrumY, double angle)
        {
            var cosAngle = Abs(Cos(angle));
            var sinAngle = Abs(Sin(angle));

            var size = new Size2D(
                (cosAngle * width) + (sinAngle * height),
                (cosAngle * height) + (sinAngle * width)
                );

            var loc = new Point2D(
                fulcrumX + (-width * 0.5d * cosAngle + -height * 0.5d * sinAngle),
                fulcrumY + (-width * 0.5d * sinAngle + -height * 0.5d * cosAngle)
                );

            return new Rectangle2D(loc, size);
        }

        /// <summary>
        /// Calculates the external Axis Aligned Bounding Box (AABB) rectangle of a Polygon.
        /// </summary>
        /// <param name="polygonPoints"></param>
        /// <returns>Returns anAxis Aligned Bounding Box (AABB) containing the polygon.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D PolygonBounds(IEnumerable<Point2D> polygonPoints)
        {
            var points = polygonPoints as List<Point2D>;
            if (points?.Count < 1) return null;

            var left = points[0].X;
            var top = points[0].Y;
            var right = points[0].X;
            var bottom = points[0].Y;

            foreach (Point2D point in points)
            {
                // ToDo: Measure performance impact of overwriting each time.
                left = point.X <= left ? point.X : left;
                top = point.Y <= top ? point.Y : top;
                right = point.X >= right ? point.X : right;
                bottom = point.Y >= bottom ? point.Y : bottom;
            }

            return Rectangle2D.FromLTRB(left, top, right, bottom);
        }

        /// <summary>
        /// Calculates the external Axis Aligned Bounding Box (AABB) rectangle of a <see cref="Polyline"/>.
        /// </summary>
        /// <param name="points">The points of the polygon.</param>
        /// <returns>Returns anAxis Aligned Bounding Box (AABB) containing the polyline.</returns>
        public static Rectangle2D PolylineBounds(List<Point2D> points)
        {
            var left = points[0].X;
            var top = points[0].Y;
            var right = points[0].X;
            var bottom = points[0].Y;

            foreach (Point2D point in points)
            {
                // ToDo: Measure performance impact of overwriting each time.
                left = point.X <= left ? point.X : left;
                top = point.Y <= top ? point.Y : top;
                right = point.X >= right ? point.X : right;
                bottom = point.Y >= bottom ? point.Y : bottom;
            }

            return Rectangle2D.FromLTRB(left, top, right, bottom);
        }

        /// <summary>
        /// Calculates the Axis Aligned Bounding Box (AABB) rectangle that contains a Polynomial curve.
        /// </summary>
        /// <param name="CurveX">The x-component polynomial of the curve.</param>
        /// <param name="CurveY">The y-component polynomial of the curve.</param>
        /// <returns>Returns an Axis Aligned Bounding Box (AABB) rectangle that bounds the polynomial curve.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D BezierBounds(Polynomial CurveX, Polynomial CurveY)
        {
            (var left, var right) = CurveX.MinMax(0, 1);
            (var top, var bottom) = CurveY.MinMax(0, 1);
            return new Rectangle2D(left, top, right - left, bottom - top);
        }

        /// <summary>
        /// Calculates the Axis Aligned Bounding Box (AABB) rectangle of a polycurve contour.
        /// </summary>
        /// <param name="contour">The polycurve contour.</param>
        /// <returns>A <see cref="Rectangle2D"/> that represents the Axis Aligned Bounding Box (AABB) of the polycurve contour.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D PolycurveContourBounds(PolycurveContour contour)
        {
            var start = contour.Items[0] as PointSegment;
            var result = new Rectangle2D(start.Start.Value, start.End.Value);

            foreach (CurveSegment member in contour.Items)
            {
                result.UnionMutate(member.Bounds);
            }

            return result;
        }

        /// <summary>
        /// Calculates the Axis Aligned Bounding Box (AABB) Rectangular boundaries of the Cartesian extremes of the chain of points generated by a parametric method.
        /// This loops through every point on every call, so it should be cached when possible.
        /// </summary>
        /// <param name="func">The list iterator method.</param>
        /// <param name="count">The number of points to use.</param>
        /// <returns>The Axis Aligned Bounding Box (AABB) rectangle of the chain of points generated by a parametric method.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D ParametricBounds(
            Func<double, List<Point2D>> func,
            double count = 100d)
        {
            // Get the list of points from the parametric method.
            var points = func(count);
            if (points?.Count < 1)
                return null;

            // Fill with initial point.
            var left = points[0].X;
            var top = points[0].Y;
            var right = points[0].X;
            var bottom = points[0].Y;

            // Locate the extremes of the parametric shape.
            foreach (Point2D point in points)
            {
                left = point.X <= left ? point.X : left;
                top = point.Y <= top ? point.Y : top;
                right = point.X >= right ? point.X : right;
                bottom = point.Y >= bottom ? point.Y : bottom;
            }

            // Return the rectangle that encompasses the points at the found extremes.
            return Rectangle2D.FromLTRB(left, top, right, bottom);
        }
        #endregion BoundsMethods

        #region Area Methods
        /// <summary>
        /// Calculates the sign of a triangle (p1, p2, o)
        /// </summary>
        /// <param name="p1">The first point</param>
        /// <param name="p2">The second point.</param>
        /// <param name="o">The reference point.</param>
        /// <returns>Returns the sign of a triangle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(Point2D p1, Point2D p2, Point2D o)
        {
            var det = (p1.X - o.X) * (p2.Y - o.Y) - (p2.X - o.X) * (p1.Y - o.Y);
            return det < 0 ? -1 : (det > 0 ? +1 : 0);
        }

        /// <summary>
        /// Calculates the signed area of the Triangle including the origin ( (0,0), p1, p2).
        /// </summary>
        /// <param name="p1">The first point.</param>
        /// <param name="p2">The second point.</param>
        /// <returns>Returns the signed area of a triangle composed of two points and the origin.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedArea(Point2D p1, Point2D p2)
            => -p2.X * (p1.Y - p2.Y) - -p2.Y * (p1.X - p2.X);

        /// <summary>
        /// Calculates the signed area of a triangle.
        /// </summary>
        /// <param name="p0">The first point of the triangle.</param>
        /// <param name="p1">The second point of the triangle.</param>
        /// <param name="p2">The third point of the triangle.</param>
        /// <returns>Returns the signed area of a triangle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedTriangleArea(Point2D p0, Point2D p1, Point2D p2)
            => SignedTriangleArea(p0.X, p0.Y, p1.X, p1.Y, p2.X, p2.Y);

        /// <summary>
        /// Calculates the signed area of the triangle (p0, p1, p2).
        /// </summary>
        /// <param name="p0">The first point of the triangle.</param>
        /// <param name="p1">The second point of the triangle.</param>
        /// <param name="p2">The third point of the triangle.</param>
        /// <returns>Returns the signed area of a triangle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedTriangleArea2(Point2D p0, Point2D p1, Point2D p2)
            => (p0.X - p2.X) * (p1.Y - p2.Y) - (p1.X - p2.X) * (p0.Y - p2.Y);

        /// <summary>
        /// Calculates the signed area of a triangle.
        /// </summary>
        /// <param name="aX">The x-component of the first point of the triangle.</param>
        /// <param name="aY">The y-component of the first point of the triangle.</param>
        /// <param name="bX">The x-component of the second point of the triangle.</param>
        /// <param name="bY">The y-component of the second point of the triangle.</param>
        /// <param name="cX">The x-component of the third point of the triangle.</param>
        /// <param name="cY">The y-component of the third point of the triangle.</param>
        /// <returns>
        /// Returns a positive number if point c is to the left of the line going from a to b, or negative if point is to right, and 0 if points are collinear.
        /// </returns>
        /// <acknowledgment>
        /// From FarSeer Physics Engine. https://github.com/VelcroPhysics/VelcroPhysics
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedTriangleArea(double aX, double aY, double bX, double bY, double cX, double cY)
            => aX * (bY - cY) + bX * (cY - aY) + cX * (aY - bY);

        /// <summary>
        /// Calculates the area of a circle.
        /// </summary>
        /// <param name="r">The radius of the circle.</param>
        /// <returns>Returns the area of the circle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircleArea(double r)
            => PI * r * r;

        /// <summary>
        /// Calculates the area of a circular arc sector.
        /// </summary>
        /// <param name="r">The radius of the circle.</param>
        /// <param name="sweepAngle">The sweep angle of the arc.</param>
        /// <returns>Returns the area of the circular arc sector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircularArcSectorArea(double r, double sweepAngle)
            => Abs(r * r * 0.5d * (sweepAngle - Sin(sweepAngle)));

        /// <summary>
        /// Calculates the area of an ellipse.
        /// </summary>
        /// <param name="rX">The horizontal radius.</param>
        /// <param name="rY">The vertical radius.</param>
        /// <returns>Returns the area of the ellipse.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipseArea(double rX, double rY)
            => PI * rY * rX;

        /// <summary>
        /// Calculates the area of an elliptical arc sector.
        /// </summary>
        /// <param name="rX">The horizontal radius.</param>
        /// <param name="rY">The vertical radius.</param>
        /// <param name="startAngle">The start angle of the arc.</param>
        /// <param name="sweepAngle">The sweep angle of the arc.</param>
        /// <returns>Returns the area of the elliptical arc sector.</returns>
        /// <acknowledgment>
        /// http://math.stackexchange.com/questions/114371/deriving-the-area-of-a-sector-of-an-ellipse?rq=1
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipticalArcSectorArea(double rX, double rY, double startAngle, double sweepAngle)
            => 0.5d * rX * rY * (Atan(rX * Tan(startAngle) / rY) - Atan(rX * Tan(startAngle + sweepAngle) / rY));

        /// <summary>
        /// Calculates the area of a rectangle.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <returns>Returns the area of the rectangle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RectangleArea(double width, double height)
            => width * height;

        /// <summary>
        /// Calculates the area of a square.
        /// </summary>
        /// <param name="depth">The length of the sides of the square.</param>
        /// <returns>Returns the area of the square.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareArea(double depth)
            => depth * depth;

        /// <summary>
        /// Calculates the signed area of a polygon.
        /// </summary>
        /// <param name="contour">The points of the polygon contour.</param>
        /// <returns>Returns the signed area of a polygon contour.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PolygonArea(List<Point2D> contour)
            => Abs(SignedPolygonArea(contour));

        /// <summary>
        /// Calculates the signed area of a polygon.
        /// </summary>
        /// <param name="contour">The points of the polygon contour.</param>
        /// <returns>Returns the signed area of a polygon contour.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedPolygonArea(List<Point2D> contour)
        {
            var count = contour.Count;
            if (count < 3) return 0d;

            var area = 0d;
            for (int i = 0, j = count - 1; i < count; ++i)
            {
                area += (contour[j].X + contour[i].X) * (contour[j].Y - contour[i].Y);
                j = i;
            }

            return -area * 0.5d;
        }
        #endregion Area Methods

        #region Other
        /// <summary>
        /// Finds the Aspect ratio of the elliptical arc or rectangle.
        /// </summary>
        /// <param name="x">The Horizontal value.</param>
        /// <param name="y">The Vertical value.</param>
        /// <returns>Returns the aspect ratio of the horizontal an vertical components.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Aspect(double x, double y)
            => x == 0 ? double.PositiveInfinity : y / x;

        /// <summary>
        /// Finds the <see cref="Eccentricity"/> of the elliptical arc or rectangle.
        /// </summary>
        /// <param name="rX">The x radius.</param>
        /// <param name="rY">The y radius.</param>
        /// <remarks>Returns a value that represents the <see cref="Eccentricity"/> of an elliptical arc or rectangle.</remarks>
        /// <acknowledgment>
        /// https://en.wikipedia.org/wiki/Ellipse
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Eccentricity(double rX, double rY)
            => Sqrt(1 - (rX / rY * (rX / rY)));

        /// <summary>
        /// Finds the Focus Radius of an <see cref="Ellipse"/>.
        /// </summary>
        /// <param name="rX">The x radius.</param>
        /// <param name="rY">The y radius.</param>
        /// <remarks>Returns a value representing the focus radius of an ellipse.</remarks>
        /// <acknowledgment>
        /// https://en.wikipedia.org/wiki/Ellipse
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipseFocusRadius(double rX, double rY)
            => Sqrt((rX * rX) - (rY * rY));

        /// <summary>
        /// Finds the closest parameter of two polynomials.
        /// </summary>
        /// <param name="curveX">The horizontal polynomial of the curve.</param>
        /// <param name="curveY">The vertical polynomial of the curve.</param>
        /// <param name="point">The point.</param>
        /// <returns>Returns a t value representing the index of the closest point on the curve to a point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClosestParameter(Polynomial curveX, Polynomial curveY, Point2D point)
        {
            var dsquare = ParameterizedSquareDistance(curveX, curveY, point);
            var deriv = dsquare.Derivate().Normalize();
            var derivRoots = deriv.Roots();
            return 1 - derivRoots
                .Where(t => t > 0 && t < 1)
                .Concat(Polynomial.Identity)
                .OrderBy(dsquare.Evaluate)
                .First();
        }

        /// <summary>
        /// Finds the shortest distance between a point and a line.
        /// </summary>
        /// <param name="segment">The line segment.</param>
        /// <param name="point">The point to test.</param>
        /// <returns>The perpendicular distance to the line.</returns>
        /// <acknowledgment>
        /// Based on: https://github.com/burningmime/curves
        /// See: http://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PerpendicularDistance(LineSegment segment, Point2D point)
        {
            var area = Abs(segment.CrossProduct + segment.BX * point.Y + point.X * segment.A.Y - point.X * segment.B.Y - segment.A.X * point.Y);
            var height = area / segment.Length;
            return height;
        }

        /// <summary>
        /// Finds the distance between a point and a polynomial curve.
        /// </summary>
        /// <param name="curveX">The horizontal polynomial of the curve.</param>
        /// <param name="curveY">The vertical polynomial of the curve.</param>
        /// <param name="point">The point.</param>
        /// <returns>Returns the distance between a curve and a point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceTo(Polynomial curveX, Polynomial curveY, Point2D point)
        {
            var dsquare = ParameterizedSquareDistance(curveX, curveY, point);
            var deriv = dsquare.Derivate().Normalize();
            var derivRoots = deriv.Roots();
            return derivRoots
                .Where(t => t > 0 && t < 1)
                .Concat(Polynomial.Identity)
                .Select(x => Sqrt(dsquare.Evaluate(1 - x)))
                .OrderBy(x => x)
                .First();
        }

        /// <summary>
        /// Finds the parametrized square distance of a polynomial curve to a point.
        /// </summary>
        /// <param name="curveX">The horizontal polynomial of the curve.</param>
        /// <param name="curveY">The vertical polynomial of the curve.</param>
        /// <param name="point">The point.</param>
        /// <returns>Returns the square distance between a curve and a point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial ParameterizedSquareDistance(Polynomial curveX, Polynomial curveY, Point2D point)
        {
            var vx = curveX - point.X;
            var vy = curveY - point.Y;
            return vx * vx + vy * vy;
        }

        /// <summary>
        /// Computes the point at the t of a polynomial curve.
        /// </summary>
        /// <param name="curveX">The horizontal polynomial of the curve.</param>
        /// <param name="curveY">The vertical polynomial of the curve.</param>
        /// <param name="t">The t index.</param>
        /// <returns>Returns a point at t for the curve.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Evaluate(Polynomial curveX, Polynomial curveY, double t)
        {
            var x = curveX.Evaluate(t);
            var y = curveY.Evaluate(t);
            return (x, y);
        }

        /// <summary>
        /// Finds the extreme angles of a circle, that fall within the sweep angle of a circular arc.
        /// </summary>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        /// <returns>Returns a list of the angles of the extremes of a circular arc.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> CirclularArcExtremeAngles(double startAngle, double sweepAngle)
            => CircleExtremeAngles().Where((a) => Intersections.Within(a, startAngle, sweepAngle)).ToList();

        /// <summary>
        /// Find the extreme angles of a circle.
        /// </summary>
        /// <returns>Returns the angles of the extremes of a circle.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> CircleExtremeAngles()
            => new List<double> { 0, HalfPi, PI, Pau };

        /// <summary>
        /// Finds the angles of the extreme points of a rotated ellipse, that fall within the sweep angle of the arc.
        /// </summary>
        /// <param name="rX">The horizontal radius of the ellipse.</param>
        /// <param name="rY">The vertical radius of the ellipse.</param>
        /// <param name="angle">The angle of orientation of the ellipse.</param>
        /// <param name="startAngle">The start angle of the arc.</param>
        /// <param name="sweepAngle">The sweep angle of the arc.</param>
        /// <returns>Returns the angles of the extreme points of an elliptical arc.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> EllipticalArcExtremeAngles(double rX, double rY, double angle, double startAngle, double sweepAngle)
            => EllipseExtremeAngles(rX, rY, angle).Where((a) => Intersections.Within(a, angle + startAngle, sweepAngle)).ToList();

        /// <summary>
        /// Finds the angles of the extreme points of the rotated ellipse.
        /// </summary>
        /// <param name="rX">The horizontal radius of the ellipse.</param>
        /// <param name="rY">The vertical radius of the ellipse.</param>
        /// <param name="angle">The angle of orientation of the ellipse.</param>
        /// <returns>Returns a list of the extreme angles of a rotated ellipse.</returns>
        /// <acknowledgment>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> EllipseExtremeAngles(double rX, double rY, double angle)
        {
            // Get the ellipse rotation transform.
            var cosT = Cos(angle);
            var sinT = Sin(angle);

            // Calculate the radii of the angle of rotation.
            var a = rX * cosT;
            var b = rY * sinT;
            var c = rX * sinT;
            var d = rY * cosT;

            // Ellipse equation for an ellipse at origin.
            var u1 = rX * Cos(Atan2(d, c));
            var v1 = -(rY * Sin(Atan2(d, c)));
            var u2 = rX * Cos(Atan2(-b, a));
            var v2 = -(rY * Sin(Atan2(-b, a)));

            // Return the list of angles.
            return new List<double>
            {
                Atan2(u1 * sinT - v1 * cosT, u1 * cosT + v1 * sinT),
                Atan2(u2 * sinT - v2 * cosT, u2 * cosT + v2 * sinT),
                Atan2(u2 * sinT - v2 * cosT, u2 * cosT + v2 * sinT) + PI,
                Atan2(u1 * sinT - v1 * cosT, u1 * cosT + v1 * sinT) + PI
            };
        }

        /// <summary>
        /// Finds the angles of the extreme points of a rotated ellipse, that fall within the sweep angle of the arc.
        /// </summary>
        /// <param name="rX">The horizontal radius of the ellipse.</param>
        /// <param name="rY">The vertical radius of the ellipse.</param>
        /// <param name="angle">The angle of orientation of the ellipse.</param>
        /// <param name="startAngle">The start angle of the arc.</param>
        /// <param name="sweepAngle">The sweep angle of the arc.</param>
        /// <returns>Returns the angles of the extreme points of an elliptical arc.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> EllipticalArcVerticalExtremeAngles(double rX, double rY, double angle, double startAngle, double sweepAngle)
            => EllipseVerticalExtremeAngles(rX, rY, angle).Where((a) => Intersections.Within(a, angle + startAngle, sweepAngle)).ToList();

        /// <summary>
        /// Finds the angles of the extreme points of the rotated ellipse.
        /// </summary>
        /// <param name="rX">The horizontal radius of the ellipse.</param>
        /// <param name="rY">The vertical radius of the ellipse.</param>
        /// <param name="angle">The angle of orientation of the ellipse.</param>
        /// <returns>Returns a list of the extreme angles of a rotated ellipse.</returns>
        /// <acknowledgment>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> EllipseVerticalExtremeAngles(double rX, double rY, double angle)
        {
            // Get the ellipse rotation transform.
            var cosT = Cos(angle);
            var sinT = Sin(angle);

            // Calculate the radii of the angle of rotation.
            var a = rX * cosT;
            var b = rY * sinT;
            var c = rX * sinT;
            var d = rY * cosT;

            // Ellipse equation for an ellipse at origin.
            var u1 = rX * Cos(Atan2(d, c));
            var v1 = -(rY * Sin(Atan2(d, c)));
            var u2 = rX * Cos(Atan2(-b, a));
            var v2 = -(rY * Sin(Atan2(-b, a)));

            // Return the list of angles.
            return new List<double>
            {
                Atan2(u1 * sinT - v1 * cosT, u1 * cosT + v1 * sinT),
                //Atan2(u2 * sinT - v2 * cosT, u2 * cosT + v2 * sinT),
                //Atan2(u2 * sinT - v2 * cosT, u2 * cosT + v2 * sinT) + PI,
                Atan2(u1 * sinT - v1 * cosT, u1 * cosT + v1 * sinT) + PI
            };
        }

        /// <summary>
        /// Calculates the points of the Cartesian extremes of a circle.
        /// </summary>
        /// <param name="x">The x-coordinate of the center of the circle.</param>
        /// <param name="y">The y-coordinate of the center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns>Returns a list of the points representing the extremes of a circle.</returns>
        /// <acknowledgment>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> CircleExtremePoints(double x, double y, double radius)
            => new List<Point2D>
            {
                Interpolators.Circle(x, y, radius, 0),
                Interpolators.Circle(x, y, radius, HalfPi),
                Interpolators.Circle(x, y, radius, PI),
                Interpolators.Circle(x, y, radius, Pau)
            };

        /// <summary>
        /// Get the points of the Cartesian extremes of a rotated ellipse.
        /// </summary>
        /// <param name="x">The x-coordinate of the center of the ellipse.</param>
        /// <param name="y">The y-coordinate of the center of the ellipse.</param>
        /// <param name="rX">The horizontal radius of the ellipse.</param>
        /// <param name="rY">The vertical radius of the ellipse.</param>
        /// <param name="angle">The angle of orientation of the ellipse.</param>
        /// <returns>Returns the points of extreme for an ellipse.</returns>
        /// <acknowledgment>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> EllipseExtremePoints(double x, double y, double rX, double rY, double angle)
            => EllipseExtremePoints(x, y, rX, rY, Cos(angle), Sin(angle));

        /// <summary>
        /// Get the points of the Cartesian extremes of a rotated ellipse.
        /// </summary>
        /// <param name="x">The x-coordinate of the center of the ellipse.</param>
        /// <param name="y">The y-coordinate of the center of the ellipse.</param>
        /// <param name="rX">The horizontal radius of the ellipse.</param>
        /// <param name="rY">The vertical radius of the ellipse.</param>
        /// <param name="cosAngle">The cosine component of the angle of orientation of the ellipse.</param>
        /// <param name="sinAngle">The sine component of the angle of orientation of the ellipse.</param>
        /// <returns>Returns the points of extreme for an ellipse.</returns>
        /// <acknowledgment>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> EllipseExtremePoints(double x, double y, double rX, double rY, double cosAngle, double sinAngle)
        {
            // Calculate the radii of the angle of rotation.
            var a = rX * cosAngle;
            var b = rY * sinAngle;
            var c = rX * sinAngle;
            var d = rY * cosAngle;

            //// Find the angles of the Cartesian extremes.
            //var a1 = Atan2(-b, a);
            //var a2 = Atan2(-b, a) + PI;
            //var a3 = Atan2(d, c);
            //var a4 = Atan2(d, c) + PI;

            //// Return the points of Cartesian extreme of the rotated ellipse.
            //return new List<Point2D>
            //{
            //    Interpolators.Ellipse(x, y, rX, rY, cosAngle, sinAngle, a1),
            //    Interpolators.Ellipse(x, y, rX, rY, cosAngle, sinAngle, a2),
            //    Interpolators.Ellipse(x, y, rX, rY, cosAngle, sinAngle, a3),
            //    Interpolators.Ellipse(x, y, rX, rY, cosAngle, sinAngle, a4)
            //};

            // ToDo: Replace the previous two sections with this return and profile to see if there is a performance improvement, and check for accuracy.
            var hypotonuseAB = Sqrt(a * a + b * b);
            var hypotonuseCD = Sqrt(c * c + d * d);
            return new List<Point2D>
            {
                new Point2D(x +                   hypotonuseAB,
                            y + (a * a - b * b) / hypotonuseAB),
                new Point2D(x -                   hypotonuseAB,
                            y - (a * a - b * b) / hypotonuseAB),
                new Point2D(x + (d * a - b * c) / hypotonuseCD,
                            y + rX * rY       / hypotonuseCD),
                new Point2D(x - (d * a - b * c) / hypotonuseCD,
                            y - rX * rY       / hypotonuseCD),
            };
        }
        #endregion Other
    }
}
