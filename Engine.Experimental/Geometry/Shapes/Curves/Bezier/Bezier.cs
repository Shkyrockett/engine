/*
  A javascript Bézier curve library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

using Engine.Experimental;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using static Engine.Operations;
using static System.Math;

namespace Engine;

/// <summary>
/// The bezier class.
/// </summary>
/// <acknowledgment>
/// http://pomax.github.io/bezierinfo/
/// </acknowledgment>
public class Bezier
{
    #region Private Fields
    /// <summary>
    /// The lut.
    /// </summary>
    public List<Point2D> lut = [];

    ///// <summary>
    ///// The dims.
    ///// </summary>
    //private readonly List<char> dims = new List<char> { 'x', 'y'/*, 'z'*/ };
    #endregion Private Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Bezier"/> class.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="v1">The v1.</param>
    /// <param name="v2">The v2.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    public Bezier(double x1, double y1, double v1, double v2, double x2, double y2)
    {
        Points =
        [
            new(x1,y1/*,0*/),
            new(v1,v2/*,0*/),
            new(x2,y2/*,0*/)
        ];
        DerivativePoints = DerivativeCoordinates(Points);
        Direction = ComputeDirection(Points);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Bezier"/> class.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="v1">The v1.</param>
    /// <param name="v2">The v2.</param>
    /// <param name="v3">The v3.</param>
    /// <param name="v4">The v4.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    public Bezier(double x1, double y1, double v1, double v2, double v3, double v4, double x2, double y2)
    {
        Points =
        [
            new(x1,y1/*,0*/),
            new(v1,v2/*,0*/),
            new(v3,v4/*,0*/),
            new(x2,y2/*,0*/)
        ];
        DerivativePoints = DerivativeCoordinates(Points);
        Direction = ComputeDirection(Points);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Bezier" /> class.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="v1">The v1.</param>
    /// <param name="v2">The v2.</param>
    /// <param name="v3">The v3.</param>
    /// <param name="v4">The v4.</param>
    /// <param name="v5">The v5.</param>
    /// <param name="v6">The v6.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="z2">The z2.</param>
    public Bezier(double x1, double y1, double z1, double v1, double v2, double v3, double v4, double v5, double v6, double x2, double y2, double z2)
    {
        _ = z1;
        _ = v3;
        _ = v6;
        _ = z2;
        Points =
        [
            new(x1,y1/*,z1*/),
            new(v1,v2/*,v3*/),
            new(v4,v5/*,v6*/),
            new(x2,y2/*,z2*/)
        ];
        DerivativePoints = DerivativeCoordinates(Points);
        Direction = ComputeDirection(Points);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Bezier"/> class.
    /// </summary>
    /// <param name="points">The points.</param>
    public Bezier(List<Point2D> points)
    {
        Points = points;
        DerivativePoints = DerivativeCoordinates(Points);
        Direction = ComputeDirection(Points);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Bezier"/> class.
    /// </summary>
    /// <param name="p1">The p1.</param>
    /// <param name="p2">The p2.</param>
    /// <param name="p3">The p3.</param>
    public Bezier(Point2D p1, Point2D p2, Point2D p3)
        : this([p1, p2, p3])
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Bezier"/> class.
    /// </summary>
    /// <param name="p1">The p1.</param>
    /// <param name="p2">The p2.</param>
    /// <param name="p3">The p3.</param>
    /// <param name="p4">The p4.</param>
    public Bezier(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
        : this([p1, p2, p3, p4])
    { }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets or sets the points.
    /// </summary>
    public List<Point2D> Points { get; set; }

    /// <summary>
    /// Gets or sets the Derivative Points.
    /// </summary>
    public List<List<Point2D>> DerivativePoints { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// </summary>
    public bool Virtual { get; set; }

    /// <summary>
    /// Gets or sets the t1.
    /// </summary>
    public double T1 { get; set; }

    /// <summary>
    /// Gets or sets the t2.
    /// </summary>
    public double T2 { get; set; }

    /// <summary>
    /// Gets a value indicating whether 
    /// </summary>
    public bool Is3d { get; }

    /// <summary>
    /// Gets a value indicating whether 
    /// </summary>
    public RotationDirection Direction { get; private set; }

    /// <summary>
    /// Gets the order.
    /// </summary>
    public int Order => Points.Count - 1;

    /// <summary>
    /// Gets a value indicating whether 
    /// </summary>
    public bool Linear { get; }

    /// <summary>
    /// Gets the length.
    /// </summary>
    public double Length => BezierUtil.Length(Derivativate);

    /// <summary>
    /// The inflections.
    /// </summary>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    public List<double> Inflections => BezierUtil.Inflections(Points);

    /// <summary>
    /// The extrema.
    /// </summary>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    public List<double> Extrema
    {
        get
        {
            var p = (from a in DerivativePoints[0] select a.X).ToList();
            var result = BezierUtil.DRoots(p);
            p = (from a in DerivativePoints[0] select a.Y).ToList();
            result.AddRange(BezierUtil.DRoots(p));
            p = (from a in DerivativePoints[1] select a.X).ToList();
            result.AddRange(BezierUtil.DRoots(p));
            p = (from a in DerivativePoints[1] select a.Y).ToList();
            result.AddRange(BezierUtil.DRoots(p));

            result = result.Where((t) => { return t >= 0 && t <= 1; }).ToList();
            result.Sort();
            return result;
        }
    }
    #endregion Properties

    #region Operators
    /// <summary>
    /// The operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public static bool operator ==(Bezier left, Bezier right) => left?.Equals(right) ?? right is null;

    /// <summary>
    /// The operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public static bool operator !=(Bezier left, Bezier right) => !(left == right);
    #endregion Operators

    #region Factories
    /// <summary>
    /// The quadratic from points.
    /// </summary>
    /// <param name="p1">The p1.</param>
    /// <param name="p2">The p2.</param>
    /// <param name="p3">The p3.</param>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="Bezier"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Bezier QuadraticFromPoints(Point2D p1, Point2D p2, Point2D p3, double t = 0.5d)
    {
        // shortcuts, although they're really dumb
        if (t == 0)
        {
            return new Bezier(p2, p2, p3);
        }

        if (t == 1)
        {
            return new Bezier(p1, p2, p2);
        }

        // real fitting.
        var abc = BezierUtil.GetABC(2, p1, p2, p3, t);

        return new Bezier(p1, abc.Item1, p3);
    }

    /// <summary>
    /// The cubic from points.
    /// </summary>
    /// <param name="S">The S.</param>
    /// <param name="B">The B.</param>
    /// <param name="E">The E.</param>
    /// <param name="t">The t.</param>
    /// <param name="d1">The d1.</param>
    /// <returns>The <see cref="Bezier"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Bezier CubicFromPoints(Point2D S, Point2D B, Point2D E, double t = 0.5d, double d1 = 0d)
    {
        var abc = BezierUtil.GetABC(3, S, B, E, t);

        if (d1 == 0)
        {
            d1 = Measurements.Distance(B, abc.Item3);
        }

        var d2 = d1 * (1 - t) / t;

        var selen = Measurements.Distance(S, E);
        var lx = (E.X - S.X) / selen;
        var ly = (E.Y - S.Y) / selen;
        //var lz = (E.Z - S.Z) / selen;

        var bx1 = d1 * lx;
        var by1 = d1 * ly;
        //var bz1 = d1 * lz;
        var bx2 = d2 * lx;
        var by2 = d2 * ly;
        //var bz2 = d2 * lz;

        // derivation of new hull coordinates
        var e1 = new Point2D(
            x: B.X - bx1,
            y: B.Y - by1
            //,z: B.Z - bz1
            );
        var e2 = new Point2D(
            x: B.X + bx2,
            y: B.Y + by2
            //,z: B.Z + bz2
            );
        var A = abc.Item1;
        var v1 = new Point2D(
            x: A.X + ((e1.X - A.X) / (1 - t)),
            y: A.Y + ((e1.Y - A.Y) / (1 - t))
            //,z: A.Z + (e1.Z - A.Z) / (1 - t)
            );
        var v2 = new Point2D(
            x: A.X + ((e2.X - A.X) / t),
            y: A.Y + ((e2.Y - A.Y) / t)
            //,z: A.Z + (e2.Z - A.Z) / (t)
            );
        var nc1 = new Point2D(
            x: S.X + ((v1.X - S.X) / t),
            y: S.Y + ((v1.Y - S.Y) / t)
            //,z: S.Y + (v1.Y - S.Y) / (t)
            );
        var nc2 = new Point2D(
            x: E.X + ((v2.X - E.X) / (1 - t)),
            y: E.Y + ((v2.Y - E.Y) / (1 - t))
            //,z: E.Y + (v2.Y - E.Y) / (1 - t)
            );

        return new Bezier(S, nc1, nc2, E);
    }
    #endregion Factories

    #region Bezier Methods
    /// <summary>
    /// Gets the derivative coordinates.
    /// </summary>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static List<List<Point2D>> DerivativeCoordinates(List<Point2D> Points)
    {
        // One-time compute of derivative coordinates
        var derivitivePoints = new List<List<Point2D>>();
        var p = Points;
        for (int d = (p?.Count).Value, c = d - 1; d > 1; d--, c--)
        {
            var list = new List<Point2D>();
            for (var j = 0; j < c; j++)
            {
                var dpt = new Point2D(
                     x: c * (p[j + 1].X - p[j].X),
                     y: c * (p[j + 1].Y - p[j].Y)
                //,z: c * (p[j + 1].Z - p[j].Z)
                );

                list?.Add(dpt);
            }

            derivitivePoints?.Add(list);
            p = list;
        }

        return derivitivePoints;
    }

    /// <summary>
    /// The compute direction.
    /// </summary>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static RotationDirection ComputeDirection(List<Point2D> Points)
    {
        ArgumentNullException.ThrowIfNull(Points);

        var angle = BezierUtil.Angle(Points[0], Points[^1], Points[1]);
        return angle > 0 ? RotationDirection.Clockwise : RotationDirection.CounterClockwise;
    }

    /// <summary>
    /// Get the look up table.
    /// </summary>
    /// <param name="steps">The steps.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<Point2D> GetLookUpTable(int steps = 100)
    {
        if (lut.Count == steps)
        {
            return lut;
        }

        lut = [];

        for (var t = 0; t <= steps; t++)
        {
            lut.Add(Interpolate(t / (double)steps));
        }

        return lut;
    }

    /// <summary>
    /// Raises the  event.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="epsilon">The error.</param>
    /// <returns>The <see cref="double"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public double On(Point2D point, double epsilon)
    {
        var lut = GetLookUpTable(1000);
        var hits = new List<Point2D>();

        double t = 0;
        for (var i = 0; i < lut.Count; i++)
        {
            var c = lut[i];
            if (Measurements.Distance(c, point) < epsilon)
            {
                hits.Add(c);
                t += i / lut.Count;
            }
        }

        return hits.Count == 0 ? 0 : (t /= hits.Count);
    }

    /// <summary>
    /// The PB. https://www.geometrictools.com/Documentation/MovingAlongCurveSpecifiedSpeed.pdf
    /// </summary>
    /// <param name="ts">The ts.</param>
    /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
    /// <returns>The <see cref="double"/>.</returns>
    public double PB(double ts, double epsilon = double.Epsilon)
    {
        // The curve parameter interval[tmin , tmax].
        var tmin = 0d;
        var tmax = 1d;
        var imax = 1d;
        //double n = 1;
        //double umin; // The curve parameter i n t e r va l [ umin , umax ] .
        //Point2D Y(double t) { return Interpolate_Ported(ts); };
        // The position Y( t ) , tmin <= t <= tmax. 
        Point2D DY(double t)
        {
            return Derivativate(ts);
        };
        static double Length(Point2D u) { return double.NaN; }
        //double LengthDY(double u) { return Length(DY(u)); }
        // The derivative dY( t )/dt , tmin <= t <= tmax . 
        double Speed(double t) { return Length(DY(t)); }
        //double Sigma(double t) { return Speed(t); } // The user−specified speed at time t .
        static double Integral(double min, double max, double u) { return double.NaN; }
        double ArcLength(double t) { return Integral(tmin, t, Speed(t)); }
        var L = ArcLength(tmax);
        // The total length of the curve.

        return GetCurveParameter1(ts);

        double GetCurveParameter1(double s) // 0 <= s <= L , output i s t
        {
            // Initial guess for Newton's method. 
            var t = tmin + (s * (tmax - tmin) / L);

            // Initial root−bounding interval for bisection . 
            double lower = tmin, upper = tmax;

            for (var i = 0; i < imax; i++) // 'imax' i s application−specified 
            {
                var F = ArcLength(t) - s;
                if (Abs(F) < epsilon) // 'epsilon' i s application−specified
                {
                    // |F( t )| i s close enough to zero, report t as the time at 
                    // which length s i s attained. 
                    return t;
                }

                // Generate a candidate for Newton ’ s method . 
                var DF = Speed(t);
                var tCandidate = t - (F / DF);

                // Update the root-bounding interval and test for containment of 
                // the candidate. 
                if (F > 0)
                {
                    upper = t;
                    if (tCandidate <= lower)
                    {
                        // Candidate i s outside the root−bounding interval. Use 
                        // bisection instead. 
                        t = 0.5 * (upper + lower);
                    }
                    else
                    {
                        // There i s no need to compare to 'upper' because the tangent 
                        // line has positive slope, guaranteeing that the t−axis 
                        // intercept i s smaller than 'upper'. 
                        t = tCandidate;
                    }
                }
                else
                {
                    lower = t;
                    if (tCandidate >= upper)
                    {
                        // Candidate i s outside the root−bounding interval. Use 
                        // bisection instead. 
                        t = 0.5 * (upper + lower);
                    }
                    else
                    {
                        // There is no need to compare to 'lower' because the tangent 
                        // line has positive slope, guaranteeing that the t−axis 
                        // intercept is larger than 'lower'.
                        t = tCandidate;
                    }
                }
            }

            // A root was not found according to the specified number of iterations 
            // and tolerance. You might want to increase iterationsor tolerance or 
            // integration accuracy . However , in this application itislikely that 
            // the time values are oscillating , due to the limited numerical 
            // precision of 32−bit floats . It is safe to use the last computed time . 
            return t;
        }

        //double GetCurveParameter2(double s) // 0 <= s <= L , output is t
        //{
        //    var t = tmin; // i n i t i a l condition
        //    var h = s / n; // step size , ‘n ’ i s application−specified
        //    for (var i = 1; i <= n; i++)
        //    {
        //        // The d i v i s i o n s here might be a problem i f the d i v i s o r s are 
        //        // nearly zero .
        //        var k1 = h / Speed(t);
        //        var k2 = h / Speed(t + k1 / 2);
        //        var k3 = h / Speed(t + k2 / 2);
        //        var k4 = h / Speed(t + k3);
        //        t += (k1 + 2 * (k2 + k3) + k4) / 6;
        //    }

        //    return t;
        //}

        //double GetU1(double t) // tmin <= t <= tmax 
        //{
        //    var ell = Integral(tmin, t, Sigma(t)); // 0 <= e l l <= L
        //    var u = GetCurveParameter2(ell); // umin <= u <= umax 
        //    return u;
        //}

        //double GetU2(double t) // tmin <= t <= tmax 
        //{
        //    var h = (t - tmin) / n; // step size , ‘n ’ i s application−specified 
        //    var u = umin; // i n i t i a l condition 
        //    t = tmin; // i n i t i a l condition 
        //    for (var i = 1; i <= n; i++)
        //    {
        //        // The d i v i s i o n s here might be a problem i f the d i v i s o r s are 
        //        // nearly zero .
        //        var k1 = h * Sigma(t) / LengthDY(u);
        //        var k2 = h * Sigma(t + h / 2) / LengthDY(u + k1 / 2);
        //        var k3 = h * Sigma(t + h / 2) / LengthDY(u + k2 / 2);
        //        var k4 = h * Sigma(t + h) / LengthDY(u + k3);
        //        t += h;
        //        u += (k1 + 2 * (k2 + k3) + k4) / 6;
        //    }
        //    return u;
        //}

    }

    /// <summary>
    /// The project.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public AccumulatorPoint2D Project(Point2D point)
    {
        // step 1: coarse check
        var LUT = GetLookUpTable(1000);
        var l = LUT.Count - 1;

        var (mdist, mpos) = BezierUtil.Closest(LUT, point);
        if (mpos == 0 || mpos == l)
        {
            var t0 = mpos / l;
            AccumulatorPoint2D pt = Interpolate(t0);
            pt.Theta = t0;
            pt.TotalDistance = mdist;
            return pt;
        }

        // step 2: fine check
        double ft;
        double t;
        AccumulatorPoint2D p;
        double d;
        var t1 = (mpos - 1) / l;
        var t2 = (mpos + 1) / l;
        var step = 0.1 / l;
        mdist += 1;

        for (t = t1, ft = t; t < t2 + step; t += step)
        {
            p = Interpolate(t);
            d = Measurements.Distance(point, p.ToPoint());
            if (d < mdist)
            {
                mdist = d;
                ft = t;
            }
        }
        p = Interpolate(ft);
        p.Theta = ft;
        p.TotalDistance = mdist;
        return p;
    }

    /// <summary>
    /// The point.
    /// </summary>
    /// <param name="idx">The idx.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private Point2D Point(int idx) => Points[idx];

    /// <summary>
    /// The Interpolate.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="Point3D"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Point2D Interpolate(double t)
    {
        // shortcuts
        if (t == 0)
        {
            return Points[0];
        }

        if (t == 1)
        {
            return Points[Order];
        }

        var p = Points;
        var mt = 1 - t;

        // linear?
        if (Order == 1)
        {
            var ret = new Point2D(
            x: (mt * p[0].X) + (t * p[1].X),
            y: (mt * p[0].Y) + (t * p[1].Y)
            //,z: mt * p[0].Z + t * p[1].Z
            );
            return ret;
        }

        // quadratic/cubic curve?
        if (Order < 4)
        {
            var mt2 = mt * mt;
            var t2 = t * t;
            double a = 0;
            double b = 0;
            double c = 0;
            double d = 0;
            if (Order == 2)
            {
                p = [Points[0], Points[1], Points[2], Point2D.Empty];
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
                new Point2D(
                x: (a * p[0].X) + (b * p[1].X) + (c * p[2].X) + (d * p[3].X),
                y: (a * p[0].Y) + (b * p[1].Y) + (c * p[2].Y) + (d * p[3].Y)
                //,z: a * p[0].Z + b * p[1].Z + c * p[2].Z + d * p[3].Z
                );
            return ret;
        }

        // higher order curves: use de Casteljau's computation
        var dCpts = Points;
        while (dCpts.Count > 1)
        {
            for (var i = 0; i < dCpts.Count - 1; i++)
            {
                dCpts[i] = new Point2D(
                x: dCpts[i].X + ((dCpts[i + 1].X - dCpts[i].X) * t),
                y: dCpts[i].Y + ((dCpts[i + 1].Y - dCpts[i].Y) * t)
                //,z: dCpts[i].Z + (dCpts[i + 1].Z - dCpts[i].Z) * t
                );
            }
            //dCpts.splice(dCpts.Count - 1, 1);
        }
        return dCpts[0];
    }

    /// <summary>
    /// The derivative.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Point2D Derivativate(double t)
    {
        var ti = 1 - t;
        double a = 0;
        double b = 0;
        double c = 0;
        var p = DerivativePoints[0];
        if (Order == 2)
        {
            p = [p[0], p[1], Point2D.Empty];
            a = ti;
            b = t;
        }
        if (Order == 3)
        {
            a = ti * ti;
            b = ti * t * 2;
            c = t * t;
        }
        var ret = new Point2D(
        x: (a * p[0].X) + (b * p[1].X) + (c * p[2].X),
        y: (a * p[0].Y) + (b * p[1].Y) + (c * p[2].Y)
        //,z: a * p[0].Z + b * p[1].Z + c * p[2].Z
        );
        return ret;
    }

    /// <summary>
    /// The normal2.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="Point3D"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Vector2D Normal(double t)
    {
        var d = Derivativate(t);
        var q = 1d / Sqrt((d.X * d.X) + (d.Y * d.Y));
        return new Vector2D(-d.Y * q, d.X * q);
    }

    ///// <summary>
    ///// The normal3.
    ///// </summary>
    ///// <param name="t">The t.</param>
    ///// <returns>The <see cref="Point3D"/>.</returns>
    ///// <acknowledgment>
    ///// http://pomax.github.io/bezierinfo/
    ///// </acknowledgment>
    //public Point3D Normal3D(double t)
    //{
    //    // see http://stackoverflow.com/questions/25453159
    //    var r1 = Derivative(t);
    //    var r2 = Derivative(t + 0.01);
    //    var q1 = Sqrt(r1.X * r1.X + r1.Y * r1.Y + r1.Z * r1.Z);
    //    var q2 = Sqrt(r2.X * r2.X + r2.Y * r2.Y + r2.Z * r2.Z);
    //    r1.X /= q1; r1.Y /= q1; r1.Z /= q1;
    //    r2.X /= q2; r2.Y /= q2; r2.Z /= q2;
    //    // cross product
    //    var c = new Point3D(
    //        x: r2.Y * r1.Z - r2.Z * r1.Y,
    //        y: r2.Z * r1.X - r2.X * r1.Z,
    //        z: r2.X * r1.Y - r2.Y * r1.X
    //    );
    //    var m = Sqrt(c.X * c.X + c.Y * c.Y + c.Z * c.Z);
    //    c.X /= m; c.Y /= m; c.Z /= m;
    //    // rotation matrix
    //    var R = new List<double> {
    //        c.X * c.X, c.X * c.Y - c.Z, c.X * c.Z + c.Y,
    //        c.X * c.Y + c.Z, c.Y * c.Y, c.Y * c.Z - c.X,
    //        c.X * c.Z - c.Y, c.Y * c.Z + c.X, c.Z * c.Z};
    //    // normal vector:
    //    var n = new Point3D(
    //        x: R[0] * r1.X + R[1] * r1.Y + R[2] * r1.Z,
    //        y: R[3] * r1.X + R[4] * r1.Y + R[5] * r1.Z,
    //        z: R[6] * r1.X + R[7] * r1.Y + R[8] * r1.Z
    //    );
    //    return n;
    //}

    /// <summary>
    /// The hull.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<Point2D> Hull(double t)
    {
        var p = Points;
        var _p = new List<Point2D>();
        Point2D pt;
        var q = new List<Point2D>();
        //var idx = 0;
        var i = 0;
        var l = 0;
        //q[idx++] = p[0];
        q.Add(p[0]);
        //q[idx++] = p[1];
        q.Add(p[1]);
        //q[idx++] = p[2];
        q.Add(p[2]);
        if (Order == 3)
        {
            //q[idx++] = p[3];
            q.Add(p[3]);
        }
        // we lerp between all points at each iteration, until we have 1 point left.
        while (p.Count > 1)
        {
            _p = [];
            for (i = 0, l = p.Count - 1; i < l; i++)
            {
                pt = Interpolators.Linear(t, p[i], p[i + 1]);
                //q[idx++] = pt;
                q.Add(pt);
                _p.Add(pt);
            }

            p = _p;
        }

        return q;
    }

    ///// <summary>
    ///// Cut a <see cref="BezierSegment2D"/> into multiple fragments at the given t indices, using "De Casteljau" algorithm.
    ///// The value at which to split the curve. Should be strictly inside ]0,1[ interval.
    ///// </summary>
    ///// <param name="t">The t.</param>
    ///// <returns>The <see cref="Array"/>.</returns>
    ///// <exception cref="ArgumentOutOfRangeException"></exception>
    ///// <acknowledgment>
    ///// http://pomax.github.io/bezierinfo/#decasteljau
    ///// https://github.com/superlloyd/Poly
    ///// </acknowledgment>
    ////[DebuggerStepThrough]
    //[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    //public (Bezier Left, Bezier Right) Split(double t)
    //{
    //    if (t < 0) t = 0;
    //    if (t > 1) t = 1;

    //    var bezier1 = new List<Point2D>();
    //    var bezier2 = new List<Point2D>();
    //    var lp = Points;

    //    while (lp.Count > 0)
    //    {
    //        bezier1.Add(lp.First());
    //        bezier2.Add(lp.Last());
    //        var next = new List<Point2D>(lp.Count - 1);
    //        for (var i = 0; i < lp.Count - 1; i++)
    //        {
    //            var p0 = lp[i];
    //            var p1 = lp[i + 1];
    //            next.Add(new Point2D((p0.X * (1 - t) + t * p1.X, p0.Y * (1 - t) + t * p1.Y)));
    //        }

    //        lp = next;
    //    }

    //    return (
    //        new Bezier(bezier1.ToList()),
    //        new Bezier(bezier2.ToList())
    //    );
    //}

    ///// <summary>
    ///// The split.
    ///// </summary>
    ///// <param name="t1">The t1.</param>
    ///// <param name="t2">The t2.</param>
    ///// <returns>The <see cref="Bezier"/>.</returns>
    ///// <acknowledgment>
    ///// http://pomax.github.io/bezierinfo/
    ///// </acknowledgment>
    ////[DebuggerStepThrough]
    //[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    //public Bezier Split(double t1, double t2)
    //{
    //    return Split(new double[] { t1, t2 })[0];
    //    //// shortcuts
    //    //if (t1 == 0 && t2 != 0) return Split(t2).Left;
    //    //if (t2 == 1) return Split(t1).Right;
    //    //// no shortcut: use "de Casteljau" iteration.
    //    //var q = Hull(t1);
    //    //var result = new Pair(
    //    //    left: Order == 2 ? new Bezier(new List<Point2D> { q[0], q[3], q[5] }) : new Bezier(new List<Point2D> { q[0], q[4], q[7], q[9] }),
    //    //    right: Order == 2 ? new Bezier(new List<Point2D> { q[5], q[4], q[2] }) : new Bezier(new List<Point2D> { q[9], q[8], q[6], q[3] }),
    //    //    span: q
    //    //);

    //    //// make sure we bind _t1/_t2 information!
    //    //result.Left.T1 = BezierUtil.Map(0, 0, 1, T1, T2);
    //    //result.Left.T2 = BezierUtil.Map(t1, 0, 1, T1, T2);
    //    //result.Right.T1 = BezierUtil.Map(t1, 0, 1, T1, T2);
    //    //result.Right.T2 = BezierUtil.Map(1, 0, 1, T1, T2);

    //    //// if we have no t2, we're done
    //    //if (t2 != 0) return result.Left;
    //    //// if we have a t2, split again:
    //    //t2 = BezierUtil.Map(t2, t1, 1, 0, 1);
    //    //var subsplit = Split(t2);
    //    //return subsplit.Left;
    //}

    /// <summary>
    /// The split.
    /// </summary>
    /// <param name="ts">The ts.</param>
    /// <returns>The <see cref="Array"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Bezier[] Split(params double[] ts)
    {
        if (ts is null)
        {
            return [new Bezier(Points)];
        }

        var filtered = ts.Where(t => t >= 0 && t <= 1).Distinct().OrderBy(t => t).ToList();

        if (filtered.Count == 0)
        {
            return [new Bezier(Points)];
        }

        var tLast = 0d;
        var prev = new Bezier(Points);
        var list = new List<Bezier>(filtered.Count + 1);
        foreach (var t in filtered)
        {
            var relT = (1 - t) / (1 - tLast);
            tLast = t;
            var cut = Split(prev.Points, relT);
            list.Add(cut[1]);
            prev = cut[0];
        }

        list.Add(prev);
        return [.. list];
    }

    /// <summary>
    /// Cut a <see cref="BezierSegment2D"/> into multiple fragments at the given t indices, using "De Casteljau" algorithm.
    /// The value at which to split the curve. Should be strictly inside ]0,1[ interval.
    /// </summary>
    /// <param name="points">The points.</param>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="Array"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/#decasteljau
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Bezier[] Split(IEnumerable<Point2D> points, double t)
    {
        if (t < 0 || t > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(t));
        }

        var bezier1 = new List<Point2D>();
        var bezier2 = new List<Point2D>();
        var lp = points.ToList();

        while (lp.Count > 0)
        {
            bezier1.Add(lp.First());
            bezier2.Add(lp.Last());
            var next = new List<Point2D>(lp.Count - 1);
            for (var i = 0; i < lp.Count - 1; i++)
            {
                var p0 = lp[i];
                var p1 = lp[i + 1];
                next.Add(new Point2D(((p0.X * (1 - t)) + (t * p1.X), (p0.Y * (1 - t)) + (t * p1.Y))));
            }

            lp = next;
        }

        return [
            new Bezier(bezier1),
            new Bezier(bezier2)
        ];
    }

    /// <summary>
    /// Cut a <see cref="Bezier"/> into multiple fragments at the given t indices, using "De Casteljau" algorithm.
    /// The value at which to split the curve. Should be strictly inside ]0,1[ interval.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="Array"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/#decasteljau
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Bezier[] Split(double t)
    {
        if (t < 0 || t > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(t));
        }

        var bezier1 = new List<Point2D>();
        var bezier2 = new List<Point2D>();
        var lp = Points;

        while (lp.Count > 0)
        {
            bezier1.Add(lp.First());
            bezier2.Add(lp.Last());
            var next = new List<Point2D>(lp.Count - 1);
            for (var i = 0; i < lp.Count - 1; i++)
            {
                var p0 = lp[i];
                var p1 = lp[i + 1];
                next.Add(new Point2D(((p0.X * (1 - t)) + (t * p1.X), (p0.Y * (1 - t)) + (t * p1.Y))));
            }

            lp = next;
        }

        return [
            new Bezier(bezier1),
            new Bezier(bezier2)
        ];
    }

    /// <summary>
    /// The offset.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<Bezier> Offset(double t)
    {
        if (Linear)
        {
            var nv = Normal(0);

            var coords = new List<Point2D>();
            foreach (var p in Points)
            {
                var ret = new Point2D(
                x: p.X + (t * nv.I),
                y: p.Y + (t * nv.J)
                //,z: p.Z + t * nv.Z
                );
                coords.Add(ret);
            }

            return [new(coords)];
        }
        var reduced = Reduce();

        return new List<Bezier>(
            from s in reduced
            select s.Scale(t)
            );
    }

    /// <summary>
    /// The offset.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <param name="d">The d.</param>
    /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public (Point2D, Point2D, Point2D) Offset(double t, double d)
    {
        var c = Interpolate(t);
        var n = Normal(t);
        return (
            c,
            (Point2D)n,
            new Point2D(
            x: c.X + (n.I * d),
            y: c.Y + (n.J * d)
            //,z: c.Z + n.Z * d
            )
        );
    }

    /// <summary>
    /// The scale.
    /// </summary>
    /// <param name="d">The d.</param>
    /// <returns>The <see cref="Bezier"/>.</returns>
    /// <exception cref="NullReferenceException">cannot scale this curve. Try reducing it first.</exception>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Bezier Scale(double d)
    {
        var order = Order;
        var clockwise = Direction;
        var r1 = d;
        var r2 = d;
        var v = new List<(Point2D, Point2D, Point2D)> { Offset(0, 10), Offset(1, 10) };
        var o = BezierUtil.Lli4(v[0].Item3, v[0].Item1, v[1].Item3, v[1].Item1);
        if (o is null)
        {
            throw new NullReferenceException("cannot scale this curve. Try reducing it first.");
        }

        // move all points by distance 'd' wrt the origin 'o'
        var points = Points;
        var np = new Point2D[order + 1];

        // move end points by fixed distance along normal.
        foreach (var t in new List<int> { 0, 1 })
        {
            var p = np[t * order] = points[t * order];
            p.X += (t == 0 ? r2 : r1) * v[t].Item2.X;
            p.Y += (t == 0 ? r2 : r1) * v[t].Item2.Y;
        }

        // move control points to lie on the intersection of the offset
        // derivative vector, and the origin-through-control vector
        foreach (var t in new List<int> { 0, 1 })
        {
            if (Order == 2)
            {
                break;
            }

            var p = np[t * order];
            var d2 = Derivativate(t);
            var p2 = new Point2D(x: p.X + d2.X, y: p.Y + d2.Y/*, z: p.Z + d2.Z*/);
            np[t + 1] = BezierUtil.Lli4(p, p2, o, points[t + 1]);
        }

        return new Bezier([.. np]);
    }

    /// <summary>
    /// The scale.
    /// </summary>
    /// <param name="distanceFn">The distanceFn.</param>
    /// <returns>The <see cref="Bezier"/>.</returns>
    /// <exception cref="NullReferenceException">cannot scale this curve. Try reducing it first.</exception>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Bezier Scale(DerivitiveMethodDouble distanceFn)
    {
        if (distanceFn is null) return null;
        var order = Order;
        if (order == 2)
        {
            return RaiseToPower().Scale(distanceFn);
        }

        // ToDo: add special handling for degenerate (=linear) curves.
        var direction = Direction;
        var r1 = distanceFn(0);
        var r2 = distanceFn(1);
        var v = new List<(Point2D, Point2D, Point2D)> { Offset(0, 10), Offset(1, 10) };
        var o = BezierUtil.Lli4(v[0].Item3, v[0].Item1, v[1].Item3, v[1].Item1);
        if (o is null)
        {
            throw new NullReferenceException("cannot scale this curve. Try reducing it first.");
        }

        // move all points by distance 'd' wrt the origin 'o'
        var points = Points;
        var np = new List<Point2D>();

        // move end points by fixed distance along normal.
        foreach (var t in new List<int> { 0, 1 })
        {
            var p = np[t * order] = points[t * order];
            p.X += (t == 0 ? r2 : r1) * v[t].Item2.X;
            p.Y += (t == 0 ? r2 : r1) * v[t].Item2.Y;
        }

        // move control points by "however much necessary to
        // ensure the correct tangent to endpoint".
        foreach (var t in new List<int> { 0, 1 })
        {
            if (Order == 2)
            {
                break;
            }

            var p = points[t + 1];
            var ov = new Point2D(
                    x: p.X - o.X,
                    y: p.Y - o.Y
                    /*,z: p.Z - o.Z*/);
            var rc = distanceFn((t + 1) / order);
            if (direction != RotationDirection.Clockwise)
            {
                rc = -rc;
            }

            var m = Sqrt((ov.X * ov.X) + (ov.Y * ov.Y));
            ov.X /= m;
            ov.Y /= m;
            np[t + 1] = new Point2D(
                x: p.X + (rc * ov.X),
                y: p.Y + (rc * ov.Y)
                /*,z: p.Z + rc * ov.Z*/);
        }

        return new Bezier(np);
    }

    /// <summary>
    /// The outline.
    /// </summary>
    /// <param name="d1">The d1.</param>
    /// <returns>The <see cref="PolyBezier2"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public PolyBezier2 Outline(double d1) => Outline(d1, d1, 0, 0, true);

    /// <summary>
    /// The outline.
    /// </summary>
    /// <param name="d1">The d1.</param>
    /// <param name="d2">The d2.</param>
    /// <returns>The <see cref="PolyBezier2"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public PolyBezier2 Outline(double d1, double d2) => Outline(d1, d2, 0, 0, true);

    /// <summary>
    /// The outline.
    /// </summary>
    /// <param name="d1">The d1.</param>
    /// <param name="d3">The d3.</param>
    /// <param name="d4">The d4.</param>
    /// <returns>The <see cref="PolyBezier2"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public PolyBezier2 Outline(double d1, double d3, double d4) => Outline(d1, d1, d3, d4, false);

    /// <summary>
    /// The outline.
    /// </summary>
    /// <param name="d1">The d1.</param>
    /// <param name="d2">The d2.</param>
    /// <param name="d3">The d3.</param>
    /// <param name="d4">The d4.</param>
    /// <param name="graduated">The graduated.</param>
    /// <returns>The <see cref="PolyBezier2"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public PolyBezier2 Outline(double d1, double d2, double d3, double d4, bool graduated = false)
    {
        var reduced = Reduce();
        var len = reduced.Count;
        var fcurves = new List<Bezier>();
        var bcurves = new List<Bezier>();
        List<Point2D> p;
        double alen = 0;
        double slen = 0;
        var tlen = Length;

        // form curve outlines
        foreach (var segment in reduced)
        {
            slen = segment.Length;
            if (graduated)
            {
                fcurves.Add(segment.Scale(BezierUtil.LinearDistanceFunction(d1, d3, tlen, alen, slen)));
                bcurves.Add(segment.Scale(BezierUtil.LinearDistanceFunction(-d2, -d4, tlen, alen, slen)));
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
        foreach (var s in bcurves)
        {
            p = s.Points;
            s.Points = p[3] is not null ? [p[3], p[2], p[1], p[0]] : [p[2], p[1], p[0]];
            tcurves.Add(s);
        }
        tcurves.Reverse();
        bcurves = tcurves;

        var segments = new List<Bezier>();

        // form the endcaps as lines
        var fs = fcurves[0].Points[0];
        var fe = fcurves[len - 1].Points[^1];
        var bs = bcurves[len - 1].Points[^1];
        var be = bcurves[0].Points[0];
        var ls = BezierUtil.MakeLine(bs, fs);
        var le = BezierUtil.MakeLine(fe, be);
        segments.Add(ls);
        segments.AddRange(fcurves);
        segments.Add(le);
        segments.AddRange(bcurves);
        slen = segments.Count;

        return new PolyBezier2(segments);
    }

    /// <summary>
    /// The outlineshapes.
    /// </summary>
    /// <param name="d1">The d1.</param>
    /// <param name="d2">The d2.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<Shape1> Outlineshapes(double d1, double d2)
    {
        //d2 = d2 || d1;
        var outline = Outline(d1, d2).Curves;
        var shapes = new List<Shape1>();
        for (int i = 1, len = outline.Count; i < len / 2; i++)
        {
            var shape = BezierUtil.MakeShape(outline[i], outline[len - i]);
            shape.Startcap.Virtual = i > 1;
            shape.Endcap.Virtual = i < (len / 2) - 1;
            shapes.Add(shape);
        }
        return shapes;
    }

    /// <summary>
    /// The arcs.
    /// </summary>
    /// <param name="errorThreshold">The errorThreshold.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<Arc2D> Arcs(double errorThreshold = 0.5d)
    {
        //errorThreshold = errorThreshold || 0.5;
        var circles = new List<Arc2D>();
        return Iterate(errorThreshold, circles);
    }

    /// <summary>
    /// The iterate.
    /// </summary>
    /// <param name="errorThreshold">The errorThreshold.</param>
    /// <param name="circles">The circles.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<Arc2D> Iterate(double errorThreshold, List<Arc2D> circles)
    {
        var s = 0d;
        var e = 1d;

        // we do a binary search to find the "good `t` closest to no-longer-good"
        do
        {
            var safety = 0d;

            // step 1: start with the maximum possible arc
            e = 1d;

            // points:
            var np1 = Interpolate(s);
            Point2D np2;
            Point2D np3;
            var arc = new Arc2D();
            Arc2D prev_arc;

            // booleans:
            bool curr_good = false, done;

            // numbers:
            double m = e, prev_e = 1, step = 0;

            // step 2: find the best possible arc
            do
            {
                var prev_good = curr_good;
                prev_arc = arc;
                m = (s + e) / 2d;
                step++;

                np2 = Interpolate(m);
                np3 = Interpolate(e);

                arc = BezierUtil.Getccenter(np1, np2, np3);
                var error = Error(arc, np1, s, e);
                curr_good = error <= errorThreshold;

                done = prev_good && !curr_good;
                if (!done)
                {
                    prev_e = e;
                }

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
                    e += (e - s) / 2d;
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

            prev_arc ??= arc;
            circles?.Add(prev_arc);
            s = prev_e;
        }
        while (e < 1);
        return circles;
    }

    /// <summary>
    /// The error.
    /// </summary>
    /// <param name="pc">The pc.</param>
    /// <param name="np1">The np1.</param>
    /// <param name="s">The s.</param>
    /// <param name="e">The e.</param>
    /// <returns>The <see cref="double"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public double Error(Arc2D pc, Point2D np1, double s, double e)
    {
        var q = (e - s) / 4d;
        var c1 = Interpolate(s + q);
        var c2 = Interpolate(e - q);
        var reff = Measurements.Distance(pc.Center, np1);
        var d1 = Measurements.Distance(pc.Center, c1);
        var d2 = Measurements.Distance(pc.Center, c2);
        return Abs(d1 - reff) + Abs(d2 - reff);
    }

    /// <summary>
    /// The raise.
    /// </summary>
    /// <returns>The <see cref="Bezier"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Bezier RaiseToPower()
    {
        var p = Points;
        var np = new List<Point2D>(Points.Count) { p[0] };
        var k = p.Count;
        Point2D pi;
        Point2D pim;
        for (var i = 1; i < k; i++)
        {
            pi = p[i];
            pim = p[i - 1];
            np[i] = new Point2D(
                x: ((k - i) / k * pi.X) + (i / k * pim.X),
                y: ((k - i) / k * pi.Y) + (i / k * pim.Y)
            //,z: (k - i) / k * pi.Z + i / k * pim.Z
            );
        }
        np[k] = p[k - 1];
        return new Bezier(np);
    }

    /// <summary>
    /// The simple.
    /// </summary>
    /// <returns>The <see cref="bool"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Simple()
    {
        if (Order == 3)
        {
            var a1 = BezierUtil.Angle(Points[0], Points[3], Points[1]);
            var a2 = BezierUtil.Angle(Points[0], Points[3], Points[2]);
            if (a1 > 0 && a2 < 0 || a1 < 0 && a2 > 0)
            {
                return false;
            }
        }
        var n1 = Normal(0);
        var n2 = Normal(1);
        var s = (n1.I * n2.I) + (n1.J * n2.J)/* + n1.K * n2.K*/;
        var angle = Abs(Acos(s));
        return angle < PI / 3d;
    }

    /// <summary>
    /// The bbox.
    /// </summary>
    /// <returns>The <see cref="BBox"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public BBox Bbox()
    {
        var extrema = Extrema;
        return new BBox(
            BezierUtil.GetMinMax(this, 0, extrema),
            BezierUtil.GetMinMax(this, 1, extrema),
            BezierUtil.GetMinMax(this, 2, extrema)
            );
    }

    /// <summary>
    /// The reduce.
    /// </summary>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<Bezier> Reduce()
    {
        int i;
        const double step = 0.01;
        Bezier segment;
        var pass1 = new List<Bezier>();
        var pass2 = new List<Bezier>();

        // first pass: split on extrema
        var extrema = Extrema;
        if (extrema.IndexOf(0) == -1)
        {
            extrema.Insert(0, 0);
        }

        if (extrema.IndexOf(1) == -1)
        {
            extrema.Add(1);
        }

        //extrema.Sort();
        extrema.Reverse();
        double t1;
        double t2;
        for (t1 = extrema[0], i = 1; i < extrema.Count; i++)
        {
            t2 = extrema[i];
            var s = Split(t1, t2);
            segment = s[1];
            segment.T1 = t1;
            segment.T2 = t2;
            pass1.Add(segment);
            t1 = t2;
        }

        // Second pass: further reduce these segments to simple segments
        foreach (var p1 in pass1)
        {
            t1 = 0;
            t2 = 0;
            while (t2 <= 1)
            {
                for (t2 = t1 + step; t2 <= 1 + step; t2 += step)
                {
                    {
                        var s = p1.Split(t1, t2);
                        segment = s[1];
                    }
                    if (!segment.Simple())
                    {
                        t2 -= step;
                        if (Abs(t1 - t2) < step)
                        {
                            // we can never form a reduction
                            return [];
                        }
                        var s = p1.Split(t1, t2);
                        segment = s[1];
                        segment.T1 = BezierUtil.Map(t1, 0, 1, p1.T1, p1.T2);
                        segment.T2 = BezierUtil.Map(t2, 0, 1, p1.T1, p1.T2);
                        pass2.Add(segment);
                        t1 = t2;
                        break;
                    }
                }
            }
            if (t1 < 1)
            {
                var s = p1.Split(t1, 1);
                segment = s[1];
                segment.T1 = BezierUtil.Map(t1, 0, 1, p1.T1, p1.T2);
                segment.T2 = p1.T2;
                pass2.Add(segment);
            }
        }

        return pass2;
    }
    #endregion Bezier Methods

    #region Intersection Methods
    /// <summary>
    /// The overlaps.
    /// </summary>
    /// <param name="curve">The curve.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Overlaps(Bezier curve)
    {
        var lbbox = Bbox();
        var tbbox = curve?.Bbox();
        return BezierUtil.Bboxoverlap(lbbox, tbbox);
    }

    /// <summary>
    /// The intersects.
    /// </summary>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<Pair> Intersects() => Selfintersects();

    /// <summary>
    /// The intersects.
    /// </summary>
    /// <param name="curve">The curve.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<Pair> Intersects(Bezier curve) => Curveintersects(Reduce(), curve?.Reduce());

    /// <summary>
    /// The intersects.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<bool> Intersects(LineSegment2D line) => LineIntersects(line);

    /// <summary>
    /// The line intersects.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<bool> LineIntersects(LineSegment2D line)
    {
        var mx = Min(line.A.X, line.B.X);
        var my = Min(line.A.Y, line.B.Y);
        var MX = Max(line.A.X, line.B.X);
        var MY = Max(line.A.Y, line.B.Y);
        var self = this;

        return new List<bool>(
            from t in BezierUtil.Roots(Points, line)
            let p = self.Interpolate(t)

            select Intersections.ApproximatelyBetween(p.X, mx, MX) && Intersections.ApproximatelyBetween(p.Y, my, MY));
    }

    /// <summary>
    /// The selfintersects.
    /// </summary>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<Pair> Selfintersects()
    {
        var reduced = Reduce();
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
    /// The curveintersects.
    /// </summary>
    /// <param name="c1">The c1.</param>
    /// <param name="c2">The c2.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static List<Pair> Curveintersects(List<Bezier> c1, List<Bezier> c2)
    {
        var pairs = new List<Pair>();
        // step 1: pair off any overlapping segments
        foreach (var l in c1)
        {
            foreach (var r in c2)
            {
                if (l.Overlaps(r))
                {
                    pairs.Add(new Pair(left: l, right: r));
                }
            }
        }

        // step 2: for each pairing, run through the convergence algorithm.
        var intersections = new List<Pair>();
        foreach (var pair in pairs)
        {
            var result = BezierUtil.Pairiteration(pair.Left, pair.Right);
            if (result.Count > 0)
            {
                intersections.AddRange(result);
            }
        }
        return intersections;
    }

    /// <summary>
    /// The cubic bezier cardano intersection.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<double> CubicBezierCardanoIntersection(LineSegment2D line) => CubicBezierCardanoIntersection(Points[0], Points[1], Points[2], Points[3], line);

    /// <summary>
    /// The cubic bezier cardano intersection.
    /// </summary>
    /// <param name="p1">The p1.</param>
    /// <param name="p2">The p2.</param>
    /// <param name="p3">The p3.</param>
    /// <param name="p4">The p4.</param>
    /// <param name="line">The line.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static List<double> CubicBezierCardanoIntersection(Point2D p1, Point2D p2, Point2D p3, Point2D p4, LineSegment2D line)
    {
        // align curve with the intersecting line, translating/rotating
        // so that the first point becomes (0,0), and the last point
        // ends up lying on the line we're trying to use as root-intersect.
        var aligned = BezierUtil.AlignPoints([p1, p2, p3, p4], line);
        // rewrite from [a(1-t)^3 + 3bt(1-t)^2 + 3c(1-t)t^2 + dt^3] form...
        var pa = aligned[0].Y;
        var pb = aligned[1].Y;
        var pc = aligned[2].Y;
        var pd = aligned[3].Y;
        // ...to [t^3 + at^2 + bt + c] form:
        var d = -pa + (3 * pb) - (3 * pc) + pd;
        var a = ((3 * pa) - (6 * pb) + (3 * pc)) / d;
        var b = ((-3 * pa) + (3 * pb)) / d;
        var c = pa / d;
        // then, determine p and q:
        var p = ((3 * b) - (a * a)) / 3;
        var dp3 = p / 3;
        var q = ((2 * a * a * a) - (9 * a * b) + (27 * c)) / 27;
        var q2 = q / 2;
        // and determine the discriminant:
        var discriminant = (q2 * q2) + (dp3 * dp3 * dp3);
        // and some reserved variables for later
        double u1, v1, x1, x2, x3;

        // If the discriminant is negative, use polar coordinates
        // to get around square roots of negative numbers
        if (discriminant < 0)
        {
            var mp3 = -p / 3;
            var mp33 = mp3 * mp3 * mp3;
            var r = Sqrt(mp33);
            var t = -q / (2 * r);

            // deal with IEEE rounding yielding <-1 or >1
            var cosphi = t < -1 ? -1 : t > 1 ? 1 : t;
            var phi = Acos(cosphi);
            var crtr = CubeRoot(r);
            var t1 = 2 * crtr;
            x1 = (t1 * Cos(phi / 3)) - (a / 3);
            x2 = (t1 * Cos((phi + Tau) / 3)) - (a / 3);
            x3 = (t1 * Cos((phi + (2 * Tau)) / 3)) - (a / 3);
            return [x1, x2, x3];
        }
        else if (discriminant == 0)
        {
            u1 = q2 < 0 ? CubeRoot(-q2) : -CubeRoot(q2);
            x1 = (2 * u1) - (a / 3);
            x2 = -u1 - (a / 3);
            return [x1, x2];
        }
        else
        {
            // one real root, and two imaginary roots
            var sd = Sqrt(discriminant);
            u1 = CubeRoot(-q2 + sd);
            v1 = CubeRoot(q2 + sd);
            x1 = u1 - v1 - (a / 3);
            return [x1];
        }
    }
    #endregion Intersection Methods

    #region Standard Methods
    /// <summary>
    /// Get the hash code.
    /// </summary>
    /// <returns>The <see cref="int"/>.</returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override int GetHashCode()
    {
        var hashcode = 0;
        foreach (var point in Points)
        {
            hashcode ^= point.X.GetHashCode();
            hashcode ^= point.Y.GetHashCode();
            //hashcode ^= point.Z.GetHashCode();
        }
        return hashcode;
    }

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Equals(Bezier left, Bezier right)
    {
        if (left?.Points.Count != right?.Points.Count)
        {
            return false;
        }

        var equals = false;
        for (var i = 0; i < left.Points.Count; i++)
        {
            equals &= left.Points[i].X == right.Points[i].X;
            equals &= left.Points[i].Y == right.Points[i].Y;
            //equals &= left.Points[i].Z == right.Points[i].Z;
            if (!equals)
            {
                break;
            }
        }

        return equals;
    }

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals(object obj) => obj is Bezier bezier && Equals(this, bezier);

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="GraphicsObject"/> inherited class.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString() => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="GraphicsObject"/> inherited class based on the IFormatProvider
    /// passed in.  If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="formatProvider">ToDo: describe provider parameter on ToString</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(IFormatProvider formatProvider) => ConvertToString(string.Empty /* format string */, formatProvider);

    /// <summary>
    /// Creates a string representation of this <see cref="GraphicsObject"/> inherited class based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(string format, IFormatProvider formatProvider) => ConvertToString(format /* format string */, formatProvider /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="CubicBezier2D"/> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ConvertToString(string format, IFormatProvider formatProvider)
    {
        if (this is null)
        {
            return nameof(Bezier);
        }

        var sep = Tokenizer.GetNumericListSeparator(formatProvider);
        IFormattable formatable = $"{nameof(Bezier)}={{A={Points[0]}{sep}B={Points[0]}{sep}C={Points[0]}{sep}D={Points[0]}}}";
        return formatable.ToString(format, formatProvider);
    }
    #endregion Standard Methods
}
