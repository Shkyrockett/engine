﻿// <copyright file="CubicBezier2D.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Operations;
using static Engine.Polynomials;
using static System.Math;

namespace Engine;

/// <summary>
/// Cubic or 3rd degree Bézier curve.
/// </summary>
/// <structure>Engine.Geometry.CubicBezier2D</structure>
/// <remarks>
/// <para>http://paulbourke.net/geometry/bezier/index.html
/// http://pomax.github.io/bezierinfo/</para>
/// </remarks>
[DataContract, Serializable]
[GraphicsObject]
[DisplayName(nameof(CubicBezier2D))]
[XmlType(TypeName = "Bezier-Cubic")]
[DebuggerDisplay("{ToString()}")]
public class CubicBezier2D
    : Shape2D, IEquatable<CubicBezier2D>
{
    #region Fields
    /// <summary>
    /// Position 1 x-coordinate.
    /// </summary>
    private double ax;

    /// <summary>
    /// Position 1 y-coordinate.
    /// </summary>
    private double ay;

    /// <summary>
    /// Tangent 1 x-coordinate.
    /// </summary>
    private double bx;

    /// <summary>
    /// Tangent 1 y-coordinate.
    /// </summary>
    private double by;

    /// <summary>
    /// Tangent 2 x-coordinate.
    /// </summary>
    private double cx;

    /// <summary>
    /// Tangent 2 y-coordinate.
    /// </summary>
    private double cy;

    /// <summary>
    /// Position 2 x-coordinate.
    /// </summary>
    private double dx;

    /// <summary>
    /// Position 2 y-coordinate.
    /// </summary>
    private double dy;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="CubicBezier2D"/> class.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public CubicBezier2D()
        : this(0, 0, 0, 0, 0, 0, 0, 0)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CubicBezier2D"/> class from a <see cref="QuadraticBezier2D"/>.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public CubicBezier2D(Point2D a, Point2D b, Point2D c)
    {
        (ax, ay, bx, by, cx, cy, dx, dy) = Conversions.QuadraticBezierToCubicBezierTuple(a.X, a.Y, b.X, b.Y, c.X, c.Y);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CubicBezier2D"/> class from a <see cref="QuadraticBezier2D"/>.
    /// </summary>
    /// <param name="tuple"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public CubicBezier2D((double aX, double aY, double bX, double bY, double cX, double cY) tuple)
        : this(tuple.aX, tuple.aY, tuple.bX, tuple.bY, tuple.cX, tuple.cY)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CubicBezier2D"/> class from a <see cref="QuadraticBezier2D"/>.
    /// </summary>
    /// <param name="ax"></param>
    /// <param name="ay"></param>
    /// <param name="bx"></param>
    /// <param name="by"></param>
    /// <param name="cx"></param>
    /// <param name="cy"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public CubicBezier2D(double ax, double ay, double bx, double by, double cx, double cy)
    {
        (this.ax, this.ay, this.bx, this.by, this.cx, this.cy, dx, dy) = Conversions.QuadraticBezierToCubicBezierTuple(ax, ay, bx, by, cx, cy);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CubicBezier2D"/> class.
    /// </summary>
    /// <param name="a">Position1</param>
    /// <param name="b">Tangent1</param>
    /// <param name="c">Position2</param>
    /// <param name="d">Tangent2</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public CubicBezier2D(Point2D a, Point2D b, Point2D c, Point2D d)
        : this(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CubicBezier2D"/> class.
    /// </summary>
    /// <param name="ax">Position1</param>
    /// <param name="ay">Position1</param>
    /// <param name="bx">Tangent1</param>
    /// <param name="by">Tangent1</param>
    /// <param name="cx">Position2</param>
    /// <param name="cy">Position2</param>
    /// <param name="dx">Tangent2</param>
    /// <param name="dy">Tangent2</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public CubicBezier2D(double ax, double ay, double bx, double by, double cx, double cy, double dx, double dy)
    {
        this.ax = ax;
        this.ay = ay;
        this.bx = bx;
        this.by = by;
        this.cx = cx;
        this.cy = cy;
        this.dx = dx;
        this.dy = dy;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CubicBezier2D"/> class.
    /// </summary>
    /// <param name="tuple"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public CubicBezier2D((double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY) tuple)
        : this(tuple.aX, tuple.aY, tuple.bX, tuple.bY, tuple.cX, tuple.cY, tuple.dX, tuple.dY)
    { }
    #endregion Constructors

    #region Deconstructors
    /// <summary>
    /// Deconstruct this <see cref="CubicBezier2D"/> to a Tuple.
    /// </summary>
    /// <param name="ax"></param>
    /// <param name="ay"></param>
    /// <param name="bx"></param>
    /// <param name="by"></param>
    /// <param name="cx"></param>
    /// <param name="cy"></param>
    /// <param name="dx"></param>
    /// <param name="dy"></param>
    public void Deconstruct(out double ax, out double ay, out double bx, out double by, out double cx, out double cy, out double dx, out double dy)
    {
        ax = this.ax;
        ay = this.ay;
        bx = this.bx;
        by = this.by;
        cx = this.cx;
        cy = this.cy;
        dx = this.dx;
        dy = this.dy;
    }
    #endregion Deconstructors

    #region Properties
    /// <summary>
    /// Gets or sets a list of points representing the handles of the <see cref="CubicBezier2D"/> curve.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(ExpandableCollectionConverter))]
    public List<Point2D> Points
    {
        get { return [A, B, C, D]; }
        set
        {
            if (value is not null)
            {
                A = value[0];
                B = value[1];
                C = value[2];
                D = value[3];
            }
        }
    }

    /// <summary>
    /// Gets or sets the starting node for the <see cref="CubicBezier2D"/> curve.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Category("Elements")]
    [Description("The first Point of a Cubic Bézier.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Point2DConverter))]
    public Point2D A
    {
        get { return new Point2D(ax, ay); }
        set
        {
            ax = value.X;
            ay = value.Y;
            ClearCache();
            OnPropertyChanged(nameof(A));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the X coordinate of the first Point of a Cubic Bézier.
    /// </summary>
    [XmlAttribute(nameof(ax))]
    [Browsable(false)]
    [Category("Elements")]
    [Description("The X coordinate of the first Point of a Cubic Bézier.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double AX
    {
        get { return ax; }
        set
        {
            ax = value;
            ClearCache();
            OnPropertyChanged(nameof(AX));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the Y coordinate of the first Point of a Cubic Bézier.
    /// </summary>
    [XmlAttribute(nameof(ay))]
    [Browsable(false)]
    [Category("Elements")]
    [Description("The y coordinate of the first Point of a Cubic Bézier.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double AY
    {
        get { return ay; }
        set
        {
            ay = value;
            ClearCache();
            OnPropertyChanged(nameof(AY));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the first middle tangent control node for the <see cref="CubicBezier2D"/> curve.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Category("Elements")]
    [Description("The second Point of a Cubic Bézier.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Point2DConverter))]
    public Point2D B
    {
        get { return new Point2D(bx, by); }
        set
        {
            bx = value.X;
            by = value.Y;
            ClearCache();
            OnPropertyChanged(nameof(B));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the X coordinate of the second Point of a Cubic Bézier.
    /// </summary>
    [XmlAttribute(nameof(bx))]
    [Browsable(false)]
    [Category("Elements")]
    [Description("The X coordinate of the second Point of a Cubic Bézier.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double BX
    {
        get { return bx; }
        set
        {
            bx = value;
            ClearCache();
            OnPropertyChanged(nameof(BX));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the Y coordinate of the second Point of a Cubic Bézier.
    /// </summary>
    [XmlAttribute(nameof(by))]
    [Browsable(false)]
    [Category("Elements")]
    [Description("The y coordinate of the second Point of a Cubic Bézier.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double BY
    {
        get { return by; }
        set
        {
            by = value;
            ClearCache();
            OnPropertyChanged(nameof(BY));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the second middle tangent control node for the <see cref="CubicBezier2D"/> curve.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Category("Elements")]
    [Description("The third Point of a Cubic Bézier.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Point2DConverter))]
    public Point2D C
    {
        get { return new Point2D(cx, cy); }
        set
        {
            cx = value.X;
            cy = value.Y;
            ClearCache();
            OnPropertyChanged(nameof(C));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the X coordinate of the third Point of a Cubic Bézier.
    /// </summary>
    [XmlAttribute(nameof(cx))]
    [Browsable(false)]
    [Category("Elements")]
    [Description("The X coordinate of the third Point of a Cubic Bézier.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double CX
    {
        get { return cx; }
        set
        {
            bx = value;
            ClearCache();
            OnPropertyChanged(nameof(CX));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the Y coordinate of the third Point of a Cubic Bézier.
    /// </summary>
    [XmlAttribute(nameof(cy))]
    [Browsable(false)]
    [Category("Elements")]
    [Description("The y coordinate of the third Point of a Cubic Bézier.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double CY
    {
        get { return cy; }
        set
        {
            by = value;
            ClearCache();
            OnPropertyChanged(nameof(CY));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the closing node for the <see cref="CubicBezier2D"/> curve.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Point2DConverter))]
    public Point2D D
    {
        get { return new Point2D(dx, dy); }
        set
        {
            dx = value.X;
            dy = value.Y;
            ClearCache();
            OnPropertyChanged(nameof(D));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the X coordinate of the fourth Point of a Cubic Bézier.
    /// </summary>
    [XmlAttribute(nameof(dx))]
    [Browsable(false)]
    [Category("Elements")]
    [Description("The X coordinate of the fourth Point of a Cubic Bézier.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double DX
    {
        get { return dx; }
        set
        {
            bx = value;
            ClearCache();
            OnPropertyChanged(nameof(DX));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the Y coordinate of the fourth Point of a Cubic Bézier.
    /// </summary>
    [XmlAttribute(nameof(dy))]
    [Browsable(false)]
    [Category("Elements")]
    [Description("The y coordinate of the fourth Point of a Cubic Bézier.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double DY
    {
        get { return dy; }
        set
        {
            by = value;
            ClearCache();
            OnPropertyChanged(nameof(DY));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets the axial aligned bounding box of the <see cref="CubicBezier2D"/> curve.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [ReadOnly(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Rectangle2DConverter))]
    public override Rectangle2D Bounds
        => (Rectangle2D)CachingProperty(() => Measurements.BezierBounds(CurveX, CurveY));

    /// <summary>
    /// An approximation of the length of a <see cref="CubicBezier2D"/> curve.
    /// </summary>
    /// <returns></returns>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public double Length
        => (double)CachingProperty(() => Measurements.CubicBezierArcLength(ax, ay, bx, by, cx, cy, dx, dy));

    /// <summary>
    /// Gets the perimeter length of the <see cref="CubicBezier2D"/> curve.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public override double Perimeter
        => Length;

    /// <summary>
    /// Gets the Polynomial degree of the <see cref="CubicBezier2D"/> curve.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public static PolynomialDegree Degree
        => PolynomialDegree.Cubic;

    /// <summary>
    /// Gets the <see cref="CubicBezier2D"/> curve's polynomial representation along the x-axis.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Polynomial<double> CurveX
    {
        get
        {
            var curveX = (Polynomial<double>)CachingProperty(() => (Polynomial<double>)CubicBezierBernsteinBasis(dx, cx, bx, ax));
            curveX.IsReadonly = true;
            return curveX;
        }
    }

    /// <summary>
    /// Gets the <see cref="CubicBezier2D"/> curve's polynomial representation along the y-axis.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Polynomial<double> CurveY
    {
        get
        {
            var curveY = (Polynomial<double>)CachingProperty(() => (Polynomial<double>)CubicBezierBernsteinBasis(dy, cy, by, ay));
            curveY.IsReadonly = true;
            return curveY;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the curve is simple.
    /// </summary>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public bool IsSimple
    {
        get
        {
            return (bool)CachingProperty(() => isSimple());

            bool isSimple()
            {
                if (Degree == PolynomialDegree.Cubic)
                {
                    var a1 = AngleVector(Points[0].X, Points[0].Y, Points[3].X, Points[3].Y, Points[1].X, Points[1].Y);
                    var a2 = AngleVector(Points[0].X, Points[0].Y, Points[3].X, Points[3].Y, Points[2].X, Points[2].Y);
                    if (a1 > 0 && a2 < 0 || a1 < 0 && a2 > 0)
                    {
                        return false;
                    }
                }
                var n1 = Normal(0);
                var n2 = Normal(1);
                var s = (n1.I * n2.I) + (n1.J * n2.J);
                var angle = Abs(Acos(s));
                return angle < PI / 3d;
            }
        }
    }

    /// <summary>
    /// Gets the derivative coordinates.
    /// </summary>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public IList<IList<Point2D>> DerivativeCoordinates
    {
        get
        {
            return (IList<IList<Point2D>>)CachingProperty(() => DerivativeCoordinates());

            IList<IList<Point2D>> DerivativeCoordinates()
            {
                // One-time compute of derivative coordinates
                var derivitivePoints = new List<IList<Point2D>>();
                var p = Points;
                for (int d = p.Count, c = d - 1; d > 1; d--, c--)
                {
                    var list = new List<Point2D>();
                    for (var j = 0; j < c; j++)
                    {
                        var dpt = new Point2D(
                        x: c * (p[j + 1].X - p[j].X),
                        y: c * (p[j + 1].Y - p[j].Y)
                        //,z: c * (p[j + 1].Z - p[j].Z)
                        );

                        list.Add(dpt);
                    }

                    derivitivePoints.Add(list);
                    p = list;
                }

                return derivitivePoints;
            }
        }
    }

    /// <summary>
    /// Gets the extrema.
    /// </summary>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public IList<double> Extrema
    {
        get
        {
            return (IList<double>)CachingProperty(() => Extrema(DerivativeCoordinates));

            // ToDo: What are DRoots?
            static List<double> Extrema(IList<IList<Point2D>> derivativeCoordinates)
            {
                var p = (from a in derivativeCoordinates[0] select a.X).ToList();
                var result = new List<double>(DRoots(p));
                p = (from a in derivativeCoordinates[0] select a.Y).ToList();
                result.AddRange(DRoots(p));
                p = (from a in derivativeCoordinates[1] select a.X).ToList();
                result.AddRange(DRoots(p));
                p = (from a in derivativeCoordinates[1] select a.Y).ToList();
                result.AddRange(DRoots(p));

                result = result.Where((t) => { return t >= 0 && t <= 1; }).ToList();
                result.Sort();
                return result;
            }
        }
    }

    /// <summary>
    /// Gets the inflections.
    /// </summary>
    /// <acknowledgment>
    /// https://pomax.github.io/bezierinfo/#inflections
    /// </acknowledgment>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public IList<double> Inflections
    {
        get
        {
            return (IList<double>)CachingProperty(() => Inflections(Points));
            static IList<double> Inflections(IList<Point2D> points)
            {
                var p = Conversions.AlignPoints(points.ToArray(), points[0].X, points[0].Y, points[3].X, points[3].Y);
                var a = p[2].X * p[1].Y;
                var b = p[3].X * p[1].Y;
                var c = p[1].X * p[2].Y;
                var d = p[3].X * p[2].Y;
                var v1 = 18 * ((-3 * a) + (2 * b) + (3 * c) - d);
                var v2 = 18 * ((3 * a) - b - (3 * c));
                var v3 = 18 * (c - a);

                if (Approximately(v1, 0))
                {
                    return Array.Empty<double>();
                }

                var descriminant = (v2 * v2) - (4 * v1 * v3);
                var sq = Sqrt(descriminant);
                d = 2 * v1;

                return Approximately(d, 0)
                    ? []
                    : new List<double>(
                    from r in new double[] { (sq - v2) / d, -(v2 + sq) / d }
                    where 0 <= r && r <= 1
                    select r
                    );
            }
        }
    }

    /// <summary>
    /// The reduce.
    /// </summary>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public IList<CubicBezier2D> Reduction
    {
        get
        {
            return (IList<CubicBezier2D>)CachingProperty(() => Reduce());

            IList<CubicBezier2D> Reduce()
            {
                int i;
                double t1 = 0, t2 = 0;
                const double step = 0.01;
                CubicBezier2D segment;
                var pass1 = new List<CubicBezier2D>();
                var pass2 = new List<CubicBezier2D>();

                // first pass: split on extrema
                var extrema = Extrema.ToList();
                if (extrema.IndexOf(0) == -1)
                {
                    extrema.Insert(0, 0);
                }

                if (extrema.IndexOf(1) == -1)
                {
                    extrema.Add(1);
                }

                t1 = extrema[0];
                for (i = 1; i < extrema.Count; i++)
                {
                    t2 = extrema[i];

                    // For some reason my splitting method produces the same starting values, but all other nodes are different, and the middle ones are in different orders.
                    var s = this.Split(t1, t2)[1];

                    segment = new CubicBezier2D(
                        s.Points[0],
                        s.Points[1],
                        s.Points[2],
                        s.Points[3]);
                    // segment,T1 and segment.T2 appear to be for debugging purposes. 
                    //segment.T1 = t1;
                    //segment.T2 = t2;
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
                                var a = p1.Split(t2, t1);
                                var s = a[1];
                                segment = new CubicBezier2D(
                                    s.Points[0],
                                    s.Points[1],
                                    s.Points[2],
                                    s.Points[3]);
                            }

                            if (!segment.IsSimple)
                            {
                                t2 -= step;
                                if (Abs(t1 - t2) < step)
                                {
                                    // we can never form a reduction
                                    return new List<CubicBezier2D>();
                                }
                                var a = p1.Split(t2, t1);
                                var s = a[1];
                                segment = new CubicBezier2D(
                                    s.Points[0],
                                    s.Points[1],
                                    s.Points[2],
                                    s.Points[3]);
                                // segment,T1 and segment.T2 appear to be for debugging purposes. 
                                //segment.T1 = BezierUtil.Map(t1, 0, 1, p1.T1, p1.T2);
                                //segment.T2 = BezierUtil.Map(t2, 0, 1, p1.T1, p1.T2);
                                pass2.Add(segment);
                                t1 = t2;
                                break;
                            }
                        }
                    }
                    if (t1 < 1)
                    {
                        var a = p1.Split(1, t1);
                        var s = a[1];
                        segment = new CubicBezier2D(
                            s.Points[0],
                            s.Points[1],
                            s.Points[2],
                            s.Points[3]);
                        //segment.T1 = BezierUtil.Map(t1, 0, 1, p1.T1, p1.T2);
                        //segment.T2 = p1.T2;
                        pass2.Add(segment);
                    }
                }

                return pass2;
            }
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator ==(CubicBezier2D left, CubicBezier2D right) => left.Equals(right);

    /// <summary>
    /// The operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator !=(CubicBezier2D left, CubicBezier2D right) => !left.Equals(right);

    /// <summary>
    /// Implicit conversion from tuple.
    /// </summary>
    /// <returns></returns>
    /// <param name="tuple"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator CubicBezier2D((double aX, double aY, double bX, double bY, double cX, double cY) tuple) => new(tuple);

    /// <summary>
    /// Implicit conversion from tuple.
    /// </summary>
    /// <returns></returns>
    /// <param name="tuple"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator CubicBezier2D((double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY) tuple) => new(tuple);
    #endregion Operators

    /// <summary>
    /// The hull.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public List<Point2D> Hull(double t)
    {
        var points = Points;
        var results = new List<Point2D>(Points);

        // We lerp between all points at each iteration, until we have 1 point left.
        while (points.Count > 1)
        {
            var remaining = new List<Point2D>();

            for (var i = 0; i < points.Count - 1; i++)
            {
                var pt = Interpolators.Linear(t, points[i], points[i + 1]);
                results.Add(pt);
                remaining.Add(pt);
            }

            points = remaining;
        }

        return results;
    }

    /// <summary>
    /// The interpolate.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override Point2D Interpolate(double t)
        => new(Interpolators.CubicBezier(t, A.X, A.Y, B.X, B.Y, C.X, C.Y, D.X, D.Y));

    /// <summary>
    /// The interpolate.
    /// </summary>
    /// <param name="ts">The ts.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IList<Point2D> Interpolate(IList<double> ts)
    {
        ArgumentNullException.ThrowIfNull(ts);

        var list = new List<Point2D>();

        foreach (var t in ts)
        {
            list.Add(Interpolate(t));
        }

        return list;
    }

    /// <summary>
    /// Gets the first derivative of the curve at the given t value.
    /// </summary>
    /// <param name="t">Time value at which to sample (should be between 0 and 1, though it won't fail if outside that range).</param>
    /// <returns>First derivative of curve at sampled point.</returns>
    /// <acknowledgment>
    /// https://github.com/burningmime/curves
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Vector2D Derivate(double t)
    {
        // The inverse of t.
        var ti = 1d - t;

        // The derivative of the de Casteljau method applied to a cubic Bezier curve.
        return (3d * ti * ti * (B - A)) + (6d * t * ti * (C - B)) + (3d * t * t * (D - C));
    }

    /// <summary>
    /// Gets the normal vector of the curve at the given t value.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    /// <acknowledgment>
    /// https://pomax.github.io/bezierinfo/#pointvectors
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Vector2D Normal(double t)
    {
        var d = Derivate(t);
        var q = 1d / Sqrt((d.I * d.I) + (d.J * d.J));
        return new Vector2D(-d.J * q, d.I * q);
    }

    /// <summary>
    /// Gets the tangent (normalized derivative) of the curve at a given t value.
    /// </summary>
    /// <param name="t">Time value at which to sample (should be between 0 and 1, though it won't fail if outside that range).</param>
    /// <returns>Direction the curve is going at that point.</returns>
    /// <acknowledgment>
    /// https://github.com/burningmime/curves
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Vector2D Tangent(double t)
        => Derivate(t).Normalize();

    #region Methods
    /// <summary>
    /// Get the enumerator.
    /// </summary>
    /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IEnumerator<Point2D> GetEnumerator()
    {
        yield return new Point2D(Interpolators.CubicBezier(Length, A.X, A.Y, B.X, B.Y, C.X, C.Y, D.X, D.Y));
    }

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Equals(CubicBezier2D? other) => A.Equals(other?.A) && B.Equals(other?.B) && C.Equals(other?.C) && D.Equals(other?.D);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals(object? obj) => obj is CubicBezier2D d && Equals(d);

    /// <summary>
    /// Get the hash code.
    /// </summary>
    /// <returns>The <see cref="int"/>.</returns>
    /// <remarks><para>https://github.com/burningmime/curves</para></remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override int GetHashCode() => HashCode.Combine(ax, ay, bx, by, cx, cy, dx, dy);

    /// <summary>
    /// Creates a string representation of this <see cref="CubicBezier2D"/> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ConvertToString(string format, IFormatProvider provider)
    {
        if (this is null)
        {
            return nameof(CubicBezier2D);
        }

        var sep = Tokenizer.GetNumericListSeparator(provider);
        IFormattable formattable = $"{nameof(CubicBezier2D)}={{{nameof(A)}={A}{sep}{nameof(B)}={B}{sep}{nameof(C)}={C}{sep}{nameof(D)}={D}}}";
        return formattable.ToString(format, provider);
    }
    #endregion Methods
}
