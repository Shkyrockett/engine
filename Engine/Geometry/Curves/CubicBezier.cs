// <copyright file="CubicBezier.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// Cubic or 3rd degree Bézier curve.
    /// </summary>
    /// <structure>Engine.Geometry.CubicBezier2D</structure>
    /// <remarks>
    /// http://paulbourke.net/geometry/bezier/index.html
    /// http://pomax.github.io/bezierinfo/
    /// </remarks>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(CubicBezier))]
    [XmlType(TypeName = "Bezier-Cubic")]
    public class CubicBezier
        : Shape, IEquatable<CubicBezier>
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
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezier()
            : this(0, 0, 0, 0, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class from a <see cref="QuadraticBezier"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezier(Point2D a, Point2D b, Point2D c)
        {
            (this.ax, this.ay, this.bx, this.by, this.cx, this.cy, this.dx, this.dy) = Conversions.QuadraticBezierToCubicBezierTuple(a.X, a.Y, b.X, b.Y, c.X, c.Y);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class from a <see cref="QuadraticBezier"/>.
        /// </summary>
        /// <param name="tuple"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezier((double aX, double aY, double bX, double bY, double cX, double cY) tuple)
            : this(tuple.aX, tuple.aY, tuple.bX, tuple.bY, tuple.cX, tuple.cY)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class from a <see cref="QuadraticBezier"/>.
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezier(double ax, double ay, double bx, double by, double cx, double cy)
        {
            (this.ax, this.ay, this.bx, this.by, this.cx, this.cy, this.dx, this.dy) = Conversions.QuadraticBezierToCubicBezierTuple(ax, ay, bx, by, cx, cy);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        /// <param name="a">Position1</param>
        /// <param name="b">Tangent1</param>
        /// <param name="c">Position2</param>
        /// <param name="d">Tangent2</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezier(Point2D a, Point2D b, Point2D c, Point2D d)
            : this(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        /// <param name="ax">Position1</param>
        /// <param name="ay">Position1</param>
        /// <param name="bx">Tangent1</param>
        /// <param name="by">Tangent1</param>
        /// <param name="cx">Position2</param>
        /// <param name="cy">Position2</param>
        /// <param name="dx">Tangent2</param>
        /// <param name="dy">Tangent2</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezier(double ax, double ay, double bx, double by, double cx, double cy, double dx, double dy)
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
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezier((double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY) tuple)
            : this(tuple.aX, tuple.aY, tuple.bX, tuple.bY, tuple.cX, tuple.cY, tuple.dX, tuple.dY)
        { }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="CubicBezier"/> to a Tuple.
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
        /// Gets or sets a list of points representing the handles of the <see cref="CubicBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> Points
        {
            get { return new List<Point2D> { A, B, C, D }; }
            set
            {
                A = value[0];
                B = value[1];
                C = value[2];
                D = value[3];
            }
        }

        /// <summary>
        /// Gets or sets the starting node for the <see cref="CubicBezier"/> curve.
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
        /// Gets or sets the first middle tangent control node for the <see cref="CubicBezier"/> curve.
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
        /// Gets or sets the second middle tangent control node for the <see cref="CubicBezier"/> curve.
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
        /// Gets or sets the closing node for the <see cref="CubicBezier"/> curve.
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
        /// Gets the axial aligned bounding box of the <see cref="CubicBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [ReadOnly(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.BezierBounds(CurveX, CurveY));

        /// <summary>
        /// An approximation of the length of a <see cref="CubicBezier"/> curve.
        /// </summary>
        /// <returns></returns>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Length
            => (double)CachingProperty(() => Measurements.CubicBezierArcLength(ax, ay, bx, by, cx, cy, dx, dy));

        /// <summary>
        /// Gets the perimeter length of the <see cref="CubicBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override double Perimeter
            => Length;

        /// <summary>
        /// Gets the Polynomial degree of the <see cref="CubicBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public PolynomialDegree Degree
            => PolynomialDegree.Cubic;

        /// <summary>
        /// Gets the <see cref="CubicBezier"/> curve's polynomial representation along the x-axis.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                var curveX = (Polynomial)CachingProperty(() => (Polynomial)Maths.CubicBezierCoefficients(dx, cx, bx, ax));
                curveX.IsReadonly = true;
                return curveX;
            }
        }

        /// <summary>
        /// Gets the <see cref="CubicBezier"/> curve's polynomial representation along the y-axis.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveY
        {
            get
            {
                var curveY = (Polynomial)CachingProperty(() => (Polynomial)Maths.CubicBezierCoefficients(dy, cy, by, ay));
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
                        var a1 = Maths.AngleVector(Points[0].X, Points[0].Y, Points[3].X, Points[3].Y, Points[1].X, Points[1].Y);
                        var a2 = Maths.AngleVector(Points[0].X, Points[0].Y, Points[3].X, Points[3].Y, Points[2].X, Points[2].Y);
                        if (a1 > 0 && a2 < 0 || a1 < 0 && a2 > 0)
                        {
                            return false;
                        }
                    }
                    var n1 = Normal(0);
                    var n2 = Normal(1);
                    var s = (n1.I * n2.I) + (n1.J * n2.J);
                    var angle = Math.Abs(Math.Acos(s));
                    return angle < Math.PI / 3d;
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
                List<double> Extrema(IList<IList<Point2D>> derivativeCoordinates)
                {
                    var p = (from a in derivativeCoordinates[0] select a.X).ToList();
                    var result = new List<double>(Maths.DRoots(p));
                    p = (from a in derivativeCoordinates[0] select a.Y).ToList();
                    result.AddRange(Maths.DRoots(p));
                    p = (from a in derivativeCoordinates[1] select a.X).ToList();
                    result.AddRange(Maths.DRoots(p));
                    p = (from a in derivativeCoordinates[1] select a.Y).ToList();
                    result.AddRange(Maths.DRoots(p));

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

                IList<double> Inflections(IList<Point2D> points)
                {
                    var p = Maths.AlignPoints(points, points[0].X, points[0].Y, points[3].X, points[3].Y);
                    var a = p[2].X * p[1].Y;
                    var b = p[3].X * p[1].Y;
                    var c = p[1].X * p[2].Y;
                    var d = p[3].X * p[2].Y;
                    var v1 = 18 * ((-3 * a) + (2 * b) + (3 * c) - d);
                    var v2 = 18 * ((3 * a) - b - (3 * c));
                    var v3 = 18 * (c - a);

                    if (Maths.Approximately(v1, 0))
                    {
                        return new double[] { };
                    }

                    var descriminant = (v2 * v2) - (4 * v1 * v3);
                    var sq = Math.Sqrt(descriminant);
                    d = 2 * v1;

                    return Maths.Approximately(d, 0)
                        ? new List<double>()
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
        /// <returns>The <see cref="T:List{Bezier}"/>.</returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public IList<CubicBezier> Reduction
        {
            get
            {
                return (IList<CubicBezier>)CachingProperty(() => Reduce());

                IList<CubicBezier> Reduce()
                {
                    int i;
                    double t1 = 0, t2 = 0;
                    const double step = 0.01;
                    CubicBezier segment;
                    var pass1 = new List<CubicBezier>();
                    var pass2 = new List<CubicBezier>();

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

                        segment = new CubicBezier(
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
                                    segment = new CubicBezier(
                                        s.Points[0],
                                        s.Points[1],
                                        s.Points[2],
                                        s.Points[3]);
                                }

                                if (!segment.IsSimple)
                                {
                                    t2 -= step;
                                    if (Math.Abs(t1 - t2) < step)
                                    {
                                        // we can never form a reduction
                                        return new List<CubicBezier>();
                                    }
                                    var a = p1.Split(t2, t1);
                                    var s = a[1];
                                    segment = new CubicBezier(
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
                            segment = new CubicBezier(
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(CubicBezier left, CubicBezier right)
            => left.Equals(right);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(CubicBezier left, CubicBezier right)
            => !left.Equals(right);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"></param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator CubicBezier((double aX, double aY, double bX, double bY, double cX, double cY) tuple)
            => new CubicBezier(tuple);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"></param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator CubicBezier((double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY) tuple)
            => new CubicBezier(tuple);
        #endregion Operators

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(CubicBezier)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(CubicBezier)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(CubicBezier)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(CubicBezier)} has been deserialized.");
        //}

        //#endregion

        /// <summary>
        /// The hull.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="T:List{Point3D}"/>.</returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                    var pt = Interpolators.Linear(points[i], points[i + 1], t);
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Point2D Interpolate(double t)
            => new Point2D(Interpolators.CubicBezier(A.X, A.Y, B.X, B.Y, C.X, C.Y, D.X, D.Y, t));

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IList<Point2D> Interpolate(IList<double> ts)
        {
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D Normal(double t)
        {
            var d = Derivate(t);
            var q = 1d / Math.Sqrt((d.I * d.I) + (d.J * d.J));
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D Tangent(double t)
            => Derivate(t).Normalize();

        #region Methods
        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="T:IEnumerator{Point2D}"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<Point2D> GetEnumerator()
        {
            yield return new Point2D(Interpolators.CubicBezier(A.X, A.Y, B.X, B.Y, C.X, C.Y, D.X, D.Y, Length));
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(CubicBezier other)
            => A.Equals(other?.A) && B.Equals(other?.B) && C.Equals(other?.C) && D.Equals(other?.D);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is CubicBezier && Equals((CubicBezier)obj);

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        /// <remarks>https://github.com/burningmime/curves</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            var hash = new JenkinsHash();
            hash.Mixin(ax.GetHashCode());
            hash.Mixin(ay.GetHashCode());
            hash.Mixin(bx.GetHashCode());
            hash.Mixin(by.GetHashCode());
            hash.Mixin(cx.GetHashCode());
            hash.Mixin(cy.GetHashCode());
            hash.Mixin(dx.GetHashCode());
            hash.Mixin(dy.GetHashCode());
            return hash.GetValue();
        }

        /// <summary>
        /// Creates a string representation of this <see cref="CubicBezier"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this is null)
            {
                return nameof(CubicBezier);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(CubicBezier)}={{{nameof(A)}={A}{sep}{nameof(B)}={B}{sep}{nameof(C)}={C}{sep}{nameof(D)}={D}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
