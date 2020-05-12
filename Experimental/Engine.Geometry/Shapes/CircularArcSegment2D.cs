// <copyright file="CircularArcSegment2D.cs" company="Shkyrockett" >
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
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The circular arc segment class.
    /// </summary>
    /// <seealso cref="Engine.IShapeSegment" />
    /// <seealso cref="Engine.IPropertyCaching" />
    /// <seealso cref="System.IEquatable{T}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<CircularArcSegment2D>))]
    [XmlType(TypeName = "arc-Circular")]
    [DebuggerDisplay("{ToString()}")]
    public struct CircularArcSegment2D
        : IShapeSegment, IPropertyCaching, IEquatable<CircularArcSegment2D>
    {
        #region Implementations
        /// <summary>
        /// The empty.
        /// </summary>
        public static readonly CircularArcSegment2D Empty = new CircularArcSegment2D(Point2D.Empty, 0d, 0d, Mathematics.Tau);
        #endregion

        #region Fields
        /// <summary>
        /// The center coordinate point of the circle.
        /// </summary>
        private Point2D center;

        /// <summary>
        /// The radius of the Arc.
        /// </summary>
        private double radius;

        /// <summary>
        /// The start angle.
        /// </summary>
        private double startAngle;

        /// <summary>
        /// The sweep angle.
        /// </summary>
        private double sweepAngle;
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
        /// Initializes a new instance of the <see cref="CircularArcSegment2D"/> class.
        /// </summary>
        public CircularArcSegment2D(Circle2D circle, double startAngle, double sweepAngle)
            : this(circle.Center, circle.Radius, startAngle, sweepAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArcSegment2D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CircularArcSegment2D((double X, double Y, double Radius, double StartAngle, double SweepAngle) tuple)
            : this(new Point2D(tuple.X, tuple.Y), tuple.Radius, tuple.StartAngle, tuple.SweepAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArcSegment2D" /> class.
        /// </summary>
        /// <param name="x">The center x coordinate point of the circle.</param>
        /// <param name="y">The center y coordinate point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CircularArcSegment2D(double x, double y, double radius, double startAngle, double sweepAngle)
            : this(new Point2D(x, y), radius, startAngle, sweepAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArcSegment2D" /> class.
        /// </summary>
        /// <param name="center">The center point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CircularArcSegment2D(Point2D center, double radius, double startAngle, double sweepAngle)
            : this()
        {
            (this.center, this.radius, this.startAngle, this.sweepAngle) = (center, radius, startAngle, sweepAngle);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArcSegment2D" /> class.
        /// </summary>
        /// <param name="triangle"></param>
        public CircularArcSegment2D(Triangle2D triangle)
            : this(triangle.A, triangle.B, triangle.C)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArcSegment2D" /> class.
        /// </summary>
        /// <param name="PointA">The point a.</param>
        /// <param name="PointB">The point b.</param>
        /// <param name="PointC">The point c.</param>
        public CircularArcSegment2D(Point2D PointA, Point2D PointB, Point2D PointC)
            : this()
        {
            // ToDo: calculate the angles of the start and end points from the center to fill them in.
            // Calculate the slopes of the lines.
            var slopeA = PointA.Slope(PointB);
            var slopeB = PointC.Slope(PointB);
            var f = new Vector2D((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X)),
                (((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X)));

            // Find the center.
            center = (f.I - (slopeB * ((f.I - f.J) / (slopeB - slopeA))), (f.I - f.J) / (slopeB - slopeA));

            // Get the radius.
            radius = center.Distance(PointA);
        }
        #endregion

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Circle2D" /> to a Tuple.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void Deconstruct(out (double x, double y) center, out double radius, out double startAngle, out double sweepAngle) => (center, radius, startAngle, sweepAngle) = ((this.center.X, this.center.Y), this.radius, this.startAngle, this.sweepAngle);

        /// <summary>
        /// Deconstruct this <see cref="Circle2D" /> to a Tuple.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void Deconstruct(out double x, out double y, out double radius, out double startAngle, out double sweepAngle) => (x, y, radius, startAngle, sweepAngle) = (this.center.X, this.center.Y, this.radius, this.startAngle, this.sweepAngle);
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the location of the center point of the circular arc.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The location of the center point of the circular arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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
        /// Gets or sets the center of the <see cref="CircularArcSegment2D"/>.
        /// </summary>
        /// <value>
        /// The center.
        /// </value>
        [Category("Elements")]
        [Description("The center location of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        /// Gets or sets the <see cref="X" /> coordinate location of the center of the <see cref="CircularArcSegment2D" />.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [Category("Elements")]
        [Description("The center x coordinate location of the arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [DataMember, XmlAttribute(nameof(X)), SoapAttribute]
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
        /// Gets or sets the <see cref="Y" /> coordinate location of the center of the <see cref="CircularArcSegment2D" />.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [Category("Elements")]
        [Description("The center y coordinate location of the arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [DataMember, XmlAttribute(nameof(Y)), SoapAttribute]
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
        /// Gets or sets the radius of the <see cref="CircularArcSegment2D" />.
        /// </summary>
        /// <value>
        /// The radius.
        /// </value>
        [RefreshProperties(RefreshProperties.All)]
        [Category("Elements")]
        [Description("The radius of the Arc.")]
        [DataMember, XmlAttribute("r"), SoapAttribute]
        public double Radius
        {
            get { return radius; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                radius = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the start angle of the <see cref="CircularArcSegment2D" />.
        /// </summary>
        /// <value>
        /// The start angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        [Category("Elements")]
        [Description("The start angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngle
        {
            get { return startAngle; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                startAngle = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the start angle of the Arc in Degrees.
        /// </summary>
        /// <value>
        /// The start angle degrees.
        /// </value>
        [XmlAttribute("angle-Start")]
        [GeometryAngleDegrees]
        [Category("Clipping")]
        [Description("The start angle of the Arc in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngleDegrees
        {
            get { return startAngle.RadiansToDegrees(); }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                startAngle = value.DegreesToRadians();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the point on the circular arc circumference coincident to the starting angle.
        /// </summary>
        /// <value>
        /// The start point.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the circular arc circumference coincident to the starting angle.")]
        public Point2D StartPoint
        {
            get
            {
                (var x, var y, var r, var startAngle, var sweepAngle) = (center.X, center.Y, radius, this.startAngle, this.sweepAngle);
                return (Point2D)(this as IPropertyCaching).CachingProperty(() => (Point2D)Interpolators.CircularArc(0d, x, y, r, startAngle, sweepAngle));
            }
        }

        /// <summary>
        /// Gets or sets the sweep angle of the <see cref="CircularArcSegment2D" />.
        /// </summary>
        /// <value>
        /// The sweep angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        [Category("Elements")]
        [Description("The sweep angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public double SweepAngle
        {
            get { return sweepAngle; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                sweepAngle = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the sweep angle of the Arc in Degrees.
        /// </summary>
        /// <value>
        /// The sweep angle degrees.
        /// </value>
        [XmlAttribute("angle-Sweep")]
        [GeometryAngleDegrees]
        [Category("Clipping")]
        [Description("The sweep angle of the Arc in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double SweepAngleDegrees
        {
            get { return sweepAngle.RadiansToDegrees(); }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                sweepAngle = value.DegreesToRadians();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the end angle of the Arc.
        /// </summary>
        /// <value>
        /// The end angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        [Category("Elements")]
        [Description("The end angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngle
        {
            get { return startAngle + sweepAngle; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                sweepAngle = value - startAngle;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the end angle of the Arc.
        /// </summary>
        /// <value>
        /// The end angle degrees.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        [GeometryAngleDegrees]
        [Category("Clipping")]
        [Description("The end angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngleDegrees
        {
            get { return (startAngle + sweepAngle).RadiansToDegrees(); }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                sweepAngle = value.DegreesToRadians() - startAngle;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the point on the circular arc circumference coincident to the ending angle.
        /// </summary>
        /// <value>
        /// The end point.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the circular arc circumference coincident to the ending angle.")]
        public Point2D EndPoint
        {
            get
            {
                (var x, var y, var r, var startAngle, var sweepAngle) = (center.X, center.Y, radius, this.startAngle, this.sweepAngle);
                return (Point2D)(this as IPropertyCaching).CachingProperty(() => (Point2D)Interpolators.CircularArc(1d, x, y, r, startAngle, sweepAngle));
            }
        }

        /// <summary>
        /// Gets the arc length of the circular arc.
        /// </summary>
        /// <value>
        /// The length of the arc.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The arc length of the circular arc.")]
        public double ArcLength
        {
            get
            {
                (var r, var sweepAngle) = (radius, this.sweepAngle);
                return (double)(this as IPropertyCaching).CachingProperty(() => Measurements.ArcLength(r, sweepAngle));
            }
        }

        /// <summary>
        /// Gets the length of the perimeter of the circular arc.
        /// </summary>
        /// <value>
        /// The perimeter.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The length of the perimeter of the circular arc.")]
        public double Perimeter => ArcLength;

        /// <summary>
        /// Gets the area of the circular sector contained by the arc.
        /// </summary>
        /// <value>
        /// The area.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The area of the circular sector contained by the arc.")]
        public double Area
        {
            get
            {
                (var r, var sweepAngle) = (radius, this.sweepAngle);
                return (double)(this as IPropertyCaching).CachingProperty(() => Measurements.CircularArcSectorArea(r, sweepAngle));
            }
        }

        /// <summary>
        /// Gets the angles of the extreme points of the circle.
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
                (var startAngle, var sweepAngle) = (this.startAngle, this.sweepAngle);
                return (List<double>)(this as IPropertyCaching).CachingProperty(() => Measurements.CirclularArcExtremeAngles(startAngle, sweepAngle));
            }
        }

        /// <summary>
        /// Get the points of the Cartesian extremes of the circle.
        /// </summary>
        /// <value>
        /// The extreme points.
        /// </value>
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
                (var x, var y, var r) = (center.X, center.Y, radius);
                return (List<Point2D>)(this as IPropertyCaching).CachingProperty(() => Measurements.EllipseExtremePoints(x, y, r, r, 0));
            }
        }

        /// <summary>
        /// Gets the axis aligned bounding box of the circular arc.
        /// </summary>
        /// <value>
        /// The bounds.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The axis aligned bounding box of the circular arc.")]
        public Rectangle2D Bounds
        {
            get
            {
                (var x, var y, var r, var startAngle, var sweepAngle) = (center.X, center.Y, radius, this.startAngle, this.sweepAngle);
                return (Rectangle2D)(this as IPropertyCaching).CachingProperty(() => Measurements.CircularArcBounds(x, y, r, 0, startAngle, sweepAngle));
            }
        }

        /// <summary>
        /// Gets the axis aligned bounding box of the complete circle that the arc is a segment of.
        /// </summary>
        /// <value>
        /// The drawing bounds.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The axis aligned bounding box of the complete circle that the arc is a segment of.")]
        public Rectangle2D DrawingBounds
        {
            get
            {
                (var x, var y, var r) = (center.X, center.Y, radius);
                return (Rectangle2D)(this as IPropertyCaching).CachingProperty(() => Measurements.CircleBounds(x, y, r));
            }
        }

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        [Browsable(false)]
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Dictionary<object, object> IPropertyCaching.PropertyCache { get; set; }
        public IShapeSegment Leading { get; set; }
        public IShapeSegment Trailing { get; set; }
        public Point2D? Head { get; set; }
        public Point2D? Tail { get; set; }
        public double Length { get; set; }
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
        public static bool operator ==(CircularArcSegment2D left, CircularArcSegment2D right) => left.Equals(right);

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
        public static bool operator !=(CircularArcSegment2D left, CircularArcSegment2D right) => !(left == right);
		
        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator CircularArcSegment2D((double X, double Y, double Radius, double StartAngle, double SweepAngle) tuple) => FromValueTuple(tuple);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is CircularArcSegment2D d && Equals(d);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(CircularArcSegment2D other) => center.Equals(other.center) && radius == other.radius && startAngle == other.startAngle && sweepAngle == other.sweepAngle;

        /// <summary>
        /// Creates a new <see cref="CircularArcSegment2D"/> from a <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArcSegment2D FromValueTuple((double X, double Y, double Radius, double StartAngle, double SweepAngle) tuple) => new CircularArcSegment2D(tuple);
        #endregion

        #region Interpolators
        /// <summary>
        /// Interpolates the circlular arc.
        /// </summary>
        /// <param name="t">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public Point2D Interpolate(double t) => Interpolators.CircularArc(t, center.X, center.Y, radius, startAngle, SweepAngle);
        #endregion

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
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
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(center.X, center.Y, radius, startAngle, sweepAngle);

        /// <summary>
        /// Creates a <see cref="string" /> representation of this <see cref="IShape" /> interface based on the current culture.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> representation of this instance of the <see cref="IShape" /> object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a <see cref="string" /> representation of this <see cref="CircularArcSegment2D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null) { return nameof(CircularArcSegment2D); }
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(CircularArcSegment2D)}{{{nameof(Center)}={Center}{sep}{nameof(Radius)}={radius}{sep}{nameof(StartAngle)}={startAngle}{sep}{nameof(EndAngle)}={sweepAngle}}}";
            return formatable.ToString(format, provider);
        }
        #endregion
    }
}