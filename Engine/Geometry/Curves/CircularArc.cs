// <copyright file="CircularArc.cs" company="Shkyrockett" >
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
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(CircularArc))]
    [XmlType(TypeName = "arc-Circular")]
    public class CircularArc
        : Shape
    {
        #region Fields

        /// <summary>
        /// The center x coordinate point of the Arc.
        /// </summary>
        private double x;

        /// <summary>
        /// The center y coordinate point of the Arc.
        /// </summary>
        private double y;

        /// <summary>
        /// The radius of the Arc.
        /// </summary>
        private double radius;

        /// <summary>
        /// 
        /// </summary>
        private double startAngle;

        /// <summary>
        /// 
        /// </summary>
        private double sweepAngle;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new default instance of the <see cref="CircularArc"/> class.
        /// </summary>
        public CircularArc()
            : this(0, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArc"/> class.
        /// </summary>
        /// <param name="triangle"></param>
        public CircularArc(Triangle triangle)
            : this(triangle.A, triangle.B, triangle.C)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArc"/> class.
        /// </summary>
        public CircularArc(Circle circle, double startAngle, double sweepAngle)
            : this(circle.Center, circle.Radius, startAngle, sweepAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArc"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        public CircularArc((double X, double Y, double Radius, double StartAngle, double SweepAngle) tuple)
            : this(tuple.X, tuple.Y, tuple.Radius, tuple.StartAngle, tuple.SweepAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArc"/> class.
        /// </summary>
        /// <param name="x">The center x coordinate point of the circle.</param>
        /// <param name="y">The center y coordinate point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public CircularArc(double x, double y, double radius, double startAngle, double sweepAngle)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.startAngle = startAngle;
            this.sweepAngle = sweepAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArc"/> class.
        /// </summary>
        /// <param name="center">The center point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public CircularArc(Point2D center, double radius, double startAngle, double sweepAngle)
        {
            x = center.X;
            y = center.Y;
            this.radius = radius;
            this.startAngle = startAngle;
            this.sweepAngle = sweepAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArc"/> class.
        /// </summary>
        /// <param name="PointA"></param>
        /// <param name="PointB"></param>
        /// <param name="PointC"></param>
        public CircularArc(Point2D PointA, Point2D PointB, Point2D PointC)
        {
            // ToDo: calculate the angles of the start and end points from the center to fill them in.
            // Calculate the slopes of the lines.
            var slopeA = (PointA.Slope(PointB));
            var slopeB = (PointC.Slope(PointB));
            var f = new Vector2D(((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X))),
                ((((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X))));

            // Find the center.
            x = f.I - (slopeB * ((f.I - f.J) / (slopeB - slopeA)));
            y = (f.I - f.J) / (slopeB - slopeA);

            // Get the radius.
            radius = (Center.Distance(PointA));
        }

        #endregion

        #region Deconstructors

        /// <summary>
        /// Deconstruct this <see cref="Circle"/> to a Tuple.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public void Deconstruct(out double x, out double y, out double radius, out double startAngle, out double sweepAngle)
        {
            x = this.x;
            y = this.y;
            radius = this.radius;
            startAngle = this.startAngle;
            sweepAngle = this.sweepAngle;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the location of the center point of the circular arc.
        /// </summary>
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
            get { return new Point2D(x, y); }
            set
            {
                x = value.X;
                y = value.Y;
                ClearCache();
                OnPropertyChanged(nameof(Location));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the center of the Arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Elements")]
        [Description("The center location of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Center
        {
            get { return new Point2D(x, y); }
            set
            {
                x = value.X;
                y = value.Y;
                ClearCache();
                OnPropertyChanged(nameof(Center));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the point on the circular arc circumference coincident to the starting angle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the circular arc circumference coincident to the starting angle.")]
        public Point2D StartPoint
            => (Point2D)CachingProperty(() => (Point2D)Interpolators.CircularArc(x, y, radius, startAngle, sweepAngle, 0));

        /// <summary>
        /// Gets the point on the circular arc circumference coincident to the ending angle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the circular arc circumference coincident to the ending angle.")]
        public Point2D EndPoint
            => (Point2D)CachingProperty(() => (Point2D)Interpolators.CircularArc(x, y, radius, startAngle, sweepAngle, 1));

        /// <summary>
        /// Gets or sets the <see cref="X"/> coordinate location of the center of the <see cref="CircularArc"/>.
        /// </summary>
        [XmlAttribute(nameof(x))]
        [Category("Elements")]
        [Description("The center x coordinate location of the arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                ClearCache();
                OnPropertyChanged(nameof(X));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Y"/> coordinate location of the center of the <see cref="CircularArc"/>.
        /// </summary>
        [XmlAttribute(nameof(y))]
        [Category("Elements")]
        [Description("The center y coordinate location of the arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                ClearCache();
                OnPropertyChanged(nameof(Y));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the radius of the Arc.
        /// </summary>
        [XmlAttribute("r")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Elements")]
        [Description("The radius of the Arc.")]
        public double Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                ClearCache();
                OnPropertyChanged(nameof(Radius));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the start angle of the Arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The start angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngle
        {
            get { return startAngle; }
            set
            {
                startAngle = value;
                ClearCache();
                OnPropertyChanged(nameof(StartAngle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the start angle of the Arc in Degrees.
        /// </summary>
        [XmlAttribute("angle-Start")]
        [GeometryAngleDegrees]
        [Category("Clipping")]
        [Description("The start angle of the Arc in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngleDegrees
        {
            get { return startAngle.ToDegrees(); }
            set
            {
                startAngle = value.ToRadians();
                ClearCache();
                OnPropertyChanged(nameof(StartAngleDegrees));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the sweep angle of the Arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The sweep angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public double SweepAngle
        {
            get { return sweepAngle; }
            set
            {
                sweepAngle = value;
                ClearCache();
                OnPropertyChanged(nameof(SweepAngle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the sweep angle of the Arc in Degrees.
        /// </summary>
        [XmlAttribute("angle-Sweep")]
        [GeometryAngleDegrees]
        [Category("Clipping")]
        [Description("The sweep angle of the Arc in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double SweepAngleDegrees
        {
            get { return sweepAngle.ToDegrees(); }
            set
            {
                sweepAngle = value.ToRadians();
                ClearCache();
                OnPropertyChanged(nameof(SweepAngleDegrees));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the end angle of the Arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The end angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngle
        {
            get { return startAngle + sweepAngle; }
            set
            {
                sweepAngle = value - startAngle;
                ClearCache();
                OnPropertyChanged(nameof(EndAngle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the end angle of the Arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        [GeometryAngleDegrees]
        [Category("Clipping")]
        [Description("The end angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngleDegrees
        {
            get { return (startAngle + sweepAngle).ToDegrees(); }
            set
            {
                sweepAngle = value.ToRadians() - startAngle;
                ClearCache();
                OnPropertyChanged(nameof(EndAngleDegrees));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the arc length of the circular arc.
        /// </summary>
        /// <returns></returns>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The arc length of the circular arc.")]
        public double ArcLength
            => (double)CachingProperty(() => Measurements.ArcLength(radius, SweepAngle));

        /// <summary>
        /// Gets the length of the perimeter of the circular arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The length of the perimeter of the circular arc.")]
        public override double Perimeter
            => ArcLength;

        /// <summary>
        /// Gets the area of the circular sector contained by the arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The area of the circular sector contained by the arc.")]
        public override double Area
            => (double)CachingProperty(() => Measurements.CircularArcSectorArea(radius, SweepAngle));

        /// <summary>
        /// Gets the angles of the extreme points of the circle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The angles of the extreme points of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<double> ExtremeAngles
            => (List<double>)CachingProperty(() => Measurements.CirclularArcExtremeAngles(startAngle, sweepAngle));

        /// <summary>
        /// Get the points of the Cartesian extremes of the circle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The locations of the extreme points of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> ExtremePoints
            => (List<Point2D>)CachingProperty(() => Measurements.EllipseExtremePoints(x, y, radius, radius, 0));

        /// <summary>
        /// Gets the axis aligned bounding box of the circular arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The axis aligned bounding box of the circular arc.")]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.CircularArcBounds(x, y, radius, 0, startAngle, sweepAngle));

        /// <summary>
        /// Gets the axis aligned bounding box of the complete circle that the arc is a segment of.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The axis aligned bounding box of the complete circle that the arc is a segment of.")]
        public Rectangle2D DrawingBounds
            => (Rectangle2D)CachingProperty(() => Measurements.CircleBounds(x, y, radius));

        #endregion

        #region Operators

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"></param>
        public static implicit operator CircularArc((double X, double Y, double Radius, double StartAngle, double SweepAngle) tuple)
            => new CircularArc(tuple);

        #endregion

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(CircularArc)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(CircularArc)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(CircularArc)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(CircularArc)} has been deserialized.");
        //}

        //#endregion

        #region Interpolators

        /// <summary>
        /// Interpolates the Arc.
        /// </summary>
        /// <param name="t">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public override Point2D Interpolate(double t)
            => Interpolators.CircularArc(x, y, radius, startAngle, SweepAngle, t);

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool Contains(Point2D point)
            => Intersections.Contains(this, point) != Inclusion.Outside;

        /// <summary>
        /// Creates a string representation of this <see cref="CircularArc"/> struct based on the format string
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
            if (this == null)
                return nameof(CircularArc);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(CircularArc)}{{{nameof(Center)}={Center}{sep}{nameof(Radius)}={radius}{sep}{nameof(StartAngle)}={startAngle}{sep}{nameof(EndAngle)}={sweepAngle}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
