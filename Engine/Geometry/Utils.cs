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
    public class Utils
    {
        // trig constants
        public const double pi = Math.PI;
        public const double tau = 2d * pi;
        public const double quart = pi / 2d;
        // float precision significant decimal
        public const double epsilon = 0.000001d;

        // Legendre-Gauss abscissae with n=24 (x_i values, defined at i=n as the roots of the nth order Legendre polynomial Pn(x))
        public static readonly List<double> Tvalues = new List<double>() {
          -0.0640568928626056260850430826247450385909,
           0.0640568928626056260850430826247450385909,
          -0.1911188674736163091586398207570696318404,
           0.1911188674736163091586398207570696318404,
          -0.3150426796961633743867932913198102407864,
           0.3150426796961633743867932913198102407864,
          -0.4337935076260451384870842319133497124524,
           0.4337935076260451384870842319133497124524,
          -0.5454214713888395356583756172183723700107,
           0.5454214713888395356583756172183723700107,
          -0.6480936519369755692524957869107476266696,
           0.6480936519369755692524957869107476266696,
          -0.7401241915785543642438281030999784255232,
           0.7401241915785543642438281030999784255232,
          -0.8200019859739029219539498726697452080761,
           0.8200019859739029219539498726697452080761,
          -0.8864155270044010342131543419821967550873,
           0.8864155270044010342131543419821967550873,
          -0.9382745520027327585236490017087214496548,
           0.9382745520027327585236490017087214496548,
          -0.9747285559713094981983919930081690617411,
           0.9747285559713094981983919930081690617411,
          -0.9951872199970213601799974097007368118745,
           0.9951872199970213601799974097007368118745
        };

        // Legendre-Gauss weights with n=24 (w_i values, defined by a function linked to in the Bezier primer article)
        public static readonly List<double> Cvalues = new List<double>() {
          0.1279381953467521569740561652246953718517,
          0.1279381953467521569740561652246953718517,
          0.1258374563468282961213753825111836887264,
          0.1258374563468282961213753825111836887264,
          0.1216704729278033912044631534762624256070,
          0.1216704729278033912044631534762624256070,
          0.1155056680537256013533444839067835598622,
          0.1155056680537256013533444839067835598622,
          0.1074442701159656347825773424466062227946,
          0.1074442701159656347825773424466062227946,
          0.0976186521041138882698806644642471544279,
          0.0976186521041138882698806644642471544279,
          0.0861901615319532759171852029837426671850,
          0.0861901615319532759171852029837426671850,
          0.0733464814110803057340336152531165181193,
          0.0733464814110803057340336152531165181193,
          0.0592985849154367807463677585001085845412,
          0.0592985849154367807463677585001085845412,
          0.0442774388174198061686027482113382288593,
          0.0442774388174198061686027482113382288593,
          0.0285313886289336631813078159518782864491,
          0.0285313886289336631813078159518782864491,
          0.0123412297999871995468056670700372915759,
          0.0123412297999871995468056670700372915759
        };

        public delegate Point2D DerivitiveMethod(double x);

        public static double arcfn(double t, DerivitiveMethod derivativeFn)
        {
            var d = derivativeFn(t);
            var l = d.X * d.X + d.Y * d.Y;
            //if (typeof d.Z != "undefined")l += d.Z * d.Z;
            return Math.Sqrt(l);
        }

        public static bool between(double v, double m, double M)
        {
            return (m <= v && v <= M) || Utils.approximately(v, m) || Utils.approximately(v, M);
        }

        public static bool approximately(double a, double b, double precision = epsilon)
        {
            return Math.Abs(a - b) <= (precision || epsilon);
        }

        public static double length(DerivitiveMethod derivativeFn)
        {
            double z = 0.5, sum = 0, len = Utils.Tvalues.Count, t;
            for (int i = 0; i < len; i++)
            {
                t = z * Utils.Tvalues[i] + z;
                sum += Utils.Cvalues[i] * Utils.arcfn(t, derivativeFn);
            }

            return z * sum;
        }

        public static double map(double v, double ds, double de, double ts, double te)
        {
            double d1 = de - ds;
            double d2 = te - ts;
            double v2 = v - ds;
            double r = v2 / d1;
            return ts + d2 * r;
        }

        public static Point2D lerp(double r, Point2D v1, Point2D v2)
        {
            Point2D ret = new Point2D(v1.X + r * (v2.X - v1.X), v1.Y + r * (v2.Y - v1.Y));
            //if(!!v1.Z && !!v2.Z) ret.Z =  v1.Z + r*(v2.Z-v1.Z);
            return ret;
        }

        public static string pointToString(Point2D p)
        {
            return p.ToString();
        }

        public static string pointsToString(List<Point2D> points)
        {
            return points.ToString();
        }

        public static object copy(object obj)
        {
            return obj;
        }

        public static double angle(Point2D o, Point2D v1, Point2D v2)
        {
            double dx1 = v1.X - o.X;
            double dy1 = v1.Y - o.Y;
            double dx2 = v2.X - o.X;
            double dy2 = v2.Y - o.Y;
            double cross = dx1 * dy2 - dy1 * dx2;
            double m1 = Math.Sqrt(dx1 * dx1 + dy1 * dy1);
            double m2 = Math.Sqrt(dx2 * dx2 + dy2 * dy2);
            dx1 /= m1;
            dy1 /= m1;
            dx2 /= m2;
            dy2 /= m2;
            double dot = dx1 * dx2 + dy1 * dy2;
            return Math.Atan2(cross, dot);
        }

        // round as string, to avoid rounding errors
        public static double round(double v, int d)
        {
            return Math.Round(v, d, MidpointRounding.AwayFromZero);
        }

        public static double dist(Point2D p1, Point2D p2)
        {
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static Point2D closest(List<Point2D> LUT, Point2D point)
        {
            double mdist = Math.Pow(2, 63);
            double mpos = 0;
            double d;
            LUT.ForEach((Point2D p, double idx)
            {
                d = Utils.dist(point, p);
                if (d < mdist)
                {
                    mdist = d;
                    mpos = idx;
                }
            });
            return new Point2D(mdist, mpos);
        }

        public static double abcratio(double t, double n = 0.5)
        {
            // see ratio(t) note on http://pomax.github.io/bezierinfo/#abc
            if (n != 2 && n != 3)
            {
                return double.NaN;
            }
            if (t == 0 || t == 1)
            {
                return t;
            }
            double bottom = Math.Pow(t, n) + Math.Pow(1 - t, n);
            double top = bottom - 1;
            return Math.Abs(top / bottom);
        }

        public static double projectionratio(double t, double n = 0.5)
        {
            // see u(t) note on http://pomax.github.io/bezierinfo/#abc
            if (n != 2 && n != 3)
            {
                return double.NaN;
            }
            if (t == 0 || t == 1)
            {
                return t;
            }
            double top = Math.Pow(1 - t, n);
            double bottom = Math.Pow(t, n) + top;
            return top / bottom;
        }

        public static Point2D lli8(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            double nx = (x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4);
            double ny = (x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4);
            double d = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (d == 0) { return null; }
            return new Point2D(nx / d, ny / d);
        }

        public static Point2D lli4(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
        {
            double x1 = p1.X, y1 = p1.Y;
            double x2 = p2.X, y2 = p2.Y;
            double x3 = p3.X, y3 = p3.Y;
            double x4 = p4.X, y4 = p4.Y;
            return Utils.lli8(x1, y1, x2, y2, x3, y3, x4, y4);
        }

        public static Point2D lli(Point2D v1, Point2D v2)
        {
            return Utils.lli4(v1, v1.c, v2, v2.c);
        }

        public static Bezier makeline(Point2D p1, Point2D p2)
        {
            double x1 = p1.X, y1 = p1.Y, x2 = p2.X, y2 = p2.Y, dx = (x2 - x1) / 3, dy = (y2 - y1) / 3;
            return new Bezier(x1, y1, x1 + dx, y1 + dy, x1 + 2 * dx, y1 + 2 * dy, x2, y2);
        }

        public static Point2D findbbox(sections)
        {
            var mx = 99999999, my = mx, MX = -mx, MY = MX;
            sections.forEach(function(s) {
                var bbox = s.bbox();
                if (mx > bbox.x.min) mx = bbox.x.min;
                if (my > bbox.y.min) my = bbox.y.min;
                if (MX < bbox.x.max) MX = bbox.x.max;
                if (MY < bbox.y.max) MY = bbox.y.max;
            });
            return new Point2D(
                x: { min: mx, mid: (mx + MX) / 2, max: MX, size: MX - mx },
                y: { min: my, mid: (my + MY) / 2, max: MY, size: MY - my }
            )
        }

        public static void shapeintersections(s1, bbox1, s2, bbox2)
        {
            if (!Utils.bboxoverlap(bbox1, bbox2)) return [];
            var intersections = [];
            var a1 = [s1.startcap, s1.forward, s1.back, s1.endcap];
            var a2 = [s2.startcap, s2.forward, s2.back, s2.endcap];
            a1.ForEach((l1) {
                if (l1.virtual) return;
                a2.ForEach((l2)
                {
                    if (l2.virtual) return;
                  var iss = l1.intersects(l2);
                  if(iss.length>0) 
                   {
                    iss.c1 = l1;
                    iss.c2 = l2;
                    iss.s1 = s1;
                    iss.s2 = s2;
                    intersections.push(iss);
                  }
});
              });
          return intersections;
        }

    public static void makeshape(forward, back)
{
    var bpl = back.points.length;
    var fpl = forward.points.length;
    var start = Utils.makeline(back.points[bpl - 1], forward.points[0]);
    var end = Utils.makeline(forward.points[fpl - 1], back.points[0]);
    var shape = {
        startcap: start,
        forward: forward,
        back: back,
        endcap: end,
        bbox: Utils.findbbox([start, forward, back, end])
      };
var self = Utils;
shape.intersections = function(s2)
{
    return self.shapeintersections(shape, shape.bbox, s2, s2.bbox);
};
      return shape;
    }

    public static void getminmax(curve, d, list)
{
    if (!list) return { min: 0, max: 0 };
    var min = 0xFFFFFFFFFFFFFFFF, max = -min, t, c;
    if (list.indexOf(0) == -1) { list = [0].concat(list); }
    if (list.indexOf(1) == -1) { list.push(1); }
    for (var i = 0, len = list.length; i < len; i++)
    {
        t = list[i];
        c = curve.get(t);
        if (c[d] < min) { min = c[d]; }
        if (c[d] > max) { max = c[d]; }
    }
    return { min: min, mid: (min + max) / 2, max: max, size: max - min };
}

public static List<Point2D> align(List<Point2D> curve, LineSegment line)
{
    double tx = line.A.X;
    double ty = line.A.Y;
    double a = -Math.Atan2(line.B.Y - ty, line.B.X - tx);
    List<Point2D> results = new List<Point2D>();
    foreach (Point2D v in curve)
    {
        results.Add(new Point2D((v.X - tx) * Math.Cos(a) - (v.Y - ty) * Math.Sin(a), (v.X - tx) * Math.Sin(a) + (v.Y - ty) * Math.Cos(a)));
    }
    return results;
}

public static roots(points, line)
{
    line = line || { p1: { x: 0,y: 0},p2: { x: 1,y: 0} };
    var order = points.length - 1;
    var p = utils.align(points, line);
    var reduce = function(t) { return 0 <= t && t <= 1; };

    if (order == 2)
    {
        var a = p[0].y,
            b = p[1].y,
            c = p[2].y,
            d = a - 2 * b + c;
        if (d !== 0)
        {
            var m1 = -sqrt(b * b - a * c),
                m2 = -a + b,
                v1 = -(m1 + m2) / d,
                v2 = -(-m1 + m2) / d;
            return [v1, v2].filter(reduce);
        }
        else if (b !== c && d == 0)
        {
            return [(2 * b - c) / 2 * (b - c)].filter(reduce);
        }
        return [];
    }

    // see http://www.trans4mind.com/personal_development/mathematics/polynomials/cubicAlgebra.htm
    var pa = p[0].y,
        pb = p[1].y,
        pc = p[2].y,
        pd = p[3].y,
        d = (-pa + 3 * pb - 3 * pc + pd),
        a = (3 * pa - 6 * pb + 3 * pc) / d,
        b = (-3 * pa + 3 * pb) / d,
        c = pa / d,
        p = (3 * b - a * a) / 3,
        p3 = p / 3,
        q = (2 * a * a * a - 9 * a * b + 27 * c) / 27,
        q2 = q / 2,
        discriminant = q2 * q2 + p3 * p3 * p3,
        u1, v1, x1, x2, x3;
    if (discriminant < 0)
    {
        var mp3 = -p / 3,
            mp33 = mp3 * mp3 * mp3,
            r = sqrt(mp33),
            t = -q / (2 * r),
            cosphi = t < -1 ? -1 : t > 1 ? 1 : t,
            phi = acos(cosphi),
            crtr = crt(r),
            t1 = 2 * crtr;
        x1 = t1 * cos(phi / 3) - a / 3;
        x2 = t1 * cos((phi + tau) / 3) - a / 3;
        x3 = t1 * cos((phi + 2 * tau) / 3) - a / 3;
        return [x1, x2, x3].filter(reduce);
    }
    else if (discriminant == 0)
    {
        u1 = q2 < 0 ? crt(-q2) : -crt(q2);
        x1 = 2 * u1 - a / 3;
        x2 = -u1 - a / 3;
        return [x1, x2].filter(reduce);
    }
    else
    {
        var sd = sqrt(discriminant);
        u1 = crt(-q2 + sd);
        v1 = crt(q2 + sd);
        return [u1 - v1 - a / 3].filter(reduce); ;
    }
},

    droots: function(p)
{
    // quadratic roots are easy
    if (p.length == 3)
    {
        var a = p[0],
            b = p[1],
            c = p[2],
            d = a - 2 * b + c;
        if (d !== 0)
        {
            var m1 = -sqrt(b * b - a * c),
                m2 = -a + b,
                v1 = -(m1 + m2) / d,
                v2 = -(-m1 + m2) / d;
            return [v1, v2];
        }
        else if (b !== c && d == 0)
        {
            return [(2 * b - c) / (2 * (b - c))];
        }
        return [];
    }

    // linear roots are even easier
    if (p.length == 2)
    {
        var a = p[0], b = p[1];
        if (a !== b) { return [a / (a - b)]; }
        return [];
    }
},

public static List<Point2D> inflections(List<Point2D> points)
{
    var p = Utils.align(points, new List<Point2D>() { points[0], points[3] });
    double a = p[2].X * p[1].Y;
    double b = p[3].X * p[1].Y;
    double c = p[1].X * p[2].Y;
    double d = p[3].X * p[2].Y;
    double v1 = 18 * (-3 * a + 2 * b + 3 * c - d);
    double v2 = 18 * (3 * a - b - 3 * c);
    double v3 = 18 * (c - a);

    if (Utils.approximately(v1, 0)) return [];

    double trm = v2 * v2 - 4 * v1 * v3;
    double sq = Math.Sqrt(trm);
    d = 2 * v1;

    if (Utils.approximately(d, 0)) return [];

    return [(sq - v2) / d, -(v2 + sq) / d].filter(function(r) {
        return (0 <= r && r <= 1);
    });
}

bboxoverlap: function(b1, b2)
{
    var dims =['x', 'y'], len = dims.length, i, dim, l, t, d
      for (i = 0; i < len; i++)
    {
        dim = dims[i];
        l = b1[dim].mid;
        t = b2[dim].mid;
        d = (b1[dim].size + b2[dim].size) / 2;
        if (abs(l - t) >= d) return false;
    }
    return true;
},

    expandbox: function(bbox, _bbox)
{
    if (_bbox.x.min < bbox.x.min) { bbox.x.min = _bbox.x.min; }
    if (_bbox.y.min < bbox.y.min) { bbox.y.min = _bbox.y.min; }
    if (_bbox.z && _bbox.z.min < bbox.z.min) { bbox.z.min = _bbox.z.min; }
    if (_bbox.x.max > bbox.x.max) { bbox.x.max = _bbox.x.max; }
    if (_bbox.y.max > bbox.y.max) { bbox.y.max = _bbox.y.max; }
    if (_bbox.z && _bbox.z.max > bbox.z.max) { bbox.z.max = _bbox.z.max; }
    bbox.x.mid = (bbox.x.min + bbox.x.max) / 2;
    bbox.y.mid = (bbox.y.min + bbox.y.max) / 2;
    if (bbox.z) { bbox.z.mid = (bbox.z.min + bbox.z.max) / 2; }
    bbox.x.size = bbox.x.max - bbox.x.min;
    bbox.y.size = bbox.y.max - bbox.y.min;
    if (bbox.z) { bbox.z.size = bbox.z.max - bbox.z.min; }
},

    pairiteration: function(c1, c2)
{
    var c1b = c1.bbox(),
        c2b = c2.bbox(),
        r = 100000,
        threshold = 0.5;
    if (c1b.x.size + c1b.y.size < threshold && c2b.x.size + c2b.y.size < threshold)
    {
        return [((r * (c1._t1 + c1._t2) / 2) | 0) / r + "/" + ((r * (c2._t1 + c2._t2) / 2) | 0) / r];
    }
    var cc1 = c1.split(0.5),
        cc2 = c2.split(0.5),
        pairs = [
            { left: cc1.left, right: cc2.left },
            { left: cc1.left, right: cc2.right },
            { left: cc1.right, right: cc2.right },
            { left: cc1.right, right: cc2.left }];
    pairs = pairs.filter(function(pair) {
        return utils.bboxoverlap(pair.left.bbox(), pair.right.bbox());
    });
    var results = [];
    if (pairs.length == 0) return results;
    pairs.forEach(function(pair) {
        results = results.concat(
          utils.pairiteration(pair.left, pair.right)
        );
    })
      results = results.filter(function(v, i) {
        return results.indexOf(v) == i;
    });
    return results;
},

    getccenter: function(p1, p2, p3)
{
    var dx1 = (p2.x - p1.x),
        dy1 = (p2.y - p1.y),
        dx2 = (p3.x - p2.x),
        dy2 = (p3.y - p2.y);
    var dx1p = dx1 * cos(quart) - dy1 * sin(quart),
        dy1p = dx1 * sin(quart) + dy1 * cos(quart),
        dx2p = dx2 * cos(quart) - dy2 * sin(quart),
        dy2p = dx2 * sin(quart) + dy2 * cos(quart);
    // chord midpoints
    var mx1 = (p1.x + p2.x) / 2,
        my1 = (p1.y + p2.y) / 2,
        mx2 = (p2.x + p3.x) / 2,
        my2 = (p2.y + p3.y) / 2;
    // midpoint offsets
    var mx1n = mx1 + dx1p,
        my1n = my1 + dy1p,
        mx2n = mx2 + dx2p,
        my2n = my2 + dy2p;
    // intersection of these lines:
    var arc = utils.lli8(mx1, my1, mx1n, my1n, mx2, my2, mx2n, my2n),
        r = utils.dist(arc, p1),
        // arc start/end values, over mid point:
        s = atan2(p1.y - arc.y, p1.x - arc.x),
        m = atan2(p2.y - arc.y, p2.x - arc.x),
        e = atan2(p3.y - arc.y, p3.x - arc.x),
        _;
    // determine arc direction (cw/ccw correction)
    if (s < e)
    {
        // if s<m<e, arc(s, e)
        // if m<s<e, arc(e, s + tau)
        // if s<e<m, arc(e, s + tau)
        if (s > m || m > e) { s += tau; }
        if (s > e) { _ = e; e = s; s = _; }
    }
    else
    {
        // if e<m<s, arc(e, s)
        // if m<e<s, arc(s, e + tau)
        // if e<s<m, arc(s, e + tau)
        if (e < m && m < s) { _ = e; e = s; s = _; } else { e += tau; }
    }
    // assign and done.
    arc.s = s;
    arc.e = e;
    arc.r = r;
    return arc;
}
    }
}
