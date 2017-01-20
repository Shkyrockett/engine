/*
  A javascript Bezier curve library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static Engine.Maths;
using static System.Math;

namespace Engine
{
    /// <summary>
    ///
    /// </summary>
    public class Bezier
    {
        #region Private Fields

        /// <summary>
        ///
        /// </summary>
        public List<Point3D> _lut = new List<Point3D>();

        /// <summary>
        ///
        /// </summary>
        private List<char> dims = new List<char> { 'x', 'y', 'z' };

        #endregion

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="v4"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public Bezier(double x1, double y1, double v1, double v2, double v3, double v4, double x2, double y2) => Points = new List<Point3D>
            {
                new Point3D(x1,y1,0),
                new Point3D(v1,v2,0),
                new Point3D(v3,v4,0),
                new Point3D(x2,y2,0)
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="v4"></param>
        /// <param name="v5"></param>
        /// <param name="v6"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        public Bezier(double x1, double y1, double z1, double v1, double v2, double v3, double v4, double v5, double v6, double x2, double y2, double z2) => Points = new List<Point3D>
            {
                new Point3D(x1,y1,z1),
                new Point3D(v1,v2,v3),
                new Point3D(v4,v5,v6),
                new Point3D(x2,y2,z2)
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="points"></param>
        public Bezier(List<Point3D> points) => this.Points = points;

        /// <summary>
        ///
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        public Bezier(Point3D p1, Point3D p2, Point3D p3)
            : this(new List<Point3D> { p1, p2, p3 })
        { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        public Bezier(Point3D p1, Point3D p2, Point3D p3, Point3D p4)
            : this(new List<Point3D> { p1, p2, p3, p4 })
        { }

        #endregion

        #region Properties

        /// <summary>
        ///
        /// </summary>
        public List<Point3D> Points { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<Point3D> Dpoints { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool Virtual { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double T1 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double T2 { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool Is3d { get; }

        /// <summary>
        ///
        /// </summary>
        public bool Clockwise { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int Order { get; }

        /// <summary>
        ///
        /// </summary>
        public bool Linear { get; }

        #endregion

        #region Operators

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Bezier left, Bezier right) => left.Equals(right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Bezier left, Bezier right) => !left.Equals(right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(Bezier left, Bezier right)
        {
            if (left.Points.Count != right.Points.Count) return false;
            bool equals = false;
            for (int i = 0; i < left.Points.Count; i++)
            {
                equals &= left.Points[i].X == right.Points[i].X;
                equals &= left.Points[i].Y == right.Points[i].Y;
                equals &= left.Points[i].Z == right.Points[i].Z;
                if (!equals) break;
            }

            return equals;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is Bezier && Equals(this, (Bezier)obj);

        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashcode = 0;
            foreach (Point3D point in Points)
            {
                hashcode ^= point.X.GetHashCode();
                hashcode ^= point.Y.GetHashCode();
                hashcode ^= point.Z.GetHashCode();
            }
            return hashcode;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="n"></param>
        /// <param name="S"></param>
        /// <param name="B"></param>
        /// <param name="E"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static (Point3D, Point3D, Point3D) GetABC(double n, Point3D S, Point3D B, Point3D E, double t = 0.5)
        {
            double u = ProjectionRatio(t, n);
            double um = 1 - u;
            var C = new Point3D(
                x: u * S.X + um * E.X,
                y: u * S.Y + um * E.Y,
                z: u * S.Z + um * E.Z
            );
            double s = Abcratio(t, n);
            var A = new Point3D(
                x: B.X + (B.X - C.X) / s,
                y: B.Y + (B.Y - C.Y) / s,
                z: B.Z + (B.Z - C.Z) / s
            );
            return (A, B, C);
        }

        private static double Abcratio(double t, double n) => throw new NotImplementedException();

        private static double ProjectionRatio(double t, double n) => throw new NotImplementedException();

        /// <summary>
        ///
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Bezier QuadraticFromPoints(Point3D p1, Point3D p2, Point3D p3, double t = 0.5)
        {
            // shortcuts, although they're really dumb
            if (t == 0) return new Bezier(p2, p2, p3); if (t == 1) return new Bezier(p1, p2, p2);             // real fitting.
            (Point3D, Point3D, Point3D) abc = GetABC(2, p1, p2, p3, t);
            return new Bezier(p1, abc.Item1, p3);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="S"></param>
        /// <param name="B"></param>
        /// <param name="E"></param>
        /// <param name="t"></param>
        /// <param name="d1"></param>
        /// <returns></returns>
        public static Bezier CubicFromPoints(Point3D S, Point3D B, Point3D E, double t = 0.5, double d1 = 0)
        {
            (Point3D, Point3D, Point3D) abc = GetABC(3, S, B, E, t);
            if (d1 == 0) d1 = Distances.Distance(B, abc.Item3); double d2 = d1 * (1 - t) / t;

            double selen = Distances.Distance(S, E);
            double lx = (E.X - S.X) / selen;
            double ly = (E.Y - S.Y) / selen;
            double lz = (E.Z - S.Z) / selen;
            double bx1 = d1 * lx;
            double by1 = d1 * ly;
            double bz1 = d1 * lz;
            double bx2 = d2 * lx;
            double by2 = d2 * ly;
            double bz2 = d2 * lz;
            // derivation of new hull coordinates
            var e1 = new Point3D(
                x: B.X - bx1,
                y: B.Y - by1,
                z: B.Z - bz1
                );
            var e2 = new Point3D(
                x: B.X + bx2,
                y: B.Y + by2,
                z: B.Z + bz2
                );
            Point3D A = abc.Item1;
            var v1 = new Point3D(
                x: A.X + (e1.X - A.X) / (1 - t),
                y: A.Y + (e1.Y - A.Y) / (1 - t),
                z: A.Z + (e1.Z - A.Z) / (1 - t)
                );
            var v2 = new Point3D(
                x: A.X + (e2.X - A.X) / (t),
                y: A.Y + (e2.Y - A.Y) / (t),
                z: A.Z + (e2.Z - A.Z) / (t)
                );
            var nc1 = new Point3D(
                x: S.X + (v1.X - S.X) / (t),
                y: S.Y + (v1.Y - S.Y) / (t),
                z: S.Y + (v1.Y - S.Y) / (t)
                );
            var nc2 = new Point3D(
                x: E.X + (v2.X - E.X) / (1 - t),
                y: E.Y + (v2.Y - E.Y) / (1 - t),
                z: E.Y + (v2.Y - E.Y) / (1 - t)
                );
            // ...done
            return new Bezier(S, nc1, nc2, E);
        }

        /// <summary>
        ///
        /// </summary>
        public void Update()
        {
            // one-time compute derivative coordinates
            Dpoints = new List<Point3D>();
            List<Point3D> p = Points;
            for (int d = p.Count, c = d - 1; d > 1; d--, c--)
            {
                var list = new List<Point3D>();
                for (int j = 0; j < c; j++)
                {
                    var dpt = new Point3D(
                         x: c * (p[j + 1].X - p[j].X),
                         y: c * (p[j + 1].Y - p[j].Y),
                         z: c * (p[j + 1].Z - p[j].Z)
                    );
                    list.Add(dpt);
                }
                Dpoints.AddRange(list);
                p = list;
            }
            Computedirection();
        }

        /// <summary>
        ///
        /// </summary>
        public void Computedirection()
        {
            List<Point3D> points = this.Points;
            double angle = Utilities.Angle(points[0], points[Order], points[1]);
            Clockwise = angle > 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public double Length()
            => Utilities.Length(Derivative);

        /// <summary>
        ///
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        public List<Point3D> GetLUT(int steps)
        {
            //steps = steps || 100;
            if (_lut.Count == steps) return _lut; _lut = new List<Point3D>();
            for (int t = 0; t <= steps; t++)
                _lut.Add(Compute(t / steps));
            return _lut;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="point"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public double On(Point3D point, double error)
        {
            //error = error || 5;
            List<Point3D> lut = GetLUT(1000);
            var hits = new List<Point3D>();
            Point3D c;
            double t = 0;
            for (int i = 0; i < lut.Count; i++)
            {
                c = lut[i];
                if (Distances.Distance(c, point) < error)
                {
                    hits.Add(c);
                    t += i / lut.Count;
                }
            }
            if (hits.Count == 0) return 0;
            return t /= hits.Count;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Point3D Project(Point3D point)
        {
            // step 1: coarse check
            List<Point3D> LUT = GetLUT(1000);
            int l = LUT.Count - 1;

            (double X, double Y) closest = Utilities.Closest(LUT, point);
            double mdist = closest.Item1;
            double mpos = closest.Item2;
            if (mpos == 0 || mpos == l)
            {
                double t0 = mpos / l;
                Point3D pt = Compute(t0);
                //pt.t = t0;
                //pt.d = mdist;
                return pt;
            }

            // step 2: fine check
            double ft;
            double t;
            Point3D p;
            double d;
            double t1 = (mpos - 1) / l,
                t2 = (mpos + 1) / l,
                step = 0.1 / l;
            mdist += 1;

            for (t = t1, ft = t; t < t2 + step; t += step)
            {
                p = Compute(t);
                d = Distances.Distance(point, p);
                if (d < mdist)
                {
                    mdist = d;
                    ft = t;
                }
            }
            p = Compute(ft);
            //p.t = ft;
            //p.d = mdist;
            return p;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        internal Point3D Get(double t)
            => Compute(t);

        /// <summary>
        ///
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        private Point3D Point(int idx)
            => Points[idx];

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Point3D Compute(double t)
        {
            // shortcuts
            if (t == 0) return Points[0]; if (t == 1) return Points[Order];
            List<Point3D> p = Points;
            double mt = 1 - t;

            // linear?
            if (Order == 1)
            {
                var ret = new Point3D(
                    x: mt * p[0].X + t * p[1].X,
                    y: mt * p[0].Y + t * p[1].Y,
                    z: mt * p[0].Z + t * p[1].Z
                );
                return ret;
            }

            // quadratic/cubic curve?
            if (Order < 4)
            {
                double mt2 = mt * mt;
                double t2 = t * t;
                double a = 0;
                double b = 0;
                double c = 0;
                double d = 0;
                if (Order == 2)
                {
                    p = new List<Point3D> { Points[0], Points[1], Points[2], Point3D.Empty };
                    a = mt2;
                    b = mt * t * 2;
                    c = t2;
                }
                else if (Order == 3)
                {
                    a = mt2 * mt;
                    b = mt2 * t * 3;
                    c = mt * t2 * 3;
                    d = t * t2;
                }
                var ret =
                    new Point3D(
                    x: a * p[0].X + b * p[1].X + c * p[2].X + d * p[3].X,
                    y: a * p[0].Y + b * p[1].Y + c * p[2].Y + d * p[3].Y,
                    z: a * p[0].Z + b * p[1].Z + c * p[2].Z + d * p[3].Z
                    );
                return ret;
            }

            // higher order curves: use de Casteljau's computation
            List<Point3D> dCpts = Points;
            while (dCpts.Count > 1)
            {
                for (int i = 0; i < dCpts.Count - 1; i++)
                {
                    dCpts[i] = new Point3D(
                        x: dCpts[i].X + (dCpts[i + 1].X - dCpts[i].X) * t,
                        y: dCpts[i].Y + (dCpts[i + 1].Y - dCpts[i].Y) * t,
                        z: dCpts[i].Z + (dCpts[i + 1].Z - dCpts[i].Z) * t
                    );
                }
                //dCpts.splice(dCpts.Count - 1, 1);
            }
            return dCpts[0];
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Bezier Raise()
        {
            List<Point3D> p = Points;
            var np = new List<Point3D>(Points.Count) { p[0] };
            int k = p.Count;
            Point3D pi;
            Point3D pim;
            for (int i = 1; i < k; i++)
            {
                pi = p[i];
                pim = p[i - 1];
                np[i] = new Point3D(
                    x: (k - i) / k * pi.X + i / k * pim.X,
                    y: (k - i) / k * pi.Y + i / k * pim.Y,
                    z: (k - i) / k * pi.Z + i / k * pim.Z
                );
            }
            np[k] = p[k - 1];
            return new Bezier(np);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Point3D Derivative(double t)
        {
            double mt = 1 - t;
            double a = 0;
            double b = 0;
            double c = 0;
            var p = new List<Point3D>(3) { Dpoints[0] };
            if (Order == 2)
            {
                p = new List<Point3D> { p[0], p[1], Point3D.Empty };
                a = mt;
                b = t;
            }
            if (Order == 3)
            {
                a = mt * mt;
                b = mt * t * 2;
                c = t * t;
            }
            var ret = new Point3D(
                x: a * p[0].X + b * p[1].X + c * p[2].X,
                y: a * p[0].Y + b * p[1].Y + c * p[2].Y,
                z: a * p[0].Z + b * p[1].Z + c * p[2].Z
            );
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private List<double> Inflections()
            => Utilities.Inflections(Points);

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Point3D Normal(double t)
            => Is3d ? Normal3(t) : Normal2(t);

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Point3D Normal2(double t)
        {
            Point3D d = Derivative(t);
            double q = Sqrt(d.X * d.X + d.Y * d.Y);
            return new Point3D(
                x: -d.Y / q,
                y: d.X / q,
                z: 0
                );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Point3D Normal3(double t)
        {
            // see http://stackoverflow.com/questions/25453159
            Point3D r1 = Derivative(t);
            Point3D r2 = Derivative(t + 0.01);
            double q1 = Sqrt(r1.X * r1.X + r1.Y * r1.Y + r1.Z * r1.Z);
            double q2 = Sqrt(r2.X * r2.X + r2.Y * r2.Y + r2.Z * r2.Z);
            r1.X /= q1; r1.Y /= q1; r1.Z /= q1;
            r2.X /= q2; r2.Y /= q2; r2.Z /= q2;
            // cross product
            var c = new Point3D(
                x: r2.Y * r1.Z - r2.Z * r1.Y,
                y: r2.Z * r1.X - r2.X * r1.Z,
                z: r2.X * r1.Y - r2.Y * r1.X
            );
            double m = Sqrt(c.X * c.X + c.Y * c.Y + c.Z * c.Z);
            c.X /= m; c.Y /= m; c.Z /= m;
            // rotation matrix
            var R = new List<double> {
                c.X * c.X, c.X * c.Y - c.Z, c.X * c.Z + c.Y,
                c.X * c.Y + c.Z, c.Y * c.Y, c.Y * c.Z - c.X,
                c.X * c.Z - c.Y, c.Y * c.Z + c.X, c.Z * c.Z};
            // normal vector:
            var n = new Point3D(
                x: R[0] * r1.X + R[1] * r1.Y + R[2] * r1.Z,
                y: R[3] * r1.X + R[4] * r1.Y + R[5] * r1.Z,
                z: R[6] * r1.X + R[7] * r1.Y + R[8] * r1.Z
            );
            return n;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<Point3D> Hull(double t)
        {
            List<Point3D> p = Points;
            var _p = new List<Point3D>();
            Point3D pt;
            var q = new List<Point3D>();
            int idx = 0,
               i = 0,
               l = 0;
            q[idx++] = p[0];
            q[idx++] = p[1];
            q[idx++] = p[2];
            if (Order == 3) q[idx++] = p[3];             // we lerp between all points at each iteration, until we have 1 point left.
            while (p.Count > 1)
            {
                _p = new List<Point3D>();
                for (i = 0, l = p.Count - 1; i < l; i++)
                {
                    pt = Interpolaters.Linear(p[i], p[i + 1], t);
                    q[idx++] = pt;
                    _p.Add(pt);
                }
                p = _p;
            }
            return q;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public Bezier Split(double t1, double t2)
        {
            // shortcuts
            if (t1 == 0 && t2 != 0) return Split(t2).Left; if (t2 == 1) return Split(t1).Right;
            // no shortcut: use "de Casteljau" iteration.
            List<Point3D> q = Hull(t1);
            var result = new Pair(
                left: Order == 2 ? new Bezier(new List<Point3D> { q[0], q[3], q[5] }) : new Bezier(new List<Point3D> { q[0], q[4], q[7], q[9] }),
                right: Order == 2 ? new Bezier(new List<Point3D> { q[5], q[4], q[2] }) : new Bezier(new List<Point3D> { q[9], q[8], q[6], q[3] }),
                span: q
            );

            // make sure we bind _t1/_t2 information!
            result.Left.T1 = Map(0, 0, 1, T1, T2);
            result.Left.T2 = Map(t1, 0, 1, T1, T2);
            result.Right.T1 = Map(t1, 0, 1, T1, T2);
            result.Right.T2 = Map(1, 0, 1, T1, T2);

            // if we have no t2, we're done
            if (t2 != 0) return result.Left;
            // if we have a t2, split again:
            t2 = Map(t2, t1, 1, 0, 1);
            Pair subsplit = result.Right.Split(t2);
            return subsplit.Left;
        }

        private double Map(double t2, double t1, double v1, double v2, double v3) => throw new NotImplementedException();

        /// <summary>
        ///
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal Pair Split(double v)
            => new Pair();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<double> Extrema()
        {
            List<char> dims = this.dims;
            var result = new List<double>();
            var roots = new List<double>();
            //Point3D p;
            //Func<T> mfn;

            //dims.forEach(function(dim) {
            //    mfn = new Func<T> function(v) { return v[dim]; };
            //    p = this.dpoints[0].map(mfn);
            //    result[dim] = Utilities.droots(p);
            //    if (this.order == 3)
            //    {
            //        p = this.dpoints[1].map(mfn);
            //        result[dim] = result[dim].AddRange(Utilities.droots(p));
            //    }
            //    result[dim] = result[dim].filter(function(t) { return (t >= 0 && t <= 1); });
            //    roots = roots.AddRange(result[dim].sort());
            //}.bind(this));

            roots.Sort();
            result = roots;
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public BBox Bbox()
        {
            List<double> extrema = this.Extrema();
            return new BBox(
                GetMinMax(this, 0, extrema),
                GetMinMax(this, 1, extrema),
                GetMinMax(this, 2, extrema)
                );
        }

        private RangeX GetMinMax(Bezier bezier, int v, List<double> extrema) => throw new NotImplementedException();

        /// <summary>
        ///
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        public bool Overlaps(Bezier curve)
        {
            BBox lbbox = Bbox(),
                tbbox = curve.Bbox();
            return Bboxoverlap(lbbox, tbbox);
        }

        private bool Bboxoverlap(BBox lbbox, BBox tbbox) => throw new NotImplementedException();

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public (Point3D, Point3D, Point3D) Offset(double t, double d)
        {
            Point3D c = Get(t);
            Point3D n = Normal(t);
            return (
                c,//c:
                n,//n:
                new Point3D(c.X + n.X * d,//x:
                c.Y + n.Y * d,//y:
                c.Z + n.Z * d//z:
                )
            );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<Bezier> Offset(double t)
        {
            if (Linear)
            {
                Point3D nv = Normal(0);

                var coords = new List<Point3D>();
                foreach (Point3D p in Points)
                {
                    var ret = new Point3D(
                        x: p.X + t * nv.X,
                        y: p.Y + t * nv.Y,
                        z: p.Z + t * nv.Z
                    );
                    coords.Add(ret);
                }

                return new List<Bezier> { new Bezier(coords) };
            }
            List<Bezier> reduced = Reduce();

            return new List<Bezier>(
                from s in reduced
                select s.Scale(t)
                );
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool Simple()
        {
            if (Order == 3)
            {
                double a1 = Utilities.Angle(Points[0], Points[3], Points[1]);
                double a2 = Utilities.Angle(Points[0], Points[3], Points[2]);
                if (a1 > 0 && a2 < 0 || a1 < 0 && a2 > 0) return false;
            }
            Point3D n1 = Normal(0);
            Point3D n2 = Normal(1);
            double s = n1.X * n2.X + n1.Y * n2.Y + n1.Z * n2.Z;
            double angle = Abs(Acos(s));
            return angle < PI / 3;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<Bezier> Reduce()
        {
            int i;
            double t1 = 0, t2 = 0;
            double step = 0.01;
            Bezier segment;
            var pass1 = new List<Bezier>();
            var pass2 = new List<Bezier>();
            // first pass: split on extrema
            List<double> extrema = this.Extrema();
            if (extrema.IndexOf(0) == -1) extrema.Insert(0, 0); if (extrema.IndexOf(1) == -1) extrema.Add(1); for (t1 = extrema[0], i = 1; i < extrema.Count; i++)
            {
                t2 = extrema[i];
                segment = Split(t1, t2);
                segment.T1 = t1;
                segment.T2 = t2;
                pass1.Add(segment);
                t1 = t2;
            }
            // second pass: further reduce these segments to simple segments
            foreach (Bezier p1 in pass1)
            {
                t1 = 0;
                t2 = 0;
                while (t2 <= 1)
                {
                    for (t2 = t1 + step; t2 <= 1 + step; t2 += step)
                    {
                        segment = p1.Split(t1, t2);
                        if (!segment.Simple())
                        {
                            t2 -= step;
                            if (Abs(t1 - t2) < step)
                            {
                                // we can never form a reduction
                                return new List<Bezier>();
                            }
                            segment = p1.Split(t1, t2);
                            segment.T1 = Map(t1, 0, 1, p1.T1, p1.T2);
                            segment.T2 = Map(t2, 0, 1, p1.T1, p1.T2);
                            pass2.Add(segment);
                            t1 = t2;
                            break;
                        }
                    }
                }
                if (t1 < 1)
                {
                    segment = p1.Split(t1, 1);
                    segment.T1 = Map(t1, 0, 1, p1.T1, p1.T2);
                    segment.T2 = p1.T2;
                    pass2.Add(segment);
                }
            }

            return pass2;
        }

        private double Map(double t1, int v1, int v2, double t2, double t3) => throw new NotImplementedException();

        /// <summary>
        ///
        /// </summary>
        /// <param name="distanceFn"></param>
        /// <returns></returns>
        public Bezier Scale(DerivitiveMethodDouble distanceFn)
        {
            int order = this.Order;
            if (order == 2)
                return Raise().Scale(distanceFn);

            // TODO: add special handling for degenerate (=linear) curves.
            bool clockwise = this.Clockwise;
            double r1 = distanceFn(0);
            double r2 = distanceFn(1);
            var v = new List<(Point3D, Point3D, Point3D)> { Offset(0, 10), Offset(1, 10) };
            Point3D o = Lli4(v[0].Item3, v[0].Item1, v[1].Item3, v[1].Item1);
            if (o == null) throw new NullReferenceException("cannot scale this curve. Try reducing it first.");
            // move all points by distance 'd' wrt the origin 'o'
            List<Point3D> points = this.Points;
            var np = new List<Point3D>();

            // move end points by fixed distance along normal.
            foreach (int t in new List<int> { 0, 1 })
            {
                Point3D p = np[t * order] = Copy(points[t * order]);
                p.X += (t == 0 ? r2 : r1) * v[t].Item2.X;
                p.Y += (t == 0 ? r2 : r1) * v[t].Item2.Y;
            }

            // move control points by "however much necessary to
            // ensure the correct tangent to endpoint".
            foreach (int t in new List<int> { 0, 1 })
            {
                if (this.Order == 2) break;
                Point3D p = points[t + 1];
                var ov = new Point3D(
                        x: p.X - o.X,
                        y: p.Y - o.Y,
                        z: p.Z - o.Z);
                double rc = distanceFn((t + 1) / order);
                if (!clockwise) rc = -rc;
                double m = Sqrt(ov.X * ov.X + ov.Y * ov.Y);
                ov.X /= m;
                ov.Y /= m;
                np[t + 1] = new Point3D(
                    x: p.X + rc * ov.X,
                    y: p.Y + rc * ov.Y,
                    z: p.Z + rc * ov.Z);
            }

            return new Bezier(np);
        }

        private Point3D Copy(Point3D point3D) => throw new NotImplementedException();

        private Point3D Lli4(Point3D item31, Point3D item11, Point3D item32, Point3D item12) => throw new NotImplementedException();

        /// <summary>
        ///
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public Bezier Scale(double d)
        {
            int order = this.Order;
            bool clockwise = this.Clockwise;
            double r1 = d;
            double r2 = d;
            var v = new List<(Point3D, Point3D, Point3D)> { Offset(0, 10), Offset(1, 10) };
            Point3D o = Lli4(v[0].Item3, v[0].Item1, v[1].Item3, v[1].Item1);
            if (o == null) throw new NullReferenceException("cannot scale this curve. Try reducing it first.");             // move all points by distance 'd' wrt the origin 'o'
            List<Point3D> points = this.Points;
            var np = new List<Point3D>();

            // move end points by fixed distance along normal.
            foreach (int t in new List<int> { 0, 1 })
            {
                Point3D p = np[t * order] = Copy(points[t * order]);
                p.X += (t == 0 ? r2 : r1) * v[t].Item2.X;
                p.Y += (t == 0 ? r2 : r1) * v[t].Item2.Y;
            }

            // move control points to lie on the intersection of the offset
            // derivative vector, and the origin-through-control vector
            foreach (int t in new List<int> { 0, 1 })
            {
                if (this.Order == 2) break;
                Point3D p = np[t * order];
                Point3D d2 = Derivative(t);
                var p2 = new Point3D(x: p.X + d2.X, y: p.Y + d2.Y, z: p.Z + d2.Z);
                np[t + 1] = Lli4(p, p2, o, points[t + 1]);
            }
            return new Bezier(np);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <param name="tlen"></param>
        /// <param name="alen"></param>
        /// <param name="slen"></param>
        /// <returns></returns>
        public double LinearDistanceFunction(double s, double e, double tlen, double alen, double slen)
        {
            double v = 0;
            double f1 = alen / tlen;
            double f2 = (alen + slen) / tlen;
            double d = e - s;
            return Map(v, 0, 1, s + f1 * d, s + f2 * d);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="d1"></param>
        /// <returns></returns>
        public PolyBezier Outline(double d1)
            => Outline(d1, d1, 0, 0, true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public PolyBezier Outline(double d1, double d2)
            => Outline(d1, d2, 0, 0, true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <returns></returns>
        public PolyBezier Outline(double d1, double d3, double d4)
            => Outline(d1, d1, d3, d4, false);

        /// <summary>
        ///
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="graduated"></param>
        /// <returns></returns>
        public PolyBezier Outline(double d1, double d2, double d3, double d4, bool graduated = false)
        {
            List<Bezier> reduced = Reduce();
            int len = reduced.Count;
            var fcurves = new List<Bezier>();
            var bcurves = new List<Bezier>();
            List<Point3D> p;
            double alen = 0;
            double slen = 0;
            double tlen = Length();

            // form curve oulines
            foreach (Bezier segment in reduced)
            {
                slen = segment.Length();
                if (graduated)
                {
                    fcurves.Add(segment.Scale(LinearDistanceFunction(d1, d3, tlen, alen, slen)));
                    bcurves.Add(segment.Scale(LinearDistanceFunction(-d2, -d4, tlen, alen, slen)));
                }
                else
                {
                    fcurves.Add(segment.Scale(d1));
                    bcurves.Add(segment.Scale(-d2));
                }
                alen += slen;
            }

            // reverse the "return" outline

            var tcurves = new List<Bezier>();
            foreach (Bezier s in bcurves)
            {
                p = s.Points;
                if (p[3] != null)
                    s.Points = new List<Point3D> { p[3], p[2], p[1], p[0] };
                else
                    s.Points = new List<Point3D> { p[2], p[1], p[0] };
                tcurves.Add(s);
            }
            tcurves.Reverse();
            bcurves = tcurves;

            var segments = new List<Bezier>();
            // form the endcaps as lines
            Point3D fs = fcurves[0].Points[0];
            Point3D fe = fcurves[len - 1].Points[fcurves[len - 1].Points.Count - 1];
            Point3D bs = bcurves[len - 1].Points[bcurves[len - 1].Points.Count - 1];
            Point3D be = bcurves[0].Points[0];
            Bezier ls = MakeLine(bs, fs);
            Bezier le = MakeLine(fe, be);
            segments.Add(ls);
            segments.AddRange(fcurves);
            segments.Add(le);
            segments.AddRange(bcurves);
            slen = segments.Count;

            return new PolyBezier(segments);
        }

        private Bezier MakeLine(Point3D bs, Point3D fs) => throw new NotImplementedException();

        /// <summary>
        ///
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public List<Shape1> Outlineshapes(double d1, double d2)
        {
            //d2 = d2 || d1;
            List<Bezier> outline = this.Outline(d1, d2).Curves;
            var shapes = new List<Shape1>();
            for (int i = 1, len = outline.Count; i < len / 2; i++)
            {
                Shape1 shape = MakeShape(outline[i], outline[len - i]);
                shape.Startcap.Virtual = (i > 1);
                shape.Endcap.Virtual = (i < len / 2 - 1);
                shapes.Add(shape);
            }
            return shapes;
        }

        private Shape1 MakeShape(Bezier bezier1, Bezier bezier2) => throw new NotImplementedException();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<Pair> Intersects()
            => Selfintersects();

        /// <summary>
        ///
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        public List<Pair> Intersects(Bezier curve)
            => Curveintersects(Reduce(), curve.Reduce());

        /// <summary>
        ///
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public List<bool> Intersects(Line1 line)
            => LineIntersects(line);

        /// <summary>
        ///
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public List<bool> LineIntersects(Line1 line)
        {
            double mx = Min(line.P1.X, line.P2.X);
            double my = Min(line.P1.Y, line.P2.Y);
            double MX = Max(line.P1.X, line.P2.X);
            double MY = Max(line.P1.Y, line.P2.Y);
            var self = this;

            return new List<bool>(
                from t in Roots(Points, line)
                let p = self.Get(t)

                select Containings.Between(p.X, mx, MX) && Containings.Between(p.Y, my, MY));
        }

        private List<double> Roots(List<Point3D> points, Line1 line) => new List<double> { 0 };

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<Pair> Selfintersects()
        {
            List<Bezier> reduced = Reduce();
            // "simple" curves cannot intersect with their direct
            // neighbor, so for each segment X we check whether
            // it intersects [0:x-2][x+2:last].
            int i, len = reduced.Count - 2;
            var results = new List<Pair>();
            List<Pair> result;
            List<Bezier> left, right;
            for (i = 0; i < len; i++)
            {
                left = reduced.GetRange(i, i + 1);
                right = reduced.GetRange(i + 2, reduced.Count - (i + 2));
                result = Curveintersects(left, right);
                results.AddRange(result);
            }
            return results;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        private List<Pair> Curveintersects(List<Bezier> c1, List<Bezier> c2)
        {
            var pairs = new List<Pair>();
            // step 1: pair off any overlapping segments
            foreach (Bezier l in c1)
            {
                foreach (Bezier r in c2)
                {
                    if (l.Overlaps(r))
                        pairs.Add(new Pair(left: l, right: r));
                }
            }

            // step 2: for each pairing, run through the convergence algorithm.
            var intersections = new List<Pair>();
            foreach (Pair pair in pairs)
            {
                List<Pair> result = Pairiteration(pair.Left, pair.Right);
                if (result.Count > 0)
                    intersections.AddRange(result);
            }
            return intersections;
        }

        private List<Pair> Pairiteration(Bezier left, Bezier right) => throw new NotImplementedException();

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorThreshold"></param>
        /// <returns></returns>
        public List<Arc1> Arcs(double errorThreshold = 0.5)
        {
            //errorThreshold = errorThreshold || 0.5;
            var circles = new List<Arc1>();
            return Iterate(errorThreshold, circles);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="np1"></param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public double Error(Arc1 pc, Point3D np1, double s, double e)
        {
            double q = (e - s) / 4;
            Point3D c1 = Get(s + q);
            Point3D c2 = Get(e - q);
            double reff = Distances.Distance(pc.Center, np1);
            double d1 = Distances.Distance(pc.Center, c1);
            double d2 = Distances.Distance(pc.Center, c2);
            return Abs(d1 - reff) + Abs(d2 - reff);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorThreshold"></param>
        /// <param name="circles"></param>
        /// <returns></returns>
        public List<Arc1> Iterate(double errorThreshold, List<Arc1> circles)
        {
            double s = 0, e = 1, safety;
            // we do a binary search to find the "good `t` closest to no-longer-good"
            do
            {
                safety = 0;

                // step 1: start with the maximum possible arc
                e = 1;

                // points:
                Point3D np1 = Get(s), np2, np3;
                Arc1 arc = new Arc1(), prev_arc;

                // booleans:
                bool curr_good = false, prev_good = false, done;

                // numbers:
                double m = e, prev_e = 1, step = 0;

                // step 2: find the best possible arc
                do
                {
                    prev_good = curr_good;
                    prev_arc = arc;
                    m = (s + e) / 2;
                    step++;

                    np2 = Get(m);
                    np3 = Get(e);

                    arc = Getccenter(np1, np2, np3);
                    double error = Error(arc, np1, s, e);
                    curr_good = (error <= errorThreshold);

                    done = prev_good && !curr_good;
                    if (!done) prev_e = e;

                    // this arc is fine: we can move 'e' up to see if we can find a wider arc
                    if (curr_good)
                    {
                        // if e is already at max, then we're done for this arc.
                        if (e >= 1)
                        {
                            prev_e = 1;
                            prev_arc = arc;
                            break;
                        }
                        // if not, move it up by half the iteration distance
                        e += (e - s) / 2;
                    }

                    // this is a bad arc: we need to move 'e' down to find a good arc
                    else
                    {
                        e = m;
                    }
                }
                while (!done && safety++ < 100);

                if (safety >= 100)
                {
                    Debug.WriteLine("arc abstraction somehow failed...");
                    break;
                }

                // console.log("[F] arc found", s, prev_e, prev_arc.x, prev_arc.y, prev_arc.s, prev_arc.e);

                prev_arc = (prev_arc ?? arc);
                circles.Add(prev_arc);
                s = prev_e;
            }
            while (e < 1);
            return circles;
        }

        private Arc1 Getccenter(Point3D np1, Point3D np2, Point3D np3) => throw new NotImplementedException();

        /// <summary>
        ///
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public List<double> CubicBezierCardanoIntersection(Line1 line)
            => CubicBezierCardanoIntersection(Points[0], Points[1], Points[2], Points[3], line);

        /// <summary>
        /// Cardano's algorithm
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/26823024/cubic-bezier-reverse-getpoint-equation-float-for-vector-vector-for-float?answertab=active#tab-top
        /// http://jsbin.com/mutaracihafi/1/edit?js,output
        /// http://pomax.github.io/bezierinfo/
        /// based on
        /// http://www.trans4mind.com/personal_development/mathematics/polynomials/cubicAlgebra.htm
        /// </remarks>
        private static List<double> CubicBezierCardanoIntersection(Point3D p1, Point3D p2, Point3D p3, Point3D p4, Line1 line)
        {
            // align curve with the intersecting line, translating/rotating
            // so that the first point becomes (0,0), and the last point
            // ends up lying on the line we're trying to use as root-intersect.
            List<Point3D> aligned = Align(new List<Point3D> { p1, p2, p3, p4 }, line);
            // rewrite from [a(1-t)^3 + 3bt(1-t)^2 + 3c(1-t)t^2 + dt^3] form...
            double pa = aligned[0].Y;
            double pb = aligned[1].Y;
            double pc = aligned[2].Y;
            double pd = aligned[3].Y;
            // ...to [t^3 + at^2 + bt + c] form:
            double d = (-pa + 3 * pb - 3 * pc + pd);
            double a = (3 * pa - 6 * pb + 3 * pc) / d;
            double b = (-3 * pa + 3 * pb) / d;
            double c = pa / d;
            // then, determine p and q:
            double p = (3 * b - a * a) / 3;
            double dp3 = p / 3;
            double q = (2 * a * a * a - 9 * a * b + 27 * c) / 27;
            double q2 = q / 2;
            // and determine the discriminant:
            double discriminant = q2 * q2 + dp3 * dp3 * dp3;
            // and some reserved variables for later
            double u1, v1, x1, x2, x3;

            // If the discriminant is negative, use polar coordinates
            // to get around square roots of negative numbers
            if (discriminant < 0)
            {
                double mp3 = -p / 3,
                    mp33 = mp3 * mp3 * mp3,
                    r = Sqrt(mp33),
                    t = -q / (2 * r),
                    // deal with IEEE rounding yielding <-1 or >1
                    cosphi = t < -1 ? -1 : t > 1 ? 1 : t,
                    phi = Acos(cosphi),
                    crtr = Maths.Crt(r),
                    t1 = 2 * crtr;
                x1 = t1 * Cos(phi / 3) - a / 3;
                x2 = t1 * Cos((phi + Tau) / 3) - a / 3;
                x3 = t1 * Cos((phi + 2 * Tau) / 3) - a / 3;
                return new List<double> { x1, x2, x3 };
            }
            else if (discriminant == 0)
            {
                u1 = q2 < 0 ? Maths.Crt(-q2) : -Maths.Crt(q2);
                x1 = 2 * u1 - a / 3;
                x2 = -u1 - a / 3;
                return new List<double> { x1, x2 };
            }
            else
            {
                // one real root, and two imaginary roots
                double sd = Sqrt(discriminant);
                double tt = -q2 + sd;
                u1 = Maths.Crt(-q2 + sd);
                v1 = Maths.Crt(q2 + sd);
                x1 = u1 - v1 - a / 3;
                return new List<double> { x1 };
            }
        }

        private static List<Point3D> Align(List<Point3D> list, Line1 line) => throw new NotImplementedException();
    }
}
