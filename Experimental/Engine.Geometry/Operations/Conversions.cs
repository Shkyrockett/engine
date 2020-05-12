// <copyright file="Conversions.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
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
using System.Runtime.CompilerServices;
using static Engine.Mathematics;
using static Engine.Operations;
using static Engine.Polynomials;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Conversion methods to convert from one shape to another.
    /// </summary>
    public static partial class Conversions
    {
        #region Conversion Extension Methods
        /// <summary>
        /// Converts a circle to an ellipse.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <returns>
        /// Returns an ellipse with the values derived from the circle.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ellipse2D ToEllipse(this Circle2D circle) => CircleToEllipse(circle.X, circle.Y, circle.Radius);

        /// <summary>
        /// Convert a circle to a circular arc.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <returns>
        /// Returns a circular arc from the parameters of a circle.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArcSegment2D ToCircularArc(this Circle2D circle) => CircleToCircularArc(circle.X, circle.Y, circle.Radius);

        /// <summary>
        /// Generate an array of CubicBeziers, representing an elliptical arc centered at (x, y)
        /// with width w and height h. The arc starts at subtended angle start and stops at subtended angle startAngle + sweepAngle.
        /// Arcs greater than 90° are split into multiple arcs.
        /// </summary>
        /// <param name="ellipse">The ellipse.</param>
        /// <returns>
        /// Returns a list of Cubic Bézier curves that approximate an elliptical arc.
        /// </returns>
        /// <acknowledgment>
        /// Code ported from: https://www.khanacademy.org/computer-programming/e/6221186997551104
        /// Math from: http://www.spaceroots.org/documents/ellipse/node22.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<CubicBezierSegment2D> ToCubicBeziers(this EllipticalArcSegment2D ellipse) => EllipticalArcToCubicBeziers(ellipse.X, ellipse.Y, ellipse.RadiusA, ellipse.RadiusB, ellipse.StartAngle, ellipse.SweepAngle);

        /// <summary>
        /// Converts a line segment to a quadratic Bézier curve.
        /// </summary>
        /// <param name="segment">The line segment</param>
        /// <returns>
        /// Returns a Quadratic Bézier from a line segment.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuadraticBezierSegment2D ToQuadraticBezier(this LineSegment2D segment) => LineSegmentToQuadraticBezier(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);

        /// <summary>
        /// Converts a line segment to a Cubic Bézier.
        /// </summary>
        /// <param name="segment">The line segment.</param>
        /// <returns>
        /// Returns a Cubic Bézier from a line segment.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezierSegment2D ToCubicBezier(this LineSegment2D segment) => LineSegmentToCubicBezier(segment.A.X, segment.A.Y, segment.B.X, segment.B.Y);

        /// <summary>
        /// Converts a Quadratic Bézier curve to a Cubic Bézier curve.
        /// </summary>
        /// <param name="curve">The Quadratic Bézier curve.</param>
        /// <returns>
        /// Returns a Cubic Bézier curve from a Quadratic Bézier curve.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezierSegment2D ToCubicBezier(this QuadraticBezierSegment2D curve) => QuadraticBezierToCubicBezier(curve.A, curve.B, curve.C);
        #endregion Conversion Extension Methods

        /// <summary>
        /// Align points to a line.
        /// </summary>
        /// <param name="points">The points to align.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <returns>
        /// The <see cref="List{Point2D}" />.
        /// </returns>
        /// <acknowledgment>
        /// https://pomax.github.io/bezierinfo/#aligning
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<Point2D> AlignPoints(Span<Point2D> points, double x1, double y1, double x2, double y2)
        {
            //var angle = -Atan2(y2 - y1, x2 - x1);
            //var sinA = Sin(angle);
            //var cosA = Cos(angle);

            // Atan2, Sin and Cos are kind of slow. In theory this should be faster.
            var dx = x2 - x1;
            var dy = y2 - y1;
            var det = (dx * dx) + (dy * dy);

            // I believe det should only be 0 if the line is a point. Not sure what the correct value should be for overlapping points.
            var sinA = det == 0 ? 0 : -dy / det;
            var cosA = det == 0 ? 1 : -dx / det;

            var results = new List<Point2D>();

            foreach (var point in points)
            {
                results.Add(new Point2D(
                    ((point.X - x1) * cosA) - ((point.Y - y1) * sinA),
                    ((point.X - x1) * sinA) + ((point.Y - y1) * cosA))
                    );
            }

            return results;
        }

        #region Conversion Implementations
        /// <summary>
        /// Converts a Circle to a Circular arc.
        /// </summary>
        /// <param name="x">The x-component of the center point.</param>
        /// <param name="y">The y-component of the center point.</param>
        /// <param name="r">The radius of circle.</param>
        /// <returns>
        /// Returns a circular arc from a circle.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArcSegment2D CircleToCircularArc(double x, double y, double r) => new CircularArcSegment2D(x, y, r, 0, Tau);

        /// <summary>
        /// Converts a Circle to an ellipse.
        /// </summary>
        /// <param name="x">The x-component of the center point.</param>
        /// <param name="y">The y-component of the center point.</param>
        /// <param name="r">The radius of the circle.</param>
        /// <returns>
        /// Returns an ellipse from a circle.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ellipse2D CircleToEllipse(double x, double y, double r) => new Ellipse2D(x, y, r, r, 0);

        /// <summary>
        /// Generate an array of CubicBeziers, representing an elliptical arc centered at (x, y)
        /// with width w and height h. The arc starts at subtended angle start and stops at subtended angle startAngle + sweepAngle.
        /// Arcs greater than 90° are split into multiple arcs.
        /// </summary>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">X radius</param>
        /// <param name="ry">Y radius</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        /// <returns>
        /// Returns a list of Cubic Bézier curves that approximate a circular arc.
        /// </returns>
        /// <acknowledgment>
        /// Code ported from: https://www.khanacademy.org/computer-programming/e/6221186997551104
        /// Math from: http://www.spaceroots.org/documents/ellipse/node22.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<CubicBezierSegment2D> EllipticalArcToCubicBeziers(
            double cx, double cy,
            double rx, double ry,
            double startAngle, double sweepAngle)
        {
            /* Definition of ellipse: x²/a² + y²/b² = 1 */
            var stop = startAngle + sweepAngle;
            startAngle += Operations.SubtendedToParametric(startAngle, rx, ry);
            sweepAngle = stop + Operations.SubtendedToParametric(stop, rx, ry) - startAngle;
            var segs = Ceiling(Abs(sweepAngle) / HalfPi);
            var theta = sweepAngle / segs;  /* arc size of each segment */
            var tanT2 = Tan(theta * OneHalf);
            var alpha = Sin(theta) * (Sqrt(4 + (3 * tanT2 * tanT2)) - 1) * OneThird;
            var sine = Sin(startAngle);
            var cosine = Cos(startAngle);
            var sx = cx + (rx * cosine);  /* start pt X coordinate */
            var sy = cy + (ry * sine);  /* start pt Y coordinate */
            var dx = -rx * sine;  /* dx/dθ at start pt */
            var dy = ry * cosine;  /* dy/dθ at start pt */

            var beziers = new List<CubicBezierSegment2D>();  /* the results */

            double ex;
            double ey;  /* end pt coordinates */
            for (var s = 0; s < segs; s++, sx = ex, sy = ey)
            {
                var c1x = sx + (alpha * dx);  /* 1st control pt X */
                var c1y = sy + (alpha * dy); /* 1st control pt Y */

                cosine = Cos(startAngle += theta);
                sine = Sin(startAngle);
                ex = cx + (rx * cosine);  /* end pt X coordinate */
                ey = cy + (ry * sine);  /* end py Y coordinate */
                dx = -rx * sine;  /* dx/dθ at end pt */
                dy = ry * cosine;  /* dy/dθ at end pt */
                var c2x = ex - (alpha * dx);  /* 2nd control pt X */
                var c2y = ey - (alpha * dy);  /* 2nd control pt Y */

                beziers.Add(new CubicBezierSegment2D(sx, sy, c1x, c1y, c2x, c2y, ex, ey));
            }
            return beziers;
        }

        /// <summary>
        /// Converts a line segment to a Quadratic Bézier curve.
        /// </summary>
        /// <param name="a">The starting point.</param>
        /// <param name="b">The end point.</param>
        /// <returns>
        /// Returns a Quadratic Bézier curve with the properties of the line segment.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuadraticBezierSegment2D LineSegmentToQuadraticBezier(Point2D a, Point2D b) => LineSegmentToQuadraticBezier(a.X, a.Y, b.X, b.Y);

        /// <summary>
        /// Converts a line segment to a Quadratic Bézier curve.
        /// </summary>
        /// <param name="x0">The x-component of the first point of a line segment.</param>
        /// <param name="y0">The y-component of the first point of a line segment.</param>
        /// <param name="x1">The x-component of the second point of a line segment.</param>
        /// <param name="y1">The y-component of the second point of a line segment.</param>
        /// <returns>
        /// Returns a Quadratic Bezier with the properties of a line segment.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuadraticBezierSegment2D LineSegmentToQuadraticBezier(double x0, double y0, double x1, double y1) => new QuadraticBezierSegment2D(new Point2D(x0, y0), Operations.Lerp(x0, y0, x1, y1, OneHalf), new Point2D(x1, y1));

        /// <summary>
        /// Converts a Line segment to a Cubic Bézier curve.
        /// </summary>
        /// <param name="a">The starting point of the line segment.</param>
        /// <param name="b">The ending point of the line segment.</param>
        /// <returns>
        /// Returns a Cubic Bezier with the properties of a line segment.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezierSegment2D LineSegmentToCubicBezier(Point2D a, Point2D b) => LineSegmentToCubicBezier(a.X, a.Y, b.X, b.Y);

        /// <summary>
        /// Converts a Line segment to a Cubic Bézier curve.
        /// </summary>
        /// <param name="x0">The x-component of the starting point.</param>
        /// <param name="y0">The y-component of the starting point.</param>
        /// <param name="x1">The x-component of the end point.</param>
        /// <param name="y1">The y-component of the end point.</param>
        /// <returns>
        /// Returns a Cubic Bézier curve from the properties of a line segment.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezierSegment2D LineSegmentToCubicBezier(double x0, double y0, double x1, double y1) => new CubicBezierSegment2D(new Point2D(x0, y0), Operations.Lerp(x0, y0, x1, y1, OneThird), Operations.Lerp(x0, y0, x1, y1, TwoThirds), new Point2D(x1, y1));

        /// <summary>
        /// Converts a Quadratic Bezier to a Cubic Bezier.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <param name="c">The third point.</param>
        /// <returns>
        /// Returns a Cubic Bezier from a Quadratic Bezier.
        /// </returns>
        /// <acknowledgment></acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D[] QuadraticBezierToCubicBezierArray(Point2D a, Point2D b, Point2D c)
            => new Point2D[]
            {
                a,
                new Point2D(a.X + (TwoThirds * (b.X - a.X)), a.Y + (TwoThirds * (b.Y - a.Y))),
                new Point2D(c.X + (TwoThirds * (b.X - c.X)), c.Y + (TwoThirds * (b.Y - c.Y))),
                c
            };

        /// <summary>
        /// Raises a <see cref="QuadraticBezier2D" /> to a <see cref="CubicBezier2D" />.
        /// </summary>
        /// <param name="a">The starting point of the Quadratic Bézier curve.</param>
        /// <param name="b">The handle of the Quadratic Bézier curve.</param>
        /// <param name="c">The end point of the Quadratic Bézier curve.</param>
        /// <returns>
        /// Returns a Cubic Bézier curve from the Quadratic Bézier curve.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezierSegment2D QuadraticBezierToCubicBezier(Point2D a, Point2D b, Point2D c)
            => new CubicBezierSegment2D(
                a.X, a.Y,
                a.X + (TwoThirds * (b.X - a.X)), a.Y + (TwoThirds * (b.Y - a.Y)),
                c.X + (TwoThirds * (b.X - c.X)), c.Y + (TwoThirds * (b.Y - c.Y)),
                c.X, c.Y
            );

        /// <summary>
        /// Raise a Quadratic Bezier to a Cubic Bezier.
        /// </summary>
        /// <param name="aX">The x-component of the starting point.</param>
        /// <param name="aY">The y-component of the starting point.</param>
        /// <param name="bX">The x-component of the handle.</param>
        /// <param name="bY">The y-component of the handle.</param>
        /// <param name="cX">The x-component of the end point.</param>
        /// <param name="cY">The y-component of the end point.</param>
        /// <returns>
        /// Returns Quadratic Bézier curve from a cubic curve.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<(double X, double Y)> QuadraticBezierToCubicBezier(double aX, double aY, double bX, double bY, double cX, double cY)
            => new List<(double X, double Y)>
            {
                (aX, aY),
                (aX + (TwoThirds * (bX - aX)), aY + (TwoThirds * (bY - aY))),
                (cX + (TwoThirds * (bX - cX)), cY + (TwoThirds * (bY - cY))),
                (cX, cY)
            };

        /// <summary>
        /// Raise a Quadratic Bezier to a Cubic Bezier.
        /// </summary>
        /// <param name="aX">The x-component of the starting point.</param>
        /// <param name="aY">The y-component of the starting point.</param>
        /// <param name="bX">The x-component of the handle.</param>
        /// <param name="bY">The y-component of the handle.</param>
        /// <param name="cX">The x-component of the end point.</param>
        /// <param name="cY">The y-component of the end point.</param>
        /// <returns>
        /// Returns Quadratic Bézier curve from a cubic curve.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY) QuadraticBezierToCubicBezierTuple(double aX, double aY, double bX, double bY, double cX, double cY)
            => (aX, aY,
                aX + (TwoThirds * (bX - aX)), aY + (TwoThirds * (bY - aY)),
                cX + (TwoThirds * (bX - cX)), cY + (TwoThirds * (bY - cY)),
                cX, cY);

        /// <summary>
        /// Cubic Hermite curve to Cubic Bezier.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="t0">The t0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="t1">The t1.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d) CubicHermiteToCubicBezier(double p0, double t0, double p1, double t1)
        {
            var (a, b, c, d) = CubicHermiteBernsteinBasis(p0, t0, p1, t1);
            var (m1x1, m1x2, m1x3, m1x4, m2x1, m2x2, m2x3, m2x4, m3x1, m3x2, m3x3, m3x4, m4x1, m4x2, m4x3, m4x4) = InverseCubicBezierBernsteinBasisMatrix;
            return MultiplyVector4DMatrix4x4(d, c, b, a, m1x1, m1x2, m1x3, m1x4, m2x1, m2x2, m2x3, m2x4, m3x1, m3x2, m3x3, m3x4, m4x1, m4x2, m4x3, m4x4);
        }

        /// <summary>
        /// Cubic Hermite curve to Cubic Bezier.
        /// </summary>
        /// <param name="px0">The PX0.</param>
        /// <param name="py0">The py0.</param>
        /// <param name="tx0">The TX0.</param>
        /// <param name="ty0">The ty0.</param>
        /// <param name="px1">The PX1.</param>
        /// <param name="py1">The py1.</param>
        /// <param name="tx1">The TX1.</param>
        /// <param name="ty1">The ty1.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double ax, double ay, double bx, double by, double cx, double cy, double dx, double dy) CubicHermiteToCubicBezier(double px0, double py0, double tx0, double ty0, double px1, double py1, double tx1, double ty1)
        {
            var (ax, bx, cx, dx) = CubicHermiteBernsteinBasis(px0, tx0, px1, tx1);
            var (ay, by, cy, dy) = CubicHermiteBernsteinBasis(py0, ty0, py1, ty1);
            var (m1x1, m1x2, m1x3, m1x4, m2x1, m2x2, m2x3, m2x4, m3x1, m3x2, m3x3, m3x4, m4x1, m4x2, m4x3, m4x4) = InverseCubicBezierBernsteinBasisMatrix;
            var (rax, rbx, rcx, rdx) = MultiplyVector4DMatrix4x4(dx, cx, bx, ax, m1x1, m1x2, m1x3, m1x4, m2x1, m2x2, m2x3, m2x4, m3x1, m3x2, m3x3, m3x4, m4x1, m4x2, m4x3, m4x4);
            var (ray, rby, rcy, rdy) = MultiplyVector4DMatrix4x4(dy, cy, by, ay, m1x1, m1x2, m1x3, m1x4, m2x1, m2x2, m2x3, m2x4, m3x1, m3x2, m3x3, m3x4, m4x1, m4x2, m4x3, m4x4);
            return (rax, ray, rbx, rby, rcx, rcy, rdx, rdy);
        }
        #endregion Conversion Implementations

        #region Parabola Conversion
        /// <summary>
        /// Convert a parabola from standard form into vertex form.
        /// </summary>
        /// <param name="a">The <paramref name="a" /> component of the parabola.</param>
        /// <param name="b">The <paramref name="b" /> component of the parabola.</param>
        /// <param name="c">The <paramref name="c" /> component of the parabola.</param>
        /// <returns>
        /// Returns  <see cref="ValueTuple{T1, T2, T3}" /> representing the a, h, and k values of the vertex form of a parabola.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double h, double k) StandardParabolaToVertexParabola(double a, double b, double c) => (a, h: -(b / (2d * a)), k: -(b * b / (4d * a)) + c);

        /// <summary>
        /// Convert a parabola from vertex form into standard form.
        /// </summary>
        /// <param name="a">The <paramref name="a" /> component of the parabola.</param>
        /// <param name="h">The horizontal component of the parabola vertex.</param>
        /// <param name="k">The vertical component of the parabola vertex.</param>
        /// <returns>
        /// Returns  <see cref="ValueTuple{T1, T2, T3}" /> representing the a, b, and c values of the standard form of a parabola.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c) VertexParabolaToStandardParabola(double a, double h, double k)
        {
            var b = -2d * a * h;
            return (a, b, c: (b * b / (4d * a)) + k);
        }

        /// <summary>
        /// Find the Quadratic Bezier curve that represents the parabola.
        /// </summary>
        /// <param name="a">The a component of the Parabola.</param>
        /// <param name="b">The b component of the Parabola.</param>
        /// <param name="c">The c component of the Parabola.</param>
        /// <param name="x1">The first x position to crop the parabola.</param>
        /// <param name="x2">The second x position to crop the parabola.</param>
        /// <returns>
        /// Returns the control point locations of a Quadric Bezier curve.
        /// </returns>
        /// <acknowledgment>
        /// https://math.stackexchange.com/a/1258196
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double ax, double ay, double bx, double by, double cx, double cy) StandardParabolaToQuadraticBezier(double a, double b, double c, double x1, double x2)
        {
            // Get the vertical components of the end points.
            var y1 = (a * x1 * x1) + (x1 * b) + c;
            var y2 = (a * x2 * x2) + (x2 * b) + c;
            // Find the intersection of the tangents at the end nodes to find the center node.
            var cx = (x2 + x1) * 0.5d;
            var cy = (a * ((x2 * x1) - (x1 * x1))) + (b * (x2 - x1) * 0.5d) + y1;
            return (x1, y1, cx, cy, x2, y2);
        }

        /// <summary>
        /// Find the Quadratic Bezier curve that represents the parabola.
        /// </summary>
        /// <param name="a">The a component of the Parabola.</param>
        /// <param name="h">The horizontal component of the vertex of the parabola.</param>
        /// <param name="k">The vertical component of the vertex of the parabola.</param>
        /// <param name="x1">The first x position to crop the parabola.</param>
        /// <param name="x2">The second x position to crop the parabola.</param>
        /// <returns>
        /// Returns the control point locations of a Quadric Bezier curve.
        /// </returns>
        /// <acknowledgment>
        /// https://math.stackexchange.com/a/1258196
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double ax, double ay, double bx, double by, double cx, double cy) VertexParabolaToQuadraticBezier(double a, double h, double k, double x1, double x2)
        {
            // Get the vertical components of the end points.
            var y1 = (a * ((h * h) + (-2d * h * x1) + (x1 * x1))) + k;
            var y2 = (a * ((h * h) + (-2d * h * x2) + (x2 * x2))) + k;

            // Find the intersection of the tangents at the end nodes to find the center node.
            var cx = (x2 + x1) * 0.5;
            var cy = (a * ((h * x1) + (x1 * x2) - (h * x2) - (x1 * x1))) + y1;
            return (x1, y1, cx, cy, x2, y2);
        }

        /// <summary>
        /// Find a parabola in standard form from three points on the parabola.
        /// </summary>
        /// <param name="x1">The x component of the first point.</param>
        /// <param name="y1">The y component of the first point.</param>
        /// <param name="x2">The x component of the second point.</param>
        /// <param name="y2">The y component of the second point.</param>
        /// <param name="x3">The x component of the third point.</param>
        /// <param name="y3">The y component of the third point.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://stackoverflow.com/a/717833
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c) FindStandardParabolaFromThreePoints(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
            // ToDo: Work out what to do when denom is 0
            var a = ((x3 * (y2 - y1)) + (x2 * (y1 - y3)) + (x1 * (y3 - y2))) / denom;
            var b = ((x3 * x3 * (y1 - y2)) + (x2 * x2 * (y3 - y1)) + (x1 * x1 * (y2 - y3))) / denom;
            var c = ((x2 * x3 * (x2 - x3) * y1) + (x3 * x1 * (x3 - x1) * y2) + (x1 * x2 * (x1 - x2) * y3)) / denom;

            return (a, b, c);
        }

        /// <summary>
        /// Find a parabola in vertex form from three points on the parabola.
        /// </summary>
        /// <param name="x1">The x component of the first point.</param>
        /// <param name="y1">The y component of the first point.</param>
        /// <param name="x2">The x component of the second point.</param>
        /// <param name="y2">The y component of the second point.</param>
        /// <param name="x3">The x component of the third point.</param>
        /// <param name="y3">The y component of the third point.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://stackoverflow.com/a/717833
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double h, double k) FindVertexParabolaFromThreePoints(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
            // ToDo: Work out what to do when denom is 0.
            var a = ((x3 * (y2 - y1)) + (x2 * (y1 - y3)) + (x1 * (y3 - y2))) / denom;
            var b = ((x3 * x3 * (y1 - y2)) + (x2 * x2 * (y3 - y1)) + (x1 * x1 * (y2 - y3))) / denom;
            var c = ((x2 * x3 * (x2 - x3) * y1) + (x3 * x1 * (x3 - x1) * y2) + (x1 * x2 * (x1 - x2) * y3)) / denom;

            return (a, -b / (2d * a), c - (b * b / (4d * a)));
        }

        /// <summary>
        /// Find the parabola that passes through two points and has a k vertex height.
        /// </summary>
        /// <param name="x1">The x component of the first point on the parabola.</param>
        /// <param name="y1">The y component of the first point on the parabola.</param>
        /// <param name="x2">The x component of the second point on the parabola.</param>
        /// <param name="y2">The y component of the second point on the parabola.</param>
        /// <param name="k">The k or vertex height of the parabola.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://answers.yahoo.com/question/index?qid=20090730215957AAFg8ZK
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double h, double k) FindVertexParabolaFromTwoPointsAndK(double x1, double y1, double x2, double y2, double k)
        {
            var (ha, hb) = FindParabolaHFromTwoPointsAndK(x1, y1, x2, y2, k);
            var hv = (ha > x1 && ha < x2) ? ha : hb;
            return FindVertexParabolaFromThreePoints(x1, y1, hv, k, x2, y2);
        }

        /// <summary>
        /// Find the h of a parabola given two points on the parabola and the k vertex height.
        /// </summary>
        /// <param name="x1">The x component of the first point on the parabola.</param>
        /// <param name="y1">The y component of the first point on the parabola.</param>
        /// <param name="x2">The x component of the second point on the parabola.</param>
        /// <param name="y2">The y component of the second point on the parabola.</param>
        /// <param name="k">The k or vertex height of the parabola.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://answers.yahoo.com/question/index?qid=20090730215957AAFg8ZK
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b) FindParabolaHFromTwoPointsAndK(double x1, double y1, double x2, double y2, double k)
        {
            var u = y1 - k;
            var v = y2 - k;

            // Take care of possible divide by 0 cases.
            if (u == 0d && v == 0d)
            {
                // If y1, y2 and k are all the same, the parabola myst be a straight line.
                return (x2 + ((x2 - x1) * OneHalf), double.NaN);
            }
            else if (u == 0d)
            {
                // If y1 is the same height as k, it must start at the apex.
                return (x1, x1);
            }
            else if (v == 0d)
            {
                // If y2 is the same height as k, it must end at the apex.
                return (x2, x2);
            }

            var a = 1d - (v / u);
            var b = (-2d * x2) + (2d * x1 * (v / u));
            var c = (x2 * x2​) - (x1 * x1 * (v / u));

            // Find the roots.
            if (x1 == x2)
            {
                return (x1, x2);
            }
            if (a is 0d)
            {
                // If a is zero, reduce to linear, if b is also zero reduce to constant.
                return b is 0d ? (c, c) : (-c / b, -c / b);
            }
            else
            {
                var b_ = b / a;
                var c_ = c / a;
                var discriminant = (b_ * b_) - (4d * c_);

                if (discriminant == 0)
                {
                    return (OneHalf * -b_, OneHalf * -b_);
                }
                else if (discriminant > 0d)
                {
                    var e = Sqrt(discriminant);
                    return (OneHalf * (-b_ + e), OneHalf * (-b_ - e));
                }
                else
                {
                    // ToDo: Not sure exactly what to do here.
                    // Imaginary number.
                    var e = Sqrt(Abs(discriminant));
                    return (OneHalf * (-b_ + e), OneHalf * (-b_ - e));
                    //return (double.NaN, double.NaN);
                }
            }
        }

        /// <summary>
        /// Find the a of a parabola given two points on the parabola and the k vertex height.
        /// </summary>
        /// <param name="x">The x component of a point on the parabola.</param>
        /// <param name="y">The y component of a point on the parabola.</param>
        /// <param name="h">The h or horizontal component of the vertex of the parabola.</param>
        /// <param name="k">The k or vertex height of the parabola.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://answers.yahoo.com/question/index?qid=20090730215957AAFg8ZK
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double FindParabolaAFromAPointAndVertex(double x, double y, double h, double k) => x - h == 0d ? 0d : (y - k) / ((x - h) * (x - h));
        #endregion

        #region Rotated Rectangle
        /// <summary>
        /// Rotates the points of the corners of a rectangle about the fulcrum point.
        /// </summary>
        /// <param name="x">The x-component of the top left corner of the rectangle.</param>
        /// <param name="y">The y-component of the top left corner of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="fulcrumX">The x-component of the rotation fulcrum point.</param>
        /// <param name="fulcrumY">The x-component of the rotation fulcrum point.</param>
        /// <param name="angle">The angle to rotate the points.</param>
        /// <returns>
        /// Returns a list of points from the rectangle, rotated about the fulcrum.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> RotatedRectangle(double x, double y, double width, double height, double fulcrumX, double fulcrumY, double angle) => RotatedRectangle(x, y, width, height, fulcrumX, fulcrumY, Cos(angle), Sin(angle));

        /// <summary>
        /// Rotates the points of the corners of a rectangle about the fulcrum point.
        /// </summary>
        /// <param name="x">The x-component of the top left corner of the rectangle.</param>
        /// <param name="y">The y-component of the top left corner of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="fulcrumX">The x-component of the rotation fulcrum point.</param>
        /// <param name="fulcrumY">The x-component of the rotation fulcrum point.</param>
        /// <param name="cosAngle">The cos angle.</param>
        /// <param name="sinAngle">The sin angle.</param>
        /// <returns>
        /// Returns a list of points from the rectangle, rotated about the fulcrum.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> RotatedRectangle(double x, double y, double width, double height, double fulcrumX, double fulcrumY, double cosAngle, double sinAngle)
        {
            _ = x;
            _ = y;
            // ToDo: Figure out how to properly include the location point.
            var points = new List<Point2D>();

            var xaxis = new Point2D(cosAngle, sinAngle);
            var yaxis = new Point2D(-sinAngle, cosAngle);

            // Apply the rotation transformation and translate to new center.
            points.Add(new Point2D(
                fulcrumX + ((-width * 0.5d * xaxis.X) + (-height * 0.5d * xaxis.Y)),
                fulcrumY + ((-width * 0.5d * yaxis.X) + (-height * 0.5d * yaxis.Y))
                ));
            points.Add(new Point2D(
                fulcrumX + ((width * 0.5d * xaxis.X) + (-height * 0.5d * xaxis.Y)),
                fulcrumY + ((width * 0.5d * yaxis.X) + (-height * 0.5d * yaxis.Y))
                ));
            points.Add(new Point2D(
                fulcrumX + ((width * 0.5d * xaxis.X) + (height * 0.5d * xaxis.Y)),
                fulcrumY + ((width * 0.5d * yaxis.X) + (height * 0.5d * yaxis.Y))
                ));
            points.Add(new Point2D(
                fulcrumX + ((-width * 0.5d * xaxis.X) + (height * 0.5d * xaxis.Y)),
                fulcrumY + ((-width * 0.5d * yaxis.X) + (height * 0.5d * yaxis.Y))
                ));

            return points;
        }
        #endregion

        /// <summary>
        /// Changes the unit to base.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <param name="base">The @base.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static double ChangeUnitToBase(double value, double multiplier, double @base = 1, double addend = 0) => Pow((value - addend) / multiplier, 1d / @base);

        /// <summary>
        /// Changes the unit from base.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <param name="base">The @base.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static double ChangeUnitFromBase(double value, double multiplier, double @base = 1, double addend = 0) => (Pow(value, @base) * multiplier) + addend;
    }
}
