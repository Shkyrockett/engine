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

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Circle")]
    public class Circle
        : Shape
    {
        /// <summary>
        /// The center point of the circle.
        /// </summary>
        private Point2D center;

        /// <summary>
        /// The radius of the circle.
        /// </summary>
        private double radius;

        /// <summary>
        /// Interpolated points.
        /// </summary>
        private List<Point2D> points;

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        public Circle()
            : this(Point2D.Empty, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="center">The center point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        public Circle(Point2D center, double radius)
        {
            this.center = center;
            this.radius = radius;
            points = InterpolatePoints();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="bounds">The bounding box of the circle.</param>
        public Circle(Rectangle2D bounds)
        {
            center = bounds.Center();
            radius = bounds.Height <= bounds.Width ? bounds.Height * 0.25f : bounds.Width * 0.25f;
            points = InterpolatePoints();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangle"></param>
        public Circle(Triangle triangle)
            : this(triangle.A, triangle.B, triangle.C)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PointA"></param>
        /// <param name="PointB"></param>
        /// <param name="PointC"></param>
        public Circle(Point2D PointA, Point2D PointB, Point2D PointC)
        {
            //  Calculate the slopes of the lines.
            double slopeA = (PointA.Slope(PointB));
            double slopeB = (PointC.Slope(PointB));
            Vector2D f = new Vector2D(((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X))),
                ((((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X))));

            // Find the center.
            center = new Point2D(f.X - (slopeB * ((f.X - f.Y) / (slopeB - slopeA))), (f.X - f.Y) / (slopeB - slopeA));

            // Get the radius.
            radius = (Center.Length(PointA));
        }

        /// <summary>
        /// Gets or sets the radius of the circle.
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Category("Elements")]
        [Description("The radius of the circle.")]
        public double Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                points = InterpolatePoints();
            }
        }

        /// <summary>
        /// Gets or sets the center of the circle.
        /// </summary>
        [Category("Elements")]
        [Description("The center location of the circle.")]
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
                points = InterpolatePoints();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The rectangular boundaries of the circle.")]
        public override Rectangle2D Bounds
        {
            get
            {
                Rectangle2D bounds = Rectangle2D.FromLTRB(
                    (center.X - radius),
                    (center.Y - radius),
                    (center.X + radius),
                    (center.Y + radius));
                return bounds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Category("Properties")]
        [Description("The distance around the circle.")]
        public double Circumference
        {
            get { return 2 * radius * Math.PI; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The area of the circle.")]
        public double Area
        {
            get { return Math.PI * radius * radius; }
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
                return new List<Point2D> { center, new Point2D(center.X + radius, center.Y) };
            }
            set
            {
                if (value.Count >= 1) center = value[0];
                if (value.Count >= 2) radius = (float)value[0].Length(value[1]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// Find the Center of A Circle from Three Points
        /// </summary>
        /// <param name="PointA">First Point on the Ellipse</param>
        /// <param name="PointB">Second Point on the Ellipse</param>
        /// <param name="PointC">Last Point on the Ellipse</param>
        /// <returns>Returns the Center point of a Circle defined by three points</returns>
        /// <remarks>
        /// </remarks>
        public static Point2D TripointCircleCenter(Point2D PointA, Point2D PointB, Point2D PointC)
        {
            //  Calculate the slopes of the lines.
            double SlopeA = (PointA.Slope(PointB));
            double SlopeB = (PointC.Slope(PointB));
            double FY = ((((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X)));
            double FX = ((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X)));
            double NewY = ((FX - FY) / (SlopeB - SlopeA));
            double NewX = (FX - (SlopeB * NewY));
            return new Point2D(NewX, NewY);
        }

        /// <summary>
        /// Find the Bounds of A Circle from Three Points 
        /// </summary>
        /// <param name="PointA">First Point on the Ellipse</param>
        /// <param name="PointB">Second Point on the Ellipse</param>
        /// <param name="PointC">Last Point on the Ellipse</param>
        /// <returns>A Rectangle Representing the bounds of A Circle Defined from three 
        /// Points</returns>
        public static Rectangle2D TripointCircleBounds(Point2D PointA, Point2D PointB, Point2D PointC)
        {
            Point2D Center = TripointCircleCenter(PointA, PointB, PointC);
            double Radius = (Center.Length(PointA));
            Rectangle2D Bounds = Rectangle2D.FromLTRB((Center.X - Radius), (Center.Y - Radius), (Center.X + Radius), (Center.Y + Radius));
            return Bounds;
        }

        /// <summary>
        /// Interpolates the circle.
        /// </summary>
        /// <param name="index">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public Point2D Interpolate(double index)
        {
            return new Point2D((float)(center.X + (Math.Sin(index) * radius)), (float)(center.X + (Math.Cos(index) * radius)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Point2D> InterpolatePoints()
        {
            double delta_phi = (2 * Math.PI / Circumference);
            List<Point2D> points = new List<Point2D>();
            for (double i = 0.0f; i <= (2.0 * Math.PI); i += delta_phi)
            {
                points.Add(Interpolate(i));
            }

            return points;
        }

        /// <summary>
        /// Render the shape to the canvas.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        public override void Render(Graphics g)
        {
            //g.FillEllipse(Style.BackBrush, Bounds);
            //g.DrawEllipse(Style.ForePen, Bounds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "Circle";
            return string.Format("{0}{{C={1},R={2}}}", "Circle", center.ToString(), radius.ToString());
        }
    }
}
