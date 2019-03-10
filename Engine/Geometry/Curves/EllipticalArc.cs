// <copyright file="EllipticArc.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
using static Engine.Maths;

namespace Engine
{
    /// <remarks>
    /// http://www.vbforums.com/showthread.php?686351-RESOLVED-Elliptical-orbit
    /// </remarks>
    /// <summary>
    /// The elliptical arc class.
    /// </summary>
    [DataContract, Serializable]
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
        private double cx;

        /// <summary>
        /// The center y-coordinate point of the <see cref="EllipticalArc"/>.
        /// </summary>
        private double cy;

        /// <summary>
        /// Major Radius of <see cref="Ellipse"/>.
        /// </summary>
        private double rX;

        /// <summary>
        /// Minor Radius of <see cref="Ellipse"/>.
        /// </summary>
        private double rY;

        /// <summary>
        /// Angle of <see cref="Ellipse"/>.
        /// </summary>
        private double angle;

        /// <summary>
        /// The start angle.
        /// </summary>
        private double startAngle;

        /// <summary>
        /// The sweep angle.
        /// </summary>
        private double sweepAngle;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc"/> class.
        /// </summary>
        public EllipticalArc()
            : this(0, 0, 0, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc"/> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="EllipticalArc"/>.</param>
        /// <param name="rX">Major radius of <see cref="EllipticalArc"/>.</param>
        /// <param name="rY">Minor radius of <see cref="EllipticalArc"/>.</param>
        /// <param name="angle">Angle of <see cref="EllipticalArc"/>.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public EllipticalArc(Point2D center, double rX, double rY, double angle, double startAngle, double sweepAngle)
            : this(center.X, center.Y, rX, rY, angle, startAngle, sweepAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc"/> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="EllipticalArc"/>.</param>
        /// <param name="size">Major and Minor radii of <see cref="EllipticalArc"/>.</param>
        /// <param name="angle">Angle of <see cref="EllipticalArc"/>.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
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
        public EllipticalArc(Rectangle2D rectangle, double angle, double startAngle, double endAngle)
            : this(rectangle.Center(), rectangle.Width, rectangle.Height, angle, startAngle, endAngle)
        { }

        /// <summary>
        /// Creates a new Instance of Ellipse
        /// </summary>
        /// <param name="ellipse">The Ellipse</param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        public EllipticalArc(Ellipse ellipse, double startAngle, double endAngle)
            : this(ellipse.Center, ellipse.MajorRadius, ellipse.MinorRadius, ellipse.Angle, startAngle, endAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        public EllipticalArc((double X, double Y, double RX, double RY, double Angle, double StartAngle, double SweepAngle) tuple)
            : this(tuple.X, tuple.Y, tuple.RX, tuple.RY, tuple.Angle, tuple.StartAngle, tuple.SweepAngle)
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
        public EllipticalArc(double x, double y, double rX, double rY, double angle, double startAngle, double sweepAngle)
        {
            cx = x;
            cy = y;
            this.rX = rX;
            this.rY = rY;
            this.angle = angle;
            this.startAngle = startAngle;
            this.sweepAngle = sweepAngle;
        }

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
            : this(startX, startY, rx, ry, Cos(angle), Sin(angle), largeArcFlag, sweepFlag, endX, endY)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc"/> class.
        /// </summary>
        /// <param name="startX">The starting X-coordinate.</param>
        /// <param name="startY">The starting Y-coordinate.</param>
        /// <param name="rx">The first radius.</param>
        /// <param name="ry">The second Radius.</param>
        /// <param name="cosAngle">Horizontal rotation transform of the Ellipse.</param>
        /// <param name="sinAngle">Vertical rotation transform of the Ellipse.</param>
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
            double cosAngle, double sinAngle,
            bool largeArcFlag,
            bool sweepFlag,
            double endX, double endY)
        {
            // Find the angle of the sine and cosine values.
            angle = Atan2(sinAngle, CosAngle);

            // Compute the half distance between the start and end points.
            var dx2 = (startX - endX) * OneHalf;
            var dy2 = (startY - endY) * OneHalf;

            // Step 1 : Compute (x1, y1).
            var x1 = (cosAngle * dx2) + (sinAngle * dy2);
            var y1 = (-sinAngle * dx2) + (cosAngle * dy2);

            // Ensure radii are positive.
            rX = Abs(rx);
            rY = Abs(ry);
            var Prx = rX * rX;
            var Pry = rY * rY;
            var Px1 = x1 * x1;
            var Py1 = y1 * y1;

            // Check that radii are large enough.
            var radiiCheck = (Px1 / Prx) + (Py1 / Pry);
            if (radiiCheck > 1)
            {
                rX = Sqrt(radiiCheck) * rX;
                rY = Sqrt(radiiCheck) * rY;
                Prx = rX * rX;
                Pry = rY * rY;
            }

            // Step 2 : Compute (cx1, cy1).
            var sign = (largeArcFlag == sweepFlag) ? -1d : 1d;
            var sq = ((Prx * Pry) - (Prx * Py1) - (Pry * Px1)) / ((Prx * Py1) + (Pry * Px1));
            sq = (sq < 0) ? 0 : sq;
            var coef = sign * Sqrt(sq);
            var cx1 = coef * (rX * y1 / rY);
            var cy1 = coef * -(rY * x1 / rX);

            // Find the center point of the Ellipse.
            var sx2 = (startX + endX) * OneHalf;
            var sy2 = (startY + endY) * OneHalf;
            cx = sx2 + ((cosAngle * cx1) - (sinAngle * cy1));
            cy = sy2 + ((sinAngle * cx1) + (cosAngle * cy1));

            // Compute the start angle and sweep angle vectors.
            var ux = (x1 - cx1) / rX;
            var uy = (y1 - cy1) / rY;
            var vx = (-x1 - cx1) / rX;
            var vy = (-y1 - cy1) / rY;

            // Compute the start angle.
            var n = Sqrt((ux * ux) + (uy * uy));
            var p = ux; // (1 * ux) + (0 * uy)
            sign = (uy < 0) ? -1d : 1d;
            startAngle = sign * Acos(p / n);

            // Compute the sweep angle.
            n = Sqrt(((ux * ux) + (uy * uy)) * ((vx * vx) + (vy * vy)));
            p = (ux * vx) + (uy * vy);
            sign = ((ux * vy) - (uy * vx) < 0) ? -1d : 1d;
            sweepAngle = sign * Acos(p / n);

            if (!sweepFlag && sweepAngle > 0)
            {
                sweepAngle -= Tau;
            }
            else if (sweepFlag && sweepAngle < 0)
            {
                sweepAngle += Tau;
            }

            sweepAngle %= Tau;
            startAngle %= Tau;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="EllipticalArc"/> to a Tuple.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public void Deconstruct(out double cx, out double cy, out double rX, out double rY, out double angle, out double startAngle, out double sweepAngle)
        {
            cx = this.cx;
            cy = this.cy;
            rX = this.rX;
            rY = this.rY;
            angle = this.angle;
            startAngle = this.startAngle;
            sweepAngle = this.sweepAngle;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the location of the center point of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The location of the center point of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Location
        {
            get { return new Point2D(cx, cy); }
            set
            {
                cx = value.X;
                cy = value.Y;
                ClearCache();
                OnPropertyChanged(nameof(Location));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Center Point of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Center) + " location of the " + nameof(EllipticalArc) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Center
        {
            get { return new Point2D(cx, cy); }
            set
            {
                cx = value.X;
                cy = value.Y;
                ClearCache();
                OnPropertyChanged(nameof(Center));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the point on the Elliptical arc circumference coincident to the starting angle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the Elliptical arc circumference coincident to the starting angle.")]
        public Point2D StartPoint
            //=> (Point2D)CachingProperty(() => (Point2D)Interpolators.EllipticalArc(x, y, rX, rY, angle, startAngle, sweepAngle, 0));
            => (Point2D)CachingProperty(() => (Point2D)Interpolators.Ellipse(cx, cy, rX, rY, CosAngle, SinAngle, PolarStartAngle));

        /// <summary>
        /// Gets the point on the Elliptical arc circumference coincident to the ending angle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the Elliptical arc circumference coincident to the ending angle.")]
        public Point2D EndPoint
            //=> (Point2D)CachingProperty(() => (Point2D)Interpolators.EllipticalArc(x, y, rX, rY, angle, startAngle, sweepAngle, 1));
            => (Point2D)CachingProperty(() => (Point2D)Interpolators.Ellipse(cx, cy, rX, rY, CosAngle, SinAngle, PolarEndAngle));

        /// <summary>
        /// Gets or sets the X coordinate location of the center of the elliptical arc.
        /// </summary>
        [XmlAttribute("x")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center x coordinate location of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double X
        {
            get { return cx; }
            set
            {
                cx = value;
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
            get { return cy; }
            set
            {
                cy = value;
                ClearCache();
                OnPropertyChanged(nameof(Y));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the first radius of the elliptical arc.
        /// </summary>
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
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The larger radius of the elliptical arc.")]
        public double MajorRadius
            => rX >= rY ? rX : rY;

        /// <summary>
        /// Gets the Minor radius of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The smaller radius of the elliptical arc.")]
        [RefreshProperties(RefreshProperties.All)]
        public double MinorRadius
            => rX <= rY ? rX : rY;

        /// <summary>
        /// Gets or sets the Aspect ratio of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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

        [XmlAttribute(nameof(angle))]
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
        /// Gets the Cosine of the angle of rotation.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Cosine of the angle of rotation.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double CosAngle
            => (double)CachingProperty(() => Cos(angle));

        /// <summary>
        /// Gets the Sine of the angle of rotation.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Sine of the angle of rotation.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double SinAngle
            => (double)CachingProperty(() => Sin(angle));

        /// <summary>
        /// Gets or sets the start angle of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        /// Gets the Polar corrected start angle of the <see cref="Ellipse"/>.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        public double PolarStartAngle
            => (double)CachingProperty(() => EllipticalPolarAngle(startAngle, rX, rY));

        /// <summary>
        /// Gets or sets the Cosine of the start angle of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Cosine of the start angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngleCos
            => (double)CachingProperty(() => Cos(startAngle));

        /// <summary>
        /// Gets or sets the Sine of the start angle of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Sine of the start angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngleSin
            => (double)CachingProperty(() => Sin(startAngle));

        /// <summary>
        /// Gets or sets the sweep angle of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        /// Gets or sets the Cosine of the sweep angle of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Cosine of the sweep angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double SweepAngleCos
            => (double)CachingProperty(() => Cos(sweepAngle));

        /// <summary>
        /// Gets or sets the Sine of the sweep angle of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Sine of the sweep angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double SweepAngleSin
            => (double)CachingProperty(() => Sin(sweepAngle));

        /// <summary>
        /// Gets or sets the end angle of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        /// Gets or sets the Cosine of the end angle of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Cosine of the end angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngleCos
            => (double)CachingProperty(() => Cos(EndAngle));

        /// <summary>
        /// Gets or sets the Sine of the end angle of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Sine of the end angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngleSin
            => (double)CachingProperty(() => Sin(EndAngle));

        /// <summary>
        /// Gets the Polar corrected end angle of the <see cref="Ellipse"/>.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        public double PolarEndAngle
            => (double)CachingProperty(() => EllipticalPolarAngle(startAngle + sweepAngle, rX, rY));

        /// <summary>
        /// Gets the Focus Radius of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The focus radius of the elliptical arc.")]
        public double FocusRadius
            => (double)CachingProperty(() => Measurements.EllipseFocusRadius(rX, rY));

        /// <summary>
        /// Gets the <see cref="Eccentricity"/> of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Eccentricity) + " of the elliptical arc.")]
        public double Eccentricity
            => (double)CachingProperty(() => Measurements.Eccentricity(rX, rY));

        /// <summary>
        /// Gets the arc length of the elliptical arc.
        /// </summary>
        /// <returns></returns>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The arc length of the elliptical arc.")]
        public double ArcLength
            => (double)CachingProperty(() => this.Length());

        /// <summary>
        /// Gets the <see cref="Perimeter"/> of the elliptical arc.
        /// </summary>
        /// <returns></returns>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Perimeter) + " of the elliptical arc.")]
        public override double Perimeter
            => ArcLength;

        /// <summary>
        /// Gets the <see cref="Area"/> of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Area) + " of the elliptical arc.")]
        public override double Area
            => (double)CachingProperty(() => Measurements.EllipticalArcSectorArea(rX, rY, startAngle, sweepAngle));

        /// <summary>
        /// Gets the angles of the extreme points of the rotated ellipse.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The locations of the extreme points of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<Point2D> ExtremePoints
            => (List<Point2D>)CachingProperty(() => Measurements.EllipseExtremePoints(cx, cy, rX, rY, angle));

        /// <summary>
        /// Gets the Bounding box of the elliptical arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The rectangular bounds of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.EllipticalArcBounds(cx, cy, rX, rY, angle, startAngle, sweepAngle));

        /// <summary>
        /// Gets the size and location of the elliptical arc, in double-point pixels, relative to the parent canvas.
        /// </summary>
        /// <returns>A System.Drawing.RectangleF in double-point pixels relative to the parent canvas that represents the size and location of the segment.</returns>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The unrotated rectangular bounds of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public Rectangle2D DrawingBounds
            => (Rectangle2D)CachingProperty(() => Measurements.EllipseBounds(cx, cy, rX, rY));
        #endregion Properties

        #region Operators
        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"></param>
        public static implicit operator EllipticalArc((double X, double Y, double RX, double RY, double Angle, double StartAngle, double SweepAngle) tuple)
            => new EllipticalArc(tuple);
        #endregion Operators

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(EllipticalArc)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(EllipticalArc)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(EllipticalArc)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(EllipticalArc)} has been deserialized.");
        //}

        //#endregion

        #region Interpolators
        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t)
            => Interpolators.EllipticalArc(cx, cy, rX, rY, CosAngle, SinAngle, startAngle, sweepAngle, t);
        #endregion Interpolators

        #region Methods
        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
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
            if (this is null)
            {
                return nameof(Ellipse);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(EllipticalArc)}{{{nameof(Center)}={Center}{sep}{nameof(RX)}={rX}{sep}{nameof(RY)}={rY}{sep}{nameof(Angle)}={angle}{sep}{nameof(StartAngle)}={startAngle}{sep}{SweepAngle}={sweepAngle}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
