// <copyright file="Arc.cs" >
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
    //[GraphicsObject]
    [DisplayName("Arc")]
    public class Arc
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
        /// 
        /// </summary>
        private double startAngle;

        /// <summary>
        /// 
        /// </summary>
        private double endAngle;

        /// <summary>
        /// Interpolated points.
        /// </summary>
        private List<Point2D> points;

        /// <summary>
        /// Initializes a new instance of the <see cref="Arc"/> class.
        /// </summary>
        public Arc()
            : this(Point2D.Empty, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arc"/> class.
        /// </summary>
        public Arc(Circle circle, double startAngle, double endAngle)
            : this(circle.Center, circle.Radius, startAngle, endAngle)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arc"/> class.
        /// </summary>
        /// <param name="center">The center point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        public Arc(Point2D center, double radius, double startAngle, double endAngle)
        {
            this.center = center;
            this.radius = radius;
            this.startAngle = startAngle;
            this.endAngle = endAngle;
            points = InterpolatePoints();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangle"></param>
        public Arc(Triangle triangle)
            : this(triangle.A, triangle.B, triangle.C)
        {        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PointA"></param>
        /// <param name="PointB"></param>
        /// <param name="PointC"></param>
        public Arc(Point2D PointA, Point2D PointB, Point2D PointC)
        {
            //ToDo: calculate the angles of the start and end points from the center to fill them in.
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
            get
            {
                return radius;
            }
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
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Center
        {
            get
            {
                return center;
            }
            set
            {
                center = value;
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
            get
            {
                return startAngle;
            }
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
            get
            {
                return endAngle;
            }
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
        [Description("The rectangular boundaries of the circle.")]
        public new Rectangle2D Bounds
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
        [Description("The distance around the arc.")]
        public double ArcLength
        {
            get
            {
                // TODO: Divide by the arc length of the arc.
                return 2 * radius * Math.PI;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The area of the arc.")]
        public double Area
        {
            get
            {
                //ToDo: Divide by the Arc-length.
                return Math.PI * radius * radius;
            }
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
        /// <remarks>Note: this may become Obsolete when I figure out how to further 
        /// Integrate this into the Ellipse Structure</remarks>
        public static Point2D TripointArcCenter(Point2D PointA, Point2D PointB, Point2D PointC)
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
        /// <remarks>Note: this may become Obsolete when I figure out how to further 
        /// Integrate this into the Ellipse Structure</remarks>
        public static Rectangle2D TripoinArcBounds(Point2D PointA, Point2D PointB, Point2D PointC)
        {
            Point2D Center = TripointArcCenter(PointA, PointB, PointC);
            float Radius = (float)(Center.Length(PointA));
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
            float delta_phi = (float)(2 * Math.PI / ArcLength);
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
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "Arc";
            return string.Format("{0}{{C={1},R={2},A1={3},A2{4}}}", "Arc", center.ToString(), radius.ToString(), startAngle.ToString(), endAngle.ToString());
        }
    }
}
