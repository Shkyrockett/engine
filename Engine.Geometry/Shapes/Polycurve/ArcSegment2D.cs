﻿// <copyright file="ArcSegment2D.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Operations;
using static System.Math;

namespace Engine;

/// <summary>
/// The arc segment class.
/// </summary>
[DataContract, Serializable]
[DebuggerDisplay("{ToString()}")]
public class ArcSegment2D
    : CurveSegment2D
{
    #region Fields
    /// <summary>
    /// The r x.
    /// </summary>
    private double rX;

    /// <summary>
    /// The r y.
    /// </summary>
    private double rY;

    /// <summary>
    /// The angle.
    /// </summary>
    private double angle;

    /// <summary>
    /// The large arc.
    /// </summary>
    private bool largeArc;

    /// <summary>
    /// The sweep.
    /// </summary>
    private bool sweep;

    /// <summary>
    /// The end.
    /// </summary>
    private Point2D end;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="ArcSegment2D"/> class.
    /// </summary>
    public ArcSegment2D()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArcSegment2D"/> class.
    /// </summary>
    /// <param name="previous">The previous.</param>
    /// <param name="centerX">The centerX.</param>
    /// <param name="centerY">The centerY.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="sweepAngle">The sweepAngle.</param>
    public ArcSegment2D(CurveSegment2D previous, double centerX, double centerY, double radius, double sweepAngle)
        : this(previous, radius, radius, sweepAngle, false, sweepAngle <= 180, PolarToCartesian(centerX, centerY, radius, Atan2((previous?.Tail).Y, previous.Tail.X) + sweepAngle))
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArcSegment2D"/> class.
    /// </summary>
    /// <param name="previous">The item.</param>
    /// <param name="relative">The relative.</param>
    /// <param name="args">The arguments.</param>
    public ArcSegment2D(CurveSegment2D previous, bool relative, double[] args)
        : this(previous, args is null ? double.NaN : args[0], args is null ? double.NaN : args[1], args is null ? double.NaN : args[2], (args is not null) && args[3] != 0, (args is not null) && args[4] != 0, args?.Length == 7 ? new Point2D(args[5], args[6]) : null)
    {
        ArgumentNullException.ThrowIfNull(args);

        if (relative)
        {
            Tail = (Point2D)(Tail + previous?.Tail);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArcSegment2D"/> class.
    /// </summary>
    /// <param name="previous">The previous.</param>
    /// <param name="rx">The rx.</param>
    /// <param name="ry">The ry.</param>
    /// <param name="angle">The angle.</param>
    /// <param name="largeArc">The largeArc.</param>
    /// <param name="sweep">The sweep.</param>
    /// <param name="end">The end.</param>
    public ArcSegment2D(CurveSegment2D previous, double rx, double ry, double angle, bool largeArc, bool sweep, Point2D? end)
    {
        ArgumentNullException.ThrowIfNull(previous);
        // SVG uses: rx, ry, x-axis-rotation, large-arc-flag, sweep-flag, xf, yf
        Previous = previous;
        previous.Next = this;
        RX = rx;
        RY = ry;
        Angle = angle;
        LargeArc = largeArc;
        Sweep = sweep;
        Tail = end;
    }
    #endregion Constructors

    #region Deconstructors
    /// <summary>
    /// Deconstruct this <see cref="EllipticalArc2D"/> to a Tuple.
    /// </summary>
    /// <param name="rX">The rX.</param>
    /// <param name="rY">The rY.</param>
    /// <param name="angle">The angle.</param>
    /// <param name="largeArc">The largeArc.</param>
    /// <param name="sweep">The sweep.</param>
    /// <param name="endX">The endX.</param>
    /// <param name="endY">The endY.</param>
    public void Deconstruct(out double rX, out double rY, out double angle, out bool largeArc, out bool sweep, out double endX, out double endY)
    {
        rX = this.rX;
        rY = this.rY;
        endX = end.X;
        endY = end.Y;
        angle = this.angle;
        largeArc = this.largeArc;
        sweep = this.sweep;
    }
    #endregion Deconstructors

    #region Properties
    /// <summary>
    /// Gets or sets the start.
    /// </summary>
    [DataMember, XmlElement, SoapElement]
    [Browsable(true)]
    [Category("Properties")]
    [Description("The point on the Elliptical arc circumference coincident to the starting angle.")]
    public override Point2D Head
    {
        get { return Previous.Tail; }
        set
        {
            if (Previous is null)
            {
                Previous = new PointSegment2D(value);
            }
            else
            {
                Previous.Tail = value;
            }

            ClearCache();
        }
    }

    /// <summary>
    /// Gets the center.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Elements")]
    [Description("The " + nameof(Center) + " location of the " + nameof(EllipticalArc2D) + ".")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Point2DConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public Point2D Center
    {
        get
        {
            return (Point2D)CachingProperty(() => center(Head, Tail, Cos(Angle), Sin(Angle)));

            Point2D center(Point2D start, Point2D end, double cosT, double sinT)
            {
                // Step 1 : Compute (x1, y1).
                var x1 = (cosT * (start.X - end.X) * Floats<double>.OneHalf) + (sinT * (start.Y - end.Y) * Floats<double>.OneHalf);
                var y1 = (-sinT * (start.X - end.X) * Floats<double>.OneHalf) + (cosT * (start.Y - end.Y) * Floats<double>.OneHalf);

                // Ensure radii are positive.
                RX = Abs(RX);
                RY = Abs(RY);

                // Check that radii are large enough.
                var radiiCheck = (x1 * x1 / (RX * RX)) + (y1 * y1 / (RY * RY));
                if (radiiCheck > 1)
                {
                    RX = Sqrt(radiiCheck) * RX;
                    RY = Sqrt(radiiCheck) * RY;
                }

                // Step 2 : Compute (cx1, cy1).
                var sq = ((RX * RX * RY * RY) - (RX * RX * y1 * y1) - (RY * RY * x1 * x1)) / ((RX * RX * y1 * y1) + (RY * RY * x1 * x1));
                sq = (sq < 0) ? 0 : sq;
                var coef = ((LargeArc == Sweep) ? -1d : 1d) * Sqrt(sq);

                // Step 3 : Compute (cx, cy) from (cx1, cy1).
                return new Point2D(
                    ((start.X + end.X) * Floats<double>.OneHalf) + ((cosT * coef * (RX * y1 / RY)) - (sinT * coef * -(RY * x1 / RX))),
                    ((start.Y + end.Y) * Floats<double>.OneHalf) + ((sinT * coef * (RX * y1 / RY)) + (cosT * coef * -(RY * x1 / RX))));
            }
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
        get { return angle.RadiansToDegrees(); }
        set
        {
            angle = value.DegreesToRadians();
            ClearCache();
        }
    }

    /// <summary>
    /// Gets the Cosine of the angle of rotation.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public double CosAngle => (double)CachingProperty(() => Cos(angle));

    /// <summary>
    /// Gets the Sine of the angle of rotation.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public double SinAngle => (double)CachingProperty(() => Sin(angle));

    /// <summary>
    /// Gets the start angle.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [GeometryAngleRadians]
    [Category("Clipping")]
    [Description("The start angle of the elliptical arc.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [RefreshProperties(RefreshProperties.All)]
    public double StartAngle => (double)CachingProperty(() => ToEllipticalArc().StartAngle);

    /// <summary>
    /// Gets the Polar corrected start angle of the <see cref="Ellipse2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [GeometryAngleRadians]
    public double PolarStartAngle => (double)CachingProperty(() => EllipticalPolarAngle(StartAngle, rX, rY));

    /// <summary>
    /// Gets the sweep angle.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [GeometryAngleRadians]
    [Category("Clipping")]
    [Description("The sweep angle of the elliptical arc.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [RefreshProperties(RefreshProperties.All)]
    public double SweepAngle => (double)CachingProperty(() => ToEllipticalArc().SweepAngle);

    /// <summary>
    /// Gets the end angle.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [GeometryAngleRadians]
    [Category("Clipping")]
    [Description("The end angle of the elliptical arc.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double EndAngle => (double)CachingProperty(() => StartAngle + SweepAngle);

    /// <summary>
    /// Gets the Polar corrected end angle of the <see cref="Ellipse2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [GeometryAngleRadians]
    public double PolarEndAngle => (double)CachingProperty(() => EllipticalPolarAngle(StartAngle + SweepAngle, rX, rY));

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// </summary>
    [DataMember, XmlAttribute, SoapAttribute]
    public bool LargeArc
    {
        get { return largeArc; }
        set
        {
            largeArc = value;
            ClearCache();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// </summary>
    [DataMember, XmlAttribute, SoapAttribute]
    public bool Sweep
    {
        get { return sweep; }
        set
        {
            sweep = value;
            ClearCache();
        }
    }

    /// <summary>
    /// Gets or sets the next to end.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public override Point2D? NextToEnd
    {
        get { return Head; }
        set
        {
            Head = value;
            ClearCache();
        }
    }

    /// <summary>
    /// Gets or sets the end.
    /// </summary>
    [DataMember, XmlElement, SoapElement]
    public override Point2D? Tail
    {
        get { return end; }
        set
        {
            end = value;
            ClearCache();
        }
    }

    /// <summary>
    /// Gets the grips.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [TypeConverter(typeof(ExpandableCollectionConverter))]
    public override List<Point2D> Grips => [Head, Tail];

    /// <summary>
    /// Gets the bounds.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [TypeConverter(typeof(Rectangle2DConverter))]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override Rectangle2D Bounds => (Rectangle2D)CachingProperty(() => ToEllipticalArc().Bounds);

    /// <summary>
    /// Gets the length.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public override double Length => (double)CachingProperty(() => ToEllipticalArc().Perimeter);
    #endregion Properties

    /// <summary>
    /// The interpolate.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    public override Point2D Interpolate(double t) => ToEllipticalArc().Interpolate(t);

    #region Methods
    /// <summary>
    /// The contains.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>The <see cref="Inclusions"/>.</returns>
    public Inclusions Contains(Point2D point) => Intersections.Contains(ToEllipticalArc(), point);

    /// <summary>
    /// The to elliptical arc.
    /// </summary>
    /// <returns>The <see cref="EllipticalArc2D"/>.</returns>
    public EllipticalArc2D ToEllipticalArc() => (EllipticalArc2D)CachingProperty(() => new EllipticalArc2D(Head.X, Head.Y, RX, RY, Angle, LargeArc, Sweep, Tail.X, Tail.Y));
    #endregion Methods
}
