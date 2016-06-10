// <copyright file="EllipseArc.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://www.vbforums.com/showthread.php?686351-RESOLVED-Elliptical-orbit
    /// </remarks>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Ellipse Arc")]
    public class EllipseArc
        : Shape
    {
        /// <summary>
        /// Center Point of Ellipse
        /// </summary>
        /// <remarks></remarks>
        private Point2D center;

        /// <summary>
        /// Major Radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        private double majorRadius;

        /// <summary>
        /// Minor Radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        private double minorRadius;

        /// <summary>
        /// Aspect of Ellipse.
        /// </summary>
        /// <remarks></remarks>
        private double aspect;

        /// <summary>
        /// Angle of Ellipse. 
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
        private double endAngle;

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;

        /// <summary>
        /// 
        /// </summary>
        public EllipseArc()
            : this(new Point2D(), 0, 0, 0, 0, 0)
        {
        }

        /// <summary>
        /// Creates a new Instance of Ellipse
        /// </summary>
        /// <param name="circle">The Ellipse</param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        /// <remarks></remarks>
        public EllipseArc(Circle circle, double startAngle, double endAngle)
            : this(circle.Center, circle.Radius, circle.Radius, 0, startAngle, endAngle)
        {
        }

        /// <summary>
        /// Creates a new Instance of Ellipse
        /// </summary>
        /// <param name="rectangle">The boundaries of the ellipse</param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        /// <remarks></remarks>
        public EllipseArc(Rectangle2D rectangle, double angle, double startAngle, double endAngle)
            : this(rectangle.Center(), rectangle.Width, rectangle.Height, angle, startAngle, endAngle)
        {
        }

        /// <summary>
        /// Creates a new Instance of Ellipse
        /// </summary>
        /// <param name="ellipse">The Ellipse</param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        /// <remarks></remarks>
        public EllipseArc(Ellipse ellipse, double startAngle, double endAngle)
            : this(ellipse.Center, ellipse.MajorRadius, ellipse.MinorRadius, ellipse.Angle, startAngle, endAngle)
        {
        }

        /// <summary>
        /// Creates a new Instance of Ellipse
        /// </summary>
        /// <param name="center">Center Point of Ellipse</param>
        /// <param name="majorRadius">Major radius of Ellipse</param>
        /// <param name="minorRadius">Minor radius of Ellipse.</param>
        /// <param name="angle">Angle of Ellipse.</param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        /// <remarks></remarks>
        public EllipseArc(Point2D center, double majorRadius, double minorRadius, double angle, double startAngle, double endAngle)
        {
            this.center = center;
            this.majorRadius = majorRadius;
            this.minorRadius = minorRadius;
            aspect = minorRadius / majorRadius;
            this.angle = angle;
            this.startAngle = startAngle;
            this.endAngle = endAngle;
            points = InterpolatePoints();
        }

        /// <summary>
        /// Creates a new Instance of Ellipse
        /// </summary>
        /// <param name="PointA">First Point on the Ellipse</param>
        /// <param name="PointB">Second Point on the Ellipse</param>
        /// <param name="PointC">Last Point on the Ellipse</param>
        /// <param name="Aspect">Aspect of Ellipse Note: Does not currently work.</param>
        /// <param name="Angle">Angle of Ellipse Note: Does not currently work.</param>
        /// <remarks></remarks>
        public EllipseArc(Point2D PointA, Point2D PointB, Point2D PointC, double Aspect, double Angle)
        {
            //ToDo: calculate the angles of the start and end points from the center to fill them in.
            //  Calculate the slopes of the lines.
            double SlopeA = (PointA.Slope(PointB));
            double SlopeB = (PointC.Slope(PointB));
            double FY = ((((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X)));
            double FX = ((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X)));
            double NewY = ((FX - FY) / (SlopeB - SlopeA));
            double NewX = (FX - (SlopeB * NewY));
            center = new Point2D(NewX, NewY);
            //  Find the Radius
            majorRadius = (center.Length(PointA));
            aspect = Aspect;
            angle = Angle;
            points = InterpolatePoints();
        }

        /// <summary>
        /// Center Point of Ellipse
        /// </summary>
        /// <remarks></remarks>
        [Category("Elements")]
        [Description("The center location of the ellipse.")]
        [XmlAttribute]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Center
        {
            get { return center; }
            set
            {
                center = value;
                points = InterpolatePoints();
            }
        }

        /// <summary>
        /// Major radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        [Category("Elements")]
        [Description("The larger radius of the ellipse.")]
        [XmlAttribute]
        public double MajorRadius
        {
            get { return majorRadius; }
            set
            {
                majorRadius = value;
                points = InterpolatePoints();
            }
        }

        /// <summary>
        /// Minor radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        [Category("Elements")]
        [Description("The smaller radius of the ellipse.")]
        [XmlAttribute]
        [RefreshProperties(RefreshProperties.All)]
        public double MinorRadius
        {
            get { return minorRadius; }
            set
            {
                minorRadius = value;
                aspect = minorRadius / majorRadius;
                points = InterpolatePoints();
            }
        }

        /// <summary>
        /// Aspect of Ellipse. 
        /// </summary>
        /// <remarks></remarks>
        [Category("Properties")]
        [Description("The aspect ratio of the major and minor axis.")]
        [RefreshProperties(RefreshProperties.All)]
        public double Aspect
        {
            get { return aspect; }
            set
            {
                aspect = value;
                minorRadius = majorRadius * aspect;
                points = InterpolatePoints();
            }
        }

        /// <summary>
        /// Angle of lEllipse.
        /// </summary>
        /// <remarks></remarks>
        /// <history>
        ///     Shkyrockett[Alma Jenks]    16/May/2005    Created
        /// </history>
        [Category("Elements")]
        [Description("The angle to rotate the ellipse.")]
        [XmlAttribute]
        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                points = InterpolatePoints();
            }
        }

        /// <summary>
        /// Gets or sets the start angle of the ellipse.
        /// </summary>
        [Category("Elements")]
        [Description("The start angle of the ellipse.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngle
        {
            get { return startAngle; }
            set
            {
                startAngle = value;
                points = InterpolatePoints();
            }
        }

        /// <summary>
        /// Gets or sets the end angle of the ellipse.
        /// </summary>
        [Category("Elements")]
        [Description("The start angle of the ellipse.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngle
        {
            get { return endAngle; }
            set
            {
                endAngle = value;
                points = InterpolatePoints();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The rectangular bounds of the ellipse.")]
        public new Rectangle2D Bounds
        {
            get
            {
                double phi = Maths.ToRadians(angle);
                double ux = majorRadius * Cos(phi);
                double uy = majorRadius * Sin(phi);
                double vx = (majorRadius * aspect) * Cos(phi + PI / 2);
                double vy = (majorRadius * aspect) * Sin(phi + PI / 2);

                double bbox_halfwidth = Sqrt(ux * ux + vx * vx);
                double bbox_halfheight = Sqrt(uy * uy + vy * vy);

                return Rectangle2D.FromLTRB(
                    (center.X - bbox_halfwidth),
                    (center.Y - bbox_halfheight),
                    (center.X + bbox_halfwidth),
                    (center.Y + bbox_halfheight)
                    );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Category("Properties")]
        [Description("The distance around the ellipse.")]
        public double ArcPerimeter
        {
            get
            {
                double minor = (majorRadius * aspect);
                return ((Sqrt(0.5 * ((minor * minor) + (majorRadius * majorRadius)))) * (PI * 2));

                // http://ellipse-circumference.blogspot.com/
                // X1=eval(form.A.value)
                // X2=eval(form.B.value)
                //MIN=min(X1,X2);
                //MAX=max(X1,X2);
                //RA=MAX/MIN;
                //RA=RA.toPrecision(6);
                //RB=MIN/MAX;
                //RB=RB.toPrecision(6);
                //HT1 = X2-X1;
                //HB1 = X2+X1;
                //H1 = (pow(HT1,2))/(pow(HB1,2));
                //H2 = 4-3*H1;
                //D1 = ((11*PI/(44-14*PI))+24100)-24100*H1;
                //C1 = PI*HB1*(1+(3*H1)/(10+pow(H2,0.5))+(1.5*pow(H1,6)-.5*pow(H1,12))/D1);
                //P = 6;
                //C1 = C1.toPrecision(P);
                //form.C.value = C1;
                //form.RX.value = RA;
                //form.RN.value = RB

            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The area of the ellipse.")]
        public override double Area => PI * minorRadius * majorRadius;

        /// <summary>
        /// 
        /// </summary>
        [Category("Functional")]
        [Description("The array of grab handles for this shape.")]
        public List<Point2D> Handles
        {
            get
            {
                return new List<Point2D>
                {
                    center,
                    Interpolate(0),
                    Interpolate(-PI * 0.5)
                };
            }
            set
            {
                if (value != null && value.Count >= 1)
                {
                    Center = value[0];
                    MajorRadius = new LineSegment(center, value[1]).Length();
                    Angle = new LineSegment(center, value[1]).Angle();
                    Aspect = (new LineSegment(center, value[2]).Length()) / majorRadius;
                }
            }
        }

        /// <summary>
        /// http://www.vbforums.com/showthread.php?686351-RESOLVED-Elliptical-orbit
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
        {
            double phi = 2 * PI / t;

            var unroatatedBounds = new Rectangle2D(
                center.X - majorRadius,
                center.Y - majorRadius,
                majorRadius,
                majorRadius * aspect
                );

            double theta = Maths.ToRadians(angle);
            var xaxis = new Point2D(Cos(theta), Sin(theta));
            var yaxis = new Point2D(-Sin(theta), Cos(theta));

            // Ellipse equation for an ellipse at origin.
            var ellipsePoint = new Point2D(
                unroatatedBounds.Width * Cos(phi),
                unroatatedBounds.Height * Sin(phi)
                );

            // Apply the rotation transformation and translate to new center.
            return new Point2D(
                Center.X + (ellipsePoint.X * xaxis.X + ellipsePoint.Y * xaxis.Y),
                Center.Y + (ellipsePoint.X * yaxis.X + ellipsePoint.Y * yaxis.Y)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Point2D> InterpolatePoints()
        {
            double phi = 2 * PI / ArcPerimeter;
            var points = new List<Point2D>();
            for (double i = 0.0f; i <= 2.0 * PI; i += phi)
                points.Add(Interpolate(i));

            return points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(EllipseArc);
            return string.Format("{0}{{{1}={2},{3}={4},{5}={6},{7}={8},{9}={10},{11}={12}}}", nameof(EllipseArc), nameof(Center), center, nameof(MinorRadius), minorRadius, nameof(MajorRadius), majorRadius, nameof(Angle), angle, nameof(StartAngle), startAngle, nameof(EndAngle), endAngle);
        }
    }
}
