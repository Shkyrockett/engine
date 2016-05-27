/*
  A javascript Bezier curve library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static Engine.Geometry.Maths;
using static Engine.Geometry.Utilities;
using static System.Math;

namespace Engine.Geometry
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
        private List<char> dims = new List<char>() { 'x', 'y', 'z' };

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
        public Bezier(double x1, double y1, double v1, double v2, double v3, double v4, double x2, double y2)
        {
            points = new List<Point3D>()
            {
                new Point3D(x1,y1,0),
                new Point3D(v1,v2,0),
                new Point3D(v3,v4,0),
                new Point3D(x2,y2,0)
            };
        }

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
        public Bezier(double x1, double y1, double z1, double v1, double v2, double v3, double v4, double v5, double v6, double x2, double y2, double z2)
        {
            points = new List<Point3D>()
            {
                new Point3D(x1,y1,z1),
                new Point3D(v1,v2,v3),
                new Point3D(v4,v5,v6),
                new Point3D(x2,y2,z2)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Bezier(List<Point3D> points)
        {
            this.points = points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        public Bezier(Point3D p1, Point3D p2, Point3D p3)
            : this(new List<Point3D>() { p1, p2, p3 })
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        public Bezier(Point3D p1, Point3D p2, Point3D p3, Point3D p4)
            : this(new List<Point3D>() { p1, p2, p3, p4 })
        { }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public List<Point3D> points { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Point3D> dpoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool _virtual { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double _t1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double _t2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool _3d { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool clockwise { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int order { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool _linear { get; private set; }

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Bezier left, Bezier right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Bezier left, Bezier right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(Bezier left, Bezier right)
        {
            if (left.points.Count != right.points.Count) return false;
            bool equals = false;
            for (int i = 0; i < left.points.Count(); i++)
            {
                equals &= left.points[i].X == right.points[i].X;
                equals &= left.points[i].Y == right.points[i].Y;
                equals &= left.points[i].Z == right.points[i].Z;
                if (!equals) break;
            }

            return equals;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Bezier && Equals(this, (Bezier)obj);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashcode = 0;
            foreach (Point3D point in points)
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
        public static Tuple<Point3D, Point3D, Point3D> getABC(double n, Point3D S, Point3D B, Point3D E, double t = 0.5)
        {
            double u = projectionratio(t, n);
            double um = 1 - u;
            Point3D C = new Point3D(
                x: u * S.X + um * E.X,
                y: u * S.Y + um * E.Y,
                z: u * S.Z + um * E.Z
            );
            double s = abcratio(t, n);
            Point3D A = new Point3D(
                x: B.X + (B.X - C.X) / s,
                y: B.Y + (B.Y - C.Y) / s,
                z: B.Z + (B.Z - C.Z) / s
            );
            return new Tuple<Point3D, Point3D, Point3D>(A, B, C);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Bezier quadraticFromPoints(Point3D p1, Point3D p2, Point3D p3, double t = 0.5)
        {
            // shortcuts, although they're really dumb
            if (t == 0) { return new Bezier(p2, p2, p3); }
            if (t == 1) { return new Bezier(p1, p2, p2); }
            // real fitting.
            var abc = getABC(2, p1, p2, p3, t);
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
        public static Bezier cubicFromPoints(Point3D S, Point3D B, Point3D E, double t = 0.5, double d1 = 0)
        {
            var abc = getABC(3, S, B, E, t);
            if (d1 == 0) { d1 = Distance(B, abc.Item3); }
            var d2 = d1 * (1 - t) / t;

            double selen = Distance(S, E);
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
            Point3D e1 = new Point3D(
                x: B.X - bx1,
                y: B.Y - by1,
                z: B.Z - bz1
                );
            Point3D e2 = new Point3D(
                x: B.X + bx2,
                y: B.Y + by2,
                z: B.Z + bz2
                );
            Point3D A = abc.Item1;
            Point3D v1 = new Point3D(
                x: A.X + (e1.X - A.X) / (1 - t),
                y: A.Y + (e1.Y - A.Y) / (1 - t),
                z: A.Z + (e1.Z - A.Z) / (1 - t)
                );
            Point3D v2 = new Point3D(
                x: A.X + (e2.X - A.X) / (t),
                y: A.Y + (e2.Y - A.Y) / (t),
                z: A.Z + (e2.Z - A.Z) / (t)
                );
            Point3D nc1 = new Point3D(
                x: S.X + (v1.X - S.X) / (t),
                y: S.Y + (v1.Y - S.Y) / (t),
                z: S.Y + (v1.Y - S.Y) / (t)
                );
            Point3D nc2 = new Point3D(
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
        public void update()
        {
            // one-time compute derivative coordinates
            dpoints = new List<Point3D>();
            var p = points;
            for (int d = p.Count(), c = d - 1; d > 1; d--, c--)
            {
                var list = new List<Point3D>();
                for (int j = 0; j < c; j++)
                {
                    Point3D dpt = new Point3D(
                         x: c * (p[j + 1].X - p[j].X),
                         y: c * (p[j + 1].Y - p[j].Y),
                         z: c * (p[j + 1].Z - p[j].Z)
                    );
                    list.Add(dpt);
                }
                dpoints.AddRange(list);
                p = list;
            };
            computedirection();
        }

        /// <summary>
        /// 
        /// </summary>
        public void computedirection()
        {
            var points = this.points;
            var angle = Utilities.angle(points[0], points[order], points[1]);
            clockwise = angle > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double length()
        {
            return Utilities.length(derivative);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        public List<Point3D> getLUT(int steps)
        {
            //steps = steps || 100;
            if (_lut.Count == steps) { return _lut; }
            _lut = new List<Point3D>();
            for (var t = 0; t <= steps; t++)
            {
                _lut.Add(compute(t / steps));
            }
            return _lut;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public double on(Point3D point, double error)
        {
            //error = error || 5;
            List<Point3D> lut = getLUT(1000);
            List<Point3D> hits = new List<Point3D>();
            Point3D c;
            double t = 0;
            for (int i = 0; i < lut.Count(); i++)
            {
                c = lut[i];
                if (Distance(c, point) < error)
                {
                    hits.Add(c);
                    t += i / lut.Count();
                }
            }
            if (hits.Count() == 0) return 0;
            return t /= hits.Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Point3D project(Point3D point)
        {
            // step 1: coarse check
            List<Point3D> LUT = getLUT(1000);
            int l = LUT.Count() - 1;

            Tuple<double, double> closest = Utilities.closest(LUT, point);
            double mdist = closest.Item1;
            double mpos = closest.Item2;
            if (mpos == 0 || mpos == l)
            {
                var t0 = mpos / l;
                Point3D pt = compute(t0);
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
                p = compute(t);
                d = Distance(point, p);
                if (d < mdist)
                {
                    mdist = d;
                    ft = t;
                }
            }
            p = compute(ft);
            //p.t = ft;
            //p.d = mdist;
            return p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        internal Point3D get(double t)
        {
            return compute(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        Point3D point(int idx)
        {
            return points[idx];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Point3D compute(double t)
        {
            // shortcuts
            if (t == 0) { return points[0]; }
            if (t == 1) { return points[order]; }

            var p = points;
            var mt = 1 - t;

            // linear?
            if (order == 1)
            {
                var ret = new Point3D(
                    x: mt * p[0].X + t * p[1].X,
                    y: mt * p[0].Y + t * p[1].Y,
                    z: mt * p[0].Z + t * p[1].Z
                );
                return ret;
            }

            // quadratic/cubic curve?
            if (order < 4)
            {
                double mt2 = mt * mt;
                double t2 = t * t;
                double a = 0;
                double b = 0;
                double c = 0;
                double d = 0;
                if (order == 2)
                {
                    p = new List<Point3D>() { points[0], points[1], points[2], Point3D.Empty };
                    a = mt2;
                    b = mt * t * 2;
                    c = t2;
                }
                else if (order == 3)
                {
                    a = mt2 * mt;
                    b = mt2 * t * 3;
                    c = mt * t2 * 3;
                    d = t * t2;
                }
                Point3D ret =
                    new Point3D(
                    x: a * p[0].X + b * p[1].X + c * p[2].X + d * p[3].X,
                    y: a * p[0].Y + b * p[1].Y + c * p[2].Y + d * p[3].Y,
                    z: a * p[0].Z + b * p[1].Z + c * p[2].Z + d * p[3].Z
                    );
                return ret;
            }

            // higher order curves: use de Casteljau's computation
            var dCpts = points;
            while (dCpts.Count > 1)
            {
                for (var i = 0; i < dCpts.Count - 1; i++)
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
        public Bezier raise()
        {
            var p = points;
            List<Point3D> np = new List<Point3D>(points.Count) { p[0] };
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
        public Point3D derivative(double t)
        {
            double mt = 1 - t;
            double a = 0;
            double b = 0;
            double c = 0;
            List<Point3D> p = new List<Point3D>(3) { dpoints[0] };
            if (order == 2)
            {
                p = new List<Point3D>() { p[0], p[1], Point3D.Empty };
                a = mt;
                b = t;
            }
            if (order == 3)
            {
                a = mt * mt;
                b = mt * t * 2;
                c = t * t;
            }
            Point3D ret = new Point3D(
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
        List<double> inflections()
        {
            return Utilities.inflections(points);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Point3D normal(double t)
        {
            return _3d ? __normal3(t) : __normal2(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Point3D __normal2(double t)
        {
            var d = derivative(t);
            var q = Sqrt(d.X * d.X + d.Y * d.Y);
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
        public Point3D __normal3(double t)
        {
            // see http://stackoverflow.com/questions/25453159
            Point3D r1 = derivative(t);
            Point3D r2 = derivative(t + 0.01);
            double q1 = Sqrt(r1.X * r1.X + r1.Y * r1.Y + r1.Z * r1.Z);
            double q2 = Sqrt(r2.X * r2.X + r2.Y * r2.Y + r2.Z * r2.Z);
            r1.X /= q1; r1.Y /= q1; r1.Z /= q1;
            r2.X /= q2; r2.Y /= q2; r2.Z /= q2;
            // cross product
            Point3D c = new Point3D(
                x: r2.Y * r1.Z - r2.Z * r1.Y,
                y: r2.Z * r1.X - r2.X * r1.Z,
                z: r2.X * r1.Y - r2.Y * r1.X
            );
            var m = Sqrt(c.X * c.X + c.Y * c.Y + c.Z * c.Z);
            c.X /= m; c.Y /= m; c.Z /= m;
            // rotation matrix
            List<double> R = new List<double>() {
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
        public List<Point3D> hull(double t)
        {
            List<Point3D> p = points;
            List<Point3D> _p = new List<Point3D>();
            Point3D pt;
            List<Point3D> q = new List<Point3D>();
            int idx = 0,
               i = 0,
               l = 0;
            q[idx++] = p[0];
            q[idx++] = p[1];
            q[idx++] = p[2];
            if (order == 3) { q[idx++] = p[3]; }
            // we lerp between all points at each iteration, until we have 1 point left.
            while (p.Count > 1)
            {
                _p = new List<Point3D>();
                for (i = 0, l = p.Count - 1; i < l; i++)
                {
                    pt = LinearInterpolate(p[i], p[i + 1],t);
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
        public Bezier split(double t1, double t2)
        {
            // shortcuts
            if (t1 == 0 && t2 != 0) { return split(t2).left; }
            if (t2 == 1) { return split(t1).right; }

            // no shortcut: use "de Casteljau" iteration.
            var q = hull(t1);
            var result = new Pair(
                left: order == 2 ? new Bezier(new List<Point3D>() { q[0], q[3], q[5] }) : new Bezier(new List<Point3D>() { q[0], q[4], q[7], q[9] }),
                right: order == 2 ? new Bezier(new List<Point3D>() { q[5], q[4], q[2] }) : new Bezier(new List<Point3D>() { q[9], q[8], q[6], q[3] }),
                span: q
            );

            // make sure we bind _t1/_t2 information!
            result.left._t1 = Utilities.map(0, 0, 1, _t1, _t2);
            result.left._t2 = Utilities.map(t1, 0, 1, _t1, _t2);
            result.right._t1 = Utilities.map(t1, 0, 1, _t1, _t2);
            result.right._t2 = Utilities.map(1, 0, 1, _t1, _t2);

            // if we have no t2, we're done
            if (t2 != 0) { return result.left; }

            // if we have a t2, split again:
            t2 = Utilities.map(t2, t1, 1, 0, 1);
            var subsplit = result.right.split(t2);
            return subsplit.left;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal Pair split(double v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<double> extrema()
        {
            List<char> dims = this.dims;
            List<double> result = new List<double>();
            List<double> roots = new List<double>();
            Point3D p;
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
        public BBox bbox()
        {
            var extrema = this.extrema();
            BBox result = new BBox(
                getminmax(this, 0, extrema),
                getminmax(this, 1, extrema),
                getminmax(this, 2, extrema)
                );
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        public bool overlaps(Bezier curve)
        {
            BBox lbbox = bbox(),
                tbbox = curve.bbox();
            return bboxoverlap(lbbox, tbbox);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public Tuple<Point3D, Point3D, Point3D> offset(double t, double d)
        {
            Point3D c = get(t);
            Point3D n = normal(t);
            Tuple<Point3D, Point3D, Point3D> ret = new Tuple<Point3D, Point3D, Point3D>(
                c,//c: 
                n,//n:
                new Point3D(c.X + n.X * d,//x: 
                c.Y + n.Y * d,//y: 
                c.Z + n.Z * d//z:
                )
            );
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<Bezier> offset(double t)
        {
            if (_linear)
            {
                var nv = normal(0);

                List<Point3D> coords = new List<Point3D>();
                foreach (Point3D p in points)
                {
                    Point3D ret = new Point3D(
                        x: p.X + t * nv.X,
                        y: p.Y + t * nv.Y,
                        z: p.Z + t * nv.Z
                    );
                    coords.Add(ret);
                }

                return new List<Bezier>() { new Bezier(coords) };
            }
            var reduced = reduce();

            return new List<Bezier>(
                from s in reduced
                select s.scale(t)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool simple()
        {
            if (order == 3)
            {
                var a1 = Utilities.angle(points[0], points[3], points[1]);
                var a2 = Utilities.angle(points[0], points[3], points[2]);
                if (a1 > 0 && a2 < 0 || a1 < 0 && a2 > 0) return false;
            }
            var n1 = normal(0);
            var n2 = normal(1);
            var s = n1.X * n2.X + n1.Y * n2.Y + n1.Z * n2.Z;
            var angle = Abs(Acos(s));
            return angle < PI / 3;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Bezier> reduce()
        {
            int i;
            double t1 = 0, t2 = 0;
            double step = 0.01;
            Bezier segment;
            List<Bezier> pass1 = new List<Bezier>();
            List<Bezier> pass2 = new List<Bezier>();
            // first pass: split on extrema
            List<double> extrema = this.extrema();
            if (extrema.IndexOf(0) == -1) { extrema.Insert(0, 0); }
            if (extrema.IndexOf(1) == -1) { extrema.Add(1); }
            for (t1 = extrema[0], i = 1; i < extrema.Count; i++)
            {
                t2 = extrema[i];
                segment = split(t1, t2);
                segment._t1 = t1;
                segment._t2 = t2;
                pass1.Add(segment);
                t1 = t2;
            }
            // second pass: further reduce these segments to simple segments
            foreach (var p1 in pass1)
            {
                t1 = 0;
                t2 = 0;
                while (t2 <= 1)
                {
                    for (t2 = t1 + step; t2 <= 1 + step; t2 += step)
                    {
                        segment = p1.split(t1, t2);
                        if (!segment.simple())
                        {
                            t2 -= step;
                            if (Abs(t1 - t2) < step)
                            {
                                // we can never form a reduction
                                return new List<Bezier>();
                            }
                            segment = p1.split(t1, t2);
                            segment._t1 = map(t1, 0, 1, p1._t1, p1._t2);
                            segment._t2 = map(t2, 0, 1, p1._t1, p1._t2);
                            pass2.Add(segment);
                            t1 = t2;
                            break;
                        }
                    }
                }
                if (t1 < 1)
                {
                    segment = p1.split(t1, 1);
                    segment._t1 = map(t1, 0, 1, p1._t1, p1._t2);
                    segment._t2 = p1._t2;
                    pass2.Add(segment);
                }
            }

            return pass2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distanceFn"></param>
        /// <returns></returns>
        public Bezier scale(DerivitiveMethodDouble distanceFn)
        {
            var order = this.order;
            if (order == 2)
            {
                return raise().scale(distanceFn);
            }

            // TODO: add special handling for degenerate (=linear) curves.
            var clockwise = this.clockwise;
            var r1 = distanceFn(0);
            var r2 = distanceFn(1);
            List<Tuple<Point3D, Point3D, Point3D>> v = new List<Tuple<Point3D, Point3D, Point3D>>() { offset(0, 10), offset(1, 10) };
            Point3D o = lli4(v[0].Item3, v[0].Item1, v[1].Item3, v[1].Item1);
            if (o == null) throw new NullReferenceException("cannot scale this curve. Try reducing it first.");
            // move all points by distance 'd' wrt the origin 'o'
            List<Point3D> points = this.points;
            List<Point3D> np = new List<Point3D>();

            // move end points by fixed distance along normal.
            foreach (var t in new List<int>() { 0, 1 })
            {
                var p = np[t * order] = copy(points[t * order]);
                p.X += (t == 0 ? r2 : r1) * v[t].Item2.X;
                p.Y += (t == 0 ? r2 : r1) * v[t].Item2.Y;
            }

            // move control points by "however much necessary to
            // ensure the correct tangent to endpoint".
            foreach (var t in new List<int>() { 0, 1 })
            {
                if (this.order == 2) break;
                var p = points[t + 1];
                Point3D ov = new Point3D(
                        x: p.X - o.X,
                        y: p.Y - o.Y,
                        z: p.Z - o.Z);
                var rc = distanceFn((t + 1) / order);
                if (!clockwise) rc = -rc;
                var m = Sqrt(ov.X * ov.X + ov.Y * ov.Y);
                ov.X /= m;
                ov.Y /= m;
                np[t + 1] = new Point3D(
                    x: p.X + rc * ov.X,
                    y: p.Y + rc * ov.Y,
                    z: p.Z + rc * ov.Z);
            }

            return new Bezier(np);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public Bezier scale(double d)
        {
            var order = this.order;
            var clockwise = this.clockwise;
            var r1 = d;
            var r2 = d;
            List<Tuple<Point3D, Point3D, Point3D>> v = new List<Tuple<Point3D, Point3D, Point3D>>() { offset(0, 10), offset(1, 10) };
            Point3D o = lli4(v[0].Item3, v[0].Item1, v[1].Item3, v[1].Item1);
            if (o == null) { throw new NullReferenceException("cannot scale this curve. Try reducing it first."); }
            // move all points by distance 'd' wrt the origin 'o'
            var points = this.points;
            List<Point3D> np = new List<Point3D>();

            // move end points by fixed distance along normal.
            foreach (var t in new List<int>() { 0, 1 })
            {
                var p = np[t * order] = copy(points[t * order]);
                p.X += (t == 0 ? r2 : r1) * v[t].Item2.X;
                p.Y += (t == 0 ? r2 : r1) * v[t].Item2.Y;
            }

            // move control points to lie on the intersection of the offset
            // derivative vector, and the origin-through-control vector
            foreach (var t in new List<int>() { 0, 1 })
            {
                if (this.order == 2) break;
                var p = np[t * order];
                var d2 = derivative(t);
                var p2 = new Point3D(x: p.X + d2.X, y: p.Y + d2.Y, z: p.Z + d2.Z);
                np[t + 1] = lli4(p, p2, o, points[t + 1]);
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
        public double linearDistanceFunction(double s, double e, double tlen, double alen, double slen)
        {
            double v = 0;
            double f1 = alen / tlen;
            double f2 = (alen + slen) / tlen;
            double d = e - s;
            return map(v, 0, 1, s + f1 * d, s + f2 * d);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1"></param>
        /// <returns></returns>
        public PolyBezier outline(double d1)
        {
            return outline(d1, d1, 0, 0, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public PolyBezier outline(double d1, double d2)
        {
            return outline(d1, d2, 0, 0, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <returns></returns>
        public PolyBezier outline(double d1, double d3, double d4)
        {
            return outline(d1, d1, d3, d4, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="graduated"></param>
        /// <returns></returns>
        public PolyBezier outline(double d1, double d2, double d3, double d4, bool graduated = false)
        {
            List<Bezier> reduced = reduce();
            int len = reduced.Count;
            List<Bezier> fcurves = new List<Bezier>();
            List<Bezier> bcurves = new List<Bezier>();
            List<Point3D> p;
            double alen = 0;
            double slen = 0;
            double tlen = length();

            // form curve oulines
            foreach (var segment in reduced)
            {
                slen = segment.length();
                if (graduated)
                {
                    fcurves.Add(segment.scale(linearDistanceFunction(d1, d3, tlen, alen, slen)));
                    bcurves.Add(segment.scale(linearDistanceFunction(-d2, -d4, tlen, alen, slen)));
                }
                else
                {
                    fcurves.Add(segment.scale(d1));
                    bcurves.Add(segment.scale(-d2));
                }
                alen += slen;
            }

            // reverse the "return" outline

            var tcurves = new List<Bezier>();
            foreach (Bezier s in bcurves)
            {
                p = s.points;
                if (p[3] != null)
                {
                    s.points = new List<Point3D>() { p[3], p[2], p[1], p[0] };
                }
                else
                {
                    s.points = new List<Point3D>() { p[2], p[1], p[0] };
                }
                tcurves.Add(s);
            }
            tcurves.Reverse();
            bcurves = tcurves;

            // form the endcaps as lines
            Point3D fs = fcurves[0].points[0];
            Point3D fe = fcurves[len - 1].points[fcurves[len - 1].points.Count - 1];
            Point3D bs = bcurves[len - 1].points[bcurves[len - 1].points.Count - 1];
            Point3D be = bcurves[0].points[0];
            Bezier ls = makeline(bs, fs);
            Bezier le = makeline(fe, be);
            List<Bezier> segments = new List<Bezier>();
            segments.Add(ls);
            segments.AddRange(fcurves);
            segments.Add(le);
            segments.AddRange(bcurves);
            slen = segments.Count;

            return new PolyBezier(segments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public List<Shape1> outlineshapes(double d1, double d2)
        {
            //d2 = d2 || d1;
            List<Bezier> outline = this.outline(d1, d2).curves;
            List<Shape1> shapes = new List<Shape1>();
            for (int i = 1, len = outline.Count(); i < len / 2; i++)
            {
                var shape = makeshape(outline[i], outline[len - i]);
                shape.startcap._virtual = (i > 1);
                shape.endcap._virtual = (i < len / 2 - 1);
                shapes.Add(shape);
            }
            return shapes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Pair> intersects()
        {
            return selfintersects();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        public List<bool> intersects(Line1 curve)
        {
            return lineIntersects(curve);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        public List<Pair> intersects(Bezier curve)
        {
            return curveintersects(reduce(), curve.reduce());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public List<bool> lineIntersects(Line1 line)
        {
            double mx = Min(line.P1.X, line.P2.X),
                my = Min(line.P1.Y, line.P2.Y),
                MX = Max(line.P1.X, line.P2.X),
                MY = Max(line.P1.Y, line.P2.Y);
            Bezier self = this;

            return new List<bool>(
                from t in roots(points, line)
                let p = self.get(t)

                select between(p.X, mx, MX) && between(p.Y, my, MY));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Pair> selfintersects()
        {
            List<Bezier> reduced = reduce();
            // "simple" curves cannot intersect with their direct
            // neighbour, so for each segment X we check whether
            // it intersects [0:x-2][x+2:last].
            int i, len = reduced.Count - 2;
            List<Pair> results = new List<Pair>();
            List<Pair> result;
            List<Bezier> left, right;
            for (i = 0; i < len; i++)
            {
                left = reduced.GetRange(i, i + 1);
                right = reduced.GetRange(i + 2, reduced.Count - (i + 2));
                result = curveintersects(left, right);
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
        List<Pair> curveintersects(List<Bezier> c1, List<Bezier> c2)
        {
            List<Pair> pairs = new List<Pair>();
            // step 1: pair off any overlapping segments
            foreach (var l in c1)
            {
                foreach (var r in c2)
                {
                    if (l.overlaps(r))
                    {
                        pairs.Add(new Pair(left: l, right: r));
                    }
                }
            }

            // step 2: for each pairing, run through the convergence algorithm.
            var intersections = new List<Pair>();
            foreach (var pair in pairs)
            {
                var result = pairiteration(pair.left, pair.right);
                if (result.Count() > 0)
                {
                    intersections.AddRange(result);
                }
            };
            return intersections;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorThreshold"></param>
        /// <returns></returns>
        public List<Arc1> arcs(double errorThreshold = 0.5)
        {
            //errorThreshold = errorThreshold || 0.5;
            List<Arc1> circles = new List<Arc1>();
            return _iterate(errorThreshold, circles);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="np1"></param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public double _error(Arc1 pc, Point3D np1, double s, double e)
        {
            double q = (e - s) / 4;
            Point3D c1 = get(s + q);
            Point3D c2 = get(e - q);
            double reff = Distance(pc.center, np1);
            double d1 = Distance(pc.center, c1);
            double d2 = Distance(pc.center, c2);
            return Abs(d1 - reff) + Abs(d2 - reff);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorThreshold"></param>
        /// <param name="circles"></param>
        /// <returns></returns>
        public List<Arc1> _iterate(double errorThreshold, List<Arc1> circles)
        {
            double s = 0, e = 1, safety;
            // we do a binary search to find the "good `t` closest to no-longer-good"
            do
            {
                safety = 0;

                // step 1: start with the maximum possible arc
                e = 1;

                // points:
                Point3D np1 = get(s), np2, np3;
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

                    np2 = get(m);
                    np3 = get(e);

                    arc = getccenter(np1, np2, np3);
                    var error = _error(arc, np1, s, e);
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
                        e = e + (e - s) / 2;
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

                prev_arc = (prev_arc != null ? prev_arc : arc);
                circles.Add(prev_arc);
                s = prev_e;
            }
            while (e < 1);
            return circles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public List<double> CubicBezierCardanoIntersection(Line1 line)
        {
            return CubicBezierCardanoIntersection(points[0], points[1], points[2], points[3], line);
        }

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
            List<Point3D> aligned = align(new List<Point3D>() { p1, p2, p3, p4 }, line);
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
                x2 = t1 * Cos((phi + Maths.Tau) / 3) - a / 3;
                x3 = t1 * Cos((phi + 2 * Maths.Tau) / 3) - a / 3;
                return new List<double>() { x1, x2, x3 };
            }
            else if (discriminant == 0)
            {
                u1 = q2 < 0 ? Maths.Crt(-q2) : -Maths.Crt(q2);
                x1 = 2 * u1 - a / 3;
                x2 = -u1 - a / 3;
                return new List<double>() { x1, x2 };
            }
            else
            {
                // one real root, and two imaginary roots
                double sd = Sqrt(discriminant);
                double tt = -q2 + sd;
                u1 = Maths.Crt(-q2 + sd);
                v1 = Maths.Crt(q2 + sd);
                x1 = u1 - v1 - a / 3;
                return new List<double>() { x1 };
            }
        }
    }
}
