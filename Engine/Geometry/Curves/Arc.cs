﻿// <copyright file="Arc.cs" >
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
using System.Diagnostics.Contracts;
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
        #region Fields

        /// <summary>
        /// The center x coordinate point of the Arc.
        /// </summary>
        private double x;

        /// <summary>
        /// The center y coordinate point of the Arc.
        /// </summary>
        private double y;

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
            : this(0, 0, 0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arc"/> class.
        /// </summary>
        /// <param name="triangle"></param>
        public Arc(Triangle triangle)
            : this(triangle.A, triangle.B, triangle.C)
        {
        }

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
        /// <param name="x">The center x coordinate point of the circle.</param>
        /// <param name="y">The center y coordinate point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        public Arc(double x, double y, double radius, double startAngle, double endAngle)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.startAngle = startAngle;
            this.endAngle = endAngle;
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
            x = center.X;
            y = center.Y;
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
            var f = new Vector2D(((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X))),
                ((((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X))));

            // Find the center.
            x = f.I - (slopeB * ((f.I - f.J) / (slopeB - slopeA)));
            y = (f.I - f.J) / (slopeB - slopeA);

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
            set
            {
                radius = value;
                update?.Invoke();
            }
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
            get { return new Point2D(x,y); }
            set
            {
                x = value.X;
                y = value.Y;
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="X"/> coordinate location of the center of the <see cref="Arc"/>.
        /// </summary>
        [Category("Elements")]
        [Description("The center x coordinate location of the arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [XmlAttribute]
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Y"/> coordinate location of the center of the <see cref="Arc"/>.
        /// </summary>
        [Category("Elements")]
        [Description("The center y coordinate location of the arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [XmlAttribute]
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public Point2D StartPoint => new Point2D(x + radius * Cos(-startAngle), y + radius * Sin(-startAngle));

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public Point2D EndPoint => new Point2D(x + radius * Cos(-endAngle), y + radius * Sin(-endAngle));

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
            set
            {
                startAngle = value;
                update?.Invoke();
            }
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
            set
            {
                endAngle = value;
                update?.Invoke();
            }
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
            set
            {
                endAngle = value + startAngle;
                update?.Invoke();
            }
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
                var bounds = new Rectangle2D(StartPoint, EndPoint);
                double angleEnd = endAngle;
                // check that angle2 > angle1
                if (angleEnd < startAngle) angleEnd += 2 * PI;
                if ((angleEnd >= 0) && (startAngle <= 0)) bounds.Right = x + radius;
                if ((angleEnd >= Maths.HalfPi) && (startAngle <= Maths.HalfPi)) bounds.Top = y - radius;
                if ((angleEnd >= PI) && (startAngle <= PI)) bounds.Left = x - radius;
                if ((angleEnd >= Maths.ThreeQuarterTau) && (startAngle <= Maths.ThreeQuarterTau)) bounds.Bottom = y + radius;
                if ((angleEnd >= Maths.Tau) && (startAngle <= Maths.Tau)) bounds.Right = x + radius;
                return bounds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The rectangular boundaries of the circle containing the Arc.")]
        public Rectangle2D DrawingBounds => Rectangle2D.FromLTRB((x - radius), (y - radius), (x + radius), (y + radius));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Category("Properties")]
        [Description("The distance around the Arc.")]
        public double ArcLength => 2 * PI * radius * -SweepAngle;

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The area of the arc.")]
        public override double Area => (radius * radius * 0.5d) * (SweepAngle - Sin(SweepAngle));

        #endregion

        #region Interpolaters

        /// <summary>
        /// Interpolates the Arc.
        /// </summary>
        /// <param name="t">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public override Point2D Interpolate(double t)
        {
            double theta = startAngle + SweepAngle * t;
            return new Point2D(
                x + (Sin(theta) * radius),
                y + (Cos(theta) * radius));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Point2D> InterpolatePoints()
        {
            double delta_phi = 2 * PI / ArcLength;
            var points = new List<Point2D>();
            for (double i = 0.0f; i <= 2.0 * PI; i += delta_phi)
                points.Add(Interpolate(i));

            return points;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a string representation of this <see cref="Arc"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        internal override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Arc);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Arc)}{{{nameof(Center)}={Center}{sep}{nameof(Radius)}={radius}{sep}{nameof(StartAngle)}={startAngle}{sep}{nameof(EndAngle)}={endAngle}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
