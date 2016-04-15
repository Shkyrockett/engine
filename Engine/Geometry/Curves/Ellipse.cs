// <copyright file="Circle.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Ellipse")]
    public class Ellipse
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
        /// Interpolated points.
        /// </summary>
        internal List<Point2D> Points;

        /// <summary>
        /// 
        /// </summary>
        public Ellipse()
            : this(new Point2D(), 0, 0, 0)
        { }

        /// <summary>
        /// Creates a new Instance of Ellipse
        /// </summary>
        /// <param name="PointA">First Point on the Ellipse</param>
        /// <param name="PointB">Second Point on the Ellipse</param>
        /// <param name="PointC">Last Point on the Ellipse</param>
        /// <param name="Aspect">Aspect of Ellipse Note: Does not currently work.</param>
        /// <param name="Angle">Angle of Ellipse Note: Does not currently work.</param>
        /// <remarks></remarks>
        public Ellipse(Point2D PointA, Point2D PointB, Point2D PointC, double Aspect, double Angle)
        {
            //  Calculate the slopes of the lines.
            double SlopeA = (float)(PointA.Slope(PointB));
            double SlopeB = (float)(PointC.Slope(PointB));
            double FY = ((((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X)));
            double FX = ((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X)));
            double NewY = ((FX - FY) / (SlopeB - SlopeA));
            double NewX = (FX - (SlopeB * NewY));
            center = new Point2D(NewX, NewY);
            //  Find the Radius
            majorRadius = (center.Length(PointA));
            aspect = Aspect;
            angle = Angle;
            Points = InterpolatePoints();
        }

        /// <summary>
        /// Creates a new Instance of Ellipse
        /// </summary>
        /// <param name="center">Center Point of Ellipse</param>
        /// <param name="majorRadius">Major radius of Ellipse</param>
        /// <param name="minorRadius">Minor radius of Ellipse.</param>
        /// <param name="Angle">Angle of Ellipse.</param>
        /// <remarks></remarks>
        public Ellipse(Point2D center, double majorRadius, double minorRadius, double Angle)
        {
            this.center = center;
            this.majorRadius = majorRadius;
            this.minorRadius = minorRadius;
            aspect = minorRadius / majorRadius;
            angle = Angle;
            Points = InterpolatePoints();
        }

        /// <summary>
        /// Center Point of Ellipse
        /// </summary>
        /// <remarks></remarks>
        [Category("Elements")]
        [Description("The center location of the ellipse.")]
        [XmlAttribute()]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(PointFConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Center
        {
            get { return center; }
            set
            {
                center = value;
                Points = InterpolatePoints();
            }
        }

        /// <summary>
        /// Major radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        [Category("Elements")]
        [Description("The larger radius of the ellipse.")]
        [XmlAttribute()]
        public double MajorRadius
        {
            get { return majorRadius; }
            set
            {
                majorRadius = value;
                Points = InterpolatePoints();
            }
        }

        /// <summary>
        /// Minor radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        [Category("Elements")]
        [Description("The smaller radius of the ellipse.")]
        [XmlAttribute()]
        [RefreshProperties(RefreshProperties.All)]
        public double MinorRadius
        {
            get { return minorRadius; }
            set
            {
                minorRadius = value;
                aspect = minorRadius / majorRadius;
                Points = InterpolatePoints();
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
                Points = InterpolatePoints();
            }
        }

        /// <summary>
        /// Angle of lEllipse.
        /// </summary>
        /// <remarks></remarks>
        [Category("Elements")]
        [Description("The angle to rotate the ellipse.")]
        [XmlAttribute()]
        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                Points = InterpolatePoints();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The rectangular bounds of the ellipse.")]
        public override Rectangle2D Bounds
        {
            get
            {
                double phi = MathExtensions.ToRadians(angle);
                double ux = majorRadius * Math.Cos(phi);
                double uy = majorRadius * Math.Sin(phi);
                double vx = (majorRadius * aspect) * Math.Cos(phi + Math.PI / 2);
                double vy = (majorRadius * aspect) * Math.Sin(phi + Math.PI / 2);

                double bbox_halfwidth = Math.Sqrt(ux * ux + vx * vx);
                double bbox_halfheight = Math.Sqrt(uy * uy + vy * vy);

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
        public double Perimeter
        {
            get
            {
                double minor = (majorRadius * aspect);
                return ((Math.Sqrt(0.5 * ((minor * minor) + (majorRadius * majorRadius)))) * (Math.PI * 2));

                // http://ellipse-circumference.blogspot.com/
                // X1=eval(form.A.value)
                // X2=eval(form.B.value)
                //MIN=Math.min(X1,X2);
                //MAX=Math.max(X1,X2);
                //RA=MAX/MIN;
                //RA=RA.toPrecision(6);
                //RB=MIN/MAX;
                //RB=RB.toPrecision(6);
                //HT1 = X2-X1;
                //HB1 = X2+X1;
                //H1 = (Math.pow(HT1,2))/(Math.pow(HB1,2));
                //H2 = 4-3*H1;
                //D1 = ((11*Math.PI/(44-14*Math.PI))+24100)-24100*H1;
                //C1 = Math.PI*HB1*(1+(3*H1)/(10+Math.pow(H2,0.5))+(1.5*Math.pow(H1,6)-.5*Math.pow(H1,12))/D1);
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
        public double Area
        {
            get { return Math.PI * minorRadius * majorRadius; }
        }

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
                    Interpolate(-Math.PI * 0.5)
                };
            }
            set
            {
                if (value != null && value.Count >= 1)
                {
                    Center = value[0];
                    MajorRadius = (new LineSegment(center, value[1]).Length());
                    Angle = (new LineSegment(center, value[1]).Angle());
                    Aspect = ((new LineSegment(center, value[2]).Length()) / majorRadius);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// Gets or sets the size and location of the ellipse, in floating-point pixels, relative to the parent canvas.
        /// </summary>
        /// <returns>A System.Drawing.RectangleF in floating-point pixels relative to the parent canvas that represents the size and location of the segment.</returns>
        /// <remarks></remarks>
        public Rectangle2D UnrotatedBounds()
        {
            return new Rectangle2D(
                (center.X - majorRadius),
                (center.Y - majorRadius),
                (majorRadius),
                (majorRadius * aspect)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="theta"></param>
        /// <param name="ellipse"></param>
        /// <param name="phi"></param>
        /// <param name="rect"></param>
        private void draw_rect_at_ellipse(Graphics g, float theta, RectangleF ellipse, float phi, RectangleF rect)
        {
            PointF xaxis = new PointF((float)Math.Cos(theta), (float)Math.Sin(theta));
            PointF yaxis = new PointF(-(float)Math.Sin(theta), (float)Math.Cos(theta));
            PointF ellipse_point;

            // Ellipse equation for an ellipse at origin.
            ellipse_point = new PointF((float)(ellipse.Width * Math.Cos(phi)), (float)(ellipse.Height * Math.Sin(phi)));

            // Apply the rotation transformation and translate to new center.
            rect.Location = new PointF(ellipse.Left + (ellipse_point.X * xaxis.X + ellipse_point.Y * xaxis.Y),
                                       ellipse.Top + (ellipse_point.X * yaxis.X + ellipse_point.Y * yaxis.Y));

            g.DrawRectangle(Pens.AntiqueWhite, rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point2D Interpolate(double index)
        {
            Rectangle2D unroatatedBounds = new Rectangle2D(
                center.X - majorRadius,
                center.Y - majorRadius,
                majorRadius,
                majorRadius * aspect
                );

            double theta = MathExtensions.ToRadians(angle);
            Point2D xaxis = new Point2D(Math.Cos(theta), Math.Sin(theta));
            Point2D yaxis = new Point2D(-Math.Sin(theta), Math.Cos(theta));

            // Ellipse equation for an ellipse at origin.
            Point2D ellipsePoint = new Point2D(
                (unroatatedBounds.Width * Math.Cos(index)),
                (unroatatedBounds.Height * Math.Sin(index))
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
            float delta_phi = (float)(2 * Math.PI / Perimeter);
            List<Point2D> points = new List<Point2D>();
            for (float i = 0.0f; i <= (float)(2.0 * Math.PI); i += delta_phi)
            {
                points.Add(Interpolate(i));
            }

            return points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public override void Render(Graphics g)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "Ellipse";
            return string.Format("{0}{{C={1},R1={2},R2{3},A={4}}}", "Ellipse", center.ToString(), minorRadius.ToString(), majorRadius.ToString(), angle.ToString());
        }
    }
}
