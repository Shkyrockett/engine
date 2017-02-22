// <copyright file="Offsets.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Offsets
    {
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
            var offset = OffsetSegment(point.X, point.Y, value.X, value.Y, distance);
            return new List<Point2D> { new Point2D(offset.x1, offset.y1), new Point2D(offset.x2, offset.y2) };
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
            => new LineSegment(OffsetSegment(point.X, point.Y, value.X, value.Y, distance));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment Offset(this LineSegment segment, double distance)
            => new LineSegment(OffsetSegment(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y, distance));

        /// <summary>
        ///
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x1, double y1, double x2, double y2)
            OffsetSegment(
            double aX, double aY,
            double bX, double bY,
            double distance)
        {
            double d = Measurements.Distance(aX, aY, bX, bY);
            double dY = (bY - aY) / d;
            double dX = (bX - aX) / d;
            return ((aX + 0.5 * -dY * distance),
                (aY + 0.5 * dX * distance),
                (bX + 0.5 * -dY * distance),
                (bY + 0.5 * dX * distance));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="aZ"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="bZ"></param>
        /// <param name="distanceX"></param>
        /// <param name="distanceY"></param>
        /// <param name="distanceZ"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x1, double y1, double z1, double x2, double y2, double z2)
            OffsetSegment(
            double aX, double aY, double aZ,
            double bX, double bY, double bZ,
            double distanceX, double distanceY, double distanceZ)
        {
            double d = Measurements.Distance(aX, aY, aZ, bX, bY, bZ);
            double dX = (bX - aX) / d;
            double dY = (bY - aY) / d;
            double dZ = (bZ - aZ) / d;
            return ((aX + 0.5 * -dY * distanceX),
                (aY + 0.5 * dX * distanceY),
                (aZ + 0.5 * dZ * distanceZ),
                (bX + 0.5 * -dY * distanceX),
                (bY + 0.5 * dX * distanceY),
                (bZ + 0.5 * dZ * distanceZ));
        }

        /// <summary>
        /// Inflate Offsets a <see cref="Circle"/> by the specified amount.
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="offset"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Circle Offset(this Circle circle, double offset)
            => new Circle(circle.X, circle.Y, circle.Radius + offset);

        /// <summary>
        /// Inflate Offsets a <see cref="Ellipse"/> by the specified amount.
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="offset"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ellipse Offset(this Ellipse ellipse, double offset)
            => new Ellipse(ellipse.X, ellipse.Y, ellipse.RX + offset, ellipse.RY + offset, ellipse.Angle);

        /// <summary>
        /// Inflate Offsets a <see cref="Ellipse"/> by the specified amount.
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ellipse Offset(this Ellipse ellipse, double r1, double r2)
            => new Ellipse(ellipse.X, ellipse.Y, ellipse.RX + r1, ellipse.RY + r2, ellipse.Angle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangle"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triangle Offset(this Triangle triangle, double offset)
            => (Triangle)Offset((Contour)triangle, offset);

        /// <summary>
        /// Inflate Offsets a <see cref="Rectangle2D"/> by the specified amount.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="offset"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Offset(this Rectangle2D rectangle, double offset)
            => new Rectangle2D(rectangle.X - offset, rectangle.Y - offset, rectangle.Width + (2 * offset), rectangle.Height + (2 * offset));

        /// <summary>
        /// Inflate Offsets a <see cref="Rectangle2D"/> by the specified amount.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Offset(this Rectangle2D rectangle, double x, double y)
            => new Rectangle2D(rectangle.X - x, rectangle.Y - y, rectangle.Width + (2 * x), rectangle.Height + (2 * y));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polyline"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polyline Offset(this Polyline polyline, double offset)
        {
            var polygon = new Polyline();

            LineSegment offsetLine = OffsetSegment(polyline.Points[0], polyline.Points[1], offset);
            polygon.Add(offsetLine.A);

            for (int i = 2; i < polyline.Points.Count; i++)
            {
                LineSegment newOffsetLine = OffsetSegment(polyline.Points[i - 1], polyline.Points[i], offset);
                polygon.Add(Intersections.LineLineIntersection(offsetLine.A.X, offsetLine.A.Y, offsetLine.B.X, offsetLine.B.Y, newOffsetLine.A.X, newOffsetLine.A.Y, newOffsetLine.B.X, newOffsetLine.B.Y)[0]);
                offsetLine = newOffsetLine;
            }

            polygon.Add(offsetLine.B);

            return polygon;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Contour Offset(this Contour polygon, double offset)
        {
            List<Point2D> points = (polygon.Points as List<Point2D>);

            var polyline = new Contour();

            LineSegment offsetLine = OffsetSegment(points[polygon.Points.Count - 1], points[0], offset);
            LineSegment startLine = offsetLine;

            for (int i = 1; i < polygon.Points.Count; i++)
            {
                LineSegment newOffsetLine = OffsetSegment(points[i - 1], points[i], offset);
                polyline.Add(Intersections.LineLineIntersection(offsetLine.A.X, offsetLine.A.Y, offsetLine.B.X, offsetLine.B.Y, newOffsetLine.A.X, newOffsetLine.A.Y, newOffsetLine.B.X, newOffsetLine.B.Y)[0]);
                offsetLine = newOffsetLine;
            }

            polyline.Add(Intersections.LineLineIntersection(offsetLine.A.X, offsetLine.A.Y, offsetLine.B.X, offsetLine.B.Y, startLine.A.X, startLine.A.Y, startLine.B.X, startLine.B.Y)[0]);

            return polyline;
        }
    }
}
