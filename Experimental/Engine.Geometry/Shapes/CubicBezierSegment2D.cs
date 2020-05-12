// <copyright file="CubicBezierSegment2D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Operations;
using static Engine.Polynomials;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Cubic or 3rd degree Bézier curve.
    /// </summary>
    /// <seealso cref="Engine.IShapeSegment" />
    /// <seealso cref="System.IEquatable{T}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<CubicBezierSegment2D>))]
    [XmlType(TypeName = "Bezier-Cubic")]
    [DebuggerDisplay("{ToString()}")]
    public struct CubicBezierSegment2D
        : IShapeSegment, IPropertyCaching, IEquatable<CubicBezierSegment2D>
    {
        #region Fields
        /// <summary>
        /// Position 1 coordinate.
        /// </summary>
        private Point2D a;

        /// <summary>
        /// Tangent 1 coordinate.
        /// </summary>
        private Point2D b;

        /// <summary>
        /// Tangent 2 coordinate.
        /// </summary>
        private Point2D c;

        /// <summary>
        /// Position 2 coordinate.
        /// </summary>
        private Point2D d;
        #endregion

        #region Event Delegates
        /// <summary>
        /// The property changed event of the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The property changing event of the <see cref="PropertyChangingEventHandler"/>.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier2D" /> class from a <see cref="QuadraticBezier2D" />.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezierSegment2D(Point2D a, Point2D b, Point2D c)
            : this(Conversions.QuadraticBezierToCubicBezierTuple(a.X, a.Y, b.X, b.Y, c.X, c.Y))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier2D" /> class from a <see cref="QuadraticBezier2D" />.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezierSegment2D((double aX, double aY, double bX, double bY, double cX, double cY) tuple)
            : this(tuple.aX, tuple.aY, tuple.bX, tuple.bY, tuple.cX, tuple.cY)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier2D" /> class from a <see cref="QuadraticBezier2D" />.
        /// </summary>
        /// <param name="ax">The ax.</param>
        /// <param name="ay">The ay.</param>
        /// <param name="bx">The bx.</param>
        /// <param name="by">The by.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezierSegment2D(double ax, double ay, double bx, double by, double cx, double cy)
            : this(Conversions.QuadraticBezierToCubicBezierTuple(ax, ay, bx, by, cx, cy))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier2D"/> struct.
        /// </summary>
        /// <param name="a">Position1</param>
        /// <param name="b">Tangent1</param>
        /// <param name="c">Position2</param>
        /// <param name="d">Tangent2</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezierSegment2D(Point2D a, Point2D b, Point2D c, Point2D d)
            : this()
        {
            (this.a, this.b, this.c, this.d) = (a, b, c, d);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier2D" /> struct.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="dX">The d x.</param>
        /// <param name="dY">The d y.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezierSegment2D(double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY)
            : this(new Point2D(aX, aY), new Point2D(bX, bY), new Point2D(cX, cY), new Point2D(dX, dY))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier2D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubicBezierSegment2D((double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY) tuple)
            : this(tuple.aX, tuple.aY, tuple.bX, tuple.bY, tuple.cX, tuple.cY, tuple.dX, tuple.dY)
        { }
        #endregion

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="CubicBezier2D" /> to a Tuple.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        public void Deconstruct(out (double ax, double ay) a, out (double bx, double by) b, out (double cx, double cy) c, out (double dx, double dy) d) => (a, b, c, d) = (this.a, this.b, this.c, this.d);

        /// <summary>
        /// Deconstruct this <see cref="CubicBezier2D" /> to a Tuple.
        /// </summary>
        /// <param name="ax">The ax.</param>
        /// <param name="ay">The ay.</param>
        /// <param name="bx">The bx.</param>
        /// <param name="by">The by.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="dx">The dx.</param>
        /// <param name="dy">The dy.</param>
        public void Deconstruct(out double ax, out double ay, out double bx, out double by, out double cx, out double cy, out double dx, out double dy) => (ax, ay, bx, by, cx, cy, dx, dy) = (this.a.X, this.a.Y, this.b.X, this.b.Y, this.c.X, this.c.Y, this.d.X, this.d.Y);
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets a list of points representing the handles of the <see cref="CubicBezier2D" /> curve.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> Points
        {
            get { return new List<Point2D> { a, b, c, d }; }
            set
            {
                if (!(value is null) && value.Count == 4)
                {
                    OnPropertyChanging();
                    (this as IPropertyCaching).ClearCache();
                    (a, b, c, d) = (value[0], value[1], value[2], value[3]);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the starting node for the <see cref="CubicBezier2D"/> curve.
        /// </summary>
        /// <value>
        /// a.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Elements")]
        [Description("The first Point of a Cubic Bézier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D A
        {
            get { return a; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                a = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the first Point of a Cubic Bézier.
        /// </summary>
        /// <value>
        /// The ax.
        /// </value>
        [XmlAttribute(nameof(AX))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The X coordinate of the first Point of a Cubic Bézier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double AX
        {
            get { return a.X; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                a.X = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the first Point of a Cubic Bézier.
        /// </summary>
        [XmlAttribute(nameof(AY))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The y coordinate of the first Point of a Cubic Bézier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double AY
        {
            get { return a.Y; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                a.Y = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the first middle tangent control node for the <see cref="CubicBezier2D" /> curve.
        /// </summary>
        /// <value>
        /// The b.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Elements")]
        [Description("The second Point of a Cubic Bézier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D B
        {
            get { return b; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                b = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the second Point of a Cubic Bézier.
        /// </summary>
        /// <value>
        /// The bx.
        /// </value>
        [XmlAttribute(nameof(BX))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The X coordinate of the second Point of a Cubic Bézier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double BX
        {
            get { return b.X; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                b.X = value;
                OnPropertyChanged(nameof(BX));
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the second Point of a Cubic Bézier.
        /// </summary>
        /// <value>
        /// The by.
        /// </value>
        [XmlAttribute(nameof(BY))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The y coordinate of the second Point of a Cubic Bézier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double BY
        {
            get { return b.Y; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                b.Y = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the second middle tangent control node for the <see cref="CubicBezier2D" /> curve.
        /// </summary>
        /// <value>
        /// The c.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Elements")]
        [Description("The third Point of a Cubic Bézier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D C
        {
            get { return c; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                c = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the third Point of a Cubic Bézier.
        /// </summary>
        /// <value>
        /// The cx.
        /// </value>
        [XmlAttribute(nameof(CX))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The X coordinate of the third Point of a Cubic Bézier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double CX
        {
            get { return c.X; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                b.X = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the third Point of a Cubic Bézier.
        /// </summary>
        /// <value>
        /// The cy.
        /// </value>
        [XmlAttribute(nameof(CY))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The y coordinate of the third Point of a Cubic Bézier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double CY
        {
            get { return c.Y; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                c.Y = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the closing node for the <see cref="CubicBezier2D" /> curve.
        /// </summary>
        /// <value>
        /// The d.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D D
        {
            get { return d; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                d = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the fourth Point of a Cubic Bézier.
        /// </summary>
        /// <value>
        /// The dx.
        /// </value>
        [XmlAttribute(nameof(DX))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The X coordinate of the fourth Point of a Cubic Bézier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double DX
        {
            get { return d.X; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                d.X = value;
                OnPropertyChanged(nameof(DX));
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the fourth Point of a Cubic Bézier.
        /// </summary>
        /// <value>
        /// The dy.
        /// </value>
        [XmlAttribute(nameof(DY))]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The y coordinate of the fourth Point of a Cubic Bézier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double DY
        {
            get { return d.Y; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                d.Y = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the axial aligned bounding box of the <see cref="CubicBezier2D" /> curve.
        /// </summary>
        /// <value>
        /// The bounds.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [ReadOnly(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public Rectangle2D Bounds
        {
            get
            {
                (var curveX, var curveY) = (CurveX, CurveY);
                return (Rectangle2D)(this as IPropertyCaching).CachingProperty(() => Measurements.BezierBounds(curveX, curveY));
            }
        }

        /// <summary>
        /// An approximation of the length of a <see cref="CubicBezier2D" /> curve.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Length
        {
            get
            {
                (var ax, var ay, var bx, var by, var cx, var cy, var dx, var dy) = (a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y);
                return (double)(this as IPropertyCaching).CachingProperty(() => Measurements.CubicBezierArcLength(ax, ay, bx, by, cx, cy, dx, dy));
            }
        }

        /// <summary>
        /// Gets the perimeter length of the <see cref="CubicBezier2D" /> curve.
        /// </summary>
        /// <value>
        /// The perimeter.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Perimeter => Length;

        /// <summary>
        /// Gets the Polynomial degree of the <see cref="CubicBezier2D" /> curve.
        /// </summary>
        /// <value>
        /// The degree.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public static PolynomialDegree Degree => PolynomialDegree.Cubic;

        /// <summary>
        /// Gets the <see cref="CubicBezier2D" /> curve's polynomial representation along the x-axis.
        /// </summary>
        /// <value>
        /// The curve x.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                (var dx, var cx, var bx, var ax) = (d.X, c.X, b.X, a.X);
                var curveX = (Polynomial)(this as IPropertyCaching).CachingProperty(() => (Polynomial)CubicBezierBernsteinBasis(dx, cx, bx, ax));
                curveX.IsReadonly = true;
                return curveX;
            }
        }

        /// <summary>
        /// Gets the <see cref="CubicBezier2D" /> curve's polynomial representation along the y-axis.
        /// </summary>
        /// <value>
        /// The curve y.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveY
        {
            get
            {
                (var dy, var cy, var by, var ay) = (d.Y, c.Y, b.Y, a.Y);
                var curveY = (Polynomial)(this as IPropertyCaching).CachingProperty(() => (Polynomial)CubicBezierBernsteinBasis(dy, cy, by, ay));
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
                (var points, var n1, var n2) = (Points, Normal(0), Normal(1));
                return (bool)(this as IPropertyCaching).CachingProperty(() => isSimple(points, n1, n2));

                static bool isSimple(List<Point2D> Points, Vector2D n1, Vector2D n2)
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
                var points = Points;
                return (IList<IList<Point2D>>)(this as IPropertyCaching).CachingProperty(() => DerivativeCoordinates(points));

                static IList<IList<Point2D>> DerivativeCoordinates(List<Point2D> points)
                {
                    // One-time compute of derivative coordinates
                    var derivitivePoints = new List<IList<Point2D>>();
                    for (int d = points.Count, c = d - 1; d > 1; d--, c--)
                    {
                        var list = new List<Point2D>();
                        for (var j = 0; j < c; j++)
                        {
                            var dpt = new Point2D(
                            x: c * (points[j + 1].X - points[j].X),
                            y: c * (points[j + 1].Y - points[j].Y)
                            //,z: c * (p[j + 1].Z - p[j].Z)
                            );

                            list.Add(dpt);
                        }

                        derivitivePoints.Add(list);
                        points = list;
                    }

                    return derivitivePoints;
                }
            }
        }

        /// <summary>
        /// Gets the extrema.
        /// </summary>
        /// <value>
        /// The extrema.
        /// </value>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public IList<double> Extrema
        {
            get
            {
                var derivativeCoordinates = DerivativeCoordinates;
                return (IList<double>)(this as IPropertyCaching).CachingProperty(() => Extrema(derivativeCoordinates));

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
        /// <value>
        /// The inflections.
        /// </value>
        /// <acknowledgment>
        /// https://pomax.github.io/bezierinfo/#inflections
        /// </acknowledgment>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public IList<double> Inflections
        {
            get
            {
                var points = Points;
                return (IList<double>)(this as IPropertyCaching).CachingProperty(() => Inflections(points));

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
        /// <value>
        /// The reduction.
        /// </value>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public IList<CubicBezierSegment2D> Reduction
        {
            get
            {
                var bezier = this;
                var extrema = Extrema;
                return (IList<CubicBezierSegment2D>)(this as IPropertyCaching).CachingProperty(() => Reduce(bezier, extrema));

                static IList<CubicBezierSegment2D> Reduce(CubicBezierSegment2D bezier, IList<double> Extrema)
                {
                    int i;
                    double t1 = 0, t2 = 0;
                    const double step = 0.01;
                    CubicBezierSegment2D segment;
                    var pass1 = new List<CubicBezierSegment2D>();
                    var pass2 = new List<CubicBezierSegment2D>();

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
                        var s = bezier.Split(t1, t2)[1];

                        segment = new CubicBezierSegment2D(
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
                                    segment = new CubicBezierSegment2D(
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
                                        return new List<CubicBezierSegment2D>();
                                    }
                                    var a = p1.Split(t2, t1);
                                    var s = a[1];
                                    segment = new CubicBezierSegment2D(
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
                            segment = new CubicBezierSegment2D(
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

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        /// <value>
        /// The property cache.
        /// </value>
        [Browsable(false)]
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Dictionary<object, object> IPropertyCaching.PropertyCache { get; set; }
        public IShapeSegment Leading { get; set; }
        public IShapeSegment Trailing { get; set; }
        public Point2D? Head { get; set; }
        public Point2D? Tail { get; set; }
        double IShapeSegment.Length { get; set; }
        #endregion

        #region Operators
        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(CubicBezierSegment2D left, CubicBezierSegment2D right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(CubicBezierSegment2D left, CubicBezierSegment2D right) => !(left == right);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"></param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator CubicBezierSegment2D((double aX, double aY, double bX, double bY, double cX, double cY) tuple) => new CubicBezierSegment2D(tuple);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"></param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator CubicBezierSegment2D((double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY) tuple) => new CubicBezierSegment2D(tuple);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is CubicBezierSegment2D d && Equals(d);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] CubicBezierSegment2D other) => A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D);
        #endregion

        /// <summary>
        /// The hull.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2D Interpolate(double t) => new Point2D(Interpolators.CubicBezier(t, A.X, A.Y, B.X, B.Y, C.X, C.Y, D.X, D.Y));

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IList<Point2D> Interpolate(IList<double> ts)
        {
            if (ts is null)
            {
                throw new ArgumentNullException(nameof(ts));
            }

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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D Tangent(double t) => Derivate(t).Normalized;

        #region Standard Methods
        /// <summary>
        /// Raises the property changing event.
        /// </summary>
        /// <param name="name">The name.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPropertyChanging([CallerMemberName] string name = "") => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="name">The name.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator{T}" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<Point2D> GetEnumerator()
        {
            yield return new Point2D(Interpolators.CubicBezier(Length, a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y));
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y);

        /// <summary>
        /// Creates a string representation of this <see cref="CubicBezier2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The format provider.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null)
            {
                return nameof(CubicBezierSegment2D);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(CubicBezierSegment2D)}={{{nameof(A)}={A}{sep}{nameof(B)}={B}{sep}{nameof(C)}={C}{sep}{nameof(D)}={D}}}";
            return formatable.ToString(format, provider);
        }
        #endregion
    }
}