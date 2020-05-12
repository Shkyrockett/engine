// <copyright file="Ellipse2D.cs" company="Shkyrockett" >
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
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Polynomials;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// http://math.stackexchange.com/questions/426150/what-is-the-general-equation-of-the-ellipse-that-is-not-in-the-origin-and-rotate
    /// The circle struct.
    /// </summary>
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="Engine.IPropertyCaching" />
    /// <seealso cref="System.IEquatable{T}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Ellipse2D>))]
    [XmlType(TypeName = "ellipse", Namespace = "http://www.w3.org/2000/svg")]
    [DebuggerDisplay("{ToString()}")]
    public struct Ellipse2D
        : IClosedShape, IPropertyCaching, IEquatable<Ellipse2D>
    {
        #region Implementations
        /// <summary>
        /// The empty.
        /// </summary>
        public static readonly Ellipse2D Empty = new Ellipse2D(0, 0, 0, 0);
        #endregion

        #region Fields
        /// <summary>
        /// The center coordinate point of the <see cref="Ellipse2D"/>.
        /// </summary>
        private Point2D center;

        /// <summary>
        /// Major Radius of <see cref="Ellipse2D"/>.
        /// </summary>
        private double radiusA;

        /// <summary>
        /// Minor Radius of <see cref="Ellipse2D"/>.
        /// </summary>
        private double radiusB;

        /// <summary>
        /// Angle of <see cref="Ellipse2D"/>.
        /// </summary>
        private double angle;
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
        /// Initializes a new instance of the <see cref="Ellipse2D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Ellipse2D((double X, double Y, double RX, double RY, double Angle) tuple)
            : this(new Point2D(tuple.X, tuple.Y), tuple.RX, tuple.RY, tuple.Angle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse2D" /> class.
        /// </summary>
        /// <param name="x">Center Point x coordinate of <see cref="Ellipse2D" />.</param>
        /// <param name="y">Center Point x coordinate of <see cref="Ellipse2D" />.</param>
        /// <param name="rX">Major radius of <see cref="Ellipse2D" />.</param>
        /// <param name="rY">Minor radius of <see cref="Ellipse2D" />.</param>
        /// <param name="angle">The angle the <see cref="Ellipse2D"/> is rotated about the center.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Ellipse2D(double x, double y, double rX, double rY, double angle = 0)
            : this(new Point2D(x, y), rX, rY, angle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse2D" /> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="Ellipse2D" />.</param>
        /// <param name="a">Major radius of <see cref="Ellipse2D" />.</param>
        /// <param name="b">Minor radius of <see cref="Ellipse2D" />.</param>
        /// <param name="angle">The angle the <see cref="Ellipse2D" /> is rotated about the center.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Ellipse2D(Point2D center, double a, double b, double angle = 0)
            : this()
        {
            (this.center, radiusA, radiusB, this.angle) = (center, a, b, angle);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse2D" /> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="Ellipse2D" />.</param>
        /// <param name="size">Major and Minor radii of <see cref="Ellipse2D" />.</param>
        /// <param name="angle">Angle of <see cref="Ellipse2D" />.</param>
        public Ellipse2D(Point2D center, Size2D size, double angle = 0)
            : this(center, size.Width, size.Height, angle)
        { }
        #endregion

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Ellipse2D" /> to a <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="RadiusA">The radius a.</param>
        /// <param name="RadiusB">The radius b.</param>
        /// <param name="angle">The angle.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out (double X, double Y) center, out double RadiusA, out double RadiusB, out double angle) => (center, RadiusA, RadiusB, angle) = (this.center, radiusA, radiusB, this.angle);

        /// <summary>
        /// Deconstruct this <see cref="Ellipse2D" /> to a <see cref="ValueTuple{T1, T2, T3, T4, T5}" />.
        /// </summary>
        /// <param name="X">The x.</param>
        /// <param name="Y">The y.</param>
        /// <param name="RadiusA">The radius a.</param>
        /// <param name="RadiusB">The radius b.</param>
        /// <param name="angle">The angle.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double X, out double Y, out double RadiusA, out double RadiusB, out double angle) => (X, Y, RadiusA, RadiusB, angle) = (center.X, center.Y, radiusA, radiusB, this.angle);

        /// <summary>
        /// Deconstruct this <see cref="Ellipse2D" /> to a <see cref="ValueTuple{T1, T2, T3}" />.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="RadiusA">The radius a.</param>
        /// <param name="RadiusB">The radius b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out (double X, double Y) center, out double RadiusA, out double RadiusB) => (center, RadiusA, RadiusB) = (this.center, radiusA, radiusB);

        /// <summary>
        /// Deconstruct this <see cref="Ellipse2D" /> to a <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </summary>
        /// <param name="X">The left.</param>
        /// <param name="Y">The top.</param>
        /// <param name="RadiusA">The radius a.</param>
        /// <param name="RadiusB">The radius b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double X, out double Y, out double RadiusA, out double RadiusB) => (X, Y, RadiusA, RadiusB) = (center.X, center.Y, radiusA, radiusB);
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the center point of the <see cref="Ellipse2D" />.
        /// </summary>
        /// <value>
        /// The center.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Center) + " location of the " + nameof(Ellipse2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Center
        {
            get { return center; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                center = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Location" /> of the <see cref="Ellipse2D" />
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Location
        {
            get { return center; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                center = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the center <see cref="X" /> coordinate location of the center of the <see cref="Ellipse2D" />.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [DataMember(Name = nameof(X)), XmlAttribute(nameof(X)), SoapAttribute(nameof(X))]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center x coordinate location of the circle.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double X
        {
            get { return center.X; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                center.X = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the center <see cref="Y" /> coordinate location of the center of the <see cref="Ellipse2D" />.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [DataMember(Name = nameof(Y)), XmlAttribute(nameof(Y)), SoapAttribute(nameof(Y))]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center y coordinate location of the circle.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double Y
        {
            get { return center.Y; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                center.Y = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the a radius of <see cref="Ellipse2D" />.
        /// </summary>
        /// <value>
        /// The radius a.
        /// </value>
        [DataMember(Name = nameof(RadiusA)), XmlAttribute(nameof(RadiusA)), SoapAttribute(nameof(RadiusA))]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The a radius of the " + nameof(Ellipse2D) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double RadiusA
        {
            get { return radiusA; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                radiusA = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the b radius of Ellipse
        /// </summary>
        /// <value>
        /// The radius b.
        /// </value>
        [DataMember(Name = nameof(RadiusB)), XmlAttribute(nameof(RadiusB)), SoapAttribute(nameof(RadiusB))]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The b radius of the " + nameof(Ellipse2D) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double RadiusB
        {
            get { return radiusB; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                radiusB = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the Major radius of <see cref="Ellipse2D" />.
        /// </summary>
        /// <value>
        /// The major radius.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The larger radius of the " + nameof(Ellipse2D) + ".")]
        public double MajorRadius => radiusA >= radiusB ? radiusA : radiusB;

        /// <summary>
        /// Gets the Minor radius of <see cref="Ellipse2D" />.
        /// </summary>
        /// <value>
        /// The minor radius.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The smaller radius of the " + nameof(Ellipse2D) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double MinorRadius => radiusA <= radiusB ? radiusA : radiusB;

        /// <summary>
        /// Gets or sets the Aspect ratio of <see cref="Ellipse2D" />.
        /// </summary>
        /// <value>
        /// The aspect.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Aspect) + " ratio of the major and minor axis of the " + nameof(Ellipse2D) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double Aspect
        {
            get { return Measurements.Aspect(radiusA, radiusB); }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                radiusB = radiusA * value;
                radiusA = radiusB / value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the angle the <see cref="Ellipse2D" /> is rotated about the center.
        /// </summary>
        /// <value>
        /// The angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Elements")]
        [Description("The " + nameof(Angle) + " to rotate the " + nameof(Ellipse2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public double Angle
        {
            get { return angle; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                angle = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Angle of the <see cref="Ellipse2D" /> in Degrees.
        /// </summary>
        /// <value>
        /// The angle degrees.
        /// </value>
        [DataMember(Name = nameof(Angle)), XmlAttribute(nameof(Angle)), SoapAttribute(nameof(Angle))]
        [Browsable(false)]
        [GeometryAngleDegrees]
        [Category("Elements")]
        [Description("The " + nameof(Angle) + " to rotate the " + nameof(Ellipse2D) + " in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double AngleDegrees
        {
            get { return angle.RadiansToDegrees(); }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                angle = value.DegreesToRadians();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the cosine of the angle.
        /// </summary>
        /// <value>
        /// The cos angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double CosAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = angle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Cos(a));
            }
        }

        /// <summary>
        /// Gets the sine of the angle.
        /// </summary>
        /// <value>
        /// The sin angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double SinAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = angle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Sin(a));
            }
        }

        /// <summary>
        /// Gets the Focus Radius of the <see cref="Ellipse2D" />.
        /// </summary>
        /// <value>
        /// The focus radius.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The focus radius of the " + nameof(Ellipse2D) + ".")]
        public double FocusRadius
        {
            get
            {
                (var radiusA, var radiusB) = (this.radiusA, this.radiusB);
                return (double)(this as IPropertyCaching).CachingProperty(() => Measurements.EllipseFocusRadius(radiusA, radiusB));
            }
        }

        /// <summary>
        /// Gets the <see cref="Eccentricity" /> of the <see cref="Ellipse2D" />.
        /// </summary>
        /// <value>
        /// The eccentricity.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Eccentricity) + " of the " + nameof(Ellipse2D) + ".")]
        public double Eccentricity
        {
            get
            {
                (var radiusA, var radiusB) = (this.radiusA, this.radiusB);
                return (double)(this as IPropertyCaching).CachingProperty(() => Measurements.Eccentricity(radiusA, radiusB));
            }
        }

        /// <summary>
        /// Gets the <see cref="Perimeter" /> of the <see cref="Ellipse2D" />.
        /// </summary>
        /// <value>
        /// The perimeter.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Perimeter) + " of the " + nameof(Ellipse2D) + ".")]
        public double Perimeter
        {
            get
            {
                (var radiusA, var radiusB) = (this.radiusA, this.radiusB);
                return (double)(this as IPropertyCaching).CachingProperty(() => Measurements.EllipsePerimeter(radiusA, radiusB));
            }
        }

        /// <summary>
        /// Gets the <see cref="Area"/> of the <see cref="Ellipse2D"/>.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Area) + " of the " + nameof(Ellipse2D) + ".")]
        public double Area
        {
            get { return Measurements.EllipseArea(radiusA, radiusB); }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                // ToDo: Figure out the correct formula.
                var a = Aspect;
                radiusA = value * a / PI;
                radiusB = value * a / PI;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the angles of the extreme points of the rotated ellipse.
        /// </summary>
        /// <value>
        /// The extreme angles.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The angles of the extreme points of the " + nameof(Ellipse2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<double> ExtremeAngles
        {
            get
            {
                (var radiusA, var radiusB, var angle) = (this.radiusA, this.radiusB, this.angle);
                return (List<double>)(this as IPropertyCaching).CachingProperty(() => Measurements.EllipseExtremeAngles(radiusA, radiusB, angle));
            }
        }

        /// <summary>
        /// Get the points of the Cartesian extremes of a rotated ellipse.
        /// </summary>
        /// <value>
        /// The extreme points.
        /// </value>
        /// <acknowledgment>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </acknowledgment>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The locations of the extreme points of the " + nameof(Ellipse2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> ExtremePoints
        {
            get
            {
                (var x, var y, var radiusA, var radiusB, var angle) = (center.X, center.Y, this.radiusA, this.radiusB, this.angle);
                return (List<Point2D>)(this as IPropertyCaching).CachingProperty(() => Measurements.EllipseExtremePoints(x, y, radiusA, radiusB, angle));
            }
        }

        /// <summary>
        /// Gets an sets the Bounding box of the <see cref="Ellipse2D" />.
        /// </summary>
        /// <value>
        /// The bounds.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The rectangular bounds of the " + nameof(Ellipse2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public Rectangle2D Bounds
        {
            get { return Measurements.EllipseBounds(center.X, center.Y, radiusA, radiusB, angle); }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                var bounds1 = Bounds;
                var aspect = Aspect;
                var bounds2 = value;

                var locDif = bounds1 == null
                    ? bounds2 == null ? Vector2D.Empty : (Vector2D)bounds2.Location
                    : bounds2 == null ? (Vector2D)bounds1.Location : bounds2.Location - bounds1.Location;
                //var scaleDif = bounds2.Size - bounds1.Size;
                Center += locDif;
                if (aspect > 1)
                {
                    radiusA = radiusA / bounds1.Width * bounds2.Width;
                    radiusB = radiusB / bounds1.Height * bounds2.Height;
                }
                else
                {
                    radiusB = radiusB / bounds1.Width * bounds2.Width;
                    radiusA = radiusA / bounds1.Height * bounds2.Height;
                }
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the size and location of the ellipse, in double-point pixels, relative to the parent canvas.
        /// </summary>
        /// <value>
        /// The orthogonal bounds.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The orthogonal rectangular bounds of the " + nameof(Ellipse2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public Rectangle2D OrthogonalBounds
        {
            get
            {
                (var x, var y, var radiusA, var radiusB) = (center.X, center.Y, this.radiusA, this.radiusB);
                return (Rectangle2D)(this as IPropertyCaching).CachingProperty(() => Measurements.EllipseBounds(x, y, radiusA, radiusB));
            }
        }

        /// <summary>
        /// Gets the <see cref="Ellipse2D" /> curve's conic polynomial representation.
        /// </summary>
        /// <value>
        /// The conic section curve.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial ConicSectionCurve
        {
            get
            {
                (var x, var y, var radiusA, var radiusB, var cos, var sin) = (center.X, center.Y, this.radiusA, this.radiusB, CosAngle, SinAngle);
                var curveY = (Polynomial)(this as IPropertyCaching).CachingProperty(() => (Polynomial)EllipseConicSectionPolynomial(x, y, radiusA, radiusB, cos, sin));
                curveY.IsReadonly = true;
                return curveY;
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
        public static bool operator ==(Ellipse2D left, Ellipse2D right) => left.Equals(right);

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
        public static bool operator !=(Ellipse2D left, Ellipse2D right) => !(left == right);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Ellipse2D((double X, double Y, double RX, double RY, double Angle) tuple) => FromValueTuple(tuple);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Ellipse2D && Equals((Ellipse2D)obj);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] Ellipse2D other) => X == other.X && Y == other.Y && RadiusA == other.RadiusA && RadiusB == other.RadiusB && Angle == other.Angle && CosAngle == other.CosAngle && SinAngle == other.SinAngle;

        /// <summary>
        /// Creates a new <see cref="Ellipse2D"/> from a <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ellipse2D FromValueTuple((double X, double Y, double RX, double RY, double Angle) tuple) => new Ellipse2D(tuple);
        #endregion

        #region Interpolators
        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2D Interpolate(double t) => Interpolators.Ellipse(t, center.X, center.Y, radiusA, radiusB, angle);
        #endregion Interpolators

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Point2D point) => Intersections.Contains(this, point) != Inclusions.Outside;

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
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(X, Y, RadiusA, RadiusB, Angle);

        /// <summary>
        /// Creates a <see cref="string"/> representation of this <see cref="IShape"/> interface based on the current culture.
        /// </summary>
        /// <returns>A <see cref="string"/> representation of this instance of the <see cref="IShape"/> object.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Ellipse2D"/> struct based on the format string
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null)
            {
                return nameof(Ellipse2D);
            }

            var sep = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Ellipse2D)}({nameof(X)}: {X.ToString(format, formatProvider)}{sep} {nameof(Y)}: {Y.ToString(format, formatProvider)}{sep} {nameof(RadiusA)}: {RadiusA.ToString(format, formatProvider)}{sep} {nameof(RadiusB)}: {RadiusB.ToString(format, formatProvider)}{sep} {nameof(Angle)}: {Angle.ToString(format, formatProvider)})";
        }
        #endregion
    }
}
