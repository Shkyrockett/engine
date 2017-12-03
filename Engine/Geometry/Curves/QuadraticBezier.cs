// <copyright file="QuadraticBezier.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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
    /// Quadratic or 2nd degree Bezier curve.
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

        #endregion

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

        #endregion

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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the X coordinate of the first Point of a Cubic Bezier.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("ax")]
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
        [XmlAttribute("ay")]
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
        /// Gets or sets the X coordinate of the second Point of a Cubic Bezier.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("bx")]
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
        /// <remarks></remarks>
        [XmlAttribute("cx")]
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
        [XmlAttribute("cy")]
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
        /// Gets or sets the Y coordinate of the second Point of a Cubic Bezier.
        /// </summary>
        [XmlAttribute("by")]
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
        /// Gets the Polynomial degree of the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public PolynomialDegree Degree
            => PolynomialDegree.Quadratic;

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(QuadraticBezier left, QuadraticBezier right)
            => left.Equals(right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
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

        #endregion

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

        #region Methods

        /// <summary>
        /// Samples the bezier curve at the given t value.
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
            return (ti * ti * ti * A) + (3d * ti * ti * t * B) + (3d * ti * t * t * C);
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
            var ti = 1d - t;
            return (3d * ti * ti * (B - A)) + (6d * t * ti * (C - B));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Point2D Interpolate(double t)
            => new Point2D(Interpolators.QuadraticBezier(ax, ay, bx, by, cx, cy, t));

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<Point2D> GetEnumerator()
        {
            yield return new Point2D(Interpolators.QuadraticBezier(A.X, A.Y, B.X, B.Y, C.X, C.Y, Length));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(QuadraticBezier other)
            => A.Equals(other?.A) && B.Equals(other?.B) && C.Equals(other?.C);

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
            if (this == null) return nameof(QuadraticBezier);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(QuadraticBezier)}={{{nameof(A)}={A}{sep}{nameof(B)}={B}{sep}{nameof(C)}={C}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
