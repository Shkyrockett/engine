// <copyright file="Boundings.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// A collection of methods used to calculate the bounding rectangles of various shapes.
    /// </summary>
    public static class Boundings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this LineSegment segment)
            => LineSegment(segment.A, segment.B);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="circle"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this Circle circle)
            => Circle(circle.X, circle.Y, circle.Radius);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this CircularArc arc)
            => CircularArc(arc.X, arc.Y, arc.Radius, arc.StartAngle, arc.SweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this Ellipse ellipse)
            => Ellipse(ellipse.X, ellipse.Y, ellipse.RX, ellipse.RY, ellipse.Angle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this EllipticalArc arc)
            => EllipticalArc(arc.X, arc.Y, arc.RX, arc.RY, arc.Angle, arc.StartAngle, arc.SweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="poly"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this Polygon poly)
            => Polygon(poly.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="poly"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this Polyline poly)
            => Polygon(poly.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this QuadraticBezier bezier)
            => QuadraticBezier(bezier.AX, bezier.AY, bezier.BX, bezier.BY, bezier.CX, bezier.CY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this CubicBezier bezier)
            => CubicBezier(bezier.AX, bezier.AY, bezier.BX, bezier.BY, bezier.CX, bezier.CY, bezier.DX, bezier.DY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(this GeometryPath path)
            => GeometryPath(path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D LineSegment(Point2D a, Point2D b)
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
        public static Rectangle2D LineSegment(
            double x0, double y0,
            double x1, double y1)
            => new Rectangle2D(new Point2D(x0, y0), new Point2D(x1, y1));

        /// <summary>
        /// Calculate the external square boundaries of a circle.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of the Circle.</param>
        /// <returns>A Rectangle that is the size and location to envelop the circle.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Circle(
            double cX, double cY,
            double r)
            => Rectangle2D.FromLTRB((cX - r), (cY - r), (cX + r), (cY + r));

        /// <summary>
        /// Calculate the external close fitting rectangular bounds of a circular arc.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of the Circle.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>The close bounding box of a circular arc.</returns>
        /// <returns>A Rectangle large enough to closely fit the circular arc inside.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D CircularArc(
            double cX, double cY,
            double r,
            double startAngle, double sweepAngle)
        {
            double s = Maths.WrapAngle(startAngle);
            double e = Maths.WrapAngle(s + sweepAngle);
            var startPoint = new Point2D(cX + r * Cos(startAngle), cY + r * Sin(startAngle));
            var endPoint = new Point2D(cX + r * Cos(e), cY + r * Sin(e));
            //if (sweepAngle < 0)
            //    Swap(ref e, ref s);
            var bounds = new Rectangle2D(startPoint, endPoint);

            // check that s > e
            if (e < s)
                e += 2d * PI;
            if ((e >= 0d) && (s <= 0d))
                bounds.Right = cX + r;
            if ((e >= HalfPi) && (s <= HalfPi))
                bounds.Top = cY - r;
            if ((e >= PI) && (s <= PI))
                bounds.Left = cX - r;
            if ((e >= Pau) && (s <= Pau))
                bounds.Bottom = cY + r;
            if ((e >= Tau) && (s <= Tau))
                bounds.Right = cX + r;
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
        public static Rectangle2D Ellipse(
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
        public static Rectangle2D Ellipse(
            double cX, double cY,
            double r1, double r2,
            double angle)
        {
            double a = r1 * Cos(angle);
            double b = r2 * Sin(angle);
            double c = r1 * Sin(angle);
            double d = r2 * Cos(angle);

            // Get the height and width.
            double width = Sqrt((a * a) + (b * b)) * 2;
            double height = Sqrt((c * c) + (d * d)) * 2;

            // Get the location point.
            double x2 = cX - width * 0.5d;
            double y2 = cY - height * 0.5d;

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
        public static Rectangle2D EllipticalArc(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle, double sweepAngle)
        {
            // Get the ellipse rotation transform.
            double cosT = Cos(angle);
            double sinT = Sin(angle);

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
            double t0 = EllipsePolarAngle(startAngle, r1, r2);
            double t1 = EllipsePolarAngle(startAngle + sweepAngle, r1, r2);

            // Interpolate the ratios of height and width of the chord.
            double sinT0 = Sin(t0);
            double cosT0 = Cos(t0);
            double sinT1 = Sin(t1);
            double cosT1 = Cos(t1);

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
            double halfWidth = Sqrt((r1 * r1 * cosT * cosT) + (r2 * r2 * sinT * sinT));
            double halfHeight = Sqrt((r1 * r1 * sinT * sinT) + (r2 * r2 * cosT * cosT));

            // Expand the elliptical boundaries if any of the extreme angles fall within the sweep angle.
            if (Containings.Contains(angles[0], angle + startAngle, sweepAngle))
                bounds.Right = cX + halfWidth;
            if (Containings.Contains(angles[1], angle + startAngle, sweepAngle))
                bounds.Bottom = cY + halfHeight;
            if (Containings.Contains(angles[2], angle + startAngle, sweepAngle))
                bounds.Left = cX - halfWidth;
            if (Containings.Contains(angles[3], angle + startAngle, sweepAngle))
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
        public static Rectangle2D RotatedRectangle(
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
        public static Rectangle2D Polygon(IEnumerable<Point2D> polygon)
        {
            List<Point2D> points = (polygon as List<Point2D>);
            if (points?.Count < 1) return null;

            double left = points[0].X;
            double top = points[0].Y;
            double right = points[0].X;
            double bottom = points[0].Y;

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
        /// http://stackoverflow.com/questions/24809978/calculating-the-bounding-box-of-cubic-bezier-curve
        /// http://nishiohirokazu.blogspot.com/2009/06/how-to-calculate-bezier-curves-bounding.html
        /// http://jsfiddle.net/SalixAlba/QQnvm/4/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D CubicBezier(
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
                    var x1 = Interpolaters.Cubic(ax, bx, cx, dx, t1);
                    if (x1 < xlow) xlow = x1;
                    if (x1 > xhigh) xhigh = x1;
                }

                // Find the negative solution using the quadratic formula.
                var t2 = (-b - Sqrt(discriminant)) / (2 * a);

                if (t2 > 0 && t2 < 1)
                {
                    var x2 = Interpolaters.Cubic(ax, bx, cx, dx, t2);
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
                    var y1 = Interpolaters.Cubic(ay, by, cy, dy, t1);
                    if (y1 < yl) yl = y1;
                    if (y1 > yh) yh = y1;
                }

                var t2 = (-b - Sqrt(discriminant)) / (2 * a);

                if (t2 > 0 && t2 < 1)
                {
                    var y2 = Interpolaters.Cubic(ay, by, cy, dy, t2);
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
        public static Rectangle2D QuadraticBezier(
            double ax, double ay,
            double bx, double by,
            double cx, double cy)
        {
            var cubic = Interpolaters.QuadraticBezierToCubicBezier(ax, ay, bx, by, cx, cy);
            return CubicBezier(cubic[0].X, cubic[0].Y, cubic[1].X, cubic[1].Y, cubic[2].X, cubic[2].Y, cubic[3].X, cubic[3].Y);
        }

        /// <summary>
        /// Calculate the external bounding rectangle of a Chain.
        /// </summary>
        /// <param name="chain">The Chain.</param>
        /// <returns>A <see cref="Rectangle2D"/> that represents the external bounds of the chain.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D GeometryPath(GeometryPath chain)
        {
            var start = chain.Items[0] as PathPoint;
            Rectangle2D result = new Rectangle2D(start.Start.Value, start.End.Value);

            foreach (PathItem member in chain.Items)
            {
                result.Union(member.Bounds);
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
        public static Rectangle2D Parametric(
            Func<double, List<Point2D>> func,
            double count = 100d)
        {
            // Get the list of points from the parametric method.
            List<Point2D> points = func(count);
            if (points?.Count < 1)
                return null;

            // Fill with initial point.
            double left = points[0].X;
            double top = points[0].Y;
            double right = points[0].X;
            double bottom = points[0].Y;

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
    }
}
