// <copyright file="Measurements.cs" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;
using Engine._Preview;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Measurements
    {
        #region Distance Extension Method Overloads

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point2D point1, Point2D point2)
            => Distance(point1.X, point1.Y, point2.X, point2.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Vector2D point1, Vector2D point2)
            => Distance(point1.I, point1.J, point2.I, point2.J);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point3D point1, Point3D point2)
            => Distance(point1.X, point1.Y, point1.Z, point2.X, point2.Y, point2.Z);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Vector3D point1, Vector3D point2)
            => Distance(point1.I, point1.J, point1.K, point2.I, point2.J, point2.K);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double? ConstrainedDistance(this LineSegment segment, Point2D point)
            => ConstrainedDistanceLineSegmentPoint(segment.AX, segment.AY, segment.BX, segment.BY, point.X, point.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this LineSegment segment, Point2D point)
            => DistanceLineSegmentPoint(segment.AX, segment.AY, segment.BX, segment.BY, point.X, point.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="segment"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point2D point, LineSegment segment)
            => DistanceLineSegmentPoint(segment.AX, segment.AY, segment.BX, segment.BY, point.X, point.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Line line, Point2D point)
            => DistanceLinePoint(line.Location.X, line.Location.Y, line.Direction.I, line.Direction.J, point.X, point.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this Point2D point, Line line)
            => DistanceLinePoint(line.Location.X, line.Location.Y, line.Direction.I, line.Direction.J, point.X, point.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this BezierSegmentX bezier, Point2D point)
            => DistanceTo(bezier.CurveX, bezier.CurveY, point);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this QuadraticBezier bezier, Point2D point)
            => DistanceTo(bezier.CurveX, bezier.CurveY, point);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this CubicBezier bezier, Point2D point)
            => DistanceTo(bezier.CurveX, bezier.CurveY, point);

        #endregion

        #region SquareDistance Extension Method Overloads

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistance(this Point2D point1, Point2D point2)
            => SquareDistance(point1.X, point1.Y, point2.X, point2.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistance(this Vector2D point1, Vector2D point2)
            => SquareDistance(point1.I, point1.J, point2.I, point2.J);

        #endregion

        #region Nearest Extension Method Overloads

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static (double X, double Y) NearestPoint(this Point2D p, Point2D point)
            => (p.X, p.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seg"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static (double X, double Y) NearestPoint(this LineSegment seg, Point2D point)
            => NearestPointOnLineSegment(seg.AX, seg.AY, seg.BX, seg.BY, point.X, point.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="seg"></param>
        /// <returns></returns>
        public static (double X, double Y) NearestPoint(this Point2D point, LineSegment seg)
            => NearestPointOnLineSegment(seg.AX, seg.AY, seg.BX, seg.BY, point.X, point.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static (double X, double Y) NearestPoint(this EllipticalArc e, Point2D point)
            => NearestPointOnEllipticalArc(e.X, e.Y, e.RX, e.RY, e.StartAngle, e.SweepAngle, point.X, point.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static double NearestT(this BezierSegmentX bezier, Point2D point)
            => ClosestParameter(bezier.CurveX, bezier.CurveY, point);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static double NearestT(this QuadraticBezier bezier, Point2D point)
            => ClosestParameter(bezier.CurveX, bezier.CurveY, point);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static double NearestT(this CubicBezier bezier, Point2D point)
            => ClosestParameter(bezier.CurveX, bezier.CurveY, point);

        #endregion

        #region Length Extension Method Overloads

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this ScreenPoint point)
            => 0;

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
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this CircularArc arc)
            => ArcLength(arc.Radius, arc.SweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="circle"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this Circle circle)
            => CircleCircumference(circle.Radius);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this EllipticalArc ellipse)
            => EllipticalArcPerimeter(ellipse.StartPoint.X, ellipse.StartPoint.Y, ellipse.EndPoint.X, ellipse.EndPoint.Y, ellipse.StartAngle, ellipse.EndAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this Ellipse ellipse)
            => EllipsePerimeter(ellipse.RX, ellipse.RY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this QuadraticBezier bezier)
            => QuadraticBezierArcLengthByIntegral(bezier.AX, bezier.AY, bezier.BX, bezier.BY, bezier.CX, bezier.CY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this CubicBezier bezier)
            => CubicBezierArcLength(bezier.AX, bezier.AY, bezier.BX, bezier.BY, bezier.CX, bezier.CY, bezier.DX, bezier.DY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this QuaternionD quaternion)
            => Sqrt(LengthSquared(quaternion));

        #endregion

        #region Bounds Extension Method Overloads

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this LineSegment segment)
            => LineSegmentBounds(segment.A, segment.B);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="circle"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this Circle circle)
            => CircleBounds(circle.X, circle.Y, circle.Radius);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this CircularArc arc)
            => CircularArcBounds(arc.X, arc.Y, arc.Radius, 0, arc.StartAngle, arc.SweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this Ellipse ellipse)
            => EllipseBounds(ellipse.X, ellipse.Y, ellipse.RX, ellipse.RY, ellipse.Angle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this EllipticalArc arc)
            => EllipticalArcBounds(arc.X, arc.Y, arc.RX, arc.RY, arc.Angle, arc.StartAngle, arc.SweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="poly"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this Contour poly)
            => PolygonBounds(poly.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="poly"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this Polyline poly)
            => PolygonBounds(poly.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this QuadraticBezier bezier)
            => QuadraticBezierBounds(bezier.AX, bezier.AY, bezier.BX, bezier.BY, bezier.CX, bezier.CY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this CubicBezier bezier)
            => CubicBezierBounds(bezier.AX, bezier.AY, bezier.BX, bezier.BY, bezier.CX, bezier.CY, bezier.DX, bezier.DY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this PolycurveContour path)
            => PolycurveContourBounds(path);

        #endregion

        #region Area Extension Method Overloads

        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangle"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this Triangle triangle)
            => Abs(SignedTriangleArea(triangle.A.X, triangle.A.Y, triangle.B.X, triangle.B.Y, triangle.C.X, triangle.C.Y));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this Rectangle2D rectangle)
            => (rectangle.Height == rectangle.Width) ? SquareArea(rectangle.Width) : RectangleArea(rectangle.Width, rectangle.Height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="poly"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this Polyline poly)
            => Abs(SignedPolygonArea(poly.Points));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="poly"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this Contour poly)
            => Abs(SignedPolygonArea(poly.Points));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this CircularArc arc)
            => CircularArcSectorArea(arc.Radius, arc.SweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="circle"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this Circle circle)
            => CircleArea(circle.Radius);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this EllipticalArc arc)
            => EllipticalArcSectorArea(arc.RX, arc.RY, arc.StartAngle, arc.SweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Area(this Ellipse ellipse)
            => EllipseArea(ellipse.RX, ellipse.RY);

        #endregion

        #region Distance Methods

        /// <summary>
        /// Distance between two tuple points.
        /// </summary>
        /// <param name="p1">First point.</param>
        /// <param name="p2">Second point.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this (double X, double Y) p1, (double X, double Y) p2)
            => Measurements.Distance(p1.X, p1.Y, p2.X, p2.Y);

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(
            double x1, double y1,
            double x2, double y2)
            => Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="z1">First Z component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <param name="z2">Second Z component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1));

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="z1">First Z component.</param>
        /// <param name="w1">First W component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <param name="z2">Second Z component.</param>
        /// <param name="w2">Second W component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(
            double x1, double y1, double z1, double w1,
            double x2, double y2, double z2, double w2)
            => Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1) + (w2 - w1) * (w2 - w1));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double? ConstrainedDistanceLineSegmentPoint(
            double aX, double aY,
            double bX, double bY,
            double pX, double pY)
        {
            var ul = bX - aX;
            var vl = bY - aY;

            var up = aX - pX;
            var vp = aY - pY;

            // Get the length of the line segment.
            var length = Sqrt(ul * ul + vl * vl);

            // Check whether the line is a line or a point.
            if (length == 0) return null;

            // Find the interpolation value.
            var t = -(up * ul + vp * vl) / (length * length);

            // Check whether the closest point falls on the line segment.
            if (t < 0d || t > 1d) return null;

            // Return the length to the nearest point on the line segment.
            return (length == 0)
                ? Sqrt(up * up + vp * vp)
                : Abs(ul * vp - up * vl) / length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        /// <remarks>
        /// From answer at: http://stackoverflow.com/a/2255848
        /// formula here: http://mathworld.wolfram.com/Point-LineDistance2-Dimensional.html
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceLinePoint(
            double aX, double aY,
            double bX, double bY,
            double pX, double pY)
        {
            var ul = bX - aX;
            var vl = bY - aY;

            var up = aX - pX;
            var vp = aY - pY;

            // Get the length of the line segment.
            var length = Sqrt(ul * ul + vl * vl);

            // Return the length to the nearest point on the line.
            return (length == 0)
                ? Sqrt(up * up + vp * vp)
                : Abs(ul * vp - up * vl) / length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceLineSegmentPoint(
            double aX, double aY,
            double bX, double bY,
            double pX, double pY)
        {
            var ul = bX - aX;
            var vl = bY - aY;

            var up0 = aX - pX;
            var vp0 = aY - pY;

            var up1 = aX - pX;
            var vp1 = aY - pY;

            // Get the length of the line segment.
            var length = Sqrt(ul * ul + vl * vl);

            // Find the interpolation value.
            var t = -(up0 * ul + vp0 * vl) / (length * length);

            // Check whether the closest point falls on the line segment.
            if (t < 0d)
                return Sqrt(up0 * up0 + vp0 * vp0);
            else if (t > 1d)
                return Sqrt(up1 * up1 + vp1 * vp1);

            // Return the length to the nearest point on the line segment.
            return (length == 0)
                ? Sqrt(up0 * up0 + vp0 * vp0)
                : Abs(ul * vp0 - up0 * vl) / length;
        }

        #endregion

        #region SquareDistance Methods

        /// <summary>
        /// The square of the distance between two points.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistance(
            double x1, double y1,
            double x2, double y2)
            => (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);

        /// <summary>
        /// The square of the distance between two points.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistance(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1);

        /// <summary>
        /// The square of the distance between two points.
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
        public static double SquareDistance(
            double x1, double y1, double z1, double w1,
            double x2, double y2, double z2, double w2)
            => (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1) + (w2 - z1) * (w2 - w1);

        /// <summary>
        /// Find the square of the distance of a point from a line.
        /// </summary>
        /// <param name="lx2">The x component of the first point on the line.</param>
        /// <param name="ly2">The y component of the first point on the line.</param>
        /// <param name="lx3">The x component of the second point on the line.</param>
        /// <param name="ly3">The y component of the second point on the line.</param>
        /// <param name="x1">The x component of the Point.</param>
        /// <param name="y1">The y component of the Point.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistanceToLine(
            double lx2, double ly2,
            double lx3, double ly3,
            double x1, double y1)
        {
            var A = ly2 - ly3;
            var B = lx3 - lx2;
            var C = (A * x1 + B * y1) - (A * lx2 + B * ly2);
            return (C * C) / (A * A + B * B);
        }

        #endregion

        #region Nearest Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) NearestPointOnLineSegment2(
            double aX, double aY,
            double bX, double bY,
            double pX, double pY)
        {
            var ul = bX - aX;
            var vl = bY - aY;

            var up = aX - pX;
            var vp = aY - pY;

            // Get the length of the line segment.
            var length = Sqrt(ul * ul + vl * vl);

            // Find the interpolation value.
            var t = -(up * ul + vp * vl) / (length * length);

            // Return the nearest point on the line segment.
            return (t < 0d)
                ? (aX, aY)
                : (t > 1d)
                ? (bX, bY)
                : (aX + t * ul, aY + t * vl);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double X, double Y) NearestPointOnLineSegment(
            double aX, double aY,
            double bX, double bY,
            double pX, double pY)
        {
            // Vector A->P
            var diffAP = new Point2D(pX - aX, pY - aY);

            // Vector A->B
            var diffAB = new Point2D(bX - aX, bY - aY);
            var dotAB = diffAB.X * diffAB.X + diffAB.Y * diffAB.Y;

            // The dot product of diffAP and diffAB
            var dotABAP = diffAP.X * diffAB.X + diffAP.Y * diffAB.Y;

            //  # The normalized "distance" from a to the closest point
            var dist = dotABAP / dotAB;

            if (dist < 0)
                return (aX, aY);
            else if (dist > dotABAP)
                return (bX, bY);
            else
                return (aX + diffAB.X * dist, aY + diffAB.Y * dist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        /// <param name="a0"></param>
        /// <param name="a1"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/36260793/algorithm-for-shortest-distance-from-a-point-to-an-elliptic-arc?rq=1
        /// </remarks>
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
                // sample arc a=<b0,b1> with step da
                for (e = 1, a = b0; e != 0; a += da)
                {
                    if (a >= b1) { a = b1; e = 0; }

                    // elliptic arc sampled point
                    x = cX + rX * Cos(a);
                    y = cY - rY * Sin(a);               // mine y axis is in reverse order therefore -
                                                        // distance^2 to x_in,y_in
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
        /// 
        /// </summary>
        /// <param name="p0X"></param>
        /// <param name="p0Y"></param>
        /// <param name="p1X"></param>
        /// <param name="p1Y"></param>
        /// <param name="p2X"></param>
        /// <param name="p2Y"></param>
        /// <param name="p3X"></param>
        /// <param name="p3Y"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double X, double Y)? NearestPointOnCubicBezier(
            double p0X, double p0Y,
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double pX, double pY)
            => null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0Y"></param>
        /// <param name="p0X"></param>
        /// <param name="p1X"></param>
        /// <param name="p1Y"></param>
        /// <param name="p2X"></param>
        /// <param name="p2Y"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double X, double Y)? NearestPointOnQuadraticBezier(
            double p0Y, double p0X,
            double p1X, double p1Y,
            double p2X, double p2Y,
            double pX, double pY)
            => null;

        #endregion

        #region Length Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="sweepAngle"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ArcLength(double r, double sweepAngle)
            => 2d * PI * r * -sweepAngle;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircleCircumference(double r)
            => 2d * r * PI;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipseCircumference05.html
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipsePerimeter(double a, double b)
            => 4d * ((PI * a * b) + ((a - b) * (a - b))) / (a + b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle">Sweep Angle.</param>
        /// <returns></returns>
        /// <remarks>
        /// http://www.iosrjournals.org/iosr-jm/papers/Vol3-issue2/B0320813.pdf
        /// http://mathforum.org/kb/servlet/JiveServlet/download/130-2391290-7852023-766514/PERIMETER%20OF%20THE%20ELLIPTICAL%20ARC%20A%20GEOMETRIC%20METHOD.pdf
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipticalArcPerimeter(double startX, double startY, double endX, double endY, double startAngle, double endAngle)
            => (/*ChordLength*/(Sqrt(Abs(endX - startX) * Abs(endX - startX) + Abs(endY - startY) * Abs(endY - startY)))
            / /*Middle Angle*/(2 * Sin(OneHalf * (startAngle - endAngle))))
            * (startAngle - endAngle);

        /// <summary>
        /// Closed-form solution to elliptic integral for arc length.
        /// </summary>
        /// <param name="ax">The starting x-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="ay">The starting y-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="bx">The middle x-coordinate for the tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="by">The middle y-coordinate for the tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="cx">The closing x-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="cy">The closing y-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns></returns>
        /// <remarks>
        /// https://algorithmist.wordpress.com/2009/01/05/quadratic-bezier-arc-length/
        /// </remarks>
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
        /// 
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        /// <remarks>http://steve.hollasch.net/cgindex/curves/cbezarclen.html</remarks>
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

            var q1 = 9d * (Sqrt(Abs(k1.X)) + Sqrt((Abs(k1.Y))));
            var q2 = 12d * (k1.X * k2.X + k1.Y * k2.Y);
            var q3 = 3d * (k1.X * k3.X + k1.Y * k3.Y) + 4d * (Sqrt(Abs(k2.X)) + Sqrt(Abs(k2.Y)));
            var q4 = 4d * (k2.X * k3.X + k2.Y * k3.Y);
            var q5 = Sqrt(Abs(k3.X)) + Sqrt(Abs(k3.Y));

            // Approximation algorithm based on Simpson. 
            var a = 0d;
            var b = 1d;
            var n_limit = 1024;
            var TOLERANCE = 0.001d;

            var n = 1;

            var multiplier = (b - a) / 6d;
            var endsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a) + CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, b);
            var interval = (b - a) * 0.5d;
            var asum = 0d;
            var bsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a + interval);
            var est1 = multiplier * (endsum + 2d * asum + 4d * bsum);
            var est0 = 2d * est1;

            while (n < n_limit && (Abs(est1) > 0d && Abs((est1 - est0) / est1) > TOLERANCE))
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
        /// Bezier Arc Length Function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="q1"></param>
        /// <param name="q2"></param>
        /// <param name="q3"></param>
        /// <param name="q4"></param>
        /// <param name="q5"></param>
        /// <returns></returns>
        /// <remarks>http://steve.hollasch.net/cgindex/curves/cbezarclen.html</remarks>
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
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PolygonPerimeter(this Point2D[] points)
            => points.Length > 0d ? points.Zip(points.Skip(1), Distance).Sum() + points[0].Distance(points[points.Length - 1]) : 0d;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PolygonPerimeter(this IEnumerable<Point2D> polygon)
            => PolygonPerimeter(polygon as List<Point2D>);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PolygonPerimeter(this List<Point2D> points)
            => points.Count > 0d ? points.Zip(points.Skip(1), Distance).Sum() + points[0].Distance(points[points.Count - 1]) : 0d;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LengthSquared(this QuaternionD quaternion)
            => ((((quaternion.X * quaternion.X)
            + (quaternion.Y * quaternion.Y))
            + (quaternion.Z * quaternion.Z))
            + (quaternion.W * quaternion.W));

        #endregion

        #region BoundsMethods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D LineSegmentBounds(Point2D a, Point2D b)
            => new Rectangle2D(a, b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D LineSegmentBounds2(
            double x0, double y0,
            double x1, double y1)
            => new Rectangle2D(new Point2D(x0, y0), new Point2D(x1, y1));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <returns></returns>
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
        /// Calculate the external square boundaries of a circle.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of the Circle.</param>
        /// <returns>A Rectangle that is the size and location to envelop the circle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D CircleBounds(
            double cX, double cY,
            double r)
            => Rectangle2D.FromLTRB((cX - r), (cY - r), (cX + r), (cY + r));

        /// <summary>
        /// Calculate the axial aligned bounding box of a circular arc.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of the Circle.</param>
        /// <param name="angle"></param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>The close bounding box of a circular arc.</returns>
        /// <returns>A Rectangle large enough to closely fit the circular arc inside.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D CircularArcBounds(
            double cX, double cY,
            double r,
            double angle,
            double startAngle, double sweepAngle)
        {
            var start = Maths.WrapAngle(angle + startAngle);
            var end = Maths.WrapAngle(start + sweepAngle);

            var bounds = new Rectangle2D(
                new Point2D(cX + r * Cos(startAngle), cY + r * Sin(startAngle)),
                new Point2D(cX + r * Cos(end), cY + r * Sin(end)));

            // Expand the boundaries if any of the extreme angles fall within the sweep angle.
            if (Intersections.Within(0, start, sweepAngle))
                bounds.Right = cX + r;
            if (Intersections.Within(Right, start, sweepAngle))
                bounds.Bottom = cY + r;
            if (Intersections.Within(PI, start, sweepAngle))
                bounds.Left = cX - r;
            if (Intersections.Within(Pau, start, sweepAngle))
                bounds.Top = cY - r;

            return bounds;
        }

        /// <summary>
        /// Calculate the rectangular external boundaries of a non-rotated ellipse.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <returns>A Rectangle that is the size and location to envelop an ellipse.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D EllipseBounds(
            double cX, double cY,
            double r1, double r2)
            => new Rectangle2D(cX - r1, cY - r2, r1 * 2, r2 * 2);

        /// <summary>
        /// Calculate the external boundaries of a rotated ellipse.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <returns>A Rectangle that is the size and location to envelop a rotated ellipse.</returns>
        /// <remarks>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D EllipseBounds(
            double cX, double cY,
            double r1, double r2,
            double angle)
        {
            var a = r1 * Cos(angle);
            var b = r2 * Sin(angle);
            var c = r1 * Sin(angle);
            var d = r2 * Cos(angle);

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
        /// Find the close fitting rectangular bounding box of a rotated ellipse elliptical arc.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>The close bounding box of a rotated elliptical arc.</returns>
        /// <remarks>
        /// Helpful hints on how this might be implemented came from:
        /// http://fridrich.blogspot.com/2011/06/bounding-box-of-svg-elliptical-arc.html,
        /// http://bazaar.launchpad.net/~inkscape.dev/inkscape/trunk/view/head:/src/2geom/elliptical-arc.cpp
        /// and http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D EllipticalArcBounds(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle, double sweepAngle)
        {
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
            var t0 = EllipsePolarAngle(startAngle, r1, r2);
            var t1 = EllipsePolarAngle(startAngle + sweepAngle, r1, r2);

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
        /// Calculate the external bounding rectangle of a rotated rectangle.
        /// </summary>
        /// <param name="height">The height of the rectangle to rotate.</param>
        /// <param name="width">The width of the rectangle to rotate.</param>
        /// <param name="fulcrum">The point at which to rotate the rectangle.</param>
        /// <param name="angle">The angle in radians to rotate the rectangle/</param>
        /// <returns>A Rectangle with the location and height, width bounding the rotated rectangle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D RotatedRectangleBounds(
            double width, double height,
            Point2D fulcrum, double angle)
        {
            var cosAngle = Abs(Cos(angle));
            var sinAngle = Abs(Sin(angle));

            var size = new Size2D(
                (cosAngle * width) + (sinAngle * height),
                (cosAngle * height) + (sinAngle * width)
                );

            var loc = new Point2D(
                fulcrum.X + ((-width * 0.5d) * cosAngle + (-height * 0.5d) * sinAngle),
                fulcrum.Y + ((-width * 0.5d) * sinAngle + (-height * 0.5d) * cosAngle)
                );

            return new Rectangle2D(loc, size);
        }

        /// <summary>
        /// Calculate the external bounding rectangle of a Polygon.
        /// </summary>
        /// <param name="polygon">The points of the polygon.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D PolygonBounds(IEnumerable<Point2D> polygon)
        {
            var points = (polygon as List<Point2D>);
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
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="CurveX"></param>
        /// <param name="CurveY"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D BezierBounds(Polynomial CurveX, Polynomial CurveY)
        {
            (double x0, double x1) = CurveX.GetMinMax(0, 1);
            (double y0, double y1) = CurveY.GetMinMax(0, 1);
            return new Rectangle2D(x0, y0, x1 - x0, y1 - y0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        /// <remarks>
        /// This method has an error where if the end nodes are horizontal to each other, while the handles are also horizontal to each other the bounds are not correctly calculated.
        /// Method created using the following resources.
        /// http://stackoverflow.com/questions/24809978/calculating-the-bounding-box-of-cubic-bezier-curve
        /// http://nishiohirokazu.blogspot.com/2009/06/how-to-calculate-bezier-curves-bounding.html
        /// http://jsfiddle.net/SalixAlba/QQnvm/4/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D CubicBezierBounds(
            double ax, double ay,
            double bx, double by,
            double cx, double cy,
            double dx, double dy)
        {
            // Calculate the polynomial of the cubic.
            var a = -3 * ax + 9 * bx - 9 * cx + 3 * dx;
            var b = 6 * ax - 12 * bx + 6 * cx;
            var c = -3 * ax + 3 * bx;

            // Calculate the discriminant of the polynomial.
            var discriminant = b * b - 4 * a * c;

            // Find the high and low x ends.
            var xlow = (dx < ax) ? dx : ax;
            var xhigh = (dx > ax) ? dx : ax;

            if (discriminant >= 0)
            {
                // Find the positive solution using the quadratic formula.
                var t1 = (-b + Sqrt(discriminant)) / (2 * a);

                if (t1 > 0 && t1 < 1)
                {
                    var x1 = Interpolators.Cubic(ax, bx, cx, dx, t1);
                    if (x1 < xlow) xlow = x1;
                    if (x1 > xhigh) xhigh = x1;
                }

                // Find the negative solution using the quadratic formula.
                var t2 = (-b - Sqrt(discriminant)) / (2 * a);

                if (t2 > 0 && t2 < 1)
                {
                    var x2 = Interpolators.Cubic(ax, bx, cx, dx, t2);
                    if (x2 < xlow) xlow = x2;
                    if (x2 > xhigh) xhigh = x2;
                }
            }

            a = -3 * ay + 9 * by - 9 * cy + 3 * dy;
            b = 6 * ay - 12 * by + 6 * cy;
            c = -3 * ay + 3 * by;

            discriminant = b * b - 4 * a * c;

            var yl = ay;
            var yh = ay;
            if (dy < yl) yl = dy;
            if (dy > yh) yh = dy;
            if (discriminant >= 0)
            {
                var t1 = (-b + Sqrt(discriminant)) / (2 * a);

                if (t1 > 0 && t1 < 1)
                {
                    var y1 = Interpolators.Cubic(ay, by, cy, dy, t1);
                    if (y1 < yl) yl = y1;
                    if (y1 > yh) yh = y1;
                }

                var t2 = (-b - Sqrt(discriminant)) / (2 * a);

                if (t2 > 0 && t2 < 1)
                {
                    var y2 = Interpolators.Cubic(ay, by, cy, dy, t2);
                    if (y2 < yl) yl = y2;
                    if (y2 > yh) yh = y2;
                }
            }

            return new Rectangle2D(xlow, xhigh, yl, yh);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/24809978/calculating-the-bounding-box-of-cubic-bezier-curve
        /// http://jsfiddle.net/SalixAlba/QQnvm/4/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D QuadraticBezierBounds(
            double ax, double ay,
            double bx, double by,
            double cx, double cy)
        {
            var cubic = Conversions.QuadraticBezierToCubicBezier(ax, ay, bx, by, cx, cy);
            return CubicBezierBounds(cubic[0].X, cubic[0].Y, cubic[1].X, cubic[1].Y, cubic[2].X, cubic[2].Y, cubic[3].X, cubic[3].Y);
        }

        /// <summary>
        /// Calculate the external bounding rectangle of a Chain.
        /// </summary>
        /// <param name="chain">The Chain.</param>
        /// <returns>A <see cref="Rectangle2D"/> that represents the external bounds of the chain.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D PolycurveContourBounds(PolycurveContour chain)
        {
            var start = chain.Items[0] as PointSegment;
            var result = new Rectangle2D(start.Start.Value, start.End.Value);

            foreach (CurveSegment member in chain.Items)
            {
                result.UnionMutate(member.Bounds);
            }

            return result;
        }

        /// <summary>
        /// Rectangular boundaries of the Cartesian extremes of the chain of points generated by a parametric method.
        /// This loops through every point on every call, so it should be cached when possible.
        /// </summary>
        /// <param name="func">The list iterator method.</param>
        /// <param name="count">The number of points to use.</param>
        /// <returns>The external bounding rectangle of the chain of points generated by a parametric method.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D ParametricBounds(
            Func<double, List<Point2D>> func,
            double count = 100d)
        {
            // Get the list of points from the parametric method.
            List<Point2D> points = func(count);
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

        #endregion

        #region Area Methods

        /// <summary>
        /// Sign of triangle (p1, p2, o)
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(Point2D p1, Point2D p2, Point2D o)
        {
            var det = (p1.X - o.X) * (p2.Y - o.Y) - (p2.X - o.X) * (p1.Y - o.Y);
            return (det < 0 ? -1 : (det > 0 ? +1 : 0));
        }

        /// <summary>
        /// Signed area of the Triangle ( (0,0), p1, p2)
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedArea(Point2D p1, Point2D p2)
            => -p2.X * (p1.Y - p2.Y) - -p2.Y * (p1.X - p2.X);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedTriangleArea(Point2D p0, Point2D p1, Point2D p2)
            => SignedTriangleArea(p0.X, p0.Y, p1.X, p1.Y, p2.X, p2.Y);

        /// <summary>
        /// Signed area of the triangle (p0, p1, p2)
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedTriangleArea2(Point2D p0, Point2D p1, Point2D p2)
            => (p0.X - p2.X) * (p1.Y - p2.Y) - (p1.X - p2.X) * (p0.Y - p2.Y);

        /// <summary>
        /// Returns a positive number if c is to the left of the line going from a to b.
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <returns>
        /// Positive number if point is left, negative if point is right, 
        /// and 0 if points are collinear.
        /// </returns>
        /// <remarks>From FarSeer Physics Engine.</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedTriangleArea(double aX, double aY, double bX, double bY, double cX, double cY)
            => aX * (bY - cY) + bX * (cY - aY) + cX * (aY - bY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircleArea(double r)
            => PI * r * r;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="sweepAngle"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircularArcSectorArea(double r, double sweepAngle)
            => Abs((r * r * 0.5d) * (sweepAngle - Sin(sweepAngle)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipseArea(double r1, double r2)
            => PI * r2 * r1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://math.stackexchange.com/questions/114371/deriving-the-area-of-a-sector-of-an-ellipse?rq=1
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipticalArcSectorArea(double rX, double rY, double startAngle, double sweepAngle)
            => 0.5d * rX * rY * (Atan(rX * Tan(startAngle) / rY) - Atan(rX * Tan(startAngle + sweepAngle) / rY));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RectangleArea(double width, double height)
            => width * height;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="depth"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareArea(double depth)
            => depth * depth;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedPolygonArea(List<Point2D> polygon)
        {
            var count = polygon.Count;
            if (count < 3) return 0d;

            var area = 0d;
            for (int i = 0, j = count - 1; i < count; ++i)
            {
                area += (polygon[j].X + polygon[i].X) * (polygon[j].Y - polygon[i].Y);
                j = i;
            }

            return -area * 0.5d;
        }

        #endregion

        #region Other

        /// <summary>
        /// Finds the Aspect ratio of the elliptical arc or rectangle.
        /// </summary>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Aspect(double rX, double rY)
            => rY / rX;

        /// <summary>
        /// Finds the <see cref="Eccentricity"/> of the elliptical arc or rectangle.
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Ellipse</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Eccentricity(double rX, double rY)
            => Sqrt(1 - ((rX / rY) * (rX / rY)));

        /// <summary>
        /// Gets the Focus Radius of an <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Ellipse</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipseFocusRadius(double rX, double rY)
            => Sqrt((rX * rX) - (rY * rY));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curveX"></param>
        /// <param name="curveY"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClosestParameter(Polynomial curveX, Polynomial curveY, Point2D point)
        {
            var dsquare = ParameterizedSquareDistance(curveX, curveY, point);
            var deriv = dsquare.Derivate().Normalize();
            var derivRoots = deriv.RealOrComplexRoots();
            return derivRoots
                .Where(t => t > 0 && t < 1)
                .Concat(Polynomial.Identity)
                .OrderBy(x => dsquare.Compute(x))
                .First();
        }

        /// <summary>
        /// Finds the shortest distance between a point and a line. See: http://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line
        /// </summary>
        /// <param name="segment">The line segment.</param>
        /// <param name="p">The point to test.</param>
        /// <returns>The perpendicular distance to the line.</returns>
        /// <remarks> Based on: https://github.com/burningmime/curves </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PerpendicularDistance(LineSegment segment, Point2D p)
        {
            var area = Abs(segment.CrossProduct + segment.BX * p.Y + p.X * segment.A.Y - p.X * segment.B.Y - segment.A.X * p.Y);
            var height = area / segment.Length;
            return height;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curveX"></param>
        /// <param name="curveY"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceTo(Polynomial curveX, Polynomial curveY, Point2D point)
        {
            var dsquare = ParameterizedSquareDistance(curveX, curveY, point);
            var deriv = dsquare.Derivate().Normalize();
            var derivRoots = deriv.RealOrComplexRoots();
            return derivRoots
                .Where(t => t > 0 && t < 1)
                .Concat(Polynomial.Identity)
                .Select(x => Sqrt(dsquare.Compute(x)))
                .OrderBy(x => x)
                .First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curveX"></param>
        /// <param name="curveY"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial ParameterizedSquareDistance(Polynomial curveX, Polynomial curveY, Point2D p)
        {
            var vx = curveX - p.X;
            var vy = curveY - p.Y;
            return vx * vx + vy * vy;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curveX"></param>
        /// <param name="curveY"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Compute(Polynomial curveX, Polynomial curveY, double t)
        {
            var x = curveX.Compute(t);
            var y = curveY.Compute(t);
            return (x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> CircleExtremeAngles()
            => new List<double> { 0, Right, PI, Pau };

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> CirclularArcExtremeAngles(double startAngle, double sweepAngle)
            => CircleExtremeAngles().Where((a) => Intersections.Within(a, startAngle, sweepAngle)).ToList();

        /// <summary>
        /// Gets the angles of the extreme points of the rotated ellipse.
        /// </summary>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        /// <remarks>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
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
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> EllipticalArcExtremeAngles(double rX, double rY, double angle, double startAngle, double sweepAngle)
            => EllipseExtremeAngles(rX, rY, angle).Where((a) => Intersections.Within(a, angle + startAngle, sweepAngle)).ToList();

        /// <summary>
        /// Get the points of the Cartesian extremes of a circle.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        /// <remarks>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> CircleExtremePoints(double x, double y, double radius)
            => new List<Point2D>
            {
                Interpolators.Circle(x, y, radius, 0),
                Interpolators.Circle(x, y, radius, Right),
                Interpolators.Circle(x, y, radius, PI),
                Interpolators.Circle(x, y, radius, Pau)
            };

        /// <summary>
        /// Get the points of the Cartesian extremes of a rotated ellipse.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        /// <remarks>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> EllipseExtremePoints(double x, double y, double rX, double rY, double angle)
        {
            // Get the ellipse rotation transform.
            var cosT = Cos(angle);
            var sinT = Sin(angle);

            // Calculate the radii of the angle of rotation.
            var a = rX * cosT;
            var c = rX * sinT;
            var d = rY * cosT;
            var b = rY * sinT;

            // Find the angles of the Cartesian extremes.
            var a1 = Atan2(-b, a);
            var a2 = Atan2(-b, a) + PI;
            var a3 = Atan2(d, c);
            var a4 = Atan2(d, c) + PI;

            // Return the points of Cartesian extreme of the rotated ellipse.
            return new List<Point2D>
            {
                Interpolators.Ellipse(x, y, rX, rY, angle, a1),
                Interpolators.Ellipse(x, y, rX, rY, angle, a2),
                Interpolators.Ellipse(x, y, rX, rY, angle, a3),
                Interpolators.Ellipse(x, y, rX, rY, angle, a4)
            };
        }

        #endregion
    }
}
