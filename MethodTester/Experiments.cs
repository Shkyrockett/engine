// <copyright file="Experiments.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <acknowledgment></acknowledgment>

using Engine;
using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static Engine.Maths;
using static System.Math;

namespace MethodSpeedTester
{
    /// <summary>
    /// Class to contain experimental methods to test.
    /// </summary>
    public static partial class Experiments
    {
        #region Boundaries of Polygons
        /// <summary>
        /// Get the bounds.
        /// </summary>
        /// <param name="paths">The paths.</param>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        public static Rectangle2D GetBounds(List<List<Point2D>> paths)
        {
            var i = 0;
            var cnt = paths.Count;
            while (i < cnt && paths[i].Count == 0)
            {
                i++;
            }

            if (i == cnt)
            {
                return new Rectangle2D(0, 0, 0, 0);
            }

            var result = new Rectangle2D
            {
                Left = paths[i][0].X
            };
            result.Right = result.Left;
            result.Top = paths[i][0].Y;
            result.Bottom = result.Top;
            for (; i < cnt; i++)
            {
                for (var j = 0; j < paths[i].Count; j++)
                {
                    if (paths[i][j].X < result.Left)
                    {
                        result.Left = paths[i][j].X;
                    }
                    else if (paths[i][j].X > result.Right)
                    {
                        result.Right = paths[i][j].X;
                    }

                    if (paths[i][j].Y < result.Top)
                    {
                        result.Top = paths[i][j].Y;
                    }
                    else if (paths[i][j].Y > result.Bottom)
                    {
                        result.Bottom = paths[i][j].Y;
                    }
                }
            }

            return result;
        }
        #endregion Boundaries of Polygons

        #region Boundaries of Polygons
        /// <summary>
        /// Set of tests to run testing methods that Find the bounds of polygons.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(BoundsOfPolygon))]
        public static List<SpeedTester> BoundsOfPolygon()
            => new List<SpeedTester> {
                new SpeedTester(() => PolygonBounds0(new List<Point2D>{(10, 10), (25,5), (5,30)}),
                $"{nameof(Experiments.PolygonBounds0)}(new List<Point2D>(){{(10, 10), (25,5), (5,30)}})"),
                 new SpeedTester(() => PolygonBounds1(new List<Point2D>{(10, 10), (25,5), (5,30)}),
                $"{nameof(Experiments.PolygonBounds1)}(new List<Point2D>(){{(10, 10), (25,5), (5,30)}})")
           };

        /// <summary>
        /// Calculate the external bounding rectangle of a Polygon.
        /// </summary>
        /// <param name="polygon">The points of the polygon.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D PolygonBounds0(IEnumerable<Point2D> polygon)
        {
            var points = polygon as List<Point2D>;
            if (points?.Count < 1)
            {
                return null;
            }

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
        /// The polygon bounds1.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D PolygonBounds1(List<Point2D> path)
        {
            var result = new Rectangle2D
            {
                Left = path[0].X,
                Top = path[0].Y,
                Right = path[0].X,
                Bottom = path[0].Y
            };

            for (var i = 0; i < path.Count; i++)
            {
                if (path[i].X < result.Left)
                {
                    result.Left = path[i].X;
                }
                else if (path[i].X > result.Right)
                {
                    result.Right = path[i].X;
                }

                if (path[i].Y < result.Top)
                {
                    result.Top = path[i].Y;
                }
                else if (path[i].Y > result.Bottom)
                {
                    result.Bottom = path[i].Y;
                }
            }

            return result;
        }
        #endregion Boundaries of Polygons

        #region Boundary of Cubic Bézier
        /// <summary>
        /// The cubic bezier bounds.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        public static Rectangle2D CubicBezierBounds(Point2D a, Point2D b, Point2D c, Point2D d)
        {
            var sortOfCloseLength = (int)Measurements.CubicBezierArcLength(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y);
            var points = new List<Point2D>(Interpolators.Interpolate0to1((i) => Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, i), sortOfCloseLength));

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
        /// The cubic bezier bounds2.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/24809978/calculating-the-bounding-box-of-cubic-bezier-curve
        /// http://floris.briolas.nl/floris/2009/10/bounding-box-of-cubic-bezier/
        /// http://jsfiddle.net/SalixAlba/QQnvm/4/
        /// </acknowledgment>
        public static Rectangle2D CubicBezierBounds2(Point2D p0, Point2D p1, Point2D p2, Point2D p3)
        {
            var a = 3 * p3.X - 9 * p2.X + 9 * p1.X - 3 * p0.X;
            var b = 6 * p0.X - 12 * p1.X + 6 * p2.X;
            var c = 3 * p1.X - 3 * p0.X;

            var disc = b * b - 4 * a * c;
            var xl = p0.X;
            var xh = p0.X;
            if (p3.X < xl)
            {
                xl = p3.X;
            }

            if (p3.X > xh)
            {
                xh = p3.X;
            }

            if (disc >= 0)
            {
                var t1 = (-b + Sqrt(disc)) / (2 * a);

                if (t1 > 0 && t1 < 1)
                {
                    var x1 = Interpolators.Cubic(p0.X, p1.X, p2.X, p3.X, t1);
                    if (x1 < xl)
                    {
                        xl = x1;
                    }

                    if (x1 > xh)
                    {
                        xh = x1;
                    }
                }

                var t2 = (-b - Sqrt(disc)) / (2 * a);

                if (t2 > 0 && t2 < 1)
                {
                    var x2 = Interpolators.Cubic(p0.X, p1.X, p2.X, p3.X, t2);
                    if (x2 < xl)
                    {
                        xl = x2;
                    }

                    if (x2 > xh)
                    {
                        xh = x2;
                    }
                }
            }

            a = 3 * p3.Y - 9 * p2.Y + 9 * p1.Y - 3 * p0.Y;
            b = 6 * p0.Y - 12 * p1.Y + 6 * p2.Y;
            c = 3 * p1.Y - 3 * p0.Y;
            disc = b * b - 4 * a * c;
            var yl = p0.Y;
            var yh = p0.Y;
            if (p3.Y < yl)
            {
                yl = p3.Y;
            }

            if (p3.Y > yh)
            {
                yh = p3.Y;
            }

            if (disc >= 0)
            {
                var t1 = (-b + Sqrt(disc)) / (2 * a);

                if (t1 > 0 && t1 < 1)
                {
                    var y1 = Interpolators.Cubic(p0.Y, p1.Y, p2.Y, p3.Y, t1);
                    if (y1 < yl)
                    {
                        yl = y1;
                    }

                    if (y1 > yh)
                    {
                        yh = y1;
                    }
                }

                var t2 = (-b - Sqrt(disc)) / (2 * a);

                if (t2 > 0 && t2 < 1)
                {
                    var y2 = Interpolators.Cubic(p0.Y, p1.Y, p2.Y, p3.Y, t2);
                    if (y2 < yl)
                    {
                        yl = y2;
                    }

                    if (y2 > yh)
                    {
                        yh = y2;
                    }
                }
            }

            return new Rectangle2D(xl, xh, yl, yh);
        }

        //public static double evalBez(double p0, double p1, double p2, double p3, double t)
        //{
        //    return p0 * (1 - t) * (1 - t) * (1 - t) + 3 * p1 * t * (1 - t) * (1 - t) + 3 * p2 * t * t * (1 - t) + p3 * t * t * t;
        //}
        #endregion Boundary of Cubic Bézier

        #region Boundary of Quadratic Bézier
        /// <summary>
        /// The quadratic bezier bounds.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        public static Rectangle2D QuadraticBezierBounds(Point2D a, Point2D b, Point2D c)
        {
            var sortOfCloseLength = Measurements.QuadraticBezierArcLengthByIntegral(a.X, a.Y, b.X, b.Y, c.X, c.Y);
            // ToDo: Need to make this more efficient. Don't need to rebuild the point array every time.
            var points = new List<Point2D>(Interpolators.Interpolate0to1((i) => Interpolators.QuadraticBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, i), (int)(sortOfCloseLength / 3)));

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
        #endregion Boundary of Quadratic Bézier

        ///// <summary>
        ///// Calculates the Axis Aligned Bounding Box (AABB) rectangle of a Quadratic Bézier curve.
        ///// </summary>
        ///// <param name="ax">The x-component of the starting point.</param>
        ///// <param name="ay">The y-component of the starting point.</param>
        ///// <param name="bx">The x-component of the handle point.</param>
        ///// <param name="by">The y-component of the handle point.</param>
        ///// <param name="cx">The x-component of the end point.</param>
        ///// <param name="cy">The y-component of the end point.</param>
        ///// <returns>Returns an Axis Aligned Bounding Box (AABB) rectangle that bounds the Quadratic Bézier curve.</returns>
        ///// <acknowledgment></acknowledgment>
        ///// <acknowledgment>
        ///// http://stackoverflow.com/questions/24809978/calculating-the-bounding-box-of-cubic-bezier-curve
        ///// http://jsfiddle.net/SalixAlba/QQnvm/4/
        ///// </acknowledgment>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Rectangle2D QuadraticBezierBounds(
        //    double ax, double ay,
        //    double bx, double by,
        //    double cx, double cy)
        //{
        //    var cubic = Conversions.QuadraticBezierToCubicBezier(ax, ay, bx, by, cx, cy);
        //    return CubicBezierBounds(cubic[0].X, cubic[0].Y, cubic[1].X, cubic[1].Y, cubic[2].X, cubic[2].Y, cubic[3].X, cubic[3].Y);
        //}

        ///// <summary>
        ///// Calculates the Axis Aligned Bounding Box (AABB) rectangle of a Cubic Bézier curve.
        ///// </summary>
        ///// <param name="ax">The x-component of the starting point.</param>
        ///// <param name="ay">The y-component of the starting point.</param>
        ///// <param name="bx">The x-component of the first handle point.</param>
        ///// <param name="by">The y-component of the first handle point.</param>
        ///// <param name="cx">The x-component of the second handle point.</param>
        ///// <param name="cy">The y-component of the second handle point.</param>
        ///// <param name="dx">The x-component of the end point.</param>
        ///// <param name="dy">The y-component of the end point.</param>
        ///// <returns>Returns an Axis Aligned Bounding Box (AABB) rectangle that bounds the Cubic Bézier curve.</returns>
        ///// <acknowledgment>
        ///// This method has an error where if the end nodes are horizontal to each other, while the handles are also horizontal to each other the bounds are not correctly calculated.
        ///// </acknowledgment>
        ///// <acknowledgment>
        ///// Method created using the following resources.
        ///// http://stackoverflow.com/questions/24809978/calculating-the-bounding-box-of-cubic-bezier-curve
        ///// http://nishiohirokazu.blogspot.com/2009/06/how-to-calculate-bezier-curves-bounding.html
        ///// http://jsfiddle.net/SalixAlba/QQnvm/4/
        ///// </acknowledgment>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Rectangle2D CubicBezierBounds(
        //    double ax, double ay,
        //    double bx, double by,
        //    double cx, double cy,
        //    double dx, double dy)
        //{
        //    // Calculate the polynomial of the cubic.
        //    var a = -3 * ax + 9 * bx - 9 * cx + 3 * dx;
        //    var b = 6 * ax - 12 * bx + 6 * cx;
        //    var c = -3 * ax + 3 * bx;

        //    // Calculate the discriminant of the polynomial.
        //    var discriminant = b * b - 4 * a * c;

        //    // Find the high and low x ends.
        //    var xlow = (dx < ax) ? dx : ax;
        //    var xhigh = (dx > ax) ? dx : ax;

        //    if (discriminant >= 0)
        //    {
        //        // Find the positive solution using the quadratic formula.
        //        var t1 = (-b + Sqrt(discriminant)) / (2 * a);

        //        if (t1 > 0 && t1 < 1)
        //        {
        //            var x1 = Interpolators.Cubic(ax, bx, cx, dx, t1);
        //            if (x1 < xlow) xlow = x1;
        //            if (x1 > xhigh) xhigh = x1;
        //        }

        //        // Find the negative solution using the quadratic formula.
        //        var t2 = (-b - Sqrt(discriminant)) / (2 * a);

        //        if (t2 > 0 && t2 < 1)
        //        {
        //            var x2 = Interpolators.Cubic(ax, bx, cx, dx, t2);
        //            if (x2 < xlow) xlow = x2;
        //            if (x2 > xhigh) xhigh = x2;
        //        }
        //    }

        //    a = -3 * ay + 9 * by - 9 * cy + 3 * dy;
        //    b = 6 * ay - 12 * by + 6 * cy;
        //    c = -3 * ay + 3 * by;

        //    discriminant = b * b - 4 * a * c;

        //    var yl = ay;
        //    var yh = ay;
        //    if (dy < yl) yl = dy;
        //    if (dy > yh) yh = dy;
        //    if (discriminant >= 0)
        //    {
        //        var t1 = (-b + Sqrt(discriminant)) / (2 * a);

        //        if (t1 > 0 && t1 < 1)
        //        {
        //            var y1 = Interpolators.Cubic(ay, by, cy, dy, t1);
        //            if (y1 < yl) yl = y1;
        //            if (y1 > yh) yh = y1;
        //        }

        //        var t2 = (-b - Sqrt(discriminant)) / (2 * a);

        //        if (t2 > 0 && t2 < 1)
        //        {
        //            var y2 = Interpolators.Cubic(ay, by, cy, dy, t2);
        //            if (y2 < yl) yl = y2;
        //            if (y2 > yh) yh = y2;
        //        }
        //    }

        //    return new Rectangle2D(xlow, xhigh, yl, yh);
        //}

        #region Catmull-Rom 1D Spline Interpolation
        /// <summary>
        /// Set of tests to run testing methods that calculate the angle between Two 3D points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(CatmullRomSplineInterpolation1DTests))]
        public static List<SpeedTester> CatmullRomSplineInterpolation1DTests()
            => new List<SpeedTester>
            {
                new SpeedTester(() => CatmullRom(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.CatmullRom)}(0, 0, 1, 1, 0.5d)"),
                new SpeedTester(() => CatmullRomSpline(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.CatmullRomSpline)}(0, 0, 1, 1, 0.5d)")
            };

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="v1">The first position in the interpolation.</param>
        /// <param name="v2">The second position in the interpolation.</param>
        /// <param name="v3">The third position in the interpolation.</param>
        /// <param name="v4">The fourth position in the interpolation.</param>
        /// <param name="t">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        /// <acknowledgment>
        /// http://www.mvps.org/directx/articles/catmull/
        /// </acknowledgment>
        public static double CatmullRom(
            double v1,
            double v2,
            double v3,
            double v4,
            double t)
        {
            var tSquared = t * t;
            var tCubed = tSquared * t;
            return 
                0.5d * (2d * v2
                + (v3 - v1) * t
                + (2d * v1 - 5d * v2 + 4d * v3 - v4) * tSquared
                + (3d * v2 - v1 - 3.0d * v3 + v4) * tCubed);
        }

        /// <summary>
        /// The catmull rom spline.
        /// </summary>
        /// <param name="v0">The v0.</param>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <param name="v3">The v3.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CatmullRomSpline(
            double v0,
            double v1,
            double v2,
            double v3,
            double t)
        {
            var mu2 = t * t;
            var a0 = -0.5 * v0 + 1.5 * v1 - 1.5 * v2 + 0.5 * v3;
            var a1 = v0 - 2.5 * v1 + 2 * v2 - 0.5 * v3;
            var a2 = -0.5 * v0 + 0.5 * v2;
            var a3 = v1;
            return a0 * t * mu2 + a1 * mu2 + a2 * t + a3;
        }
        #endregion Catmull-Rom 1D Spline Interpolation

        #region Catmull-Rom 2D Spline Interpolation
        /// <summary>
        /// Set of tests to run testing methods that calculate the angle between Two 3D points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(CatmullRomSplineInterpolation2DTests))]
        public static List<SpeedTester> CatmullRomSplineInterpolation2DTests()
            => new List<SpeedTester>
            {
                new SpeedTester(() => InterpolateCatmullRom(0, 0, 0, 1, 1, 1, 1, 0, 0.5d),
                $"{nameof(Experiments.InterpolateCatmullRom)}(0, 0, 0, 1, 1, 1, 1, 0, 0.5d)"),
                new SpeedTester(() => CatmullRomSpline(0, 0, 0, 1, 1, 1, 1, 0, 0.5d),
                $"{nameof(Experiments.CatmullRomSpline)}(0, 0, 0, 1, 1, 1, 1, 0, 0.5d)")
           };

        /// <summary>
        /// Calculates interpolated point between two points using Catmull-Rom Spline
        /// </summary>
        /// <param name="t0X">First Point</param>
        /// <param name="t0Y">First Point</param>
        /// <param name="p1X">Second Point</param>
        /// <param name="p1Y">Second Point</param>
        /// <param name="p2X">Third Point</param>
        /// <param name="p2Y">Third Point</param>
        /// <param name="t3X">Fourth Point</param>
        /// <param name="t3Y">Fourth Point</param>
        /// <param name="t">
        /// Normalized distance between second and third point
        /// where the spline point will be calculated
        /// </param>
        /// <returns>
        /// Calculated Spline Point
        /// </returns>
        /// <acknowledgment>
        /// Points calculated exist on the spline between points two and three.
        /// From: http://tehc0dez.blogspot.com/2010/04/nice-curves-catmullrom-spline-in-c.html
        /// </acknowledgment>
        public static (double X, double Y) InterpolateCatmullRom(
            double t0X, double t0Y,
            double p1X, double p1Y,
            double p2X, double p2Y,
            double t3X, double t3Y,
            double t)
        {
            var tSquared = t * t;
            var tCubed = tSquared * t;
            return (
                0.5d * (2d * p1X
                + (-t0X + p2X) * t
                + (2d * t0X - 5d * p1X + 4d * p2X - t3X) * tSquared
                + (-t0X + 3d * p1X - 3d * p2X + t3X) * tCubed),
                0.5d * (2d * p1Y
                + (-t0Y + p2Y) * t
                + (2d * t0Y - 5d * p1Y + 4d * p2Y - t3Y) * tSquared
                + (-t0Y + 3d * p1Y - 3d * p2Y + t3Y) * tCubed));
        }

        /// <summary>
        /// The catmull rom spline.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) CatmullRomSpline(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double t)
        {
            var mu2 = t * t;

            var aX0 = -0.5 * x0 + 1.5 * x1 - 1.5 * x2 + 0.5 * x3;
            var aY0 = -0.5 * y0 + 1.5 * y1 - 1.5 * y2 + 0.5 * y3;
            var aX1 = x0 - 2.5 * x1 + 2 * x2 - 0.5 * x3;
            var aY1 = y0 - 2.5 * y1 + 2 * y2 - 0.5 * y3;
            var aX2 = -0.5 * x0 + 0.5 * x2;
            var aY2 = -0.5 * y0 + 0.5 * y2;

            return (
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1);
        }
        #endregion Catmull-Rom 2D Spline Interpolation

        #region Catmull-Rom 3D Spline Interpolation
        /// <summary>
        /// Set of tests to run testing methods that calculate the angle between Two 3D points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(CatmullRomSplineInterpolation3DTests))]
        public static List<SpeedTester> CatmullRomSplineInterpolation3DTests()
            => new List<SpeedTester>
            {
                new SpeedTester(() => CatmullRom(0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0.5d),
                $"{nameof(Experiments.CatmullRom)}(0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0.5d)"),
                new SpeedTester(() => CatmullRomSpline(0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0.5d),
                $"{nameof(Experiments.CatmullRomSpline)}(0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0.5d)")
           };

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="x1">The first position in the interpolation.</param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2">The second position in the interpolation.</param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="x3">The third position in the interpolation.</param>
        /// <param name="y3"></param>
        /// <param name="z3"></param>
        /// <param name="x4">The fourth position in the interpolation.</param>
        /// <param name="y4"></param>
        /// <param name="z4"></param>
        /// <param name="t">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        /// <acknowledgment>
        /// http://www.mvps.org/directx/articles/catmull/
        /// </acknowledgment>
        public static (double X, double Y, double Z) CatmullRom(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double x4, double y4, double z4,
            double t)
        {
            var tSquared = t * t;
            var tCubed = tSquared * t;
            return (
                0.5d * (2d * x2
                + (x3 - x1) * t
                + (2d * x1 - 5d * x2 + 4d * x3 - x4) * tSquared
                + (3d * x2 - x1 - 3d * x3 + x4) * tCubed),
                0.5d * (2d * x2
                + (y3 - y1) * t
                + (2d * y1 - 5d * y2 + 4d * y3 - y4) * tSquared
                + (3d * y2 - y1 - 3d * y3 + y4) * tCubed),
                0.5d * (2d * z2
                + (z3 - z1) * t
                + (2d * z1 - 5d * z2 + 4d * z3 - z4) * tSquared
                + (3d * z2 - z1 - 3d * z3 + z4) * tCubed));
        }

        /// <summary>
        /// The catmull rom spline.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="z0">The z0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="z3">The z3.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) CatmullRomSpline(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double t)
        {
            var mu2 = t * t;

            var aX0 = -0.5 * x0 + 1.5 * x1 - 1.5 * x2 + 0.5 * x3;
            var aY0 = -0.5 * y0 + 1.5 * y1 - 1.5 * y2 + 0.5 * y3;
            var aZ0 = -0.5 * z0 + 1.5 * z1 - 1.5 * z2 + 0.5 * z3;
            var aX1 = x0 - 2.5 * x1 + 2 * x2 - 0.5 * x3;
            var aY1 = y0 - 2.5 * y1 + 2 * y2 - 0.5 * y3;
            var aZ1 = z0 - 2.5 * z1 + 2 * z2 - 0.5 * z3;
            var aX2 = -0.5 * x0 + 0.5 * x2;
            var aY2 = -0.5 * y0 + 0.5 * y2;
            var aZ2 = -0.5 * z0 + 0.5 * z2;

            return (
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1,
                aZ0 * t * mu2 + aZ1 * mu2 + aZ2 * t + z1);
        }
        #endregion Catmull-Rom 3D Spline Interpolation

        #region Change The Angle of a Vector
        /// <summary>
        /// Set of tests to run testing methods that calculate the change of an angle of a vector.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(ChangeAngleofVectorTests))]
        public static List<SpeedTester> ChangeAngleofVectorTests() => new List<SpeedTester> {
                new SpeedTester(() => SetAngle(0, 1, 1),
                $"{nameof(Experiments.SetAngle)}(0, 1, 1)")
           };

        /// <summary>
        /// Set the angle.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        public static (double X, double Y) SetAngle(double i, double j, double angle)
        {
            //double rads = angle; // * (PI / 180); // Original code used degrees rather than radians.
            var dist = Sqrt(i * i + j * j);
            return (
                Sin(angle) * dist,
                -(Cos(angle) * dist));
        }
        #endregion Change The Angle of a Vector

        #region Complex Product of Two 2D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the complex product of Two 2D points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(ComplexProduct2DTests))]
        public static List<SpeedTester> ComplexProduct2DTests() => new List<SpeedTester> {
                new SpeedTester(() => ComplexProduct(0, 0, 1, 1),
                $"{nameof(Experiments.ComplexProduct)}(0, 0, 1, 1)")
           };

        /// <summary>
        /// The complex product.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/1476497/multiply-two-point-objects
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) ComplexProduct(
            double x0, double y0,
            double x1, double y1)
            => (x0 * x1 - y0 * y1, x0 * y1 + y0 * x1);
        #endregion Complex Product of Two 2D Points

        #region Cosine Interpolation of 1D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D Cosine interpolation point.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(CosineInterpolateTests1D))]
        public static List<SpeedTester> CosineInterpolateTests1D() => new List<SpeedTester> {
                new SpeedTester(() => CosineInterpolate1D(0, 1, 0.5d),
                $"{nameof(Experiments.CosineInterpolate1D)}(0, 1, 0.5d)")
            };

        /// <summary>
        /// The cosine interpolate1d.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        public static double CosineInterpolate1D(double v1, double v2, double t)
        {
            var mu2 = (1 - Cos(t * PI)) / 2;
            return v1 * (1 - mu2) + v2 * mu2;
        }
        #endregion Cosine Interpolation of 1D Points

        #region Cosine Interpolation of 2D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D Cosine interpolation point.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(CosineInterpolate2DTests))]
        public static List<SpeedTester> CosineInterpolate2DTests() => new List<SpeedTester> {
                new SpeedTester(() => CosineInterpolate2D(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.CosineInterpolate2D)}(0, 0, 1, 1, 0.5d)")
            };

        /// <summary>
        /// The cosine interpolate2d.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        public static (double X, double Y) CosineInterpolate2D(
            double x1, double y1,
            double x2, double y2,
            double t)
        {
            var mu2 = (1 - Cos(t * PI)) / 2;
            return (
                x1 * (1 - mu2) + x2 * mu2,
                y1 * (1 - mu2) + y2 * mu2);
        }

        /// <summary>
        /// Function For cosine interpolated Line
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public static Point2D Interpolate(Point2D a, Point2D b, double index)
        {
            //Single MU2 = (double)((1.0 - Cos(index * 180)) * 0.5);
            //return Y1 * (1.0 - MU2) + Y2 * MU2;
            var MU2 = (1.0 - Cos(index * 180)) * 0.5;
            return (Point2D)a.Scale(1.0 - MU2).Add(b.Scale(MU2));
        }

        /// <summary>
        /// Function For cosine interpolated Line
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static Point2D CosineInterpolate(Point2D a, Point2D b, double Index)
        {
            var MU = (1 - Cos(Index * 180)) / 2;
            return new Point2D(
                (a.X * (1 - MU)) + (b.X * MU),
                (a.Y * (1 - MU)) + (b.Y * MU)
                );
        }
        #endregion Cosine Interpolation of 2D Points

        #region Cosine Interpolation of 3D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Cosine interpolation point.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(CosineInterpolate3DTests))]
        public static List<SpeedTester> CosineInterpolate3DTests() => new List<SpeedTester> {
                new SpeedTester(() => CosineInterpolate3D(0, 0, 0, 1, 1, 1, 0.5d),
                $"{nameof(Experiments.CosineInterpolate3D)}(0, 0, 0, 1, 1, 1, 0.5d)")
            };

        /// <summary>
        /// The cosine interpolate3d.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        public static (double X, double Y, double Z) CosineInterpolate3D(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            var mu2 = (1 - Cos(t * PI)) / 2;
            return (
                x1 * (1 - mu2) + x2 * mu2,
                y1 * (1 - mu2) + y2 * mu2,
                z1 * (1 - mu2) + z2 * mu2);
        }
        #endregion Cosine Interpolation of 3D Points

        #region Clamp a Value Between a Minimum and a Maximum
        /// <summary>
        /// Set of tests to run testing methods that clamp a number between a minimum, and a maximum.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(ClampTests))]
        public static List<SpeedTester> ClampTests() => new List<SpeedTester> {
                new SpeedTester(() => Clamp0(2, 0, 1),
                $"{nameof(Experiments.Clamp0)}(2, 0, 1)"),
                new SpeedTester(() => Clamp1(2, 0, 1),
                $"{nameof(Experiments.Clamp1)}(2, 0, 1)")
            };

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp0(double value, double min, double max)
            => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp1(double value, double min, double max)
            => Max(min, Min(value, max));
        #endregion Clamp a Value Between a Minimum and a Maximum

        #region Closest Point On Line
        /// <summary>
        /// Set of tests to run testing methods that calculate the cross product of three 2D points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(NearestPointOnLineSegmentTests))]
        public static List<SpeedTester> NearestPointOnLineSegmentTests() => new List<SpeedTester> {
                new SpeedTester(() => ClosestPointOnLineSegmentMvG(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.ClosestPointOnLineSegmentMvG)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => ClosestPointOnLineSegmentDarienPardinas(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.ClosestPointOnLineSegmentDarienPardinas)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => ClosestPointOnLineDarienPardinas(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.ClosestPointOnLineDarienPardinas)}(0, 0, 1, 0, 1, 1)")
            };

        /// <summary>
        /// The closest point on line segment mv g.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="pX">The pX.</param>
        /// <param name="pY">The pY.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line
        /// </acknowledgment>
        public static Point2D ClosestPointOnLineSegmentMvG(
            double aX, double aY,
            double bX, double bY,
            double pX, double pY)
        {
            // Vector A->B
            var diffAB = new Point2D(aX - bX, aY - bY);
            var det = aY * bX - aX * bY;
            var dot = diffAB.X * pX + diffAB.Y * pY;
            var val = new Point2D(dot * diffAB.X + det * diffAB.Y, dot * diffAB.Y - det * diffAB.X);
            var magnitude = diffAB.X * diffAB.X + diffAB.Y * diffAB.Y;
            var inverseDist = 1 / magnitude;
            return new Point2D(val.X * inverseDist, val.Y * inverseDist);
        }

        /// <summary>
        /// The closest point on line segment darien pardinas.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="pX">The pX.</param>
        /// <param name="pY">The pY.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line</acknowledgment>
        public static Point2D ClosestPointOnLineSegmentDarienPardinas(
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
            {
                return new Point2D(aX, aY);
            }
            else if (dist > dotABAP)
            {
                return new Point2D(bX, bY);
            }
            else
            {
                return new Point2D(aX + diffAB.X * dist, aY + diffAB.Y * dist);
            }
        }

        /// <summary>
        /// The closest point on line darien pardinas.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="pX">The pX.</param>
        /// <param name="pY">The pY.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line
        /// </acknowledgment>
        public static Point2D ClosestPointOnLineDarienPardinas(
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
            // The normalized "distance" from a to the closest point
            var dist = dotABAP / dotAB;
            return new Point2D(aX + diffAB.X * dist, aY + diffAB.Y * dist);
        }
        #endregion Closest Point On Line

        #region Cross Product of Two 2D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the cross product of two 2D points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(CrossProduct2Points2DTests))]
        public static List<SpeedTester> CrossProduct2Points2DTests() => new List<SpeedTester> {
                new SpeedTester(() => CrossProduct2Points2D_0(0, 0, 1, 0),
                $"{nameof(Experiments.CrossProduct2Points2D_0)}(0, 0, 1, 0)")
            };

        /// <summary>
        /// Cross Product of two points.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <returns>the cross product AB · BC.</returns>
        /// <acknowledgment>
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct2Points2D_0(
            double x1, double y1,
            double x2, double y2)
            => (x1 * y2) - (y1 * x2);
        #endregion Cross Product of Two 2D Points

        #region Cross Product of Two 3D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the cross product of two 2D points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(CrossProduct2Points3DTests))]
        public static List<SpeedTester> CrossProduct2Points3DTests() => new List<SpeedTester> {
                new SpeedTester(() => CrossProduct2Points3D_0(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.CrossProduct2Points3D_0)}(0, 0, 0, 1, 1, 1)")
            };

        /// <summary>
        /// Cross Product of two points.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="z1"></param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2"></param>
        /// <returns>the cross product AB · BC.</returns>
        /// <acknowledgment>
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) CrossProduct2Points3D_0(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (
                    (y1 * z2) - (z1 * y2), // X
                    (z1 * x2) - (x1 * z2), // Y
                    (x1 * y2) - (y1 * x2)  // Z
                );
        #endregion Cross Product of Two 3D Points

        #region Cubic Bézier Get T
        /// <summary>
        /// Find the tfor coordinate.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="Lut">The Lut.</param>
        /// <returns>The <see cref="T:List{double}"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/27053888/how-to-get-time-value-from-bezier-curve-given-length/27071218#27071218
        /// </acknowledgment>
        public static List<double> FindTforCoordinate(Point2D value, List<Point2D> Lut)
        {
            var point = new Point2D();
            var found = new List<double>();
            var len = Lut.Count;
            for (var i = 0; i < len; i++)
            {
                point.X = Lut[i].X;
                point.Y = Lut[i].Y;
                if (Abs(value.X - point.X) < DoubleEpsilon && Abs(value.Y - point.Y) < DoubleEpsilon)
                {
                    found.Add(i / len);
                }
            }
            return found;
        }

        /// <summary>
        /// Build the LUT.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/27053888/how-to-get-time-value-from-bezier-curve-given-length/27071218#27071218
        /// </acknowledgment>
        public static List<Point2D> BuildLUT(Point2D a, Point2D b, Point2D c, Point2D d)
        {
            var Lut = new List<Point2D>(100);
            for (double t = 0; t <= 1; t += 0.01)
            {
                Lut[(int)(t * 100)] = new Point2D(Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, t));
            }

            return Lut;
        }
        #endregion Cubic Bézier Get T

        #region Cubic Bézier Length Approximations
        /// <summary>
        /// The cubic bezier arc length.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <param name="p4">The p4.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://steve.hollasch.net/cgindex/curves/cbezarclen.html
        /// </acknowledgment>
        public static double CubicBezierArcLength(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
        {
            var k1 = (Point2D)(-p1 + 3 * (p2 - p3) + p4);
            var k2 = 3 * (p1 + p3) - 6 * p2;
            var k3 = (Point2D)(3 * (p2 - p1));
            var k4 = p1;

            var q1 = 9.0 * (Sqrt(Abs(k1.X)) + Sqrt(Abs(k1.Y)));
            var q2 = 12.0 * (k1.X * k2.X + k1.Y * k2.Y);
            var q3 = 3.0 * (k1.X * k3.X + k1.Y * k3.Y) + 4.0 * (Sqrt(Abs(k2.X)) + Sqrt(Abs(k2.Y)));
            var q4 = 4.0 * (k2.X * k3.X + k2.Y * k3.Y);
            var q5 = Sqrt(Abs(k3.X)) + Sqrt(Abs(k3.Y));

            // Approximation algorithm based on Simpson.
            const double a = 0;
            const double b = 1;
            const int n_limit = 1024;
            const double TOLERANCE = 0.001;

            var n = 1;

            var multiplier = (b - a) / 6.0;
            var endsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a) + CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, b);
            var interval = (b - a) / 2.0;
            double asum = 0;
            var bsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a + interval);
            var est1 = multiplier * (endsum + 2 * asum + 4 * bsum);
            var est0 = 2 * est1;

            while (n < n_limit && Abs(est1) > 0 && Abs((est1 - est0) / est1) > TOLERANCE)
            {
                n *= 2;
                multiplier /= 2;
                interval /= 2;
                asum += bsum;
                bsum = 0;
                est0 = est1;
                var interval_div_2n = interval / (2.0 * n);

                for (var i = 1; i < 2 * n; i += 2)
                {
                    var t = a + i * interval_div_2n;
                    bsum += CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, t);
                }

                est1 = multiplier * (endsum + 2 * asum + 4 * bsum);
            }

            return est1 * 10;
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
        /// <acknowledgment>
        /// http://steve.hollasch.net/cgindex/curves/cbezarclen.html
        /// </acknowledgment>
        public static double CubicBezierArcLengthHelper(ref double q1, ref double q2, ref double q3, ref double q4, ref double q5, double t)
        {
            var result = q5 + t * (q4 + t * (q3 + t * (q2 + t * q1)));
            result = Sqrt(Abs(result));
            return result;
        }

        /// <summary>
        /// Approximate length of the Bézier curve which starts at "start" and
        /// is defined by "c". According to Computing the Arc Length of Cubic Bezier Curves
        /// there is no closed form integral for it.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://www.lemoda.net/maths/bezier-length/index.html
        /// </acknowledgment>
        public static double CubicBezierLength(Point2D p1, Point2D p2, Point2D p3, Point2D p4, int steps = 10)
        {
            double t;
            Point2D dot;
            var previous_dot = new Point2D();
            var length = 0.0;
            for (var i = 0; i <= steps; i++)
            {
                t = (double)i / steps;
                dot = new Point2D(Interpolators.CubicBezier(p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y, p4.X, p4.Y, t));
                if (i > 0)
                {
                    var x_diff = dot.X - previous_dot.X;
                    var y_diff = dot.Y - previous_dot.Y;
                    length += Sqrt(x_diff * x_diff + y_diff * y_diff);
                }
                previous_dot = dot;
            }
            return length;
        }
        #endregion Cubic Bézier Length Approximations

        #region Cubic CatmulRom Spline Interpolation of 1D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D Catmull Rom Spline interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(CubicInterpolateCatmullRomSplines1DTests))]
        public static List<SpeedTester> CubicInterpolateCatmullRomSplines1DTests() => new List<SpeedTester> {
                new SpeedTester(() => CubicInterpolateCatmullRomSplines1D(0, 1, 2, 3, 0.5d),
                $"{nameof(Experiments.CubicInterpolateCatmullRomSplines1D)}(0, 1, 2, 3, 0.5d)")
            };

        /// <summary>
        /// The cubic interpolate catmull rom splines1d.
        /// </summary>
        /// <param name="v0">The v0.</param>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <param name="v3">The v3.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        public static double CubicInterpolateCatmullRomSplines1D(double v0, double v1, double v2, double v3, double t)
        {
            double a0, a1, a2, a3, mu2;

            mu2 = t * t;
            a0 = -0.5 * v0 + 1.5 * v1 - 1.5 * v2 + 0.5 * v3;
            a1 = v0 - 2.5 * v1 + 2 * v2 - 0.5 * v3;
            a2 = -0.5 * v0 + 0.5 * v2;
            a3 = v1;

            return a0 * t * mu2 + a1 * mu2 + a2 * t + a3;
        }
        #endregion Cubic CatmulRom Spline Interpolation of 1D Points

        #region Cubic CatmulRom Spline Interpolation of 2D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D Catmull Rom Spline interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(CubicInterpolateCatmullRomSplines2DTests))]
        public static List<SpeedTester> CubicInterpolateCatmullRomSplines2DTests() => new List<SpeedTester> {
                new SpeedTester(() => CubicInterpolateCatmullRomSplines2D(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicInterpolateCatmullRomSplines2D)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)")
            };

        /// <summary>
        /// The cubic interpolate catmull rom splines2d.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        public static (double X, double Y) CubicInterpolateCatmullRomSplines2D(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double t)
        {
            var mu2 = t * t;

            var aX0 = -0.5 * x0 + 1.5 * x1 - 1.5 * x2 + 0.5 * x3;
            var aY0 = -0.5 * y0 + 1.5 * y1 - 1.5 * y2 + 0.5 * y3;
            var aX1 = x0 - 2.5 * x1 + 2 * x2 - 0.5 * x3;
            var aY1 = y0 - 2.5 * y1 + 2 * y2 - 0.5 * y3;
            var aX2 = -0.5 * x0 + 0.5 * x2;
            var aY2 = -0.5 * y0 + 0.5 * y2;

            return (
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1);
        }
        #endregion Cubic CatmulRom Spline Interpolation of 2D Points

        #region Cubic CatmulRom Spline Interpolation of 3D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Catmull Rom Spline interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(CubicInterpolateCatmullRomSplines3DTests))]
        public static List<SpeedTester> CubicInterpolateCatmullRomSplines3DTests() => new List<SpeedTester> {
                new SpeedTester(() => CubicInterpolateCatmullRomSplines3D(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d),
                $"{nameof(Experiments.CubicInterpolateCatmullRomSplines3D)}(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d)")
            };

        /// <summary>
        /// The cubic interpolate catmull rom splines3d.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="z0">The z0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="z3">The z3.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        public static (double X, double Y, double Z) CubicInterpolateCatmullRomSplines3D(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double t)
        {
            var mu2 = t * t;

            var aX0 = -0.5 * x0 + 1.5 * x1 - 1.5 * x2 + 0.5 * x3;
            var aY0 = -0.5 * y0 + 1.5 * y1 - 1.5 * y2 + 0.5 * y3;
            var aZ0 = -0.5 * z0 + 1.5 * z1 - 1.5 * z2 + 0.5 * z3;
            var aX1 = x0 - 2.5 * x1 + 2 * x2 - 0.5 * x3;
            var aY1 = y0 - 2.5 * y1 + 2 * y2 - 0.5 * y3;
            var aZ1 = z0 - 2.5 * z1 + 2 * z2 - 0.5 * z3;
            var aX2 = -0.5 * x0 + 0.5 * x2;
            var aY2 = -0.5 * y0 + 0.5 * y2;
            var aZ2 = -0.5 * z0 + 0.5 * z2;

            return (
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1,
                aZ0 * t * mu2 + aZ1 * mu2 + aZ2 * t + z1);
        }
        #endregion Cubic CatmulRom Spline Interpolation of 3D Points

        #region Cubic Bézier Derivative
        /// <summary>
        /// The cubic bezier derivative0.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="PointF"/>.</returns>
        /// <acknowledgment>
        /// http://www.cs.mtu.edu/~shene/COURSES/cs3621/NOTES/spline/Bezier/bezier-der.html
        /// </acknowledgment>
        public static PointF CubicBezierDerivative0(PointF p0, PointF p1, PointF p2, PointF p3, double t) => new PointF((float)(3 * Pow(1 - t, 2) * (p1.X - p0.X) + 6 * (1 - t) * t * (p2.X - p1.X) + 3 * Pow(t, 2) * (p3.X - p2.X)),
                              (float)(3 * Pow(1 - t, 2) * (p1.Y - p0.Y) + 6 * (1 - t) * t * (p2.Y - p1.Y) + 3 * Pow(t, 2) * (p3.Y - p2.Y)));

        /// <summary>
        /// The cubic bezier derivative1.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="PointF"/>.</returns>
        /// <acknowledgment>
        /// http://www.cs.mtu.edu/~shene/COURSES/cs3621/NOTES/spline/Bezier/bezier-der.html
        /// </acknowledgment>
        public static PointF CubicBezierDerivative1(PointF p0, PointF p1, PointF p2, PointF p3, double t)
        {
            var mu1 = 1 - t;
            var mu12 = mu1 * mu1;
            var mu2 = t * t;

            return new PointF(
                (float)(3 * mu12 * (p1.X - p0.X) + 6 * mu1 * t * (p2.X - p1.X) + 3 * mu2 * (p3.X - p2.X)),
                (float)(3 * mu12 * (p1.Y - p0.Y) + 6 * mu1 * t * (p2.Y - p1.Y) + 3 * mu2 * (p3.Y - p2.Y))
                );
        }
        #endregion Cubic Bézier Derivative

        #region Cubic Bézier Self Intersection
        /// <summary>
        /// The cubic bezier self intersection x.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <returns>returns null if the curve is self-intersecting, or the point of intersection if it is.</returns>
        public static Point2D? CubicBezierSelfIntersectionX(double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3)
            => CubicBezierSelfIntersection(
                CubicBezierCoefficients(x0, x1, x2, x3),
                CubicBezierCoefficients(y0, y1, y2, y3));

        /// <summary>
        /// https://groups.google.com/d/msg/comp.graphics.algorithms/SRm97nRWlw4/R1Rn38ep8n0J
        /// </summary>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <returns></returns>
        public static Point2D? CubicBezierSelfIntersection(Polynomial xCurve, Polynomial yCurve)
        {
            (var a, var b) = (xCurve[0] == 0d) ? (xCurve[1], xCurve[2]) : (xCurve[1] / xCurve[0], xCurve[2] / xCurve[0]);
            (var p, var q) = (yCurve[0] == 0d) ? (yCurve[1], yCurve[2]) : (yCurve[1] / yCurve[0], yCurve[2] / yCurve[0]);

            if (a == p || q == b)
            {
                return null;
            }

            var k = (q - b) / (a - p);

            var poly = new Polynomial(
                2,
                -3 * k,
                3 * k * k + 2 * k * a + 2 * b,
                -k * k * k - a * k * k - b * k);

            var r = poly.Roots().OrderByDescending(c => c).ToArray();
            if (r.Length != 3)
            {
                return null;
            }

            if (r[0] >= 0.0 && r[0] <= 1.0 && r[2] >= 0.0 && r[2] <= 1.0)
            {
                var s = r[0];
                return new Point2D(
                    xCurve[0] * s * s * s + xCurve[1] * s * s + xCurve[2] * s + xCurve[3],
                    yCurve[0] * s * s * s + yCurve[1] * s * s + yCurve[2] * s + yCurve[3]);
            }

            return null;
        }

        /// <summary>
        /// https://groups.google.com/d/msg/comp.graphics.algorithms/SRm97nRWlw4/R1Rn38ep8n0J
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns>returns null if the curve is self-intersecting, or the point of intersection if it is.</returns>
        public static Point2D? CubicBezierSelfIntersection(double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var (xCurveA, xCurveB, xCurveC, xCurveD) = CubicBezierCoefficients(x0, x1, x2, x3);
            (var a, var b) = (xCurveD == 0d) ? (xCurveC, xCurveB) : (xCurveC / xCurveD, xCurveB / xCurveD);
            var (yCurveA, yCurveB, yCurveC, yCurveD) = CubicBezierCoefficients(y0, y1, y2, y3);
            (var p, var q) = (yCurveD == 0d) ? (yCurveC, yCurveB) : (yCurveC / yCurveD, yCurveB / yCurveD);

            if (a == p || q == b)
            {
                return null;
            }

            var k = (q - b) / (a - p);

            var poly = new double[]
            {
                -k * k * k - a * k * k - b * k,
                3 * k * k + 2 * k * a + 2 * b,
                -3 * k,
                2
            };

            var roots = CubicRoots(poly[3], poly[2], poly[1], poly[0])
                .OrderByDescending(c => c).ToArray();
            if (roots.Length != 3)
            {
                return null;
            }

            if (roots[0] >= 0d && roots[0] <= 1d && roots[2] >= 0d && roots[2] <= 1d)
            {
                return Interpolators.CubicBezier(x0, y0, x1, y1, x2, y2, x3, y3, roots[0]);
            }

            return null;
        }
        #endregion Cubic Bézier Self Intersection

        #region Cubic BSpline Interpolation
        /// <summary>
        /// Function to Interpolate a Cubic Bezier Spline
        /// </summary>
        /// <param name="points"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Point2D InterpolateBSpline(List<Point2D> points, double index)
        {
            if (points.Count >= 4)
            {
                var VPoints = new List<Point2D>(4);

                const int A = 0;
                const int B = 1;
                const int C = 2;
                const int D = 3;

                VPoints.Add(new Point2D(
                    points[D].X - points[C].X - (points[A].X - points[B].X),
                    points[D].Y - points[C].Y - (points[A].Y - points[B].Y)
                    ));

                VPoints.Add(new Point2D(
                    points[A].X - points[B].X - VPoints[A].X,
                    points[A].Y - points[B].Y - VPoints[A].Y
                    ));

                VPoints.Add(new Point2D(
                    points[C].X - points[A].X,
                    points[C].Y - points[A].Y
                    ));

                VPoints.Add(points[1]);

                return new Point2D(
                    VPoints[0].X * index * index * index + VPoints[1].X * index * index * index + VPoints[2].X * index + VPoints[3].X,
                    VPoints[0].Y * index * index * index + VPoints[1].Y * index * index * index + VPoints[2].Y * index + VPoints[3].Y
                );
            }

            return Point2D.Empty;
        }

        /// <summary>
        /// General Bézier curve Number of control points is n+1 0 less than or equal to mu less than 1
        /// IMPORTANT, the last point is not computed.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Point2D Interpolate(List<Point2D> points, double index)
        {
            var n = points.Count - 1;
            int kn;
            int nn;
            int nkn;

            double blend;
            double muk = 1;
            var munk = Pow(1 - index, n);

            var b = new Point2D(0.0f, 0.0f);

            for (var k = 0; k <= n; k++)
            {
                nn = n;
                kn = k;
                nkn = n - k;
                blend = muk * munk;
                muk *= index;
                munk /= 1 - index;
                while (nn >= 1)
                {
                    blend *= nn;
                    nn--;
                    if (kn > 1)
                    {
                        blend /= kn;
                        kn--;
                    }
                    if (nkn > 1)
                    {
                        blend /= nkn;
                        nkn--;
                    }
                }

                b = new Point2D(
                b.X + points[k].X * blend,
                b.Y + points[k].Y * blend
                    );
            }

            return b;
        }

        /// <summary>
        /// Function to Interpolate a Cubic Bezier Spline
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static Point2D InterpolateCubicBSplinePoint(Point2D[] Points, double Index)
        {
            const int A = 0;
            const int B = 1;
            const int C = 2;
            const int D = 3;
            var V1 = Index;
            var VPoints = new Point2D[4];

            VPoints[0] = new Point2D(
                Points[D].X - Points[C].X - (Points[A].X - Points[B].X),
                Points[D].Y - Points[C].Y - (Points[A].Y - Points[B].Y)
                );

            VPoints[1] = new Point2D(
                Points[A].X - Points[B].X - VPoints[A].X,
                Points[A].Y - Points[B].Y - VPoints[A].Y
                );

            VPoints[2] = new Point2D(
                Points[C].X - Points[A].X,
                Points[C].Y - Points[A].Y
                );

            VPoints[3] = Points[1];

            return new Point2D(
                VPoints[0].X * V1 * V1 * V1 + VPoints[1].X * V1 * V1 * V1 + VPoints[2].X * V1 + VPoints[3].X,
                VPoints[0].Y * V1 * V1 * V1 + VPoints[1].Y * V1 * V1 * V1 + VPoints[2].Y * V1 + VPoints[3].Y
            );
        }
        #endregion Cubic BSpline Interpolation

        #region Distance Between Two 3D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the distance between two 3D points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(Distance3DTests))]
        public static List<SpeedTester> Distance3DTests() => new List<SpeedTester> {
                new SpeedTester(() => Distance3D_0(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.Distance3D_0)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => Distance3D_1(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.Distance3D_1)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => Distance3D_2(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.Distance3D_2)}(0, 0, 1, 0, 1, 1)")
            };

        /// <summary>
        /// Distance between two 3D points.
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
        public static double Distance3D_0(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => Sqrt((x2 - x1) * (x2 - x1)
                + (y2 - y1) * (y2 - y1)
                + (z2 - z1) * (z2 - z1));

        /// <summary>
        /// Distance between two 3D points.
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
        public static double Distance3D_1(
        double x1, double y1, double z1,
        double x2, double y2, double z2) => Sqrt((x2 - x1) * (x2 - x1)
        + (y2 - y1) * (y2 - y1)
        + (z2 - z1) * (z2 - z1));

        /// <summary>
        /// Distance between two 3D points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="z1">First Z component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <param name="z2">Second Z component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        public static double Distance3D_2(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
        {
            var x = x2 - x1;
            var y = y2 - y1;
            var z = z2 - z1;
            return Sqrt(x * x + y * y + z * z);
        }
        #endregion Distance Between Two 3D Points

        #region Distance of Point To Line Segment
        /// <summary>
        /// Calculate the distance between the point and the segment.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="RetNear"></param>
        /// <returns></returns>
        public static double DistanceToSegment(Point2D p, Point2D A, Point2D B, out Point2D RetNear)
        {
            RetNear = new Point2D();
            var Delta = new Point2D(B.X - A.X, B.Y - A.Y);
            if ((Abs(Delta.X) < DoubleEpsilon) && (Abs(Delta.Y) < DoubleEpsilon))
            {
                //  It's a point not a line segment.
                Delta.X = p.X - A.X;
                Delta.Y = p.Y - A.Y;
                RetNear.X = A.X;
                RetNear.Y = A.Y;
                return Sqrt((Delta.X * Delta.X) + (Delta.Y * Delta.Y));
            }
            //  Calculate the t that minimizes the distance.
            var t = (((p.X - A.X) * Delta.X) + ((p.Y - A.Y) * Delta.Y)) / ((Delta.X * Delta.X) + (Delta.Y * Delta.Y));
            if (t < 0)
            {
                Delta.X = p.X - A.X;
                Delta.Y = p.Y - A.Y;
                RetNear.X = A.X;
                RetNear.Y = A.Y;
            }
            else if (t > 1)
            {
                Delta.X = p.X - B.X;
                Delta.Y = p.Y - B.Y;
                RetNear.X = B.X;
                RetNear.Y = B.Y;
            }
            else
            {
                RetNear.X = A.X + (t * Delta.X);
                RetNear.Y = A.Y + (t * Delta.Y);
                Delta.X = p.X - RetNear.X;
                Delta.Y = p.Y - RetNear.Y;
            }
            return Sqrt((Delta.X * Delta.X) + (Delta.Y * Delta.Y));
        }

        /// <summary>
        /// The dist to segment2.
        /// </summary>
        /// <param name="px">The px.</param>
        /// <param name="py">The py.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double DistToSegment2(double px, double py, double x1, double y1, double x2, double y2)
        {
            double dx;
            double dy;
            double t;
            dx = x2 - x1;
            dy = y2 - y1;
            if ((Abs(dx) < DoubleEpsilon) && (Abs(dy) < DoubleEpsilon))
            {
                //  It's a point not a line segment.
                dx = px - x1;
                dy = py - y1;
                return Sqrt((dx * dx) + (dy * dy));
            }
            t = (px + (py - (x1 - y1))) / (dx + dy);
            if (t < 0)
            {
                dx = px - x1;
                dy = py - y1;
            }
            else if (t > 1)
            {
                dx = px - x2;
                dy = py - y2;
            }
            else
            {
                var x3 = x1 + (t * dx);
                var y3 = y1 + (t * dy);
                dx = px - x3;
                dy = py - y3;
            }
            return Sqrt((dx * dx) + (dy * dy));
        }

        /// <summary>
        /// Calculate the distance between the point and the segment.
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double DistToSegment(double px, double py, double x1, double y1, double x2, double y2)
        {
            var dx = x2 - x1;
            var dy = y2 - y1;
            if ((Abs(dx) < DoubleEpsilon) && (Abs(dy) < DoubleEpsilon))
            {
                //  It's a point not a line segment.
                dx = px - x1;
                dy = py - y1;
                return Sqrt((dx * dx) + (dy * dy));
            }
            var t = (px + (py - (x1 - y1))) / (dx + dy);
            if (t < 0)
            {
                dx = px - x1;
                dy = py - y1;
            }
            else if (t > 1)
            {
                dx = px - x2;
                dy = py - y2;
            }
            else
            {
                var x3 = x1 + (t * dx);
                var y3 = y1 + (t * dy);
                dx = px - x3;
                dy = py - y3;
            }
            return Sqrt((dx * dx) + (dy * dy));
        }

        /// <summary>
        /// The dist from seg.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="q0">The q0.</param>
        /// <param name="q1">The q1.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="u">The u.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <exception cref="Exception">Expected line segment, not point.</exception>
        /// <acknowledgment>
        /// From: http://stackoverflow.com/questions/2255842/detecting-coincident-subset-of-two-coincident-line-segments/2255848
        /// </acknowledgment>
        public static double DistFromSeg(Point2D p, Point2D q0, Point2D q1, double radius, ref double u)
        {
            // formula here:
            // http://mathworld.wolfram.com/Point-LineDistance2-Dimensional.html
            // where x0,y0 = p
            //       x1,y1 = q0
            //       x2,y2 = q1
            var dx21 = q1.X - q0.X;
            var dy21 = q1.Y - q0.Y;
            var dx10 = q0.X - p.X;
            var dy10 = q0.Y - p.Y;
            var segLength = Sqrt(dx21 * dx21 + dy21 * dy21);
            if (segLength < Epsilon)
            {
                throw new Exception("Expected line segment, not point.");
            }

            var num = Abs(dx21 * dy10 - dx10 * dy21);
            var d = num / segLength;
            return d;
        }
        #endregion Distance of Point To Line Segment

        #region Elliptic Star Points
        /// <summary>
        /// The star points.
        /// </summary>
        /// <param name="num_points">The num_points.</param>
        /// <param name="bounds">The bounds.</param>
        /// <returns>The <see cref="T:PointF[]"/>. Return PointFs to define a star.</returns>
        public static PointF[] StarPoints(int num_points, Rectangle bounds)
        {
            // Make room for the points.
            var pts = new PointF[num_points];

            double rx = bounds.Width / 2;
            double ry = bounds.Height / 2;
            var cx = bounds.X + rx;
            var cy = bounds.Y + ry;

            // Start at the top.
            var theta = -PI / 2;
            var dtheta = 4 * PI / num_points;
            for (var i = 0; i < num_points; i++)
            {
                pts[i] = new PointF(
                    (float)(cx + rx * Cos(theta)),
                    (float)(cy + ry * Sin(theta)));
                theta += dtheta;
            }

            return pts;
        }
        #endregion Elliptic Star Points

        #region Evaluate a Polynomial
        /// <summary>
        /// Set of tests to run testing methods that evaluates a polynomial.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(EvaluatePolynomialTests))]
        public static List<SpeedTester> EvaluatePolynomialTests() => new List<SpeedTester> {
                new SpeedTester(() => Evaluate(new double[] { 100d, 200d, -200d }, 0.5),
                $"{nameof(Experiments.Evaluate)}(( 100d, 200d, -200d ), 0.5)"),
                new SpeedTester(() => Horner(new double[] { 100d, 200d, -200d }, 0.5),
                $"{nameof(Experiments.Horner)}(( 100d, 200d, -200d ), 0.5)"),
                new SpeedTester(() => Compute(new double[] { 100d, 200d, -200d }, 0.5),
                $"{nameof(Experiments.Compute)}(( 100d, 200d, -200d ), 0.5)"),
                new SpeedTester(() => ComputeC(new double[] { 100d, 200d, -200d }, new Complex(0.5, 0)),
                $"{nameof(Experiments.Compute)}(( 100d, 200d, -200d ), new Complex(0.5, 0))"),
                new SpeedTester(() => ComputeC2(new double[] { 100d, 200d, -200d }, new Complex(0.5, 0)),
                $"{nameof(Experiments.ComputeC2)}(( 100d, 200d, -200d ), new Complex(0.5, 0))"),
           };

        /// <summary>
        /// An implementation of Horner's Evaluate method.
        /// </summary>
        /// <param name="coefficients"></param>
        /// <param name="x">The value to evaluate.</param>
        /// <returns></returns>

        /// <acknowledgment>
        /// https://en.wikipedia.org/wiki/Horner%27s_method
        /// https://github.com/thelonious/kld-polynomial
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Evaluate(double[] coefficients, double x)
        {
            if (double.IsNaN(x))
            {
                throw new ArithmeticException($"{nameof(Evaluate)}: parameter {nameof(x)} must be a number");
            }

            var degree = coefficients.Length - 1;
            var result = 0d;

            for (var i = degree; i >= 0; i--)
            {
                result = result * x + coefficients[i];
            }

            return result;
        }

        /// <summary>
        /// The horner.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        /// <param name="x">The x.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <exception cref="ArithmeticException"></exception>
        /// <acknowledgment>
        /// http://rosettacode.org/wiki/Horner%27s_rule_for_polynomial_evaluation
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Horner(double[] coefficients, double x)
        {
            if (double.IsNaN(x))
            {
                throw new ArithmeticException($"{nameof(Horner)}: parameter {nameof(x)} must be a number");
            }

            return coefficients.Reverse().Aggregate(
                    (accumulator, coefficient) => accumulator * x + coefficient);
        }

        /// <summary>
        /// The compute.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        /// <param name="x">The x.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <exception cref="ArithmeticException"></exception>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Compute(double[] coefficients, double x)
        {
            if (double.IsNaN(x))
            {
                throw new ArithmeticException($"{nameof(Compute)}: parameter {nameof(x)} must be a number");
            }

            var degree = coefficients.Length - 1;
            var result = 0d;
            var ncoef = 1d;
            for (var i = 0; i <= degree; i++)
            {
                result += coefficients[i] * ncoef;
                ncoef *= x;
            }

            return result;
        }

        /// <summary>
        /// The compute c.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        /// <param name="x">The x.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex ComputeC(double[] coefficients, Complex x)
        {
            var degree = coefficients.Length - 1;
            var result = Complex.Zero;
            var ncoef = Complex.One;

            for (var i = 0; i <= degree; i++)
            {
                result += coefficients[i] * ncoef;
                ncoef *= x;
            }

            return result;
        }

        /// <summary>
        /// The compute c2.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        /// <param name="x">The x.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        /// <acknowledgment>
        /// https://en.wikipedia.org/wiki/Horner%27s_method
        /// https://github.com/superlloyd/Poly
        /// https://github.com/thelonious/kld-polynomial
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex ComputeC2(double[] coefficients, Complex x)
        {
            var degree = coefficients.Length - 1;
            var result = Complex.Zero;

            for (var i = degree; i >= 0; i--)
            {
                result = result * x + coefficients[i];
            }

            return result;
        }
        #endregion Evaluate a Polynomial

        #region Find Polygon Ear
        /// <summary>
        /// Find the indexes of three points that form an "ear."
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// </acknowledgment>
        public static Triangle FindEar(PolygonContour polygon, ref int a, ref int b, ref int c)
        {
            var num_points = polygon.Points.Count;

            for (a = 0; a < num_points; a++)
            {
                b = (a + 1) % num_points;
                c = (b + 1) % num_points;

                if (FormsEar(polygon, a, b, c))
                {
                    return new Triangle(polygon.Points[a], polygon.Points[b], polygon.Points[c]);
                }
            }

            // We should never get here because there should
            // always be at least two ears.
            Debug.Assert(false);

            return null;
        }

        /// <summary>
        /// The forms ear.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <returns>The <see cref="bool"/>. Return true if the three points form an ear.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// </acknowledgment>
        public static bool FormsEar(PolygonContour polygon, int a, int b, int c)
        {
            // See if the angle ABC is concave.
            if (AngleVector(
                polygon.Points[a].X, polygon.Points[a].Y,
                polygon.Points[b].X, polygon.Points[b].Y,
                polygon.Points[c].X, polygon.Points[c].Y) > 0)
            {
                // This is a concave corner so the triangle
                // cannot be an ear.
                return false;
            }

            // Make the triangle A, B, C.
            var triangle = new Triangle(polygon.Points[a], polygon.Points[b], polygon.Points[c]);

            // Check the other points to see
            // if they lie in triangle A, B, C.
            for (var i = 0; i < polygon.Points.Count; i++)
            {
                if ((i != a) && (i != b) && (i != c) && triangle.Contains(polygon.Points[i]))
                {
                    // This point is in the triangle
                    // do this is not an ear.
                    return false;
                }
            }

            // This is an ear.
            return true;
        }
        #endregion Find Polygon Ear

        #region Fit in Rectangle
        /// <summary>
        /// The fit rect.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="radians">The radians.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        public static Size2D FitRect(Size2D size, double radians)
        {
            var angleCos = Cos(radians);
            var angleSin = Sin(radians);

            var x1 = -size.Width * 0.5f;
            var x2 = size.Width * 0.5f;
            var x3 = size.Width * 0.5f;
            var x4 = -size.Width * 0.5f;

            var y1 = size.Height * 0.5f;
            var y2 = size.Height * 0.5f;
            var y3 = -size.Height * 0.5f;
            var y4 = -size.Height * 0.5f;

            var x11 = (x1 * angleCos) + (y1 * angleSin);
            var y11 = (-x1 * angleSin) + (y1 * angleCos);

            var x21 = (x2 * angleCos) + (y2 * angleSin);
            var y21 = (-x2 * angleSin) + (y2 * angleCos);

            var x31 = (x3 * angleCos) + (y3 * angleSin);
            var y31 = (-x3 * angleSin) + (y3 * angleCos);

            var x41 = (x4 * angleCos) + (y4 * angleSin);
            var y41 = (-x4 * angleSin) + (y4 * angleCos);

            var x_min = Min(Min(x11, x21), Min(x31, x41));
            var x_max = Max(Max(x11, x21), Max(x31, x41));

            var y_min = Min(Min(y11, y21), Min(y31, y41));
            var y_max = Max(Max(y11, y21), Max(y31, y41));

            return new Size2D(x_max - x_min, y_max - y_min);
        }
        #endregion Fit in Rectangle

        #region Gear Points
        // Draw the gear.
        /// <summary>
        /// The pic gears paint.
        /// </summary>
        /// <param name="e">The paint event arguments.</param>
        /// <param name="bounds">The bounds.</param>
        public static void PicGears_Paint(PaintEventArgs e, Rectangle bounds)
        {
            // Draw smoothly.
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            const double radius = 50;
            const double tooth_length = 10;
            var x = bounds.Width / 2 - radius - tooth_length - 1;
            double y = bounds.Height / 3;
            DrawGear(e.Graphics, Brushes.Black, Brushes.LightBlue, Pens.Blue, new Point2D(x, y),
                radius, tooth_length, 10, 5, true);

            x += 2 * radius + tooth_length + 2;
            DrawGear(e.Graphics, Brushes.Black, Brushes.LightGreen, Pens.Green, new Point2D(x, y),
                radius, tooth_length, 10, 5, false);

            y += 2 * radius + tooth_length + 2;
            DrawGear(e.Graphics, Brushes.Black, Brushes.Pink, Pens.Red, new Point2D(x, y),
                radius, tooth_length, 10, 5, true);
        }

        /// <summary>
        /// Draw a gear.
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="axle_brush">The axle_brush.</param>
        /// <param name="gear_brush">The gear_brush.</param>
        /// <param name="gear_pen">The gear_pen.</param>
        /// <param name="center">The center.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="tooth_length">The tooth_length.</param>
        /// <param name="num_teeth">The num_teeth.</param>
        /// <param name="axle_radius">The axle_radius.</param>
        /// <param name="start_with_tooth">The start_with_tooth.</param>
        public static void DrawGear(Graphics gr, Brush axle_brush, Brush gear_brush, Pen gear_pen, Point2D center, double radius, double tooth_length, int num_teeth, double axle_radius, bool start_with_tooth)
        {
            var dtheta = PI / num_teeth;
            var dtheta_degrees = dtheta * 180 / PI; // dtheta in degrees.

            const double chamfer = 2;
            var tooth_width = radius * dtheta - chamfer;
            var alpha = tooth_width / (radius + tooth_length);
            var alpha_degrees = alpha * 180 / PI;
            var phi = (dtheta - alpha) / 2;

            // Set theta for the beginning of the first tooth.
            double theta;
            theta = start_with_tooth ? dtheta / 2 : -dtheta / 2;

            // Make rectangles to represent the gear's inner and outer arcs.
            var inner_rect = new Rectangle2D(
                center.X - radius, center.Y - radius,
                2 * radius, 2 * radius);
            var outer_rect = new Rectangle2D(
                center.X - radius - tooth_length, center.Y - radius - tooth_length,
                2 * (radius + tooth_length), 2 * (radius + tooth_length));

            // Make a path representing the gear.
            var path = new GraphicsPath();
            for (var i = 0; i < num_teeth; i++)
            {
                // Move across the gap between teeth.
                var degrees = theta * 180 / PI;
                path.AddArc(inner_rect.ToRectangleF(), (float)degrees, (float)dtheta_degrees);
                theta += dtheta;

                // Move across the tooth's outer edge.
                degrees = (theta + phi) * 180 / PI;
                path.AddArc(outer_rect.ToRectangleF(), (float)degrees, (float)alpha_degrees);
                theta += dtheta;
            }

            path.CloseFigure();

            // Draw the gear.
            gr.FillPath(gear_brush, path);
            gr.DrawPath(gear_pen, path);
            gr.FillEllipse(axle_brush,
                 (float)(center.X - axle_radius), (float)(center.Y - axle_radius),
                (float)(2 * axle_radius), (float)(2 * axle_radius));
        }
        #endregion Gear Points

        #region Heart Interpolation
        /// <summary>
        /// The curve's parametric equations.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public static Point2D Heart(double t)
        {
            var sin_t = Sin(t);
            return new Point2D(16 * sin_t * sin_t * sin_t,
                 13 * Cos(t)
                - 5 * Cos(2 * t)
                - 2 * Cos(3 * t)
                - Cos(4 * t));
        }

        // Draw the curve on a bitmap.
        /// <summary>
        /// The draw heart.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>The <see cref="Bitmap"/>.</returns>
        public static Bitmap DrawHeart(int width, int height)
        {
            var bm = new Bitmap(width, height);
            using (var gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                // Generate the points.
                const int num_points = 100;
                var points = new List<Point2D>();
                const double dt = 2 * PI / num_points;
                for (double t = 0; t <= 2 * PI; t += dt)
                {
                    points.Add(new Point2D(Heart(t).X, Heart(t).Y));
                }

                // Get the coordinate bounds.
                var wxmin = points[0].X;
                var wxmax = wxmin;
                var wymin = points[0].Y;
                var wymax = wymin;
                foreach (Point2D point in points)
                {
                    if (wxmin > point.X)
                    {
                        wxmin = point.X;
                    }

                    if (wxmax < point.X)
                    {
                        wxmax = point.X;
                    }

                    if (wymin > point.Y)
                    {
                        wymin = point.Y;
                    }

                    if (wymax < point.Y)
                    {
                        wymax = point.Y;
                    }
                }

                // Make the world coordinate rectangle.
                var world_rect = new Rectangle2D(
                    wxmin, wymin, wxmax - wxmin, wymax - wymin);

                // Make the device coordinate rectangle with a margin.
                const int margin = 5;
                var device_rect = new Rectangle2D(
                    margin, margin,
                    width - 2 * margin,
                    height - 2 * margin);

                // Map world to device coordinates without distortion.
                // Flip vertically so Y increases downward.
                SetTransformationWithoutDisortion(gr, world_rect, device_rect, false, true);

                // Draw the curve.
                gr.FillPolygon(Brushes.Pink, points.ToPointFArray());
                using (var pen = new Pen(Color.Red, 0))
                {
                    gr.DrawPolygon(pen, points.ToPointFArray());

                    //// Draw a rectangle around the coordinate bounds.
                    //pen.Color = Color.Blue;
                    //gr.DrawRectangle(pen, Rectangle.Round( world_rect));

                    //// Draw the X and Y axes.
                    //pen.Color = Color.Green;
                    //gr.DrawLine(pen, -20, 0, 20, 0);
                    //gr.DrawLine(pen, 0, -20, 0, 20);
                    //for (int x = -20; x <= 20; x++)
                    //    gr.DrawLine(pen, x, -0.3f, x, 0.3f);
                    //for (int y = -20; y <= 20; y++)
                    //    gr.DrawLine(pen, -0.3f, y, 0.3f, y);
                }
            }
            return bm;
        }

        // Map from world coordinates to device coordinates
        // without distortion.
        /// <summary>
        /// Set the transformation without distortion.
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="world_rect">The world_rect.</param>
        /// <param name="device_rect">The device_rect.</param>
        /// <param name="invert_x">The invert_x.</param>
        /// <param name="invert_y">The invert_y.</param>
        public static void SetTransformationWithoutDisortion(Graphics gr,
            Rectangle2D world_rect, Rectangle2D device_rect,
            bool invert_x, bool invert_y)
        {
            // Get the aspect ratios.
            var world_aspect = world_rect.Width / world_rect.Height;
            var device_aspect = device_rect.Width / device_rect.Height;

            // Adjust the world rectangle to maintain the aspect ratio.
            var world_cx = world_rect.X + world_rect.Width / 2f;
            var world_cy = world_rect.Y + world_rect.Height / 2f;
            if (world_aspect > device_aspect)
            {
                // The world coordinates are too short and width.
                // Make them taller.
                var world_height = world_rect.Width / device_aspect;
                world_rect = new Rectangle2D(
                    world_rect.Left,
                    world_cy - world_height / 2f,
                    world_rect.Width,
                    world_height);
            }
            else
            {
                // The world coordinates are too tall and thin.
                // Make them wider.
                var world_width = device_aspect * world_rect.Height;
                world_rect = new Rectangle2D(
                    world_cx - world_width / 2f,
                    world_rect.Top,
                    world_width,
                    world_rect.Height);
            }

            // Map the new world coordinates into the device coordinates.
            SetTransformation(gr, world_rect, device_rect, invert_x, invert_y);
        }

        // Map from world coordinates to device coordinates.
        /// <summary>
        /// Set the transformation.
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="world_rect">The world_rect.</param>
        /// <param name="device_rect">The device_rect.</param>
        /// <param name="invert_x">The invert_x.</param>
        /// <param name="invert_y">The invert_y.</param>
        public static void SetTransformation(Graphics gr,
            Rectangle2D world_rect, Rectangle2D device_rect,
            bool invert_x, bool invert_y)
        {
            var device_points = new List<Point2D>
            {
                new Point2D(device_rect.Left, device_rect.Top),      // Upper left.
                new Point2D(device_rect.Right, device_rect.Top),     // Upper right.
                new Point2D(device_rect.Left, device_rect.Bottom)   // Lower left.
            };

            if (invert_x)
            {
                device_points[0] = new Point2D(device_rect.Right, device_points[0].Y);
                device_points[1] = new Point2D(device_rect.Left, device_points[1].Y);
                device_points[2] = new Point2D(device_rect.Right, device_points[2].Y);
            }
            if (invert_y)
            {
                device_points[0] = new Point2D(device_points[0].X, device_rect.Bottom);
                device_points[1] = new Point2D(device_points[1].X, device_rect.Bottom);
                device_points[2] = new Point2D(device_points[2].X, device_rect.Top);
            }

            gr.Transform = new Matrix(world_rect.ToRectangleF(), device_points.ToPointFArray());
        }
        #endregion Heart Interpolation

        #region Hermite Interpolation of 1D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(HermiteInterpolate1DTests))]
        public static List<SpeedTester> HermiteInterpolate1DTests()
            => new List<SpeedTester> {
                new SpeedTester(() => HermiteInterpolate1D(0, 1, 2, 3, 0.5d, 1, 0),
                $"{nameof(Experiments.HermiteInterpolate1D)}(0, 1, 2, 3, 0.5d, 1, 0)"),
                new SpeedTester(() => Hermite(0, 1, 2, 3, 0.5d),
                $"{nameof(Experiments.Hermite)}(0, 1, 2, 3, 0.5d)")
            };

        /// <summary>
        /// The hermite interpolate1d.
        /// </summary>
        /// <param name="v0">The v0.</param>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <param name="v3">The v3.</param>
        /// <param name="s">The s.</param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        public static double HermiteInterpolate1D(
            double v0,
            double v1,
            double v2,
            double v3,
            double s, double tension, double bias)
        {
            var sSquared = s * s;
            var sCubed = sSquared * s;
            var m0 = (v1 - v0) * (1 + bias) * (1 - tension) / 2;
            m0 += (v2 - v1) * (1 - bias) * (1 - tension) / 2;
            var m1 = (v2 - v1) * (1 + bias) * (1 - tension) / 2;
            m1 += (v3 - v2) * (1 - bias) * (1 - tension) / 2;
            var a0 = 2 * sCubed - 3 * sSquared + 1;
            var a1 = sCubed - 2 * sSquared + s;
            var a2 = sCubed - sSquared;
            var a3 = -2 * sCubed + 3 * sSquared;

            return a0 * v1 + a1 * m0 + a2 * m1 + a3 * v2;
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        /// <param name="v1">Source position.</param>
        /// <param name="t1">Source tangent.</param>
        /// <param name="v2">Source position.</param>
        /// <param name="t2">Source tangent.</param>
        /// <param name="s">Weighting factor.</param>
        /// <returns>The result of the Hermite spline interpolation.</returns>
        public static double Hermite(
            double v1,
            double t1,
            double v2,
            double t2,
            double s)
        {
            double result;
            var sSquared = s * s;
            var sCubed = sSquared * s;

            if (s == 0f)
            {
                result = v1;
            }
            else
            {
                result = s == 1f ? v2 : (2 * v1 - 2 * v2 + t2 + t1) * sCubed
                   + (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared
                   + t1 * s
                   + v1;
            }

            return result;
        }
        #endregion Hermite Interpolation of 1D Points

        #region Hermite Interpolation of 2D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(HermiteInterpolate2DTests))]
        public static List<SpeedTester> HermiteInterpolate2DTests()
            => new List<SpeedTester> {
                new SpeedTester(() => HermiteInterpolate2D(0, 1, 2, 3, 4, 5, 6, 7, 0.5d, 1, 0),
                $"{nameof(Experiments.HermiteInterpolate2D)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d, 1, 0)")
            };

        /// <summary>
        /// The hermite interpolate2d.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="mu">The mu.</param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        public static (double X, double Y) HermiteInterpolate2D(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double mu, double tension, double bias)
        {
            var mu2 = mu * mu;
            var mu3 = mu2 * mu;

            var mX0 = (x1 - x0) * (1 + bias) * (1 - tension) / 2;
            mX0 += (x2 - x1) * (1 - bias) * (1 - tension) / 2;
            var mY0 = (y1 - y0) * (1 + bias) * (1 - tension) / 2;
            mY0 += (y2 - y1) * (1 - bias) * (1 - tension) / 2;
            var mX1 = (x2 - x1) * (1 + bias) * (1 - tension) / 2;
            mX1 += (x3 - x2) * (1 - bias) * (1 - tension) / 2;
            var mY1 = (y2 - y1) * (1 + bias) * (1 - tension) / 2;
            mY1 += (y3 - y2) * (1 - bias) * (1 - tension) / 2;
            var a0 = 2 * mu3 - 3 * mu2 + 1;
            var a1 = mu3 - 2 * mu2 + mu;
            var a2 = mu3 - mu2;
            var a3 = -2 * mu3 + 3 * mu2;

            return (
                a0 * x1 + a1 * mX0 + a2 * mX1 + a3 * x2,
                a0 * y1 + a1 * mY0 + a2 * mY1 + a3 * y2);
        }

        /// <summary>
        /// Tension: 1 is high, 0 normal, -1 is low
        /// Bias: 0 is even,
        /// positive is towards First segment,
        /// negative towards the other
        /// </summary>
        /// <param name="a"></param>
        /// <param name="aTan"></param>
        /// <param name="b"></param>
        /// <param name="bTan"></param>
        /// <param name="index"></param>
        /// <param name="tension"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        public static Point2D Interpolate1(Point2D a, Point2D aTan, Point2D b, Point2D bTan, double tension, double bias, double index)
        {
            //double mu2 = mu * mu;
            //double mu3 = mu2 * mu;
            //Point2D m0 = (y1 - y0) * (1 + bias) * (1 - tension) * 0.5;
            //m0 += (y2 - y1) * (1 - bias) * (1 - tension) * 0.5;
            //Point2D m1 = (y2 - y1) * (1 + bias) * (1 - tension) * 0.5;
            //m1 += (y3 - y2) * (1 - bias) * (1 - tension) * 0.5;
            //double a0 = 2 * mu3 - 3 * mu2 + 1;
            //double a1 = mu3 - 2 * mu2 + mu;
            //double a2 = mu3 - mu2;
            //double a3 = -2 * mu3 + 3 * mu2;
            //return (a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2);
            var mu2 = index * index;
            var mu3 = mu2 * index;
            var m0 = aTan.Subtract(a).Scale(1 + bias).Scale(1 - tension).Scale(0.5);
            m0 = m0.Add(b.Subtract(aTan).Scale(1 - bias).Scale(1 - tension).Scale(0.5));
            var m1 = b.Subtract(aTan).Scale(1 + bias).Scale(1 - tension).Scale(0.5);
            m1 = m1.Add(bTan.Subtract(b).Scale(1 - bias).Scale(1 - tension).Scale(0.5));
            var a0 = 2 * mu3 - 3 * mu2 + 1;
            var a1 = mu3 - 2 * mu2 + index;
            var a2 = mu3 - mu2;
            var a3 = -2 * mu3 + 3 * mu2;
            return (Point2D)aTan.Scale(a0).Add(m0.Scale(a1)).Add(m1.Scale(a2)).Add(b.Scale(a3));
        }

        /// <summary>
        /// The hermite interpolate.
        /// </summary>
        /// <param name="y0">The y0.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="mu">The mu.</param>
        /// <param name="tension">Tension: 1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">Bias: 0 is even,</param>
        /// <returns>The <see cref="double"/>. positive is towards First segment, negative towards the other</returns>
        public static double Hermite_Interpolate(double y0, double y1, double y2, double y3, double mu, double tension, double bias)
        {
            var m0 = (y1 - y0) * ((1 + bias) * ((1 - tension) / 2));
            m0 += (y2 - y1) * ((1 - bias) * ((1 - tension) / 2));
            var m1 = (y2 - y1) * ((1 + bias) * ((1 - tension) / 2));
            m1 += (y3 - y2) * ((1 - bias) * ((1 - tension) / 2));
            var mu2 = mu * mu;
            var mu3 = mu2 * mu;
            var a0 = (2 * mu3) - (3 * mu2) + 1;
            var a1 = mu3 - (2 * mu2) + mu;
            var a2 = mu3 - mu2;
            var a3 = (2 * mu3 * -1) + (3 * mu2);
            return (a0 * y1) + ((a1 * m0) + ((a2 * m1) + (a3 * y2)));
        }

        /// <summary>
        /// The hermite interpolate.
        /// </summary>
        /// <param name="y0">The y0.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="mu">The mu.</param>
        /// <param name="tension">The tension.</param>
        /// <param name="bias">The bias.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double HermiteInterpolate(double y0, double y1, double y2, double y3, double mu, double tension, double bias)
        {
            var m0 = (y1 - y0) * ((1 + bias) * ((1 - tension) / 2));
            m0 += (y2 - y1) * ((1 - bias) * ((1 - tension) / 2));
            var m1 = (y2 - y1) * ((1 + bias) * ((1 - tension) / 2));
            m1 += (y3 - y2) * ((1 - bias) * ((1 - tension) / 2));
            var mu2 = mu * mu;
            var mu3 = mu2 * mu;
            var a0 = (2 * mu3) - (3 * mu2) + 1;
            var a1 = mu3 - (2 * mu2) + mu;
            var a2 = mu3 - mu2;
            var a3 = (2 * mu3 * -1) + (3 * mu2);

            return (a0 * y1) + ((a1 * m0) + ((a2 * m1) + (a3 * y2)));
        }

        /// <summary>
        /// Tension: 1 is high, 0 normal, -1 is low
        /// Bias: 0 is even,
        /// positive is towards First segment,
        /// negative towards the other
        /// </summary>
        /// <param name="a"></param>
        /// <param name="aTan"></param>
        /// <param name="b"></param>
        /// <param name="bTan"></param>
        /// <param name="index"></param>
        /// <param name="tension"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        public static Point2D Interpolate2(Point2D a, Point2D aTan, Point2D b, Point2D bTan, double tension, double bias, double index)
        {
            var t2 = index * index;
            var t3 = t2 * index;
            var tb = (1 + bias) * ((1 - tension) / 2);
            return (Point2D)aTan.Scale((2 * t3) - (3 * t2) + 1).Add(aTan.Subtract(a).Add(b.Subtract(aTan)).Scale(t3 - (2 * t2) + index).Add(b.Subtract(aTan).Add(bTan.Subtract(b)).Scale(t3 - t2)).Scale(tb).Add(b.Scale((3 * t2) - (2 * t3))));
        }
        #endregion Hermite Interpolation of 2D Points

        #region Hermite Interpolation of 3D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(HermiteInterpolate3DTests))]
        public static List<SpeedTester> HermiteInterpolate3DTests() => new List<SpeedTester> {
                new SpeedTester(() => HermiteInterpolate3D(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d, 1, 0),
                $"{nameof(Experiments.HermiteInterpolate3D)}(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d, 1, 0)")
            };

        /// <summary>
        /// The hermite interpolate3d.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="z0">The z0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="z3">The z3.</param>
        /// <param name="mu">The mu.</param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        public static (double X, double Y, double Z) HermiteInterpolate3D(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double mu, double tension, double bias)
        {
            var mu2 = mu * mu;
            var mu3 = mu2 * mu;

            var mX0 = (x1 - x0) * (1 + bias) * (1 - tension) / 2;
            mX0 += (x2 - x1) * (1 - bias) * (1 - tension) / 2;
            var mY0 = (y1 - y0) * (1 + bias) * (1 - tension) / 2;
            mY0 += (y2 - y1) * (1 - bias) * (1 - tension) / 2;
            var mZ0 = (z1 - z0) * (1 + bias) * (1 - tension) / 2;
            mZ0 += (z2 - z1) * (1 - bias) * (1 - tension) / 2;
            var mX1 = (x2 - x1) * (1 + bias) * (1 - tension) / 2;
            mX1 += (x3 - x2) * (1 - bias) * (1 - tension) / 2;
            var mY1 = (y2 - y1) * (1 + bias) * (1 - tension) / 2;
            mY1 += (y3 - y2) * (1 - bias) * (1 - tension) / 2;
            var mZ1 = (z2 - z1) * (1 + bias) * (1 - tension) / 2;
            mZ1 += (z3 - z2) * (1 - bias) * (1 - tension) / 2;
            var a0 = 2 * mu3 - 3 * mu2 + 1;
            var a1 = mu3 - 2 * mu2 + mu;
            var a2 = mu3 - mu2;
            var a3 = -2 * mu3 + 3 * mu2;

            return (
                a0 * x1 + a1 * mX0 + a2 * mX1 + a3 * x2,
                a0 * y1 + a1 * mY0 + a2 * mY1 + a3 * y2,
                a0 * z1 + a1 * mZ0 + a2 * mZ1 + a3 * z2);
        }
        #endregion Hermite Interpolation of 3D Points

        #region Hermite To Cubic Bézier
        /// <summary>
        /// The to cubic bezier.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="aTan">The aTan.</param>
        /// <param name="b">The b.</param>
        /// <param name="bTan">The bTan.</param>
        /// <returns>The <see cref="CubicBezier"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/29087503/how-to-create-jigsaw-puzzle-pieces-using-opengl-and-bezier-curve/29089681#29089681
        /// </acknowledgment>
        public static CubicBezier ToCubicBezier(Point2D a, Point2D aTan, Point2D b, Point2D bTan) => new CubicBezier(aTan, new Point2D(aTan.X - (b.X - a.X) / 6, aTan.Y - (b.Y - a.Y) / 6), new Point2D(b.X + (bTan.X - aTan.X) / 6, b.Y + (bTan.Y - aTan.Y) / 6), bTan);
        #endregion Hermite To Cubic Bézier

        #region Horizontal Line Segments Overlap
        /// <summary>
        /// The horz segments overlap.
        /// </summary>
        /// <param name="segAX">The segAX.</param>
        /// <param name="segAY">The segAY.</param>
        /// <param name="segBX">The segBX.</param>
        /// <param name="segBY">The segBY.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com/delphi/clipper.php
        /// </acknowledgment>
        public static bool HorzSegmentsOverlap(double segAX, double segAY, double segBX, double segBY)
        {
            if (segAX > segAY)
            {
                Maths.Swap(ref segAX, ref segAY);
            }

            if (segBX > segBY)
            {
                Maths.Swap(ref segBX, ref segBY);
            }

            return (segAX < segBY) && (segBX < segAY);
        }
        #endregion Horizontal Line Segments Overlap

        #region Intersection of Circle and Circle
        /// <summary>
        /// Find the points where the two circles intersect.
        /// </summary>
        /// <param name="cx0"></param>
        /// <param name="cy0"></param>
        /// <param name="radius0"></param>
        /// <param name="cx1"></param>
        /// <param name="cy1"></param>
        /// <param name="radius1"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/09/determine-where-two-circles-intersect-in-c/
        /// </acknowledgment>
        public static (int, (double X, double Y), (double X, double Y)) FindCircleCircleIntersections(
            double cx0,
            double cy0,
            double radius0,
            double cx1,
            double cy1,
            double radius1)
        {
            // Find the distance between the centers.
            var dx = cx0 - cx1;
            var dy = cy0 - cy1;
            var dist = Sqrt(dx * dx + dy * dy);

            (double X, double Y) intersection1;
            (double X, double Y) intersection2;

            // See how many solutions there are.
            if (dist > radius0 + radius1)
            {
                // No solutions, the circles are too far apart.
                intersection1 = (double.NaN, double.NaN);
                intersection2 = (double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else if (dist < Abs(radius0 - radius1))
            {
                // No solutions, one circle contains the other.
                intersection1 = (double.NaN, double.NaN);
                intersection2 = (double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else if ((Abs(dist) < DoubleEpsilon) && (Abs(radius0 - radius1) < DoubleEpsilon))
            {
                // No solutions, the circles coincide.
                intersection1 = (double.NaN, double.NaN);
                intersection2 = (double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else
            {
                // Find a and h.
                var a = (radius0 * radius0
                    - radius1 * radius1 + dist * dist) / (2 * dist);
                var h = Sqrt(radius0 * radius0 - a * a);

                // Find P2.
                var cx2 = cx0 + a * (cx1 - cx0) / dist;
                var cy2 = cy0 + a * (cy1 - cy0) / dist;

                // Get the points P3.
                intersection1 = (
                    cx2 + h * (cy1 - cy0) / dist,
                    cy2 - h * (cx1 - cx0) / dist);
                intersection2 = (
                    cx2 - h * (cy1 - cy0) / dist,
                    cy2 + h * (cx1 - cx0) / dist);

                // See if we have 1 or 2 solutions.
                if (Abs(dist - radius0 + radius1) < DoubleEpsilon)
                {
                    return (1, intersection1, intersection2);
                }

                return (2, intersection1, intersection2);
            }
        }

        /// <summary>
        /// The circle circle intersection.
        /// </summary>
        /// <param name="A">The A.</param>
        /// <param name="B">The B.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://www.xarg.org/2016/07/calculate-the-intersection-points-of-two-circles/
        /// </acknowledgment>
        public static (int, (double X, double Y), (double X, double Y))? CircleCircleIntersection(Circle A, Circle B)
        {
            var d = Sqrt(Pow(B.X - A.X, 2) + Pow(B.Y - A.Y, 2));

            if (d <= A.Radius + B.Radius)
            {
                var x = (A.Radius * A.Radius - B.Radius * B.Radius + d * d) / (2 * d);
                var y = Sqrt(A.Radius * A.Radius - x * x);

                if (A.Radius < Abs(x))
                {
                    // No intersection, one circle is in the other
                    return null;
                }
                else
                {
                    var e1 = (X: (B.X - A.X) / d, Y: (B.Y - A.Y) / d);
                    var e2 = (X: -e1.X, e1.Y);
                    var P1 = (X: A.X + x * e1.X + y * e2.Y, Y: A.Y + x * e1.Y + y * e2.X);
                    var P2 = (X: A.X + x * e1.X - y * e2.Y, Y: A.Y + x * e1.Y - y * e2.X);
                    return (2, P1, P2);
                }
            }
            else
            {
                // No Intersection, far outside
                return null;
            }
        }
        #endregion Intersection of Circle and Circle

        #region Intersection of Circle and Line Segment
        /// <summary>
        /// Find the points of the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CircleLineSegmentIntersection1(
            double cX, double cY,
            double r,
            double lAX, double lAY,
            double lBX, double lBY)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((lAX == lBX) && (lAY == lBY)))
            {
                return result;
            }

            var dx = lBX - lAX;
            var dy = lBY - lAY;

            // Calculate the quadratic parameters.
            var a = dx * dx + dy * dy;
            var b = 2 * (dx * (lAX - cX) + dy * (lAY - cY));
            var c = (lAX - cX) * (lAX - cX) + (lAY - cY) * (lAY - cY) - r * r;

            // Calculate the discriminant.
            var discriminant = b * b - 4 * a * c;

            if ((a <= Epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                var t = -b / (2 * a);

                // Add the points if they are between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d))
                {
                    result = new Intersection(IntersectionState.Intersection);
                    result.AppendPoint(new Point2D(lAX + t * dx, lAY + t * dy));
                }
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = (-b + Sqrt(discriminant)) / (2 * a);
                var t2 = (-b - Sqrt(discriminant)) / (2 * a);

                // Add the points if they are between the end points of the line segment.
                result = new Intersection(IntersectionState.Intersection);
                if ((t1 >= 0d) && (t1 <= 1d))
                {
                    result.AppendPoint(new Point2D(lAX + t1 * dx, lAY + t1 * dy));
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    result.AppendPoint(new Point2D(lAX + t2 * dx, lAY + t2 * dy));
                }
            }

            return result;
        }

        /// <summary>
        /// The circle line segment intersection.
        /// </summary>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="r">The r.</param>
        /// <param name="lAX">The lAX.</param>
        /// <param name="lAY">The lAY.</param>
        /// <param name="lBX">The lBX.</param>
        /// <param name="lBY">The lBY.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="Intersection"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CircleLineSegmentIntersection(
            double cX, double cY,
            double r,
            double lAX, double lAY,
            double lBX, double lBY,
            double epsilon = Epsilon)
        {
            Intersection result;

            var a = (lBX - lAX) * (lBX - lAX) + (lBY - lAY) * (lBY - lAY);
            var b = 2 * ((lBX - lAX) * (lAX - cX) + (lBY - lAY) * (lAY - cY));
            var c = cX * cX + cY * cY + lAX * lAX + lAY * lAY - 2 * (cX * lAX + cY * lAY) - r * r;

            var determinant = b * b - 4 * a * c;
            if (determinant < 0)
            {
                result = new Intersection(IntersectionState.Outside);
            }
            else if (determinant == 0)
            {
                result = new Intersection(IntersectionState.Tangent | IntersectionState.Intersection);
                var u1 = (-b) / (2 * a);
                if (u1 < 0 || u1 > 1)
                {
                    result = (u1 < 0) || (u1 > 1) ? new Intersection(IntersectionState.Outside) : new Intersection(IntersectionState.Inside);
                }
                else
                {
                    result = new Intersection(IntersectionState.Intersection);
                    if (0 <= u1 && u1 <= 1)
                    {
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u1));
                    }
                }
            }
            else
            {
                var e = Sqrt(determinant);
                var u1 = (-b + e) / (2 * a);
                var u2 = (-b - e) / (2 * a);
                if ((u1 < 0 || u1 > 1) && (u2 < 0 || u2 > 1))
                {
                    result = (u1 < 0 && u2 < 0) || (u1 > 1 && u2 > 1) ? new Intersection(IntersectionState.Outside) : new Intersection(IntersectionState.Inside);
                }
                else
                {
                    result = new Intersection(IntersectionState.Intersection);
                    if (0 <= u1 && u1 <= 1)
                    {
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u1));
                    }

                    if (0 <= u2 && u2 <= 1)
                    {
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u2));
                    }
                }
            }
            return result;
        }
        #endregion Intersection of Circle and Line Segment

        #region Intersection of Circle and Line
        /// <summary>
        /// Find the points of intersection.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int, (double X, double Y), (double X, double Y)) LineCircle(
            (double X, double Y) center,
            double radius,
            (double X, double Y) point1,
            (double X, double Y) point2)
        {
            double t;

            var dx = point2.X - point1.X;
            var dy = point2.Y - point1.Y;

            var A = dx * dx + dy * dy;
            var B = 2 * (dx * (point1.X - center.X) + dy * (point1.Y - center.Y));
            var C = (point1.X - center.X) * (point1.X - center.X) + (point1.Y - center.Y) * (point1.Y - center.Y) - radius * radius;

            (double X, double Y) intersection1;
            (double X, double Y) intersection2;

            var det = B * B - 4 * A * C;
            if ((A <= 0.0000001) || (det < 0))
            {
                // No real solutions.
                intersection1 = (double.NaN, double.NaN);
                intersection2 = (double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else if (Abs(det) < DoubleEpsilon)
            {
                // One solution.
                t = -B / (2 * A);
                intersection1 = (point1.X + t * dx, point1.Y + t * dy);
                intersection2 = (double.NaN, double.NaN);
                return (1, intersection1, intersection2);
            }
            else
            {
                // Two solutions.
                t = (-B + Sqrt(det)) / (2 * A);
                intersection1 = (point1.X + t * dx, point1.Y + t * dy);
                t = (-B - Sqrt(det)) / (2 * A);
                intersection2 = (point1.X + t * dx, point1.Y + t * dy);
                return (2, intersection1, intersection2);
            }
        }

        /// <summary>
        /// The circle line intersection.
        /// </summary>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="r">The r.</param>
        /// <param name="lAX">The lAX.</param>
        /// <param name="lAY">The lAY.</param>
        /// <param name="lBX">The lBX.</param>
        /// <param name="lBY">The lBY.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="Intersection"/>.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CircleLineIntersection(
            double cX, double cY,
            double r,
            double lAX, double lAY,
            double lBX, double lBY,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((lAX == lBX) && (lAY == lBY)))
            {
                return result;
            }

            var dx = lBX - lAX;
            var dy = lBY - lAY;

            // Calculate the quadratic parameters.
            var a = dx * dx + dy * dy;
            var b = 2 * (dx * (lAX - cX) + dy * (lAY - cY));
            var c = (lAX - cX) * (lAX - cX) + (lAY - cY) * (lAY - cY) - r * r;

            // Calculate the discriminant.
            var discriminant = b * b - 4 * a * c;

            if ((a <= Epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                var t = -b / (2 * a);

                // Add the points.
                result = new Intersection(IntersectionState.Intersection);
                result.AppendPoint(new Point2D(lAX + t * dx, lAY + t * dy));
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = (-b + Sqrt(discriminant)) / (2 * a);
                var t2 = (-b - Sqrt(discriminant)) / (2 * a);

                // Add the points.
                result = new Intersection(IntersectionState.Intersection);
                result.AppendPoint(new Point2D(lAX + t1 * dx, lAY + t1 * dy));
                result.AppendPoint(new Point2D(lAX + t2 * dx, lAY + t2 * dy));
            }

            return result;
        }
        #endregion Intersection of Circle and Line

        #region Intersection of Conic Section With Line Segment
        // http://csharphelper.com/blog/2014/11/see-where-a-line-intersects-a-conic-section-in-c/
        #endregion Intersection of Conic Section With Line Segment

        #region Intersection of Conic Section With Conic Section
        // http://csharphelper.com/blog/2014/11/see-where-two-conic-sections-intersect-in-c/
        #endregion Intersection of Conic Section With Conic Section

        #region Intersection of Ellipse and Ellipse

        /// <summary>
        /// Finds Intersection of two Ellipse'
        /// </summary>
        /// <param name="ellipseA"></param>
        /// <param name="ellipseB"></param>
        /// <returns></returns>
        public static LineSegment Intersect(Ellipse ellipseA, Ellipse ellipseB)
        {
            var d = ellipseB.Center.X * ellipseB.Center.X - ellipseA.Center.X * ellipseA.Center.X - ellipseB.MajorRadius * ellipseB.MajorRadius - Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2) + ellipseA.MajorRadius * ellipseA.MajorRadius;
            var a = Pow(2 * ellipseA.Center.X - 2 * ellipseB.Center.X, 2) + 4 * Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2);
            var b = 2 * d * (2 * ellipseA.Center.X - 2 * ellipseB.Center.X) - 8 * ellipseB.Center.X * Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2);
            var C = 4 * ellipseB.Center.X * ellipseB.Center.X * Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2) + d * d - 4 * Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2) * ellipseB.MajorRadius * ellipseB.MajorRadius;
            var XA = (-b + Sqrt(b * b - 4 * a * C)) / (2 * a);
            var XB = (-b - Sqrt(b * b - 4 * a * C)) / (2 * a);
            var YA = Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Pow(XA - ellipseA.Center.X, 2)) + ellipseA.Center.Y;
            var YB = -Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Pow(XA - ellipseA.Center.X, 2)) + ellipseA.Center.Y;
            var YC = Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Pow(XB - ellipseA.Center.X, 2)) + ellipseA.Center.Y;
            var YD = -Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Pow(XB - ellipseA.Center.X, 2)) + ellipseA.Center.Y;
            var e = XA - ellipseB.Center.X + Pow(YA - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius;
            var F = XA - ellipseB.Center.X + Pow(YB - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius;
            var g = XB - ellipseB.Center.X + Pow(YC - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius;
            var H = XB - ellipseB.Center.X + Pow(YD - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius;
            if (Abs(F) < Abs(e))
            {
                YA = YB;
            }

            if (Abs(H) < Abs(g))
            {
                YC = YD;
            }

            if (Abs(ellipseA.Center.Y - ellipseB.Center.Y) < DoubleEpsilon)
            {
                YC = 2 * ellipseA.Center.Y - YA;
            }

            return new LineSegment(XA, YA, XB, YC);
        }

        /// <summary>
        /// Find the points of intersection.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="eis"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// </acknowledgment>
        public static void FindPointsOfIntersectionNewtonsMethod(double xmin, double xmax, EllipseIntersectStuff eis)
        {
            eis.Roots = new List<Point2D>();
            eis.RootSign1 = new List<double>();
            eis.RootSign2 = new List<double>();

            if (!eis.GotEllipse1 || !eis.GotEllipse2)
            {
                return;
            }

            // Find roots for each of the difference equations.
            double[] signs = { +1f, -1f };
            foreach (var sign1 in signs)
            {
                foreach (var sign2 in signs)
                {
                    var points = FindRootsUsingNewtonsMethod(
                        xmin, xmax,
                        eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, sign1,
                        eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, sign2);
                    if (points.Count > 0)
                    {
                        eis.Roots.AddRange(points);
                        for (var i = 0; i < points.Count; i++)
                        {
                            eis.RootSign1.Add(sign1);
                            eis.RootSign2.Add(sign2);
                        }
                    }
                }
            }

            // Find corresponding points of intersection.
            eis.PointsOfIntersection = new List<Point2D>();
            for (var i = 0; i < eis.Roots.Count; i++)
            {
                var y1 = G1(eis.Roots[i].X, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, eis.RootSign1[i]);
                var y2 = G1(eis.Roots[i].X, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, eis.RootSign2[i]);
                eis.PointsOfIntersection.Add(new Point2D(eis.Roots[i].X, y1));

                // Validation.
                Debug.Assert(Abs(y1 - y2) < DoubleEpsilon);
            }
        }

        /// <summary>
        /// Find the points of intersection.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="eis"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-4/
        /// </acknowledgment>
        public static void FindPointsOfIntersectionUsingBinaryDivision(double xmin, double xmax, EllipseIntersectStuff eis)
        {
            eis.Roots = new List<Point2D>();
            eis.RootSign1 = new List<double>();
            eis.RootSign2 = new List<double>();

            if (!eis.GotEllipse1 || !eis.GotEllipse2)
            {
                return;
            }

            // Find roots for each of the difference equations.
            double[] signs = { +1f, -1f };
            foreach (var sign1 in signs)
            {
                foreach (var sign2 in signs)
                {
                    var points = FindRootsUsingBinaryDivision(
                        xmin, xmax,
                        eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, sign1,
                        eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, sign2);
                    if (points.Count > 0)
                    {
                        eis.Roots.AddRange(points);
                        for (var i = 0; i < points.Count; i++)
                        {
                            eis.RootSign1.Add(sign1);
                            eis.RootSign2.Add(sign2);
                        }
                    }
                }
            }

            // Find corresponding points of intersection.
            eis.PointsOfIntersection = new List<Point2D>();
            for (var i = 0; i < eis.Roots.Count; i++)
            {
                var y1 = G1(eis.Roots[i].X, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, eis.RootSign1[i]);
                var y2 = G1(eis.Roots[i].X, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, eis.RootSign2[i]);
                eis.PointsOfIntersection.Add(new Point2D(eis.Roots[i].X, y1));

                // Validation.
                Debug.Assert(Abs(y1 - y2) < DoubleEpsilon);
            }
        }

        /// <summary>
        /// Find roots by using Newton's method.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// </acknowledgment>
        public static List<Point2D> FindRootsUsingNewtonsMethod(double xmin, double xmax,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            var roots = new List<Point2D>();
            const int num_tests = 1000;
            var delta_x = (xmax - xmin) / (num_tests - 1);

            // Loop over the possible x values looking for roots.
            var x0 = xmin;
            for (var i = 0; i < num_tests; i++)
            {
                // Try to find a root at this position.
                UseNewtonsMethod(x0, out var x, out var y,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);

                // See if we have already found this root.
                if (IsNumber(y))
                {
                    var is_new = true;
                    foreach (Point2D pt in roots)
                    {
                        if (Abs(pt.X - x) < DoubleEpsilon)
                        {
                            is_new = false;
                            break;
                        }
                    }

                    // If this is a new point, save it.
                    if (is_new)
                    {
                        roots.Add(new Point2D(x, y));

                        // If we've found two roots, we won't find any more.
                        if (roots.Count > 1)
                        {
                            return roots;
                        }
                    }
                }

                x0 += delta_x;
            }

            return roots;
        }

        /// <summary>
        /// Find roots by using binary division.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-4/
        /// </acknowledgment>
        public static List<Point2D> FindRootsUsingBinaryDivision(double xmin, double xmax,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            var roots = new List<Point2D>();
            const int num_tests = 10;
            var delta_x = (xmax - xmin) / (num_tests - 1);

            // Loop over the possible x values looking for roots.
            var x0 = xmin;
            for (var i = 0; i < num_tests; i++)
            {
                // Try to find a root in this range.
                UseBinaryDivision(x0, delta_x, out var x, out var y,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);

                // See if we have already found this root.
                if (IsNumber(y))
                {
                    var is_new = true;
                    foreach (Point2D pt in roots)
                    {
                        if (Abs(pt.X - x) < DoubleEpsilon)
                        {
                            is_new = false;
                            break;
                        }
                    }

                    // If this is a new point, save it.
                    if (is_new)
                    {
                        roots.Add(new Point2D(x, y));

                        // If we've found two roots, we won't find any more.
                        if (roots.Count > 1)
                        {
                            return roots;
                        }
                    }
                }

                x0 += delta_x;
            }

            return roots;
        }

        /// <summary>
        /// Find a root by using Newton's method.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// </acknowledgment>
        public static void UseNewtonsMethod(double x0, out double x, out double y,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            const double cutoff = 0.0000001f;
            const double tiny = 0.00001f;
            const int max_iterations = 100;
            double epsilon;
            var iterations = 0;

            do
            {
                // Display this guess x0.
                iterations++;

                // Make sure x0 isn't on a flat spot.
                var g_prime = GPrime(x0,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);
                while (Abs(g_prime) < tiny)
                {
                    x0 += tiny;
                    g_prime = GPrime(x0,
                        A1, B1, C1, D1, E1, F1, sign1,
                        A2, B2, C2, D2, E2, F2, sign2);
                }

                // Calculate the next estimate for x0.
                var g = G(x0,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);
                epsilon = -g / g_prime;
                x0 += epsilon;
            } while ((Abs(epsilon) > cutoff) && (iterations < max_iterations));

            x = x0;
            y = G(x0,
                A1, B1, C1, D1, E1, F1, sign1,
                A2, B2, C2, D2, E2, F2, sign2);
            //Console.WriteLine("G1(" + x + ") = " + y +
            //    ", Epsilon: " + epsilon +
            //    ", Iterations: " + iterations);
        }

        /// <summary>
        /// Find a root by using binary division.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="delta_x"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-4/
        /// </acknowledgment>
        public static void UseBinaryDivision(double x0, double delta_x,
            out double x, out double y,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            const int num_trials = 200;
            const int sgn_nan = -2;

            // Get G(x) for the bounds.
            var xmin = x0;
            var g_xmin = G(xmin,
                A1, B1, C1, D1, E1, F1, sign1,
                A2, B2, C2, D2, E2, F2, sign2);
            if (Abs(g_xmin) < DoubleEpsilon)
            {
                x = xmin;
                y = g_xmin;
                return;
            }

            var xmax = xmin + delta_x;
            var g_xmax = G(xmax,
                A1, B1, C1, D1, E1, F1, sign1,
                A2, B2, C2, D2, E2, F2, sign2);
            if (Abs(g_xmax) < DoubleEpsilon)
            {
                x = xmax;
                y = g_xmax;
                return;
            }

            // Get the sign of the values.
            int sgn_min, sgn_max;
            sgn_min = IsNumber(g_xmin) ? Sign(g_xmin) : sgn_nan;
            sgn_max = IsNumber(g_xmax) ? Sign(g_xmax) : sgn_nan;

            // If the two values have the same sign,
            // then there is no root here.
            if (sgn_min == sgn_max)
            {
                x = 1;
                y = double.NaN;
                return;
            }

            // Use binary division to find the point of intersection.
            double xmid = 0, g_xmid = 0;
            var sgn_mid = 0;
            for (var i = 0; i < num_trials; i++)
            {
                // Get values for the midpoint.
                xmid = (xmin + xmax) / 2;
                g_xmid = G(xmid,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);
                sgn_mid = IsNumber(g_xmid) ? Sign(g_xmid) : sgn_nan;

                // If sgn_mid is 0, gxmid is 0 so this is the root.
                if (sgn_mid == 0)
                {
                    break;
                }

                // See which half contains the root.
                if (sgn_mid == sgn_min)
                {
                    // The min and mid values have the same sign.
                    // Search the right half.
                    xmin = xmid;
                    g_xmin = g_xmid;
                }
                else if (sgn_mid == sgn_max)
                {
                    // The max and mid values have the same sign.
                    // Search the left half.
                    xmax = xmid;
                    g_xmax = g_xmid;
                }
                else
                {
                    // The three values have different signs.
                    // Assume min or max is NaN.
                    if (sgn_min == sgn_nan)
                    {
                        // Value g_xmin is NaN. Use the right half.
                        xmin = xmid;
                        g_xmin = g_xmid;
                    }
                    else if (sgn_max == sgn_nan)
                    {
                        // Value g_xmax is NaN. Use the right half.
                        xmax = xmid;
                        g_xmax = g_xmid;
                    }
                    else
                    {
                        // This is a weird case. Just trap it.
                        throw new InvalidOperationException(
                            "Unexpected difference curve. " +
                            "Cannot find a root between X = " +
                            xmin + " and X = " + xmax);
                    }
                }
            }

            if (IsNumber(g_xmid) && (Abs(g_xmid) < DoubleEpsilon))
            {
                x = xmid;
                y = g_xmid;
            }
            else if (IsNumber(g_xmin) && (Abs(g_xmin) < DoubleEpsilon))
            {
                x = xmin;
                y = g_xmin;
            }
            else if (IsNumber(g_xmax) && (Abs(g_xmax) < DoubleEpsilon))
            {
                x = xmax;
                y = g_xmax;
            }
            else
            {
                x = xmid;
                y = double.NaN;
            }
        }

        /// <summary>
        /// Get an ellipse's points from its equation.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// </acknowledgment>
        public static List<Point2D> GetPointsFromEquation(double xmin, double xmax,
            double a, double b, double c, double d, double e, double f)
        {
            var points = new List<Point2D>();
            for (var x = xmin; x <= xmax; x++)
            {
                var y = G1(a, b, c, d, e, f, x, +1f);
                if (IsNumber(y))
                {
                    points.Add(new Point2D(x, y));
                }
            }
            for (var x = xmax; x >= xmin; x--)
            {
                var y = G1(a, b, c, d, e, f, x, -1f);
                if (IsNumber(y))
                {
                    points.Add(new Point2D(x, y));
                }
            }
            return points;
        }

        /// <summary>
        /// Get points representing the difference between the two ellipses' equations.
        /// </summary>
        /// <param name="xmin1"></param>
        /// <param name="xmax1"></param>
        /// <param name="xmin2"></param>
        /// <param name="xmax2"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// </acknowledgment>
        public static List<List<Point2D>> GetDifferencePoints(
            double xmin1, double xmax1,
            double xmin2, double xmax2,
            double A1, double B1, double C1, double D1, double E1, double F1,
            double A2, double B2, double C2, double D2, double E2, double F2)
        {
            var xmin = Min(xmin1, xmin2);
            var xmax = Max(xmax1, xmax2);
            var result = new List<List<Point2D>>();

            double[] signs = { -1f, +1f };
            foreach (var sign1 in signs)
            {
                foreach (var sign2 in signs)
                {
                    var points = new List<Point2D>();
                    result.Add(points);
                    for (var x = xmin; x <= xmax; x++)
                    {
                        var y1 = G1(A1, B1, C1, D1, E1, F1, x, sign1);
                        if (IsNumber(y1))
                        {
                            var y2 = G1(A2, B2, C2, D2, E2, F2, x, sign2);
                            if (IsNumber(y2))
                            {
                                points.Add(new Point2D(x, y1 - y2));
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Find tangents to the difference functions.
        /// </summary>
        /// <param name="eis"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// </acknowledgment>
        public static void FindDifferenceTangents(EllipseIntersectStuff eis)
        {
            eis.TangentCenters = new List<Point2D>();
            eis.TangentP1 = new List<Point2D>();
            eis.TangentP2 = new List<Point2D>();

            if (!eis.GotEllipse1 || !eis.GotEllipse2)
            {
                return;
            }

            const double tangent_length = 50;

            //++
            var tangent_y = G(eis.TangentX,
                eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f,
                eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
            if (IsNumber(tangent_y))
            {
                var slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f)
                    - G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
                if (IsNumber(slope))
                {
                    var delta_x = Sqrt(
                        tangent_length * tangent_length / (1 + slope * slope)) / 2;
                    eis.TangentCenters.Add(new Point2D(eis.TangentX, tangent_y));
                    eis.TangentP1.Add(new Point2D(eis.TangentX - delta_x, tangent_y - slope * delta_x));
                    eis.TangentP2.Add(new Point2D(eis.TangentX + delta_x, tangent_y + slope * delta_x));
                }
            }

            //+-
            tangent_y = G(eis.TangentX,
                eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f,
                eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
            if (IsNumber(tangent_y))
            {
                var slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f)
                    - G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
                if (IsNumber(slope))
                {
                    var delta_x = Sqrt(
                        tangent_length * tangent_length / (1 + slope * slope)) / 2;
                    eis.TangentCenters.Add(new Point2D(eis.TangentX, tangent_y));
                    eis.TangentP1.Add(new Point2D(eis.TangentX - delta_x, tangent_y - slope * delta_x));
                    eis.TangentP2.Add(new Point2D(eis.TangentX + delta_x, tangent_y + slope * delta_x));
                }
            }

            //-+
            tangent_y = G(eis.TangentX,
                eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f,
                eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
            if (IsNumber(tangent_y))
            {
                var slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f)
                    - G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
                if (IsNumber(slope))
                {
                    var delta_x = Sqrt(
                        tangent_length * tangent_length / (1 + slope * slope)) / 2;
                    eis.TangentCenters.Add(new Point2D(eis.TangentX, tangent_y));
                    eis.TangentP1.Add(new Point2D(eis.TangentX - delta_x, tangent_y - slope * delta_x));
                    eis.TangentP2.Add(new Point2D(eis.TangentX + delta_x, tangent_y + slope * delta_x));
                }
            }

            //--
            tangent_y = G(eis.TangentX,
                eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f,
                eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
            if (IsNumber(tangent_y))
            {
                var slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f)
                    - G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
                if (IsNumber(slope))
                {
                    var delta_x = Sqrt(
                        tangent_length * tangent_length / (1 + slope * slope)) / 2;
                    eis.TangentCenters.Add(new Point2D(eis.TangentX, tangent_y));
                    eis.TangentP1.Add(new Point2D(eis.TangentX - delta_x, tangent_y - slope * delta_x));
                    eis.TangentP2.Add(new Point2D(eis.TangentX + delta_x, tangent_y + slope * delta_x));
                }
            }
        }

        /// <summary>
        /// Get the equation for this ellipse.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="Dx"></param>
        /// <param name="Dy"></param>
        /// <param name="Rx"></param>
        /// <param name="Ry"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// </acknowledgment>
        public static void GetEllipseFormula(Rectangle2D rect,
            out double Dx, out double Dy, out double Rx, out double Ry,
            out double a, out double b, out double c, out double d,
            out double e, out double f)
        {
            Dx = rect.X + rect.Width / 2f;
            Dy = rect.Y + rect.Height / 2f;
            Rx = rect.Width / 2f;
            Ry = rect.Height / 2f;

            a = 1f / Rx / Rx;
            b = 0;
            c = 1f / Ry / Ry;
            d = -2f * Dx / Rx / Rx;
            e = -2f * Dy / Ry / Ry;
            f = Dx * Dx / Rx / Rx + Dy * Dy / Ry / Ry - 1;

            // Verify the parameters.
            Console.WriteLine();
            var xmid = rect.Left + rect.Width / 2f;
            var ymid = rect.Top + rect.Height / 2f;
            VerifyEquation(a, b, c, d, e, f, rect.Left, ymid);
            VerifyEquation(a, b, c, d, e, f, rect.Right, ymid);
            VerifyEquation(a, b, c, d, e, f, xmid, rect.Top);
            VerifyEquation(a, b, c, d, e, f, xmid, rect.Bottom);
        }

        /// <summary>
        /// Verify that the equation gives a value close to 0 for the given point (x, y).
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// </acknowledgment>
        public static void VerifyEquation(double a, double b, double c, double d, double e, double f, double x, double y)
        {
            var total = a * x * x + b * x * y + c * y * y + d * x + e * y + f;
            Console.WriteLine($"VerifyEquation ({x}, {y}) = {total}");
            Debug.Assert(Abs(total) < 0.001f);
        }

        /// <summary>
        /// Calculate G1(x). root_sign is -1 or 1.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="root_sign"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// </acknowledgment>
        public static double G1(double x, double a, double b, double c, double d, double e, double f, double root_sign)
        {
            var result = b * x + e;
            result *= result;
            result -= 4 * c * (a * x * x + d * x + f);
            result = root_sign * Sqrt(result);
            result = -(b * x + e) + result;
            result = result / 2 / c;

            return result;
        }

        /// <summary>
        /// Calculate G1'(x). root_sign is -1 or 1.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="root_sign"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// </acknowledgment>
        public static double G1Prime(double x, double a, double b, double c, double d, double e, double f, double root_sign)
        {
            var numerator = 2 * (b * x + e) * b - 4 * c * (2 * a * x + d);
            var denominator = 2 * Sqrt((b * x + e) * (b * x + e) - 4 * c * (a * x * x + d * x + f));
            var result = -b + root_sign * numerator / denominator;
            result = result / 2 / c;
            return result;
        }

        /// <summary>
        /// Calculate G(x).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// </acknowledgment>
        public static double G(double x,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
            => G1(x, A1, B1, C1, D1, E1, F1, sign1)
            - G1(x, A2, B2, C2, D2, E2, F2, sign2);

        /// <summary>
        /// Calculate G'(x).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// </acknowledgment>
        public static double GPrime(double x,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
            => G1Prime(x, A1, B1, C1, D1, E1, F1, sign1)
            - G1Prime(x, A2, B2, C2, D2, E2, F2, sign2);

        /// <summary>
        /// Return true if the number is not infinity or NaN.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
        /// </acknowledgment>
        public static bool IsNumber(double number)
            => !(double.IsNaN(number) || double.IsInfinity(number));
        #endregion Intersection of Ellipse and Ellipse

        #region Intersection of Ellipse and Line Segment
        /// <summary>
        /// Find the points of the intersection between an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool, (double, double)?, bool, (double, double)?) FindEllipseSegmentIntersections(
            double cx, double cy,
            double rx, double ry,
            double x0, double y0,
            double x1, double y1)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((cx == 0d) || (cy == 0d) ||
                ((x0 == x1) && (y0 == y1)))
            {
                return (false, null, false, null);
            }

            // Translate lines to meet the ellipse translated centered at the origin.
            var p1X = x0 - cx;
            var p1Y = y0 - cy;
            var p2X = x1 - cx;
            var p2Y = y1 - cy;

            // Calculate the quadratic parameters.
            var a = (p2X - p1X) * (p2X - p1X) / rx / rx + (p2Y - p1Y) * (p2Y - p1Y) / ry / ry;
            var b = 2d * p1X * (p2X - p1X) / rx / rx + 2 * p1Y * (p2Y - p1Y) / ry / ry;
            var c = p1X * p1X / rx / rx + p1Y * p1Y / ry / ry - 1d;

            // Calculate the discriminant.
            var discriminant = b * b - 4d * a * c;

            if (discriminant == 0)
            {
                // One real solution.
                var t = 0.5d * -b / a;

                // Return the point. If the point is on the segment set the bool to true.
                return ((t >= 0d) && (t <= 1d),
                        (p1X + (p2X - p1X) * t + cx,
                        p1Y + (p2Y - p1Y) * t + cy),
                        false, null);
            }
            else if (discriminant > 0)
            {
                // Two real solutions.
                var t1 = 0.5d * (-b + Sqrt(discriminant)) / a;
                var t2 = 0.5d * (-b - Sqrt(discriminant)) / a;

                // Return the points. If the points are on the segment set the bool to true.
                return ((t1 >= 0d) && (t1 <= 1d), (p1X + (p2X - p1X) * t1 + cx, p1Y + (p2Y - p1Y) * t1 + cy),
                        (t2 >= 0d) && (t2 <= 1d), (p1X + (p2X - p1X) * t2 + cx, p1Y + (p2Y - p1Y) * t2 + cy));
            }

            // Return the points.
            return (false, null, false, null);
        }

        /// <summary>
        /// The ellipse line segment.
        /// </summary>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="rX">The rX.</param>
        /// <param name="rY">The rY.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://forums.codeguru.com/showthread.php?157823-How-to-get-ellipse-and-line-Intersection-points
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool, (double, double)?, bool, (double, double)?) EllipseLineSegment(
            double cX, double cY,
            double rX, double rY,
            double x1, double y1,
            double x2, double y2)
        {
            double a;
            double b;
            double c;
            var m = 0d;

            // Check whether line is vertical.
            if (x1 != x2)
            {
                m = (y2 - y1) / (x2 - x1);
                var cc = y1 - m * x1;

                // Non-vertical line case.
                a = rY * rY + rX * rX * m * m;
                b = 2d * rX * rX * cc * m - 2d * rX * rX * cY * m - 2d * cX * rY * rY;
                c = rY * rY * cX * cX + rX * rX * cc * cc - 2 * rX * rX * cY * cc + rX * rX * cY * cY - rX * rX * rY * rY;
            }
            else
            {
                // vertical line case.
                a = rX * rX;
                b = -2d * cY * rX * rX;
                c = -rX * rX * rY * rY + rY * rY * (x1 - cX) * (x1 - cX);
            }

            var discriminant = b * b - 4d * a * c;

            if (discriminant == 0)
            {
                if (x1 != x2)
                {
                    var t = 0.5d * -b / a;
                    return ((t >= 0d) && (t <= 1d), (t, y1 + m * (t - x1)), false, null);
                }
                else
                {
                    var t = 0.5d * -b / a;
                    return ((t >= 0d) && (t <= 1d), (x1, t), false, null);
                }
            }
            else if (discriminant > 0d)
            {
                if (x1 != x2)
                {
                    var t1 = (-b + Sqrt(discriminant)) / (2d * a);
                    var t2 = (-b - Sqrt(discriminant)) / (2d * a);
                    return ((t1 >= 0d) && (t1 <= 1d), (t1, y1 + m * (t1 - x1)),
                            (t2 >= 0d) && (t2 <= 1d), (t2, y1 + m * (t2 - x1)));
                }
                else
                {
                    var t1 = (-b + Sqrt(discriminant)) / (2d * a);
                    var t2 = (-b - Sqrt(discriminant)) / (2d * a);
                    return ((t1 >= 0d) && (t1 <= 1d), (x1, t1),
                            (t2 >= 0d) && (t2 <= 1d), (x2, t2));
                }
            }
            else
            {
                // no intersections
                return (false, null, false, null);
            }
        }

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipseLineSegmentIntersection2(
            double cx, double cy,
            double rx, double ry,
            double x0, double y0,
            double x1, double y1,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
            {
                return result;
            }

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x0 - cx;
            var v1 = y0 - cy;
            var u2 = x1 - cx;
            var v2 = y1 - cy;

            // Calculate the quadratic parameters.
            var a = (u2 - u1) * (u2 - u1) / (rx * rx) + (v2 - v1) * (v2 - v1) / (ry * ry);
            var b = 2d * u1 * (u2 - u1) / (rx * rx) + 2d * v1 * (v2 - v1) / (ry * ry);
            var c = u1 * u1 / (rx * rx) + v1 * v1 / (ry * ry) - 1d;

            // Calculate the discriminant.
            var discriminant = b * b - 4d * a * c;

            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                var t = 0.5d * -b / a;

                // Add the points if it is between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t + cx, v1 + (v2 - v1) * t + cy));
                    result.State = IntersectionState.Intersection;
                }
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var t1 = 0.5d * (-b + Sqrt(discriminant)) / a;
                var t2 = 0.5d * (-b - Sqrt(discriminant)) / a;

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) && (t1 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t1 + cx, v1 + (v2 - v1) * t1 + cy));
                    result.State = IntersectionState.Intersection;
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy));
                    result.State = IntersectionState.Intersection;
                }
            }

            return result;
        }

        /// <summary>
        /// The unrotated ellipse line segment intersection.
        /// </summary>
        /// <param name="centerX">The centerX.</param>
        /// <param name="centerY">The centerY.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <returns>The <see cref="Intersection"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipseLineSegmentIntersection(
            double centerX, double centerY,
            double rx, double ry,
            double a1X, double a1Y,
            double a2X, double a2Y)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var origin = new Vector2D(a1X, a1Y);
            var dir = new Vector2D(a1X, a1Y, a2X, a2Y);
            var diff = origin.Subtract(centerX, centerY);
            var mDir = new Vector2D(dir.I / (rx * rx), dir.J / (ry * ry));
            var mDiff = new Vector2D(diff.I / (rx * rx), diff.J / (ry * ry));
            var a = dir.DotProduct(mDir);
            var b = dir.DotProduct(mDiff);
            var c = diff.DotProduct(mDiff) - 1.0;
            var d = b * b - a * c;
            if (d < 0)
            {
                result = new Intersection(IntersectionState.Outside);
            }
            else if (d > 0)
            {
                var root = Sqrt(d);
                var t_a = (-b - root) / a;
                var t_b = (-b + root) / a;
                if ((t_a < 0 || 1 < t_a) && (t_b < 0 || 1 < t_b))
                {
                    result = (t_a < 0 && t_b < 0) || (t_a > 1 && t_b > 1) ? new Intersection(IntersectionState.Outside) : new Intersection(IntersectionState.Inside);
                }
                else
                {
                    result = new Intersection(IntersectionState.Intersection);
                    if (0 <= t_a && t_a <= 1)
                    {
                        result.AppendPoint(Lerp(a1X, a1Y, a2X, a2Y, t_a));
                    }

                    if (0 <= t_b && t_b <= 1)
                    {
                        result.AppendPoint(Lerp(a1X, a1Y, a2X, a2Y, t_b));
                    }
                }
            }
            else
            {
                var t = -b / a; if (0 <= t && t <= 1)
                {
                    result = new Intersection(IntersectionState.Intersection);
                    result.AppendPoint(Lerp(a1X, a1Y, a2X, a2Y, t));
                }
                else
                {
                    result = new Intersection(IntersectionState.Outside);
                }
            }

            return result;
        }

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="angle"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection EllipseLineSegmentIntersection(
            double cx, double cy,
            double rx, double ry,
            double angle,
            double x0, double y0,
            double x1, double y1,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
            {
                return result;
            }

            // Get the Sine and Cosine of the angle.
            var sinA = Sin(angle);
            var cosA = Cos(angle);

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x0 - cx;
            var v1 = y0 - cy;
            var u2 = x1 - cx;
            var v2 = y1 - cy;

            // Apply Rotation Transform to line at the origin.
            var u1A = 0 + (u1 * cosA - v1 * sinA);
            var v1A = 0 + (u1 * sinA + v1 * cosA);
            var u2A = 0 + (u2 * cosA - v2 * sinA);
            var v2A = 0 + (u2 * sinA + v2 * cosA);

            // Calculate the quadratic parameters.
            var a = (u2A - u1A) * (u2A - u1A) / (rx * rx) + (v2A - v1A) * (v2A - v1A) / (ry * ry);
            var b = 2d * u1A * (u2A - u1A) / (rx * rx) + 2d * v1A * (v2A - v1A) / (ry * ry);
            var c = u1A * u1A / (rx * rx) + v1A * v1A / (ry * ry) - 1d;

            // Calculate the discriminant.
            var discriminant = b * b - 4d * a * c;

            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                var t = 0.5d * -b / a;

                // Add the point if it is between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t + cx, v1 + (v2 - v1) * t + cy));
                    result.State = IntersectionState.Intersection;
                }

            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var t1 = 0.5d * (-b + Sqrt(discriminant)) / a;
                var t2 = 0.5d * (-b - Sqrt(discriminant)) / a;

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) && (t1 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t1 + cx, v1 + (v2 - v1) * t1 + cy));
                    result.State = IntersectionState.Intersection;
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy));
                    result.State = IntersectionState.Intersection;
                }

                // ToDo: Figure out why the results are weird between 30 degrees and 5 degrees.
            }

            return result;
        }

        /// <summary>
        /// Finds the Intersection of a Ellipse and a Line
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool, (double, double)?, bool, (double, double)?) Intersect(Ellipse ellipse, LineSegment line)
        {
            var slopeA = line.Slope();
            var slopeB = line.A.Y - (slopeA * line.A.X);

            var a = 1 + (slopeA * slopeA);
            var b = (2 * (slopeA * (slopeB - ellipse.Center.Y))) - (2d * ellipse.Center.X);
            var c = (ellipse.Center.X * ellipse.Center.X) + (((slopeB - ellipse.Center.Y) * (slopeB - ellipse.Center.X)) - (ellipse.MajorRadius * ellipse.MajorRadius));

            var xA = ((b * -1) + Sqrt((b * b) - (a * c))) / (2d * a);
            var xB = (b - Sqrt((b * b) - (a * c))) * -1d / (2d * a);
            var yA = (slopeA * xA) + slopeB;
            var yB = (slopeA * xB) + slopeB;

            return (true, (xA, yA), true, (xB, yB));
        }
        #endregion Intersection of Ellipse and Line Segment

        #region Intersection of Elliptical Arc and Line Segment
        /// <summary>
        /// The unrotated elliptical arc line segment intersection.
        /// </summary>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <returns>The <see cref="Intersection"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipticalArcLineSegmentIntersection(
            double cx, double cy,
            double rx, double ry,
            double startAngle, double sweepAngle,
            double x0, double y0,
            double x1, double y1)
        {
            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate the line to put the ellipse centered at the origin.
            var origin = new Vector2D(x0, y0);
            var dir = new Vector2D(x0, y0, x1, y1);
            var diff = origin.Subtract(cx, cy);
            var mDir = new Vector2D(dir.I / (rx * rx), dir.J / (ry * ry));
            var mDiff = new Vector2D(diff.I / (rx * rx), diff.J / (ry * ry));

            // Calculate the quadratic parameters.
            var a = dir.DotProduct(mDir);
            var b = dir.DotProduct(mDiff);
            var c = diff.DotProduct(mDiff) - 1d;

            // Calculate the discriminant of the quadratic. The 4 is omitted.
            var discriminant = b * b - a * c;

            // Check whether line segment is outside of the ellipse.
            if (discriminant < 0)
            {
                return new Intersection(IntersectionState.Outside);
            }

            // Find the start and end angles.
            var sa = EllipticalPolarAngle(startAngle, rx, ry);
            var ea = EllipticalPolarAngle(startAngle + sweepAngle, rx, ry);

            // Get the ellipse rotation transform.
            var cosT = Cos(0);
            var sinT = Sin(0);

            // Ellipse equation for an ellipse at origin for the chord end points.
            var u1 = rx * Cos(sa);
            var v1 = -(ry * Sin(sa));
            var u2 = rx * Cos(ea);
            var v2 = -(ry * Sin(ea));

            // Find the points of the chord.
            var sX = cx + (u1 * cosT + v1 * sinT);
            var sY = cy + (u1 * sinT - v1 * cosT);
            var eX = cx + (u2 * cosT + v2 * sinT);
            var eY = cy + (u2 * sinT - v2 * cosT);

            if (discriminant > 0)
            {
                // Two real possible solutions.
                var root = Sqrt(discriminant);
                var t1 = (-b - root) / a;
                var t2 = (-b + root) / a;
                if ((t1 < 0 || 1 < t1) && (t2 < 0 || 1 < t2))
                {
                    result = (t1 < 0 && t2 < 0) || (t1 > 1 && t2 > 1) ? new Intersection(IntersectionState.Outside) : new Intersection(IntersectionState.Inside);
                }
                else
                {
                    var p = Lerp(x0, y0, x1, y1, t1);
                    // Find the determinant of the chord.
                    var determinant = (sX - p.X) * (eY - p.Y) - (eX - p.X) * (sY - p.Y);

                    // Check whether the point is on the side of the chord as the center.
                    if (0 <= t1 && t1 <= 1 && (Sign(determinant) != Sign(sweepAngle)))
                    {
                        result.AppendPoint(p);
                    }

                    p = Lerp(x0, y0, x1, y1, t2);
                    // Find the determinant of the chord.
                    determinant = (sX - p.X) * (eY - p.Y) - (eX - p.X) * (sY - p.Y);
                    if (0 <= t2 && t2 <= 1 && (Sign(determinant) != Sign(sweepAngle)))
                    {
                        result.AppendPoint(p);
                    }
                }
            }
            else // discriminant == 0.
            {
                // One real possible solution.
                var t = -b / a;
                if (t >= 0d && t <= 1d)
                {
                    var p = Lerp(x0, y0, x1, y1, t);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = (sX - p.X) * (eY - p.Y) - (eX - p.X) * (sY - p.Y);

                    // Add the point if it is on the sweep side of the chord.
                    if (Sign(determinant) != Sign(sweepAngle))
                    {
                        result.AppendPoint(Lerp(x0, y0, x1, y1, t));
                    }
                }
                else
                {
                    result = new Intersection(IntersectionState.Outside);
                }
            }

            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }
        #endregion Intersection of Elliptical Arc and Line Segment

        #region Intersection of Parabola and Hyperbola
        //http://csharphelper.com/blog/2014/11/see-where-a-parabola-and-hyperbola-intersect-in-c/
        #endregion Intersection of Parabola and Hyperbola

        #region Intersection Of Two Line Segments
        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(LineIntersection2DTests))]
        public static List<SpeedTester> LineIntersection2DTests()
            => new List<SpeedTester> {
                new SpeedTester(() => Intersection0(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection0)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection1(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection1)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection2(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection2)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection3(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection3)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection4(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection4)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection5(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection5)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => FindIntersection(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.FindIntersection)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => LineIntersection(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.LineIntersection)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => LineSegmentIntersection(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.LineSegmentIntersection)}(0, 0, 2, 2, 0, 2, 2, 0)")
            };

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="x4"></param>
        /// <param name="y4"></param>
        /// <returns>Returns the point of intersection.</returns>
        public static (bool intersects, (double X, double Y)? point) Intersection0(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4)
        {
            // Calculate the delta length vectors for the line segments.
            var deltaBAI = x2 - x1;
            var deltaBAJ = y2 - y1;
            var deltaDCI = x4 - x3;
            var deltaDCJ = y4 - y3;
            var deltaCAI = x3 - x1;
            var deltaCAJ = y3 - y1;

            //  If the segments are parallel return false.
            if (Abs((deltaDCI * deltaBAJ) - (deltaDCJ * deltaBAI)) < DoubleEpsilon)
            {
                return (false, null);
            }

            // Find the index where the intersection point lies on the line.
            var s = ((deltaBAI * deltaCAJ) + (deltaBAJ * -deltaCAI)) / ((deltaDCI * deltaBAJ) - (deltaDCJ * deltaBAI));
            var t = ((deltaDCI * -deltaCAJ) + (deltaDCJ * deltaCAI)) / ((deltaDCJ * deltaBAI) - (deltaDCI * deltaBAJ));

            return (
                // Check whether the point is on the segment.
                (s >= 0d) && (s <= 1d) && (t >= 0d) && (t <= 1d),
                // If the point exists, the point of intersection is:
                (x1 + (t * deltaBAI), y1 + (t * deltaBAJ)));
        }

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x1">The x component of the first point of the first line.</param>
        /// <param name="y1">The y component of the first point of the first line.</param>
        /// <param name="x2">The x component of the second point of the first line.</param>
        /// <param name="y2">The y component of the second point of the first line.</param>
        /// <param name="x3">The x component of the first point of the second line.</param>
        /// <param name="y3">The y component of the first point of the second line.</param>
        /// <param name="x4">The x component of the second point of the second line.</param>
        /// <param name="y4">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <acknowledgment>
        /// https://www.topcoder.com/community/data-science/data-science-tutorials/geometry-concepts-line-intersection-and-its-applications/
        /// </acknowledgment>
        public static (bool, (double X, double Y)?) Intersection1(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4)
        {
            // Calculate the delta length vectors for the line segments.
            var deltaAI = x1 - x2;
            var deltaAJ = y2 - y1;
            var deltaBI = y4 - y3;
            var deltaBJ = x3 - x4;

            // Calculate the determinant of the vectors.
            var determinant = (deltaAJ * deltaBJ) - (deltaBI * deltaAI);

            // Check if the lines are parallel.
            if (Abs(determinant) < DoubleEpsilon)
            {
                return (false, null);
            }

            // Find the index where the intersection point lies on the line.
            var s = (deltaAJ * x1 + deltaAI * y1) / -determinant;
            var t = (deltaBI * x3 + deltaBJ * y3) / determinant;

            // Interpolate the point of intersection.
            return (
                // Check whether the point is on the segment.
                (s >= 0d) && (s <= 1d) && (t >= 0d) && (t <= 1d),
                // If the point exists, the point of intersection is:
                (-((deltaAI * t) + (deltaBJ * s)), (deltaAJ * t) + (deltaBI * s)));
        }

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x1">The x component of the first point of the first line.</param>
        /// <param name="y1">The y component of the first point of the first line.</param>
        /// <param name="x2">The x component of the second point of the first line.</param>
        /// <param name="y2">The y component of the second point of the first line.</param>
        /// <param name="x3">The x component of the first point of the second line.</param>
        /// <param name="y3">The y component of the first point of the second line.</param>
        /// <param name="x4">The x component of the second point of the second line.</param>
        /// <param name="y4">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        public static (bool, (double X, double Y)?) Intersection2(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4)
        {
            // Calculate the delta length vectors for the line segments.
            var deltaAI = x2 - x1;
            var deltaAJ = y2 - y1;
            var deltaBI = x4 - x3;
            var deltaBJ = y4 - y3;

            // Calculate the determinant of the coefficient matrix.
            var determinant = (deltaBJ * deltaAI) - (deltaBI * deltaAJ);

            // Check if the line are parallel.
            if (Abs(determinant) < DoubleEpsilon)
            {
                return (false, null);
            }

            // Find the index where the intersection point lies on the line.
            var s = ((x1 - x3) * deltaAJ + (y3 - y1) * deltaAI) / -determinant;
            var t = ((x3 - x1) * deltaBJ + (y1 - y3) * deltaBI) / determinant;

            return (
                 // Check whether the point is on the segment.
                 (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d),
                // If it exists, the point of intersection is:
                (x1 + t * deltaAI, y1 + t * deltaAJ));
        }

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x1">The x component of the first point of the first line.</param>
        /// <param name="y1">The y component of the first point of the first line.</param>
        /// <param name="x2">The x component of the second point of the first line.</param>
        /// <param name="y2">The y component of the second point of the first line.</param>
        /// <param name="x3">The x component of the first point of the second line.</param>
        /// <param name="y3">The y component of the first point of the second line.</param>
        /// <param name="x4">The x component of the second point of the second line.</param>
        /// <param name="y4">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/08/determine-where-two-lines-intersect-in-c/
        /// </acknowledgment>
        public static (bool, (double X, double Y)?) Intersection3(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4)
        {
            // Calculate the delta length vectors for the line segments.
            var deltaAI = x2 - x1;
            var deltaAJ = y2 - y1;
            var deltaBI = x4 - x3;
            var deltaBJ = y4 - y3;

            // Calculate the determinant of the coefficient matrix.
            var determinant = (deltaBI * deltaAJ) - (deltaBJ * deltaAI);

            // Check if the lines are parallel.
            if (Abs(determinant) < DoubleEpsilon)
            {
                return (false, null);
            }

            // Find the index where the intersection point lies on the line.
            var s = ((x3 - x1) * deltaAJ + (y1 - y3) * deltaAI) / -determinant;
            var t = ((x1 - x3) * deltaBJ + (y3 - y1) * deltaBI) / determinant;

            // Interpolate the point of intersection.
            return (
                // The segments intersect if t1 and t2 are between 0 and 1.
                (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d),
                // If it exists, the point of intersection is:
                (x1 + t * deltaAI, y1 + t * deltaAJ));

            //// Find the closest points on the segments.
            //if (t < 0) t = 0;
            //else if (t > 1) t = 1;
            //if (s < 0) s = 0;
            //else if (s > 1) s = 1;

            //Point2D close_p1 = new Point2D(aX + deltaAI * t, aY + deltaAJ * t);
            //Point2D close_p2 = new Point2D(cX + deltaBI * s, cY + deltaBJ * s);
        }

        /// <summary>
        /// SlopeMax is a large value "close to infinity" (Close to the largest value allowed for the data
        /// type). Used in the Slope of a LineSeg
        /// </summary>
        public const double SlopeMax = 9223372036854775807d;

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x0">The x component of the first point of the first line.</param>
        /// <param name="y0">The y component of the first point of the first line.</param>
        /// <param name="x1">The x component of the second point of the first line.</param>
        /// <param name="y1">The y component of the second point of the first line.</param>
        /// <param name="x2">The x component of the first point of the second line.</param>
        /// <param name="y2">The y component of the first point of the second line.</param>
        /// <param name="x3">The x component of the second point of the second line.</param>
        /// <param name="y3">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <acknowledgment>
        /// http://www.gamedev.net/page/resources/_/technical/math-and-physics/fast-2d-line-intersection-algorithm-r423
        /// </acknowledgment>
        public static (bool, (double X, double Y)?) Intersection4(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            // Compute the slopes of each line. Note the kludge for infinity, however, this will be close enough.
            var m1 = (Abs(x1 - x0) < DoubleEpsilon) ? SlopeMax : (y1 - y0) / (x1 - x0);
            var m2 = (Abs(x3 - x2) < DoubleEpsilon) ? SlopeMax : (y3 - y2) / (x3 - x2);

            // Check if the lines are parallel.
            if (Abs(m1 - m2) < DoubleEpsilon)
            {
                return (false, null);
            }

            // Compute the determinate of the coefficient matrix.
            var determinate = m2 - m1;

            var s = (y0 - (m1 * x0)) / determinate;
            var t = (y2 - (m2 * x2)) / -determinate;

            // Use Cramer's rule to compute the return values.
            return (
                (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d),
                (s + t, m2 * s + m1 * t));
        }

        /// <summary>
        /// Returns the intersection of the two lines (line segments are passed in, but they are treated like infinite lines)
        /// </summary>
        /// <acknowledgment>
        /// http://rosettacode.org/wiki/Sutherland-Hodgman_polygon_clipping#C.23
        /// Got this here:
        /// http://stackoverflow.com/questions/14480124/how-do-i-detect-triangle-and-rectangle-intersection
        /// </acknowledgment>
        public static (bool, (double X, double Y)?) Intersection5(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            var direction1I = x1 - x0;
            var direction1J = y1 - y0;
            var direction2I = x3 - x2;
            var direction2J = y3 - y2;

            var dotPerp = (direction1I * direction2J) - (direction1J * direction2I);

            // Check if the lines are parallel.
            if (Abs(dotPerp) < DoubleEpsilon)
            {
                return (false, null);
            }

            // If it's 0, it means the lines are parallel so have infinite intersection points
            if (NearZero0(dotPerp))
            {
                return (false, null);
            }

            var cI = x2 - x0;
            var cJ = y2 - y0;
            var t = (cI * direction2J - cJ * direction2I) / dotPerp;
            //if ((t < 0) || (t > 1)) return null; // lies outside the line segment

            var u = (cI * direction1J - cJ * direction1I) / dotPerp;
            //if ((u < 0) || (u > 1)) return null; // lies outside the line segment

            //	Return the intersection point
            return (
                (t > 0) && (t < 1) && (u > 0) && (u < 1),
                (
                x0 + (t * direction1I),
                y0 + (t * direction1J)));
        }

        /// <summary>
        /// Find the point of intersection between two lines.
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="Y1"></param>
        /// <param name="X2"></param>
        /// <param name="Y2"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/
        /// </acknowledgment>
        public static (bool, (double X, double Y)?) FindIntersection(
            double X1, double Y1,
            double X2, double Y2,
            double A1, double B1,
            double A2, double B2)
        {
            var dx = X2 - X1;
            var dy = Y2 - Y1;
            var da = A2 - A1;
            var db = B2 - B1;

            // If the segments are parallel, return False.
            if (Abs(da * dy - db * dx) < Epsilon)
            {
                return (false, null);
            }

            // Find the point of intersection.
            var s = (dx * (B1 - Y1) + dy * (X1 - A1)) / (da * dy - db * dx);
            var t = (da * (Y1 - B1) + db * (A1 - X1)) / (db * dx - da * dy);

            return (true, (X1 + t * dx, Y1 + t * dy));
        }

        /// <summary>
        ///  Determines the intersection point of the line defined by points A and B with the
        ///  line defined by points C and D.
        ///
        ///  Returns YES if the intersection point was found, and stores that point in X,Y.
        ///  Returns NO if there is no determinable intersection point, in which case X,Y will
        ///  be unmodified.
        ///  /// </summary>
        /// <param name="Ax"></param>
        /// <param name="Ay"></param>
        /// <param name="Bx"></param>
        /// <param name="By"></param>
        /// <param name="Cx"></param>
        /// <param name="Cy"></param>
        /// <param name="Dx"></param>
        /// <param name="Dy"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://alienryderflex.com/intersect/
        /// </acknowledgment>
        public static (bool, (double X, double Y)?) LineIntersection(
            double Ax, double Ay,
            double Bx, double By,
            double Cx, double Cy,
            double Dx, double Dy)
        {
            double distAB, theCos, theSin, newX, ABpos;

            //  Fail if either line is undefined.
            if (Ax == Bx && Ay == By || Cx == Dx && Cy == Dy)
            {
                return (false, null);
            }

            //  (1) Translate the system so that point A is on the origin.
            Bx -= Ax; By -= Ay;
            Cx -= Ax; Cy -= Ay;
            Dx -= Ax; Dy -= Ay;

            //  Discover the length of segment A-B.
            distAB = Sqrt(Bx * Bx + By * By);

            //  (2) Rotate the system so that point B is on the positive X axis.
            theCos = Bx / distAB;
            theSin = By / distAB;
            newX = Cx * theCos + Cy * theSin;
            Cy = Cy * theCos - Cx * theSin; Cx = newX;
            newX = Dx * theCos + Dy * theSin;
            Dy = Dy * theCos - Dx * theSin; Dx = newX;

            //  Fail if the lines are parallel.
            if (Cy == Dy)
            {
                return (false, null);
            }

            //  (3) Discover the position of the intersection point along line A-B.
            ABpos = Dx + (Cx - Dx) * Dy / (Dy - Cy);

            //  Success.
            //  (4) Apply the discovered position to line A-B in the original coordinate system.
            return (true, (Ax + ABpos * theCos, Ay + ABpos * theSin));
        }

        /// <summary>
        ///  Determines the intersection point of the line segment defined by points A and B
        ///  with the line segment defined by points C and D.
        ///
        ///  Returns YES if the intersection point was found, and stores that point in X,Y.
        ///  Returns NO if there is no determinable intersection point, in which case X,Y will
        ///  be unmodified.
        /// </summary>
        /// <param name="Ax"></param>
        /// <param name="Ay"></param>
        /// <param name="Bx"></param>
        /// <param name="By"></param>
        /// <param name="Cx"></param>
        /// <param name="Cy"></param>
        /// <param name="Dx"></param>
        /// <param name="Dy"></param>
        /// <returns></returns>
        /// <acknowledgment>
        ///  public domain function by Darel Rex Finley, 2006
        ///  http://alienryderflex.com/intersect/
        /// </acknowledgment>
        public static (bool, (double X, double Y)?) LineSegmentIntersection(
            double Ax, double Ay,
            double Bx, double By,
            double Cx, double Cy,
            double Dx, double Dy)
        {
            double distAB, theCos, theSin, newX, ABpos;

            //  Fail if either line segment is zero-length.
            if (Ax == Bx && Ay == By || Cx == Dx && Cy == Dy)
            {
                return (false, null);
            }

            //  Fail if the segments share an end-point.
            if (Ax == Cx && Ay == Cy || Bx == Cx && By == Cy
           || Ax == Dx && Ay == Dy || Bx == Dx && By == Dy)
            {
                return (false, null);
            }

            //  (1) Translate the system so that point A is on the origin.
            Bx -= Ax; By -= Ay;
            Cx -= Ax; Cy -= Ay;
            Dx -= Ax; Dy -= Ay;

            //  Discover the length of segment A-B.
            distAB = Sqrt(Bx * Bx + By * By);

            //  (2) Rotate the system so that point B is on the positive X axis.
            theCos = Bx / distAB;
            theSin = By / distAB;
            newX = Cx * theCos + Cy * theSin;
            Cy = Cy * theCos - Cx * theSin; Cx = newX;
            newX = Dx * theCos + Dy * theSin;
            Dy = Dy * theCos - Dx * theSin; Dx = newX;

            //  Fail if segment C-D doesn't cross line A-B.
            if (Cy < 0d && Dy < 0d || Cy >= 0d && Dy >= 0d)
            {
                return (false, null);
            }

            //  (3) Discover the position of the intersection point along line A-B.
            ABpos = Dx + (Cx - Dx) * Dy / (Dy - Cy);

            //  Fail if segment C-D crosses line A-B outside of segment A-B.
            if (ABpos < 0d || ABpos > distAB)
            {
                return (false, (Ax + ABpos * theCos, Ay + ABpos * theSin));
            }

            //  Success.
            //  (4) Apply the discovered position to line A-B in the original coordinate system.
            return (true, (Ax + ABpos * theCos, Ay + ABpos * theSin));
        }

        /// <summary>
        /// The intersect line line.
        /// </summary>
        /// <param name="Ax">The Ax.</param>
        /// <param name="Ay">The Ay.</param>
        /// <param name="Bx">The Bx.</param>
        /// <param name="By">The By.</param>
        /// <param name="Cx">The Cx.</param>
        /// <param name="Cy">The Cy.</param>
        /// <param name="Dx">The Dx.</param>
        /// <param name="Dy">The Dy.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-intersections
        /// </acknowledgment>
        public static List<Point2D> IntersectLineLine(
            double Ax, double Ay,
            double Bx, double By,
            double Cx, double Cy,
            double Dx, double Dy)
        {
            List<Point2D> result;

            var ua_t = (Dx - Cx) * (Ay - Cy) - (Dy - Cy) * (Ax - Cx);
            var ub_t = (Bx - Ax) * (Ay - Cy) - (By - Ay) * (Ax - Cx);
            var u_b = (Dy - Cy) * (Bx - Ax) - (Dx - Cx) * (By - Ay);

            if (u_b != 0)
            {
                var ua = ua_t / u_b;
                var ub = ub_t / u_b;

                result = 0 <= ua && ua <= 1 && 0 <= ub && ub <= 1 ? new List<Point2D>
                    {
                        new Point2D(
                            Ax + ua * (Bx - Ax),
                            Ay + ua * (By - Ay)
                        )
                    } : new List<Point2D>();
            }
            else
            {
                result = null;
            }

            return result;
        }
        #endregion Intersection Of Two Line Segments

        #region Intersection of a Line and a Line Segment
        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(LineLineSegmentIntersection2DTests))]
        public static List<SpeedTester> LineLineSegmentIntersection2DTests()
            => new List<SpeedTester> {
                new SpeedTester(() => LineLineSegmentIntersection0(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.LineLineSegmentIntersection0)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => LineLineSegmentIntersection1(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.LineLineSegmentIntersection1)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => LineLineSegmentIntersection2(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.LineLineSegmentIntersection2)}(0, 0, 2, 2, 0, 2, 2, 0)"),
            };

        /// <summary>
        /// The line line segment intersection0.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="dX">The dX.</param>
        /// <param name="dY">The dY.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="Intersection"/>.</returns>
        /// <acknowledgment>
        /// https://www.gamedev.net/topic/488904-lineline-segment-intersection-in-2d/
        /// </acknowledgment>
        public static Intersection LineLineSegmentIntersection0(
            double aX, double aY,
            double bX, double bY, //Line
            double cX, double cY,
            double dX, double dY,  // Segment
            double epsilon = Epsilon)
        {
            //test for parallel case
            var denom = (dY - cY) * (bX - aX) - (dX - cX) * (bY - aY);
            if (Abs(denom) < epsilon)
            {
                return new Intersection(IntersectionState.NoIntersection);
            }

            //calculate segment parameter and ensure its within bounds
            var t = ((bX - aX) * (aY - cY) - (bY - aY) * (aX - cX)) / denom;
            if (t < 0d || t > 1d)
            {
                return new Intersection(IntersectionState.NoIntersection);
            }

            //store actual intersection
            var result = new Intersection(IntersectionState.Intersection);
            result.AppendPoint(new Point2D(cX + (dX - cX) * t, cY + (dY - cY) * t));
            return result;

        }

        /// <summary>
        /// Find the intersection point between two line segments.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineLineSegmentIntersection1(
                double a1X, double a1Y,
                double a2X, double a2Y,
                double b1X, double b1Y,
                double b2X, double b2Y)
        {
            Intersection result;
            var ua_t = (b2X - b1X) * (a1Y - b1Y) - (b2Y - b1Y) * (a1X - b1X);
            var ub_t = (a2X - a1X) * (a1Y - b1Y) - (a2Y - a1Y) * (a1X - b1X);
            var u_b = (b2Y - b1Y) * (a2X - a1X) - (b2X - b1X) * (a2Y - a1Y);
            if (u_b != 0)
            {
                var ua = ua_t / u_b;
                var ub = ub_t / u_b;
                if (//true
                    //0 <= ua && ua <= 1
                    //&&
                    0 <= ub && ub <= 1
                    )
                {
                    result = new Intersection(IntersectionState.Intersection);
                    result.AppendPoint(new Point2D(a1X + ua * (a2X - a1X), a1Y + ua * (a2Y - a1Y)));
                    //result.AppendPoint(new Point2D(b1X + ub * (b2X - b1X), b1Y + ub * (b2Y - b1Y)));
                }
                else
                {
                    result = new Intersection(IntersectionState.NoIntersection);
                }
            }
            else
            {
                if (ua_t == 0 || ub_t == 0)
                {
                    result = new Intersection(IntersectionState.Coincident | IntersectionState.Parallel | IntersectionState.Intersection);
                    result.AppendPoints(new List<Point2D> { new Point2D(b1X, b1Y), new Point2D(b2X, b2Y) });
                }
                else
                {
                    result = new Intersection(IntersectionState.Parallel | IntersectionState.NoIntersection);
                }
            }
            return result;
        }

        /// <summary>
        /// Find the intersection point between a line and a line segment.
        /// </summary>
        /// <param name="x0">The x component of the first point of the first line.</param>
        /// <param name="y0">The y component of the first point of the first line.</param>
        /// <param name="x1">The x component of the second point of the first line.</param>
        /// <param name="y1">The y component of the second point of the first line.</param>
        /// <param name="x2">The x component of the first point of the second line.</param>
        /// <param name="y2">The y component of the first point of the second line.</param>
        /// <param name="x3">The x component of the second point of the second line.</param>
        /// <param name="y3">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineLineSegmentIntersection2(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate lines to origin.
            var u1 = x1 - x0;
            var v1 = y1 - y0;
            var u2 = x3 - x2;
            var v2 = y3 - y2;

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the lines are parallel or coincident.
            if (Abs(determinant) < Epsilon)
            {
                return result;
            }

            // Find the index where the intersection point lies on the line.
            //var s = ((x0 - x2) * v1 + (y2 - y0) * u1) / -determinant;
            var t = ((x2 - x0) * v2 + (y0 - y2) * u2) / determinant;

            // Check whether the point is on the segment.
            if (t >= 0d && t <= 1d)
            {
                result = new Intersection(IntersectionState.Intersection);
                result.AppendPoint(new Point2D(x0 + t * u1, y0 + t * v1));
            }

            return result;
        }
        #endregion Intersection of a Line and a Line Segment

        #region Intersection of a Line and a Quadratic Bézier
        /// <summary>
        /// The line quadratic bezier intersection.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="p1X">The p1X.</param>
        /// <param name="p1Y">The p1Y.</param>
        /// <param name="p2X">The p2X.</param>
        /// <param name="p2Y">The p2Y.</param>
        /// <param name="p3X">The p3X.</param>
        /// <param name="p3Y">The p3Y.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>The <see cref="Intersection"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineQuadraticBezierIntersection(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double epsilon = Epsilon)
        {
            var min = MinPoint(a1X, a1Y, a2X, a2Y);
            var max = MaxPoint(a1X, a1Y, a2X, a2Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            var a = new Vector2D(p2X, p2Y).Scale(-2);
            var c2 = new Vector2D(p1X, p1Y).Add(a.Add(new Vector2D(p3X, p3Y)));
            a = new Vector2D(p1X, p1Y).Scale(-2);
            var b = new Vector2D(p2X, p2Y).Scale(2);
            var c1 = a.Add(b);
            var c0 = new Point2D(p1X, p1Y);
            var n = new Point2D(a1Y - a2Y, a2X - a1X);
            var cl = a1X * a2Y - a2X * a1Y;
            var roots = QuadraticRoots(
                n.DotProduct(c2),
                n.DotProduct(c1),
                n.DotProduct(c0) + cl,
                epsilon);
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                if (0 <= t && t <= 1)
                {
                    Point2D p4 = Lerp(p1X, p1Y, p2X, p2Y, t);
                    Point2D p5 = Lerp(p2X, p2Y, p3X, p3Y, t);
                    Point2D p6 = Lerp(p4.X, p4.Y, p5.X, p5.Y, t);
                    if (a1X == a2X)
                    {
                        result.AppendPoint(p6);
                    }
                    else if (a1Y == a2Y)
                    {
                        result.AppendPoint(p6);
                    }
                    else if (p6.GreaterThanOrEqual(min) && p6.LessThanOrEqual(max))
                    {
                        result.AppendPoint(p6);
                    }
                }
            }

            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }
        #endregion Intersection of a Line and a Quadratic Bézier

        #region Intersection of a Line and a Cubic Bézier
        /// <summary>
        /// The line cubic bezier intersection0.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="p1X">The p1X.</param>
        /// <param name="p1Y">The p1Y.</param>
        /// <param name="p2X">The p2X.</param>
        /// <param name="p2Y">The p2Y.</param>
        /// <param name="p3X">The p3X.</param>
        /// <param name="p3Y">The p3Y.</param>
        /// <param name="p4X">The p4X.</param>
        /// <param name="p4Y">The p4Y.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>The <see cref="Intersection"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineCubicBezierIntersection0(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double p4X, double p4Y,
            double epsilon = Epsilon)
        {
            Vector2D a, b, c, d;
            Vector2D c3, c2, c1, c0;
            double cl;
            Vector2D n;
            var min = MinPoint(a1X, a1Y, a2X, a2Y);
            var max = MaxPoint(a1X, a1Y, a2X, a2Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            a = new Vector2D(p1X, p1Y).Scale(-1);
            b = new Vector2D(p2X, p2Y).Scale(3);
            c = new Vector2D(p3X, p3Y).Scale(-3);
            d = a.Add(b.Add(c.Add(new Vector2D(p4X, p4Y))));
            c3 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y).Scale(3);
            b = new Vector2D(p2X, p2Y).Scale(-6);
            c = new Vector2D(p3X, p3Y).Scale(3);
            d = a.Add(b.Add(c));
            c2 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y).Scale(-3);
            b = new Vector2D(p2X, p2Y).Scale(3);
            c = a.Add(b);
            c1 = new Vector2D(c.I, c.J);
            c0 = new Vector2D(p1X, p1Y);
            n = new Vector2D(a1Y - a2Y, a2X - a1X);
            cl = a1X * a2Y - a2X * a1Y;
            var roots = CubicRoots(
                n.DotProduct(c3),
                n.DotProduct(c2),
                n.DotProduct(c1),
                n.DotProduct(c0) + cl,
                epsilon);
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                if (0 <= t && t <= 1)
                {
                    var p5 = Lerp(p1X, p1Y, p2X, p2Y, t);
                    var p6 = Lerp(p2X, p2Y, p3X, p3Y, t);
                    var p7 = Lerp(p3X, p3Y, p4X, p4Y, t);
                    var p8 = Lerp(p5.X, p5.Y, p6.X, p6.Y, t);
                    var p9 = Lerp(p6.X, p6.Y, p7.X, p7.Y, t);
                    var p10 = Lerp(p8.X, p8.Y, p9.X, p9.Y, t);
                    if (a1X == a2X)
                    {
                        result.State = IntersectionState.Intersection;
                        result.AppendPoint(p10);
                    }
                    else if (a1Y == a2Y)
                    {
                        result.State = IntersectionState.Intersection;
                        result.AppendPoint(p10);
                    }
                    else if (p10.GreaterThanOrEqual(min) && p10.LessThanOrEqual(max))
                    {
                        result.State = IntersectionState.Intersection;
                        result.AppendPoint(p10);
                    }
                }
            }

            return result;
        }
        #endregion Intersection of a Line and a Cubic Bézier

        #region Intersection of a Line Segment and a Quadratic Bézier
        /// <summary>
        /// The quadratic bezier line segment intersection1.
        /// </summary>
        /// <param name="p1X">The p1X.</param>
        /// <param name="p1Y">The p1Y.</param>
        /// <param name="p2X">The p2X.</param>
        /// <param name="p2Y">The p2Y.</param>
        /// <param name="p3X">The p3X.</param>
        /// <param name="p3Y">The p3Y.</param>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <returns>The <see cref="Intersection"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/27664298/calculating-intersection-point-of-quadratic-bezier-curve
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierLineSegmentIntersection1(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double a1X, double a1Y,
            double a2X, double a2Y)
        {
            var intersections = new Intersection(IntersectionState.NoIntersection);

            // inverse line normal
            var normal = new Point2D(a1Y - a2Y, a2X - a1X);

            // Q-coefficients
            var c2 = new Point2D(p1X + p2X * -2 + p3X, p1Y + p2Y * -2 + p3Y);
            var c1 = new Point2D(p1X * -2 + p2X * 2, p1Y * -2 + p2Y * 2);
            var c0 = new Point2D(p1X, p1Y);

            // Transform to line
            var coefficient = a1X * a2Y - a2X * a1Y;
            var a = normal.X * c2.X + normal.Y * c2.Y;
            var b = (normal.X * c1.X + normal.Y * c1.Y) / a;
            var c = (normal.X * c0.X + normal.Y * c0.Y + coefficient) / a;

            // solve the roots
            var roots = new List<double>();
            var d = b * b - 4 * c;
            if (d > 0)
            {
                var e = Sqrt(d);
                roots.Add((-b + Sqrt(d)) / 2);
                roots.Add((-b - Sqrt(d)) / 2);
            }
            else if (d == 0)
            {
                roots.Add(-b / 2);
            }

            // calc the solution points
            for (var i = 0; i < roots.Count; i++)
            {
                var minX = Min(a1X, a2X);
                var minY = Min(a1Y, a2Y);
                var maxX = Max(a1X, a2X);
                var maxY = Max(a1Y, a2Y);
                var t = roots[i];
                if (t >= 0 && t <= 1)
                {
                    // possible point -- pending bounds check
                    var point = new Point2D(
                        Interpolators.Linear(Interpolators.Linear(p1X, p2X, t), Interpolators.Linear(p2X, p3X, t), t),
                        Interpolators.Linear(Interpolators.Linear(p1Y, p2Y, t), Interpolators.Linear(p2Y, p3Y, t), t));
                    var x = point.X;
                    var y = point.Y;
                    var result = new Intersection(IntersectionState.Intersection);
                    // bounds checks
                    if (a1X == a2X && y >= minY && y <= maxY)
                    {
                        // vertical line
                        intersections.AppendPoint(point);
                    }
                    else if (a1Y == a2Y && x >= minX && x <= maxX)
                    {
                        // horizontal line
                        intersections.AppendPoint(point);
                    }
                    else if (x >= minX && y >= minY && x <= maxX && y <= maxY)
                    {
                        // line passed bounds check
                        intersections.AppendPoint(point);
                    }
                }
            }
            return intersections;
        }

        /// <summary>
        /// The quadratic bezier line segment intersection.
        /// </summary>
        /// <param name="p1X">The p1X.</param>
        /// <param name="p1Y">The p1Y.</param>
        /// <param name="p2X">The p2X.</param>
        /// <param name="p2Y">The p2Y.</param>
        /// <param name="p3X">The p3X.</param>
        /// <param name="p3Y">The p3Y.</param>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>The <see cref="Intersection"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierLineSegmentIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double a1X, double a1Y,
            double a2X, double a2Y,
            double epsilon = Epsilon)
        {
            var min = MinPoint(a1X, a1Y, a2X, a2Y);
            var max = MaxPoint(a1X, a1Y, a2X, a2Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            var a = new Vector2D(p2X, p2Y).Scale(-2);
            var c2 = new Vector2D(p1X, p1Y).Add(a.Add(new Vector2D(p3X, p3Y)));
            a = new Vector2D(p1X, p1Y).Scale(-2);
            var b = new Vector2D(p2X, p2Y).Scale(2);
            var c1 = a.Add(b);
            var c0 = new Point2D(p1X, p1Y);
            var n = new Point2D(a1Y - a2Y, a2X - a1X);
            var cl = a1X * a2Y - a2X * a1Y;
            var roots = QuadraticRoots(
                n.DotProduct(c2),
                n.DotProduct(c1),
                n.DotProduct(c0) + cl,
                epsilon);
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                if (0 <= t && t <= 1)
                {
                    Point2D p4 = Lerp(p1X, p1Y, p2X, p2Y, t);
                    Point2D p5 = Lerp(p2X, p2Y, p3X, p3Y, t);
                    Point2D p6 = Lerp(p4.X, p4.Y, p5.X, p5.Y, t);
                    if (a1X == a2X)
                    {
                        if (min.Y <= p6.Y && p6.Y <= max.Y)
                        {
                            result.State = IntersectionState.Intersection;
                            result.AppendPoint(p6);
                        }
                    }
                    else if (a1Y == a2Y)
                    {
                        if (min.X <= p6.X && p6.X <= max.X)
                        {
                            result.State = IntersectionState.Intersection;
                            result.AppendPoint(p6);
                        }
                    }
                    else if (p6.GreaterThanOrEqual(min) && p6.LessThanOrEqual(max))
                    {
                        result.State = IntersectionState.Intersection;
                        result.AppendPoint(p6);
                    }
                }
            }
            return result;
        }
        #endregion Intersection of a Line Segment and a Quadratic Bézier

        #region Intersection of a Line Segment and a Cubic Bézier
        /// <summary>
        /// The cubic bezier line segment intersection1.
        /// </summary>
        /// <param name="p0x">The p0x.</param>
        /// <param name="p0y">The p0y.</param>
        /// <param name="p1x">The p1x.</param>
        /// <param name="p1y">The p1y.</param>
        /// <param name="p2x">The p2x.</param>
        /// <param name="p2y">The p2y.</param>
        /// <param name="p3x">The p3x.</param>
        /// <param name="p3y">The p3y.</param>
        /// <param name="l0x">The l0x.</param>
        /// <param name="l0y">The l0y.</param>
        /// <param name="l1x">The l1x.</param>
        /// <param name="l1y">The l1y.</param>
        /// <returns>The <see cref="Intersection"/>.</returns>
        /// <acknowledgment>
        /// This method has an error where it does not return an intersection with a horizontal line and the end points of the curve share the same y value, as well as the handles sharing another y value.
        /// Found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Based on code now found at: http://www.abecedarical.com/javascript/script_cubic.html
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierLineSegmentIntersection1(
            double p0x, double p0y,
            double p1x, double p1y,
            double p2x, double p2y,
            double p3x, double p3y,
            double l0x, double l0y,
            double l1x, double l1y)
        {
            // ToDo: Figure out why this can't handle intersection with horizontal lines.
            var I = new Intersection(IntersectionState.NoIntersection);

            var A = l1y - l0y;      //A=y2-y1
            var B = l0x - l1x;      //B=x1-x2
            var C = l0x * (l0y - l1y) + l0y * (l1x - l0x);  //C=x1*(y1-y2)+y1*(x2-x1)

            var xCoeff = CubicBezierCoefficients(p0x, p1x, p2x, p3x);
            var yCoeff = CubicBezierCoefficients(p0y, p1y, p2y, p3y);

            var r = CubicRoots(
                /* t^3 */ A * xCoeff.D + B * yCoeff.D,
                /* t^2 */ A * xCoeff.C + B * yCoeff.C,
                /* t^1 */ A * xCoeff.B + B * yCoeff.B,
                /* 1 */ A * xCoeff.A + B * yCoeff.A + C
                );

            /*verify the roots are in bounds of the linear segment*/
            for (var i = 0; i < 3; i++)
            {
                var t = r[i];

                var x = xCoeff.D * t * t * t + xCoeff.C * t * t + xCoeff.B * t + xCoeff.A;
                var y = yCoeff.D * t * t * t + yCoeff.C * t * t + yCoeff.B * t + yCoeff.A;

                /*above is intersection point assuming infinitely long line segment,
                  make sure we are also in bounds of the line*/
                double m;
                m = (l1x - l0x) != 0 ? (x - l0x) / (l1x - l0x) : (y - l0y) / (l1y - l0y);

                /*in bounds?*/
                if (t < 0 || t > 1d || m < 0 || m > 1d)
                {
                    x = 0;// -100;  /*move off screen*/
                    y = 0;// -100;
                }
                else
                {
                    /*intersection point*/
                    I.AppendPoint(new Point2D(x, y));
                    I.State = IntersectionState.Intersection;
                }
            }
            return I;
        }

        /// <summary>
        /// The cubic bezier line segment intersection.
        /// </summary>
        /// <param name="p1X">The p1X.</param>
        /// <param name="p1Y">The p1Y.</param>
        /// <param name="p2X">The p2X.</param>
        /// <param name="p2Y">The p2Y.</param>
        /// <param name="p3X">The p3X.</param>
        /// <param name="p3Y">The p3Y.</param>
        /// <param name="p4X">The p4X.</param>
        /// <param name="p4Y">The p4Y.</param>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>The <see cref="Intersection"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierLineSegmentIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double p4X, double p4Y,
            double a1X, double a1Y,
            double a2X, double a2Y,
            double epsilon = Epsilon)
        {
            Vector2D a, b, c, d;
            Vector2D c3, c2, c1, c0;
            double cl;
            Vector2D n;
            var min = MinPoint(a1X, a1Y, a2X, a2Y);
            var max = MaxPoint(a1X, a1Y, a2X, a2Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            a = new Vector2D(p1X, p1Y).Scale(-1);
            b = new Vector2D(p2X, p2Y).Scale(3);
            c = new Vector2D(p3X, p3Y).Scale(-3);
            d = a.Add(b.Add(c.Add(new Vector2D(p4X, p4Y))));
            c3 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y).Scale(3);
            b = new Vector2D(p2X, p2Y).Scale(-6);
            c = new Vector2D(p3X, p3Y).Scale(3);
            d = a.Add(b.Add(c));
            c2 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y).Scale(-3);
            b = new Vector2D(p2X, p2Y).Scale(3);
            c = a.Add(b);
            c1 = new Vector2D(c.I, c.J);
            c0 = new Vector2D(p1X, p1Y);
            n = new Vector2D(a1Y - a2Y, a2X - a1X);
            cl = a1X * a2Y - a2X * a1Y;
            var roots = CubicRoots(
                n.DotProduct(c3),
                n.DotProduct(c2),
                n.DotProduct(c1),
                n.DotProduct(c0) + cl,
                epsilon);
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                if (0 <= t && t <= 1)
                {
                    var p5 = Lerp(p1X, p1Y, p2X, p2Y, t);
                    var p6 = Lerp(p2X, p2Y, p3X, p3Y, t);
                    var p7 = Lerp(p3X, p3Y, p4X, p4Y, t);
                    var p8 = Lerp(p5.X, p5.Y, p6.X, p6.Y, t);
                    var p9 = Lerp(p6.X, p6.Y, p7.X, p7.Y, t);
                    var p10 = Lerp(p8.X, p8.Y, p9.X, p9.Y, t);
                    if (a1X == a2X)
                    {
                        if (min.Y <= p10.Y && p10.Y <= max.Y)
                        {
                            result.State = IntersectionState.Intersection;
                            result.AppendPoint(p10);
                        }
                    }
                    else if (a1Y == a2Y)
                    {
                        if (min.X <= p10.X && p10.X <= max.X)
                        {
                            result.State = IntersectionState.Intersection;
                            result.AppendPoint(p10);
                        }
                    }
                    else if (p10.GreaterThanOrEqual(min) && p10.LessThanOrEqual(max))
                    {
                        result.State = IntersectionState.Intersection;
                        result.AppendPoint(p10);
                    }
                }
            }
            return result;
        }
        #endregion Intersection of a Line Segment and a Cubic Bézier

        #region Intersection of a Quadratic Bézier and a Cubic Bézier
        /// <summary>
        /// Set of tests to run testing methods that calculate the Intersection of two Cubic Bézier curves.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(IntersectionQuadraticBezierCubicBezier))]
        public static List<SpeedTester> IntersectionQuadraticBezierCubicBezier()
            => new List<SpeedTester> {
                new SpeedTester(() => QuadraticBezierSegmentCubicBezierSegmentIntersection(0, 5, 10, 15, 20, 0, 5, 10, -5, 20, 10, 30, 5, Epsilon),
                $"{nameof(Experiments.QuadraticBezierSegmentCubicBezierSegmentIntersection)}(0, 5, 10, 15, 20, 0, 5, 10, -5, 20, 10, 30, 5, Epsilon)"),
                new SpeedTester(() => QuadraticBezierSegmentCubicBezierSegmentIntersection2(0, 5, 10, 15, 20, 0, 5, 10, -5, 20, 10, 30, 5, Epsilon),
                $"{nameof(Experiments.QuadraticBezierSegmentCubicBezierSegmentIntersection2)}(0, 5, 10, 15, 20, 0, 5, 10, -5, 20, 10, 30, 5, Epsilon)"),
            };

        /// <summary>
        /// Find the intersection between a quadratic bezier and a cubic bezier.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="a3X"></param>
        /// <param name="a3Y"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="b3X"></param>
        /// <param name="b3Y"></param>
        /// <param name="b4X"></param>
        /// <param name="b4Y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentCubicBezierSegmentIntersection(
            double a1X, double a1Y, double a2X, double a2Y, double a3X, double a3Y,
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y, double epsilon = Epsilon)
        {
            var a = new Vector2D(a2X, a2Y) * -2;
            var c12 = new Vector2D(a1X, a1Y) + a + new Vector2D(a3X, a3Y);
            a = new Vector2D(a1X, a1Y) * -2;
            var b = new Vector2D(a2X, a2Y) * 2;
            var c11 = a + b;
            var c10 = new Vector2D(a1X, a1Y);
            a = new Vector2D(b1X, b1Y) * -1;
            b = new Vector2D(b2X, b2Y) * 3;
            var c = new Vector2D(b3X, b3Y) * -3;
            var d = a + b + c + new Vector2D(b4X, b4Y);
            var c23 = new Vector2D(d.I, d.J);
            a = new Vector2D(b1X, b1Y) * 3;
            b = new Vector2D(b2X, b2Y) * -6;
            c = new Vector2D(b3X, b3Y) * 3;
            d = a + b + c;
            var c22 = new Vector2D(d.I, d.J);
            a = new Vector2D(b1X, b1Y) * -3;
            b = new Vector2D(b2X, b2Y) * 3;
            c = a + b;
            var c21 = new Vector2D(c.I, c.J);
            var c20 = new Vector2D(b1X, b1Y);

            var c10x2 = c10.I * c10.I;
            var c10y2 = c10.J * c10.J;
            var c11x2 = c11.I * c11.I;
            var c11y2 = c11.J * c11.J;
            var c12x2 = c12.I * c12.I;
            var c12y2 = c12.J * c12.J;
            var c20x2 = c20.I * c20.I;
            var c20y2 = c20.J * c20.J;
            var c21x2 = c21.I * c21.I;
            var c21y2 = c21.J * c21.J;
            var c22x2 = c22.I * c22.I;
            var c22y2 = c22.J * c22.J;
            var c23x2 = c23.I * c23.I;
            var c23y2 = c23.J * c23.J;

            var roots = new Polynomial(
                -2 * c12.I * c12.J * c22.I * c23.J - 2 * c12.I * c12.J * c22.J * c23.I + 2 * c12y2 * c22.I * c23.I + 2 * c12x2 * c22.J * c23.J,
                -2 * c12.I * c12.J * c23.I * c23.J + c12x2 * c23y2 + c12y2 * c23x2,
                -2 * c12.I * c21.I * c12.J * c23.J - 2 * c12.I * c12.J * c21.J * c23.I - 2 * c12.I * c12.J * c22.I * c22.J + 2 * c21.I * c12y2 * c23.I + c12y2 * c22x2 + c12x2 * (2 * c21.J * c23.J + c22y2),
                2 * c10.I * c12.I * c12.J * c23.J + 2 * c10.J * c12.I * c12.J * c23.I + c11.I * c11.J * c12.I * c23.J + c11.I * c11.J * c12.J * c23.I - 2 * c20.I * c12.I * c12.J * c23.J - 2 * c12.I * c20.J * c12.J * c23.I - 2 * c12.I * c21.I * c12.J * c22.J - 2 * c12.I * c12.J * c21.J * c22.I - 2 * c10.I * c12y2 * c23.I - 2 * c10.J * c12x2 * c23.J + 2 * c20.I * c12y2 * c23.I + 2 * c21.I * c12y2 * c22.I - c11y2 * c12.I * c23.I - c11x2 * c12.J * c23.J + c12x2 * (2 * c20.J * c23.J + 2 * c21.J * c22.J),
                2 * c10.I * c12.I * c12.J * c22.J + 2 * c10.J * c12.I * c12.J * c22.I + c11.I * c11.J * c12.I * c22.J + c11.I * c11.J * c12.J * c22.I - 2 * c20.I * c12.I * c12.J * c22.J - 2 * c12.I * c20.J * c12.J * c22.I - 2 * c12.I * c21.I * c12.J * c21.J - 2 * c10.I * c12y2 * c22.I - 2 * c10.J * c12x2 * c22.J + 2 * c20.I * c12y2 * c22.I - c11y2 * c12.I * c22.I - c11x2 * c12.J * c22.J + c21x2 * c12y2 + c12x2 * (2 * c20.J * c22.J + c21y2),
                2 * c10.I * c12.I * c12.J * c21.J + 2 * c10.J * c12.I * c21.I * c12.J + c11.I * c11.J * c12.I * c21.J + c11.I * c11.J * c21.I * c12.J - 2 * c20.I * c12.I * c12.J * c21.J - 2 * c12.I * c20.J * c21.I * c12.J - 2 * c10.I * c21.I * c12y2 - 2 * c10.J * c12x2 * c21.J + 2 * c20.I * c21.I * c12y2 - c11y2 * c12.I * c21.I - c11x2 * c12.J * c21.J + 2 * c12x2 * c20.J * c21.J,
                -2 * c10.I * c10.J * c12.I * c12.J - c10.I * c11.I * c11.J * c12.J - c10.J * c11.I * c11.J * c12.I + 2 * c10.I * c12.I * c20.J * c12.J + 2 * c10.J * c20.I * c12.I * c12.J + c11.I * c20.I * c11.J * c12.J + c11.I * c11.J * c12.I * c20.J - 2 * c20.I * c12.I * c20.J * c12.J - 2 * c10.I * c20.I * c12y2 + c10.I * c11y2 * c12.I + c10.J * c11x2 * c12.J - 2 * c10.J * c12x2 * c20.J - c20.I * c11y2 * c12.I - c11x2 * c20.J * c12.J + c10x2 * c12y2 + c10y2 * c12x2 + c20x2 * c12y2 + c12x2 * c20y2
                ).RootsInInterval();

            var result = new Intersection(IntersectionState.NoIntersection);

            for (var i = 0; i < roots.Length; i++)
            {
                var s = roots[i];
                var xRoots = QuadraticRoots(
                    c12.I,
                    c11.I,
                    c10.I - c20.I - s * c21.I - s * s * c22.I - s * s * s * c23.I,
                    epsilon);
                var yRoots = QuadraticRoots(
                    c12.J,
                    c11.J,
                    c10.J - c20.J - s * c21.J - s * s * c22.J - s * s * s * c23.J,
                    epsilon);
                if (xRoots.Count > 0 && yRoots.Count > 0)
                {
                    for (var j = 0; j < xRoots.Count; j++)
                    {
                        var xRoot = xRoots[j];
                        if (0 <= xRoot && xRoot <= 1)
                        {
                            for (var k = 0; k < yRoots.Count; k++)
                            {
                                if (Abs(xRoot - yRoots[k]) < epsilon)
                                {
                                    result.Points.Add((Point2D)c23 * s * s * s + (c22 * s * s + (c21 * s + c20)));
                                    goto checkRoots;
                                }
                            }
                        }
                    }
                    checkRoots:;
                }
            }

            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a quadratic bezier and a cubic bezier.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="a3X"></param>
        /// <param name="a3Y"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="b3X"></param>
        /// <param name="b3Y"></param>
        /// <param name="b4X"></param>
        /// <param name="b4Y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// This is a performance improved rewrite of a method ported from: http://www.kevlindev.com/ also found at: https://github.com/thelonious/kld-intersections/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentCubicBezierSegmentIntersection2(
            double a1X, double a1Y, double a2X, double a2Y, double a3X, double a3Y,
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y, double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // ToDo: Break early if the AABB bounding box of the curve does not intersect.

            var c12 = new Vector2D(a1X - a2X * 2 + a3X, a1Y - a2Y * 2 + a3Y);
            var c11 = new Vector2D(2 * (a2X - a1X), 2 * (a2Y - a1Y));
            var c23 = new Vector2D(b4X - b3X * 3 + b2X * 3 - b1X * 1, b4Y - b3Y * 3 + b2Y * 3 - b1Y * 1);
            var c22 = new Vector2D(3 * (b3X - b2X * 2 + b1X), 3 * (b3Y - b2Y * 2 + b1Y));
            var c21 = new Vector2D(3 * (b2X - b1X), 3 * (b2Y - b1Y));

            var c10x2 = a1X * a1X;
            var c10y2 = a1Y * a1Y;
            var c11x2 = c11.I * c11.I;
            var c11y2 = c11.J * c11.J;
            var c12x2 = c12.I * c12.I;
            var c12y2 = c12.J * c12.J;

            var c20x2 = b1X * b1X;
            var c20y2 = b1Y * b1Y;
            var c21x2 = c21.I * c21.I;
            var c21y2 = c21.J * c21.J;
            var c22x2 = c22.I * c22.I;
            var c22y2 = c22.J * c22.J;
            var c23x2 = c23.I * c23.I;
            var c23y2 = c23.J * c23.J;

            var roots = new Polynomial(
                /* t^0 */ -2 * c12.I * c12.J * c22.I * c23.J - 2 * c12.I * c12.J * c22.J * c23.I + 2 * c12y2 * c22.I * c23.I + 2 * c12x2 * c22.J * c23.J,
                /* t^1 */ -2 * c12.I * c12.J * c23.I * c23.J + c12x2 * c23y2 + c12y2 * c23x2,
                /* t^2 */ -2 * c12.I * c21.I * c12.J * c23.J - 2 * c12.I * c12.J * c21.J * c23.I - 2 * c12.I * c12.J * c22.I * c22.J + 2 * c21.I * c12y2 * c23.I + c12y2 * c22x2 + c12x2 * (2 * c21.J * c23.J + c22y2),
                /* t^3 */ 2 * a1X * c12.I * c12.J * c23.J + 2 * a1Y * c12.I * c12.J * c23.I + c11.I * c11.J * c12.I * c23.J + c11.I * c11.J * c12.J * c23.I - 2 * b1X * c12.I * c12.J * c23.J - 2 * c12.I * b1Y * c12.J * c23.I - 2 * c12.I * c21.I * c12.J * c22.J - 2 * c12.I * c12.J * c21.J * c22.I - 2 * a1X * c12y2 * c23.I - 2 * a1Y * c12x2 * c23.J + 2 * b1X * c12y2 * c23.I + 2 * c21.I * c12y2 * c22.I - c11y2 * c12.I * c23.I - c11x2 * c12.J * c23.J + c12x2 * (2 * b1Y * c23.J + 2 * c21.J * c22.J),
                /* t^4 */ 2 * a1X * c12.I * c12.J * c22.J + 2 * a1Y * c12.I * c12.J * c22.I + c11.I * c11.J * c12.I * c22.J + c11.I * c11.J * c12.J * c22.I - 2 * b1X * c12.I * c12.J * c22.J - 2 * c12.I * b1Y * c12.J * c22.I - 2 * c12.I * c21.I * c12.J * c21.J - 2 * a1X * c12y2 * c22.I - 2 * a1Y * c12x2 * c22.J + 2 * b1X * c12y2 * c22.I - c11y2 * c12.I * c22.I - c11x2 * c12.J * c22.J + c21x2 * c12y2 + c12x2 * (2 * b1Y * c22.J + c21y2),
                /* t^5 */ 2 * a1X * c12.I * c12.J * c21.J + 2 * a1Y * c12.I * c21.I * c12.J + c11.I * c11.J * c12.I * c21.J + c11.I * c11.J * c21.I * c12.J - 2 * b1X * c12.I * c12.J * c21.J - 2 * c12.I * b1Y * c21.I * c12.J - 2 * a1X * c21.I * c12y2 - 2 * a1Y * c12x2 * c21.J + 2 * b1X * c21.I * c12y2 - c11y2 * c12.I * c21.I - c11x2 * c12.J * c21.J + 2 * c12x2 * b1Y * c21.J,
                /* t^6 */ -2 * a1X * a1Y * c12.I * c12.J - a1X * c11.I * c11.J * c12.J - a1Y * c11.I * c11.J * c12.I + 2 * a1X * c12.I * b1Y * c12.J + 2 * a1Y * b1X * c12.I * c12.J + c11.I * b1X * c11.J * c12.J + c11.I * c11.J * c12.I * b1Y - 2 * b1X * c12.I * b1Y * c12.J - 2 * a1X * b1X * c12y2 + a1X * c11y2 * c12.I + a1Y * c11x2 * c12.J - 2 * a1Y * c12x2 * b1Y - b1X * c11y2 * c12.I - c11x2 * b1Y * c12.J + c10x2 * c12y2 + c10y2 * c12x2 + c20x2 * c12y2 + c12x2 * c20y2
                ).RootsInInterval(0, 1);

            foreach (var s in roots)
            {
                var point = new Point2D(c23.I * s * s * s + c22.I * s * s + c21.I * s + b1X, c23.J * s * s * s + c22.J * s * s + c21.J * s + b1Y);
                var xRoots = QuadraticRoots(
                    /* c */ c12.I,
                    /* t^1 */ c11.I,
                    /* t^2 */ a1X - point.X,
                    epsilon);
                var yRoots = QuadraticRoots(
                    /* c */ c12.J,
                    /* t^1 */ c11.J,
                    /* t^2 */ a1Y - point.Y,
                    epsilon);
                if (xRoots.Count > 0 && yRoots.Count > 0)
                {
                    foreach (var xRoot in xRoots)
                    {
                        if (0 <= xRoot && xRoot <= 1)
                        {
                            foreach (var yRoot in yRoots)
                            {
                                var t = xRoot - yRoot;
                                if ((t >= 0 ? t : -t) < epsilon)
                                {
                                    result.Points.Add(point);
                                    goto checkRoots;
                                }
                            }
                        }
                    }
                    checkRoots:;
                }
            }

            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }
        #endregion Intersection of a Quadratic Bézier and a Cubic Bézier

        #region Intersection of a Cubic Bézier and a Cubic Bézier
        #endregion Intersection of a Cubic Bézier and a Cubic Bézier

        #region Is Convex
        /// <summary>
        /// For each set of three adjacent points A, B, C,
        /// find the dot product AB · BC. If the sign of
        /// all the dot products is the same, the angles
        /// are all positive or negative (depending on the
        /// order in which we visit them) so the polygon
        /// is convex.
        /// </summary>
        /// <returns>
        /// Return true if the polygon is convex.
        /// </returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-polygon-is-convex-in-c/
        /// </acknowledgment>
        public static bool IsConvex(PolygonContour polygon)
        {
            var got_negative = false;
            var got_positive = false;
            var num_points = polygon.Points.Count;
            int B, C;
            for (var A = 0; A < num_points; A++)
            {
                B = (A + 1) % num_points;
                C = (B + 1) % num_points;

                var cross_product = CrossProductVector(
                        polygon.Points[A].X, polygon.Points[A].Y,
                        polygon.Points[B].X, polygon.Points[B].Y,
                        polygon.Points[C].X, polygon.Points[C].Y);
                if (cross_product < 0)
                {
                    got_negative = true;
                }
                else
                {
                    got_positive |= cross_product > 0;
                }

                if (got_negative && got_positive)
                {
                    return false;
                }
            }

            // If we got this far, the polygon is convex.
            return true;
        }
        #endregion Is Convex

        #region Is Valid
        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(IsValidTests))]
        public static List<SpeedTester> IsValidTests()
            => new List<SpeedTester> {
                new SpeedTester(() => IsValid(double.NaN),
                $"{nameof(Experiments.IsValid)}({double.NaN})"),
                new SpeedTester(() => IsValid1(double.NaN),
                $"{nameof(Experiments.IsValid1)}({double.NaN})"),
            };

        /// <summary>
        /// Make sure that a double number is not a NaN or infinity.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, returns false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValid(double value)
            => !double.IsNaN(value) && !double.IsInfinity(value);

        /// <summary>
        /// This function is used to ensure that a floating point number is
        /// not a NaN or infinity.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>
        /// 	<c>true</c> if the specified x is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid1(double x)
        {
            if (double.IsNaN(x))
            {
                // NaN.
                return false;
            }

            return !double.IsInfinity(x);
        }
        #endregion Is Valid

        #region Linear Offset Interpolation
        /// <summary>
        /// The offset interpolate.
        /// </summary>
        /// <param name="Value1">The Value1.</param>
        /// <param name="Value2">The Value2.</param>
        /// <param name="Offset">The Offset.</param>
        /// <param name="Weight">The Weight.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public static Point2D OffsetInterpolate(Point2D Value1, Point2D Value2, double Offset, double Weight)
        {
            var UnitVectorAB = new Vector2D(Value1, Value2);
            var PerpendicularAB = UnitVectorAB.Perpendicular().Scale(0.5).Scale(Offset);
            return Interpolators.Linear(Value1, Value2, Weight).Inflate(PerpendicularAB);
        }
        #endregion Linear Offset Interpolation

        #region Line Overlap
        /// <summary>
        /// Get the overlap.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="Left">The Left.</param>
        /// <param name="Right">The Right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com/delphi/clipper.php
        /// </acknowledgment>
        public static bool GetOverlap(double a1, double a2, double b1, double b2, out double Left, out double Right)
        {
            if (a1 < a2)
            {
                if (b1 < b2) { Left = Max(a1, b1); Right = Min(a2, b2); }
                else { Left = Max(a1, b2); Right = Min(a2, b1); }
            }
            else
            {
                if (b1 < b2) { Left = Max(a2, b1); Right = Min(a1, b2); }
                else { Left = Max(a2, b2); Right = Min(a1, b1); }
            }
            return Left < Right;
        }
        #endregion Line Overlap

        #region List Interpolation Points of Cubic Bézier
        /// <summary>
        /// The interpolate points.
        /// </summary>
        /// <param name="bezier">The bezier.</param>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> InterpolatePoints(CubicBezier bezier, int count)
        {
            var ipoints = new Point2D[count + 1];
            for (var i = 0; i <= count; i += 1)
            {
                var v = 1d / count * i;
                ipoints[i] = bezier.Interpolate(v);
            }

            return new List<Point2D>(ipoints);
        }

        /// <summary>
        /// The interpolate cubic beizer points.
        /// </summary>
        /// <param name="bezier">The bezier.</param>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> InterpolateCubicBeizerPoints(CubicBezier bezier, int count) => InterpolateCubicBeizerPoints(bezier.A, bezier.B, bezier.C, bezier.D, count);

        /// <summary>
        /// The interpolate cubic beizer points.
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="Precision">The Precision.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> InterpolateCubicBeizerPoints(Point2D a, Point2D b, Point2D c, Point2D d, double Precision)
        {
            var BPoints = new Point2D[(int)((1 / Precision) + 2)];
            BPoints[0] = a;
            BPoints[BPoints.Length - 1] = d;
            var Node = 0;
            for (double Index = 0; Index < 1; Index += Precision)
            {
                Node++;
                BPoints[Node] = new Point2D(Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, Index));
            }

            return new List<Point2D>(BPoints);
        }

        /// <summary>
        /// The compute bezier interpolations.
        /// </summary>
        /// <param name="bezier">The bezier.</param>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> ComputeBezierInterpolations(CubicBezier bezier, int count) => ComputeBezierInterpolations(bezier.A, bezier.B, bezier.C, bezier.D, count);

        /// <summary>
        ///  ComputeBezier fills an array of Point2D structs with the curve points
        ///  generated from the control points cp. Caller must allocate sufficient memory
        ///  for the result, which is [sizeof(Point2D) * numberOfPoints]
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="numberOfPoints"></param>
        public static List<Point2D> ComputeBezierInterpolations(Point2D a, Point2D b, Point2D c, Point2D d, int numberOfPoints)
        {
            var curve = new List<Point2D>();
            double t = 0;
            var dt = 1.0d / (numberOfPoints - 1);
            for (var i = 0; i <= numberOfPoints; i++)
            {
                t += dt;
                curve.Add(new Point2D(Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, t)));
            }
            return curve;
        }

        /// <summary>
        /// The interpolate cubic beizer points0.
        /// </summary>
        /// <param name="bezier">The bezier.</param>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> InterpolateCubicBeizerPoints0(CubicBezier bezier, int count) => InterpolateCubicBeizerPoints0(bezier.A, bezier.B, bezier.C, bezier.D, count);

        /// <summary>
        /// Function to Plot a Cubic Bezier
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="Precision"></param>
        /// <returns></returns>
        public static List<Point2D> InterpolateCubicBeizerPoints0(Point2D a, Point2D b, Point2D c, Point2D d, double Precision)
        {
            var BPoints = new Point2D[(int)((1 / Precision) + 2)];
            BPoints[0] = a;
            BPoints[BPoints.Length - 1] = d;
            var Node = 0;
            for (double Index = 0; Index <= 1; Index = Index + Precision)
            {
                Node++;
                BPoints[Node] = new Point2D(Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, Index));
            }

            return new List<Point2D>(BPoints);
        }
        #endregion List Interpolation Points of Cubic Bézier

        #region List Interpolation Points of Quadratic Bézier
        /// <summary>
        /// The interpolate quadratic bezier points.
        /// </summary>
        /// <param name="bezier">The bezier.</param>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> InterpolateQuadraticBezierPoints(QuadraticBezier bezier, int count)
        {
            var ipoints = new Point2D[count + 1];
            for (var i = 0; i <= count; i += 1)
            {
                double v = 1f / count * i;
                ipoints[i] = bezier.Interpolate(v);
            }

            return new List<Point2D>(ipoints);
        }
        #endregion List Interpolation Points of Quadratic Bézier

        #region Log2
        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(Log2Tests))]
        public static List<SpeedTester> Log2Tests()
            => new List<SpeedTester> {
                new SpeedTester(() => Log(12, 2),
                $"{nameof(Math.Log)}(12, 2)"),
                new SpeedTester(() => Log2(12),
                $"{nameof(Experiments.Log2)}(12)"),
                new SpeedTester(() => Log2_1(12),
                $"{nameof(Experiments.Log2_1)}(12)")
            };

        /// <summary>
        /// Determine the position of the highest one-bit in a number.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Log2(int a)
        {
            byte bits = 0;
            while (a != 0)
            {
                ++bits;
                a >>= 1;
            }
            return bits;
        }

        /// <summary>
        /// The multiply de bruijn bit position (readonly). Value: new byte[32] { 0, 9, 1, 10, 13, 21, 2, 29, 11, 14, 16, 18, 22, 25, 3, 30, 8, 12, 20, 28, 15, 17, 24, 7, 19, 27, 23, 6, 26, 5, 4, 31 }.
        /// </summary>
        /// <acknowledgment>
        /// http://graphics.stanford.edu/~seander/bithacks.html
        /// </acknowledgment>
        public static readonly byte[] multiplyDeBruijnBitPosition = new byte[32]
        {
            0, 9, 1, 10, 13, 21, 2, 29, 11, 14, 16, 18, 22, 25, 3, 30,
            8, 12, 20, 28, 15, 17, 24, 7, 19, 27, 23, 6, 26, 5, 4, 31
        };

        /// <summary>
        /// Returns log2(x) for positive values of x.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Log2_1(int x)
        {
            if (x <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(x), "must be positive");
            }

            if (x == 1)
            {
                return 0;
            }

            // Locate the highest set bit.
            var v = unchecked((uint)x);
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;

            var i = unchecked(v * 0x7c4acdd) >> 27;
            int r = multiplyDeBruijnBitPosition[i];

            return r;
        }

        /// <summary>
        /// Returns log2(x) for positive values of x.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Log2_1(uint x)
        {
            if (x <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(x), "must be positive");
            }

            if (x == 1)
            {
                return 0;
            }

            // Locate the highest set bit.
            var v = unchecked(x);
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;

            var i = unchecked(v * 0x7c4acdd) >> 27;
            int r = multiplyDeBruijnBitPosition[i];

            return r;
        }
        #endregion Log2

        #region Mixed Product of Three 3D Points
        /// <summary>
        /// Set of tests to run testing methods that calculate the mixed product for three 3D points.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(MixedProduct3D_0Tests))]
        public static List<SpeedTester> MixedProduct3D_0Tests() => new List<SpeedTester> {
                new SpeedTester(() => MixedProduct3D_0(0, 0,0, 1, 1, 1,2,2,2),
                $"{nameof(Experiments.MixedProduct3D_0)}(0, 0, 1, 1, 0.5d)")
            };

        /// <summary>
        /// The mixed product3d 0.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="z3">The z3.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double MixedProduct3D_0(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3)
            => DotProduct(CrossProduct2Points3D_0(x1, y1, z1, x2, y2, z2), x3, y3, z3);
        #endregion Mixed Product of Three 3D Points

        #region Near Zero Inquiry
        /// <summary>
        /// The near zero epsilon (const). Value: 1E-20.
        /// </summary>
        public const double NearZeroEpsilon = 1E-20;

        /// <summary>
        /// Set of tests to run testing methods that query whether a number is near zero.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(NearZeroTests))]
        public static List<SpeedTester> NearZeroTests() => new List<SpeedTester> {
                new SpeedTester(() => NearZero0(0.000000001d),
                $"{nameof(Experiments.NearZero0)}(0.000000001d)"),
                new SpeedTester(() => NearZero1(0.000000001d),
                $"{nameof(Experiments.NearZero1)}(0.000000001d)")
            };

        /// <summary>
        /// The near zero0.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearZero0(double value, double epsilon = NearZeroEpsilon)
            => (value > -epsilon) && (value < -epsilon);

        /// <summary>
        /// The near zero1.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearZero1(double value, double epsilon = NearZeroEpsilon)
            => Abs(value) <= epsilon;
        #endregion Near Zero Inquiry

        #region Normalize The Vector Between Two 2D Points
        /// <summary>
        /// Find the Normal of Two points.
        /// </summary>
        /// <param name="x1">The x component of the first Point.</param>
        /// <param name="y1">The y component of the first Point.</param>
        /// <param name="x2">The x component of the second Point.</param>
        /// <param name="y2">The y component of the second Point.</param>
        /// <returns>The Normal of two Points</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Normalize(
            double x1, double y1,
            double x2, double y2)
            => (
                x1 / Sqrt((x1 * x2) + (y1 * y2)),
                y1 / Sqrt((x1 * x2) + (y1 * y2))
                );
        #endregion Normalize The Vector Between Two 2D Points

        #region Normalize The Vector Between Two 3D Points
        /// <summary>
        /// Find the Normal of Two points.
        /// </summary>
        /// <param name="x1">The x component of the first Point.</param>
        /// <param name="y1">The y component of the first Point.</param>
        /// <param name="z1">The z component of the first Point.</param>
        /// <param name="x2">The x component of the second Point.</param>
        /// <param name="y2">The y component of the second Point.</param>
        /// <param name="z2">The z component of the second Point.</param>
        /// <returns>The Normal of two Points</returns>
        /// <acknowledgment>
        /// http://www.fundza.com/vectors/normalize/
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Normalize(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (
                x1 / Sqrt((x1 * x2) + (y1 * y2) + (z1 * z2)),
                y1 / Sqrt((x1 * x2) + (y1 * y2) + (z1 * z2)),
                z1 / Sqrt((x1 * x2) + (y1 * y2) + (z1 * z2))
                );
        #endregion Normalize The Vector Between Two 3D Points

        #region N Polygon Star
        // Draw the stars.
        /// <summary>
        /// The pic canvas paint.
        /// </summary>
        /// <param name="e">The paint event arguments.</param>
        /// <param name="NumPoints">The NumPoints.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="chkHalfOnly">The chkHalfOnly.</param>
        /// <param name="chkRelPrimeOnly">The chkRelPrimeOnly.</param>
        public static void PicCanvas_Paint(PaintEventArgs e, int NumPoints, Rectangle bounds, bool chkHalfOnly, bool chkRelPrimeOnly)
        {
            if (NumPoints < 3)
            {
                return;
            }

            // Get the radii.
            int r1, r2, r3;
            r3 = Min(bounds.Width, bounds.Height) / 2;
            r1 = r3 / 2;
            r2 = r3 / 4;
            r3 = r1 + r2;

            // Position variables.
            var cx = bounds.Width / 2;
            var cy = bounds.Height / 2;

            // Position the original points.
            var pts1 = new PointF[NumPoints];
            var pts2 = new PointF[NumPoints];
            var theta = -PI / 2;
            var dtheta = 2 * PI / NumPoints;
            for (var i = 0; i < NumPoints; i++)
            {
                pts1[i].X = (float)(r1 * Cos(theta));
                pts1[i].Y = (float)(r1 * Sin(theta));
                pts2[i].X = (float)(r2 * Cos(theta));
                pts2[i].Y = (float)(r2 * Sin(theta));
                theta += dtheta;
            }

            // Draw stars.
            var max = NumPoints - 1;
            if (chkHalfOnly)
            {
                max = NumPoints / 2;
            }

            for (var skip = 1; skip <= max; skip++)
            {
                // See if they are relatively prime.
                var draw_all = !chkRelPrimeOnly;
                if (draw_all || GCD(skip, NumPoints) == 1)
                {
                    // Draw the big version of the star.
                    DrawStar(e.Graphics, cx, cy, pts1, skip, NumPoints);

                    // Draw the smaller version.
                    theta = -PI / 2 + skip * 2 * PI / NumPoints;
                    var x = (int)(cx + r3 * Cos(theta));
                    var y = (int)(cy + r3 * Sin(theta));

                    DrawStar(e.Graphics, x, y, pts2, skip, NumPoints);
                }
            }
        }

        // Return the greatest common divisor (GCD) of a and b.
        /// <summary>
        /// The GCD.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public static long GCD(long a, long b)
        {
            long remainder;

            // Pull out remainders.
            for (; ; )
            {
                remainder = a % b;
                if (remainder == 0)
                {
                    break;
                }

                a = b;
                b = remainder;
            }

            return b;
        }

        // Draw a star centered at (x, y) using this skip value.
        /// <summary>
        /// The draw star.
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="orig_pts">The orig_pts.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="NumPoints">The NumPoints.</param>
        public static void DrawStar(Graphics gr, int x, int y, PointF[] orig_pts, int skip, int NumPoints)
        {
            // Make a PointF array with the points in the proper order.
            var pts = new PointF[NumPoints];
            for (var i = 0; i < NumPoints; i++)
            {
                pts[i] = orig_pts[i * skip % NumPoints];
            }

            // Draw the star.
            gr.TranslateTransform(x, y);
            gr.DrawPolygon(Pens.Blue, pts);
            gr.ResetTransform();
        }
        #endregion N Polygon Star

        #region N Polygon Intersecting Star
        // Return PointFs to define a non-intersecting star.
        /// <summary>
        /// The non intersecting star points.
        /// </summary>
        /// <param name="num_points">The num_points.</param>
        /// <param name="bounds">The bounds.</param>
        /// <returns>The <see cref="T:PointF[]"/>.</returns>
        public static PointF[] NonIntersectingStarPoints(int num_points, Rectangle bounds)
        {
            // Make room for the points.
            var pts = new PointF[2 * num_points];

            double rx1 = bounds.Width / 2;
            double ry1 = bounds.Height / 2;
            var rx2 = rx1 * 0.5;
            var ry2 = ry1 * 0.5;
            var cx = bounds.X + rx1;
            var cy = bounds.Y + ry1;

            // Start at the top.
            var theta = -PI / 2;
            var dtheta = PI / num_points;
            for (var i = 0; i < 2 * num_points; i += 2)
            {
                pts[i] = new PointF(
                    (float)(cx + rx1 * Cos(theta)),
                    (float)(cy + ry1 * Sin(theta)));
                theta += dtheta;

                pts[i + 1] = new PointF(
                    (float)(cx + rx2 * Cos(theta)),
                    (float)(cy + ry2 * Sin(theta)));
                theta += dtheta;
            }

            return pts;
        }
        #endregion N Polygon Intersecting Star

        #region Offset Line
        /// <summary>
        /// Calculate the geometry of points offset at a specified distance. aka Double Line.
        /// </summary>
        /// <param name="pointA">First reference point.</param>
        /// <param name="pointB">First inclusive point.</param>
        /// <param name="pointC">Second inclusive point.</param>
        /// <param name="pointD">Second reference point.</param>
        /// <param name="offsetDistance">Offset Distance</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Suppose you have 4 points; A, B C, and D. With Lines AB, BC, and CD.<BR/>
        ///<BR/>
        ///                 B1         BC1       C1<BR/>
        ///                   |\¯B¯¯¯¯¯BC¯¯¯C¯¯¯/|<BR/>
        ///                   | \--------------/ |<BR/>
        ///                   | |\____________/| |<BR/>
        ///                   | | |B2  BC2 C2| | |<BR/>
        ///                 AB| | |          | | |CD<BR/>
        ///                AB1| | |AB2    CD2| | |CD1<BR/>
        ///                   | | |          | | |<BR/>
        ///                   | | |          | | |<BR/>
        ///               A1  A  A2      D2  D  D1<BR/>
        ///
        /// </acknowledgment>
        public static Point2D[] CenteredOffsetLinePoints(Point2D pointA, Point2D pointB, Point2D pointC, Point2D pointD, double offsetDistance)
        {
            // To get the vectors of the angles at each corner B and C, Normalize the Unit Delta Vectors along AB, BC, and CD.
            var UnitVectorAB = pointB.Subtract(pointA).Unit();
            var UnitVectorBC = pointC.Subtract(pointB).Unit();
            var UnitVectorCD = pointD.Subtract(pointC).Unit();

            //  Find the Perpendicular of the outside vectors
            var PerpendicularAB = UnitVectorAB.Perpendicular();
            var PerpendicularCD = UnitVectorCD.Perpendicular();

            //  Normalized Vectors pointing out from B and C.
            var OutUnitVectorB = (UnitVectorAB - UnitVectorBC).Unit();
            var OutUnitVectorC = (UnitVectorCD - UnitVectorBC).Unit();

            //  The distance out from B is the radius / Cos(theta) where theta is the angle
            //  from the perpendicular of BC of the UnitVector. The cosine can also be
            //  calculated by doing the dot product of  Unit(Perpendicular(AB)) and
            //  UnitVector.
            var BPointScale = PerpendicularAB.DotProduct(OutUnitVectorB) * offsetDistance;
            var CPointScale = PerpendicularCD.DotProduct(OutUnitVectorC) * offsetDistance;

            OutUnitVectorB = OutUnitVectorB.Scale(CPointScale);
            OutUnitVectorC = OutUnitVectorC.Scale(BPointScale);

            // Corners of the parallelogram to draw
            var Out = new Point2D[] {
                pointC + OutUnitVectorC,
                pointB + OutUnitVectorB,
                pointB - OutUnitVectorB,
                pointC - OutUnitVectorC,
                pointC + OutUnitVectorC
            };
            return Out;
        }
        #endregion Offset Line

        #region Operation Addition Safe
        /// <summary>
        /// Set of tests to run testing methods that calculate the safty of operations.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(IsAdditionSafeTests))]
        public static List<SpeedTester> IsAdditionSafeTests()
            => new List<SpeedTester> {
                new SpeedTester(() => IsAdditionSafe(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsAdditionSafe)}(int.MaxValue / 2, int.MaxValue / 2)"),
                new SpeedTester(() => IsAdditionSafe2(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsAdditionSafe2)}(int.MaxValue / 2, int.MaxValue / 2)"),
                new SpeedTester(() => IsAdditionSafe3(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsAdditionSafe3)}(int.MaxValue / 2, int.MaxValue / 2)"),
                new SpeedTester(() => IsAdditionSafe4(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsAdditionSafe4)}(int.MaxValue / 2, int.MaxValue / 2)"),
                new SpeedTester(() => IsAdditionSafe5(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsAdditionSafe5)}(int.MaxValue / 2, int.MaxValue / 2)"),
                new SpeedTester(() => IsAdditionSafe6(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsAdditionSafe6)}(int.MaxValue / 2, int.MaxValue / 2)")
            };

        /// <summary>
        /// The is addition safe.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        public static bool IsAdditionSafe(int a, int b)
            => Log2(a) < sizeof(int) && Log2(b) < sizeof(int);

        /// <summary>
        /// The is addition safe2.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        public static bool IsAdditionSafe2(int a, int b)
        {
            var L_Mask = int.MaxValue;
            L_Mask >>= 1;
            L_Mask = ~L_Mask;

            a &= L_Mask;
            b &= L_Mask;

            return a == 0 || b == 0 || a == -0 || b == -0;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long?noredirect=1
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe3(int a, int b)
        {
            if (a > 0)
            {
                return b > (int.MaxValue - a);
            }

            if (a < 0)
            {
                return b > (int.MinValue + a);
            }

            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long?noredirect=1
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe4(int a, int b)
            => a < 0 != b < 0 || (a < 0
            ? b > int.MinValue - a
            : b < int.MaxValue - a);

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long?noredirect=1
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe5(int a, int b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0)
            {
                return true;
            }

            if (a < 0)
            {
                return b >= (int.MinValue - a);
            }

            if (a > 0)
            {
                return b <= (int.MaxValue - a);
            }

            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c?rq=1
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe6(int a, int b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0)
            {
                return true;
            }

            if (b > 0)
            {
                return a > int.MaxValue - b;
            }

            if (b < 0)
            {
                return a < int.MinValue - b;
            }

            return true;
        }
        #endregion Operation Addition Safe

        #region Operation Division Safe
        /// <summary>
        /// Set of tests to run testing methods that calculate the safty of operations.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(IsDivisionSafeTests))]
        public static List<SpeedTester> IsDivisionSafeTests()
            => new List<SpeedTester> {
                new SpeedTester(() => IsDivisionSafe(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsDivisionSafe)}(int.MaxValue / 2, int.MaxValue / 2)"),
            };

        /// <summary>
        /// The is division safe.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c?rq=1
        /// </acknowledgment>
        public static bool IsDivisionSafe(int a, int b)
        {
            if (b == 0)
            {
                return false;
            }
            //for division(except for the INT_MIN and - 1 special case) there is no possibility of going over INT_MIN or INT_MAX.
            if (a == int.MinValue && b == -1)
            {
                return false;
            }

            return true;
        }
        #endregion Operation Division Safe

        #region Operation Exponentiation Safe
        /// <summary>
        /// Set of tests to run testing methods that calculate the safty of operations.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(IsExponentiationSafeTests))]
        public static List<SpeedTester> IsExponentiationSafeTests()
            => new List<SpeedTester> {
                new SpeedTester(() => IsExponentiationSafe(2, 39),
                $"{nameof(Experiments.IsExponentiationSafe)}(2, 39)")
            };

        /// <summary>
        /// The is exponentiation safe.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        public static bool IsExponentiationSafe(int a, int b)
            => Log2(a) * b <= sizeof(int);
        #endregion Operation Exponentiation Safe

        #region Operation Multiplication Safe
        /// <summary>
        /// Set of tests to run testing methods that calculate the safety of operations.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(IsMultiplicationSafeTests))]
        public static List<SpeedTester> IsMultiplicationSafeTests()
            => new List<SpeedTester> {
                new SpeedTester(() => IsMultiplicationSafe(2, int.MaxValue / 2),
                $"{nameof(Experiments.IsMultiplicationSafe)}(2, int.MaxValue / 2)"),
                new SpeedTester(() => IsMultiplicationSafe0(2, int.MaxValue / 2),
                $"{nameof(Experiments.IsMultiplicationSafe0)}(2, int.MaxValue / 2)"),
                new SpeedTester(() => IsMultiplicationSafe1(2, int.MaxValue / 2),
                $"{nameof(Experiments.IsMultiplicationSafe1)}(2, int.MaxValue / 2)"),
                new SpeedTester(() => IsMultiplicationSafe2(2, int.MaxValue / 2),
                $"{nameof(Experiments.IsMultiplicationSafe2)}(2, int.MaxValue / 2)"),
                new SpeedTester(() => IsMultiplicationSafe3(2, int.MaxValue / 2),
                $"{nameof(Experiments.IsMultiplicationSafe3)}(2, int.MaxValue / 2)")
            };

        /// <summary>
        /// The is multiplication safe.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        public static bool IsMultiplicationSafe(int a, int b)
            => Log2(a) + Log2(b) <= sizeof(int);

        /// <summary>
        /// The is multiplication safe0.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        public static bool IsMultiplicationSafe0(uint a, uint b)
            => Log2_1(a) + Log2_1(b) <= sizeof(uint);

        /// <summary>
        /// The is multiplication safe1.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsMultiplicationSafe1(uint a, uint b)
            => Math.Round(Log(a, 2) + Log(b, 2), MidpointRounding.AwayFromZero) <= sizeof(uint);

        /// <summary>
        /// The is multiplication safe2.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        public static bool IsMultiplicationSafe2(int a, int b)
        {
            if (a == 0)
            {
                return true;
            }
            // a * b would overflow
            return b > int.MaxValue / a;
        }

        /// <summary>
        /// The is multiplication safe3.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c?rq=1
        /// </acknowledgment>
        public static bool IsMultiplicationSafe3(int a, int b)
        {
            if (a == 0)
            {
                return true;
            }

            if (a > int.MaxValue / b)
            {
                return false /* `a * x` would overflow */;
            }

            if (a < int.MinValue / b)
            {
                return false /* `a * x` would underflow */;
            }
            // there may be need to check for -1 for two's complement machines
            if ((a == -1) && (b == int.MinValue))
            {
                return false /* `a * x` can overflow */;
            }

            if ((b == -1) && (a == int.MinValue))
            {
                return false /* `a * x` (or `a / x`) can overflow */;
            }

            return true;
        }
        #endregion Operation Multiplication Safe

        #region Operation Subtraction Safe
        /// <summary>
        /// Set of tests to run testing methods that calculate the safety of operations.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(IsSubtractionSafeTests))]
        public static List<SpeedTester> IsSubtractionSafeTests()
            => new List<SpeedTester> {
                new SpeedTester(() => IsSubtractionSafe(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsSubtractionSafe)}(int.MaxValue / 2, int.MaxValue / 2)"),
            };

        /// <summary>
        /// The is subtraction safe.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c?rq=1
        /// </acknowledgment>
        public static bool IsSubtractionSafe(int a, int b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0)
            {
                return true;
            }

            if (b < 0)
            {
                return a > int.MaxValue + b;
            }

            if (b > 0)
            {
                return a < int.MinValue + b;
            }

            return true;
        }
        #endregion Operation Subtraction Safe

        #region Orient Polygon Clockwise
        /// <summary>
        /// If the polygon is oriented counterclockwise,
        /// reverse the order of its points.
        /// </summary>
        /// <param name="polygon"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// </acknowledgment>
        public static void OrientPolygonClockwise(PolygonContour polygon)
        {
            if (polygon.Orientation == RotationDirections.CounterClockwise)
            {
                polygon.Points.Reverse();
            }
        }
        #endregion Orient Polygon Clockwise

        #region Perimeter Polygon of a Polygon
        /// <summary>
        /// Determine the radian angle of the specified point (as it relates to the origin).
        /// Warning: Do not pass zero in both parameters, as this will cause a division-by-zero.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static double AngleOf(double x, double y)
        {
            var dist = Sqrt(x * x + y * y);
            if (y >= 0d)
            {
                return Acos(x / dist);
            }
            else
            {
                return Acos(-x / dist) + PI;
            }
        }

        /// <summary>
        ///  Returns the perimeter polygon.
        ///  If for any reason the procedure fails, it will return null.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://alienryderflex.com/polygon_perimeter/
        /// </acknowledgment>
        public static List<(double X, double Y)> PolygonPerimeter(List<(double X, double Y)> points)
        {
            var corners = points.Count;

            const int MAX_SEGS = 1000;

            var segS = new List<(double X, double Y)>();
            var segE = new List<(double X, double Y)>();
            var segAngle = new List<double>();
            var segRet = new List<(double X, double Y)>();
            (double X, double Y)? intersect;
            var start = points[0];
            var lastAngle = PI;
            var j = corners - 1;
            var segs = 0;

            if (corners > MAX_SEGS)
            {
                return null;
            }

            //  1,3.  Reformulate the polygon as a set of line segments, and choose a
            //        starting point that must be on the perimeter.
            for (var i = 0; i < corners; i++)
            {
                if (points[i].X != points[j].X || points[i].Y != points[j].Y)
                {
                    segS[segs] = (points[i].X, points[i].Y);
                    segE[segs] = (points[j].X, points[j].Y);
                }
                j = i;
                if (points[i].Y > start.Y || points[i].Y == start.Y && points[i].X < start.X)
                {
                    start.X = points[i].X;
                    start.Y = points[i].Y;
                }
            }
            if (segs == 0)
            {
                return null;
            }

            //  2.  Break the segments up at their intersection points.
            for (var i = 0; i < segs - 1; i++)
            {
                for (j = i + 1; j < segs; j++)
                {
                    var (intersects, point) = Intersection0(
                    segS[i].X, segS[i].Y, segE[i].X, segE[i].Y,
                    segS[j].X, segS[j].Y, segE[j].X, segE[j].Y);
                    intersect = point;
                    if (intersects)
                    {
                        if ((intersect?.X != segS[i].X || intersect?.Y != segS[i].Y)
                        && (intersect?.X != segE[i].X || intersect?.Y != segE[i].Y))
                        {
                            if (segs == MAX_SEGS)
                            {
                                return null;
                            }

                            segS[segs] = (segS[i].X, segS[i].Y);
                            segE[segs] = ((double)intersect?.X, (double)intersect?.Y);
                            segS[i] = ((double)intersect?.X, (double)intersect?.Y);
                        }
                        if ((intersect?.X != segS[j].X || intersect?.Y != segS[j].Y)
                        && (intersect?.X != segE[j].X || intersect?.Y != segE[j].Y))
                        {
                            if (segs == MAX_SEGS)
                            {
                                return null;
                            }

                            segS[segs] = (segS[j].X, segS[j].Y);
                            segE[segs] = ((double)intersect?.X, (double)intersect?.Y);
                            segS[j] = ((double)intersect?.X, (double)intersect?.Y);
                        }
                    }
                }
            }

            //  Calculate the angle of each segment.
            for (var i = 0; i < segs; i++)
            {
                segAngle[i] = AngleOf(segE[i].X - segS[i].X, segE[i].Y - segS[i].Y);
            }

            //  4.  Build the perimeter polygon.
            var c = start.X;
            var d = start.Y;
            var a = c - 1d;
            var b = d;
            double e = 0;
            double f = 0;

            double angleDif = 0;
            var bestAngleDif = Tau;

            segRet.Add((c, d));
            corners = 1;
            while (true)
            {
                bestAngleDif = Tau;
                for (var i = 0; i < segs; i++)
                {
                    if (segS[i].X == c && segS[i].Y == d && (segE[i].X != a || segE[i].Y != b))
                    {
                        angleDif = lastAngle - segAngle[i];
                        while (angleDif >= Tau)
                        {
                            angleDif -= Tau;
                        }

                        while (angleDif < 0)
                        {
                            angleDif += Tau;
                        }

                        if (angleDif < bestAngleDif)
                        {
                            bestAngleDif = angleDif; e = segE[i].X; f = segE[i].Y;
                        }
                    }
                    if (segE[i].X == c && segE[i].Y == d && (segS[i].X != a || segS[i].Y != b))
                    {
                        angleDif = lastAngle - segAngle[i] + .5 * Tau;
                        while (angleDif >= Tau)
                        {
                            angleDif -= Tau;
                        }

                        while (angleDif < 0)
                        {
                            angleDif += Tau;
                        }

                        if (angleDif < bestAngleDif)
                        {
                            bestAngleDif = angleDif;
                            e = segS[i].X;
                            f = segS[i].Y;
                        }
                    }
                }
                if (corners > 1
                    && c == segRet[0].X
                    && d == segRet[0].Y
                    && e == segRet[1].X
                    && f == segRet[1].Y)
                {
                    corners--;
                    return segRet;
                }
                if (bestAngleDif == Tau || corners == MAX_SEGS)
                {
                    return null;
                }

                lastAngle -= bestAngleDif + .5 * Tau;
                segRet[corners++] = (e, f);
                a = c;
                b = d;
                c = e;
                d = f;
            }
        }
        #endregion Perimeter Polygon of a Polygon

        #region Perpendicular Vector in The Clockwise Direction
        /// <summary>
        /// Find the Clockwise Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// To get the perpendicular vector in two dimensions use I = -J, J = I
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) PerpendicularClockwise(double i, double j)
            => (-j, i);
        #endregion Perpendicular Vector in The Clockwise Direction

        #region Perpendicular Vector in The Counter Clockwise Direction
        /// <summary>
        /// Find the Counter Clockwise Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// To get the perpendicular vector in two dimensions use I = -J, J = I
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) PerpendicularCounterClockwise(double i, double j)
            => (j, -i);
        #endregion Perpendicular Vector in The Counter Clockwise Direction

        #region Point in Ellipse
        /// <summary>
        /// Checks whether a point is found within the boundaries of an ellipse.
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        public static Inclusion PointInEllipse(Ellipse ellipse, Point2D point)
        {
            if (ellipse.RX <= 0d || ellipse.RY <= 0d)
            {
                return Inclusion.Outside;
            }

            var cosT = Cos(-ellipse.Angle);
            var sinT = Sin(-ellipse.Angle);

            var u = point.X - ellipse.Center.X;
            var v = point.Y - ellipse.Center.Y;

            var a = (cosT * u + sinT * v) * (cosT * u + sinT * v);
            var b = (sinT * u - cosT * v) * (sinT * u - cosT * v);

            var d1Squared = 4 * ellipse.RX * ellipse.RX;
            var d2Squared = 4 * ellipse.RY * ellipse.RY;

            var normalizedRadius = (a / d1Squared)
                                    + (b / d2Squared);

            return (normalizedRadius <= 1d) ? ((Abs(normalizedRadius - 1d) < DoubleEpsilon) ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
        }
        #endregion Point in Ellipse

        #region Point in Ellipse Unrotated
        /// <summary>
        /// The unrotated ellipse contains point.
        /// </summary>
        /// <param name="ellipse">The ellipse.</param>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool UnrotatedEllipseContainsPoint(Ellipse ellipse, Point2D point)
        {
            if (ellipse.RX <= 0d || ellipse.RY <= 0d)
            {
                return false;
            }

            var u = point.X - ellipse.Center.X;
            var v = point.Y - ellipse.Center.Y;

            var a = u * u;
            var b = u * u;

            var d1Squared = ellipse.RX * ellipse.RX;
            var d2Squared = ellipse.RY * ellipse.RY;

            return (a / d1Squared)
                 + (b / d2Squared) <= 1d;
        }
        #endregion Point in Ellipse Unrotated

        #region Point in Elliptical Arc Sector
        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipticalArcSectorContainsPoint(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle,
            double sweepAngle,
            double pX, double pY,
            double epsilon = Epsilon)
            => EllipticalArcSectorContainsPoint(cX, cY, r1, r2, Cos(angle), Sin(angle), Cos(startAngle), Sin(startAngle), Cos(sweepAngle), Sin(sweepAngle), pX, pY, epsilon);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosT">The cosine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="sinT">The sine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="startCosT"></param>
        /// <param name="startSinT"></param>
        /// <param name="sweepCosT"></param>
        /// <param name="sweepSinT"></param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipticalArcSectorContainsPoint(
            double cX, double cY,
            double r1, double r2,
            double cosT, double sinT,
            double startCosT, double startSinT,
            double sweepCosT, double sweepSinT,
            double pX, double pY,
            double epsilon = Epsilon)
        {
            // If the ellipse is empty it can't contain anything.
            if (r1 <= 0d || r2 <= 0d)
            {
                return Inclusion.Outside;
            }

            var endSinT = sweepSinT * startCosT + sweepCosT * startSinT;
            var endCosT = sweepCosT * startCosT - sweepSinT * startSinT;

            // Find the start and end angles.
            var sa = EllipticalPolarVector(startCosT, startSinT, r1, r2);
            var ea = EllipticalPolarVector(endCosT, endSinT, r1, r2);

            // Ellipse equation for an ellipse at origin for the chord end points.
            var u1 = r1 * sa.cosT;
            var v1 = -(r2 * sa.sinT);
            var u2 = r1 * ea.cosT;
            var v2 = -(r2 * ea.sinT);

            // Find the points of the chord.
            var sX = cX + (u1 * cosT + v1 * sinT);
            var sY = cY + (u1 * sinT - v1 * cosT);
            var eX = cX + (u2 * cosT + v2 * sinT);
            var eY = cY + (u2 * sinT - v2 * cosT);

            // Find the determinant of the chord.
            var determinant = (sX - pX) * (eY - pY) - (eX - pX) * (sY - pY);

            // Check if the point is on the chord.
            if (Abs(determinant) <= Epsilon)
            {
                return (sX < eX) ?
                (sX <= pX && pX <= eX) ? Inclusion.Boundary : Inclusion.Outside :
                (eX <= pX && pX <= sX) ? Inclusion.Boundary : Inclusion.Outside;
            }

            // Check whether the point is on the side of the chord as the center.
            if (Sign(-determinant) == Sign(sweepSinT * sweepCosT))
            {
                return Inclusion.Outside;
            }

            // Translate points to origin.
            var u0 = pX - cX;
            var v0 = pY - cY;

            // Apply the rotation transformation.
            var a = u0 * cosT + v0 * sinT;
            var b = u0 * sinT - v0 * cosT;

            // Normalize the radius.
            var normalizedRadius
                = (a * a / (r1 * r1))
                + (b * b / (r2 * r2));

            return (normalizedRadius <= 1d)
                ? ((Abs(normalizedRadius - 1d) < Epsilon)
                ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipticalArcSectorContainsPoint0(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle,
            double sweepAngle,
            double pX, double pY,
            double epsilon = Epsilon)
            => EllipticalArcSectorContainsPoint0(cX, cY, r1, r2, Cos(angle), Sin(angle), startAngle, sweepAngle, pX, pY, epsilon);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosT">The cosine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="sinT">The sine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipticalArcSectorContainsPoint0(
            double cX, double cY,
            double r1, double r2,
            double cosT, double sinT,
            double startAngle, double sweepAngle,
            double pX, double pY,
            double epsilon = Epsilon)
        {
            // If the ellipse is empty it can't contain anything.
            if (r1 <= 0d || r2 <= 0d)
            {
                return Inclusion.Outside;
            }

            var endAngle = startAngle + sweepAngle;

            // Find the start and end angles.
            var sa = EllipticalPolarAngle(startAngle, r1, r2);
            var ea = EllipticalPolarAngle(endAngle, r1, r2);

            // Ellipse equation for an ellipse at origin for the chord end points.
            var u1 = r1 * Cos(sa);
            var v1 = -(r2 * Sin(sa));
            var u2 = r1 * Cos(ea);
            var v2 = -(r2 * Sin(ea));

            // Find the points of the chord.
            var sX = cX + (u1 * cosT + v1 * sinT);
            var sY = cY + (u1 * sinT - v1 * cosT);
            var eX = cX + (u2 * cosT + v2 * sinT);
            var eY = cY + (u2 * sinT - v2 * cosT);

            // Find the determinant of the chord.
            var determinant = (sX - pX) * (eY - pY) - (eX - pX) * (sY - pY);

            // Check if the point is on the chord.
            if (Abs(determinant) <= Epsilon)
            {
                return (sX < eX) ?
                (sX <= pX && pX <= eX) ? Inclusion.Boundary : Inclusion.Outside :
                (eX <= pX && pX <= sX) ? Inclusion.Boundary : Inclusion.Outside;
            }

            // Check whether the point is on the side of the chord as the center.
            if (Sign(determinant) == Sign(sweepAngle))
            {
                return Inclusion.Outside;
            }

            // Translate points to origin.
            var u0 = pX - cX;
            var v0 = pY - cY;

            // Apply the rotation transformation.
            var a = u0 * cosT + v0 * sinT;
            var b = u0 * sinT - v0 * cosT;

            // Normalize the radius.
            var normalizedRadius
                = (a * a / (r1 * r1))
                + (b * b / (r2 * r2));

            return (normalizedRadius <= 1d)
                ? ((Abs(normalizedRadius - 1d) < Epsilon)
                ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipticalArcContainsPoint1(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle,
            double sweepAngle,
            double pX, double pY,
            double epsilon = Epsilon)
            => EllipticalArcContainsPoint1(cX, cY, r1, r2, Cos(angle), Sin(angle), Cos(startAngle), Sin(startAngle), Cos(sweepAngle), Sin(sweepAngle), pX, pY, epsilon);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="sinT">The sine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="cosT">The cosine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="startCosT"></param>
        /// <param name="startSinT"></param>
        /// <param name="sweepCosT"></param>
        /// <param name="sweepSinT"></param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// https://math.stackexchange.com/a/1760296
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipticalArcContainsPoint1(
            double cX, double cY,
            double r1, double r2,
            double cosT, double sinT,
            double startCosT, double startSinT,
            double sweepCosT, double sweepSinT,
            double pX, double pY,
            double epsilon = Epsilon)
        {
            // If the ellipse is empty it can't contain anything.
            if (r1 <= 0d || r2 <= 0d)
            {
                return Inclusion.Outside;
            }

            // If the Sweep angle is Tau, the EllipticalArc must be an Ellipse.
            if (Abs(sweepCosT - 1d) < epsilon && Abs(sweepSinT) < epsilon)
            {
                return Intersections.EllipseContainsPoint(cX, cY, r1, r2, sinT, cosT, pX, pY);
            }

            var endSinT = sweepSinT * startCosT + sweepCosT * startSinT;
            var endCosT = sweepCosT * startCosT - sweepSinT * startSinT;

            // Find the start and end angles.
            var sa = EllipticalPolarVector(startCosT, startSinT, r1, r2);
            var ea = EllipticalPolarVector(endCosT, endSinT, r1, r2);

            // Ellipse equation for an ellipse at origin for the chord end points.
            var u1 = r1 * sa.cosT;
            var v1 = -(r2 * sa.sinT);
            var u2 = r1 * ea.cosT;
            var v2 = -(r2 * ea.sinT);

            // Find the points of the chord.
            var sX = cX + (u1 * cosT + v1 * sinT);
            var sY = cY + (u1 * sinT - v1 * cosT);
            var eX = cX + (u2 * cosT + v2 * sinT);
            var eY = cY + (u2 * sinT - v2 * cosT);

            // Find the determinant of the chord.
            var determinant = (sX - pX) * (eY - pY) - (eX - pX) * (sY - pY);

            // Check whether the point is on the same side of the chord as the center.
            if (Sign(-determinant) == Sign(sweepSinT * sweepCosT))
            {
                return Inclusion.Outside;
            }

            // Translate point to origin.
            var u0 = pX - cX;
            var v0 = pY - cY;

            // Apply the rotation transformation to the point at the origin.
            var a = u0 * cosT + v0 * sinT;
            var b = u0 * sinT - v0 * cosT;

            // Normalize the radius.
            var normalizedRadius
                = (a * a / (r1 * r1))
                + (b * b / (r2 * r2));

            return (normalizedRadius <= 1d)
                ? ((Abs(normalizedRadius - 1d) < epsilon)
                ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipticalArcContainsPoint2(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle,
            double sweepAngle,
            double pX, double pY,
            double epsilon = Epsilon)
            => EllipticalArcContainsPoint2(cX, cY, r1, r2, Cos(angle), Sin(angle), Cos(startAngle), Sin(startAngle), Cos(sweepAngle), Sin(sweepAngle), pX, pY, epsilon);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="sinT">The sine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="cosT">The cosine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="startCosT"></param>
        /// <param name="startSinT"></param>
        /// <param name="sweepCosT"></param>
        /// <param name="sweepSinT"></param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// https://math.stackexchange.com/a/1760296
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipticalArcContainsPoint2(
            double cX, double cY,
            double r1, double r2,
            double cosT, double sinT,
            double startCosT, double startSinT,
            double sweepCosT, double sweepSinT,
            double pX, double pY,
            double epsilon = Epsilon)
        {
            // If the ellipse is empty it can't contain anything.
            if (r1 <= 0d || r2 <= 0d)
            {
                return Inclusion.Outside;
            }

            // If the Sweep angle is Tau, the EllipticalArc must be an Ellipse.
            if (Abs(sweepCosT - 1d) < epsilon && Abs(sweepSinT) < epsilon)
            {
                return Intersections.EllipseContainsPoint(cX, cY, r1, r2, sinT, cosT, pX, pY);
            }

            var endSinT = sweepSinT * startCosT + sweepCosT * startSinT;
            var endCosT = sweepCosT * startCosT - sweepSinT * startSinT;

            // ToDo: Simplify out Atan2
            var startAngle = Atan2(startSinT, startCosT);
            var endAngle = Atan2(endSinT, endCosT);

            // Find the start and end angles.
            var sa = EllipticalPolarAngle(startAngle, r1, r2);
            var ea = EllipticalPolarAngle(endAngle, r1, r2);

            // Ellipse equation for an ellipse at origin for the chord end points.
            var u1 = r1 * Cos(sa);
            var v1 = -(r2 * Sin(sa));
            var u2 = r1 * Cos(ea);
            var v2 = -(r2 * Sin(ea));

            // Find the points of the chord.
            var sX = cX + (u1 * cosT + v1 * sinT);
            var sY = cY + (u1 * sinT - v1 * cosT);
            var eX = cX + (u2 * cosT + v2 * sinT);
            var eY = cY + (u2 * sinT - v2 * cosT);

            // Find the determinant of the chord.
            var determinant = (sX - pX) * (eY - pY) - (eX - pX) * (sY - pY);

            // Check whether the point is on the same side of the chord as the center.
            if (Sign(-determinant) == Sign(sweepSinT * sweepCosT))
            {
                return Inclusion.Outside;
            }

            // Translate point to origin.
            var u0 = pX - cX;
            var v0 = pY - cY;

            // Apply the rotation transformation to the point at the origin.
            var a = u0 * cosT + v0 * sinT;
            var b = u0 * sinT - v0 * cosT;

            // Normalize the radius.
            var normalizedRadius
                = (a * a / (r1 * r1))
                + (b * b / (r2 * r2));

            return (normalizedRadius <= 1d)
                ? ((Abs(normalizedRadius - 1d) < epsilon)
                ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
        }
        #endregion Point in Elliptical Arc Sector

        #region Point in Rectangle
        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static Inclusion Contains(Rectangle2D rectangle, Point2D point) => (rectangle.X <= point.X
            && point.X < rectangle.X + rectangle.Width
            && rectangle.Y <= point.Y
            && point.Y < rectangle.Y + rectangle.Height) ? Inclusion.Inside : Inclusion.Outside;

        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static Inclusion Contains2(Rectangle2D rectangle, Point2D point)
        {
            if (((Abs(rectangle.X - point.X) < DoubleEpsilon
                || Abs(rectangle.Bottom - point.X) < DoubleEpsilon)
                && ((rectangle.Y <= point.Y) == (rectangle.Bottom >= point.Y)))
             || ((Abs(rectangle.Right - point.Y) < DoubleEpsilon
             || Abs(rectangle.Left - point.Y) < DoubleEpsilon)
             && ((rectangle.X <= point.X) == (rectangle.Right >= point.X))))
            {
                return Inclusion.Boundary;
            }

            return (rectangle.X <= point.X
                && point.X < rectangle.X + rectangle.Width
                && rectangle.Y <= point.Y
                && point.Y < rectangle.Y + rectangle.Height) ? Inclusion.Inside : Inclusion.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static Inclusion PointOnRectangleX(Rectangle2D rectangle, Point2D point)
        {
            var top = Sqrt((rectangle.TopRight.X - rectangle.TopLeft.X) * (rectangle.TopRight.X - rectangle.TopLeft.X) + (rectangle.TopRight.Y - rectangle.TopLeft.Y) * (rectangle.TopRight.Y - rectangle.TopLeft.Y));
            var right = Sqrt((rectangle.BottomRight.X - rectangle.TopRight.X) * (rectangle.BottomRight.X - rectangle.TopRight.X) + (rectangle.BottomRight.Y - rectangle.TopRight.Y) * (rectangle.BottomRight.Y - rectangle.TopRight.Y));
            var tlp = (point.X - rectangle.TopLeft.X) * (point.X - rectangle.TopLeft.X) + (point.Y - rectangle.TopLeft.Y) * (point.Y - rectangle.TopLeft.Y);
            var trp = (point.X - rectangle.TopRight.X) * (point.X - rectangle.TopRight.X) + (point.Y - rectangle.TopRight.Y) * (point.Y - rectangle.TopRight.Y);
            var brp = (point.X - rectangle.BottomRight.X) * (point.X - rectangle.BottomRight.X) + (point.Y - rectangle.BottomRight.Y) * (point.Y - rectangle.BottomRight.Y);
            var blp = (point.X - rectangle.BottomLeft.X) * (point.X - rectangle.BottomLeft.X) + (point.Y - rectangle.BottomLeft.Y) * (point.Y - rectangle.BottomLeft.Y);

            if (Abs(top - Sqrt(tlp - trp)) < DoubleEpsilon
                || Abs(right - Sqrt(trp - brp)) < DoubleEpsilon
                || Abs(top - Sqrt(brp - blp)) < DoubleEpsilon
                || Abs(right - Sqrt(blp - tlp)) < DoubleEpsilon)
            {
                return Inclusion.Boundary;
            }

            return (rectangle.X <= point.X
                && point.X < rectangle.X + rectangle.Width
                && rectangle.Y <= point.Y
                && point.Y < rectangle.Y + rectangle.Height) ? Inclusion.Inside : Inclusion.Outside;
        }
        #endregion Point in Rectangle

        #region Point Near Ellipse
        /// <summary>
        /// Return True if the point is inside the ellipse
        /// (expanded by distance close_distance vertically
        /// and horizontally).
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="close_distance"></param>
        /// <returns></returns>
        public static bool PointNearEllipse(double px, double py, double x1, double y1, double x2, double y2, double close_distance)
        {
            var a = (Abs(x2 - x1) / 2) + close_distance;
            var b = (Abs(y2 - y1) / 2) + close_distance;
            px = px - (x2 + x1) / 2;
            py = py - (y2 + y1) / 2;
            return (px * px / (a * a)) + (py * py / (b * b)) <= 1;
        }
        #endregion Point Near Ellipse

        #region Point Near a Line Segment
        /// <summary>
        /// The point near segment.
        /// </summary>
        /// <param name="px">The px.</param>
        /// <param name="py">The py.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="close_distance">The close_distance. Return True if (px, py) is within close_distance if the segment from (x1, y1) to (X2, y2).</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool PointNearSegment(double px, double py, double x1, double y1, double x2, double y2, double close_distance)
            => DistToSegment2(px, py, x1, y1, x2, y2) <= close_distance;

        /// <summary>
        /// Return True if (px, py) is within close_distance if the segment from (x1, y1) to (X2, y2).
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="close_distance"></param>
        /// <returns></returns>
        public static bool PointNearSegment2(double px, double py, double x1, double y1, double x2, double y2, double close_distance)
            => DistToSegment(px, py, x1, y1, x2, y2) <= close_distance;
        #endregion Point Near a Line Segment

        #region Point On Line Segment
        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(RoundTests))]
        public static List<SpeedTester> PointOnLineSegmentTests()
            => new List<SpeedTester> {
                new SpeedTester(() => PointOnLineSegment(1, 1, 2, 2, 1.5, 1.5),
                $"{nameof(Experiments.PointOnLineSegment)}(1, 1, 2, 2, 1.5, 1.5)"),
                new SpeedTester(() => PointLineSegment(1, 1, 2, 2, 1.5, 1.5),
                $"{nameof(Experiments.PointLineSegment)}(1, 1, 2, 2, 1.5, 1.5)"),
                new SpeedTester(() => PointOnLine( new LineSegment(1, 1, 2, 2), new Point2D( 1.5, 1.5)),
                $"{nameof(Experiments.PointOnLine)}(1, 1, 2, 2, 1.5, 1.5)")
            };

        /// <summary>
        /// The point on line segment.
        /// </summary>
        /// <param name="segmentAX">The segmentAX.</param>
        /// <param name="segmentAY">The segmentAY.</param>
        /// <param name="segmentBX">The segmentBX.</param>
        /// <param name="segmentBY">The segmentBY.</param>
        /// <param name="pointX">The pointX.</param>
        /// <param name="pointY">The pointY.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool PointOnLineSegment(
            double segmentAX,
            double segmentAY,
            double segmentBX,
            double segmentBY,
            double pointX,
            double pointY)
            => ((Abs(pointX - segmentAX) < DoubleEpsilon)
            && (Abs(pointY - segmentAY) < DoubleEpsilon))
            || ((Abs(pointX - segmentBX) < DoubleEpsilon)
            && (Abs(pointY - segmentBY) < DoubleEpsilon))
            || (((pointX > segmentAX) == (pointX < segmentBX))
            && ((pointY > segmentAY) == (pointY < segmentBY))
            && (Abs((pointX - segmentAX) * (segmentBY - segmentAY) - (segmentBX - segmentAX) * (pointY - segmentAY)) < DoubleEpsilon));

        /// <summary>
        /// The point line segment.
        /// </summary>
        /// <param name="segmentAX">The segmentAX.</param>
        /// <param name="segmentAY">The segmentAY.</param>
        /// <param name="segmentBX">The segmentBX.</param>
        /// <param name="segmentBY">The segmentBY.</param>
        /// <param name="pointX">The pointX.</param>
        /// <param name="pointY">The pointY.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com/delphi/clipper.php
        /// </acknowledgment>
        public static bool PointLineSegment(
            double segmentAX,
            double segmentAY,
            double segmentBX,
            double segmentBY,
            double pointX,
            double pointY)
            => ((pointX == segmentAX) && (pointY == segmentAY)) ||
                ((pointX == segmentBX) && (pointY == segmentBY)) ||
                (((pointX > segmentAX) == (pointX < segmentBX)) &&
                ((pointY > segmentAY) == (pointY < segmentBY)) &&
                ((pointX - segmentAX) * (segmentBY - segmentAY) ==
                (segmentBX - segmentAX) * (pointY - segmentAY)));

        /// <summary>
        /// The point line segment.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="a1">The a1.</param>
        /// <param name="a2">The a2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// From: http://stackoverflow.com/questions/2255842/detecting-coincident-subset-of-two-coincident-line-segments/2255848
        /// </acknowledgment>
        public static bool PointLineSegment(Point2D p, Point2D a1, Point2D a2)
        {
            var dummyU = 0.0d;
            var d = DistFromSeg(p, a1, a2, Epsilon, ref dummyU);
            return d < Epsilon;
        }

        /// <summary>
        /// The point on line.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool PointOnLine(LineSegment segment, Point2D point)
        {
            var Length1 = point.Distance(segment.B);
            // Sqrt((Point.X - Line.B.X) ^ 2 + (Point.Y - Line.B.Y))
            var Length2 = point.Distance(segment.A);
            // Sqrt((Point.X - Line.A.X) ^ 2 + (Point.Y - Line.A.Y))
            return Abs(segment.Length() - Length1 + Length2) < DoubleEpsilon;
        }
        #endregion Point On Line Segment

        #region Points Are Close
        /// <summary>
        /// Return True if (x1, y1) is within close_distance vertically and horizontally of (x2, y2).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        public static bool AreClose(double x1, double y1, double x2, double y2, double epsilon = DoubleEpsilon)
            => (Abs(x2 - x1) <= epsilon) && (Abs(y2 - y1) <= epsilon);

        /// <summary>
        /// Compares two points for fuzzy equality.  This function
        /// helps compensate for the fact that double values can
        /// acquire error when operated upon
        /// </summary>
        /// <param name='point1'>The first point to compare</param>
        /// <param name='point2'>The second point to compare</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Whether or not the two points are equal</returns>
        public static bool AreClose(Point2D point1, Point2D point2, double epsilon = DoubleEpsilon)
            => Maths.AreClose(point1.X, point2.X, epsilon)
            && Maths.AreClose(point1.Y, point2.Y, epsilon);

        /// <summary>
        /// Compares two Size instances for fuzzy equality.  This function
        /// helps compensate for the fact that double values can
        /// acquire error when operated upon
        /// </summary>
        /// <param name='size1'>The first size to compare</param>
        /// <param name='size2'>The second size to compare</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Whether or not the two Size instances are equal</returns>
        public static bool AreClose(Size2D size1, Size2D size2, double epsilon = DoubleEpsilon)
            => Maths.AreClose(size1.Width, size2.Width, epsilon)
            && Maths.AreClose(size1.Height, size2.Height, epsilon);

        /// <summary>
        /// Compares two Vector instances for fuzzy equality.  This function
        /// helps compensate for the fact that double values can
        /// acquire error when operated upon
        /// </summary>
        /// <param name='vector1'>The first Vector to compare</param>
        /// <param name='vector2'>The second Vector to compare</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Whether or not the two Vector instances are equal</returns>
        public static bool AreClose(Vector2D vector1, Vector2D vector2, double epsilon = DoubleEpsilon)
            => Maths.AreClose(vector1.I, vector2.I, epsilon)
            && Maths.AreClose(vector1.J, vector2.J, epsilon);
        #endregion Points Are Close

        #region Power of Two
        /// <summary>
        /// Determines if value is powered by two.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <returns><c>true</c> if <c>value</c> is powered by two; otherwise <c>false</c>.</returns>
        public static bool IsPowerOfTwo(int value)
            => (value > 0) && ((value & (value - 1)) == 0);
        #endregion Power of Two

        #region Quadratic Bézier Length Approximations
        /// <summary>
        /// Closed-form solution to elliptic integral for arc length.
        /// </summary>
        /// <param name="pointA">The starting node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointB">The middle tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointC">The closing node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://algorithmist.wordpress.com/2009/01/05/quadratic-bezier-arc-length/
        /// </acknowledgment>
        public static double QuadraticBezierArcLengthByIntegral(Point2D pointA, Point2D pointB, Point2D pointC)
        {
            var ax = pointA.X - 2 * pointB.X + pointC.X;
            var ay = pointA.Y - 2 * pointB.Y + pointC.Y;
            var bx = 2 * pointB.X - 2 * pointA.X;
            var by = 2 * pointB.Y - 2 * pointA.Y;

            var a = 4 * (ax * ax + ay * ay);
            var b = 4 * (ax * bx + ay * by);
            var c = bx * bx + by * by;

            var abc = 2 * Sqrt(a + b + c);
            var a2 = Sqrt(a);
            var a32 = 2 * a * a2;
            var c2 = 2 * Sqrt(c);
            var ba = b / a2;

            return (a32 * abc + a2 * b * (abc - c2) + (4 * c * a - b * b) * Log((2 * a2 + ba + abc) / (ba + c2))) / (4 * a32);
        }

        /// <summary>
        /// Naive computation of arc length by summing up small segment lengths.
        /// </summary>
        /// <param name="pointA">The starting node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointB">The middle tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointC">The closing node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://algorithmist.wordpress.com/2009/01/05/quadratic-bezier-arc-length/
        /// </acknowledgment>
        public static double QuadraticBezierArcLengthBySegments(Point2D pointA, Point2D pointB, Point2D pointC)
        {
            double length = 0;
            var p = new Point2D(Interpolators.QuadraticBezier(pointA.X, pointA.Y, pointB.X, pointB.Y, pointC.X, pointC.Y, 0));
            var prevX = p.X;
            var prevY = p.Y;
            for (var t = 0.005; t <= 1.0; t += 0.005)
            {
                p = new Point2D(Interpolators.QuadraticBezier(pointA.Y, pointA.X, pointB.Y, pointB.X, pointC.X, pointC.Y, t));
                var deltaX = p.X - prevX;
                var deltaY = p.Y - prevY;
                length += Sqrt(deltaX * deltaX + deltaY * deltaY);

                prevX = p.X;
                prevY = p.Y;
            }

            // exercise:  due to roundoff, it's possible to miss a small segment at the end.  how to compensate??
            return length;
        }

        /// <summary>
        /// Approximate arc-length by Gauss-Legendre numerical integration.
        /// </summary>
        /// <param name="pointA">The starting node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointB">The middle tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointC">The closing node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://algorithmist.wordpress.com/2009/01/05/quadratic-bezier-arc-length/
        /// https://code.google.com/archive/p/degrafa/source/default/source
        /// </acknowledgment>
        public static double QuadraticBezierApproxArcLength(Point2D pointA, Point2D pointB, Point2D pointC)
        {
            double sum = 0;

            // Compute the quadratic bezier polynomial coefficients.
            var coeff0X = pointA.X;
            var coeff0Y = pointA.Y;
            var coeff1X = 2.0 * (pointB.X - pointA.X);
            var coeff1Y = 2.0 * (pointB.Y - pointA.Y);
            var coeff2X = pointA.X - 2.0 * pointB.X + pointC.X;
            var coeff2Y = pointA.Y - 2.0 * pointB.Y + pointC.Y;

            // Count should be between 2 and 8 to align with MathExtensions.abscissa and MathExtensions.weight.
            const int count = 5;
            const int startl = (count == 2) ? 0 : count * (count - 1) / 2 - 1;

            // Minimum, maximum, and scalers.
            const double min = 0;
            const double max = 1;
            const double mult = 0.5 * (max - min);
            const double ab2 = 0.5 * (min + max);

            double theta = 0;
            double xPrime = 0;
            double yPrime = 0;
            double integrand = 0;

            // Evaluate the integral arc length along entire curve from t=0 to t=1.
            for (var index = 0; index < count; ++index)
            {
                theta = ab2 + mult * abscissa[startl + index];

                // First-derivative of the quadratic bezier.
                xPrime = coeff1X + 2.0 * coeff2X * theta;
                yPrime = coeff1Y + 2.0 * coeff2Y * theta;

                // Integrand for Gauss-Legendre numerical integration.
                integrand = Sqrt(xPrime * xPrime + yPrime * yPrime);

                sum += integrand * weight[startl + index];
            }

            return Abs(mult) < DoubleEpsilon ? sum : mult * sum;
        }
        #endregion Quadratic Bézier Length Approximations

        #region Rectangle Center
        /// <summary>
        /// Extension method to find the center point of a rectangle.
        /// </summary>
        /// <param name="rectangle">The <see cref="RectangleF"/> of which you want the center.</param>
        /// <returns>A <see cref="PointF"/> representing the center point of the <see cref="RectangleF"/>.</returns>
        /// <acknowledgment>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.
        /// </acknowledgment>
        public static PointF Center0(RectangleF rectangle) => new PointF(
            rectangle.Left + (rectangle.Right - rectangle.Left) * 0.5f,
            rectangle.Top + (rectangle.Bottom - rectangle.Top) * 0.5f
        );

        /// <summary>
        /// Extension method to find the center point of a rectangle.
        /// </summary>
        /// <param name="rectangle">The <see cref="RectangleF"/> of which you want the center.</param>
        /// <returns>A <see cref="PointF"/> representing the center point of the <see cref="RectangleF"/>.</returns>
        /// <acknowledgment>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.
        /// </acknowledgment>
        public static PointF Center1(RectangleF rectangle) => new PointF(
            (rectangle.Left + rectangle.Right) * 0.5f,
            (rectangle.Top + rectangle.Bottom) * 0.5f
        );
        #endregion Rectangle Center

        #region Rectangle To Square
        /// <summary>
        /// The to square.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        public static Rectangle2D ToSquare(Rectangle2D rect)
        {
            var smallest = rect.Height <= rect.Width ? rect.Height : rect.Width;
            return new Rectangle2D(
                rect.X + ((rect.Width - smallest) * 0.5d),
                rect.Y + ((rect.Height - smallest) * 0.5d),
                smallest,
                smallest);
        }
        #endregion Rectangle To Square

        #region Rectangle Has NaN
        /// <summary>
        /// rectHasNaN - this returns true if this rectangle has X, Y , Height or Width as NaN.
        /// </summary>
        /// <param name='rect'>The rectangle to test</param>
        /// <returns>returns whether the Rectangle has NaN</returns>
        public static bool RectHasNaN(Rectangle2D rect) => double.IsNaN(rect.X)
             || double.IsNaN(rect.Y)
             || double.IsNaN(rect.Height)
             || double.IsNaN(rect.Width);
        #endregion Rectangle Has NaN

        #region Rectangles Are Close
        /// <summary>
        /// Compares two rectangles for fuzzy equality.  This function
        /// helps compensate for the fact that double values can
        /// acquire error when operated upon
        /// </summary>
        /// <param name='rect1'>The first rectangle to compare</param>
        /// <param name='rect2'>The second rectangle to compare</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Whether or not the two rectangles are equal</returns>
        public static bool AreClose(Rectangle2D rect1, Rectangle2D rect2, double epsilon = DoubleEpsilon)
        {
            // If they're both empty, don't bother with the double logic.
            if (rect1.IsEmpty)
            {
                return rect2.IsEmpty;
            }

            // At this point, rect1 isn't empty, so the first thing we can test is
            // rect2.IsEmpty, followed by property-wise compares.
            return (!rect2.IsEmpty)
                && Maths.AreClose(rect1.X, rect2.X, epsilon)
                && Maths.AreClose(rect1.Y, rect2.Y, epsilon)
                && Maths.AreClose(rect1.Height, rect2.Height, epsilon)
                && Maths.AreClose(rect1.Width, rect2.Width, epsilon);
        }
        #endregion Rectangles Are Close

        #region Remove Point
        /// <summary>
        /// Remove point target from the array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="target"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// </acknowledgment>
        public static void RemovePoint(PolygonContour polygon, int target)
            => polygon.Points.RemoveAt(target);

        /// <summary>
        /// Remove point target from the array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="target"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// </acknowledgment>
        public static void RemovePoint1(PolygonContour polygon, int target)
        {
            var points = new Point2D[polygon.Points.Count - 1];
            //List.Copy(polygon.Points, 0, points, 0, target);
            Array.Copy(polygon.Points.ToArray(), target + 1, points, target, polygon.Points.Count - target - 1);
            polygon.Points = points.ToList();
        }
        #endregion Remove Point

        #region Remove Polygon Ear
        /// <summary>
        /// Remove an ear from the polygon and add it to the triangles array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="triangles"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// </acknowledgment>
        public static void RemoveEar(PolygonContour polygon, List<Triangle> triangles)
        {
            // Find an ear.
            var A = 0;
            var B = 0;
            var C = 0;

            // Create a new triangle for the ear.
            triangles.Add(FindEar(polygon, ref A, ref B, ref C));

            // Remove the ear from the polygon.
            RemovePoint(polygon, B);
        }
        #endregion Remove Polygon Ear

        #region Retrieve Cursor Resource
        /// <summary>
        /// Retrieve Cursor Resource from Executable
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// BE SURE (embedded).cur HAS BUILD ACTION IN PROPERTIES SET TO EMBEDDED RESOURCE!!
        /// </acknowledgment>
        public static Cursor RetriveCursorResource(string ResourceName)
        {
            //  Get the namespace
            var strNameSpace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            //  Get the resource into a stream
            var ResourceStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(strNameSpace + "." + ResourceName);
            if (ResourceStream is null)
            {
                // TODO: #If Then ... Warning!!! not translated
                MessageBox.Show("Unable to find: "
                                + ResourceName + "\r\n" + "Be Sure "
                                + ResourceName + " Property Build Action is set to Embedded Resource" + "\r\n" + "Another reason can be that the Project Root Namespace is not the same as the Assembly Name");
                // TODO: # ... Warning!!! not translated
            }
            else
            {
                //  ToDo: Report the Error message in a nicer fashion since this in game.
                //  Perhaps on Exit provide a message errors were encountered and
                //  ignored would you like to send an error report?
                // TODO: #End If ... Warning!!! not translated
                return Cursors.Default;
            }
            //  Return the Resource as a cursor
            if (ResourceStream.CanRead)
            {
                return new Cursor(ResourceStream);
            }
            else
            {
                return Cursors.Default;
            }
        }
        #endregion Retrieve Cursor Resource

        #region Rotated Rectangle Bounds
        /// <summary>
        /// The rotated rectangle bounds.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        public static Rectangle2D RotatedRectangleBounds(double x, double y, double width, double height, Point2D fulcrum, double angle)
        {
            var cosAngle = Abs(Cos(angle));
            var sinAngle = Abs(Sin(angle));

            var size = new Size2D(
                (cosAngle * width) + (sinAngle * height),
                (cosAngle * height) + (sinAngle * width)
                );

            var loc = new Point2D(
                fulcrum.X + (-width / 2 * cosAngle + -height / 2 * sinAngle),
                fulcrum.Y + (-width / 2 * sinAngle + -height / 2 * cosAngle)
                );

            return new Rectangle2D(loc, size);
        }
        #endregion Rotated Rectangle Bounds

        #region Rotated Rectangle Points
        /// <summary>
        /// The rotated rectangle corners.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> RotatedRectangleCorners(double x, double y, double width, double height, Point2D fulcrum, double angle)
        {
            var points = new List<Point2D>();

            var xaxis = new Point2D(Cos(angle), Sin(angle));
            var yaxis = new Point2D(-Sin(angle), Cos(angle));

            // Apply the rotation transformation and translate to new center.
            points.Add(new Point2D(
                fulcrum.X + (-width / 2 * xaxis.X + -height / 2 * xaxis.Y),
                fulcrum.Y + (-width / 2 * yaxis.X + -height / 2 * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrum.X + (width / 2 * xaxis.X + -height / 2 * xaxis.Y),
                fulcrum.Y + (width / 2 * yaxis.X + -height / 2 * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrum.X + (width / 2 * xaxis.X + height / 2 * xaxis.Y),
                fulcrum.Y + (width / 2 * yaxis.X + height / 2 * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrum.X + (-width / 2 * xaxis.X + height / 2 * xaxis.Y),
                fulcrum.Y + (-width / 2 * yaxis.X + height / 2 * yaxis.Y)
                ));

            return points;
        }
        #endregion Rotated Rectangle Points

        #region Rotation Matrix
        /// <summary>
        /// Creates a matrix to rotate an object around a particular point.
        /// </summary>
        /// <param name="center">The point around which to rotate.</param>
        /// <param name="angle">The angle to rotate in radians.</param>
        /// <returns>Return a rotation matrix to rotate around a point.</returns>
        public static Matrix3x2D RotateAroundPoint(Point2D center, double angle)
        {
            // Translate the point to the origin.
            var result = new Matrix3x2D();

            // We need to go counter-clockwise.
            result.RotateAt(-angle.ToDegrees(), center.X, center.Y);

            return result;
        }
        #endregion Rotation Matrix

        #region Round
        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(RoundTests))]
        public static List<SpeedTester> RoundTests()
        {
            const double value = 0.5d;
            return new List<SpeedTester> {
                new SpeedTester(() => RoundAFZ(value),
                $"{nameof(Experiments.RoundAFZ)}({value})"),
                new SpeedTester(() => RoundToEven(value),
                $"{nameof(Experiments.RoundToEven)}({value})"),
                new SpeedTester(() => RoundToInt32(value),
                $"{nameof(Experiments.RoundToInt32)}({value})"),
                new SpeedTester(() => Round(value),
                $"{nameof(Experiments.Round)}({value})"),
                new SpeedTester(() => Truncate(value),
                $"{nameof(Experiments.Truncate)}({value})")
            };
        }

        /// <summary>
        /// Away from zero rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static double RoundAFZ(double value, int decimals)
            => Math.Round(value, decimals, MidpointRounding.AwayFromZero);

        /// <summary>
        /// Away from zero rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double RoundAFZ(double value)
            => Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// To Even, or Bankers rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double RoundToEven(double value)
            => Math.Round(value, 0, MidpointRounding.ToEven);

        /// <summary>
        /// To Even, or Bankers rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double RoundToInt32(double value)
            => Convert.ToInt32(value);

        /// <summary>
        /// Away from zero rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Round(double value)
            => value < 0 ? (int)(value - 0.5) : (int)(value + 0.5);

        /// <summary>
        /// Truncate rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Truncate(double value)
            => (int)value;
        #endregion Round

        #region Self Intersecting Bézier
        // https://github.com/Parclytaxel/Kinross/blob/master/kinback/segment.py
        #endregion Self Intersecting Bézier

        #region Sign
        // sign of number
        /// <summary>
        /// The sign0.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double Sign0(double x)
            => (x < 0d) ? -1 : 1;
        #endregion Sign

        #region Signed Triangle Area
        /// <summary>
        /// Set of Finds the signed area of a triangle.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(SignedTriangleArea))]
        public static List<SpeedTester> SignedTriangleArea()
            => new List<SpeedTester> {
                new SpeedTester(() => SignedTriangleArea(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.SignedTriangleArea)}(0, 0, 0, 1, 1, 1)"),
                new SpeedTester(() => SignedTriangleAreaW8R(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.SignedTriangleAreaW8R)}(0, 0, 0, 1, 1, 1)"),
            };

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
        /// <acknowledgment>
        /// From Farseer Physics Engine.
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedTriangleArea(double aX, double aY, double bX, double bY, double cX, double cY)
            => aX * (bY - cY) + bX * (cY - aY) + cX * (aY - bY);

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
        /// <acknowledgment>
        /// w8r
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedTriangleAreaW8R(double aX, double aY, double bX, double bY, double cX, double cY)
            => (aX - cX) * (bY - cY) - (bX - cX) * (aY - cY);
        #endregion Signed Triangle Area

        #region Sine Cosine Lookup
        /// <summary>
        /// Set of tests to lookup the Sin and Cos of a radian angle.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(SinCosLookupTableTests))]
        public static List<SpeedTester> SinCosLookupTableTests()
        {
            var rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            var value = default(double);
            return new List<SpeedTester> {
                new SpeedTester(() => SinCos0(Maths.WrapAngle(value += Radian + ClearSinCosTable())),
                $"{nameof(Experiments.SinCos0)}({Maths.WrapAngle(value += Radian)})"),
                new SpeedTester(() => SinCos1(Maths.WrapAngle(value += Radian + ClearSinCosTable())),
                $"{nameof(Experiments.SinCos1)}({Maths.WrapAngle(value += Radian)})"),
                new SpeedTester(() => SinCos2(Maths.WrapAngle(value += Radian + ClearSinCosTable())),
                $"{nameof(Experiments.SinCos2)}({Maths.WrapAngle(value += Radian)})"),
                new SpeedTester(() => SinCos3(Maths.WrapAngle(value += Radian + ClearSinCosTable())),
                $"{nameof(Experiments.SinCos3)}({Maths.WrapAngle(value += Radian)})"),
                new SpeedTester(() => SinCos4(Maths.WrapAngle(value += Radian + ClearSinCosTable())),
                $"{nameof(Experiments.SinCos4)}({Maths.WrapAngle(value += Radian)})"),
            };
        }

        /// <summary>
        /// The sin cos table.
        /// </summary>
        public static Dictionary<double, (double, double)?> sinCosTable = new Dictionary<double, (double, double)?>();

        /// <returns></returns>
        /// <summary>
        /// Clear the sin cos table.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        public static double ClearSinCosTable()
        {
            sinCosTable.Clear();
            return 0;
        }

        /// <summary>
        /// The sin cos0.
        /// </summary>
        /// <param name="radian">The radian.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        public static (double, double) SinCos0(double radian)
            // lookup, if not exists add to table and return the result.
            => sinCosTable.GetValueOrDefault(radian) ?? (sinCosTable[radian] = (Sin(radian), Cos(radian))).Value;

        /// <summary>
        /// The sin cos1.
        /// </summary>
        /// <param name="radian">The radian.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        public static (double, double) SinCos1(double radian)
            // lookup and replace with same value, or add if not exists.
            => (sinCosTable[radian] = sinCosTable.GetValueOrDefault(radian) ?? (Sin(radian), Cos(radian))).Value;

        /// <summary>
        /// The sin cos2.
        /// </summary>
        /// <param name="radian">The radian.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        public static (double, double) SinCos2(double radian)
        {
            if (!sinCosTable.ContainsKey(radian))
            {
                sinCosTable.Add(radian, (Sin(radian), Cos(radian)));
            }

            return sinCosTable[radian].Value;
        }

        /// <summary>
        /// The sin cos3.
        /// </summary>
        /// <param name="radian">The radian.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        public static (double, double) SinCos3(double radian)
        {
            if (!sinCosTable.ContainsKey(radian))
            {
                var value = (Sin(radian), Cos(radian));
                sinCosTable.Add(radian, value);
                return value;
            }

            return sinCosTable[radian].Value;
        }

        /// <summary>
        /// The sin cos4.
        /// </summary>
        /// <param name="radian">The radian.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        public static (double, double) SinCos4(double radian)
        {
            if (!sinCosTable.ContainsKey(radian))
            {
                return (sinCosTable[radian] = (Sin(radian), Cos(radian))).Value;
            }

            return sinCosTable[radian].Value;
        }
        #endregion Sine Cosine Lookup

        #region Sine Interpolation of 1D
        /// <summary>
        /// The sine.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        public static double Sine(
            double v1,
            double v2,
            double t)
        {
            var mu2 = (1 - Sin(t * PI)) / 2;
            return v1 * (1 - mu2) + v2 * mu2;
        }
        #endregion Sine Interpolation of 1D

        #region Sine Interpolation of 2D Points
        /// <summary>
        /// The sine.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        public static (double X, double Y) Sine(
            double x1, double y1,
            double x2, double y2,
            double t)
        {
            var mu2 = (1 - Sin(t * PI)) / 2;
            return (x1 * (1 - mu2) + x2 * mu2, y1 * (1 - mu2) + y2 * mu2);
        }

        /// <summary>
        /// Function For sine interpolated Line
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public static Point2D InterpolateSine(Point2D a, Point2D b, double index)
        {
            //Single MU2 = (double)((1.0 - Cos(index * 180)) * 0.5);
            //return Y1 * (1.0 - MU2) + Y2 * MU2;
            var MU2 = (1.0 - Sin(index * 180)) * 0.5;
            return (Point2D)a.Scale(1.0 - MU2).Add(b.Scale(MU2));
        }
        #endregion Sine Interpolation of 2D Points

        #region Sine Interpolation of 3D Points
        /// <summary>
        /// The sine.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        public static (double X, double Y, double Z) Sine(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            var mu2 = (1 - Sin(t * PI)) / 2;
            return (
                x1 * (1 - mu2) + x2 * mu2,
                y1 * (1 - mu2) + y2 * mu2,
                z1 * (1 - mu2) + z2 * mu2);
        }
        #endregion Sine Interpolation of 3D Points

        #region Slopes Near Collinear
        /// <summary>
        /// The slopes near collinear.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="distSqrd">The distSqrd.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool SlopesNearCollinear(Point2D a, Point2D b, Point2D c, double distSqrd)
        {
            // this function is more accurate when the point that's GEOMETRICALLY
            // between the other 2 points is the one that's tested for distance.
            // nb: with 'spikes', either pt1 or pt3 is geometrically between the other pts
            if (Abs(a.X - b.X) > Abs(a.Y - b.Y))
            {
                if ((a.X > b.X) == (a.X < c.X))
                {
                    return SquareDistanceToLine(a.X, a.Y, b.X, b.Y, c.X, c.Y) < distSqrd;
                }
                else if ((b.X > a.X) == (b.X < c.X))
                {
                    return SquareDistanceToLine(b.X, b.Y, a.X, a.Y, c.X, c.Y) < distSqrd;
                }
                else
                {
                    return SquareDistanceToLine(c.X, c.Y, a.X, a.Y, b.X, b.Y) < distSqrd;
                }
            }
            else
            {
                if ((a.Y > b.Y) == (a.Y < c.Y))
                {
                    return SquareDistanceToLine(a.X, a.Y, b.X, b.Y, c.X, c.Y) < distSqrd;
                }
                else if ((b.Y > a.Y) == (b.Y < c.Y))
                {
                    return SquareDistanceToLine(b.X, b.Y, a.X, a.Y, c.X, c.Y) < distSqrd;
                }
                else
                {
                    return SquareDistanceToLine(c.X, c.Y, a.X, a.Y, b.X, b.Y) < distSqrd;
                }
            }
        }
        #endregion Slopes Near Collinear

        #region Slopes of Lines Equal
        /// <summary>
        /// The slopes equal.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="UseFullRange">The UseFullRange.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com/delphi/clipper.php
        /// </acknowledgment>
        public static bool SlopesEqual(Point2D a, Point2D b, Point2D c, bool UseFullRange = false)
            => UseFullRange ? BigInteger.Multiply((BigInteger)(a.Y - b.Y), (BigInteger)(b.X - c.X)) == BigInteger.Multiply((BigInteger)(a.X - b.X), (BigInteger)(b.Y - c.Y))
            : (a.Y - b.Y) * (b.X - c.X) - (a.X - b.X) * (b.Y - c.Y) == 0;

        /// <summary>
        /// The slopes equal.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="UseFullRange">The UseFullRange.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com/delphi/clipper.php
        /// </acknowledgment>
        public static bool SlopesEqual(Point2D a, Point2D b, Point2D c, Point2D d, bool UseFullRange = false)
            => UseFullRange ? BigInteger.Multiply((BigInteger)(a.Y - b.Y), (BigInteger)(b.X - c.X)) == BigInteger.Multiply((BigInteger)(a.X - b.X), (BigInteger)(b.Y - c.Y))
            : (int)(a.Y - b.Y) * (c.X - d.X) - (int)(a.X - b.X) * (c.Y - d.Y) == 0;
        #endregion Slopes of Lines Equal

        #region Smooth Step
        /// <summary>
        /// Interpolates between two values using a cubic equation.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Interpolated value.</returns>
        public static double SmoothStep(double value1, double value2, double amount)
        {
            // It is expected that 0 < amount < 1
            // If amount < 0, return value1
            // If amount > 1, return value2
            var result = Clamp0(amount, 0f, 1f);
            result = Hermite(value1, 0f, value2, 0f, result);

            return result;
        }
        #endregion Smooth Step

        #region Split RGB
        /// <summary>
        /// http://xbeat.net/vbspeed/c_SplitRGB.htm
        /// by www.Abstractvb.com, Date: 3/9/2001 9:26:43 PM, 20010922
        /// </summary>
        /// <param name="lColor"></param>
        public static (int lRed, int lGreen, int lBlue) SplitRGB01(int lColor)
        {
            lColor = lColor & 0xFFFFFF;
            var lRed = lColor % 0x100;
            lColor = lColor / 0x100;
            var lGreen = lColor % 0x100;
            lColor = lColor / 0x100;
            var lBlue = lColor % 0x100;
            return (lRed, lGreen, lBlue);
        }

        /// <summary>
        /// http://xbeat.net/vbspeed/c_SplitRGB.htm
        /// by Donald, donald@xbeat.net, 20010922
        /// </summary>
        /// <param name="lColor"></param>
        public static (int lRed, int lGreen, int lBlue) SplitRGB02(int lColor)
            => (lColor & 0xFF,
            (lColor & 0xFF00) / 0x100,
            (lColor & 0xFF0000) / 0x10000);
        #endregion Split RGB

        #region Squared Distance Between Two 2D Points
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
        #endregion Squared Distance Between Two 2D Points

        #region Squared Distance To a Line
        /// <summary>
        /// Find the square of the distance of a point from a line.
        /// </summary>
        /// <param name="x1">The x component of the Point.</param>
        /// <param name="y1">The y component of the Point.</param>
        /// <param name="x2_">The x component of the first point on the line.</param>
        /// <param name="y2_">The y component of the first point on the line.</param>
        /// <param name="x3_">The x component of the second point on the line.</param>
        /// <param name="y3_">The y component of the second point on the line.</param>
        /// <returns></returns>
        public static double SquareDistanceToLine(
            double x1, double y1,
            double x2_, double y2_,
            double x3_, double y3_)
        {
            var A = y2_ - y3_;
            var B = x3_ - x2_;
            var C = A * x1 + B * y1 - (A * x2_ + B * y2_);
            return C * C / (A * A + B * B);
        }
        #endregion Squared Distance To a Line

        #region Triangulate a Polygon
        /// <summary>
        /// Set of tests to run testing methods that get the triangles of a polygon.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(TriangulateTests))]
        public static List<SpeedTester> TriangulateTests()
            => new List<SpeedTester> {
                new SpeedTester(() => Triangulate(new PolygonContour(new Point2D[] { new Point2D(0, 0), new Point2D(0, 1), new Point2D(1, 1), new Point2D(1, 0)})),
                $"{nameof(Experiments.Triangulate)}(new Polygon(new Point2D[] {{ new Point2D(0, 0), new Point2D(0, 1), new Point2D(1, 1), new Point2D(1, 0)}}))"),
           };

        /// <summary>
        /// Triangulate the polygon.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// For a nice, detailed explanation of this method,
        /// see Ian Garton's Web page:
        /// http://www-cgrl.cs.mcgill.ca/~godfried/teaching/cg-projects/97/Ian/cutting_ears.html
        /// </acknowledgment>
        public static List<Triangle> Triangulate(PolygonContour polygon)
        {
            // Copy the points into a scratch array.
            var pts = new List<Point2D>(polygon.Points);

            // Make a scratch polygon.
            var pgon = new PolygonContour(pts);

            // Orient the polygon clockwise.
            OrientPolygonClockwise(pgon);

            // Make room for the triangles.
            var triangles = new List<Triangle>();

            // While the copy of the polygon has more than
            // three points, remove an ear.
            while (pgon.Points.Count > 3)
            {
                // Remove an ear from the polygon.
                RemoveEar(pgon, triangles);
            }

            // Copy the last three points into their own triangle.
            triangles.Add(new Triangle(pgon.Points[0], pgon.Points[1], pgon.Points[2]));

            return triangles;
        }
        #endregion Triangulate a Polygon

        #region Trim Leading Zeros from Polynomials
        ///// <summary>
        ///// Simplify a polynomial, removing near zero terms.
        ///// </summary>
        ///// <param name="epsilon">The minimal difference for comparison.</param>
        ///// <returns>Returns a new instance of the <see cref="Polynomial"/> struct with the near zero terms removed.</returns>
        ///// <acknowledgment>
        ///// This is intended to be used in situations where the polynomial should be reduced. For instance in intersection calculations.
        ///// Simplifying a polynomial before GetMinMax will fail to appropriately get the min, max.
        ///// </acknowledgment>
        ///// <acknowledgment>
        ///// http://www.kevlindev.com/
        ///// </acknowledgment>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public Polynomial Trim(double epsilon = Epsilon)
        //{
        //    var coefficients = new double[Count];
        //    var degree = (int)Degree;
        //    Array.Copy(this.coefficients, coefficients, degree);
        //    for (var i = degree; i >= 0; i--)
        //    {
        //        if (Abs(coefficients[i]) <= epsilon)
        //            coefficients = coefficients.RemoveAt(i);
        //        else
        //            break;
        //    }

        //    return new Polynomial() { coefficients = coefficients, isReadonly = this.isReadonly };
        //}

        ///// <summary>
        ///// Trim off any leading zero term coefficients from the Polynomial.
        ///// </summary>
        ///// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        ///// <returns>Returns a <see cref="Polynomial"/> with any leading zero term coefficients removed.</returns>
        ///// <acknowledgment>
        ///// A hodge-podge method based on Simplify from of: http://www.kevlindev.com/
        ///// and Trim from: https://github.com/superlloyd/Poly
        ///// </acknowledgment>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public Polynomial Trim(double[] coefficients, double epsilon = Epsilon)
        //{
        //    var pos = 0;
        //    for (var i = Count - 1; i >= 0; i--)
        //    {
        //        if (Abs(this.coefficients[i]) <= epsilon)
        //            pos++;
        //        else
        //            break;
        //    }

        //    var ret = new double[Count - pos];
        //    Array.Copy(this.coefficients, 0, ret, 0, Count - pos);
        //    return new Polynomial() { coefficients = ret, isReadonly = this.isReadonly };
        //}

        ///// <summary>
        ///// Trim off empty coefficients.
        ///// </summary>
        ///// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        ///// <returns></returns>
        ///// <acknowledgment>
        ///// https://github.com/superlloyd/Poly
        ///// </acknowledgment>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public Polynomial Trim(double epsilon = Epsilon)
        //{
        //    var order = 0;
        //    for (var i = 0; i < coefficients.Length; i++)
        //    {
        //        if (Abs(coefficients[i]) > epsilon)
        //        {
        //            order = i;
        //        }
        //    }

        //    var res = new double[order + 1];
        //    for (var i = 0; i < res.Length; i++)
        //    {
        //        if (Abs(coefficients[i]) > epsilon)
        //        {
        //            res[i] = coefficients[i];
        //        }
        //    }

        //    return new Polynomial() { coefficients = res, isReadonly = this.isReadonly };
        //}
        #endregion Trim Leading Zeros from Polynomials

        #region Values Are Close
        /// <summary>
        /// Set of tests to run testing methods that determine whether values are close.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(ValuesAreCloseTests))]
        public static List<SpeedTester> ValuesAreCloseTests()
            => new List<SpeedTester> {
                new SpeedTester(() => AreClose(0d, double.Epsilon),
                $"{nameof(Experiments.AreClose)}({0d}, {double.Epsilon})"),
           };

        /// <summary>
        /// AreClose - Returns whether or not two doubles are "close".  That is, whether or
        /// not they are within epsilon of each other.  Note that this epsilon is proportional
        /// to the numbers themselves to that AreClose survives scalar multiplication.
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        public static bool AreClose(double value1, double value2, double epsilon = DoubleEpsilon)
        {
            // in case they are Infinities (then epsilon check does not work)
            if (Abs(value1 - value2) < DoubleEpsilon)
            {
                return true;
            }
            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            var eps = (Abs(value1) + Abs(value2) + 10d) * epsilon;
            var delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }
        #endregion Values Are Close

        #region Vector Between Vectors
        /// <summary>
        /// Set of tests to run testing methods that calculate whether a vector is between two others.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(VectorBetweenTests))]
        public static List<SpeedTester> VectorBetweenTests()
        {
            var a = 45d.ToRadians();
            var b = 90d.ToRadians();
            var c = 0d.ToRadians();
            var i = Sin(a);
            var j = Cos(a);
            var i2 = Sin(b);
            var j2 = Cos(b);
            var i3 = Sin(c);
            var j3 = Cos(c);

            return new List<SpeedTester> {
                new SpeedTester(() => VectorBetween0(i, j, i2, j2, i3, j3),
                $"{nameof(Experiments.VectorBetween0)}({i}, {j}, {i2}, {j2}, {i3}, {j3})"),
                new SpeedTester(() => VectorBetween1(i, j, i2, j2, i3, j3),
                $"{nameof(Experiments.VectorBetween1)}({i}, {j}, {i2}, {j2}, {i3}, {j3})"),
           };
        }

        /// <summary>
        /// The vector between0.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="i2">The i2.</param>
        /// <param name="j2">The j2.</param>
        /// <param name="i3">The i3.</param>
        /// <param name="j3">The j3.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://math.stackexchange.com/questions/1698835/find-if-a-vector-is-between-2-vectors
        /// http://stackoverflow.com/questions/13640931/how-to-determine-if-a-vector-is-between-two-other-vectors
        /// http://gamedev.stackexchange.com/questions/22392/what-is-a-good-way-to-determine-if-a-vector-is-between-two-other-vectors-in-2d
        /// http://math.stackexchange.com/questions/169998/figure-out-if-a-fourth-point-resides-within-an-angle-created-by-three-other-poin
        /// </acknowledgment>
        public static bool VectorBetween0(double i, double j, double i2, double j2, double i3, double j3)
            => CrossProduct(i2, j2, i, j) * CrossProduct(i2, j2, i3, j3) >= 0
            && CrossProduct(i3, j3, i, j) * CrossProduct(i3, j3, i2, j2) >= 0;

        /// <summary>
        /// The vector between1.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="i2">The i2.</param>
        /// <param name="j2">The j2.</param>
        /// <param name="i3">The i3.</param>
        /// <param name="j3">The j3.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://math.stackexchange.com/questions/1698835/find-if-a-vector-is-between-2-vectors
        /// http://stackoverflow.com/questions/13640931/how-to-determine-if-a-vector-is-between-two-other-vectors
        /// http://gamedev.stackexchange.com/questions/22392/what-is-a-good-way-to-determine-if-a-vector-is-between-two-other-vectors-in-2d
        /// http://math.stackexchange.com/questions/169998/figure-out-if-a-fourth-point-resides-within-an-angle-created-by-three-other-poin
        /// </acknowledgment>
        public static bool VectorBetween1(double i, double j, double i2, double j2, double i3, double j3)
            => ((i2 * j) - (j2 * i)) * ((i2 * j3) - (j2 * i3)) >= 0
            && ((i3 * j) - (j3 * i)) * ((i3 * j2) - (j3 * i2)) >= 0;
        #endregion Vector Between Vectors

        #region Wrap Point On Rectangle Bounds
        /// <summary>
        /// Set of tests to run testing methods that calculate the wrapped position of a point in a rectangle.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(WrapPointToRectangleTests))]
        public static List<SpeedTester> WrapPointToRectangleTests()
        {
            var reff = new Point2D();
            return new List<SpeedTester> {
                new SpeedTester(() => WrapPointToRectangle(new Rectangle2D(0, 0, 20, 20),new Point2D(31, 21), ref reff),
                $"{nameof(Experiments.WrapPointToRectangle)}(new Rectangle2D(0, 0, 20, 20),new Point2D(31, 21), ref reff)"),
           };
        }

        /// <summary>
        /// The wrap point to rectangle.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        /// <param name="point">The point.</param>
        /// <param name="reference">The reference.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public static Point2D WrapPointToRectangle(Rectangle2D bounds, Point2D point, ref Point2D reference)
        {
            if (point.X <= bounds.X)
            {
                reference = reference - new Size2D(bounds.X, 0);
                return new Point2D(bounds.Width - 2, point.Y);
            }
            if (point.Y <= bounds.Y)
            {
                reference = reference - new Size2D(0, bounds.Y);
                return new Point2D(point.X, bounds.Height - 2);
            }
            if (point.X >= (bounds.Width - 1))
            {
                reference = reference + new Size2D(bounds.Width, 0);
                return new Point2D(bounds.X + 2, point.Y);
            }
            if (point.Y >= (bounds.Height - 1))
            {
                reference = reference + new Size2D(0, bounds.Height);
                return new Point2D(point.X, bounds.Y + 2);
            }
            return point;
            // 'ToDo: Adjust My_StartPoint when Screen is wrapped
        }
        #endregion Wrap Point On Rectangle Bounds

        /// <summary>
        /// The draw rect at ellipse.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="theta">The theta.</param>
        /// <param name="ellipse">The ellipse.</param>
        /// <param name="phi">The phi.</param>
        /// <param name="rect">The rect.</param>
        public static void Draw_rect_at_ellipse(Graphics g, double theta, Rectangle2D ellipse, double phi, Rectangle2D rect)
        {
            var xaxis = new Point2D(Cos(theta), Sin(theta));
            var yaxis = new Point2D(-Sin(theta), Cos(theta));
            Point2D ellipse_point;

            // Ellipse equation for an ellipse at origin.
            ellipse_point = new Point2D(ellipse.Width * Cos(phi), ellipse.Height * Sin(phi));

            // Apply the rotation transformation and translate to new center.
            rect.Location = new Point2D(ellipse.Left + (ellipse_point.X * xaxis.X + ellipse_point.Y * xaxis.Y),
                                       ellipse.Top + (ellipse_point.X * yaxis.X + ellipse_point.Y * yaxis.Y));

            g.DrawRectangle(Pens.AntiqueWhite, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }

        /// <summary>
        /// Bow Curve (2D)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="DPen"></param>
        /// <param name="Precision"></param>
        /// <param name="Offset"></param>
        /// <param name="Multiplyer"></param>
        /// <acknowledgment>
        ///  Also known as the "cocked hat", it was first documented by Sylvester around
        ///  1864 and Cayley in 1867.
        /// </acknowledgment>
        public static void DrawBowCurve2D(Graphics g, Pen DPen, double Precision, Size2D Offset, Size2D Multiplyer)
        {
            var NewPoint = new Point2D(
                (1 - (Tan(PI * -1) * 2)) * Cos(PI * -1) * Multiplyer.Width,
                (1 - (Tan(PI * -1) * 2)) * (2 * Sin(PI * -1)) * Multiplyer.Height
                );

            var LastPoint = NewPoint;

            for (var Index = PI * -1; Index <= PI; Index += Precision)
            {
                LastPoint = NewPoint;
                NewPoint = new Point2D(
                    (1 - (Tan(Index) * 2)) * Cos(Index) * Multiplyer.Width,
                    (1 - (Tan(Index) * 2)) * (2 * Sin(Index)) * Multiplyer.Height
                    );

                g.DrawLine(DPen, NewPoint.ToPointF(), LastPoint.ToPointF());
            }
        }

        /// <summary>
        /// Butterfly Curve
        /// </summary>
        /// <param name="g"></param>
        /// <param name="DPen"></param>
        /// <param name="Precision"></param>
        /// <param name="Offset"></param>
        /// <param name="Multiplyer"></param>
        public static void DrawButterflyCurve2D(Graphics g, Pen DPen, double Precision, SizeF Offset, SizeF Multiplyer)
        {
            const double N = 10000;
            var U = 0 * (24 * (PI / N));

            var NewPoint = new Point2D(
                Cos(U) * ((Exp(Cos(U)) - ((2 * Cos(4 * U)) - Pow(Sin(U / 12), 5))) * Multiplyer.Width),
                Sin(U) * (Exp(Cos(U)) - ((2 * Cos(4 * U)) - Pow(Sin(U / 12), 5))) * Multiplyer.Height
                );

            var LastPoint = NewPoint;

            for (double Index = 1; Index <= N; Index = Index + Precision)
            {
                LastPoint = NewPoint;
                U = Index * (24 * (PI / N));

                NewPoint = new Point2D(
                    Cos(U) * ((Exp(Cos(U)) - ((2 * Cos(4 * U)) - Pow(Sin(U / 12), 5))) * Multiplyer.Width),
                    Sin(U) * (Exp(Cos(U)) - ((2 * Cos(4 * U)) - Pow(Sin(U / 12), 5))) * Multiplyer.Height
                    );

                g.DrawLine(DPen, NewPoint.ToPointF(), LastPoint.ToPointF());
            }
        }
    }
}
