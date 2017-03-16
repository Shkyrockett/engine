// <copyright file="PathArc.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
using System.Xml.Serialization;
using static System.Math;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PathArc
        : PathItem
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private double rx;

        /// <summary>
        /// 
        /// </summary>
        private double ry;

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
        public PathArc()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PathArc(PathItem item, bool relitive, Double[] args)
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
        public PathArc(PathItem previous, double rx, double ry, double angle, bool largeArc, bool sweep, Point2D? end)
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
        [XmlElement]
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
        [XmlIgnore, SoapIgnore]
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
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
        public double RX
        {
            get { return rx; }
            set
            {
                rx = value;
                ClearCache();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
        public double RY
        {
            get { return ry; }
            set
            {
                ry = value;
                ClearCache();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
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
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public double StartAngle
        {
            get
            {
                //return (double)CachingProperty(() => startAngle(Start.Value, End.Value, Cos(Angle), Sin(Angle)));
                return (double)CachingProperty(() => new EllipticalArc(Start.Value.X, Start.Value.Y, RX, RY, Angle, LargeArc, Sweep, End.Value.X, End.Value.Y).StartAngle);

                double startAngle(Point2D start, Point2D end, double cosT, double sinT)
                {
                    // Compute (x1, y1).
                    var x1 = (cosT * (start.X - end.X) * OneHalf + sinT * (start.Y - end.Y) * OneHalf);
                    var y1 = (-sinT * (start.X - end.X) * OneHalf + cosT * (start.Y - end.Y) * OneHalf);

                    // Ensure radii are positive.
                    RX = Abs(RX);
                    RY = Abs(RY);

                    // Check that radii are large enough.
                    var radiiCheck = x1 * x1 / RX * RX + y1 * y1 / RY * RY;
                    if (radiiCheck > 1)
                    {
                        RX = Sqrt(radiiCheck) * RX;
                        RY = Sqrt(radiiCheck) * RY;
                    }

                    // Compute coefficient.
                    var sq = ((RX * RX * RY * RY) - (RX * RX * y1 * y1) - (RY * RY * x1 * x1)) / ((RX * RX * y1 * y1) + (RY * RY * x1 * x1));
                    sq = (sq < 0) ? 0 : sq;
                    var coef = (((LargeArc == Sweep) ? -1d : 1d) * Sqrt(sq));

                    // Compute the start angle vector.
                    var ux = (x1 - coef * ((RX * y1) / RY)) / RX;
                    var uy = (y1 - coef * -((RY * x1) / RX)) / RY;

                    // Compute the start angle.
                    var angleStart = ((uy < 0) ? -1d : 1d) * Acos(ux / Sqrt((ux * ux) + (uy * uy)));
                    angleStart %= Tau;
                    return angleStart;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public double SweepAngle
        {
            get
            {
                //return (double)CachingProperty(() => sweepAngle(Start.Value, End.Value, Cos(Angle), Sin(Angle)));
                return (double)CachingProperty(() => new EllipticalArc(Start.Value.X, Start.Value.Y, RX, RY, Angle, LargeArc, Sweep, End.Value.X, End.Value.Y).SweepAngle);

                double sweepAngle(Point2D start, Point2D end, double cosT, double sinT)
                {
                    // Compute (x1, y1).
                    var x1 = (cosT * (start.X - end.X) * OneHalf + sinT * (start.Y - end.Y) * OneHalf);
                    var y1 = (-sinT * (start.X - end.X) * OneHalf + cosT * (start.Y - end.Y) * OneHalf);

                    // Ensure radii are positive.
                    RX = Abs(RX);
                    RY = Abs(RY);

                    // Check that radii are large enough.
                    var radiiCheck = x1 * x1 / RX * RX + y1 * y1 / RY * RY;
                    if (radiiCheck > 1)
                    {
                        RX = Sqrt(radiiCheck) * RX;
                        RY = Sqrt(radiiCheck) * RY;
                    }

                    // Compute coefficient.
                    var sq = ((RX * RX * RY * RY) - (RX * RX * y1 * y1) - (RY * RY * x1 * x1)) / ((RX * RX * y1 * y1) + (RY * RY * x1 * x1));
                    sq = (sq < 0) ? 0 : sq;
                    var coef = (((LargeArc == Sweep) ? -1d : 1d) * Sqrt(sq));

                    // Compute the Start angle and the sweep angle vectors.
                    var ux = (x1 - coef * ((RX * y1) / RY)) / RX;
                    var uy = (y1 - coef * -((RY * x1) / RX)) / RY;
                    var vx = (-x1 - coef * ((RX * y1) / RY)) / RX;
                    var vy = (-y1 - coef * -((RY * x1) / RX)) / RY;

                    // Compute the sweep angle.
                    var angleSweep = ((ux * vy - uy * vx < 0) ? -1d : 1d) * Acos((ux * vx + uy * vy) / Sqrt((ux * ux + uy * uy) * (vx * vx + vy * vy)));

                    if (!Sweep && angleSweep > 0)
                    {
                        angleSweep -= Tau;
                    }
                    else if (Sweep && angleSweep < 0)
                    {
                        angleSweep += Tau;
                    }
                    angleSweep %= Tau;
                    return angleSweep;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public double EndAngle
            => (double)CachingProperty(() => StartAngle + SweepAngle);

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
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
        [XmlAttribute, SoapAttribute]
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
        [XmlIgnore, SoapIgnore]
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
        [XmlElement]
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
        [XmlIgnore, SoapIgnore]
        public override List<Point2D> Grips
            => new List<Point2D> { Start.Value, End.Value };

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(Rectangle2DConverter))]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => ToEllipticalArc().Bounds);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
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
