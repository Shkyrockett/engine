// <copyright file="Arc.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
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
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Arc))]
    public class Arc
        : Shape, IOpenShape
    {
        #region Private Fields

        /// <summary>
        /// The center point of the Arc.
        /// </summary>
        private Point2D center;

        /// <summary>
        /// The radius of the Arc.
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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new default instance of the <see cref="Arc"/> class.
        /// </summary>
        public Arc()
            : this(Point2D.Empty, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arc"/> class.
        /// </summary>
        /// <param name="triangle"></param>
        public Arc(Triangle triangle)
            : this(triangle.A, triangle.B, triangle.C)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arc"/> class.
        /// </summary>
        public Arc(Circle circle, double startAngle, double endAngle)
            : this(circle.Center, circle.Radius, startAngle, endAngle)
        { }

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
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arc"/> class.
        /// </summary>
        /// <param name="PointA"></param>
        /// <param name="PointB"></param>
        /// <param name="PointC"></param>
        public Arc(Point2D PointA, Point2D PointB, Point2D PointC)
        {
            // ToDo: calculate the angles of the start and end points from the center to fill them in.
            // Calculate the slopes of the lines.
            double slopeA = (PointA.Slope(PointB));
            double slopeB = (PointC.Slope(PointB));
            Vector2D f = new Vector2D(((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X))),
                ((((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X))));

            // Find the center.
            center = new Point2D(f.I - (slopeB * ((f.I - f.J) / (slopeB - slopeA))), (f.I - f.J) / (slopeB - slopeA));

            // Get the radius.
            radius = (Center.Length(PointA));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the radius of the Arc.
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Category("Elements")]
        [Description("The radius of the Arc.")]
        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        /// <summary>
        /// Gets or sets the center of the Arc.
        /// </summary>
        [Category("Elements")]
        [Description("The center location of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Center
        {
            get { return center; }
            set { center = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public Point2D StartPoint
        {
            get { return new Point2D(center.X + radius * Cos(-startAngle), center.Y + radius * Sin(-startAngle)); }
            //set;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public Point2D EndPoint
        {
            get { return new Point2D(center.X + radius * Cos(-endAngle), center.Y + radius * Sin(-endAngle)); }
            //set;
        }

        /// <summary>
        /// Gets or sets the start angle of the Arc.
        /// </summary>
        [Category("Elements")]
        [Description("The start angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngle
        {
            get { return startAngle; }
            set { startAngle = value; }
        }

        /// <summary>
        /// Gets or sets the end angle of the Arc.
        /// </summary>
        [Category("Elements")]
        [Description("The end angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngle
        {
            get { return endAngle; }
            set { endAngle = value; }
        }

        /// <summary>
        /// Gets or sets the sweep angle of the Arc.
        /// </summary>
        [Category("Elements")]
        [Description("The sweep angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [XmlIgnore]
        public double SweepAngle
        {
            get { return startAngle - endAngle; }
            set { endAngle = value + startAngle; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override double Perimeter => ArcLength;

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The tight rectangular boundaries of the Arc.")]
        [XmlIgnore]
        public override Rectangle2D Bounds
        {
            get
            {
                Rectangle2D bounds = new Rectangle2D(StartPoint, EndPoint);
                double angleEnd = endAngle;
                // check that angle2 > angle1
                if (angleEnd < startAngle) angleEnd += 2 * PI;
                if ((angleEnd >= 0) && (startAngle <= 0)) bounds.Right = center.X + radius;
                if ((angleEnd >= Maths.HalfPi) && (startAngle <= Maths.HalfPi)) bounds.Top = center.Y - radius;
                if ((angleEnd >= PI) && (startAngle <= PI)) bounds.Left = center.X - radius;
                if ((angleEnd >= Maths.ThreeQuarterTau) && (startAngle <= Maths.ThreeQuarterTau)) bounds.Bottom = center.Y + radius;
                if ((angleEnd >= Maths.Tau) && (startAngle <= Maths.Tau)) bounds.Right = center.X + radius;
                return bounds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The rectangular boundaries of the circle containing the Arc.")]
        public Rectangle2D DrawingBounds
        {
            get { return Rectangle2D.FromLTRB((center.X - radius), (center.Y - radius), (center.X + radius), (center.Y + radius)); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Category("Properties")]
        [Description("The distance around the Arc.")]
        public double ArcLength
        {
            get { return 2 * PI * radius * -SweepAngle; }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //[Category("Properties")]
        //[Description("The area of the arc.")]
        //public double Area
        //{
        //    get
        //    {
        //        //ToDo: Divide by the Arc-length.
        //        return PI * radius * radius;
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //[Category("Functional")]
        //[Description("The array of grab handles for this shape.")]
        //public List<Point2D> Handles
        //{
        //    get { return new List<Point2D> { center, new Point2D(center.X + radius, center.Y) }; }
        //    set
        //    {
        //        if (value.Count >= 1) center = value[0];
        //        if (value.Count >= 2) radius = value[0].Length(value[1]);
        //    }
        //}

        #endregion

        /// <summary>
        /// Interpolates the Arc.
        /// </summary>
        /// <param name="index">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public override Point2D Interpolate(double index)
        {
            double t = startAngle + SweepAngle * index;
            return new Point2D(
                center.X + (Sin(t) * radius),
                center.X + (Cos(t) * radius));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Point2D> InterpolatePoints()
        {
            double delta_phi = 2 * PI / ArcLength;
            List<Point2D> points = new List<Point2D>();
            for (double i = 0.0f; i <= 2.0 * PI; i += delta_phi)
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
            if (this == null) return nameof(Arc);
            return $"{nameof(Arc)}{{{nameof(Center)}={center},{nameof(Radius)}={radius},{nameof(StartAngle)}={startAngle},{nameof(EndAngle)}={endAngle}}}";
        }
    }
}
