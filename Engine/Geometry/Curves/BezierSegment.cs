// <copyright file="BezierSegment.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using static System.Math;
using static Engine.Maths;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// Utility class about 2D Bezier curves
    /// Aka a segment, or quadratic or cubic bezier curve.
    /// Extensive Bezier explanation can be found at http://pomax.github.io/bezierinfo/
    /// </summary>
    /// <remarks> https://github.com/superlloyd/Poly </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(QuadraticBezier))]
    [XmlType(TypeName = "bezier-Segment")]
    public class BezierSegment
        : Shape, IOpenShape
    {
        #region Constants

        private static readonly double[] Bezier01 = new double[] { 0, 1 };

        private static readonly Polynomial T = new Polynomial(Bezier01);

        private static readonly Polynomial OneMinusT = 1 - T;

        #endregion

        #region Fields

        /// <summary>
        /// 
        /// </summary>
        Point2D[] points;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public BezierSegment()
            : this(new Point2D[] { })
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public BezierSegment(IEnumerable<Point2D> points)
            : this(points.ToArray())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public BezierSegment(params Point2D[] points)
            : base()
            => this.points = points;

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public void Deconstruct(out Point2D[] points)
        {
            points = this.points;
        }

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point2D this[int index]
        {
            get { return points[index]; }
            set { points[index] = value; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlArray]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D[] Points
        {
            get { return points; }
            set
            {
                points = value;
                OnPropertyChanged(nameof(Points));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore, SoapIgnore]
        public override Rectangle2D Bounds
        {
            get
            {
                return (Rectangle2D)CachingProperty(() => bounds());

                Rectangle2D bounds()
                {
                    (double x0, double x1) = CurveX.GetMinMax(0, 1);
                    (double y0, double y1) = CurveY.GetMinMax(0, 1);
                    return new Rectangle2D(x0, y0, x1 - x0, y1 - y0);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                var curveX = (Polynomial)CachingProperty(() => Bezier(points.Select(p => p.X).ToArray()));
                curveX.IsReadonly = true;
                return curveX;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Polynomial CurveY
        {
            get
            {
                var curveY = (Polynomial)CachingProperty(() => Bezier(points.Select(p => p.Y).ToArray()));
                curveY.IsReadonly = true;
                return curveY;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public PolynomialDegree Degree
            => (PolynomialDegree)(Points.Length - 1);

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double ClosestParameter(Point2D point)
        {
            var dsquare = ParameterizedSquareDistance(point);
            var deriv = dsquare.Derivate().Normalize();
            var derivRoots = deriv.RealOrComplexRoots();
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
            var derivRoots = deriv.RealOrComplexRoots();
            return derivRoots
                .Where(t => t > 0 && t < 1)
                .Concat(Bezier01)
                .Select(x => Sqrt(dsquare.Compute(x)))
                .OrderBy(x => x)
                .First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Polynomial ParameterizedSquareDistance(Point2D p)
        {
            var vx = CurveX - p.X;
            var vy = CurveY - p.Y;
            return vx * vx + vy * vy;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Polynomial Bezier(params double[] values)
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
        public static Polynomial Bezier(int from, int to, double[] values)
        {
            if (from == to)
                return new Polynomial(values[from]);
            return OneMinusT * Bezier(from, to - 1, values) + T * Bezier(from + 1, to, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public static Polynomial Line(double p0, double p1)
        {
            var T = new Polynomial(0, 1);
            return (1 - T) * p0 + T * p1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Polynomial Quadratic(double p0, double p1, double p2)
        {
            var T = new Polynomial(0, 1);
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
        public static Polynomial Cubic(double p0, double p1, double p2, double p3)
        {
            var T = new Polynomial(0, 1);
            return (1 - T) * Quadratic(p0, p1, p2) + T * Quadratic(p1, p2, p3);
        }

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
        /// Cut a <see cref="BezierSegment"/> in multiple fragment at the given t indices, using "De Casteljau" algorithm.
        /// <param name="t">The value at which to split the curve. Should be strictly inside ]0,1[ interval.</param>
        /// </summary>
        public BezierSegment[] Split(double t)
        {
            if (t < 0 || t > 1)
                throw new ArgumentOutOfRangeException();
            // http://pomax.github.io/bezierinfo/#decasteljau
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
            return new[] { new BezierSegment(r0.ToArray()), new BezierSegment(r1.ToArray()) };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public BezierSegment[] Split(params double[] ts)
            => Split((IEnumerable<double>)ts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public BezierSegment[] Split(IEnumerable<double> ts)
        {
            if (ts == null)
                return new[] { this };
            var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToList();
            if (filtered.Count == 0)
                return new[] { this };

            var tLast = 0.0;
            var start = this;
            var list = new List<BezierSegment>(filtered.Count + 1);
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

        /// <summary>
        /// Creates a string representation of this <see cref="Polygon"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(BezierSegment);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(BezierSegment)}{{{string.Join(sep.ToString(), points)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
