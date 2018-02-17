// <copyright file="Offsets.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// The offsets class.
    /// </summary>
    public static class Offsets
    {
        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="value">The value.</param>
        /// <param name="distance">The distance.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> Offset(this Point2D point, Point2D value, double distance)
        {
            var (x1, y1, x2, y2) = OffsetSegment(point.X, point.Y, value.X, value.Y, distance);
            return new List<Point2D> { new Point2D(x1, y1), new Point2D(x2, y2) };
        }

        /// <summary>
        /// The offset segment.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="value">The value.</param>
        /// <param name="distance">The distance.</param>
        /// <returns>The <see cref="LineSegment"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment OffsetSegment(Point2D point, Point2D value, double distance)
            => new LineSegment(OffsetSegment(point.X, point.Y, value.X, value.Y, distance));

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <param name="distance">The distance.</param>
        /// <returns>The <see cref="LineSegment"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment Offset(this LineSegment segment, double distance)
            => new LineSegment(OffsetSegment(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y, distance));

        /// <summary>
        /// The offset segment.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="distance">The distance.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x1, double y1, double x2, double y2)
            OffsetSegment(
            double aX, double aY,
            double bX, double bY,
            double distance)
        {
            var d = Measurements.Distance(aX, aY, bX, bY);
            var dY = (bY - aY) / d;
            var dX = (bX - aX) / d;
            return ((aX + 0.5 * -dY * distance),
                (aY + 0.5 * dX * distance),
                (bX + 0.5 * -dY * distance),
                (bY + 0.5 * dX * distance));
        }

        /// <summary>
        /// The offset segment.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="aZ">The aZ.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="bZ">The bZ.</param>
        /// <param name="distanceX">The distanceX.</param>
        /// <param name="distanceY">The distanceY.</param>
        /// <param name="distanceZ">The distanceZ.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x1, double y1, double z1, double x2, double y2, double z2)
            OffsetSegment(
            double aX, double aY, double aZ,
            double bX, double bY, double bZ,
            double distanceX, double distanceY, double distanceZ)
        {
            var d = Measurements.Distance(aX, aY, aZ, bX, bY, bZ);
            var dX = (bX - aX) / d;
            var dY = (bY - aY) / d;
            var dZ = (bZ - aZ) / d;
            return ((aX + 0.5 * -dY * distanceX),
                (aY + 0.5 * dX * distanceY),
                (aZ + 0.5 * dZ * distanceZ),
                (bX + 0.5 * -dY * distanceX),
                (bY + 0.5 * dX * distanceY),
                (bZ + 0.5 * dZ * distanceZ));
        }

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The <see cref="Circle"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Circle Offset(this Circle circle, double offset)
            => new Circle(circle.X, circle.Y, circle.Radius + offset);

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="ellipse">The ellipse.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The <see cref="Ellipse"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ellipse Offset(this Ellipse ellipse, double offset)
            => new Ellipse(ellipse.X, ellipse.Y, ellipse.RX + offset, ellipse.RY + offset, ellipse.Angle);

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="ellipse">The ellipse.</param>
        /// <param name="r1">The r1.</param>
        /// <param name="r2">The r2.</param>
        /// <returns>The <see cref="Ellipse"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ellipse Offset(this Ellipse ellipse, double r1, double r2)
            => new Ellipse(ellipse.X, ellipse.Y, ellipse.RX + r1, ellipse.RY + r2, ellipse.Angle);

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="triangle">The triangle.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The <see cref="Triangle"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triangle Offset(this Triangle triangle, double offset)
            => (Triangle)Offset((PolygonContour)triangle, offset);

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Offset(this Rectangle2D rectangle, double offset)
            => new Rectangle2D(rectangle.X - offset, rectangle.Y - offset, rectangle.Width + (2 * offset), rectangle.Height + (2 * offset));

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Offset(this Rectangle2D rectangle, double x, double y)
            => new Rectangle2D(rectangle.X - x, rectangle.Y - y, rectangle.Width + (2 * x), rectangle.Height + (2 * y));

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="polyline">The polyline.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The <see cref="Polyline"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polyline Offset(this Polyline polyline, double offset)
        {
            var polygon = new Polyline();

            var offsetLine = OffsetSegment(polyline.Points[0], polyline.Points[1], offset).ToLine();
            polygon.Add(offsetLine.Location);

            for (var i = 2; i < polyline.Points.Count; i++)
            {
                var newOffsetLine = OffsetSegment(polyline.Points[i - 1], polyline.Points[i], offset).ToLine();
                polygon.Add(offsetLine.Intersection(newOffsetLine)[0]);
                offsetLine = newOffsetLine;
            }

            polygon.Add(offsetLine.Location + offsetLine.Direction);

            return polygon;
        }

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The <see cref="PolygonContour"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolygonContour Offset(this PolygonContour polygon, double offset)
        {
            var points = (polygon.Points as List<Point2D>);

            var polyline = new PolygonContour();

            var offsetLine = OffsetSegment(points[polygon.Points.Count - 1], points[0], offset).ToLine();
            var startLine = offsetLine;

            for (var i = 1; i < polygon.Points.Count; i++)
            {
                var newOffsetLine = OffsetSegment(points[i - 1], points[i], offset).ToLine();
                polyline.Add(offsetLine.Intersection(newOffsetLine)[0]);
                offsetLine = newOffsetLine;
            }

            polyline.Add(offsetLine.Intersection(startLine)[0]);

            return polyline;
        }
    }
}
