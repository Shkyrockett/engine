// <copyright file="Offsets.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Geometry;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Offsets
    {
        /// <summary>
        /// Inflate Offsets a <see cref="Circle"/> by the specified amount.
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="offset"></param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Circle Offset( this Circle circle, double offset)
        {
            return new Circle(circle.X, circle.Y, circle.Radius + offset);
        }

        /// <summary>
        /// Inflate Offsets a <see cref="Ellipse"/> by the specified amount.
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="offset"></param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ellipse Offset(this Ellipse ellipse, double offset)
        {
            return new Ellipse(ellipse.X, ellipse.Y, ellipse.R1 + offset, ellipse.R2 + offset, ellipse.Angle);
        }

        /// <summary>
        /// Inflate Offsets a <see cref="Ellipse"/> by the specified amount.
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ellipse Offset(this Ellipse ellipse, double r1, double r2)
        {
            return new Ellipse(ellipse.X, ellipse.Y, ellipse.R1 + r1, ellipse.R2 + r2, ellipse.Angle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangle"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triangle Offset(this Triangle triangle, double  offset)
        {
            return (Triangle)Offset((Polygon)triangle, offset);
        }

        /// <summary>
        /// Inflate Offsets a <see cref="Rectangle2D"/> by the specified amount.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="offset"></param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Offset(this Rectangle2D rectangle, double offset)
        {
            return new Rectangle2D(rectangle.X - offset, rectangle.Y - offset, rectangle.Width + (2 * offset), rectangle.Height + (2 * offset));
        }

        /// <summary>
        /// Inflate Offsets a <see cref="Rectangle2D"/> by the specified amount.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Offset(this Rectangle2D rectangle, double x, double y)
        {
            return new Rectangle2D(rectangle.X - x, rectangle.Y - y, rectangle.Width + (2 * x), rectangle.Height + (2 * y));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polyline"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polyline Offset(this Polyline polyline, double offset)
        {
            var polygon = new Polyline();

            LineSegment offsetLine = Primitives.OffsetSegment(polyline.Points[0], polyline.Points[1], offset);
            polygon.Add(offsetLine.A);

            for (int i = 2; i < polyline.Points.Count; i++)
            {
                LineSegment newOffsetLine = Primitives.OffsetSegment(polyline.Points[i - 1], polyline.Points[i], offset);
                polygon.Add(Intersections.LineLine(offsetLine.A.X, offsetLine.A.Y, offsetLine.B.X, offsetLine.B.Y, newOffsetLine.A.X, newOffsetLine.A.Y, newOffsetLine.B.X, newOffsetLine.B.Y).Item2);
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
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polygon Offset(this Polygon polygon, double offset)
        {
            List<Point2D> points = (polygon.Points as List<Point2D>);

            var polyline = new Polygon();

            LineSegment offsetLine = Primitives.OffsetSegment(points[polygon.Points.Count - 1], points[0], offset);
            LineSegment startLine = offsetLine;

            for (int i = 1; i < polygon.Points.Count; i++)
            {
                LineSegment newOffsetLine = Primitives.OffsetSegment(points[i - 1], points[i], offset);
                polyline.Add(Intersections.LineLine(offsetLine.A.X, offsetLine.A.Y, offsetLine.B.X, offsetLine.B.Y, newOffsetLine.A.X, newOffsetLine.A.Y, newOffsetLine.B.X, newOffsetLine.B.Y).Item2);
                offsetLine = newOffsetLine;
            }

            polyline.Add(Intersections.LineLine(offsetLine.A.X, offsetLine.A.Y, offsetLine.B.X, offsetLine.B.Y, startLine.A.X, startLine.A.Y, startLine.B.X, startLine.B.Y).Item2);

            return polyline;
        }
    }
}
