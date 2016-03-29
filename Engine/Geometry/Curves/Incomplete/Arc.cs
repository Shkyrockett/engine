// <copyright file="Arc.cs" company="Shkyrockett">
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
using System.Drawing;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    [DisplayName("Arc")]
    public class Arc
        : Shape
    {
        /// <summary>
        /// The center point of the circle.
        /// </summary>
        private PointF center;

        /// <summary>
        /// The radius of the circle.
        /// </summary>
        private float radius;

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
        private List<PointF> points;

        /// <summary>
        /// Initializes a new instance of the <see cref="Arc"/> class.
        /// </summary>
        public Arc()
            : this(PointF.Empty, 0, 0, 0)
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
        public Arc(PointF center, float radius, double startAngle, double endAngle)
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
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PointA"></param>
        /// <param name="PointB"></param>
        /// <param name="PointC"></param>
        public Arc(PointF PointA, PointF PointB, PointF PointC)
        {
            //ToDo: calculate the angles of the start and end points from the center to fill them in.
            //  Calculate the slopes of the lines.
            float slopeA = (float)(PointA.Slope(PointB));
            float slopeB = (float)(PointC.Slope(PointB));
            VectorF f = new VectorF(((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X))),
                ((((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X))));

            // Find the center.
            center = new PointF(f.X - (slopeB * ((f.X - f.Y) / (slopeB - slopeA))), (f.X - f.Y) / (slopeB - slopeA));

            // Get the radius.
            radius = (float)(Center.Length(PointA));
        }

        /// <summary>
        /// Gets or sets the radius of the circle.
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Category("Elements")]
        [Description("The radius of the circle.")]
        public float Radius
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
        [TypeConverter(typeof(PointFConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public PointF Center
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
        public RectangleF Bounds
        {
            get
            {
                RectangleF bounds = RectangleF.FromLTRB(
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
        public List<PointF> Handles
        {
            get
            {
                return new List<PointF> { center, new PointF(center.X + radius, center.Y) };
            }
            set
            {
                if (value.Count >= 1) center = value[0];
                if (value.Count >= 2) radius = (float)value[0].Length(value[1]);
            }
        }

        /// <summary>
        /// Find the Center of A Circle from Three Points
        /// </summary>
        /// <param name="PointA">First Point on the Ellipse</param>
        /// <param name="PointB">Second Point on the Ellipse</param>
        /// <param name="PointC">Last Point on the Ellipse</param>
        /// <returns>Returns the Center point of a Circle defined by three points</returns>
        /// <remarks>Note: this may become Obsolete when I figure out how to further 
        /// Integrate this into the Ellipse Structure</remarks>
        public static PointF TripointArcCenter(PointF PointA, PointF PointB, PointF PointC)
        {
            //  Calculate the slopes of the lines.
            float SlopeA = (float)(PointA.Slope(PointB));
            float SlopeB = (float)(PointC.Slope(PointB));
            float FY = ((((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X)));
            float FX = ((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X)));
            float NewY = ((FX - FY) / (SlopeB - SlopeA)); float NewX = (FX - (SlopeB * NewY));
            return new PointF(NewX, NewY);
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
        public static RectangleF TripoinArcBounds(PointF PointA, PointF PointB, PointF PointC)
        {
            PointF Center = TripointArcCenter(PointA, PointB, PointC);
            float Radius = (float)(Center.Length(PointA));
            RectangleF Bounds = RectangleF.FromLTRB((Center.X - Radius), (Center.Y - Radius), (Center.X + Radius), (Center.Y + Radius));
            return Bounds;
        }

        /// <summary>
        /// Interpolates the circle.
        /// </summary>
        /// <param name="index">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public PointF Interpolate(double index)
        {
            return new PointF((float)(center.X + (Math.Sin(index) * radius)), (float)(center.X + (Math.Cos(index) * radius)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PointF> InterpolatePoints()
        {
            float delta_phi = (float)(2 * Math.PI / ArcLength);
            List<PointF> points = new List<PointF>();
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
