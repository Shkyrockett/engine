// <copyright file="Maths.Splitings.cs" >
//    Copyright © 2017 Shkyrockett. All rights reserved.
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
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Splitings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Split(this Point2D point, double t)
            => point;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Split(this Point2D point, params double[] ts)
            => point;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Split(this Point2D point, IEnumerable<double> ts)
            => point;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray[] Split(this Line line, double t)
            => new Ray[] { new Ray(line.Interpolate(t), -line.Direction), new Ray(line.Interpolate(t), line.Direction) };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Shape[] Split(this Line line, params double[] ts)
        {
            if (ts == null)
            {
                return new[] { line };
            }

            var filtered = ts.Distinct().OrderBy(t => t).ToArray();
            if (filtered.Length == 0)
            {
                return new[] { line };
            }

            var n = filtered.Length;
            var shapes = new Shape[n + 1];
            var prev = line.Interpolate(filtered[0]);
            shapes[0] = new Ray(prev, -line.Direction);

            for (var i = 1; i < n; i++)
            {
                var next = line.Interpolate(filtered[i]);
                shapes[i] = new LineSegment(prev, next);
                prev = next;
            }

            shapes[shapes.Length - 1] = new Ray(prev, line.Direction);
            return shapes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Shape[] Split(this Line line, IEnumerable<double> ts)
            => Split(line, ts.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Shape[] Split(this Ray ray, double t)
            => Split(ray, new double[] { t });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Shape[] Split(this Ray ray, params double[] ts)
        {
            if (ts == null)
            {
                return new[] { ray };
            }

            var filtered = ts.Where(t => t >= 0).Distinct().OrderBy(t => t).ToArray();
            if (filtered.Length == 0)
            {
                return new[] { ray };
            }

            var n = filtered.Length;
            var shapes = new Shape[n + 1];
            var prev = ray.Location;
            for (var i = 0; i < n; i++)
            {
                var next = ray.Interpolate(filtered[i]);
                shapes[i] = new LineSegment(prev, next);
                prev = next;
            }

            shapes[shapes.Length - 1] = new Ray(prev, ray.Direction);
            return shapes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Shape[] Split(this Ray ray, IEnumerable<double> ts)
            => Split(ray, ts.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment[] Split(this LineSegment segment, double t)
        {
            if (t < 0 || t > 1) throw new ArgumentOutOfRangeException();
            return new[] {
                new LineSegment(segment.A, segment.Interpolate(t)),
                new LineSegment(segment.Interpolate(t), segment.B)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment[] Split(this LineSegment segment, params double[] ts)
        {
            if (ts == null)
            {
                return new[] { segment };
            }

            var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToArray();
            if (filtered.Length == 0)
            {
                return new[] { segment };
            }

            var start = segment;
            var list = new LineSegment[filtered.Length + 1];
            var previous = segment.A;
            for (var i = 0; i < filtered.Length; i++)
            {
                var next = segment.Interpolate(filtered[i]);
                list[i] = new LineSegment(previous, next);
                previous = next;
            }

            list[list.Length - 1] = new LineSegment(previous, segment.B);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LineSegment[] Split(this LineSegment segment, IEnumerable<double> ts)
            => Split(segment, ts.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArc Split(this Circle circle, double t)
        {
            if (t < 0 || t > 1) throw new ArgumentOutOfRangeException();
            return new CircularArc(circle.Center, circle.Radius, Tau * t, Tau);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArc[] Split(this Circle circle, params double[] ts)
        {
            if (ts == null)
                return new[] { Split(circle, 0) };
            var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
                return new[] { Split(circle, 0) };

            var arc = Split(circle, filtered[0]);
            var tLast = 0d;
            var start = arc;
            var list = new List<CircularArc>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = 1 - (1 - t) / (1 - tLast);
                tLast = t;
                var cut = Split(arc, relT);
                list.Add(cut[0]);
                start = cut[1];
            }

            list.Add(start);
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArc[] Split(this Circle circle, IEnumerable<double> ts)
            => Split(circle, ts.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArc[] Split(this CircularArc arc, double t)
        {
            if (t < 0 || t > 1) throw new ArgumentOutOfRangeException();
            return new[] {
                new CircularArc(arc.Center, arc.Radius, arc.StartAngle, (arc.SweepAngle * t)),
                new CircularArc(arc.Center, arc.Radius, (arc.StartAngle + (arc.SweepAngle * t)), (arc.SweepAngle - (arc.SweepAngle * t)))
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArc[] Split(this CircularArc arc, params double[] ts)
        {
            if (ts == null)
                return new[] { arc };
            var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
                return new[] { arc };

            var tLast = 0d;
            var start = arc;
            var list = new List<CircularArc>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = 1 - (1 - t) / (1 - tLast);
                tLast = t;
                var cut = Split(arc, relT);
                list.Add(cut[0]);
                start = cut[1];
            }

            list.Add(start);
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArc[] Split(this CircularArc arc, IEnumerable<double> ts)
            => Split(arc, ts.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EllipticalArc Split(this Ellipse ellipse, double t)
        {
            if (t < 0 || t > 1) throw new ArgumentOutOfRangeException();
            return new EllipticalArc(ellipse.Center, ellipse.RX, ellipse.RY, ellipse.Angle, Tau * t, Tau);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EllipticalArc[] Split(this Ellipse ellipse, params double[] ts)
        {
            if (ts == null)
                return new[] { Split(ellipse, 0) };
            var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
                return new[] { Split(ellipse, 0) };

            var arc = Split(ellipse, filtered[0]);
            var tLast = 0d;
            var start = arc;
            var list = new List<EllipticalArc>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = 1 - (1 - t) / (1 - tLast);
                tLast = t;
                var cut = Split(arc, relT);
                list.Add(cut[0]);
                start = cut[1];
            }

            list.Add(start);
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EllipticalArc[] Split(this Ellipse ellipse, IEnumerable<double> ts)
            => Split(ellipse, ts.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EllipticalArc[] Split(this EllipticalArc arc, double t)
        {
            if (t < 0 || t > 1) throw new ArgumentOutOfRangeException();
            return new[] {
                new EllipticalArc(arc.Center, arc.RX, arc.RY, arc.Angle, arc.StartAngle, (arc.SweepAngle * t)),
                new EllipticalArc(arc.Center, arc.RX, arc.RY, arc.Angle, (arc.StartAngle + (arc.SweepAngle * t)), (arc.SweepAngle - (arc.SweepAngle * t)))
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EllipticalArc[] Split(this EllipticalArc arc, params double[] ts)
        {
            if (ts == null)
                return new[] { arc };
            var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
                return new[] { arc };

            var tLast = 0d;
            var start = arc;
            var list = new List<EllipticalArc>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = 1 - (1 - t) / (1 - tLast);
                tLast = t;
                var cut = Split(arc, relT);
                list.Add(cut[0]);
                start = cut[1];
            }

            list.Add(start);
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EllipticalArc[] Split(this EllipticalArc arc, IEnumerable<double> ts)
            => Split(arc, ts.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierSegment[] Split(this BezierSegment bezier, double t)
            => SplitBezier(bezier.Points, t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierSegment[] Split(this BezierSegment bezier, params double[] ts)
        {
            if (ts == null)
                return new[] { bezier };
            var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
                return new[] { bezier };

            var tLast = 0d;
            var start = bezier;
            var list = new List<BezierSegment>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = 1 - (1 - t) / (1 - tLast);
                tLast = t;
                var cut = SplitBezier(bezier.Points, relT);
                list.Add(cut[0]);
                start = cut[1];
            }
            list.Add(start);
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/#decasteljau
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierSegment[] Split(this BezierSegment bezier, IEnumerable<double> ts)
            => Split(bezier, ts.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/#decasteljau
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierSegment[] Split(this QuadraticBezier bezier, double t)
            => SplitBezier(bezier.Points, t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/#decasteljau
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierSegment[] Split(this QuadraticBezier bezier, params double[] ts)
        {
            if (ts == null)
            {
                return new[] { new BezierSegment(bezier.Points) };
            }

            var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
            {
                return new[] { new BezierSegment(bezier.Points) };
            }

            var tLast = 0d;
            var start = new BezierSegment(bezier.Points);
            var list = new List<BezierSegment>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = (1 - t) / (1 - tLast);
                tLast = t;
                var cut = SplitBezier(start.Points, relT);
                list.Add(cut[1]);
                start = cut[0];
            }

            list.Add(start);
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/#decasteljau
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierSegment[] Split(this QuadraticBezier bezier, IEnumerable<double> ts)
            => Split(bezier, ts.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/#decasteljau
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierSegment[] Split(this CubicBezier bezier, double t)
            => SplitBezier(bezier.Points, t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/#decasteljau
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierSegment[] Split(this CubicBezier bezier, params double[] ts)
        {
            if (ts == null)
            {
                return new[] { new BezierSegment(bezier.Points) };
            }

            var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToList();

            if (filtered.Count == 0)
            {
                return new[] { new BezierSegment(bezier.Points) };
            }

            var tLast = 0d;
            var prev = new BezierSegment(bezier.Points);
            var list = new List<BezierSegment>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = (1 - t) / (1 - tLast);
                tLast = t;
                var cut = SplitBezier(prev.Points, relT);
                list.Add(cut[1]);
                prev = cut[0];
            }

            list.Add(prev);
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierSegment[] Split(this CubicBezier bezier, IEnumerable<double> ts)
            => Split(bezier, ts.ToArray());

        /// <summary>
        /// Cut a <see cref="BezierSegment"/> into multiple fragments at the given t indices, using "De Casteljau" algorithm.
        /// The value at which to split the curve. Should be strictly inside ]0,1[ interval.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="t"></param>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/#decasteljau
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BezierSegment[] SplitBezier(IEnumerable<Point2D> points, double t)
        {
            if (t < 0 || t > 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            var bezier1 = new List<Point2D>();
            var bezier2 = new List<Point2D>();
            var lp = points.ToList();

            while (lp.Count > 0)
            {
                bezier1.Add(lp.First());
                bezier2.Add(lp.Last());
                var next = new List<Point2D>(lp.Count - 1);
                for (var i = 0; i < lp.Count - 1; i++)
                {
                    var p0 = lp[i];
                    var p1 = lp[i + 1];
                    next.Add(new Point2D((p0.X * (1 - t) + t * p1.X, p0.Y * (1 - t) + t * p1.Y)));
                }

                lp = next;
            }

            return new[] {
                new BezierSegment(bezier1.ToArray()),
                new BezierSegment(bezier2.ToArray())
            };
        }
    }
}
