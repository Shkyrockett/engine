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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [XmlAttribute()]
        private Point2D center;

        /// <summary>
        /// Major Radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        private double a;

        /// <summary>
        /// Minor Radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        private double b;

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
        /// <param name="pointA">First Point on the Ellipse</param>
        /// <param name="pointB">Second Point on the Ellipse</param>
        /// <param name="pointC">Last Point on the Ellipse</param>
        /// <param name="aspect">Aspect of Ellipse Note: Does not currently work.</param>
        /// <param name="angle">Angle of Ellipse Note: Does not currently work.</param>
        /// <remarks></remarks>
        public Ellipse(Point2D pointA, Point2D pointB, Point2D pointC, double aspect, double angle)
        {
            //  Calculate the slopes of the lines.
            double SlopeA = pointA.Slope(pointB);
            double SlopeB = pointC.Slope(pointB);
            double FY = ((((pointA.X - pointB.X) * (pointA.X + pointB.X)) + ((pointA.Y - pointB.Y) * (pointA.Y + pointB.Y))) / (2 * (pointA.X - pointB.X)));
            double FX = ((((pointC.X - pointB.X) * (pointC.X + pointB.X)) + ((pointC.Y - pointB.Y) * (pointC.Y + pointB.Y))) / (2 * (pointC.X - pointB.X)));
            double NewY = ((FX - FY) / (SlopeB - SlopeA));
            double NewX = (FX - (SlopeB * NewY));
            center = new Point2D(NewX, NewY);
            //  Find the Radius
            a = (center.Length(pointA));
            this.Aspect = aspect;
            this.angle = angle;
            Points = InterpolatePoints();
        }

        /// <summary>
        /// Creates a new Instance of Ellipse
        /// </summary>
        /// <param name="center">Center Point of Ellipse</param>
        /// <param name="a">Major radius of Ellipse</param>
        /// <param name="b">Minor radius of Ellipse.</param>
        /// <param name="angle">Angle of Ellipse.</param>
        /// <remarks></remarks>
        public Ellipse(Point2D center, double a, double b, double angle)
        {
            this.center = center;
            this.a = a;
            this.b = b;
            this.angle = angle;
            Points = InterpolatePoints();
        }

        /// <summary>
        /// Center Point of Ellipse
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        [Category("Elements")]
        [Description("The center location of the ellipse.")]
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
                Points = InterpolatePoints();
            }
        }

        /// <summary>
        /// First radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        [Category("Elements")]
        [Description("The first radius of the ellipse.")]
        [RefreshProperties(RefreshProperties.All)]
        public double A
        {
            get { return a; }
            set
            {
                a = value;
                Points = InterpolatePoints();
            }
        }

        /// <summary>
        /// Second radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        [Category("Elements")]
        [Description("The second radius of the ellipse.")]
        [RefreshProperties(RefreshProperties.All)]
        public double B
        {
            get { return b; }
            set
            {
                b = value;
                Points = InterpolatePoints();
            }
        }

        /// <summary>
        /// Major radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        [Category("Elements")]
        [Description("The larger radius of the ellipse.")]
        public double MajorRadius
        {
            get { return a >= b ? a : b; }
        }

        /// <summary>
        /// Minor radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        [Category("Elements")]
        [Description("The smaller radius of the ellipse.")]
        [RefreshProperties(RefreshProperties.All)]
        public double MinorRadius
        {
            get { return a <= b ? a : b; }
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
            get { return b / a; }
            set
            {
                b = a * value;
                a = b / value;
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
        /// <remarks>https://en.wikipedia.org/wiki/Ellipse</remarks>
        [XmlIgnore]
        [Category("Elements")]
        [Description("The focus radius of the ellipse.")]
        public double FocusRadius
        {
            get { return Math.Sqrt((a * a) - (b * b)); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Ellipse</remarks>
        [XmlIgnore]
        [Category("Elements")]
        [Description("The eccentricity radius of the ellipse.")]
        public double Eccentricity
        {
            get { return Math.Sqrt(1 - ((a / b) * (a / b))); }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The rectangular bounds of the ellipse.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get
            {
                double phi = MathExtensions.ToRadians(angle);
                double ux = a * Math.Cos(phi);
                double uy = a * Math.Sin(phi);
                double vx = (a * Aspect) * Math.Cos(phi + Math.PI / 2);
                double vy = (a * Aspect) * Math.Sin(phi + Math.PI / 2);

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
                return this.EllipsePerimeterSykoraRiveraCantrellsParticularlyFruitful();
                //return this.PerimeterAhmadi2006();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The area of the ellipse.")]
        public double Area
        {
            get { return Math.PI * b * a; }
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
                    A = (new LineSegment(center, value[1]).Length());
                    Angle = (new LineSegment(center, value[1]).Angle());
                    Aspect = ((new LineSegment(center, value[2]).Length()) / a);
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
            return new Rectangle2D(center.X - a, center.Y - a, a, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="theta"></param>
        /// <param name="ellipse"></param>
        /// <param name="phi"></param>
        /// <param name="rect"></param>
        private void draw_rect_at_ellipse(Graphics g, double theta, Rectangle2D ellipse, double phi, Rectangle2D rect)
        {
            Point2D xaxis = new Point2D(Math.Cos(theta), Math.Sin(theta));
            Point2D yaxis = new Point2D(-Math.Sin(theta), Math.Cos(theta));
            Point2D ellipse_point;

            // Ellipse equation for an ellipse at origin.
            ellipse_point = new Point2D((float)(ellipse.Width * Math.Cos(phi)), (float)(ellipse.Height * Math.Sin(phi)));

            // Apply the rotation transformation and translate to new center.
            rect.Location = new Point2D(ellipse.Left + (ellipse_point.X * xaxis.X + ellipse_point.Y * xaxis.Y),
                                       ellipse.Top + (ellipse_point.X * yaxis.X + ellipse_point.Y * yaxis.Y));

            g.DrawRectangle(Pens.AntiqueWhite, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point2D Interpolate(double index)
        {
            Rectangle2D unroatatedBounds = UnrotatedBounds();

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
            double delta_phi = (2 * Math.PI / Perimeter);
            List<Point2D> points = new List<Point2D>();
            for (double i = 0.0f; i <= (2.0 * Math.PI); i += delta_phi)
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
            return string.Format("{0}{{C={1},R1={2},R2{3},A={4}}}", "Ellipse", center.ToString(), b.ToString(), a.ToString(), angle.ToString());
        }
    }
}
