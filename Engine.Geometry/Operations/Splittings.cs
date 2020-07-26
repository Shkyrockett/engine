// <copyright file="Splittings.cs" >
//    Copyright © 2017 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Mathematics;

namespace Engine
{
    /// <summary>
    /// The splittings class.
    /// </summary>
    public static class Splittings
    {
        #region Split Points
        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Split(this Point2D point, double t)
        {
            _ = t;
            return point;
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Split(this Point2D point, params double[] ts)
        {
            _ = ts;
            return point;
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Split(this Point2D point, IEnumerable<double> ts)
        {
            _ = ts;
            return point;
        }
        #endregion

        #region Split Lines
        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<Ray2D> Split(this Line2D line, double t) => new Ray2D[] { new Ray2D(line.Interpolate(t), -line.Direction), new Ray2D(line.Interpolate(t), line.Direction) };

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>
        /// The <see cref="Array" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<IShapeSegment> Split(this Line2D line, params double[] ts)
        {
            if (ts is null)
            {
                return new IShapeSegment[] { line };
            }

            var filtered = ts.Distinct().OrderBy(t => t).ToArray();
            if (filtered.Length == 0)
            {
                return new IShapeSegment[] { line };
            }

            var n = filtered.Length;
            var shapes = new IShapeSegment[n + 1];
            var prev = line.Interpolate(filtered[0]);
            shapes[0] = new Ray2D(prev, -line.Direction);

            for (var i = 1; i < n; i++)
            {
                var next = line.Interpolate(filtered[i]);
                shapes[i] = new LineSegment2D(prev, next);
                prev = next;
            }

            shapes[^1] = new Ray2D(prev, line.Direction);
            return shapes;
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<IShapeSegment> Split(this Line2D line, IEnumerable<double> ts) => Split(line, ts.ToArray());
        #endregion

        #region Split Rays
        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="ray">The ray.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<IShapeSegment> Split(this Ray2D ray, double t) => Split(ray, new double[] { t });

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="ray">The ray.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<IShapeSegment> Split(this Ray2D ray, params double[] ts)
        {
            if (ts is null)
            {
                return new IShapeSegment[] { ray };
            }

            var filtered = ts.Where(t => t >= 0).Distinct().OrderBy(t => t).ToArray();
            if (filtered.Length == 0)
            {
                return new IShapeSegment[] { ray };
            }

            var n = filtered.Length;
            var shapes = new IShapeSegment[n + 1];
            var prev = ray.Location;
            for (var i = 0; i < n; i++)
            {
                var next = ray.Interpolate(filtered[i]);
                shapes[i] = new LineSegment2D(prev, next);
                prev = next;
            }

            shapes[^1] = new Ray2D(prev, ray.Direction);
            return shapes;
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="ray">The ray.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<IShapeSegment> Split(this Ray2D ray, IEnumerable<double> ts) => Split(ray, ts.ToArray());
        #endregion

        #region Split Line Segments
        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<LineSegment2D> Split(this LineSegment2D segment, double t)
        {
            if (t < 0d || t > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(t));
            }

            return new[] {
                new LineSegment2D(segment.A, segment.Interpolate(t)),
                new LineSegment2D(segment.Interpolate(t), segment.B)
            };
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<LineSegment2D> Split(this LineSegment2D segment, params double[] ts)
        {
            if (ts is null)
            {
                return new[] { segment };
            }

            var filtered = ts.Where(t => t >= 0d && t <= 1d).Distinct().OrderBy(t => t).ToArray();
            if (filtered.Length == 0)
            {
                return new[] { segment };
            }

            var start = segment;
            var list = new LineSegment2D[filtered.Length + 1];
            var previous = segment.A;
            for (var i = 0; i < filtered.Length; i++)
            {
                var next = segment.Interpolate(filtered[i]);
                list[i] = new LineSegment2D(previous, next);
                previous = next;
            }

            list[^1] = new LineSegment2D(previous, segment.B);
            return list;
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<LineSegment2D> Split(this LineSegment2D segment, IEnumerable<double> ts) => Split(segment, ts.ToArray());
        #endregion

        #region Split Circles
        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="CircularArc2D"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArc2D Split(this Circle2D circle, double t)
        {
            if (t < 0d || t > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(t));
            }

            return new CircularArc2D(circle.Center, circle.Radius, Tau * t, Tau);
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<CircularArc2D> Split(this Circle2D circle, params double[] ts)
        {
            if (ts is null)
            {
                return new[] { Split(circle, 0d) };
            }

            var filtered = ts.Where(t => t >= 0d && t <= 1d).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
            {
                return new[] { Split(circle, 0d) };
            }

            var arc = Split(circle, filtered[0]);
            var tLast = 0d;
            var start = arc;
            var list = new List<CircularArc2D>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = 1d - ((1d - t) / (1d - tLast));
                tLast = t;
                var cut = Split(arc, relT);
                list.Add(cut[0]);
                start = cut[1];
            }

            list.Add(start);
            return list.ToArray();
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<CircularArc2D> Split(this Circle2D circle, IEnumerable<double> ts) => Split(circle, ts.ToArray());
        #endregion

        #region Split Circular Arcs
        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="arc">The arc.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<CircularArc2D> Split(this CircularArc2D arc, double t)
        {
            if (t < 0d || t > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(t));
            }

            return new[] {
                new CircularArc2D(arc.Center, arc.Radius, arc.StartAngle, arc.SweepAngle * t),
                new CircularArc2D(arc.Center, arc.Radius, arc.StartAngle + (arc.SweepAngle * t), arc.SweepAngle - (arc.SweepAngle * t))
            };
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="arc">The arc.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<CircularArc2D> Split(this CircularArc2D arc, params double[] ts)
        {
            if (ts is null)
            {
                return new[] { arc };
            }

            var filtered = ts.Where(t => t >= 0d && t <= 1d).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
            {
                return new[] { arc };
            }

            var tLast = 0d;
            var start = arc;
            var list = new List<CircularArc2D>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = 1d - ((1d - t) / (1d - tLast));
                tLast = t;
                var cut = Split(arc, relT);
                list.Add(cut[0]);
                start = cut[1];
            }

            list.Add(start);
            return list.ToArray();
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="arc">The arc.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<CircularArc2D> Split(this CircularArc2D arc, IEnumerable<double> ts) => Split(arc, ts.ToArray());
        #endregion

        #region Split Ellipse
        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="ellipse">The ellipse.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="EllipticalArc2D"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EllipticalArc2D Split(this Ellipse2D ellipse, double t)
        {
            if (t < 0d || t > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(t));
            }

            return new EllipticalArc2D(ellipse.Center, ellipse.RadiusA, ellipse.RadiusB, ellipse.Angle, Tau * t, Tau);
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="ellipse">The ellipse.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<EllipticalArc2D> Split(this Ellipse2D ellipse, params double[] ts)
        {
            if (ts is null)
            {
                return new[] { Split(ellipse, 0d) };
            }

            var filtered = ts.Where(t => t >= 0d && t <= 1d).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
            {
                return new[] { Split(ellipse, 0d) };
            }

            var arc = Split(ellipse, filtered[0]);
            var tLast = 0d;
            var start = arc;
            var list = new List<EllipticalArc2D>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = 1d - ((1d - t) / (1d - tLast));
                tLast = t;
                var cut = Split(arc, relT);
                list.Add(cut[0]);
                start = cut[1];
            }

            list.Add(start);
            return list.ToArray();
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="ellipse">The ellipse.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<EllipticalArc2D> Split(this Ellipse2D ellipse, IEnumerable<double> ts) => Split(ellipse, ts.ToArray());
        #endregion

        #region Split Elliptical Arc
        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="arc">The arc.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<EllipticalArc2D> Split(this EllipticalArc2D arc, double t)
        {
            if (t < 0d || t > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(t));
            }

            return new[] {
                new EllipticalArc2D(arc.Center, arc.RadiusA, arc.RadiusB, arc.Angle, arc.StartAngle, arc.SweepAngle * t),
                new EllipticalArc2D(arc.Center, arc.RadiusA, arc.RadiusB, arc.Angle, arc.StartAngle + (arc.SweepAngle * t), arc.SweepAngle - (arc.SweepAngle * t))
            };
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="arc">The arc.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<EllipticalArc2D> Split(this EllipticalArc2D arc, params double[] ts)
        {
            if (ts is null)
            {
                return new[] { arc };
            }

            var filtered = ts.Where(t => t >= 0d && t <= 1d).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
            {
                return new[] { arc };
            }

            var tLast = 0d;
            var start = arc;
            var list = new List<EllipticalArc2D>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = 1d - ((1d - t) / (1d - tLast));
                tLast = t;
                var cut = Split(arc, relT);
                list.Add(cut[0]);
                start = cut[1];
            }

            list.Add(start);
            return list.ToArray();
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="arc">The arc.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<EllipticalArc2D> Split(this EllipticalArc2D arc, IEnumerable<double> ts) => Split(arc, ts.ToArray());
        #endregion

        #region Split Quadratic Bezier Curves
        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="bezier">The Bézier.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<BezierSegment2D> Split(this QuadraticBezier2D bezier, double t) => SplitBezier(bezier?.Points, t);

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="bezier">The Bézier.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<BezierSegment2D> Split(this QuadraticBezier2D bezier, params double[] ts)
        {
            if (ts is null)
            {
                return new[] { new BezierSegment2D(bezier?.Points) };
            }

            var filtered = ts.Where(t => t >= 0d && t <= 1d).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
            {
                return new[] { new BezierSegment2D(bezier?.Points) };
            }

            var tLast = 0d;
            var start = new BezierSegment2D(bezier?.Points);
            var list = new List<BezierSegment2D>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = (1d - t) / (1d - tLast);
                tLast = t;
                var cut = SplitBezier(start.Points, relT);
                list.Add(cut[1]);
                start = cut[0];
            }

            list.Add(start);
            return list.ToArray();
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="bezier">The Bézier.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<BezierSegment2D> Split(this QuadraticBezier2D bezier, IEnumerable<double> ts) => Split(bezier, ts.ToArray());
        #endregion

        #region Split Cubic Bezier Curves
        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="bezier">The Bézier.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<BezierSegment2D> Split(this CubicBezier2D bezier, double t) => SplitBezier(bezier.Points, t);

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="bezier">The Bézier.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<BezierSegment2D> Split(this CubicBezier2D bezier, params double[] ts)
        {
            if (ts is null)
            {
                return new[] { new BezierSegment2D(bezier.Points) };
            }

            var filtered = ts.Where(t => t >= 0d && t <= 1d).Distinct().OrderBy(t => t).ToList();

            if (filtered.Count == 0)
            {
                return new[] { new BezierSegment2D(bezier.Points) };
            }

            var tLast = 0d;
            var prev = new BezierSegment2D(bezier.Points);
            var list = new List<BezierSegment2D>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = (1d - t) / (1d - tLast);
                tLast = t;
                var cut = SplitBezier(prev.Points, relT);
                list.Add(cut[1]);
                prev = cut[0];
            }

            list.Add(prev);
            return list.ToArray();
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="bezier">The Bézier.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<BezierSegment2D> Split(this CubicBezier2D bezier, IEnumerable<double> ts) => Split(bezier, ts.ToArray());
        #endregion

        #region Split General Bezier Curves
        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="bezier">The Bézier.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<BezierSegment2D> Split(this BezierSegment2D bezier, double t) => SplitBezier(bezier.Points, t);

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="bezier">The Bézier.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<BezierSegment2D> Split(this BezierSegment2D bezier, params double[] ts)
        {
            if (ts is null)
            {
                return new[] { bezier };
            }

            var filtered = ts.Where(t => t >= 0d && t <= 1d).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
            {
                return new[] { bezier };
            }

            var tLast = 0d;
            var start = bezier;
            var list = new List<BezierSegment2D>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = 1d - ((1d - t) / (1d - tLast));
                tLast = t;
                var cut = SplitBezier(bezier.Points, relT);
                list.Add(cut[0]);
                start = cut[1];
            }
            list.Add(start);
            return list.ToArray();
        }

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="bezier">The Bézier.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<BezierSegment2D> Split(this BezierSegment2D bezier, IEnumerable<double> ts) => Split(bezier, ts.ToArray());

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="points">The Bézier.</param>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<BezierSegment2D> SplitBezier(IEnumerable<Point2D> points, params double[] ts)
        {
            if (ts is null)
            {
                return new[] { new BezierSegment2D(points) };
            }

            var filtered = ts.Where(t => t >= 0d && t <= 1d).Distinct().OrderBy(t => t).ToList();

            if (filtered.Count == 0)
            {
                return new[] { new BezierSegment2D(points) };
            }

            var tLast = 0d;
            var prev = new BezierSegment2D(points);
            var list = new List<BezierSegment2D>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = (1d - t) / (1d - tLast);
                tLast = t;
                var cut = SplitBezier(prev.Points, relT);
                list.Add(cut[1]);
                prev = cut[0];
            }

            list.Add(prev);
            return list.ToArray();
        }

        /// <summary>
        /// Cut a <see cref="BezierSegment2D"/> into multiple fragments at the given t indices, using "De Casteljau" algorithm.
        /// The value at which to split the curve. Should be strictly inside ]0,1[ interval.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/#decasteljau
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<BezierSegment2D> SplitBezier(IEnumerable<Point2D> points, double t)
        {
            if (t < 0d || t > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(t));
            }

            var bezier2 = new List<Point2D>();
            var bezier1 = new List<Point2D>();
            var lp = points.ToList();

            while (lp.Count > 0)
            {
                bezier2.Insert(0, lp.Last());
                bezier1.Add(lp.First());
                var next = new List<Point2D>(lp.Count - 1);
                for (var i = 0; i < lp.Count - 1; i++)
                {
                    var p0 = lp[i];
                    var p1 = lp[i + 1];
                    next.Add(new Point2D(((p0.X * (1d - t)) + (t * p1.X), (p0.Y * (1d - t)) + (t * p1.Y))));
                }

                lp = next;
            }

            return new[] {
                new BezierSegment2D(bezier1.ToArray()),
                new BezierSegment2D(bezier2.ToArray())
            };
        }
        #endregion
    }
}
