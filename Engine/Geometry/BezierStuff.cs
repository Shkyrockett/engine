using Engine.Geometry;
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
    public class Bezier
    {
        List<Point2D> points;

        List<Point2D> dpoints;

        int order;

        bool clockwise;

        double _t1;

        double _t2;

        bool _3d;

        bool _linear;

        List<Point2D> _lut;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        public Bezier(Point2D p1, Point2D p2, Point2D p3)
        {
            points = new List<Point2D>();
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            order = points.Count - 1;
            _t1 = 0;
            _t2 = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        public Bezier(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
        {
            points = new List<Point2D>();
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            order = points.Count - 1;
        }

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
            points = new List<Point2D>();
            points.Add(new Point2D(x1,y1));
            points.Add(new Point2D(v1,v2));
            points.Add(new Point2D(v3,v4));
            points.Add(new Point2D(x2, y2));
            order = points.Count - 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Bezier(List<Point2D> points)
        {
            this.points = points;
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
        public static List<Point2D> getABC(double n, Point2D S, Point2D B, Point2D E, double t = 0.5)
        {
            double u = Utils.projectionratio(t, n);
            double um = 1 - u;
            Point2D C = new Point2D(u * S.X + um * E.X, u * S.Y + um * E.Y);
            double s = Utils.abcratio(t, n);
            Point2D A = new Point2D(B.X + (B.X - C.X) / s, B.Y + (B.Y - C.Y) / s);
            return new List<Point2D>() { A, B, C };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Bezier quadraticFromPoints(Point2D p1, Point2D p2, Point2D p3, double t = 0.5)
        {
            // shortcuts, although they're really dumb
            if (t == 0) { return new Bezier(p2, p2, p3); }
            if (t == 1) { return new Bezier(p1, p2, p2); }
            // real fitting.
            List<Point2D> abc = getABC(2, p1, p2, p3, t);
            return new Bezier(p1, abc[0], p3);
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
        public static Bezier cubicFromPoints(Point2D S, Point2D B, Point2D E, double t = 0.5, double d1 = double.NaN)
        {
            List<Point2D> abc = getABC(3, S, B, E, t);
            if (d1 == double.NaN) { d1 = Utils.dist(B, abc[2]); }
            double d2 = d1 * (1 - t) / t;

            double selen = Utils.dist(S, E);
            double lx = (E.X - S.X) / selen;
            double ly = (E.Y - S.Y) / selen;
            double bx1 = d1 * lx;
            double by1 = d1 * ly;
            double bx2 = d2 * lx;
            double by2 = d2 * ly;
            // derivation of new hull coordinates
            Point2D e1 = new Point2D(B.X - bx1, B.Y - by1);
            Point2D e2 = new Point2D(B.X + bx2, B.Y + by2);
            Point2D A = abc[0];
            Point2D v1 = new Point2D(A.X + (e1.X - A.X) / (1 - t), A.Y + (e1.Y - A.Y) / (1 - t));
            Point2D v2 = new Point2D(A.X + (e2.X - A.X) / (t), A.Y + (e2.Y - A.Y) / (t));
            Point2D nc1 = new Point2D(S.X + (v1.X - S.X) / (t), y: S.Y + (v1.Y - S.Y) / (t));
            Point2D nc2 = new Point2D(E.X + (v2.X - E.X) / (1 - t), y: E.Y + (v2.Y - E.Y) / (1 - t));
            // ...done
            return new Bezier(S, nc1, nc2, E);
        }

        /// <summary>
        /// 
        /// </summary>
        public void update()
        {
            // one-time compute derivative coordinates
            this.dpoints = new List<Point2D>();
            List<Point2D> p = points;
            for (int d = points.Count, c = d - 1; d > 1; d--, c--)
            {
                List<Point2D> list = new List<Point2D>();
                for (int j = 0; j < c; j++)
                {
                    Point2D dpt = new Point2D(c * (p[j + 1].X - p[j].X), c * (p[j + 1].Y - p[j].Y));
                    //if (this._3d)dpt.Z = c * (p[j + 1].Z - p[j].Z);
                    list.Add(dpt);
                }
                dpoints.AddRange(list);
                p = list;
            }
            computedirection();
        }

        /// <summary>
        /// 
        /// </summary>
        public void computedirection()
        {
            List<Point2D> points = this.points;
            double angle = Utils.angle(points[0], points[order], points[1]);
            clockwise = angle > 0;
        }

        public double length()
        {
            return Utils.length(this.derivative(this));
        }

        public List<Point2D> getLUT(int steps = 0)
        {
            steps = steps | 100;
            if (_lut.Count == steps) { return _lut; }
            _lut = new List<Point2D>();
            for (var t = 0; t <= steps; t++)
            {
                _lut.Add(this.compute(t / steps));
            }
            return this._lut;
        }

        public double on(Point2D point, int error)
        {
            error = error | 5;
            var lut = this.getLUT();
            List<Point2D> hits = new List<Point2D>();
            Point2D c;
            double t = 0;
            for (var i = 0; i < lut.Count; i++)
            {
                c = lut[i];
                if (Utils.dist(c, point) < error)
                {
                    hits.Add(c);
                    t += i / lut.Count;
                }
            }
            if (hits.Count != 0) return 0;
            return t /= hits.Count;
        }

        public double project(Point2D point)
        {
            // step 1: coarse check
            List<Point2D> LUT = this.getLUT();
            int l = LUT.Count - 1;
            Point2D closest = Utils.closest(LUT, point);
            double mdist = closest.X;
            double mpos = closest.Y;
            if (mpos == 0 || mpos == l)
            {
                double t = mpos / l;
                Point2D pt = this.compute(t);
                pt.t = t;
                pt.d = mdist;
                return pt;
            }

            // step 2: fine check
            double ft, t, p, d,
                t1 = (mpos - 1) / l,
                t2 = (mpos + 1) / l,
                step = 0.1 / l;
            mdist += 1;
            for (t = t1, ft = t; t < t2 + step; t += step)
            {
                p = this.compute(t);
                d = Utils.dist(point, p);
                if (d < mdist)
                {
                    mdist = d;
                    ft = t;
                }
            }
            p = this.compute(ft);
            p.t = ft;
            p.d = mdist;
            return p;
        }

        public Point2D get(double t)
        {
            return this.compute(t);
        }

        public Point2D point(int idx)
        {
            return this.points[idx];
        }

        public Point2D compute(double t)
        {
            // shortcuts
            if (t == 0) { return points[0]; }
            if (t == 1) { return points[order]; }

            List<Point2D> p = points;
            double mt = 1 - t;

            // linear?
            if (order == 1)
            {
                Point2D ret = new Point2D(mt * p[0].X + t * p[1].X, mt * p[0].Y + t * p[1].Y);
                //if (this._3d) { ret.Z = mt * p[0].Z + t * p[1].Z; }
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
                    p = new List<Point2D>() { p[0], p[1], p[2] };
                    a = mt2;
                    b = mt * t * 2;
                    c = t2;
                }
                else if (this.order == 3)
                {
                    a = mt2 * mt;
                    b = mt2 * t * 3;
                    c = mt * t2 * 3;
                    d = t * t2;
                }
                Point2D ret = new Point2D(a * p[0].X + b * p[1].X + c * p[2].X + d * p[3].X, a * p[0].y + b * p[1].Y + c * p[2].Y + d * p[3].Y);
                //if (this._3d)ret.Z = a * p[0].Z + b * p[1].Z + c * p[2].Z + d * p[3].Z;
                return ret;
            }

            // higher order curves: use de Casteljau's computation
            var dCpts = JSON.parse(JSON.stringify(this.points));
            while (dCpts.length > 1)
            {
                for (var i = 0;

                i < dCpts.length - 1; i++)
                {
                    dCpts[i] = {
                        x: dCpts[i].x + (dCpts[i + 1].x - dCpts[i].x) * t,
              y: dCpts[i].y + (dCpts[i + 1].y - dCpts[i].y) * t
              };
                    if (typeof dCpts[i].z !== "undefined")
                    {
                        dCpts[i] = dCpts[i].z + (dCpts[i + 1].z - dCpts[i].z) * t
                    }
                }
                dCpts.splice(dCpts.length - 1, 1);
            }
            return dCpts[0];
        }

        public Bezier raise()
        {
            List<Point2D> p = this.points;
            List<Point2D> np = new List<Point2D>() { p[0] };
            int k = p.Count;
            Point2D pi;
            Point2D pim;
            for (int i = 1; i < k; i++)
            {
                pi = p[i];
                pim = p[i - 1];
                np.Add(new Point2D((k - i) / k * pi.X + i / k * pim.X, (k - i) / k * pi.Y + i / k * pim.Y));
            }
            np[k] = p[k - 1];
            return new Bezier(np);
        }

        public Point2D derivative(double t)
        {
            var mt = 1 - t;
            double a = 0;
            double b = 0;
            double c = 0;
            List<Point2D> p = this.dpoints;
            if (this.order == 2)
            {
                p = new List<Point2D>() { p[0], p[1], };
                a = mt;
                b = t;
            }
            if (this.order == 3)
            {
                a = mt * mt;
                b = mt * t * 2;
                c = t * t;
            }
            Point2D ret = new Point2D(a * p[0].X + b * p[1].X + c * p[2].X, a * p[0].Y + b * p[1].Y + c * p[2].Y);
            //if (this._3d) ret.Z = a * p[0].Z + b * p[1].Z + c * p[2].Z;
            return ret;
        }

        public List<Point2D> inflections()
        {
            return Utils.inflections(this.points);
        }

        public Point2D normal(double t)
        {
            return this._3d ? this.__normal3(t) : this.__normal2(t);
        }

        public Point2D __normal2(double t)
        {
            var d = this.derivative(t);
            var q = Math.Sqrt(d.X * d.X + d.Y * d.Y);
            return new Point2D(x: -d.Y / q, y: d.X / q);
        }

        public Point2D __normal3(double t)
        {
            // see http://stackoverflow.com/questions/25453159
            Point2D r1 = this.derivative(t);
            Point2D r2 = this.derivative(t + 0.01);
            double q1 = Math.Sqrt(r1.X * r1.X + r1.Y * r1.Y + r1.Z * r1.Z);
            double q2 = Math.Sqrt(r2.X * r2.X + r2.Y * r2.Y + r2.Z * r2.ZS);
            r1.X /= q1;
            r1.Y /= q1;
            r1.Z /= q1;
            r2.X /= q2;
            r2.Y /= q2;
            r2.Z /= q2;
            // cross product
            var c = new Point2D(
                x: r2.Y * r1.Z - r2.Z * r1.Y,
                y: r2.Z * r1.X - r2.X * r1.Z,
                z: r2.X * r1.Y - r2.Y * r1.X
              );
            var m = Math.Sqrt(c.X * c.X + c.Y * c.Y + c.Z * c.Z);
            c.X /= m;
            c.Y /= m;
            c.Z /= m;
            // rotation matrix
            var R = new List<double> {c.X * c.X, c.X * c.Y - c.Z, c.X * c.Z + c.Y,
                      c.X * c.Y + c.Z, c.Y * c.Y, c.Y * c.Z - c.X,
                      c.X * c.Z - c.Y, c.Y * c.Z + c.X, c.Z * c.Z};
            // normal vector:
            Point2D n = new Point2D(
                x: R[0] * r1.X + R[1] * r1.Y + R[2] * r1.Z,
                y: R[3] * r1.X + R[4] * r1.Y + R[5] * r1.Z,
                z: R[6] * r1.X + R[7] * r1.Y + R[8] * r1.Z
            );
            return n;
        }

        public List<Point2D> hull(t)
        {
            var p = this.points,
                _p = [],
                pt,
                q = [],
                idx = 0,
                i = 0,
                l = 0;
            q[idx++] = p[0];
            q[idx++] = p[1];
            q[idx++] = p[2];
            if (this.order == 3) { q[idx++] = p[3]; }
            // we lerp between all points at each iteration, until we have 1 point left.
            while (p.length > 1)
            {
                _p = [];
                for (i = 0, l = p.length - 1; i < l; i++)
                {
                    pt = Utils.lerp(t, p[i], p[i + 1]);
                    q[idx++] = pt;
                    _p.push(pt);
                }
                p = _p;
            }
            return q;
        }

        split: function(t1, t2)
        {
            // shortcuts
            if (t1 == 0 && !!t2) { return this.split(t2).left; }
            if (t2 == 1) { return this.split(t1).right; }

            // no shortcut: use "de Casteljau" iteration.
            var q = this.hull(t1);
            var result = {
        left: this.order == 2 ? new Bezier([q[0], q[3], q[5]]) : new Bezier([q[0], q[4], q[7], q[9]]),
        right: this.order == 2 ? new Bezier([q[5], q[4], q[2]]) : new Bezier([q[9], q[8], q[6], q[3]]),
        span: q
              };

        // make sure we bind _t1/_t2 information!
        result.left._t1  = utils.map(0,  0,1, this._t1,this._t2);
    result.left._t2  = utils.map(t1, 0,1, this._t1,this._t2);
    result.right._t1 = utils.map(t1, 0,1, this._t1,this._t2);
    result.right._t2 = utils.map(1,  0,1, this._t1,this._t2);

      // if we have no t2, we're done
      if(!t2) { return result; }

    // if we have a t2, split again:
    t2 = utils.map(t2,t1,1,0,1);
    var subsplit = result.right.split(t2);
      return subsplit.left;
    }

extrema: function()
{
    var dims = this.dims,
        result = { },
        roots =[],
        p, mfn;
    dims.forEach(function(dim) {
        mfn = function(v) { return v[dim]; };
        p = this.dpoints[0].map(mfn);
        result[dim] = utils.droots(p);
        if (this.order == 3)
        {
            p = this.dpoints[1].map(mfn);
            result[dim] = result[dim].concat(utils.droots(p));
        }
        result[dim] = result[dim].filter(function(t) { return (t >= 0 && t <= 1); });
        roots = roots.concat(result[dim].sort());
    }.bind(this));
    roots.sort();
    result.values = roots;
    return result;
}

bbox: function()
{
    var extrema = this.extrema(), result = { };
    this.dims.forEach(function(d) {
        result[d] = utils.getminmax(this, d, extrema[d]);
    }.bind(this));
    return result;
}

overlaps: function(curve)
{
    var lbbox = this.bbox(),
        tbbox = curve.bbox();
    return utils.bboxoverlap(lbbox, tbbox);
}

offset: function(t, d)
{
    if (typeof d !== "undefined")
    {
        var c = this.get(t);
        var n = this.normal(t);
        var ret = {
          c: c,
          n: n,
          x: c.x + n.x * d,
          y: c.y + n.y * d
            };
    if (this._3d)
    {
        ret.z = c.z + n.z * d;
    };
    return ret;
}
      if(this._linear) {
    var nv = this.normal(0);
    var coords = this.points.map(function(p) {
        var ret = {
            x: p.x + t * nv.x,
            y: p.y + t * nv.y
              };
    if (p.z && n.z) { ret.z = p.z + t * nv.z; }
    return ret;
});
        return [new Bezier(coords)];
}
var reduced = this.reduce();
      return reduced.map(function(s)
{
    return s.scale(t);
});
    },
    simple: function()
{
    if (this.order == 3)
    {
        var a1 = utils.angle(this.points[0], this.points[3], this.points[1]);
        var a2 = utils.angle(this.points[0], this.points[3], this.points[2]);
        if (a1 > 0 && a2 < 0 || a1 < 0 && a2 > 0) return false;
    }
    var n1 = this.normal(0);
    var n2 = this.normal(1);
    var s = n1.x * n2.x + n1.y * n2.y;
    if (this._3d) { s += n1.z * n2.z; }
    var angle = abs(acos(s));
    return angle < pi / 3;
}

reduce: function()
{
    var i, t1 = 0, t2 = 0, step = 0.01, segment, pass1 =[], pass2 =[];
    // first pass: split on extrema
    var extrema = this.extrema().values;
    if (extrema.indexOf(0) == -1) { extrema = [0].concat(extrema); }
    if (extrema.indexOf(1) == -1) { extrema.push(1); }
    for (t1 = extrema[0], i = 1; i < extrema.length; i++)
    {
        t2 = extrema[i];
        segment = this.split(t1, t2);
        segment._t1 = t1;
        segment._t2 = t2;
        pass1.push(segment);
        t1 = t2;
    }
    // second pass: further reduce these segments to simple segments
    pass1.forEach(function(p1) {
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
                    if (abs(t1 - t2) < step)
                    {
                        // we can never form a reduction
                        return [];
                    }
                    segment = p1.split(t1, t2);
                    segment._t1 = utils.map(t1, 0, 1, p1._t1, p1._t2);
                    segment._t2 = utils.map(t2, 0, 1, p1._t1, p1._t2);
                    pass2.push(segment);
                    t1 = t2;
                    break;
                }
            }
        }
        if (t1 < 1)
        {
            segment = p1.split(t1, 1);
            segment._t1 = utils.map(t1, 0, 1, p1._t1, p1._t2);
            segment._t2 = p1._t2;
            pass2.push(segment);
        }
    });
    return pass2;
}

scale: function(d)
{
    var order = this.order;
    var distanceFn = false
      if (typeof d == "function") { distanceFn = d; }
    if (distanceFn && order == 2) { return this.raise().scale(distanceFn); }

    // TODO: add special handling for degenerate (=linear) curves.
    var clockwise = this.clockwise;
    var r1 = distanceFn ? distanceFn(0) : d;
    var r2 = distanceFn ? distanceFn(1) : d;
    var v = [this.offset(0, 10), this.offset(1, 10)];
    var o = utils.lli4(v[0], v[0].c, v[1], v[1].c);
    if (!o) { throw new Error("cannot scale this curve. Try reducing it first."); }
    // move all points by distance 'd' wrt the origin 'o'
    var points = this.points, np =[];

      // move end points by fixed distance along normal.
[0,1].forEach(function(t)
{
    var p = np[t * order] = utils.copy(points[t * order]);
    p.x += (t ? r2 : r1) * v[t].n.x;
    p.y += (t ? r2 : r1) * v[t].n.y;
}.bind(this));

      if (!distanceFn) {
        // move control points to lie on the intersection of the offset
        // derivative vector, and the origin-through-control vector
        [0,1].forEach(function(t)
{
    if (this.order == 2 && !!t) return;
    var p = np[t * order];
    var d = this.derivative(t);
    var p2 = { x: p.x + d.x, y: p.y + d.y };
np[t + 1] = utils.lli4(p, p2, o, points[t + 1]);
        }.bind(this));
        return new Bezier(np);
      }

      // move control points by "however much necessary to
      // ensure the correct tangent to endpoint".
      [0,1].forEach(function(t)
{
    if (this.order == 2 && !!t) return;
    var p = points[t + 1];
    var ov = {
          x: p.x - o.x,
          y: p.y - o.y
        };
var rc = distanceFn ? distanceFn((t + 1) / order) : d;
        if(distanceFn && !clockwise) rc = -rc;
        var m = sqrt(ov.x * ov.x + ov.y * ov.y);
ov.x /= m;
        ov.y /= m;
        np[t + 1] = {
          x: p.x + rc* ov.x,
          y: p.y + rc* ov.y
        }
      }.bind(this));
      return new Bezier(np);
    },
    outline: function(d1, d2, d3, d4)
{
    d2 = (typeof d2 == "undefined") ? d1 : d2;
    var reduced = this.reduce(),
        len = reduced.length,
        fcurves = [],
        bcurves = [],
        p,
        alen = 0,
        tlen = this.length();

    var graduated = (typeof d3 !== "undefined" && typeof d4 !== "undefined");

    function linearDistanceFunction(s, e, tlen, alen, slen) {
        return function(v) {
            var f1 = alen / tlen, f2 = (alen + slen) / tlen, d = e - s;
            return utils.map(v, 0, 1, s + f1 * d, s + f2 * d);
        };
    };

    // form curve oulines
    reduced.forEach(function(segment) {
        slen = segment.length();
        if (graduated)
        {
            fcurves.push(segment.scale(linearDistanceFunction(d1, d3, tlen, alen, slen)));
            bcurves.push(segment.scale(linearDistanceFunction(-d2, -d4, tlen, alen, slen)));
        }
        else
        {
            fcurves.push(segment.scale(d1));
            bcurves.push(segment.scale(-d2));
        }
        alen += slen;
    });

    // reverse the "return" outline
    bcurves = bcurves.map(function(s) {
        p = s.points;
        if (p[3]) { s.points = [p[3], p[2], p[1], p[0]]; }
        else { s.points = [p[2], p[1], p[0]]; }
        return s;
    }).reverse();

    // form the endcaps as lines
    var fs = fcurves[0].points[0],
        fe = fcurves[len - 1].points[fcurves[len - 1].points.length - 1],
        bs = bcurves[len - 1].points[bcurves[len - 1].points.length - 1],
        be = bcurves[0].points[0],
        ls = utils.makeline(bs, fs),
        le = utils.makeline(fe, be),
        segments = [ls].concat(fcurves).concat([le]).concat(bcurves),
          slen = segments.length;

    return new PolyBezier(segments);
}

outlineshapes: function(d1, d2)
{
    d2 = d2 || d1;
    var outline = this.outline(d1, d2).curves;
    var shapes = [];
    for (var i = 1, len = outline.length; i < len / 2; i++)
    {
        var shape = utils.makeshape(outline[i], outline[len - i]);
        shape.startcap.virtual = (i > 1);
        shape.endcap.virtual = (i < len / 2 - 1);
        shapes.push(shape);
    }
    return shapes;
}

intersects: function(curve)
{
    if (!curve) return this.selfintersects();
    if (curve.p1 && curve.p2)
    {
        return this.lineIntersects(curve);
    }
    if (curve instanceof Bezier) { curve = curve.reduce(); }
    return this.curveintersects(this.reduce(), curve);
}

lineIntersects: function(line)
{
    var mx = min(line.p1.x, line.p2.x),
        my = min(line.p1.y, line.p2.y),
        MX = max(line.p1.x, line.p2.x),
        MY = max(line.p1.y, line.p2.y),
        self = this;
    return utils.roots(this.points, line).filter(function(t) {
        var p = self.get(t);
        return utils.between(p.x, mx, MX) && utils.between(p.y, my, MY);
    });
}

selfintersects: function()
{
    var reduced = this.reduce();
    // "simple" curves cannot intersect with their direct
    // neighbour, so for each segment X we check whether
    // it intersects [0:x-2][x+2:last].
    var i, len = reduced.length - 2, results =[], result, left, right;
    for (i = 0; i < len; i++)
    {
        left = reduced.slice(i, i + 1);
        right = reduced.slice(i + 2);
        result = this.curveintersects(left, right);
        results = results.concat(result);
    }
    return results;
}

curveintersects: function(c1, c2)
{
    var pairs = [];
    // step 1: pair off any overlapping segments
    c1.forEach(function(l) {
        c2.forEach(function(r) {
            if (l.overlaps(r))
            {
                pairs.push({ left: l, right: r });
            }
        });
    });
    // step 2: for each pairing, run through the convergence algorithm.
    var intersections = [];
    pairs.forEach(function(pair) {
        var result = utils.pairiteration(pair.left, pair.right);
        if (result.length > 0)
        {
            intersections = intersections.concat(result);
        }
    });
    return intersections;
}

arcs: function(errorThreshold)
{
    errorThreshold = errorThreshold || 0.5;
    var circles = [];
    return this._iterate(errorThreshold, circles);
}

_error: function(pc, np1, s, e)
{
    var q = (e - s) / 4,
        c1 = this.get(s + q),
        c2 = this.get(e - q),
          ref = utils.dist(pc, np1),
          d1 = utils.dist(pc, c1),
          d2 = utils.dist(pc, c2);
    return abs(d1 -ref) + abs(d2 -ref);
}

_iterate: function(errorThreshold, circles)
{
    var s = 0, e = 1, safety;
    // we do a binary search to find the "good `t` closest to no-longer-good"
    do
    {
        safety = 0;

        // step 1: start with the maximum possible arc
        e = 1;

        // points:
        var np1 = this.get(s), np2, np3, arc, prev_arc;

        // booleans:
        var curr_good = false, prev_good = false, done;

        // numbers:
        var m = e, prev_e = 1, step = 0;

        // step 2: find the best possible arc
        do
        {
            prev_good = curr_good;
            prev_arc = arc;
            m = (s + e) / 2;
            step++;

            np2 = this.get(m);
            np3 = this.get(e);

            arc = utils.getccenter(np1, np2, np3);
            var error = this._error(arc, np1, s, e);
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
            console.error("arc abstraction somehow failed...");
            break;
        }

        // console.log("[F] arc found", s, prev_e, prev_arc.x, prev_arc.y, prev_arc.s, prev_arc.e);

        prev_arc = (prev_arc ? prev_arc : arc);
        circles.push(prev_arc);
        s = prev_e;
    }
    while (e < 1);
    return circles;
}
  }










    }
}
