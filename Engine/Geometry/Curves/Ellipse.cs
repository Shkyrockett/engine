// <copyright file="Ellipse.cs" company="Shkyrockett" >
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
using static System.Math;

namespace Engine
{
    /// <summary>
    /// http://math.stackexchange.com/questions/426150/what-is-the-general-equation-of-the-ellipse-that-is-not-in-the-origin-and-rotate
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Ellipse))]
    [XmlType(TypeName = "ellipse", Namespace = "http://www.w3.org/2000/svg")]
    public class Ellipse
        : Shape
    {
        #region Fields

        /// <summary>
        /// The center x coordinate point of the <see cref="Ellipse"/>.
        /// </summary>
        private double x;

        /// <summary>
        /// The center y coordinate point of the <see cref="Ellipse"/>.
        /// </summary>
        private double y;

        /// <summary>
        /// Major Radius of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        private double rX;

        /// <summary>
        /// Minor Radius of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        private double rY;

        /// <summary>
        /// Angle of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        private double angle;

        #endregion

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        public Ellipse()
            : this(0, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse"/> class.
        /// </summary>
        /// <param name="x">Center Point x coordinate of <see cref="Ellipse"/>.</param>
        /// <param name="y">Center Point x coordinate of <see cref="Ellipse"/>.</param>
        /// <param name="rX">Major radius of <see cref="Ellipse"/>.</param>
        /// <param name="rY">Minor radius of <see cref="Ellipse"/>.</param>
        /// <param name="angle">Angle of <see cref="Ellipse"/>.</param>
        /// <remarks></remarks>
        public Ellipse(double x, double y, double rX, double rY, double angle = 0)
        {
            this.x = x;
            this.y = y;
            this.rX = rX;
            this.rY = rY;
            this.angle = angle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse"/> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="Ellipse"/>.</param>
        /// <param name="a">Major radius of <see cref="Ellipse"/>.</param>
        /// <param name="b">Minor radius of <see cref="Ellipse"/>.</param>
        /// <param name="angle">Angle of <see cref="Ellipse"/>.</param>
        /// <remarks></remarks>
        public Ellipse(Point2D center, double a, double b, double angle = 0)
        {
            x = center.X;
            y = center.Y;
            rX = a;
            rY = b;
            this.angle = angle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse"/> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="Ellipse"/>.</param>
        /// <param name="size">Major and Minor radii of <see cref="Ellipse"/>.</param>
        /// <param name="angle">Angle of <see cref="Ellipse"/>.</param>
        /// <remarks></remarks>
        public Ellipse(Point2D center, Size2D size, double angle = 0)
            : this(center, size.Width, size.Height, angle)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="Location"/> of the <see cref="Ellipse"/>
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        public Point2D Location
        {
            get { return new Point2D(x - rX, y - rY); }
            set
            {
                x = value.X + rX;
                y = value.Y + rY;
                ClearCache();
                OnPropertyChanged(nameof(Location));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Center Point of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Center) + " location of the " + nameof(Ellipse) + ".")]
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
        /// Gets or sets the X coordinate location of the center of the circle.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("x")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center x coordinate location of the circle.")]
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
        /// Gets or sets the Y coordinate location of the center of the circle.
        /// </summary>
        [XmlAttribute("y")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center y coordinate location of the circle.")]
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
        /// Gets or sets the first radius of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("rx")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The first radius of the " + nameof(Ellipse) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double RX
        {
            get { return rX; }
            set
            {
                rX = value;
                ClearCache();
                OnPropertyChanged(nameof(RX));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the second radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("ry")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The second radius of the " + nameof(Ellipse) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double RY
        {
            get { return rY; }
            set
            {
                rY = value;
                ClearCache();
                OnPropertyChanged(nameof(RY));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the Major radius of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The larger radius of the " + nameof(Ellipse) + ".")]
        public double MajorRadius
            => rX >= rY ? rX : rY;

        /// <summary>
        /// Gets the Minor radius of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The smaller radius of the " + nameof(Ellipse) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double MinorRadius
            => rX <= rY ? rX : rY;

        /// <summary>
        /// Gets or sets the Aspect ratio of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Aspect) + " ratio of the major and minor axis of the " + nameof(Ellipse) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double Aspect
        {
            get { return Measurements.Aspect(rX, rY); }
            set
            {
                rY = rX * value;
                rX = rY / value;
                ClearCache();
                OnPropertyChanged(nameof(Aspect));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Angle of the <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Elements")]
        [Description("The " + nameof(Angle) + " to rotate the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                ClearCache();
                OnPropertyChanged(nameof(Angle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Angle of the <see cref="Ellipse"/> in Degrees.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("angle")]
        [Browsable(false)]
        [GeometryAngleDegrees]
        [Category("Elements")]
        [Description("The " + nameof(Angle) + " to rotate the " + nameof(Ellipse) + " in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double AngleDegrees
        {
            get { return angle.ToDegrees(); }
            set
            {
                angle = value.ToRadians();
                ClearCache();
                OnPropertyChanged(nameof(AngleDegrees));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double CosAngle
            => (double)CachingProperty(() => Cos(angle));

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double SinAngle
            => (double)CachingProperty(() => Sin(angle));

        /// <summary>
        /// Gets the Focus Radius of the <see cref="Ellipse"/>.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The focus radius of the " + nameof(Ellipse) + ".")]
        public double FocusRadius
            => (double)CachingProperty(() => Measurements.EllipseFocusRadius(rX, rY));

        /// <summary>
        /// Gets the <see cref="Eccentricity"/> of the <see cref="Ellipse"/>.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Eccentricity) + " of the " + nameof(Ellipse) + ".")]
        public double Eccentricity
            => (double)CachingProperty(() => Measurements.Eccentricity(rX, rY));

        /// <summary>
        /// Gets the <see cref="Perimeter"/> of the <see cref="Ellipse"/>.
        /// </summary>
        /// <returns></returns>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Perimeter) + " of the " + nameof(Ellipse) + ".")]
        public override double Perimeter
            => (double)CachingProperty(() => Measurements.EllipsePerimeter(rX, rY));

        /// <summary>
        /// Gets the <see cref="Area"/> of the <see cref="Ellipse"/>.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Area) + " of the " + nameof(Ellipse) + ".")]
        public override double Area
        {
            get { return Measurements.EllipseArea(rX, rY); }
            set
            {
                // ToDo: Figure out the correct formula.
                var a = Aspect;
                rX = value * a / PI;
                rY = value * a / PI;
                ClearCache();
                OnPropertyChanged(nameof(Area));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the angles of the extreme points of the rotated ellipse.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The angles of the extreme points of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<double> ExtremeAngles
            => (List<double>)CachingProperty(() => Measurements.EllipseExtremeAngles(rX, rY, angle));

        /// <summary>
        /// Get the points of the Cartesian extremes of a rotated ellipse.
        /// </summary>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </acknowledgment>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The locations of the extreme points of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> ExtremePoints
            => (List<Point2D>)CachingProperty(() => Measurements.EllipseExtremePoints(x, y, rX, rY, angle));

        /// <summary>
        /// Gets an sets the Bounding box of the <see cref="Ellipse"/>.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The rectangular bounds of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get { return Measurements.EllipseBounds(x, y, rX, rY, angle); }
            set
            {
                Rectangle2D bounds1 = Bounds;
                var aspect = Aspect;
                Rectangle2D bounds2 = value;
                Vector2D locDif = bounds2.Location - bounds1.Location;
                Size2D scaleDif = bounds2.Size - bounds1.Size;
                Center += locDif;
                if (aspect > 1)
                {
                    rX = rX / bounds1.Width * bounds2.Width;
                    rY = rY / bounds1.Height * bounds2.Height;
                }
                else
                {
                    rY = rY / bounds1.Width * bounds2.Width;
                    rX = rX / bounds1.Height * bounds2.Height;
                }
                ClearCache();
                OnPropertyChanged(nameof(Bounds));
            }
        }

        /// <summary>
        /// Gets the size and location of the ellipse, in double-point pixels, relative to the parent canvas.
        /// </summary>
        /// <returns>A System.Drawing.RectangleF in double-point pixels relative to the parent canvas that represents the size and location of the segment.</returns>
        /// <remarks></remarks>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The unrotated rectangular bounds of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public Rectangle2D UnrotatedBounds
            => (Rectangle2D)CachingProperty(() => Measurements.EllipseBounds(x, y, rX, rY));

        #endregion

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Ellipse)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Ellipse)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Ellipse)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Ellipse)} has been deserialized.");
        //}

        //#endregion

        #region Interpolators

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Interpolators.Ellipse(x, y, rX, rY, angle, t);

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
        /// Creates a string representation of this <see cref="Ellipse"/> struct based on the format string
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
                return nameof(Ellipse);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Ellipse)}{{{nameof(Center)}={Center},{nameof(RX)}={rX},{nameof(RY)}={rY},{nameof(Angle)}={angle}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
