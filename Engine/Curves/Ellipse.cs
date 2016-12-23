// <copyright file="Ellipse.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// http://math.stackexchange.com/questions/426150/what-is-the-general-equation-of-the-ellipse-that-is-not-in-the-origin-and-rotate
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Ellipse))]
    [XmlType(TypeName = "ellipse")]
    public class Ellipse
        : Shape, IClosedShape
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
        /// <param name="r1">Major radius of <see cref="Ellipse"/>.</param>
        /// <param name="r2">Minor radius of <see cref="Ellipse"/>.</param>
        /// <param name="angle">Angle of <see cref="Ellipse"/>.</param>
        /// <remarks></remarks>
        public Ellipse(double x, double y, double r1, double r2, double angle)
        {
            this.x = x;
            this.y = y;
            this.rX = r1;
            this.rY = r2;
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
        public Ellipse(Point2D center, double a, double b, double angle)
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
        public Ellipse(Point2D center, Size2D size, double angle)
            : this(center, size.Width, size.Height, angle)
        { }

        #endregion

        #region Properties

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore]
        [Browsable(true)]
        public Point2D Location
        {
            get { return new Point2D(x - rX, y - rY); }
            set
            {
                x = value.X + rX;
                y = value.Y + rY;
                OnPropertyChanged(nameof(Location));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Center Point of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
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
                OnPropertyChanged(nameof(RY));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the Major radius of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The larger radius of the " + nameof(Ellipse) + ".")]
        public double MajorRadius
            => rX >= rY ? rX : rY;

        /// <summary>
        /// Gets the Minor radius of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
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
        [XmlIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Aspect) + " ratio of the major and minor axis of the " + nameof(Ellipse) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double Aspect
        {
            get { return rY / rX; }
            set
            {
                rY = rX * value;
                rX = rY / value;
                OnPropertyChanged(nameof(Aspect));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Angle of the <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
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
                OnPropertyChanged(nameof(AngleDegrees));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the Focus Radius of the <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Ellipse</remarks>
        [XmlIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The focus radius of the " + nameof(Ellipse) + ".")]
        public double FocusRadius
            => Sqrt((rX * rX) - (rY * rY));

        /// <summary>
        /// Gets the <see cref="Eccentricity"/> of the <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Ellipse</remarks>
        [XmlIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Eccentricity) + " of the " + nameof(Ellipse) + ".")]
        public double Eccentricity
            => Sqrt(1 - ((rX / rY) * (rX / rY)));

        /// <summary>
        /// Gets the angles of the extreme points of the rotated ellipse.
        /// </summary>
        [XmlIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The angles of the extreme points of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<double> ExtremeAngles
        {
            get
            {
                // Get the ellipse rotation transform.
                double cosT = Cos(angle);
                double sinT = Sin(angle);

                // Calculate the radii of the angle of rotation.
                double a = rX * cosT;
                double b = rY * sinT;
                double c = rX * sinT;
                double d = rY * cosT;

                // Ellipse equation for an ellipse at origin.
                double u1 = rX * Cos(Atan2(d, c));
                double v1 = -(rY * Sin(Atan2(d, c)));
                double u2 = rX * Cos(Atan2(-b, a));
                double v2 = -(rY * Sin(Atan2(-b, a)));

                return new List<double>
                {
                    Atan2(u1 * sinT - v1 * cosT, u1 * cosT + v1 * sinT),
                    Atan2(u2 * sinT - v2 * cosT, u2 * cosT + v2 * sinT),
                    Atan2(u2 * sinT - v2 * cosT, u2 * cosT + v2 * sinT) + PI,
                    Atan2(u1 * sinT - v1 * cosT, u1 * cosT + v1 * sinT) + PI
                };
            }
        }

        /// <summary>
        /// Get the points of the Cartesian extremes of a rotated ellipse.
        /// </summary>
        /// <remarks>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
        [XmlIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The locations of the extreme points of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<Point2D> ExtremePoints
        {
            get
            {
                double a = rX * Cos(angle);
                double c = rX * Sin(angle);
                double d = rY * Cos(angle);
                double b = rY * Sin(angle);

                // Find the angles of the Cartesian extremes.
                double a1 = Atan2(-b, a);
                double a2 = Atan2(-b, a) + PI;
                double a3 = Atan2(d, c);
                double a4 = Atan2(d, c) + PI;

                // Return the points of Cartesian extreme of the rotated ellipse.
                return new List<Point2D> {
                    Interpolaters.Ellipse(x, y, rX, rY, angle, a1),
                    Interpolaters.Ellipse(x, y, rX, rY, angle, a2),
                    Interpolaters.Ellipse(x, y, rX, rY, angle, a3),
                    Interpolaters.Ellipse(x, y, rX, rY, angle, a4)
                };
            }
        }

        /// <summary>
        /// Gets the Bounding box of the <see cref="Ellipse"/>.
        /// </summary>
        [XmlIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The rectangular bounds of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get { return Boundings.Ellipse(x, y, rX, rY, angle); }
            set
            {
                Rectangle2D bounds1 = Bounds;
                double aspect = Aspect;
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
                OnPropertyChanged(nameof(Bounds));
            }
        }

        /// <summary>
        /// Gets the <see cref="Perimeter"/> of the <see cref="Ellipse"/>.
        /// </summary>
        /// <returns></returns>
        [XmlIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Perimeter) + " of the " + nameof(Ellipse) + ".")]
        public override double Perimeter
            => Distances.EllipsePerimeter(rX, rY);

        /// <summary>
        /// Gets the <see cref="Area"/> of the <see cref="Ellipse"/>.
        /// </summary>
        [XmlIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Area) + " of the " + nameof(Ellipse) + ".")]
        public override double Area
        {
            get { return Areas.Ellipse(rX, rY); }
            set
            {
                // ToDo: Figure out the correct formula.
                double a = Aspect;
                rX = value * a / PI;
                rY = value * a / PI;
                OnPropertyChanged(nameof(Area));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the size and location of the ellipse, in double-point pixels, relative to the parent canvas.
        /// </summary>
        /// <returns>A System.Drawing.RectangleF in double-point pixels relative to the parent canvas that represents the size and location of the segment.</returns>
        /// <remarks></remarks>
        [XmlIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The unrotated rectangular bounds of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public Rectangle2D UnrotatedBounds
            => Boundings.Ellipse(x, y, rX, rY);

        #endregion

        #region Interpolaters

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Interpolaters.Ellipse(x, y, rX, rY, angle, t);

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool Contains(Point2D point)
            => Containings.Contains(this, point) != Inclusion.Outside;

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
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Ellipse)}{{{nameof(Center)}={Center},{nameof(RX)}={rX},{nameof(RY)}={rY},{nameof(Angle)}={angle}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
