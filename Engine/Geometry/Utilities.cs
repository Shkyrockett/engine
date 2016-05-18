/*
  A javascript Bezier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Geometry
{
    public delegate double DerivitiveMethodDouble(double x);
    public delegate Point2D DerivitiveMethod2D(double x);
    public delegate Point3D DerivitiveMethod3D(double x);

    /// <summary>
    /// 
    /// </summary>
    internal class Utilities
    {
        // math-inlining.
        static double abs(double value) => Math.Abs(value);
        static double cos(double value) => Math.Cos(value);
        static double sin(double value) => Math.Sin(value);
        static double acos(double value) => Math.Acos(value);
        static double atan2(double x, double y) => Math.Atan2(x, y);
        static double sqrt(double value) => Math.Sqrt(value);
        static double pow(double x, double y) => Math.Pow(x, y);

        // cube root function yielding real roots
        static double crt(double value) => value < 0 ? -Math.Pow(-value, 1d / 3d) : Math.Pow(value, 1d / 3d);

        // trig constants
        const double pi = Math.PI;
        const double tau = 2d * pi;
        const double quart = pi / 2d;

        // float precision significant decimal
        const double epsilon = 0.000001d;

        // Legendre-Gauss abscissae with n=24 (x_i values, defined at i=n as the roots of the nth order Legendre polynomial Pn(x))
        public static List<double> Tvalues = new List<double>()
        {
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
        public static List<double> Cvalues = new List<double>()
        {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="derivativeFn"></param>
        /// <returns></returns>
        public static double arcfn(double t, DerivitiveMethod2D derivativeFn)
        {
            var d = derivativeFn(t);
            var l = d.X * d.X + d.Y * d.Y;
            return Math.Sqrt(l);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="derivativeFn"></param>
        /// <returns></returns>
        public static double arcfn(double t, DerivitiveMethod3D derivativeFn)
        {
            var d = derivativeFn(t);
            var l = d.X * d.X + d.Y * d.Y + d.Z * d.Z;
            return Math.Sqrt(l);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="m"></param>
        /// <param name="M"></param>
        /// <returns></returns>
        public static bool between(double v, double m, double M)
        {
            return (m <= v && v <= M) || approximately(v, m) || approximately(v, M);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static bool approximately(double a, double b, double precision = epsilon)
        {
            return Math.Abs(a - b) <= precision;// (precision || epsilon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="derivativeFn"></param>
        /// <returns></returns>
        public static double length(DerivitiveMethod3D derivativeFn)
        {
            double z = 0.5, sum = 0, len = Tvalues.Count, t;
            for (int i = 0; i < len; i++)
            {
                t = z * Tvalues[i] + z;
                sum += Cvalues[i] * arcfn(t, derivativeFn);
            }

            return z * sum;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="ds"></param>
        /// <param name="de"></param>
        /// <param name="ts"></param>
        /// <param name="te"></param>
        /// <returns></returns>
        public static double map(double v, double ds, double de, double ts, double te)
        {
            double d1 = de - ds;
            double d2 = te - ts;
            double v2 = v - ds;
            double r = v2 / d1;
            return ts + d2 * r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Point2D lerp(double r, Point2D v1, Point2D v2)
        {
            Point2D ret = new Point2D(v1.X + r * (v2.X - v1.X), v1.Y + r * (v2.Y - v1.Y));
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Point3D lerp(double r, Point3D v1, Point3D v2)
        {
            Point3D ret = new Point3D(
                v1.X + r * (v2.X - v1.X),
                v1.Y + r * (v2.Y - v1.Y),
                v1.Z + r * (v2.Z - v1.Z)
                );
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string pointToString(Point2D p)
        {
            return p.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string pointToString(Point3D p)
        {
            return p.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static string pointsToString(List<Point2D> points)
        {
            return points.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static string pointsToString(List<Point3D> points)
        {
            return points.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Point3D copy(Point3D obj)
        {
            return new Point3D(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double angle(Point3D o, Point3D v1, Point3D v2)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double round(double v, int d)
        {
            return Math.Round(v, d, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double dist(Point2D p1, Point2D p2)
        {
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double dist(Point3D p1, Point3D p2)
        {
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;
            double dz = p1.Z - p2.Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LUT"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Tuple<double, double> closest(List<Point3D> LUT, Point3D point)
        {
            double mdist = Math.Pow(2, 63);
            double mpos = 0;
            for (int i = 0; i < LUT.Count; i++)
            {
                double d = dist(point, LUT[i]);
                if (d < mdist)
                {
                    mdist = d;
                    mpos = i;
                }
            }

            return new Tuple<double, double>(mdist, mpos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double abcratio(double t = 0.5, double n = 0.5)
        {
            // see ratio(t) note on http://pomax.github.io/bezierinfo/#abc
            if (n != 2 && n != 3)
            {
                return double.NaN;
            }
            if (t == 0.5d) t = 0.5;
            else if (t == 0 || t == 1)
            {
                return t;
            }
            double bottom = Math.Pow(t, n) + Math.Pow(1 - t, n);
            double top = bottom - 1;
            return Math.Abs(top / bottom);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double projectionratio(double t = 0.5, double n = 0.5)
        {
            // see u(t) note on http://pomax.github.io/bezierinfo/#abc
            if (n != 2 && n != 3)
            {
                return double.NaN;
            }
            if (t == 0.5d) t = 0.5;
            else if (t == 0 || t == 1)
            {
                return t;
            }
            double top = Math.Pow(1 - t, n);
            double bottom = Math.Pow(t, n) + top;
            return top / bottom;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="x4"></param>
        /// <param name="y4"></param>
        /// <returns></returns>
        public static Point3D lli8(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            double nx = (x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4);
            double ny = (x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4);
            double d = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (d == 0) { return null; }
            return new Point3D(nx / d, ny / d, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <returns></returns>
        public static Point3D lli4(Point3D p1, Point3D p2, Point3D p3, Point3D p4)
        {
            double x1 = p1.X, y1 = p1.Y;
            double x2 = p2.X, y2 = p2.Y;
            double x3 = p3.X, y3 = p3.Y;
            double x4 = p4.X, y4 = p4.Y;
            return lli8(x1, y1, x2, y2, x3, y3, x4, y4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Point3D lli(Point3D v1, Point3D v2)
        {
            // return utils.lli4(v1,v1.c,v2,v2.c);
            return lli4(v1, v1, v2, v2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Bezier makeline(Point3D p1, Point3D p2)
        {
            double x1 = p1.X;
            double y1 = p1.Y;
            double x2 = p2.X;
            double y2 = p2.Y;
            double dx = (x2 - x1) / 3;
            double dy = (y2 - y1) / 3;
            return new Bezier(x1, y1, x1 + dx, y1 + dy, x1 + 2 * dx, y1 + 2 * dy, x2, y2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static BBox findbbox(List<Bezier> sections)
        {
            double mx = 99999999;
            double my = mx;
            double MX = -mx;
            double MY = MX;
            foreach (Bezier s in sections)
            {
                BBox bbox = s.bbox();
                if (mx > bbox.x.min) mx = bbox.x.min;
                if (my > bbox.y.min) my = bbox.y.min;
                if (MX < bbox.x.max) MX = bbox.x.max;
                if (MY < bbox.y.max) MY = bbox.y.max;
            }

            return new BBox(
                x: new Range(min: mx, mid: (mx + MX) / 2, max: MX, size: MX - mx),
                y: new Range(min: my, mid: (my + MY) / 2, max: MY, size: MY - my)
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="bbox1"></param>
        /// <param name="s2"></param>
        /// <param name="bbox2"></param>
        /// <returns></returns>
        public static List<Pair> shapeintersections(Shape1 s1, BBox bbox1, Shape1 s2, BBox bbox2)
        {
            if (!bboxoverlap(bbox1, bbox2)) return new List<Pair>();
            var intersections = new List<Pair>();
            var a1 = new List<Bezier>() { s1.startcap, s1.forward, s1.back, s1.endcap };
            var a2 = new List<Bezier>() { s2.startcap, s2.forward, s2.back, s2.endcap };
            foreach (Bezier l1 in a1)
            {
                if (l1._virtual) return new List<Pair>();
                foreach (Bezier l2 in a2)
                {
                    if (l2._virtual) return new List<Pair>();
                    List<Pair> iss = l1.intersects(l2);
                    foreach (var i in iss)
                    {
                        if (i.length > 0)
                        {
                            i.left = l1;
                            i.right = l2;
                            i.s1 = s1;
                            i.s2 = s2;
                            intersections.Add(i);
                        }
                    }
                }
            }
            return intersections;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forward"></param>
        /// <param name="back"></param>
        /// <returns></returns>
        public static Shape1 makeshape(Bezier forward, Bezier back)
        {
            var bpl = back.points.Count;
            var fpl = forward.points.Count;
            Bezier start = makeline(back.points[bpl - 1], forward.points[0]);
            Bezier end = makeline(forward.points[fpl - 1], back.points[0]);
            Shape1 shape = new Shape1(
                startcap: start,
                forward: forward,
                back: back,
                endcap: end,
                bbox: findbbox(new List<Bezier>() { start, forward, back, end })
              );
            //shape.intersections = new Shape.IntersectionsDelegate(Bezier s2)
            //{
            //    return shapeintersections(shape, shape.bbox, s2, s2.bbox);
            //};
            return shape;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="d"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Range getminmax(Bezier curve, int d, List<double> list)
        {
            if (list == null) return new Range(min: 0, max: 0);
            double min = 0xFFFFFFFFFFFFFFFF;
            double max = -min;
            double t;
            Point3D c;
            if (list.IndexOf(0) == -1) { list.Insert(0, 0); }
            if (list.IndexOf(1) == -1) { list.Add(1); }
            for (int i = 0, len = list.Count; i < len; i++)
            {
                t = list[i];
                c = curve.get(t);
                switch (d)
                {
                    case 0:
                        if (c.X < min) { min = c.X; }
                        if (c.X > max) { max = c.X; }
                        break;
                    case 1:
                        if (c.Y < min) { min = c.Y; }
                        if (c.Y > max) { max = c.Y; }
                        break;
                    case 2:
                        if (c.Z < min) { min = c.Z; }
                        if (c.Z > max) { max = c.Z; }
                        break;
                    default:
                        break;
                }
            }
            return new Range(min: min, mid: (min + max) / 2, max: max, size: max - min);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://pomax.github.io/bezierinfo/
        /// </remarks>
        public static List<Point3D> align(List<Point3D> points, Line1 line)
        {
            double tx = line.P1.X;
            double ty = line.P1.Y;
            double a = -Math.Atan2(line.P2.Y - ty, line.P2.X - tx);
            List<Point3D> results = new List<Point3D>();
            foreach (Point3D v in points)
            {
                results.Add(new Point3D((v.X - tx) * Math.Cos(a) - (v.Y - ty) * Math.Sin(a), (v.X - tx) * Math.Sin(a) + (v.Y - ty) * Math.Cos(a), 0));
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static List<double> roots(List<Point3D> points, Line1 line)
        {
            //line = line || new Line(p1: new Point2D(x: 0, y: 0), p2: new Point2D(x: 1, y: 0));
            var order = points.Count - 1;
            List<Point3D> pts = align(points, line);

            double a = 0;
            double b = 0;
            double c = 0;
            double d = 0;

            double m1 = 0;
            double m2 = 0;

            if (order == 2)
            {
                a = pts[0].Y;
                b = pts[1].Y;
                c = pts[2].Y;
                d = a - 2 * b + c;
                if (d != 0)
                {
                    m1 = -sqrt(b * b - a * c);
                    m2 = -a + b;
                    double v1_ = -(m1 + m2) / d;
                    double v2_ = -(-m1 + m2) / d;

                    return new List<double>(
                        from t0 in new List<double>() { v1_, v2_ }
                        where 0 <= t0 && t0 <= 1
                        select t0
                    );
                }
                else if (b != c && d == 0)
                {
                    return new List<double>(
                        from t1 in new List<double>() { (2 * b - c) / 2 * (b - c) }
                        where 0 <= t1 && t1 <= 1
                        select t1
                    );
                }
                return new List<double>();
            }

            // see http://www.trans4mind.com/personal_development/mathematics/polynomials/cubicAlgebra.htm
            double pa = pts[0].Y,
                pb = pts[1].Y,
                pc = pts[2].Y,
                pd = pts[3].Y;
            d = (-pa + 3 * pb - 3 * pc + pd);
            a = (3 * pa - 6 * pb + 3 * pc) / d;
            b = (-3 * pa + 3 * pb) / d;
            c = pa / d;
            double p_ = (3 * b - a * a) / 3;
            double p3 = p_ / 3;
            double q = (2 * a * a * a - 9 * a * b + 27 * c) / 27;
            double q2 = q / 2;
            double discriminant = q2 * q2 + p3 * p3 * p3;
            double u1, v1, x1, x2, x3;
            if (discriminant < 0)
            {
                double mp3 = -p_ / 3,
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

                return new List<double>(
                    from t2 in new List<double>() { x1, x2, x3 }
                    where 0 <= t2 && t2 <= 1
                    select t2
                );
            }
            else if (discriminant == 0)
            {
                u1 = q2 < 0 ? crt(-q2) : -crt(q2);
                x1 = 2 * u1 - a / 3;
                x2 = -u1 - a / 3;

                return new List<double>(
                    from t3 in new List<double>() { x1, x2 }
                    where 0 <= t3 && t3 <= 1
                    select t3
                );
            }
            else
            {
                var sd = sqrt(discriminant);
                u1 = crt(-q2 + sd);
                v1 = crt(q2 + sd);

                return new List<double>(
                    from t4 in new List<double>() { u1 - v1 - a / 3 }
                    where 0 <= t4 && t4 <= 1
                    select t4
                );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static List<double> droots(List<double> p)
        {
            // quadratic roots are easy
            if (p.Count == 3)
            {
                double a = p[0],
                    b = p[1],
                    c = p[2],
                    d = a - 2 * b + c;
                if (d != 0)
                {
                    double m1 = -sqrt(b * b - a * c),
                        m2 = -a + b,
                        v1 = -(m1 + m2) / d,
                        v2 = -(-m1 + m2) / d;
                    return new List<double>() { v1, v2 };
                }
                else if (b != c && d == 0)
                {
                    return new List<double>() { (2 * b - c) / (2 * (b - c)) };
                }
                return new List<double>();
            }

            // linear roots are even easier
            if (p.Count == 2)
            {
                double a = p[0], b = p[1];
                if (a != b) { return new List<double>() { a / (a - b) }; }
                return new List<double>();
            }

            return new List<double>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<double> inflections(List<Point3D> points)
        {
            var p = align(points, new Line1(p1: points[0], p2: points[3]));
            double a = p[2].X * p[1].Y;
            double b = p[3].X * p[1].Y;
            double c = p[1].X * p[2].Y;
            double d = p[3].X * p[2].Y;
            double v1 = 18 * (-3 * a + 2 * b + 3 * c - d);
            double v2 = 18 * (3 * a - b - 3 * c);
            double v3 = 18 * (c - a);

            if (approximately(v1, 0)) return new List<double>();

            double trm = v2 * v2 - 4 * v1 * v3;
            double sq = Math.Sqrt(trm);
            d = 2 * v1;

            if (approximately(d, 0)) return new List<double>();

            return new List<double>(
                from r in new List<double>() { (sq - v2) / d, -(v2 + sq) / d }
                where (0 <= r && r <= 1)
                select r
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static bool bboxoverlap(BBox b1, BBox b2)
        {
            var dims = new List<int> { 0/*X*/, 1/*Y*/ };
            double len = dims.Count;
            double l;
            double t;
            double d;
            l = b1.x.mid;
            t = b2.x.mid;
            d = (b1.x.size + b2.x.size) / 2;
            if (abs(l - t) >= d) return false;
            l = b1.y.mid;
            t = b2.y.mid;
            d = (b1.y.size + b2.y.size) / 2;
            if (abs(l - t) >= d) return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bbox"></param>
        /// <param name="_bbox"></param>
        public static void expandbox(BBox bbox, BBox _bbox)
        {
            if (_bbox.x.min < bbox.x.min) { bbox.x.min = _bbox.x.min; }
            if (_bbox.y.min < bbox.y.min) { bbox.y.min = _bbox.y.min; }
            if (_bbox.z != null && _bbox.z.min < bbox.z.min) { bbox.z.min = _bbox.z.min; }
            if (_bbox.x.max > bbox.x.max) { bbox.x.max = _bbox.x.max; }
            if (_bbox.y.max > bbox.y.max) { bbox.y.max = _bbox.y.max; }
            if (_bbox.z != null && _bbox.z.max > bbox.z.max) { bbox.z.max = _bbox.z.max; }
            bbox.x.mid = (bbox.x.min + bbox.x.max) / 2;
            bbox.y.mid = (bbox.y.min + bbox.y.max) / 2;
            if (bbox.z != null) { bbox.z.mid = (bbox.z.min + bbox.z.max) / 2; }
            bbox.x.size = bbox.x.max - bbox.x.min;
            bbox.y.size = bbox.y.max - bbox.y.min;
            if (bbox.z != null) { bbox.z.size = bbox.z.max - bbox.z.min; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        public static List<Pair> pairiteration(Bezier c1, Bezier c2)
        {
            BBox c1b = c1.bbox();
            BBox c2b = c2.bbox();
            double r = 100000;
            double threshold = 0.5;
            if (c1b.x.size + c1b.y.size < threshold && c2b.x.size + c2b.y.size < threshold)
            {
                //return new List<Pair>() {  ((r * (c1._t1 + c1._t2) / 2) | 0d) / r + "/" + ((r * (c2._t1 + c2._t2) / 2) | 0) / r };
            }
            Pair cc1 = c1.split(0.5),
                cc2 = c2.split(0.5);

            List<Pair> pairs = new List<Pair>(
                from pair in new List<Pair>() {
                new Pair(left: cc1.left, right: cc2.left),
                new Pair(left: cc1.left, right: cc2.right),
                new Pair(left: cc1.right, right: cc2.right),
                new Pair(left: cc1.right, right: cc2.left)}
                where bboxoverlap(pair.left.bbox(), pair.right.bbox())
                select pair);

            List<Pair> results = new List<Pair>();
            if (pairs.Count == 0) return results;

            foreach (Pair pair in pairs)
            {
                results.AddRange(pairiteration(pair.left, pair.right));
            }

            return (List<Pair>)new List<Pair>(
                from v in results
                select v).Distinct();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static Arc1 getccenter(Point3D p1, Point3D p2, Point3D p3)
        {
            double dx1 = (p2.X - p1.X);
            double dy1 = (p2.Y - p1.Y);
            double dx2 = (p3.X - p2.X);
            double dy2 = (p3.Y - p2.Y);
            double dx1p = dx1 * cos(quart) - dy1 * sin(quart);
            double dy1p = dx1 * sin(quart) + dy1 * cos(quart);
            double dx2p = dx2 * cos(quart) - dy2 * sin(quart);
            double dy2p = dx2 * sin(quart) + dy2 * cos(quart);
            // chord midpoints
            double mx1 = (p1.X + p2.X) / 2d;
            double my1 = (p1.Y + p2.Y) / 2d;
            double mx2 = (p2.X + p3.X) / 2d;
            double my2 = (p2.Y + p3.Y) / 2d;
            // midpoint offsets
            double mx1n = mx1 + dx1p;
            double my1n = my1 + dy1p;
            double mx2n = mx2 + dx2p;
            double my2n = my2 + dy2p;
            // intersection of these lines:
            Point3D arcCenter = lli8(mx1, my1, mx1n, my1n, mx2, my2, mx2n, my2n);
            double r = dist(arcCenter, p1);
            // arc start/end values, over mid point:
            double s = atan2(p1.Y - arcCenter.Y, p1.X - arcCenter.X);
            double m = atan2(p2.Y - arcCenter.Y, p2.X - arcCenter.X);
            double e = atan2(p3.Y - arcCenter.Y, p3.X - arcCenter.X);
            double _;
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
            Arc1 arc = new Arc1();
            arc.c = arcCenter;
            arc.s = s;
            arc.e = e;
            arc.r = r;
            return arc;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Arc1
    {
        public Point3D c { get; internal set; }
        public double r { get; internal set; }
        public double e { get; internal set; }
        public double s { get; internal set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Shape1
    {
        public delegate void IntersectionsDelegate(Bezier s2);

        public Shape1(Bezier startcap, Bezier forward, Bezier back, Bezier endcap, BBox bbox)
        {
            this.startcap = startcap;
            this.forward = forward;
            this.back = back;
            this.endcap = endcap;
            this.bbox = bbox;
        }

        public Bezier startcap { get; internal set; }
        public Bezier forward { get; internal set; }
        public Bezier back { get; internal set; }
        public Bezier endcap { get; internal set; }
        public BBox bbox { get; internal set; }
        public IntersectionsDelegate intersections { get; internal set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BBox
    {
        public BBox(Range x, Range y, Range z = null)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Range x { get; set; }
        public Range y { get; set; }
        public Range z { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Range
    {
        public Range(int min, int max)
            : this(min, min + (max - min) / 2d, max, min - max)
        { }

        public Range(double min, double mid, double max, double size)
        {
            this.min = min;
            this.mid = mid;
            this.max = max;
            this.size = size;
        }

        public double min { get; set; }
        public double mid { get; set; }
        public double max { get; set; }
        public double size { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Line1
    {
        public Line1(Point3D p1, Point3D p2)
        {
            this.P1 = p1;
            this.P2 = p2;
        }

        public Point3D P1 { get; set; }

        public Point3D P2 { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class Pair
    {
        public Pair(Bezier left, Bezier right)
        {
            this.left = left;
            this.right = right;
        }

        public Pair(Bezier left, Bezier right, List<Point3D> span) : this(left, right)
        {
            this.span = span;
        }

        public Bezier left { get; internal set; }
        public Bezier right { get; internal set; }
        public int length { get; internal set; }
        public Shape1 s1 { get; internal set; }
        public Shape1 s2 { get; internal set; }
        public List<Point3D> span { get; internal set; }
        public double _t1 { get; internal set; }
        public double _t2 { get; internal set; }

        public static bool operator ==(Pair left, Pair right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Pair left, Pair right)
        {
            return !left.Equals(right);
        }

        public static bool Equals(Pair left, Pair right)
        {
            return left.left == right.left && right.right == left.right;
        }

        public override bool Equals(object obj)
        {
            return obj is Pair && Equals(this, (Pair)obj);
        }

        public override int GetHashCode()
        {
            return left.GetHashCode() ^ right.GetHashCode();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class PairComparer
        : IEqualityComparer<Pair>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(Pair x, Pair y)
        {

            //Check whether the compared objects reference the same data.
            if (ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x == y && x == y;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.
        public int GetHashCode(Pair pair)
        {
            //Check whether the object is null
            if (ReferenceEquals(pair, null)) return 0;

            //Calculate the hash code for the product.
            return pair.GetHashCode();
        }
    }
}
