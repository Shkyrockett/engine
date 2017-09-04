// <copyright file="ArcSegment.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
    /// <summary>
    /// 
    /// </summary>
    [DataContract, Serializable]
    public class ArcSegment
        : CurveSegment
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private double rX;

        /// <summary>
        /// 
        /// </summary>
        private double rY;

        /// <summary>
        /// 
        /// </summary>
        private double angle;

        /// <summary>
        /// 
        /// </summary>
        private bool largeArc;

        /// <summary>
        /// 
        /// </summary>
        private bool sweep;

        /// <summary>
        /// 
        /// </summary>
        private Point2D end;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ArcSegment()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArcSegment(CurveSegment item, bool relitive, Double[] args)
            : this(item, args[0], args[1], args[2], args[3] != 0, args[4] != 0, args.Length == 7 ? (Point2D?)new Point2D(args[5], args[6]) : null)
        {
            if (relitive)
                End = (Point2D)(End + item.End);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="angle"></param>
        /// <param name="largeArc"></param>
        /// <param name="sweep"></param>
        /// <param name="end"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArcSegment(CurveSegment previous, double rx, double ry, double angle, bool largeArc, bool sweep, Point2D? end)
        {
            Previous = previous;
            previous.Next = this;
            RX = rx;
            RY = ry;
            Angle = angle;
            LargeArc = largeArc;
            Sweep = sweep;
            End = end.Value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the Elliptical arc circumference coincident to the starting angle.")]
        public override Point2D? Start
        {
            get { return Previous.End; }
            set
            {
                Previous.End = value;
                ClearCache();
            }
        }

        /// <summary>
        /// 
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
            get
            {
                return (Point2D)CachingProperty(() => center(Start.Value, End.Value, Cos(Angle), Sin(Angle)));

                Point2D center(Point2D start, Point2D end, double cosT, double sinT)
                {
                    // Step 1 : Compute (x1, y1).
                    var x1 = (cosT * (start.X - end.X) * OneHalf + sinT * (start.Y - end.Y) * OneHalf);
                    var y1 = (-sinT * (start.X - end.X) * OneHalf + cosT * (start.Y - end.Y) * OneHalf);

                    // Ensure radii are positive.
                    RX = Abs(RX);
                    RY = Abs(RY);

                    // Check that radii are large enough.
                    var radiiCheck = (x1 * x1) / (RX * RX) + (y1 * y1) / (RY * RY);
                    if (radiiCheck > 1)
                    {
                        RX = Sqrt(radiiCheck) * RX;
                        RY = Sqrt(radiiCheck) * RY;
                    }

                    // Step 2 : Compute (cx1, cy1).
                    var sq = ((RX * RX * RY * RY) - (RX * RX * y1 * y1) - (RY * RY * x1 * x1)) / ((RX * RX * y1 * y1) + (RY * RY * x1 * x1));
                    sq = (sq < 0) ? 0 : sq;
                    var coef = (((LargeArc == Sweep) ? -1d : 1d) * Sqrt(sq));

                    // Step 3 : Compute (cx, cy) from (cx1, cy1).
                    return new Point2D(
                        (start.X + end.X) * OneHalf + (cosT * coef * ((RX * y1) / RY) - sinT * coef * -((RY * x1) / RX)),
                        (start.Y + end.Y) * OneHalf + (sinT * coef * ((RX * y1) / RY) + cosT * coef * -((RY * x1) / RX)));
                }
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
            }
        }

        /// <summary>
        /// Gets or sets the Angle of the elliptical arc.
        /// </summary>
        /// <remarks></remarks>
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
            }
        }

        /// <summary>
        /// Gets the Cosine of the angle of rotation.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double CosAngle
            => (double)CachingProperty(() => Cos(angle));

        /// <summary>
        /// Gets the Sine of the angle of rotation.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double SinAngle
            => (double)CachingProperty(() => Sin(angle));

        /// <summary>
        /// 
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
            => (double)CachingProperty(() => ToEllipticalArc().StartAngle);

        /// <summary>
        /// Gets the Polar corrected start angle of the <see cref="Ellipse"/>.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        public double PolarStartAngle
            => (double)CachingProperty(() => EllipticalPolarAngle(StartAngle, rX, rY));

        /// <summary>
        /// 
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
            => (double)CachingProperty(() => ToEllipticalArc().SweepAngle);

        /// <summary>
        /// 
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
            => (double)CachingProperty(() => StartAngle + SweepAngle);

        /// <summary>
        /// Gets the Polar corrected end angle of the <see cref="Ellipse"/>.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        public double PolarEndAngle
            => (double)CachingProperty(() => EllipticalPolarAngle(StartAngle + SweepAngle, rX, rY));

        /// <summary>
        /// 
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
        /// 
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
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D? NextToEnd
        {
            get { return Start; }
            set
            {
                Start = value;
                ClearCache();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public override Point2D? End
        {
            get { return end; }
            set
            {
                end = value.Value;
                ClearCache();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public override List<Point2D> Grips
            => new List<Point2D> { Start.Value, End.Value };

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(Rectangle2DConverter))]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => ToEllipticalArc().Bounds);

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override double Length
            => (double)CachingProperty(() => ToEllipticalArc().Perimeter);

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => ToEllipticalArc().Interpolate(t);

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Inclusion Contains(Point2D point)
            => Intersections.Contains(ToEllipticalArc(), point);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EllipticalArc ToEllipticalArc()
            => (EllipticalArc)CachingProperty(() => new EllipticalArc(Start.Value.X, Start.Value.Y, RX, RY, Angle, LargeArc, Sweep, End.Value.X, End.Value.Y));

        #endregion
    }
}
