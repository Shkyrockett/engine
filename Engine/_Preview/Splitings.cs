using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
