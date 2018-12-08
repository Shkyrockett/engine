// <copyright file="QuadraticBezier.cs" company="Shkyrockett" >
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
    /// Quadratic or 2nd degree Bézier curve.
    /// </summary>
    /// <remarks>
    /// http://paulbourke.net/geometry/bezier/index.html
    /// http://pomax.github.io/bezierinfo/
    /// </remarks>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(QuadraticBezier))]
    [XmlType(TypeName = "bezier-Quadratic")]
    public class QuadraticBezier
        : Shape, IEquatable<QuadraticBezier>
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
        /// Tangent x-coordinate.
        /// </summary>
        private double bx;

        /// <summary>
        /// Tangent y-coordinate.
        /// </summary>
        private double by;

        /// <summary>
        /// Position 2 x-coordinate.
        /// </summary>
        private double cx;

        /// <summary>
        /// Position 2 y-coordinate.
        /// </summary>
        private double cy;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezier"/> class.
        /// </summary>
        public QuadraticBezier()
            : this(0, 0, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezier"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public QuadraticBezier(Point2D a, Point2D b, Point2D c)
            : this(a.X, a.Y, b.X, b.Y, c.X, c.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezier"/> class.
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        public QuadraticBezier(double ax, double ay, double bx, double by, double cx, double cy)
        {
            this.ax = ax;
            this.ay = ay;
            this.bx = bx;
            this.by = by;
            this.cx = cx;
            this.cy = cy;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezier"/> class from a <see cref="QuadraticBezier"/>.
        /// </summary>
        /// <param name="tuple"></param>
        public QuadraticBezier((double aX, double aY, double bX, double bY, double cX, double cY) tuple)
            : this(tuple.aX, tuple.aY, tuple.bX, tuple.bY, tuple.cX, tuple.cY)
        { }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="QuadraticBezier"/> to a Tuple.
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        public void Deconstruct(out double ax, out double ay, out double bx, out double by, out double cx, out double cy)
        {
            ax = this.ax;
            ay = this.ay;
            bx = this.bx;
            by = this.by;
            cx = this.cx;
            cy = this.cy;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets a list of points representing the handles of the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> Points
            => new List<Point2D> { A, B, C };

        /// <summary>
        /// Gets or sets the starting node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        /// Gets or sets the X coordinate of the first Point of a Cubic Bezier.
        /// </summary>
        [XmlAttribute(nameof(ax))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The X coordinate of the first Point of a Cubic Bezier.")]
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
        /// Gets or sets the Y coordinate of the first Point of a Cubic Bezier.
        /// </summary>
        [XmlAttribute(nameof(ay))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The y coordinate of the first Point of a Cubic Bezier.")]
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
        /// Gets or sets the middle tangent control node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        /// Gets or sets the X coordinate of the second Point of a Cubic Bezier.
        /// </summary>
        [XmlAttribute(nameof(bx))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The X coordinate of the second Point of a Cubic Bezier.")]
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
        /// Gets or sets the closing node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        /// Gets or sets the X coordinate of the third Point of a Cubic Bezier.
        /// </summary>
        [XmlAttribute(nameof(cx))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The X coordinate of the third Point of a Cubic Bezier.")]
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
        /// Gets or sets the Y coordinate of the third Point of a Cubic Bezier.
        /// </summary>
        [XmlAttribute(nameof(cy))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The y coordinate of the third Point of a Cubic Bezier.")]
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
        /// Gets or sets the Y coordinate of the second Point of a Cubic Bezier.
        /// </summary>
        [XmlAttribute(nameof(by))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The y coordinate of the second Point of a Cubic Bezier.")]
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
        /// Gets the axial aligned bounding box of the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [ReadOnly(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.BezierBounds(CurveX, CurveY));

        /// <summary>
        /// An approximation of the length of a <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Length
            => (double)CachingProperty(() => Measurements.QuadraticBezierArcLengthByIntegral(ax, ay, bx, by, cx, cy));

        /// <summary>
        /// Gets the perimeter length of the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override double Perimeter
            => Length;

        /// <summary>
        /// Gets the Polynomial degree of the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public PolynomialDegree Degree
            => PolynomialDegree.Quadratic;

        /// <summary>
        /// Gets the <see cref="QuadraticBezier"/> curve's polynomial representation along the x-axis.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                var curveX = (Polynomial)CachingProperty(() => (Polynomial)Maths.QuadraticBezierCoefficients(cx, bx, ax));
                curveX.IsReadonly = true;
                return curveX;
            }
        }

        /// <summary>
        /// Gets the <see cref="QuadraticBezier"/> curve's polynomial representation along the y-axis.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveY
        {
            get
            {
                var curveY = (Polynomial)CachingProperty(() => (Polynomial)Maths.QuadraticBezierCoefficients(cy, by, ay));
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
                    var n1 = Normal(0);
                    var n2 = Normal(1);
                    var s = n1.I * n2.I + n1.J * n2.J;
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
        public List<List<Point2D>> DerivativeCoordinates
        {
            get
            {
                return (List<List<Point2D>>)CachingProperty(() => DerivativeCoordinates());

                List<List<Point2D>> DerivativeCoordinates()
                {
                    // One-time compute of derivative coordinates
                    var derivitivePoints = new List<List<Point2D>>();
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
        public List<double> Extrema
        {
            get
            {
                return (List<double>)CachingProperty(() => Extrema(DerivativeCoordinates));

                // ToDo: What are DRoots?
                List<double> Extrema(List<List<Point2D>> derivativeCoordinates)
                {
                    var p = (from a in derivativeCoordinates[0] select a.X).ToList();
                    var r = Maths.DRoots(p);
                    var result = new List<double>(r);
                    p = (from a in derivativeCoordinates[0] select a.Y).ToList();
                    r = Maths.DRoots(p);
                    result.AddRange(r);
                    p = (from a in derivativeCoordinates[1] select a.X).ToList();
                    r = Maths.DRoots(p);
                    result.AddRange(r);
                    p = (from a in derivativeCoordinates[1] select a.Y).ToList();
                    r = Maths.DRoots(p);
                    result.AddRange(r);

                    result = result.Where((t) => { return t >= 0 && t <= 1; }).ToList();
                    result.Sort();
                    result.Reverse();
                    return result;
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
                    for (t1 = extrema[0], i = 1; i < extrema.Count; i++)
                    {
                        t2 = extrema[i];
                        var s = this.Split(t1, t2);

                        segment = new CubicBezier(
                            s[1].Points[0],
                            s[1].Points[1],
                            s[1].Points[2],
                            s[1].Points[3]);
                        //segment.T1 = t1;
                        //segment.T2 = t2;
                        pass1.Add(segment);
                        t1 = t2;
                    }

                    // Second pass: further reduce these segments to simple segments
                    foreach (CubicBezier p1 in pass1)
                    {
                        t1 = 0;
                        t2 = 0;
                        while (t2 <= 1)
                        {
                            for (t2 = t1 + step; t2 <= 1 + step; t2 += step)
                            {
                                {
                                    var s = p1.Split(t1, t2);
                                    segment = new CubicBezier(
                                        s[1].Points[0],
                                        s[1].Points[1],
                                        s[1].Points[2],
                                        s[1].Points[3]);

                                }
                                if (!segment.IsSimple)
                                {
                                    t2 -= step;
                                    if (Math.Abs(t1 - t2) < step)
                                    {
                                        // we can never form a reduction
                                        return new List<CubicBezier>();
                                    }
                                    var s = p1.Split(t1, t2);
                                    segment = new CubicBezier(
                                        s[1].Points[0],
                                        s[1].Points[1],
                                        s[1].Points[2],
                                        s[1].Points[3]);
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
                            var s = p1.Split(t1, 1);
                            segment = new CubicBezier(
                                s[1].Points[0],
                                s[1].Points[1],
                                s[1].Points[2],
                                s[1].Points[3]);
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
        public static bool operator ==(QuadraticBezier left, QuadraticBezier right)
            => left.Equals(right);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(QuadraticBezier left, QuadraticBezier right)
            => !left.Equals(right);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"></param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator QuadraticBezier((double aX, double aY, double bX, double bY, double cX, double cY) tuple)
            => new QuadraticBezier(tuple);
        #endregion Operators

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(QuadraticBezier)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(QuadraticBezier)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(QuadraticBezier)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(QuadraticBezier)} has been deserialized.");
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
            => new Point2D(Interpolators.QuadraticBezier(ax, ay, bx, by, cx, cy, t));

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="ts">The ts.</param>
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
        /// https://www.gamedev.net/forums/topic/419818-derivative-of-bezier-curve/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D Derivate(double t)
        {
            // The inverse of t.
            var ti = 1d - t;

            // The derivative of the de Casteljau method applied to a quadratic Bezier curve.
            return (2d * ti * (B - A)) + (2d * t * (C - B));
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
            var q = 1d / Math.Sqrt(d.I * d.I + d.J * d.J);
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
            yield return new Point2D(Interpolators.QuadraticBezier(A.X, A.Y, B.X, B.Y, C.X, C.Y, Length));
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(QuadraticBezier other)
            => A.Equals(other?.A) && B.Equals(other?.B) && C.Equals(other?.C);

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
        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
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
            return hash.GetValue();
            //return ax.GetHashCode() ^ ay.GetHashCode() ^ bx.GetHashCode() ^ by.GetHashCode() ^ cx.GetHashCode() ^ cy.GetHashCode();
        }

        /// <summary>
        /// Creates a string representation of this <see cref="QuadraticBezier"/> struct based on the format string
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
                return nameof(QuadraticBezier);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(QuadraticBezier)}={{{nameof(A)}={A}{sep}{nameof(B)}={B}{sep}{nameof(C)}={C}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
