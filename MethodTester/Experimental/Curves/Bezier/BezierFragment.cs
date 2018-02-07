using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Utility class about 2D Bezier curves
    /// Aka a segment, or quadratic or cubic bezier curve.
    /// Extensive Bezier explanation can be found at http://pomax.github.io/bezierinfo/
    /// </summary>
    /// <remarks>
    /// https://github.com/superlloyd/Poly
    /// </remarks>
    public class BezierFragment
    {
        /// <summary>
        /// 
        /// </summary>
        static readonly double[] Bezier01 = new double[] { 0, 1 };

        /// <summary>
        /// 
        /// </summary>
        Point2D[] controlPoints;

        /// <summary>
        /// 
        /// </summary>
        ReadonlyPoints roPoints;

        /// <summary>
        /// 
        /// </summary>
        Polynomialx mCurveX;

        /// <summary>
        /// 
        /// </summary>
        Polynomialx mCurveY;

        /// <summary>
        /// 
        /// </summary>
        Rectangle2D bounds = Rectangle2D.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public BezierFragment(IEnumerable<Point2D> points)
            : this(points.ToArray())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public BezierFragment(params Point2D[] points)
        {
            if (points == null)
                throw new ArgumentNullException();
            if (points.Length < 2)
                throw new ArgumentException("Bezier curve need at least 2 points (segment).");
            controlPoints = points;
        }

        #region Compute() CurveX CurveY
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Point2D Compute(double t)
        {
            var x = CurveX.Compute(t);
            var y = CurveY.Compute(t);
            return new Point2D(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        public Polynomialx CurveX
        {
            get
            {
                if (mCurveX == null)
                {
                    mCurveX = Bezier(controlPoints.Select(p => p.X).ToArray());
                    mCurveX.IsReadonly = true;
                }
                return mCurveX;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Polynomialx CurveY
        {
            get
            {
                if (mCurveY == null)
                {
                    mCurveY = Bezier(controlPoints.Select(p => p.Y).ToArray());
                    mCurveY.IsReadonly = true;
                }
                return mCurveY;
            }
        }
        #endregion Compute() CurveX CurveY

        #region ControlPoints
        /// <summary>
        /// 
        /// </summary>
        public class ReadonlyPoints
            : IReadOnlyList<Point2D>
        {
            /// <summary>
            /// 
            /// </summary>
            Point2D[] values;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="values"></param>
            internal ReadonlyPoints(Point2D[] values)
            {
                this.values = values;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public Point2D this[int index] => values[index];

            /// <summary>
            /// 
            /// </summary>
            public int Count { get { return values.Length; } }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public IEnumerator<Point2D> GetEnumerator() => values.Cast<Point2D>().GetEnumerator();

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        public ReadonlyPoints ControlPoints
        {
            get
            {
                if (roPoints == null)
                    roPoints = new ReadonlyPoints(controlPoints);
                return roPoints;
            }
        }
        #endregion ControlPoints

        #region Basic Static Operations
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Polynomialx Bezier(params double[] values)
        {
            if (values == null || values.Length < 1)
                throw new ArgumentNullException();
            return Bezier(0, values.Length - 1, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        static Polynomialx Bezier(int from, int to, double[] values)
        {
            if (from == to)
                return new Polynomialx(values[from]);
            return OneMinusT * Bezier(from, to - 1, values) + T * Bezier(from + 1, to, values);
        }

        /// <summary>
        /// 
        /// </summary>
        static readonly Polynomialx T = new Polynomialx(0, 1);

        /// <summary>
        /// 
        /// </summary>
        static readonly Polynomialx OneMinusT = 1 - T;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public static Polynomialx Line(double p0, double p1)
        {
            var T = new Polynomialx(0, 1);
            return (1 - T) * p0 + T * p1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Polynomialx Quadratic(double p0, double p1, double p2)
        {
            var T = new Polynomialx(0, 1);
            return (1 - T) * Line(p0, p1) + T * Line(p1, p2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static Polynomialx Cubic(double p0, double p1, double p2, double p3)
        {
            var T = new Polynomialx(0, 1);
            return (1 - T) * Quadratic(p0, p1, p2) + T * Quadratic(p1, p2, p3);
        }
        #endregion Basic Static Operations

        #region BoundingBox()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Rectangle2D BoundingBox()
        {
            if (bounds.IsEmpty)
            {
                CurveX.GetMinMax(0, 1, out double x0, out double x1);
                CurveY.GetMinMax(0, 1, out double y0, out double y1);
                bounds = new Rectangle2D(x0, y0, x1 - x0, y1 - y0);
            }
            return bounds;
        }
        #endregion BoundingBox()

        #region Split()
        /// <summary>
        /// Cut a <see cref="BezierFragment"/> in multiple fragment at the given t indices, using "De Casteljau" algorithm.
        /// <param name="t">The value at which to split the curve. Should be strictly inside ]0,1[ interval.</param>
        /// </summary>
        public BezierFragment[] Split(double t)
        {
            if (t < 0 || t > 1)
                throw new ArgumentOutOfRangeException();
            // http://pomax.github.io/bezierinfo/#decasteljau
            var r0 = new List<Point2D>();
            var r1 = new List<Point2D>();
            var lp = controlPoints.ToList();
            while (lp.Count > 0)
            {
                r0.Add(lp.First());
                r1.Add(lp.Last());
                var next = new List<Point2D>(lp.Count - 1);
                for (var i = 0; i < lp.Count - 1; i++)
                {
                    var p0 = lp[i];
                    var p1 = lp[i + 1];
                    var x = p0.X * (1 - t) + t * p1.X;
                    var y = p0.Y * (1 - t) + t * p1.Y;
                    next.Add(new Point2D(x, y));
                }
                lp = next;
            }
            return new[] { new BezierFragment(r0.ToArray()), new BezierFragment(r1.ToArray()) };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public BezierFragment[] Split(params double[] ts)
            => Split((IEnumerable<double>)ts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public BezierFragment[] Split(IEnumerable<double> ts)
        {
            if (ts == null)
                return new[] { this };
            var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
                return new[] { this };

            var tLast = 0.0;
            var start = this;
            var list = new List<BezierFragment>(filtered.Count + 1);
            foreach (var t in filtered)
            {
                var relT = 1 - (1 - t) / (1 - tLast);
                tLast = t;
                var cut = start.Split(relT);
                list.Add(cut[0]);
                start = cut[1];
            }
            list.Add(start);
            return list.ToArray();
        }
        #endregion Split()

        #region ParameterizedSquareDistance() ClosestParameter() DistanceTo()
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Polynomialx ParameterizedSquareDistance(Point2D p)
        {
            var vx = CurveX - p.X;
            var vy = CurveY - p.Y;
            return vx * vx + vy * vy;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double ClosestParameter(Point2D point)
        {
            var dsquare = ParameterizedSquareDistance(point);
            var deriv = dsquare.Derivate().Normalize();
            var derivRoots = deriv.SolveOrFindRealRoots();
            return derivRoots
                .Where(t => t > 0 && t < 1)
                .Concat(Bezier01)
                .OrderBy(x => dsquare.Compute(x))
                .First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double DistanceTo(Point2D point)
        {
            var dsquare = ParameterizedSquareDistance(point);
            var deriv = dsquare.Derivate().Normalize();
            var derivRoots = deriv.SolveOrFindRealRoots();
            return derivRoots
                .Where(t => t > 0 && t < 1)
                .Concat(Bezier01)
                .Select(x => Sqrt(dsquare.Compute(x)))
                .OrderBy(x => x)
                .First();
        }
        #endregion ParameterizedSquareDistance() ClosestParameter() DistanceTo()
    }
}
