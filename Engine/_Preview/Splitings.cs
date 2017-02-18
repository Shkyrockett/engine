using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static Point2D Split(this Point2D point, double t)
            => point;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static Point2D Split(this Point2D point, params double[] ts)
            => point;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static Point2D Split(this Point2D point, IEnumerable<double> ts)
            => point;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="t"></param>
        /// <returns></returns>
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
        public static LineSegment[] Split(this LineSegment segment, params double[] ts)
            => Split(segment, (IEnumerable<double>)ts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static LineSegment[] Split(this LineSegment segment, IEnumerable<double> ts)
        {
            {
                if (ts == null)
                    return new[] { segment };
                var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToList();
                if (filtered.Count == 0)
                    return new[] { segment };

                var tLast = 0d;
                var start = segment;
                var list = new List<LineSegment>(filtered.Count + 1);
                foreach (var t in filtered)
                {
                    var relT = 1 - (1 - t) / (1 - tLast);
                    tLast = t;
                    var cut = Split(segment, relT);
                    list.Add(cut[0]);
                    start = cut[1];
                }

                list.Add(start);
                return list.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="t"></param>
        /// <returns></returns>
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
        public static CircularArc[] Split(this Circle circle, params double[] ts)
            => Split(circle, (IEnumerable<double>)ts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static CircularArc[] Split(this Circle circle, IEnumerable<double> ts)
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
        /// <param name="arc"></param>
        /// <param name="t"></param>
        /// <returns></returns>
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
        public static CircularArc[] Split(this CircularArc arc, params double[] ts)
            => Split(arc, (IEnumerable<double>)ts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static CircularArc[] Split(this CircularArc arc, IEnumerable<double> ts)
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
        /// <param name="ellipse"></param>
        /// <param name="t"></param>
        /// <returns></returns>
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
        public static EllipticalArc[] Split(this Ellipse ellipse, params double[] ts)
            => Split(ellipse, (IEnumerable<double>)ts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static EllipticalArc[] Split(this Ellipse ellipse, IEnumerable<double> ts)
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
        /// <param name="arc"></param>
        /// <param name="t"></param>
        /// <returns></returns>
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
        public static EllipticalArc[] Split(this EllipticalArc arc, params double[] ts)
            => Split(arc, (IEnumerable<double>)ts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arc"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static EllipticalArc[] Split(this EllipticalArc arc, IEnumerable<double> ts)
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
        /// <param name="bezier"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static BezierSegment[] Split(this BezierSegment bezier, double t)
            => SplitBezier(bezier.Points, t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static BezierSegment[] Split(this BezierSegment bezier, params double[] ts)
            => Split(bezier, (IEnumerable<double>)ts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static BezierSegment[] Split(this BezierSegment bezier, IEnumerable<double> ts)
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
        /// <param name="t"></param>
        /// <returns></returns>
        public static BezierSegment[] Split(this QuadraticBezier bezier, double t)
            => SplitBezier(bezier.Points, t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static BezierSegment[] Split(this QuadraticBezier bezier, IEnumerable<double> ts)
        {
            if (ts == null)
                return new[] { new BezierSegment(bezier.Points) };
            var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
                return new[] { new BezierSegment(bezier.Points) };

            var tLast = 0d;
            var start = new BezierSegment(bezier.Points);
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
        /// <param name="t"></param>
        /// <returns></returns>
        public static BezierSegment[] Split(this CubicBezier bezier, double t)
            => SplitBezier(bezier.Points, t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static BezierSegment[] Split(this CubicBezier bezier, IEnumerable<double> ts)
        {
            if (ts == null) return new[] { new BezierSegment(bezier.Points) };

            var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToList();

            if (filtered.Count == 0) return new[] { new BezierSegment(bezier.Points) };

            var tLast = 0d;
            var start = new BezierSegment(bezier.Points);
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
        /// Cut a <see cref="BezierSegment"/> into multiple fragments at the given t indices, using "De Casteljau" algorithm.
        /// The value at which to split the curve. Should be strictly inside ]0,1[ interval.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="t"></param>
        /// <remarks>
        /// http://pomax.github.io/bezierinfo/#decasteljau
        /// </remarks>
        public static BezierSegment[] SplitBezier(IEnumerable<Point2D> points, double t)
        {
            if (t < 0 || t > 1) throw new ArgumentOutOfRangeException();
            var r0 = new List<Point2D>();
            var r1 = new List<Point2D>();
            var lp = points.ToList();
            while (lp.Count > 0)
            {
                r0.Add(lp.First());
                r1.Add(lp.Last());
                var next = new List<Point2D>(lp.Count - 1);
                for (int i = 0; i < lp.Count - 1; i++)
                {
                    var p0 = lp[i];
                    var p1 = lp[i + 1];
                    var x = p0.X * (1 - t) + t * p1.X;
                    var y = p0.Y * (1 - t) + t * p1.Y;
                    next.Add(new Point2D(x, y));
                }
                lp = next;
            }

            return new[] {
                new BezierSegment(r0.ToArray()),
                new BezierSegment(r1.ToArray())
            };
        }
    }
}
