using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Utility class about 2D Bézier curves
    /// Aka a segment, or quadratic or cubic bezier curve.
    /// Extensive Bezier explanation can be found at http://pomax.github.io/bezierinfo/
    /// </summary>
    /// <remarks>
    /// https://github.com/superlloyd/Poly
    /// </remarks>
    public class BezierFragment
    {
        /// <summary>
        /// The bezier01 (readonly). Value: new double[] { 0, 1 }.
        /// </summary>
        private static readonly double[] Bezier01 = new double[] { 0, 1 };

        /// <summary>
        /// The control points.
        /// </summary>
        private Point2D[] controlPoints;

        /// <summary>
        /// The ro points.
        /// </summary>
        private ReadonlyPoints roPoints;

        /// <summary>
        /// The m curve x.
        /// </summary>
        private Polynomialx mCurveX;

        /// <summary>
        /// The m curve y.
        /// </summary>
        private Polynomialx mCurveY;

        /// <summary>
        /// The bounds.
        /// </summary>
        private Rectangle2D bounds = Rectangle2D.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BezierFragment"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public BezierFragment(IEnumerable<Point2D> points)
            : this(points.ToArray())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BezierFragment"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException">Bézier curve need at least 2 points (segment).</exception>
        public BezierFragment(params Point2D[] points)
        {
            if (points is null)
                throw new ArgumentNullException();
            if (points.Length < 2)
                throw new ArgumentException("Bézier curve need at least 2 points (segment).");
            controlPoints = points;
        }

        #region Compute() CurveX CurveY
        /// <summary>
        /// The compute.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public Point2D Compute(double t)
        {
            var x = CurveX.Compute(t);
            var y = CurveY.Compute(t);
            return new Point2D(x, y);
        }

        /// <summary>
        /// Gets the curve x.
        /// </summary>
        public Polynomialx CurveX
        {
            get
            {
                if (mCurveX is null)
                {
                    mCurveX = Bezier(controlPoints.Select(p => p.X).ToArray());
                    mCurveX.IsReadonly = true;
                }
                return mCurveX;
            }
        }

        /// <summary>
        /// Gets the curve y.
        /// </summary>
        public Polynomialx CurveY
        {
            get
            {
                if (mCurveY is null)
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
        /// The readonly points class.
        /// </summary>
        public class ReadonlyPoints
            : IReadOnlyList<Point2D>
        {
            /// <summary>
            /// The values.
            /// </summary>
            private Point2D[] values;

            /// <summary>
            /// Initializes a new instance of the <see cref="ReadonlyPoints"/> class.
            /// </summary>
            /// <param name="values">The values.</param>
            internal ReadonlyPoints(Point2D[] values)
            {
                this.values = values;
            }

            /// <summary>
            /// The Indexer.
            /// </summary>
            /// <param name="index">The index index.</param>
            /// <returns>One element of type Point2D.</returns>
            public Point2D this[int index] => values[index];

            /// <summary>
            /// Gets the count.
            /// </summary>
            public int Count => values.Length;

            /// <summary>
            /// Get the enumerator.
            /// </summary>
            /// <returns>The <see cref="T:IEnumerator{Point2D}"/>.</returns>
            public IEnumerator<Point2D> GetEnumerator() => values.Cast<Point2D>().GetEnumerator();

            /// <summary>
            /// Get the enumerator.
            /// </summary>
            /// <returns>The <see cref="IEnumerator"/>.</returns>
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        /// <summary>
        /// Gets the control points.
        /// </summary>
        public ReadonlyPoints ControlPoints
        {
            get
            {
                if (roPoints is null)
                    roPoints = new ReadonlyPoints(controlPoints);
                return roPoints;
            }
        }
        #endregion ControlPoints

        #region Basic Static Operations
        /// <summary>
        /// The bezier.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Polynomialx Bezier(params double[] values)
        {
            if (values is null || values.Length < 1)
                throw new ArgumentNullException();
            return Bezier(0, values.Length - 1, values);
        }

        /// <summary>
        /// The bezier.
        /// </summary>
        /// <param name="from">The from.</param>
        /// <param name="to">The to.</param>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        private static Polynomialx Bezier(int from, int to, double[] values)
        {
            if (from == to)
                return new Polynomialx(values[from]);
            return OneMinusT * Bezier(from, to - 1, values) + T * Bezier(from + 1, to, values);
        }

        /// <summary>
        /// The t (readonly). Value: new Polynomialx(0, 1).
        /// </summary>
        private static readonly Polynomialx T = new Polynomialx(0, 1);

        /// <summary>
        /// The one minus t (readonly). Value: 1 - T.
        /// </summary>
        private static readonly Polynomialx OneMinusT = 1 - T;

        /// <summary>
        /// The line.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        public static Polynomialx Line(double p0, double p1)
        {
            var T = new Polynomialx(0, 1);
            return (1 - T) * p0 + T * p1;
        }

        /// <summary>
        /// The quadratic.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        public static Polynomialx Quadratic(double p0, double p1, double p2)
        {
            var T = new Polynomialx(0, 1);
            return (1 - T) * Line(p0, p1) + T * Line(p1, p2);
        }

        /// <summary>
        /// The cubic.
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        public static Polynomialx Cubic(double p0, double p1, double p2, double p3)
        {
            var T = new Polynomialx(0, 1);
            return (1 - T) * Quadratic(p0, p1, p2) + T * Quadratic(p1, p2, p3);
        }
        #endregion Basic Static Operations

        #region BoundingBox()
        /// <summary>
        /// The bounding box.
        /// </summary>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        public Rectangle2D BoundingBox()
        {
            if (bounds.IsEmpty)
            {
                CurveX.GetMinMax(0, 1, out var x0, out var x1);
                CurveY.GetMinMax(0, 1, out var y0, out var y1);
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
        /// The split.
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="T:BezierFragment[]"/>.</returns>
        public BezierFragment[] Split(params double[] ts)
            => Split((IEnumerable<double>)ts);

        /// <summary>
        /// The split.
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="T:BezierFragment[]"/>.</returns>
        public BezierFragment[] Split(IEnumerable<double> ts)
        {
            if (ts is null)
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
        /// The parameterized square distance.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        public Polynomialx ParameterizedSquareDistance(Point2D p)
        {
            var vx = CurveX - p.X;
            var vy = CurveY - p.Y;
            return vx * vx + vy * vy;
        }

        /// <summary>
        /// The closest parameter.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="double"/>.</returns>
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
        /// The distance to.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="double"/>.</returns>
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
