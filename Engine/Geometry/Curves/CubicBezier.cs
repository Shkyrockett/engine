// <copyright file="CubicBezier.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
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
    /// CubicBezier
    /// </summary>
    /// <structure>Engine.Geometry.CubicBezier2D</structure>
    /// <remarks>
    /// http://paulbourke.net/geometry/bezier/index.html
    /// http://pomax.github.io/bezierinfo/
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(CubicBezier))]
    [XmlType(TypeName = "bezier-Cubic")]
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

        #endregion

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
            var nodes = Interpolaters.QuadraticBezierToCubicBezier(a, b, c);
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
            var nodes = Interpolaters.QuadraticBezierToCubicBezier(ax, ay, bx, by, cx, cy);
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

        #endregion

        #region Indexers

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //public Point2D this[double index]
        //{
        //    get { return Experimental.InterpolateCubicBezier(this, index); }
        //} 

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<Point2D> Points
            => new List<Point2D> { A, B, C, D };

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Category("Elements")]
        [Description("The first Point of a Cubic Bezier.")]
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
        /// <remarks></remarks>
        [XmlAttribute("ax")]
        [Browsable(true)]
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
        [XmlAttribute("ay")]
        [Browsable(true)]
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
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Category("Elements")]
        [Description("The second Point of a Cubic Bezier.")]
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
        /// <remarks></remarks>
        [XmlAttribute("bx")]
        [Browsable(true)]
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
        /// Gets or sets the Y coordinate of the second Point of a Cubic Bezier.
        /// </summary>
        [XmlAttribute("by")]
        [Browsable(true)]
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
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Category("Elements")]
        [Description("The third Point of a Cubic Bezier.")]
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
        /// <remarks></remarks>
        [XmlAttribute("cx")]
        [Browsable(true)]
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
        [XmlAttribute("cy")]
        [Browsable(true)]
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
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
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
        /// Gets or sets the X coordinate of the fourth Point of a Cubic Bezier.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("dx")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The X coordinate of the fourth Point of a Cubic Bezier.")]
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
        /// Gets or sets the Y coordinate of the fourth Point of a Cubic Bezier.
        /// </summary>
        [XmlAttribute("dy")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The y coordinate of the fourth Point of a Cubic Bezier.")]
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
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore, SoapIgnore]
        public double Length
            => (double)CachingProperty(() => Measurements.CubicBezierArcLength(ax, ay, bx, by, cx, cy, dx, dy));

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override double Perimeter
            => Length;

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        [XmlIgnore, SoapIgnore]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.BezierBounds(CurveX, CurveY));

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                var curveX = (Polynomial)CachingProperty(() => Polynomial.Bezier(Points.Select(p => p.X).ToArray()));
                curveX.IsReadonly = true;
                return curveX;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Polynomial CurveY
        {
            get
            {
                var curveY = (Polynomial)CachingProperty(() => Polynomial.Bezier(Points.Select(p => p.Y).ToArray()));
                curveY.IsReadonly = true;
                return curveY;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public PolynomialDegree Degree
            => PolynomialDegree.Cubic;

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(CubicBezier left, CubicBezier right)
            => left.Equals(right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(CubicBezier left, CubicBezier right)
            => !left.Equals(right);

        #endregion

        #region Serialization

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing()]
        protected void OnSerializing(StreamingContext context)
        {
            // Assert("This value went into the data file during serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerialized()]
        protected void OnSerialized(StreamingContext context)
        {
            // Assert("This value was reset after serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing()]
        protected void OnDeserializing(StreamingContext context)
        {
            // Assert("This value was set during deserialization");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized()]
        protected void OnDeserialized(StreamingContext context)
        {
            // Assert("This value was set after deserialization.");
        }

        #endregion

        /// <summary>
        /// Samples the bezier curve at the given t value.
        /// </summary>
        /// <param name="t">Time value at which to sample (should be between 0 and 1, though it won't fail if outside that range).</param>
        /// <returns>Sampled point.</returns>
        /// <remarks> https://github.com/burningmime/curves </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2D Sample(double t)
        {
            double ti = 1 - t;
            double t0 = ti * ti * ti;
            double t1 = 3 * ti * ti * t;
            double t2 = 3 * ti * t * t;
            double t3 = t * t * t;
            return (Point2D)((t0 * A) + (t1 * B) + (t2 * C) + (t3 * D));
        }

        /// <summary>
        /// Gets the first derivative of the curve at the given T value.
        /// </summary>
        /// <param name="t">Time value at which to sample (should be between 0 and 1, though it won't fail if outside that range).</param>
        /// <returns>First derivative of curve at sampled point.</returns>
        /// <remarks> https://github.com/burningmime/curves </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D Derivative(double t)
        {
            double ti = 1 - t;
            double tp0 = 3 * ti * ti;
            double tp1 = 6 * t * ti;
            double tp2 = 3 * t * t;
            return (tp0 * (B - A)) + (tp1 * (C - B)) + (tp2 * (D - C));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Point2D Interpolate(double t)
            => new Point2D(Interpolaters.CubicBezier(A.X, A.Y, B.X, B.Y, C.X, C.Y, D.X, D.Y, t));

        /// <summary>
        /// Gets the tangent (normalized derivative) of the curve at a given T value.
        /// </summary>
        /// <param name="t">Time value at which to sample (should be between 0 and 1, though it won't fail if outside that range).</param>
        /// <returns>Direction the curve is going at that point.</returns>
        /// <remarks> https://github.com/burningmime/curves </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D Tangent(double t)
            => Primitives.Normalize(Derivative(t));

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Point2D> GetEnumerator()
        {
            yield return new Point2D(Interpolaters.CubicBezier(A.X, A.Y, B.X, B.Y, C.X, C.Y, D.X, D.Y, Length));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CubicBezier other)
            => A.Equals(other?.A) && B.Equals(other?.B) && C.Equals(other?.C) && D.Equals(other?.D);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
            => obj is CubicBezier && Equals((CubicBezier)obj);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks> https://github.com/burningmime/curves </remarks>
        public override int GetHashCode()
        {
            JenkinsHash hash = new JenkinsHash();
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
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(CubicBezier);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(CubicBezier)}={{{nameof(A)}={A}{sep}{nameof(B)}={B}{sep}{nameof(C)}={C}{sep}{nameof(D)}={D}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
