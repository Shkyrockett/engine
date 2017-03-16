// <copyright file="EllipticArc.cs" company="Shkyrockett" >
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
using System.Diagnostics;
using System.Runtime.Serialization;
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
        private double x;

        /// <summary>
        /// The center y-coordinate point of the <see cref="EllipticalArc"/>.
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
            this.x = x;
            this.y = y;
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
            x = center.X;
            y = center.Y;
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
        /// <param name="startX">The starting X-coordinate.</param>
        /// <param name="startY">The starting Y-coordinate.</param>
        /// <param name="rx">The first radius.</param>
        /// <param name="ry">The second Radius.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <param name="largeArcFlag">Flag used to toggle whether to choose the largest matching ellipse.</param>
        /// <param name="sweepFlag">Flag used to toggle whether to choose the upper or lower elliptical solution. </param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        /// <remarks>
        /// Elliptical arc implementation based on the SVG specification notes
        /// http://stackoverflow.com/questions/14399406/implementing-svg-arc-curves-in-python
        /// https://github.com/blackears/svgSalamander
        /// http://java.net/projects/svgsalamander/sources/svn/content/trunk/svg-core/src/main/java/com/kitfox/svg/pathcmd/Arc.java
        /// http://www.w3.org/TR/SVG/paths.html#PathDataEllipticalArcCommands
        /// </remarks>
        public EllipticalArc(
            double startX, double startY,
            double rx, double ry,
            double angle,
            bool largeArcFlag,
            bool sweepFlag,
            double endX, double endY)
        {
            // Compute the half distance between the start and end points.
            var dx2 = (startX - endX) * OneHalf;
            var dy2 = (startY - endY) * OneHalf;

            // Get the ellipse rotation transform.
            var cosT = Cos(angle);
            var sinT = Sin(angle);

            // Step 1 : Compute (x1, y1).
            var x1 = (cosT * dx2 + sinT * dy2);
            var y1 = (-sinT * dx2 + cosT * dy2);

            // Ensure radii are positive.
            rx = Abs(rx);
            ry = Abs(ry);
            var Prx = rx * rx;
            var Pry = ry * ry;
            var Px1 = x1 * x1;
            var Py1 = y1 * y1;

            // Check that radii are large enough.
            var radiiCheck = Px1 / Prx + Py1 / Pry;
            if (radiiCheck > 1)
            {
                rx = Sqrt(radiiCheck) * rx;
                ry = Sqrt(radiiCheck) * ry;
                Prx = rx * rx;
                Pry = ry * ry;
            }

            // Step 2 : Compute (cx1, cy1).
            var sign = (largeArcFlag == sweepFlag) ? -1d : 1d;
            var sq = ((Prx * Pry) - (Prx * Py1) - (Pry * Px1)) / ((Prx * Py1) + (Pry * Px1));
            sq = (sq < 0) ? 0 : sq;
            var coef = (sign * Sqrt(sq));
            var cx1 = coef * ((rx * y1) / ry);
            var cy1 = coef * -((ry * x1) / rx);

            // Step 3 : Compute (cx, cy) from (cx1, cy1).
            var sx2 = (startX + endX) * OneHalf;
            var sy2 = (startY + endY) * OneHalf;
            var cx = sx2 + (cosT * cx1 - sinT * cy1);
            var cy = sy2 + (sinT * cx1 + cosT * cy1);

            // Step 4 : Compute the angleStart (angle1) and the angleExtent (dangle).
            var ux = (x1 - cx1) / rx;
            var uy = (y1 - cy1) / ry;
            var vx = (-x1 - cx1) / rx;
            var vy = (-y1 - cy1) / ry;

            // Compute the angle start.
            var n = Sqrt((ux * ux) + (uy * uy));
            var p = ux; // (1 * ux) + (0 * uy)
            sign = (uy < 0) ? -1d : 1d;
            var angleStart = sign * Acos(p / n);

            // Compute the angle extent.
            n = Sqrt((ux * ux + uy * uy) * (vx * vx + vy * vy));
            p = ux * vx + uy * vy;
            sign = (ux * vy - uy * vx < 0) ? -1d : 1d;
            var angleExtent = sign * Acos(p / n);

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

            // We can now build the resulting Arc2D in double precision.
            this.x = cx;
            this.y = cy;
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
        /// Gets the point on the Elliptical arc circumference coincident to the starting angle.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the Elliptical arc circumference coincident to the starting angle.")]
        public Point2D StartPoint
            => (Point2D)CachingProperty(() => (Point2D)Interpolaters.EllipticalArc(x, y, rX, rY, angle, startAngle, sweepAngle, 0));

        /// <summary>
        /// Gets the point on the Elliptical arc circumference coincident to the ending angle.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the Elliptical arc circumference coincident to the ending angle.")]
        public Point2D EndPoint
            => (Point2D)CachingProperty(() => (Point2D)Interpolaters.EllipticalArc(x, y, rX, rY, angle, startAngle, sweepAngle, 1));

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
            get { return rY / rX; }
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
            get { return angle.ToDegrees(); }
            set
            {
                angle = value.ToRadians();
                ClearCache();
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
            get { return startAngle.ToDegrees(); }
            set
            {
                startAngle = value.ToRadians();
                ClearCache();
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
        /// Gets the Focus Radius of the elliptical arc.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The focus radius of the elliptical arc.")]
        public double FocusRadius
            => (double)CachingProperty(() => Measurements.EllipseFocusRadius(rX, rY));

        /// <summary>
        /// Gets the <see cref="Eccentricity"/> of the elliptical arc.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Eccentricity) + " of the elliptical arc.")]
        public double Eccentricity
            => (double)CachingProperty(() => Measurements.Eccentricity(rX, rY));

        /// <summary>
        /// Gets the arc length of the elliptical arc.
        /// </summary>
        /// <returns></returns>
        [XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The arc length of the elliptical arc.")]
        public double ArcLength
            => (double)CachingProperty(() => Measurements.Length(this));

        /// <summary>
        /// Gets the <see cref="Perimeter"/> of the elliptical arc.
        /// </summary>
        /// <returns></returns>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Perimeter) + " of the elliptical arc.")]
        public override double Perimeter
            => ArcLength;

        /// <summary>
        /// Gets the <see cref="Area"/> of the elliptical arc.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Area) + " of the elliptical arc.")]
        public override double Area
            => (double)CachingProperty(() => Measurements.EllipticalArcSectorArea(rX, rY, startAngle, sweepAngle));

        /// <summary>
        /// Gets the angles of the extreme points of the rotated ellipse.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The angles of the extreme points of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<double> ExtremeAngles
            => (List<double>)CachingProperty(() => Measurements.EllipseExtremeAngles(rX, rY, angle));

        /// <summary>
        /// Get the points of the Cartesian extremes of a rotated ellipse.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The locations of the extreme points of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<Point2D> ExtremePoints
            => (List<Point2D>)CachingProperty(() => Measurements.EllipseExtremePoints(x, y, rX, rY, angle));

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
            => (Rectangle2D)CachingProperty(() => Measurements.EllipticalArcBounds(x, y, rX, rY, angle, startAngle, sweepAngle));

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
            => (Rectangle2D)CachingProperty(() => Measurements.EllipseBounds(x, y, rX, rY));

        #endregion

        #region Serialization

        /// <summary>
        /// Sends an event indicating that this value went into the data file during serialization.
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing()]
        private void OnSerializing(StreamingContext context)
        {
            Debug.WriteLine($"{nameof(EllipticalArc)} is being serialized.");
        }

        /// <summary>
        /// Sends an event indicating that this value was reset after serialization.
        /// </summary>
        /// <param name="context"></param>
        [OnSerialized()]
        private void OnSerialized(StreamingContext context)
        {
            Debug.WriteLine($"{nameof(EllipticalArc)} has been serialized.");
        }

        /// <summary>
        /// Sends an event indicating that this value was set during deserialization.
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing()]
        private void OnDeserializing(StreamingContext context)
        {
            Debug.WriteLine($"{nameof(EllipticalArc)} is being deserialized.");
        }

        /// <summary>
        /// Sends an event indicating that this value was set after deserialization.
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized()]
        private void OnDeserialized(StreamingContext context)
        {
            Debug.WriteLine($"{nameof(EllipticalArc)} has been deserialized.");
        }

        #endregion

        #region Interpolaters

        /// <summary>
        /// http://www.vbforums.com/showthread.php?686351-RESOLVED-Elliptical-orbit
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Interpolaters.EllipticalArc(x, y, rX, rY, angle, startAngle, sweepAngle, t);

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
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(EllipticalArc)}{{{nameof(Center)}={Center},{nameof(RX)}={rX},{nameof(RY)}={rY},{nameof(Angle)}={angle},{nameof(StartAngle)}={startAngle},{SweepAngle}={sweepAngle}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
