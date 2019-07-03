// <copyright file="Intersections.ScanBeam.cs" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>

// <copyright company="kevlindev" >
//     Many of the Intersections methods were adapted from Kevin Lindsey's site http://www.kevlindev.com/gui/math/intersection/.
//     Copyright © 2000 - 2003 Kevin Lindsey. All rights reserved.
// </copyright>
// <author id="thelonious">Kevin Lindsey</author>
// <license>
//     Licensed under the BSD-3-Clause https://github.com/thelonious/kld-intersections/blob/development/LICENSE
// </license>

// <copyright company="angusj" >
//     The Point in Polygon method is from the Clipper Library.
//     Copyright © 2010 - 2014 Angus Johnson. All rights reserved.
// </copyright>
// <author id="angusj">Angus Johnson</author>
// <license id="Boost">
//     Licensed under the Boost Software License (http://www.boost.org/LICENSE_1_0.txt).
// </license>

// <copyright company="vb-helper" >
//     Some of the methods came from Rod Stephens excellent blogs vb-helper(http://vb-helper.com), and csharphelper (http://csharphelper.com), as well as from his books.
//     Copyright © Rod Stephens.
// </copyright>
// <author id="RodStephens">Rod Stephens</author>
// <license id="No Restrictions">
//     You can use the code you find on this site or in my books. I request but don’t require an acknowledgment.
//     I also recommend (but again don’t require) that you put the URL where you found the code in a comment inside your code in case you need to look it up later.
//     So really no restrictions. (http://csharphelper.com/blog/rod/)
// </license>

// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Mathematics;
using static Engine.Measurements;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The intersections class.
    /// </summary>
    public static partial class Intersections
    {
        #region Scan-beam Intersection Methods
        /// <summary>
        /// Find the scan-beam intersections of a point.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamPoint(ref List<double> scanlist, double x, double y, double px, double py, double epsilon = Epsilon)
        {
            _ = epsilon;
            if ((y - py) / (x - px) == 1d)
            {
                scanlist.Add(x);
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of a line.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamLine(ref List<double> scanlist, double x, double y, double x0, double y0, double i, double j, double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u1 = 1d - x;
            var v1 = 0d - y;

            var ua = (i * (y - y0)) - (j * (x - x0));
            var ub = (u1 * (y - y0)) - (v1 * (x - x0));

            // Calculate the determinant of the coefficient matrix.
            var determinant = (j * u1) - (i * v1);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0d || ub == 0d)
                {
                    // Line segment is coincident to the Line. There are an infinite number of intersections.
                    scanlist.Add(double.NegativeInfinity);
                    scanlist.Add(double.PositiveInfinity);
                }
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;

                // One intersection.
                scanlist.Add(x0 + (ta * u1));
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of a line segment.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamLineSegment(ref List<double> scanlist, double x, double y, double x0, double y0, double x1, double y1, double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u2 = x1 - x0;
            var v2 = y1 - y0;

            var ua = (u2 * (y - y0)) - (v2 * (x - x0));
            var ub = y - y0;

            // Calculate the determinant of the coefficient matrix.
            var determinant = v2;

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0d || ub == 0d)
                {
                    // Line segment is coincident to the scan-beam. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                    scanlist.Add(x0);
                    scanlist.Add(x1);
                }
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (tb >= 0d && tb <= 1d)
                {
                    // One intersection.
                    scanlist.Add(x + (ta * 1d));
                }
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of a quadratic Bézier curve segment.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamQuadraticBezierSegment(
            ref List<double> scanlist,
            double x, double y,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y,
            double epsilon = Epsilon)
            => ScanbeamQuadraticBezierSegment(
                ref scanlist,
                x, y,
                QuadraticBezierCoefficients(b0x, b1x, b2x),
                QuadraticBezierCoefficients(b0y, b1y, b2y),
                epsilon);

        /// <summary>
        /// Find the scan-beam intersections of a quadratic Bézier curve segment.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamQuadraticBezierSegment(
            ref List<double> scanlist,
            double x, double y,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            _ = epsilon;
            var c = (x * (y - (y + 0d))) + (y * (x + 1d - x));
            var roots = (yCurve - c).Trim().Roots();
            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0d || s > 1d))
                {
                    scanlist.Add((xCurve[0] * s * s) + (xCurve[1] * s) + xCurve[2]);
                }
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of a cubic Bézier curve segment.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="b3x"></param>
        /// <param name="b3y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamCubicBezierSegment(
            ref List<double> scanlist,
            double x, double y,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y, double b3x, double b3y,
            double epsilon = Epsilon)
            => ScanbeamCubicBezierSegment(
                ref scanlist,
                x, y,
                CubicBezierCoefficients(b0x, b1x, b2x, b3x),
                CubicBezierCoefficients(b0y, b1y, b2y, b3y),
                epsilon);

        /// <summary>
        /// Find the scan-beam intersections of a cubic Bézier curve segment.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamCubicBezierSegment(
            ref List<double> scanlist,
            double x, double y,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            _ = epsilon;
            // Translate the line to the origin.
            var c = (x * (y - (y + 0d))) + (y * (x + 1d - x));
            var roots = (yCurve - c).Trim().Roots();
            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0d || s > 1d))
                {
                    scanlist.Add((xCurve[0] * s * s * s) + (xCurve[1] * s * s) + (xCurve[2] * s) + xCurve[3]);
                }
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of a circle.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamCircle(ref List<double> scanlist, double x, double y, double cX, double cY, double r, double epsilon = Epsilon)
        {
            _ = epsilon;
            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((x == x + 1d) && (y == x + 0d)))
            {
                return;
            }

            // Calculate the quadratic parameters.
            var b = 2d * (x - cX);
            var c = ((x - cX) * (x - cX)) + ((y - cY) * (y - cY)) - (r * r);

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * c);

            if (discriminant < 0d)
            {
                // No real solutions.
                return;
            }
            else if (discriminant == 0d)
            {
                // One possible solution.
                var t = -b / 2d;

                // Add two points for top or bottom of the circle.
                scanlist.Add(x + (t * 1d));
                scanlist.Add(x + (t * 1d));
            }
            else if (discriminant > 0d)
            {
                // Two possible solutions.
                var t1 = (-b + Sqrt(discriminant)) / (2d * 1d);
                var t2 = (-b - Sqrt(discriminant)) / (2d * 1d);

                // Add the points.
                scanlist.Add(x + (t1 * 1d));
                scanlist.Add(x + (t2 * 1d));
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of a circular arc.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamCircularArc(ref List<double> scanlist, double x, double y, double cX, double cY, double r, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the circle or line segment are empty, return no intersections.
            if (r == 0d)
            {
                return;
            }

            // Calculate the quadratic parameters.
            var b = 2d * (x - cX);
            var c = ((x - cX) * (x - cX)) + ((y - cY) * (y - cY)) - (r * r);

            // Find the points of the chord.
            var startPoint = new Point2D(cX + (Cos(angle + startAngle) * r), cY + (Sin(angle + startAngle) * r));
            var endPoint = new Point2D(cX + (Cos(angle + startAngle + sweepAngle) * r), cY + (Sin(angle + startAngle + sweepAngle) * r));

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * c);

            // Check for intersections.
            if ((1d <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                return;
            }
            else if (discriminant == 0d)
            {
                // One possible solution.
                var t = -b / 2d;

                // Find the point.
                var pX = x + t;
                var pY = y + t;

                // Find the determinant of the chord and point.
                var determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle))
                {
                    // Add the point.
                    scanlist.Add(pX);
                    scanlist.Add(pX);
                }
            }
            else if (discriminant > 0d)
            {
                // Two possible solutions.
                var t1 = (-b + Sqrt(discriminant)) / 2d;
                var t2 = (-b - Sqrt(discriminant)) / 2d;

                // Find the point.
                var pX = x + (t1 * 1d);
                var pY = y + t1;

                // Find the determinant of the chord and point.
                var determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle))
                {
                    // Add the point.
                    scanlist.Add(pX);
                }

                // Find the point.
                pX = x + (t2 * 1d);
                pY = y + t2;

                // Find the determinant of the chord and point.
                determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle))
                {
                    // Add the point.
                    scanlist.Add(pX);
                }
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of an ellipse.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamEllipse(ref List<double> scanlist, double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double epsilon = Epsilon)
        {
            // If the ellipse is empty, return no intersections.
            if ((rx == 0d) || (ry == 0d))
            {
                return;
            }

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x - cx;
            var v1 = y - cy;
            var u2 = x - cx + 1d;
            var v2 = y - cy + 0d;

            // Apply Rotation Transform to line at the origin.
            var u1A = (u1 * cosA) - (v1 * -sinA);
            var v1A = (u1 * -sinA) + (v1 * cosA);
            var u2A = (u2 * cosA) - (v2 * -sinA);
            var v2A = (u2 * -sinA) + (v2 * cosA);

            // Calculate the quadratic parameters.
            var a = ((u2A - u1A) * (u2A - u1A) / (rx * rx)) + ((v2A - v1A) * (v2A - v1A) / (ry * ry));
            var b = (2d * u1A * (u2A - u1A) / (rx * rx)) + (2d * v1A * (v2A - v1A) / (ry * ry));
            var c = (u1A * u1A / (rx * rx)) + (v1A * v1A / (ry * ry)) - 1d;

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * a * c);

            // Find solutions.
            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                return;
            }
            else if (discriminant == 0d)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Add two points for either the top or bottom.
                scanlist.Add(u1 + ((u2 - u1) * t) + cx);
                scanlist.Add(u1 + ((u2 - u1) * t) + cx);
            }
            else if (discriminant > 0d)
            {
                // Two real possible solutions.
                var t1 = OneHalf * (-b + Sqrt(discriminant)) / a;
                var t2 = OneHalf * (-b - Sqrt(discriminant)) / a;

                // Add the points.
                scanlist.Add(u1 + ((u2 - u1) * t1) + cx);
                scanlist.Add(u1 + ((u2 - u1) * t2) + cx);
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of an elliptical arc.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamEllipticalArc(ref List<double> scanlist, double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d))
            {
                return;
            }

            // Translate the line to put it at the ellipse centered at the origin.
            var u0 = x - cx;
            var v0 = y - cy;
            var u1 = x - cx + 1d;
            var v1 = y - cy;

            // Apply the rotation transformation to line at the origin.
            var u0A = (u0 * cosA) - (v0 * -sinA);
            var v0A = (u0 * -sinA) + (v0 * cosA);
            var u1A = (u1 * cosA) - (v1 * -sinA);
            var v1A = (u1 * -sinA) + (v1 * cosA);

            // Calculate the quadratic parameters.
            var a = ((u1A - u0A) * (u1A - u0A) / (rx * rx)) + ((v1A - v0A) * (v1A - v0A) / (ry * ry));
            var b = (2d * u0A * (u1A - u0A) / (rx * rx)) + (2d * v0A * (v1A - v0A) / (ry * ry));
            var c = (u0A * u0A / (rx * rx)) + (v0A * v0A / (ry * ry)) - 1d;

            // Calculate the discriminant of the quadratic.
            var discriminant = (b * b) - (4d * a * c);

            // Check whether line segment is outside of the ellipse.
            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                return;
            }

            // Find the corrected start and end angles.
            var sa = EllipticalPolarAngle(startAngle, rx, ry);
            var ea = EllipticalPolarAngle(startAngle + sweepAngle, rx, ry);

            // Ellipse equation for an ellipse at origin; for the chord end points.
            var usa = rx * Cos(sa);
            var vsa = -(ry * Sin(sa));
            var uea = rx * Cos(ea);
            var vea = -(ry * Sin(ea));

            // Apply the rotation and translation transformations to find the chord points.
            var sx = cx + ((usa * cosA) + (vsa * sinA));
            var sy = cy + ((usa * sinA) - (vsa * cosA));
            var ex = cx + ((uea * cosA) + (vea * sinA));
            var ey = cy + ((uea * sinA) - (vea * cosA));

            if (discriminant == 0d)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Find the point.
                var px = u0 + ((u1 - u0) * t) + cx;
                var py = v0 + ((v1 - v0) * t) + cy;

                // Find the determinant of the matrix representing the chord.
                var determinant = ((sx - px) * (ey - py)) - ((ex - px) * (sy - py));

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                {
                    scanlist.Add(px);
                    scanlist.Add(px);
                }
            }
            else if (discriminant > 0d)
            {
                // Two real possible solutions.
                var root = Sqrt(discriminant);
                var t1 = OneHalf * (-b + root) / a;
                var t2 = OneHalf * (-b - root) / a;

                // Find the point.
                var px = u0 + ((u1 - u0) * t1) + cx;
                var py = v0 + ((v1 - v0) * t1) + cy;

                // Find the determinant of the matrix representing the chord.
                var determinant = ((sx - px) * (ey - py)) - ((ex - px) * (sy - py));

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                {
                    scanlist.Add(px);
                }

                // Find the point.
                px = u0 + ((u1 - u0) * t2) + cx;
                py = v0 + ((v1 - v0) * t2) + cy;

                // Find the determinant of the matrix representing the chord.
                determinant = ((sx - px) * (ey - py)) - ((ex - px) * (sy - py));

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                {
                    scanlist.Add(px);
                }
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of a rectangle.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamRectangle(ref List<double> scanlist, double x, double y, double r1X, double r1Y, double r2X, double r2Y, double epsilon = Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            ScanbeamLineSegment(ref scanlist, x, y, maxX, minY, maxX, maxY, epsilon);
            ScanbeamLineSegment(ref scanlist, x, y, minX, maxY, minX, minY, epsilon);
        }

        /// <summary>
        /// Find the scan-beam intersections of a polygon contour.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamPolygonContour(ref List<double> scanlist, double x, double y, List<Point2D> points, double epsilon = Epsilon)
        {
            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            var b1 = points[points.Count - 1];
            for (var i = 0; i < points.Count; ++i)
            {
                var b2 = points[i];

                ScanbeamLineSegment(ref scanlist, x, y, b1.X, b1.Y, b2.X, b2.Y, epsilon);

                b1 = b2;
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of a polycurve contour.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="curve"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamPolycurveContour(ref List<double> scanlist, double x, double y, PolycurveContour curve, double epsilon = Epsilon)
        {
            foreach (var segment in curve.Items)
            {
                switch (segment)
                {
                    case PointSegment t:
                        ScanbeamPoint(ref scanlist, x, y, t.Start.Value.X, t.Start.Value.Y, epsilon);
                        break;
                    case LineCurveSegment t:
                        ScanbeamLineSegment(ref scanlist, x, y, t.Start.Value.X, t.Start.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case QuadraticBezierSegment t:
                        ScanbeamQuadraticBezierSegment(ref scanlist, x, y, t.Start.Value.X, t.Start.Value.Y, t.Handle.Value.X, t.Handle.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case CubicBezierSegment t:
                        ScanbeamCubicBezierSegment(ref scanlist, x, y, t.Start.Value.X, t.Start.Value.Y, t.Handle1.X, t.Handle1.Y, t.Handle2.Value.X, t.Handle2.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case ArcSegment t:
                        ScanbeamEllipticalArc(ref scanlist, x, y, t.Center.X, t.Center.Y, t.RX, t.RY, t.CosAngle, t.SinAngle, t.StartAngle, t.SweepAngle, epsilon);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion Scan-beam Intersection Methods

        #region Scan-beam To Left Increment Methods
        /// <summary>
        /// Find the scan-beam points to the left of a point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftPoint(double x, double y, double px, double py, double epsilon = Epsilon)
        {
            _ = epsilon;
            return (((y - py) / (x - px) == 1) && (px <= x)) ? 1 : 0;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a line.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftLine(double x, double y, double x0, double y0, double i, double j, double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u1 = 1d - x;
            var v1 = 0d - y;

            var ua = (i * (y - y0)) - (j * (x - x0));
            var ub = (u1 * (y - y0)) - (v1 * (x - x0));

            // Calculate the determinant of the coefficient matrix.
            var determinant = (j * u1) - (i * v1);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0d || ub == 0d)
                {
                    // Line segment is coincident to the Line. There are an infinite number of intersections.
                    return 1;
                }
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;

                // One intersection.
                if (x0 + (ta * u1) <= x)
                {
                    return 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a line segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftLineSegment(double x, double y, double x0, double y0, double x1, double y1, double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u2 = x1 - x0;
            var v2 = y1 - y0;

            var ua = (u2 * (y - y0)) - (v2 * (x - x0));
            var ub = y - y0;

            // Calculate the determinant of the coefficient matrix.
            var determinant = v2;

            var result = 0;

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0d || ub == 0d)
                {
                    // Line segment is coincident to the scan-beam. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                    if (x0 <= x)
                    {
                        result++;
                    }

                    if (x1 <= x)
                    {
                        result++;
                    }
                }
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (tb >= 0d && tb <= 1d)
                {
                    // One intersection.
                    if (x0 + ta <= x)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a quadratic Bézier segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftQuadraticBezierSegment(
            double x, double y,
            double p0x, double p0y, double p1x, double p1y, double p2x, double p2y,
            double epsilon = Epsilon)
            => ScanbeamPointsToLeftQuadraticBezierSegment(
                x, y,
                QuadraticBezierCoefficients(p0x, p1x, p2x),
                QuadraticBezierCoefficients(p0y, p1y, p2y),
                epsilon);

        /// <summary>
        /// Find the scan-beam points to the left of a quadratic Bézier segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftQuadraticBezierSegment(
            double x, double y,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            _ = epsilon;
            var c = (x * (y - (y + 0d))) + (y * (x + 1d - x));
            var roots = (yCurve - c).Trim().Roots();
            var result = 0;
            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0d || s > 1d) && ((xCurve[0] * s * s) + (xCurve[1] * s) + xCurve[2] <= x))
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a cubic Bézier segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="b3x"></param>
        /// <param name="b3y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftCubicBezierSegment(
            double x, double y,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y, double b3x, double b3y,
            double epsilon = Epsilon)
            => ScanbeamPointsToLeftCubicBezierSegment(
                x, y,
                CubicBezierCoefficients(b0x, b1x, b2x, b3x),
                CubicBezierCoefficients(b0y, b1y, b2y, b3y),
                epsilon);

        /// <summary>
        /// Find the scan-beam points to the left of a cubic Bézier segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftCubicBezierSegment(
            double x, double y,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            _ = epsilon;
            // Translate the line to the origin.
            var c = (x * (y - (y + 0d))) + (y * (x + 1d - x));
            var roots = (yCurve - c).Trim().Roots();
            var results = 0;
            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0d || s > 1d) && ((xCurve[0] * s * s * s) + (xCurve[1] * s * s) + (xCurve[2] * s) + xCurve[3]) <= x)
                {
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a circle.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftCircle(double x, double y, double cX, double cY, double r, double angle = 0, double startAngle = 0, double sweepAngle = 0, double epsilon = Epsilon)
        {
            _ = angle;
            _ = startAngle;
            _ = sweepAngle;
            _ = epsilon;
            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((x == x + 1d) && (y == x + 0d)))
            {
                return 0;
            }

            // Calculate the quadratic parameters.
            var b = 2d * (x - cX);
            var c = ((x - cX) * (x - cX)) + ((y - cY) * (y - cY)) - (r * r);

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * c);

            var result = 0;
            if (discriminant < 0d)
            {
                // No real solutions.
                return 0;
            }
            else if (discriminant == 0d)
            {
                // One possible solution.
                var t = -b / 2d;

                // Add two points for top or bottom of the circle.
                if (x + (t * 1) <= x)
                {
                    result++;
                    result++;
                }
            }
            else if (discriminant > 0d)
            {
                // Two possible solutions.
                var t1 = (-b + Sqrt(discriminant)) / (2d * 1d);
                var t2 = (-b - Sqrt(discriminant)) / (2d * 1d);

                // Add the points.
                if (x + (t1 * 1d) <= x)
                {
                    result++;
                }

                if (x + (t2 * 1d) <= x)
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a circular arc.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftCircularArc(double x, double y, double cX, double cY, double r, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the circle or line segment are empty, return no intersections.
            if (r == 0d)
            {
                return 0;
            }

            // Calculate the quadratic parameters.
            var b = 2d * (x - cX);
            var c = ((x - cX) * (x - cX)) + ((y - cY) * (y - cY)) - (r * r);

            // Find the points of the chord.
            var startPoint = new Point2D(cX + (Cos(angle + startAngle) * r), cY + (Sin(angle + startAngle) * r));
            var endPoint = new Point2D(cX + (Cos(angle + startAngle + sweepAngle) * r), cY + (Sin(angle + startAngle + sweepAngle) * r));

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * c);

            var results = 0;

            // Check for intersections.
            if ((1 <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                return 0;
            }
            else if (discriminant == 0d)
            {
                // One possible solution.
                var t = -b / 2d;

                // Find the point.
                var pX = x + t;
                var pY = y + t;

                // Find the determinant of the chord and point.
                var determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && pX <= x)
                {
                    // Add the point.
                    results++;
                    results++;
                }
            }
            else if (discriminant > 0d)
            {
                // Two possible solutions.
                var t1 = (-b + Sqrt(discriminant)) / 2d;
                var t2 = (-b - Sqrt(discriminant)) / 2d;

                // Find the point.
                var pX = x + t1;
                var pY = y + t1;

                // Find the determinant of the chord and point.
                var determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && pX <= x)
                {
                    // Add the point.
                    results++;
                }

                // Find the point.
                pX = x + t2;
                pY = y + t2;

                // Find the determinant of the chord and point.
                determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && pX <= x)
                {
                    // Add the point.
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the left of an ellipse.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftEllipse(double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double epsilon = Epsilon)
        {
            // If the ellipse is empty, return no intersections.
            if ((rx == 0d) || (ry == 0d))
            {
                return 0;
            }

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x - cx;
            var v1 = y - cy;
            var u2 = x - cx + 1d;
            var v2 = y - cy + 0d;

            // Apply Rotation Transform to line at the origin.
            var u1A = (u1 * cosA) - (v1 * -sinA);
            var v1A = (u1 * -sinA) + (v1 * cosA);
            var u2A = (u2 * cosA) - (v2 * -sinA);
            var v2A = (u2 * -sinA) + (v2 * cosA);

            // Calculate the quadratic parameters.
            var a = ((u2A - u1A) * (u2A - u1A) / (rx * rx)) + ((v2A - v1A) * (v2A - v1A) / (ry * ry));
            var b = (2d * u1A * (u2A - u1A) / (rx * rx)) + (2d * v1A * (v2A - v1A) / (ry * ry));
            var c = (u1A * u1A / (rx * rx)) + (v1A * v1A / (ry * ry)) - 1d;

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * a * c);

            var results = 0;

            // Find solutions.
            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                return 0;
            }
            else if (discriminant == 0d)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Add two points for either the top or bottom.
                if ((u1 + ((u2 - u1) * t) + cx) <= x)
                {
                    results++;
                }

                if ((u1 + ((u2 - u1) * t) + cx) <= x)
                {
                    results++;
                }
            }
            else if (discriminant > 0d)
            {
                // Two real possible solutions.
                var t1 = OneHalf * (-b + Sqrt(discriminant)) / a;
                var t2 = OneHalf * (-b - Sqrt(discriminant)) / a;

                // Add the points.
                if (u1 + ((u2 - u1) * t1) + cx <= x)
                {
                    results++;
                }

                if (u1 + ((u2 - u1) * t2) + cx <= x)
                {
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the left of an elliptical arc.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftEllipticalArc(double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d))
            {
                return 0;
            }

            // Translate the line to put it at the ellipse centered at the origin.
            var u0 = x - cx;
            var v0 = y - cy;
            var u1 = x - cx + 1d;
            var v1 = y - cy;

            // Apply the rotation transformation to line at the origin.
            var u0A = (u0 * cosA) - (v0 * -sinA);
            var v0A = (u0 * -sinA) + (v0 * cosA);
            var u1A = (u1 * cosA) - (v1 * -sinA);
            var v1A = (u1 * -sinA) + (v1 * cosA);

            // Calculate the quadratic parameters.
            var a = ((u1A - u0A) * (u1A - u0A) / (rx * rx)) + ((v1A - v0A) * (v1A - v0A) / (ry * ry));
            var b = (2d * u0A * (u1A - u0A) / (rx * rx)) + (2d * v0A * (v1A - v0A) / (ry * ry));
            var c = (u0A * u0A / (rx * rx)) + (v0A * v0A / (ry * ry)) - 1d;

            // Calculate the discriminant of the quadratic.
            var discriminant = (b * b) - (4d * a * c);

            // Check whether line segment is outside of the ellipse.
            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                return 0;
            }

            // Find the corrected start and end angles.
            var sa = EllipticalPolarAngle(startAngle, rx, ry);
            var ea = EllipticalPolarAngle(startAngle + sweepAngle, rx, ry);

            // Ellipse equation for an ellipse at origin; for the chord end points.
            var usa = rx * Cos(sa);
            var vsa = -(ry * Sin(sa));
            var uea = rx * Cos(ea);
            var vea = -(ry * Sin(ea));

            // Apply the rotation and translation transformations to find the chord points.
            var sx = cx + ((usa * cosA) + (vsa * sinA));
            var sy = cy + ((usa * sinA) - (vsa * cosA));
            var ex = cx + ((uea * cosA) + (vea * sinA));
            var ey = cy + ((uea * sinA) - (vea * cosA));

            var results = 0;
            if (discriminant == 0d)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Find the point.
                var px = u0 + ((u1 - u0) * t) + cx;
                var py = v0 + ((v1 - v0) * t) + cy;

                // Find the determinant of the matrix representing the chord.
                var determinant = ((sx - px) * (ey - py)) - ((ex - px) * (sy - py));

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px <= x)
                {
                    results++;
                    results++;
                }
            }
            else if (discriminant > 0d)
            {
                // Two real possible solutions.
                var root = Sqrt(discriminant);
                var t1 = OneHalf * (-b + root) / a;
                var t2 = OneHalf * (-b - root) / a;

                // Find the point.
                var px = u0 + ((u1 - u0) * t1) + cx;
                var py = v0 + ((v1 - v0) * t1) + cy;

                // Find the determinant of the matrix representing the chord.
                var determinant = ((sx - px) * (ey - py)) - ((ex - px) * (sy - py));

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px <= x)
                {
                    results++;
                }

                // Find the point.
                px = u0 + ((u1 - u0) * t2) + cx;
                py = v0 + ((v1 - v0) * t2) + cy;

                // Find the determinant of the matrix representing the chord.
                determinant = ((sx - px) * (ey - py)) - ((ex - px) * (sy - py));

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px <= x)
                {
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a rectangle.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftRectangle(double x, double y, double r1X, double r1Y, double r2X, double r2Y, double epsilon = Epsilon)
        {
            var results = 0;
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            results += ScanbeamPointsToLeftLineSegment(x, y, maxX, minY, maxX, maxY, epsilon);
            results += ScanbeamPointsToLeftLineSegment(x, y, minX, maxY, minX, minY, epsilon);
            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a polygon contour.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftPolygonContour(double x, double y, List<Point2D> points, double epsilon = Epsilon)
        {
            var result = 0;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            var b1 = points[points.Count - 1];
            for (var i = 0; i < points.Count; ++i)
            {
                var b2 = points[i];

                result += ScanbeamPointsToLeftLineSegment(x, y, b1.X, b1.Y, b2.X, b2.Y, epsilon);

                b1 = b2;
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a polycurve contour.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="curve"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftPolycurveContour(double x, double y, PolycurveContour curve, double epsilon = Epsilon)
        {
            var results = 0;
            foreach (var segment in curve.Items)
            {
                switch (segment)
                {
                    case PointSegment t:
                        results += ScanbeamPointsToLeftPoint(x, y, t.Start.Value.X, t.Start.Value.Y, epsilon);
                        break;
                    case LineCurveSegment t:
                        results += ScanbeamPointsToLeftLineSegment(x, y, t.Start.Value.X, t.Start.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case QuadraticBezierSegment t:
                        results += ScanbeamPointsToLeftQuadraticBezierSegment(x, y, t.Start.Value.X, t.Start.Value.Y, t.Handle.Value.X, t.Handle.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case CubicBezierSegment t:
                        results += ScanbeamPointsToLeftCubicBezierSegment(x, y, t.Start.Value.X, t.Start.Value.Y, t.Handle1.X, t.Handle1.Y, t.Handle2.Value.X, t.Handle2.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case ArcSegment t:
                        results += ScanbeamPointsToLeftEllipticalArc(x, y, t.Center.X, t.Center.Y, t.RX, t.RY, t.CosAngle, t.SinAngle, t.StartAngle, t.SweepAngle, epsilon);
                        break;
                    default:
                        break;
                }
            }

            return results;
        }
        #endregion Scan-beam To Left Increment Methods

        #region Scan-beam To Right Increment Methods
        /// <summary>
        /// Find the scan-beam points to the right of a point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightPoint(double x, double y, double px, double py, double epsilon = Epsilon)
        {
            _ = epsilon;
            return (((y - py) / (x - px) == 1) && (px >= x)) ? 1 : 0;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a line.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightLine(double x, double y, double x0, double y0, double i, double j, double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u1 = 1d - x;
            var v1 = 0d - y;

            var ua = (i * (y - y0)) - (j * (x - x0));
            var ub = (u1 * (y - y0)) - (v1 * (x - x0));

            // Calculate the determinant of the coefficient matrix.
            var determinant = (j * u1) - (i * v1);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0d || ub == 0d)
                {
                    // Line segment is coincident to the Line. There are an infinite number of intersections.
                    return 1;
                }
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;

                // One intersection.
                if (x0 + (ta * u1) >= x)
                {
                    return 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a line segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightLineSegment(double x, double y, double x0, double y0, double x1, double y1, double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u2 = x1 - x0;
            var v2 = y1 - y0;

            var ua = (u2 * (y - y0)) - (v2 * (x - x0));
            var ub = y - y0;

            // Calculate the determinant of the coefficient matrix.
            var determinant = v2;

            var result = 0;

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                //if (ua == 0d || ub == 0d)
                //{
                //    // Line segment is coincident to the scan-beam. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                //    if (x0 >= x)
                //        result++;
                //    if (x1 >= x)
                //        result++;
                //}
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (tb >= 0d && tb <= 1d)
                {
                    // One intersection.
                    if (x0 + ta >= x)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a quadratic Bézier curve segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightQuadraticBezierSegment(
            double x, double y,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y,
            double epsilon = Epsilon)
            => ScanbeamPointsToRightQuadraticBezierSegment(
                x, y,
                QuadraticBezierCoefficients(b0x, b1x, b2x),
                QuadraticBezierCoefficients(b0y, b1y, b2y),
                epsilon);

        /// <summary>
        /// Find the scan-beam points to the right of a quadratic Bézier curve segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightQuadraticBezierSegment(
            double x, double y,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            _ = epsilon;
            var c = (x * (y - (y + 0d))) + (y * (x + 1d - x));
            var roots = (yCurve - c).Trim().Roots();
            var result = 0;
            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0 || s > 1d) && (((xCurve[0] * s * s) + (xCurve[1] * s) + xCurve[2]) >= x))
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a cubic Bézier curve segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="b3x"></param>
        /// <param name="b3y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightCubicBezierSegment(
            double x, double y,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y, double b3x, double b3y,
            double epsilon = Epsilon)
            => ScanbeamPointsToRightCubicBezierSegment(
                x, y,
                CubicBezierCoefficients(b0x, b1x, b2x, b3x),
                CubicBezierCoefficients(b0y, b1y, b2y, b3y),
                epsilon);

        /// <summary>
        /// Find the scan-beam points to the right of a cubic Bézier curve segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightCubicBezierSegment(
            double x, double y,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            _ = epsilon;
            // Translate the line to the origin.
            var c = (x * (y - (y + 0d))) + (y * (x + 1d - x));
            var roots = (yCurve - c).Trim().Roots();
            var results = 0;
            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0d || s > 1d) && ((xCurve[0] * s * s * s) + (xCurve[1] * s * s) + (xCurve[2] * s) + xCurve[3]) >= x)
                {
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a circle.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightCircle(double x, double y, double cX, double cY, double r, double angle = 0, double startAngle = 0, double sweepAngle = 0, double epsilon = Epsilon)
        {
            _ = angle;
            _ = startAngle;
            _ = sweepAngle;
            _ = epsilon;
            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((x == x + 1d) && (y == x + 0d)))
            {
                return 0;
            }

            // Calculate the quadratic parameters.
            var b = 2d * (x - cX);
            var c = ((x - cX) * (x - cX)) + ((y - cY) * (y - cY)) - (r * r);

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * c);

            var result = 0;
            if (discriminant < 0d)
            {
                // No real solutions.
                return 0;
            }
            else if (discriminant == 0d)
            {
                // One possible solution.
                var t = -b / 2d;

                // Add two points for top or bottom of the circle.
                if (x + (t * 1) >= x)
                {
                    result++;
                    result++;
                }
            }
            else if (discriminant > 0d)
            {
                // Two possible solutions.
                var t1 = (-b + Sqrt(discriminant)) / (2d * 1d);
                var t2 = (-b - Sqrt(discriminant)) / (2d * 1d);

                // Add the points.
                if (x + (t1 * 1d) >= x)
                {
                    result++;
                }

                if (x + (t2 * 1d) >= x)
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a circular arc.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightCircularArc(double x, double y, double cX, double cY, double r, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the circle or line segment are empty, return no intersections.
            if (r == 0d)
            {
                return 0;
            }

            // Calculate the quadratic parameters.
            var b = 2d * (x - cX);
            var c = ((x - cX) * (x - cX)) + ((y - cY) * (y - cY)) - (r * r);

            // Find the points of the chord.
            var startPoint = new Point2D(cX + (Cos(angle + startAngle) * r), cY + (Sin(angle + startAngle) * r));
            var endPoint = new Point2D(cX + (Cos(angle + startAngle + sweepAngle) * r), cY + (Sin(angle + startAngle + sweepAngle) * r));

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * c);

            var results = 0;

            // Check for intersections.
            if ((1d <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                return 0;
            }
            else if (discriminant == 0d)
            {
                // One possible solution.
                var t = -b / 2d;

                // Find the point.
                var pX = x + t;
                var pY = y + t;

                // Find the determinant of the chord and point.
                var determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && pX >= x)
                {
                    // Add the point.
                    results++;
                    results++;
                }
            }
            else if (discriminant > 0d)
            {
                // Two possible solutions.
                var t1 = (-b + Sqrt(discriminant)) / 2d;
                var t2 = (-b - Sqrt(discriminant)) / 2d;

                // Find the point.
                var pX = x + t1;
                var pY = y + t1;

                // Find the determinant of the chord and point.
                var determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && pX >= x)
                {
                    // Add the point.
                    results++;
                }

                // Find the point.
                pX = x + t2;
                pY = y + t2;

                // Find the determinant of the chord and point.
                determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && pX >= x)
                {
                    // Add the point.
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the right of an ellipse.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightEllipse(double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double epsilon = Epsilon)
        {
            // If the ellipse is empty, return no intersections.
            if ((rx == 0d) || (ry == 0d))
            {
                return 0;
            }

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x - cx;
            var v1 = y - cy;
            var u2 = x - cx + 1d;
            var v2 = y - cy + 0d;

            // Apply Rotation Transform to line at the origin.
            var u1A = (u1 * cosA) - (v1 * -sinA);
            var v1A = (u1 * -sinA) + (v1 * cosA);
            var u2A = (u2 * cosA) - (v2 * -sinA);
            var v2A = (u2 * -sinA) + (v2 * cosA);

            // Calculate the quadratic parameters.
            var a = ((u2A - u1A) * (u2A - u1A) / (rx * rx)) + ((v2A - v1A) * (v2A - v1A) / (ry * ry));
            var b = (2d * u1A * (u2A - u1A) / (rx * rx)) + (2d * v1A * (v2A - v1A) / (ry * ry));
            var c = (u1A * u1A / (rx * rx)) + (v1A * v1A / (ry * ry)) - 1d;

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * a * c);

            var results = 0;

            // Find solutions.
            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                return 0;
            }
            else if (discriminant == 0d)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Add two points for either the top or bottom.
                if ((u1 + ((u2 - u1) * t) + cx) >= x)
                {
                    results++;
                }

                if ((u1 + ((u2 - u1) * t) + cx) >= x)
                {
                    results++;
                }
            }
            else if (discriminant > 0d)
            {
                // Two real possible solutions.
                var t1 = OneHalf * (-b + Sqrt(discriminant)) / a;
                if (u1 + ((u2 - u1) * t1) + cx >= x)
                {
                    results++;
                }

                var t2 = OneHalf * (-b - Sqrt(discriminant)) / a;
                if (u1 + ((u2 - u1) * t2) + cx >= x)
                {
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the right of an elliptical arc.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightEllipticalArc(double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d))
            {
                return 0;
            }

            // Translate the line to put it at the ellipse centered at the origin.
            var u0 = x - cx;
            var v0 = y - cy;
            var u1 = x - cx + 1;
            var v1 = y - cy;

            // Apply the rotation transformation to line at the origin.
            var u0A = (u0 * cosA) - (v0 * -sinA);
            var v0A = (u0 * -sinA) + (v0 * cosA);
            var u1A = (u1 * cosA) - (v1 * -sinA);
            var v1A = (u1 * -sinA) + (v1 * cosA);

            // Calculate the quadratic parameters.
            var a = ((u1A - u0A) * (u1A - u0A) / (rx * rx)) + ((v1A - v0A) * (v1A - v0A) / (ry * ry));
            var b = (2d * u0A * (u1A - u0A) / (rx * rx)) + (2d * v0A * (v1A - v0A) / (ry * ry));
            var c = (u0A * u0A / (rx * rx)) + (v0A * v0A / (ry * ry)) - 1d;

            // Calculate the discriminant of the quadratic.
            var discriminant = (b * b) - (4d * a * c);

            // Check whether line segment is outside of the ellipse.
            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                return 0;
            }

            // Find the corrected start and end angles.
            var sa = EllipticalPolarAngle(startAngle, rx, ry);
            var ea = EllipticalPolarAngle(startAngle + sweepAngle, rx, ry);

            // Ellipse equation for an ellipse at origin; for the chord end points.
            var usa = rx * Cos(sa);
            var vsa = -(ry * Sin(sa));
            var uea = rx * Cos(ea);
            var vea = -(ry * Sin(ea));

            // Apply the rotation and translation transformations to find the chord points.
            var sx = cx + ((usa * cosA) + (vsa * sinA));
            var sy = cy + ((usa * sinA) - (vsa * cosA));
            var ex = cx + ((uea * cosA) + (vea * sinA));
            var ey = cy + ((uea * sinA) - (vea * cosA));

            var results = 0;
            if (discriminant == 0d)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Find the point.
                var px = u0 + ((u1 - u0) * t) + cx;
                var py = v0 + ((v1 - v0) * t) + cy;

                // Find the determinant of the matrix representing the chord.
                var determinant = ((sx - px) * (ey - py)) - ((ex - px) * (sy - py));

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px >= x)
                {
                    //if (sx != px && sy != py || ex != px && ey != py)
                    results++;
                    results++;
                }
            }
            else if (discriminant > 0d)
            {
                // Two real possible solutions.
                var root = Sqrt(discriminant);
                var t1 = OneHalf * (-b + root) / a;
                var t2 = OneHalf * (-b - root) / a;

                // Find the point.
                var px = u0 + ((u1 - u0) * t1) + cx;
                var py = v0 + ((v1 - v0) * t1) + cy;

                // Find the determinant of the matrix representing the chord.
                var determinant = ((sx - px) * (ey - py)) - ((ex - px) * (sy - py));

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px >= x)
                {
                    results++;
                }

                // Find the point.
                px = u0 + ((u1 - u0) * t2) + cx;
                py = v0 + ((v1 - v0) * t2) + cy;

                // Find the determinant of the matrix representing the chord.
                determinant = ((sx - px) * (ey - py)) - ((ex - px) * (sy - py));

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px >= x)
                {
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a rectangle.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightRectangle(double x, double y, double r1X, double r1Y, double r2X, double r2Y, double epsilon = Epsilon)
        {
            var results = 0;
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            results += ScanbeamPointsToRightLineSegment(x, y, maxX, minY, maxX, maxY, epsilon);
            results += ScanbeamPointsToRightLineSegment(x, y, minX, maxY, minX, minY, epsilon);
            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a polygon contour.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightPolygonContour(double x, double y, List<Point2D> points, double epsilon = Epsilon)
        {
            var result = 0;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            var b1 = points[points.Count - 1];
            for (var i = 0; i < points.Count; ++i)
            {
                var b2 = points[i];

                result += ScanbeamPointsToRightLineSegment(x, y, b1.X, b1.Y, b2.X, b2.Y, epsilon);

                b1 = b2;
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a polycurve contour.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="curve"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightPolycurveContour(double x, double y, PolycurveContour curve, double epsilon = Epsilon)
        {
            var results = 0;
            foreach (var segment in curve.Items)
            {
                switch (segment)
                {
                    case PointSegment t:
                        results += ScanbeamPointsToRightPoint(x, y, t.Start.Value.X, t.Start.Value.Y, epsilon);
                        break;
                    case LineCurveSegment t:
                        results += ScanbeamPointsToRightLineSegment(x, y, t.Start.Value.X, t.Start.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case QuadraticBezierSegment t:
                        results += ScanbeamPointsToRightQuadraticBezierSegment(x, y, t.Start.Value.X, t.Start.Value.Y, t.Handle.Value.X, t.Handle.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case CubicBezierSegment t:
                        results += ScanbeamPointsToRightCubicBezierSegment(x, y, t.Start.Value.X, t.Start.Value.Y, t.Handle1.X, t.Handle1.Y, t.Handle2.Value.X, t.Handle2.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case ArcSegment t:
                        results += ScanbeamPointsToRightEllipticalArc(x, y, t.Center.X, t.Center.Y, t.RX, t.RY, t.CosAngle, t.SinAngle, t.StartAngle, t.SweepAngle, epsilon);
                        break;
                    default:
                        break;
                }
            }

            return results;
        }
        #endregion Scan-beam To Right Increment Methods
    }
}
