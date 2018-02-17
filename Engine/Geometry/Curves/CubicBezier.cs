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
    [XmlType(TypeName = "Bézier-Cubic")]
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
        public CubicBezier()
            : this(0, 0, 0, 0, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class from a <see cref="QuadraticBezier"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public CubicBezier(Point2D a, Point2D b, Point2D c)
        {
            var nodes = Conversions.QuadraticBezierToCubicBezier(a, b, c);
            ax = nodes.A.X;
            ay = nodes.A.Y;
            bx = nodes.B.X;
            by = nodes.B.Y;
            cx = nodes.C.X;
            cy = nodes.C.Y;
            dx = nodes.D.X;
            dy = nodes.D.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class from a <see cref="QuadraticBezier"/>.
        /// </summary>
        /// <param name="tuple"></param>
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
        public CubicBezier(double ax, double ay, double bx, double by, double cx, double cy)
        {
            var nodes = Conversions.QuadraticBezierToCubicBezier(ax, ay, bx, by, cx, cy);
            ax = nodes[0].X;
            ay = nodes[0].Y;
            bx = nodes[1].X;
            by = nodes[1].Y;
            cx = nodes[2].X;
            cy = nodes[2].Y;
            dx = nodes[3].X;
            dy = nodes[3].Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        /// <param name="a">Position1</param>
        /// <param name="b">Tangent1</param>
        /// <param name="c">Position2</param>
        /// <param name="d">Tangent2</param>
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
        /// <remarks></remarks>
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
        /// <remarks></remarks>
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
        /// <remarks></remarks>
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
        /// <remarks></remarks>
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
        /// Gets the Polynomial degree of the <see cref="CubicBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public PolynomialDegree Degree
            => PolynomialDegree.Cubic;
        #endregion Properties

        #region Operators
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(CubicBezier left, CubicBezier right)
            => left.Equals(right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
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
        /// Samples the Bézier curve at the given t value.
        /// </summary>
        /// <param name="t">Time value at which to sample (should be between 0 and 1, though it won't fail if outside that range).</param>
        /// <returns>Sampled point.</returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/burningmime/curves
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2D Sample(double t)
        {
            var ti = 1d - t;
            return (Point2D)((ti * ti * ti * A) + (3 * ti * ti * t * B) + (3 * ti * t * t * C) + (t * t * t * D));
        }

        /// <summary>
        /// Gets the first derivative of the curve at the given T value.
        /// </summary>
        /// <param name="t">Time value at which to sample (should be between 0 and 1, though it won't fail if outside that range).</param>
        /// <returns>First derivative of curve at sampled point.</returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/burningmime/curves
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D Derivative(double t)
        {
            var ti = 1 - t;
            return (3 * ti * ti * (B - A)) + (6 * t * ti * (C - B)) + (3 * t * t * (D - C));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Point2D Interpolate(double t)
            => new Point2D(Interpolators.CubicBezier(A.X, A.Y, B.X, B.Y, C.X, C.Y, D.X, D.Y, t));

        /// <summary>
        /// Gets the tangent (normalized derivative) of the curve at a given T value.
        /// </summary>
        /// <param name="t">Time value at which to sample (should be between 0 and 1, though it won't fail if outside that range).</param>
        /// <returns>Direction the curve is going at that point.</returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/burningmime/curves
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D Tangent(double t)
            => Derivative(t).Normalize();

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<Point2D> GetEnumerator()
        {
            yield return new Point2D(Interpolators.CubicBezier(A.X, A.Y, B.X, B.Y, C.X, C.Y, D.X, D.Y, Length));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(CubicBezier other)
            => A.Equals(other?.A) && B.Equals(other?.B) && C.Equals(other?.C) && D.Equals(other?.D);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is CubicBezier && Equals((CubicBezier)obj);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
            if (this == null) return nameof(CubicBezier);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(CubicBezier)}={{{nameof(A)}={A}{sep}{nameof(B)}={B}{sep}{nameof(C)}={C}{sep}{nameof(D)}={D}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
