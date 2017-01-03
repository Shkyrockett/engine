// <copyright file="EllipticArc.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Xml.Serialization;
using static System.Math;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    /// http://www.vbforums.com/showthread.php?686351-RESOLVED-Elliptical-orbit
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Elliptical Arc")]
    [XmlType(TypeName = "arc-Elliptical")]
    public class EllipticalArc
        : Shape
    {
        #region Fields

        /// <summary>
        /// The center x-coordinate point of the <see cref="EllipticalArc"/>.
        /// </summary>
        private double cX;

        /// <summary>
        /// The center y-coordinate point of the <see cref="EllipticalArc"/>.
        /// </summary>
        private double cY;

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
        ///
        /// </summary>
        public EllipticalArc()
            : this(0, 0, 0, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc"/> class.
        /// </summary>
        /// <param name="x">Center Point x coordinate of <see cref="EllipticalArc"/>.</param>
        /// <param name="y">Center Point x coordinate of <see cref="EllipticalArc"/>.</param>
        /// <param name="rX">Major radius of <see cref="EllipticalArc"/>.</param>
        /// <param name="rY">Minor radius of <see cref="EllipticalArc"/>.</param>
        /// <param name="angle">Angle of <see cref="EllipticalArc"/>.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <remarks></remarks>
        public EllipticalArc(double x, double y, double rX, double rY, double angle, double startAngle, double sweepAngle)
        {
            this.cX = x;
            this.cY = y;
            this.rX = rX;
            this.rY = rY;
            this.angle = angle;
            this.startAngle = startAngle;
            this.sweepAngle = sweepAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc"/> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="EllipticalArc"/>.</param>
        /// <param name="rX">Major radius of <see cref="EllipticalArc"/>.</param>
        /// <param name="rY">Minor radius of <see cref="EllipticalArc"/>.</param>
        /// <param name="angle">Angle of <see cref="EllipticalArc"/>.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <remarks></remarks>
        public EllipticalArc(Point2D center, double rX, double rY, double angle, double startAngle, double sweepAngle)
        {
            cX = center.X;
            cY = center.Y;
            this.rX = rX;
            this.rY = rY;
            this.angle = angle;
            this.startAngle = startAngle;
            this.sweepAngle = sweepAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc"/> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="EllipticalArc"/>.</param>
        /// <param name="size">Major and Minor radii of <see cref="EllipticalArc"/>.</param>
        /// <param name="angle">Angle of <see cref="EllipticalArc"/>.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <remarks></remarks>
        public EllipticalArc(Point2D center, Size2D size, double angle, double startAngle, double sweepAngle)
            : this(center.X, center.Y, size.Width, size.Height, angle, startAngle, sweepAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc"/> class.
        /// </summary>
        /// <param name="rectangle">The boundaries of the ellipse</param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        /// <remarks></remarks>
        public EllipticalArc(Rectangle2D rectangle, double angle, double startAngle, double endAngle)
            : this(rectangle.Center(), rectangle.Width, rectangle.Height, angle, startAngle, endAngle)
        { }

        /// <summary>
        /// Creates a new Instance of Ellipse
        /// </summary>
        /// <param name="ellipse">The Ellipse</param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        /// <remarks></remarks>
        public EllipticalArc(Ellipse ellipse, double startAngle, double endAngle)
            : this(ellipse.Center, ellipse.MajorRadius, ellipse.MinorRadius, ellipse.Angle, startAngle, endAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc"/> class.
        /// </summary>
        /// <param name="x0">The starting X-coordinate.</param>
        /// <param name="y0">The starting Y-coordinate.</param>
        /// <param name="rx">The first radius.</param>
        /// <param name="ry">The second Radius.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <param name="largeArcFlag">Flag used to toggle whether to choose the largest matching ellipse.</param>
        /// <param name="sweepFlag">Flag used to toggle whether to choose the upper or lower elliptical solution. </param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// Elliptical arc implementation based on the SVG specification notes
        /// http://stackoverflow.com/questions/14399406/implementing-svg-arc-curves-in-python
        /// https://github.com/blackears/svgSalamander
        /// http://java.net/projects/svgsalamander/sources/svn/content/trunk/svg-core/src/main/java/com/kitfox/svg/pathcmd/Arc.java
        /// http://www.w3.org/TR/SVG/paths.html#PathDataEllipticalArcCommands
        /// </remarks>
        public EllipticalArc(
            double x0, double y0,
            double rx, double ry,
            double angle,
            bool largeArcFlag,
            bool sweepFlag,
            double x, double y)
        {
            // Compute the half distance between the current and the final point
            double dx2 = (x0 - x) * 0.5d;
            double dy2 = (y0 - y) * 0.5d;

            // Get the ellipse rotation transform.
            double cosT = Cos(angle);
            double sinT = Sin(angle);

            //
            // Step 1 : Compute (x1, y1)
            //
            double x1 = (cosT * dx2 + sinT * dy2);
            double y1 = (-sinT * dx2 + cosT * dy2);

            // Ensure radii are large enough
            rx = Abs(rx);
            ry = Abs(ry);
            double Prx = rx * rx;
            double Pry = ry * ry;
            double Px1 = x1 * x1;
            double Py1 = y1 * y1;

            // Check that radii are large enough
            double radiiCheck = Px1 / Prx + Py1 / Pry;
            if (radiiCheck > 1)
            {
                rx = Sqrt(radiiCheck) * rx;
                ry = Sqrt(radiiCheck) * ry;
                Prx = rx * rx;
                Pry = ry * ry;
            }

            //
            // Step 2 : Compute (cx1, cy1)
            //
            double sign = (largeArcFlag == sweepFlag) ? -1 : 1;
            double sq = ((Prx * Pry) - (Prx * Py1) - (Pry * Px1)) / ((Prx * Py1) + (Pry * Px1));
            sq = (sq < 0) ? 0 : sq;
            double coef = (sign * Sqrt(sq));
            double cx1 = coef * ((rx * y1) / ry);
            double cy1 = coef * -((ry * x1) / rx);

            //
            // Step 3 : Compute (cx, cy) from (cx1, cy1)
            //
            double sx2 = (x0 + x) / 2.0;
            double sy2 = (y0 + y) / 2.0;
            double cx = sx2 + (cosT * cx1 - sinT * cy1);
            double cy = sy2 + (sinT * cx1 + cosT * cy1);

            //
            // Step 4 : Compute the angleStart (angle1) and the angleExtent (dangle)
            //
            double ux = (x1 - cx1) / rx;
            double uy = (y1 - cy1) / ry;
            double vx = (-x1 - cx1) / rx;
            double vy = (-y1 - cy1) / ry;

            // Compute the angle start
            double n = Sqrt((ux * ux) + (uy * uy));
            double p = ux; // (1 * ux) + (0 * uy)
            sign = (uy < 0) ? -1d : 1d;
            double angleStart = sign * Acos(p / n);

            // Compute the angle extent
            n = Sqrt((ux * ux + uy * uy) * (vx * vx + vy * vy));
            p = ux * vx + uy * vy;
            sign = (ux * vy - uy * vx < 0) ? -1d : 1d;
            double angleExtent = sign * Acos(p / n);

            if (!sweepFlag && angleExtent > 0)
            {
                angleExtent -= Tau;
            }
            else if (sweepFlag && angleExtent < 0)
            {
                angleExtent += Tau;
            }
            angleExtent %= Tau;
            angleStart %= Tau;

            //
            // We can now build the resulting Arc2D in double precision
            //
            this.cX = cx;
            this.cY = cy;
            this.rX = rx;
            this.rY = ry;
            this.angle = angle;
            this.startAngle = angleStart;
            this.sweepAngle = angleExtent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the location of the center point of the elliptical arc.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The location of the center point of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Location
        {
            get => new Point2D(cX, cY);
            set
            {
                cX = value.X;
                cY = value.Y;
                OnPropertyChanged(nameof(Location));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Center Point of the elliptical arc.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Center) + " location of the " + nameof(EllipticalArc) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Center
        {
            get => new Point2D(cX, cY); set
            {
                cX = value.X;
                cY = value.Y;
                OnPropertyChanged(nameof(Center));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate location of the center of the elliptical arc.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("x")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center x coordinate location of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double X
        {
            get => cX;
            set
            {
                cX = value;
                OnPropertyChanged(nameof(X));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate location of the center of the elliptical arc.
        /// </summary>
        [XmlAttribute("y")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center y coordinate location of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double Y
        {
            get => cY;
            set
            {
                cY = value;
                OnPropertyChanged(nameof(Y));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the first radius of the elliptical arc.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("rx")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The first radius of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double RX
        {
            get => rX;
            set
            {
                rX = value;
                OnPropertyChanged(nameof(RX));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the second radius of elliptical arc.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("ry")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The second radius of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double RY
        {
            get => rY;
            set
            {
                rY = value;
                OnPropertyChanged(nameof(RY));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the Major radius of elliptical arc.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The larger radius of the elliptical arc.")]
        public double MajorRadius
            => rX >= rY ? rX : rY;

        /// <summary>
        /// Gets the Minor radius of the elliptical arc.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The smaller radius of the elliptical arc.")]
        [RefreshProperties(RefreshProperties.All)]
        public double MinorRadius
            => rX <= rY ? rX : rY;

        /// <summary>
        /// Gets or sets the Aspect ratio of the elliptical arc.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Aspect) + " ratio of the major and minor axis of the elliptical arc.")]
        [RefreshProperties(RefreshProperties.All)]
        public double Aspect
        {
            get => rY / rX;
            set
            {
                rY = rX * value;
                rX = rY / value;
                OnPropertyChanged(nameof(Aspect));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Angle of the elliptical arc.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Elements")]
        [Description("The " + nameof(Angle) + " to rotate the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public double Angle
        {
            get => angle;
            set
            {
                angle = value;
                OnPropertyChanged(nameof(Angle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Angle of the elliptical arc in Degrees.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("angle")]
        [Browsable(false)]
        [GeometryAngleDegrees]
        [Category("Elements")]
        [Description("The " + nameof(Angle) + " to rotate the elliptical arc in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double AngleDegrees
        {
            get => angle.ToDegrees();
            set
            {
                angle = value.ToRadians();
                OnPropertyChanged(nameof(Angle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the start angle of the elliptical arc.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The start angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngle
        {
            get => startAngle;
            set
            {
                startAngle = value;
                OnPropertyChanged(nameof(StartAngle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the start angle of the elliptical arc in Degrees.
        /// </summary>
        [XmlAttribute("angle-Start")]
        [Browsable(false)]
        [GeometryAngleDegrees]
        [Category("Clipping")]
        [Description("The start angle of the elliptical arc in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngleDegrees
        {
            get => startAngle.ToDegrees();
            set
            {
                startAngle = value.ToRadians();
                OnPropertyChanged(nameof(StartAngle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the sweep angle of the elliptical arc.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The sweep angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double SweepAngle
        {
            get => sweepAngle;
            set
            {
                sweepAngle = value;
                OnPropertyChanged(nameof(SweepAngle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the sweep angle of the elliptical arc in Degrees.
        /// </summary>
        [XmlAttribute("angle-Sweep")]
        [Browsable(false)]
        [GeometryAngleDegrees]
        [Category("Clipping")]
        [Description("The sweep angle of the elliptical arc in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double SweepAngleDegrees
        {
            get => sweepAngle.ToDegrees();
            set
            {
                sweepAngle = value.ToRadians();
                OnPropertyChanged(nameof(SweepAngleDegrees));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the end angle of the elliptical arc.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The end angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngle
        {
            get => startAngle + sweepAngle;
            set
            {
                sweepAngle = value - startAngle;
                OnPropertyChanged(nameof(EndAngle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the end angle of the elliptical arc in Degrees.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(false)]
        [GeometryAngleDegrees]
        [Category("Clipping")]
        [Description("The end angle of the elliptical arc in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngleDegrees
        {
            get => (startAngle + sweepAngle).ToDegrees();
            set
            {
                sweepAngle = value.ToRadians() - startAngle;
                OnPropertyChanged(nameof(EndAngleDegrees));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the Focus Radius of the elliptical arc.
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Ellipse</remarks>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The focus radius of the elliptical arc.")]
        public double FocusRadius
            => Sqrt((rX * rX) - (rY * rY));

        /// <summary>
        /// Gets the <see cref="Eccentricity"/> of the elliptical arc.
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Ellipse</remarks>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Eccentricity) + " of the elliptical arc.")]
        public double Eccentricity
            => Sqrt(1 - ((rX / rY) * (rX / rY)));

        /// <summary>
        /// Gets the point on the Elliptical arc circumference coincident to the starting angle.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the Elliptical arc circumference coincident to the starting angle.")]
        public Point2D StartPoint
            => Interpolaters.EllipticalArc(cX, cY, rX, rY, angle, startAngle, sweepAngle, 0);

        /// <summary>
        /// Gets the point on the Elliptical arc circumference coincident to the ending angle.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the Elliptical arc circumference coincident to the ending angle.")]
        public Point2D EndPoint
            => Interpolaters.EllipticalArc(cX, cY, rX, rY, angle, startAngle, sweepAngle, 1);

        /// <summary>
        /// Gets the Bounding box of the elliptical arc.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The rectangular bounds of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Boundings.EllipticalArc(cX, cY, rX, rY, angle, startAngle, sweepAngle);

        /// <summary>
        /// Gets the <see cref="Perimeter"/> of the elliptical arc.
        /// </summary>
        /// <returns></returns>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Perimeter) + " of the elliptical arc.")]
        public override double Perimeter
            => Distances.EllipticalArcPerimeter(StartPoint, EndPoint, startAngle, EndAngle);

        /// <summary>
        /// Gets the <see cref="Area"/> of the elliptical arc.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Area) + " of the elliptical arc.")]
        public override double Area
            => Areas.EllipticalArcSector(rX, rY, startAngle, sweepAngle);

        /// <summary>
        /// Gets the size and location of the elliptical arc, in double-point pixels, relative to the parent canvas.
        /// </summary>
        /// <returns>A System.Drawing.RectangleF in double-point pixels relative to the parent canvas that represents the size and location of the segment.</returns>
        /// <remarks></remarks>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The unrotated rectangular bounds of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public Rectangle2D DrawingBounds
            => Boundings.Ellipse(cX, cY, rX, rY);

        #endregion

        #region Interpolaters

        /// <summary>
        /// http://www.vbforums.com/showthread.php?686351-RESOLVED-Elliptical-orbit
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Interpolaters.EllipticalArc(cX, cY, rX, rY, angle, startAngle, sweepAngle, t);

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
        /// Creates a string representation of this elliptical arc struct based on the format string
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
            IFormattable formatable = $"{nameof(EllipticalArc)}{{{nameof(Center)}={Center},{nameof(RX)}={rX},{nameof(RY)}={rY},{nameof(Angle)}={angle},{nameof(StartAngle)}={startAngle},{SweepAngle}={sweepAngle}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
