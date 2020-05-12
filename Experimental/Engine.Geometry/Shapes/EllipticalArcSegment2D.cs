// <copyright file="EllipticalArcSegment2D.cs" company="Shkyrockett" >
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
using static Engine.Mathematics;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The elliptical arc segment struct.
    /// </summary>
    /// <seealso cref="Engine.IShapeSegment" />
    /// <seealso cref="Engine.IPropertyCaching" />
    /// <seealso cref="System.IEquatable{T}" />
    /// <remarks>
    /// <para>http://www.vbforums.com/showthread.php?686351-RESOLVED-Elliptical-orbit</para>
    /// </remarks>
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="System.IEquatable{T}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<EllipticalArcSegment2D>))]
    [XmlType(TypeName = "arc-Elliptical")]
    [DebuggerDisplay("{ToString()}")]
    public struct EllipticalArcSegment2D
        : IShapeSegment, IPropertyCaching, IEquatable<EllipticalArcSegment2D>
    {
        #region Implementations
        /// <summary>
        /// The empty.
        /// </summary>
        public static readonly EllipticalArcSegment2D Empty = new EllipticalArcSegment2D(0, 0, 0, 0, 0, 0, 0);
        #endregion

        #region Fields
        /// <summary>
        /// The center coordinate point of the <see cref="EllipticalArc2D" />.
        /// </summary>
        private Point2D center;

        /// <summary>
        /// Major Radius of <see cref="Ellipse2D" />.
        /// </summary>
        private double radiusA;

        /// <summary>
        /// Minor Radius of <see cref="Ellipse2D" />.
        /// </summary>
        private double radiusB;

        /// <summary>
        /// Angle of <see cref="Ellipse2D" />.
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
        #endregion

        #region Event Delegates
        /// <summary>
        /// The property changed event of the <see cref="PropertyChangedEventHandler" />.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The property changing event of the <see cref="PropertyChangingEventHandler" />.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc2D" /> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="EllipticalArc2D" />.</param>
        /// <param name="rX">Major radius of <see cref="EllipticalArc2D" />.</param>
        /// <param name="rY">Minor radius of <see cref="EllipticalArc2D" />.</param>
        /// <param name="angle">Angle of <see cref="EllipticalArc2D" />.</param>
        /// <param name="startAngle">The start angle of <see cref="EllipticalArc2D" />.</param>
        /// <param name="sweepAngle">The sweep angle of <see cref="EllipticalArc2D" />.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public EllipticalArcSegment2D(Point2D center, double rX, double rY, double angle, double startAngle, double sweepAngle)
            : this()
        {
            (this.center, radiusA, radiusB, this.angle, this.startAngle, this.sweepAngle) = (center, rX, rY, angle, startAngle, sweepAngle);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc2D" /> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="EllipticalArc2D" />.</param>
        /// <param name="size">Major and Minor radii of <see cref="EllipticalArc2D" />.</param>
        /// <param name="angle">Angle of <see cref="EllipticalArc2D" />.</param>
        /// <param name="startAngle">The start angle of <see cref="EllipticalArc2D" />.</param>
        /// <param name="sweepAngle">The sweep angle of <see cref="EllipticalArc2D" />.</param>
        public EllipticalArcSegment2D(Point2D center, Size2D size, double angle, double startAngle, double sweepAngle)
            : this(center.X, center.Y, size.Width, size.Height, angle, startAngle, sweepAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc2D" /> class.
        /// </summary>
        /// <param name="rectangle">The boundaries of the ellipse</param>
        /// <param name="angle">Angle of <see cref="EllipticalArc2D" />.</param>
        /// <param name="startAngle">The start angle of <see cref="EllipticalArc2D" />.</param>
        /// <param name="endAngle">The end angle of <see cref="EllipticalArc2D" />.</param>
        public EllipticalArcSegment2D(Rectangle2D rectangle, double angle, double startAngle, double endAngle)
            : this(rectangle.Center, rectangle.Width, rectangle.Height, angle, startAngle, endAngle)
        { }

        /// <summary>
        /// Creates a new Instance of Ellipse
        /// </summary>
        /// <param name="ellipse">The Ellipse</param>
        /// <param name="startAngle">The start angle of <see cref="EllipticalArc2D" />.</param>
        /// <param name="endAngle">The end angle of <see cref="EllipticalArc2D" />.</param>
        public EllipticalArcSegment2D(Ellipse2D ellipse, double startAngle, double endAngle)
            : this(ellipse.Center, ellipse.MajorRadius, ellipse.MinorRadius, ellipse.Angle, startAngle, endAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc2D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public EllipticalArcSegment2D((double X, double Y, double RX, double RY, double Angle, double StartAngle, double SweepAngle) tuple)
            : this(tuple.X, tuple.Y, tuple.RX, tuple.RY, tuple.Angle, tuple.StartAngle, tuple.SweepAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc2D" /> class.
        /// </summary>
        /// <param name="x">Center Point x coordinate of <see cref="EllipticalArc2D" />.</param>
        /// <param name="y">Center Point x coordinate of <see cref="EllipticalArc2D" />.</param>
        /// <param name="rX">Major radius of <see cref="EllipticalArc2D" />.</param>
        /// <param name="rY">Minor radius of <see cref="EllipticalArc2D" />.</param>
        /// <param name="angle">Angle of <see cref="EllipticalArc2D" />.</param>
        /// <param name="startAngle">The start angle of <see cref="EllipticalArc2D" />.</param>
        /// <param name="sweepAngle">The sweep angle of <see cref="EllipticalArc2D" />.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public EllipticalArcSegment2D(double x, double y, double rX, double rY, double angle, double startAngle, double sweepAngle)
            : this(new Point2D(x, y), rX, rY, angle, startAngle, sweepAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc2D" /> class.
        /// </summary>
        /// <param name="startX">The starting X-coordinate.</param>
        /// <param name="startY">The starting Y-coordinate.</param>
        /// <param name="rx">The first radius.</param>
        /// <param name="ry">The second Radius.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <param name="largeArcFlag">Flag used to toggle whether to choose the largest matching ellipse.</param>
        /// <param name="sweepFlag">Flag used to toggle whether to choose the upper or lower elliptical solution.</param>
        /// <param name="endX">The end x.</param>
        /// <param name="endY">The end y.</param>
        /// <remarks>
        /// <para>Elliptical arc implementation based on the SVG specification notes
        /// http://stackoverflow.com/questions/14399406/implementing-svg-arc-curves-in-python
        /// https://github.com/blackears/svgSalamander
        /// http://java.net/projects/svgsalamander/sources/svn/content/trunk/svg-core/src/main/java/com/kitfox/svg/pathcmd/Arc.java
        /// http://www.w3.org/TR/SVG/paths.html#PathDataEllipticalArcCommands</para>
        /// </remarks>
        /// <returns></returns>
        public EllipticalArcSegment2D(
            double startX, double startY,
            double rx, double ry,
            double angle,
            bool largeArcFlag,
            bool sweepFlag,
            double endX, double endY)
            : this(startX, startY, rx, ry, Cos(angle), Sin(angle), largeArcFlag, sweepFlag, endX, endY)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc2D" /> class.
        /// </summary>
        /// <param name="startX">The starting X-coordinate.</param>
        /// <param name="startY">The starting Y-coordinate.</param>
        /// <param name="rx">The first radius.</param>
        /// <param name="ry">The second Radius.</param>
        /// <param name="cosAngle">Horizontal rotation transform of the Ellipse.</param>
        /// <param name="sinAngle">Vertical rotation transform of the Ellipse.</param>
        /// <param name="largeArcFlag">Flag used to toggle whether to choose the largest matching ellipse.</param>
        /// <param name="sweepFlag">Flag used to toggle whether to choose the upper or lower elliptical solution.</param>
        /// <param name="endX">The end x.</param>
        /// <param name="endY">The end y.</param>
        /// <remarks>
        /// <para>Elliptical arc implementation based on the SVG specification notes
        /// http://stackoverflow.com/questions/14399406/implementing-svg-arc-curves-in-python
        /// https://github.com/blackears/svgSalamander
        /// http://java.net/projects/svgsalamander/sources/svn/content/trunk/svg-core/src/main/java/com/kitfox/svg/pathcmd/Arc.java
        /// http://www.w3.org/TR/SVG/paths.html#PathDataEllipticalArcCommands</para>
        /// </remarks>
        /// <returns></returns>
        public EllipticalArcSegment2D(
            double startX, double startY,
            double rx, double ry,
            double cosAngle, double sinAngle,
            bool largeArcFlag,
            bool sweepFlag,
            double endX, double endY)
            : this()
        {
            // Find the angle of the sine and cosine values.
            angle = Atan2(sinAngle, cosAngle);

            // Compute the half distance between the start and end points.
            var dx2 = (startX - endX) * OneHalf;
            var dy2 = (startY - endY) * OneHalf;

            // Step 1 : Compute (x1, y1).
            var x1 = (cosAngle * dx2) + (sinAngle * dy2);
            var y1 = (-sinAngle * dx2) + (cosAngle * dy2);

            // Ensure radii are positive.
            radiusA = Abs(rx);
            radiusB = Abs(ry);
            var Prx = radiusA * radiusA;
            var Pry = radiusB * radiusB;
            var Px1 = x1 * x1;
            var Py1 = y1 * y1;

            // Check that radii are large enough.
            var radiiCheck = (Px1 / Prx) + (Py1 / Pry);
            if (radiiCheck > 1)
            {
                radiusA = Sqrt(radiiCheck) * radiusA;
                radiusB = Sqrt(radiiCheck) * radiusB;
                Prx = radiusA * radiusA;
                Pry = radiusB * radiusB;
            }

            // Step 2 : Compute (cx1, cy1).
            var sign = (largeArcFlag == sweepFlag) ? -1d : 1d;
            var sq = ((Prx * Pry) - (Prx * Py1) - (Pry * Px1)) / ((Prx * Py1) + (Pry * Px1));
            sq = (sq < 0) ? 0 : sq;
            var coef = sign * Sqrt(sq);
            var cx1 = coef * (radiusA * y1 / radiusB);
            var cy1 = coef * -(radiusB * x1 / radiusA);

            // Find the center point of the Ellipse.
            var sx2 = (startX + endX) * OneHalf;
            var sy2 = (startY + endY) * OneHalf;
            center = (sx2 + ((cosAngle * cx1) - (sinAngle * cy1)), sy2 + ((sinAngle * cx1) + (cosAngle * cy1)));

            // Compute the start angle and sweep angle vectors.
            var ux = (x1 - cx1) / radiusA;
            var uy = (y1 - cy1) / radiusB;
            var vx = (-x1 - cx1) / radiusA;
            var vy = (-y1 - cy1) / radiusB;

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
        #endregion

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="EllipticalArc2D" /> to a Tuple.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="rX">The r x.</param>
        /// <param name="rY">The r y.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void Deconstruct(out (double cx, double cy) center, out double rX, out double rY, out double angle, out double startAngle, out double sweepAngle) => (center, rX, rY, angle, startAngle, sweepAngle) = (this.center, radiusA, radiusB, this.angle, this.startAngle, this.sweepAngle);

        /// <summary>
        /// Deconstruct this <see cref="EllipticalArc2D" /> to a Tuple.
        /// </summary>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rX">The r x.</param>
        /// <param name="rY">The r y.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        public void Deconstruct(out double cx, out double cy, out double rX, out double rY, out double angle, out double startAngle, out double sweepAngle) => (cx, cy, rX, rY, angle, startAngle, sweepAngle) = (this.center.X, this.center.Y, radiusA, radiusB, this.angle, this.startAngle, this.sweepAngle);
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the location of the center point of the elliptical arc.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
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
        /// Gets or sets the center of the <see cref="EllipticalArc2D" />.
        /// </summary>
        /// <value>
        /// The center.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The " + nameof(Center) + " location of the " + nameof(EllipticalArcSegment2D) + ".")]
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
        /// Gets or sets the <see cref="X" /> coordinate location of the center of the <see cref="EllipticalArc2D" />.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [DataMember(Name = nameof(X)), XmlAttribute(nameof(X)), SoapAttribute(nameof(X))]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center x coordinate location of the elliptical arc.")]
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
        /// Gets or sets the <see cref="Y" /> coordinate location of the center of the <see cref="EllipticalArc2D" />.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [DataMember(Name = nameof(Y)), XmlAttribute(nameof(Y)), SoapAttribute(nameof(Y))]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The center y coordinate location of the elliptical arc.")]
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
        /// Gets the point on the <see cref="EllipticalArc2D" /> circumference coincident to the starting angle.
        /// </summary>
        /// <value>
        /// The start point.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the Elliptical arc circumference coincident to the starting angle.")]
        public Point2D StartPoint
        {
            get
            {
                (var polarStartAngle, var cx, var cy, var radiusA, var radiusB, var cos, var sin) = (this.PolarStartAngle, center.X, center.Y, this.radiusA, this.radiusB, CosAngle, SinAngle);
                return (Point2D)(this as IPropertyCaching).CachingProperty(() => (Point2D)Interpolators.Ellipse(polarStartAngle, cx, cy, radiusA, radiusB, cos, sin));
            }
        }

        /// <summary>
        /// Gets the point on the <see cref="EllipticalArc2D" /> circumference coincident to the ending angle.
        /// </summary>
        /// <value>
        /// The end point.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the Elliptical arc circumference coincident to the ending angle.")]
        public Point2D EndPoint
        {
            get
            {
                (var polarEndAngle, var cx, var cy, var radiusA, var radiusB, var cos, var sin) = (this.PolarEndAngle, center.X, center.Y, this.radiusA, this.radiusB, CosAngle, SinAngle);
                return (Point2D)(this as IPropertyCaching).CachingProperty(() => (Point2D)Interpolators.Ellipse(polarEndAngle, cx, cy, radiusA, radiusB, cos, sin));
            }
        }

        /// <summary>
        /// Gets or sets the a radius of the <see cref="EllipticalArc2D" />.
        /// </summary>
        /// <value>
        /// The radius a.
        /// </value>
        [DataMember(Name = nameof(RadiusA)), XmlAttribute(nameof(RadiusA)), SoapAttribute(nameof(RadiusA))]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The first radius of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
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
        /// Gets or sets the second radius of <see cref="EllipticalArc2D" />.
        /// </summary>
        /// <value>
        /// The radius b.
        /// </value>
        [DataMember(Name = nameof(RadiusB)), XmlAttribute(nameof(RadiusB)), SoapAttribute(nameof(RadiusB))]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The second radius of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
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
        /// Gets the Major radius of elliptical arc.
        /// </summary>
        /// <value>
        /// The major radius.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The larger radius of the elliptical arc.")]
        public double MajorRadius => radiusA >= radiusB ? radiusA : radiusB;

        /// <summary>
        /// Gets the Minor radius of the elliptical arc.
        /// </summary>
        /// <value>
        /// The minor radius.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The smaller radius of the elliptical arc.")]
        [RefreshProperties(RefreshProperties.All)]
        public double MinorRadius => radiusA <= radiusB ? radiusA : radiusB;

        /// <summary>
        /// Gets or sets the Aspect ratio of the elliptical arc.
        /// </summary>
        /// <value>
        /// The aspect.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Aspect) + " ratio of the major and minor axis of the elliptical arc.")]
        [RefreshProperties(RefreshProperties.All)]
        public double Aspect
        {
            get { return radiusB / radiusA; }
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
        /// Gets or sets the angle the <see cref="EllipticalArc2D" /> is rotated about the center.
        /// </summary>
        /// <value>
        /// The angle.
        /// </value>
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
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                angle = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Angle of the elliptical arc in Degrees.
        /// </summary>
        /// <value>
        /// The angle degrees.
        /// </value>
        [DataMember(Name = nameof(Angle)), XmlAttribute(nameof(Angle)), SoapAttribute(nameof(Angle))]
        [Browsable(false)]
        [GeometryAngleDegrees]
        [Category("Elements")]
        [Description("The " + nameof(Angle) + " to rotate the elliptical arc in Degrees.")]
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
                angle = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the Cosine of the angle of rotation.
        /// </summary>
        /// <value>
        /// The cosine of the angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Cosine of the angle of rotation.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double CosAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = Angle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Cos(a));
            }
        }

        /// <summary>
        /// Gets the Sine of the angle of rotation.
        /// </summary>
        /// <value>
        /// The sine of the angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Sine of the angle of rotation.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double SinAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = Angle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Sin(a));
            }
        }

        /// <summary>
        /// Gets or sets the start angle of the elliptical arc.
        /// </summary>
        /// <value>
        /// The start angle.
        /// </value>
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
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                startAngle = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the start angle of the elliptical arc in Degrees.
        /// </summary>
        /// <value>
        /// The start angle degrees.
        /// </value>
        [DataMember(Name = nameof(StartAngle)), XmlAttribute(nameof(StartAngle)), SoapAttribute(nameof(StartAngle))]
        [Browsable(false)]
        [GeometryAngleDegrees]
        [Category("Clipping")]
        [Description("The start angle of the elliptical arc in Degrees.")]
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
        /// Gets the Polar corrected start angle of the <see cref="Ellipse2D" />.
        /// </summary>
        /// <value>
        /// The polar start angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        public double PolarStartAngle
        {
            get
            {
                (var startAngle, var radiusA, var radiusB) = (this.startAngle, this.radiusA, this.radiusB);
                return (double)(this as IPropertyCaching).CachingProperty(() => EllipticalPolarAngle(startAngle, radiusA, radiusB));
            }
        }

        /// <summary>
        /// Gets or sets the Cosine of the start angle of the elliptical arc.
        /// </summary>
        /// <value>
        /// The cosine of the angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Cosine of the start angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double CosStartAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = StartAngle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Cos(a));
            }
        }

        /// <summary>
        /// Gets or sets the Sine of the start angle of the elliptical arc.
        /// </summary>
        /// <value>
        /// The sine of the angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Sine of the start angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double SinStartAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = StartAngle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Sin(a));
            }
        }

        /// <summary>
        /// Gets or sets the end angle of the elliptical arc.
        /// </summary>
        /// <value>
        /// The end angle.
        /// </value>
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
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                sweepAngle = value - startAngle;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the end angle of the elliptical arc in Degrees.
        /// </summary>
        /// <value>
        /// The end angle.
        /// </value>
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
        /// Gets or sets the Cosine of the end angle of the elliptical arc.
        /// </summary>
        /// <value>
        /// The cosine of the end angle.
        /// </value>
        [Browsable(true)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Cosine of the end angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public double CosEndAngle
        {
            get
            {
                var a = EndAngle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Cos(a));
            }
        }

        /// <summary>
        /// Gets or sets the Sine of the end angle of the elliptical arc.
        /// </summary>
        /// <value>
        /// The cosine of the end angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Sine of the end angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public double SinEndAngle
        {
            get
            {
                var a = EndAngle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Sin(a));
            }
        }

        /// <summary>
        /// Gets the Polar corrected end angle of the <see cref="Ellipse2D" />.
        /// </summary>
        /// <value>
        /// The polar end angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        public double PolarEndAngle
        {
            get
            {
                (var startAngle, var sweepAngle, var radiusA, var radiusB) = (this.startAngle, this.sweepAngle, this.radiusA, this.radiusB);
                return (double)(this as IPropertyCaching).CachingProperty(() => EllipticalPolarAngle(startAngle + sweepAngle, radiusA, radiusB));
            }
        }

        /// <summary>
        /// Gets or sets the sweep angle of the elliptical arc.
        /// </summary>
        /// <value>
        /// The sweep angle.
        /// </value>
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The sweep angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        /// Gets or sets the sweep angle of the elliptical arc in Degrees.
        /// </summary>
        /// <value>
        /// The sweep angle.
        /// </value>
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
        /// Gets or sets the Cosine of the sweep angle of the elliptical arc.
        /// </summary>
        /// <value>
        /// The cosine of the angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Cosine of the sweep angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double CosSweepAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = SweepAngle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Cos(a));
            }
        }

        /// <summary>
        /// Gets or sets the Sine of the sweep angle of the elliptical arc.
        /// </summary>
        /// <value>
        /// The sine of the angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [GeometryAngleRadians]
        [Category("Clipping")]
        [Description("The Sine of the sweep angle of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double SinSweepAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = SweepAngle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Sin(a));
            }
        }

        /// <summary>
        /// Gets the Focus Radius of the elliptical arc.
        /// </summary>
        /// <value>
        /// The focus radius.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The focus radius of the elliptical arc.")]
        public double FocusRadius
        {
            get
            {
                (var radiusA, var radiusB) = (this.radiusA, this.radiusB);
                return (double)(this as IPropertyCaching).CachingProperty(() => Measurements.EllipseFocusRadius(radiusA, radiusB));
            }
        }

        /// <summary>
        /// Gets the <see cref="Eccentricity" /> of the elliptical arc.
        /// </summary>
        /// <value>
        /// The eccentricity.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Eccentricity) + " of the elliptical arc.")]
        public double Eccentricity
        {
            get
            {
                (var radiusA, var radiusB) = (this.radiusA, this.radiusB);
                return (double)(this as IPropertyCaching).CachingProperty(() => Measurements.Eccentricity(radiusA, radiusB));
            }
        }

        /// <summary>
        /// Gets the arc length of the elliptical arc.
        /// </summary>
        /// <value>
        /// The length of the arc.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The arc length of the elliptical arc.")]
        public double ArcLength
        {
            get
            {
                var length = this.Length();
                return (double)(this as IPropertyCaching).CachingProperty(() => length);
            }
        }

        /// <summary>
        /// Gets the <see cref="Perimeter" /> of the elliptical arc.
        /// </summary>
        /// <value>
        /// The perimeter.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Perimeter) + " of the elliptical arc.")]
        public double Perimeter => ArcLength;

        /// <summary>
        /// Gets the <see cref="Area" /> of the elliptical arc.
        /// </summary>
        /// <value>
        /// The area.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The " + nameof(Area) + " of the elliptical arc.")]
        public double Area
        {
            get
            {
                (var radiusA, var radiusB, var startAngle, var sweepAngle) = (this.radiusA, this.radiusB, this.startAngle, this.sweepAngle);
                return (double)(this as IPropertyCaching).CachingProperty(() => Measurements.EllipticalArcSectorArea(radiusA, radiusB, startAngle, sweepAngle));
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
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The locations of the extreme points of the " + nameof(Ellipse2D) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<Point2D> ExtremePoints
        {
            get
            {
                (var cx, var cy, var radiusA, var radiusB, var angle) = (center.X, center.Y, this.radiusA, this.radiusB, this.angle);
                return (List<Point2D>)(this as IPropertyCaching).CachingProperty(() => Measurements.EllipseExtremePoints(cx, cy, radiusA, radiusB, angle));
            }
        }

        /// <summary>
        /// Gets the Bounding box of the elliptical arc.
        /// </summary>
        /// <value>
        /// The bounds.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The rectangular bounds of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public Rectangle2D Bounds
        {
            get
            {
                (var cx, var cy, var radiusA, var radiusB, var angle, var startAngle, var sweepAngle) = (center.X, center.Y, this.radiusA, this.radiusB, this.angle, this.startAngle, this.sweepAngle);
                return (Rectangle2D)(this as IPropertyCaching).CachingProperty(() => Measurements.EllipticalArcBounds(cx, cy, radiusA, radiusB, angle, startAngle, sweepAngle));
            }
        }

        /// <summary>
        /// Gets the size and location of the elliptical arc, in double-point pixels, relative to the parent canvas.
        /// </summary>
        /// <value>
        /// The orthogonal bounds.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The orthogonal rectangular bounds of the elliptical arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public Rectangle2D OrthogonalBounds
        {
            get
            {
                (var cx, var cy, var radiusA, var radiusB) = (center.X, center.Y, this.radiusA, this.radiusB);
                return (Rectangle2D)(this as IPropertyCaching).CachingProperty(() => Measurements.EllipseBounds(cx, cy, radiusA, radiusB));
            }
        }

        /// <summary>
        /// Gets or sets the property cache.
        /// </summary>
        /// <value>
        /// The property cache.
        /// </value>
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
        public static bool operator ==(EllipticalArcSegment2D left, EllipticalArcSegment2D right) => left.Equals(right);

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
        public static bool operator !=(EllipticalArcSegment2D left, EllipticalArcSegment2D right) => !(left == right);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator EllipticalArcSegment2D((double X, double Y, double RX, double RY, double Angle, double StartAngle, double SweepAngle) tuple) => FromValueTuple(tuple);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is EllipticalArcSegment2D d && Equals(d);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] EllipticalArcSegment2D other)
            => X == other.X &&
                Y == other.Y &&
                RadiusA == other.RadiusA &&
                RadiusB == other.RadiusB &&
                Angle == other.Angle &&
                CosAngle == other.CosAngle &&
                SinAngle == other.SinAngle &&
                StartAngle == other.StartAngle &&
                SweepAngle == other.SweepAngle;

        /// <summary>
        /// Creates a new <see cref="EllipticalArc2D" /> from a <see cref="ValueTuple{T1, T2, T3, T4, T5}" />.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EllipticalArcSegment2D FromValueTuple((double X, double Y, double RX, double RY, double Angle, double StartAngle, double SweepAngle) tuple) => new EllipticalArcSegment2D(tuple);
        #endregion

        #region Interpolators
        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="Point2D" />.
        /// </returns>
        public Point2D Interpolate(double t) => Interpolators.EllipticalArc(t, center.X, center.Y, radiusA, radiusB, CosAngle, SinAngle, startAngle, sweepAngle);
        #endregion Interpolators

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public bool Contains(Point2D point) => Intersections.Contains(this, point) != Inclusions.Outside;

        #region Methods
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
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(X);
            hash.Add(Y);
            hash.Add(RadiusA);
            hash.Add(RadiusB);
            hash.Add(Angle);
            hash.Add(CosAngle);
            hash.Add(SinAngle);
            hash.Add(StartAngle);
            hash.Add(SweepAngle);
            return hash.ToHashCode();
        }

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
        /// Creates a string representation of this elliptical arc struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null)
            {
                return nameof(EllipticalArcSegment2D);
            }

            var sep = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(EllipticalArcSegment2D)}({nameof(X)}: {X.ToString(format, formatProvider)}{sep} {nameof(Y)}: {Y.ToString(format, formatProvider)}{sep} {nameof(RadiusA)}: {RadiusA.ToString(format, formatProvider)}{sep} {nameof(RadiusB)}: {RadiusB.ToString(format, formatProvider)}{sep} {nameof(Angle)}: {Angle.ToString(format, formatProvider)}{sep} {nameof(StartAngle)}: {StartAngle.ToString(format, formatProvider)}{sep} {nameof(SweepAngle)}: {SweepAngle.ToString(format, formatProvider)})";
        }
        #endregion
    }
}