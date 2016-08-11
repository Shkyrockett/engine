// <copyright file="Arc.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(CircularArc))]
    public class CircularArc
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
        private double sweepAngle;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new default instance of the <see cref="CircularArc"/> class.
        /// </summary>
        public CircularArc()
            : this(0, 0, 0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArc"/> class.
        /// </summary>
        /// <param name="triangle"></param>
        public CircularArc(Triangle triangle)
            : this(triangle.A, triangle.B, triangle.C)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArc"/> class.
        /// </summary>
        public CircularArc(Circle circle, double startAngle, double sweepAngle)
            : this(circle.Center, circle.Radius, startAngle, sweepAngle)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArc"/> class.
        /// </summary>
        /// <param name="x">The center x coordinate point of the circle.</param>
        /// <param name="y">The center y coordinate point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public CircularArc(double x, double y, double radius, double startAngle, double sweepAngle)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.startAngle = startAngle;
            this.sweepAngle = sweepAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArc"/> class.
        /// </summary>
        /// <param name="center">The center point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public CircularArc(Point2D center, double radius, double startAngle, double sweepAngle)
        {
            x = center.X;
            y = center.Y;
            this.radius = radius;
            this.startAngle = startAngle;
            this.sweepAngle = sweepAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArc"/> class.
        /// </summary>
        /// <param name="PointA"></param>
        /// <param name="PointB"></param>
        /// <param name="PointC"></param>
        public CircularArc(Point2D PointA, Point2D PointB, Point2D PointC)
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
            get { return new Point2D(x, y); }
            set
            {
                x = value.X;
                y = value.Y;
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="X"/> coordinate location of the center of the <see cref="CircularArc"/>.
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
        /// Gets or sets the <see cref="Y"/> coordinate location of the center of the <see cref="CircularArc"/>.
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
        /// Gets or sets the start angle of the Arc.
        /// </summary>
        [GeometryAngle]
        [Category("Clipping")]
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
        /// Gets or sets the sweep angle of the Arc.
        /// </summary>
        [GeometryAngle]
        [Category("Clipping")]
        [Description("The sweep angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [XmlIgnore]
        public double SweepAngle
        {
            get { return sweepAngle; }
            set
            {
                sweepAngle = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the end angle of the Arc.
        /// </summary>
        [GeometryAngle]
        [Category("Clipping")]
        [Description("The end angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngle
        {
            get { return startAngle + sweepAngle; }
            set
            {
                sweepAngle = value - startAngle;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public Point2D StartPoint
            => Interpolaters.CircularArc(x, y, radius, startAngle, sweepAngle, 0);
        // new Point2D(x + radius * Cos(-startAngle), y + radius * Sin(-startAngle));

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public Point2D EndPoint
            => Interpolaters.CircularArc(x, y, radius, startAngle, sweepAngle, 1);
        //new Point2D(x + radius * Cos(-sweepAngle), y + radius * Sin(-sweepAngle));

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override double Perimeter
            => ArcLength;

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The tight rectangular boundaries of the Arc.")]
        [XmlIgnore]
        public override Rectangle2D Bounds
            => Boundings.CircularArc(x, y, radius, startAngle, SweepAngle);

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The rectangular boundaries of the circle containing the Arc.")]
        public Rectangle2D DrawingBounds
            => Boundings.Circle(x, y, radius);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Category("Properties")]
        [Description("The distance around the Arc.")]
        public double ArcLength
            => Perimeters.ArcLength(radius, SweepAngle);

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The area of the arc.")]
        public override double Area
            => Areas.Arc(radius, SweepAngle);

        #endregion

        #region Interpolaters

        /// <summary>
        /// Interpolates the Arc.
        /// </summary>
        /// <param name="t">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public override Point2D Interpolate(double t)
            => Interpolaters.CircularArc(x, y, radius, startAngle, SweepAngle, t);

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool Contains(Point2D point)
            => Intersections.Contains(this, point) != Inclusion.Outside;

        /// <summary>
        /// Creates a string representation of this <see cref="CircularArc"/> struct based on the format string
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
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null)
                return nameof(CircularArc);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(CircularArc)}{{{nameof(Center)}={Center}{sep}{nameof(Radius)}={radius}{sep}{nameof(StartAngle)}={startAngle}{sep}{nameof(EndAngle)}={sweepAngle}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
