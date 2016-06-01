// <copyright file="Ellipse.cs" >
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
using System.Drawing;
using System.Globalization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// http://math.stackexchange.com/questions/426150/what-is-the-general-equation-of-the-ellipse-that-is-not-in-the-origin-and-rotate
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Ellipse))]
    public class Ellipse
        : Shape, IClosedShape, IFormattable
    {
        #region Fields

        /// <summary>
        /// Center Point of Ellipse
        /// </summary>
        /// <remarks></remarks>
        private Point2D center;

        /// <summary>
        /// Major Radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        private double r1;

        /// <summary>
        /// Minor Radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        private double r2;

        /// <summary>
        /// Angle of <see cref="Ellipse"/>. 
        /// </summary>
        /// <remarks></remarks>
        private double angle;

        #endregion

        #region Constructors

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
            r1 = (center.Length(pointA));
            Aspect = aspect;
            this.angle = angle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse"/> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="Ellipse"/>.</param>
        /// <param name="a">Major radius of <see cref="Ellipse"/>.</param>
        /// <param name="b">Minor radius of <see cref="Ellipse"/>.</param>
        /// <param name="angle">Angle of <see cref="Ellipse"/>.</param>
        /// <remarks></remarks>
        public Ellipse(Point2D center, double a, double b, double angle)
        {
            this.center = center;
            this.r1 = a;
            this.r2 = b;
            this.angle = angle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse"/> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="Ellipse"/>.</param>
        /// <param name="size">Major and Minor radii of <see cref="Ellipse"/>.</param>
        /// <param name="angle">Angle of <see cref="Ellipse"/>.</param>
        /// <remarks></remarks>
        public Ellipse(Point2D center, Size2D size, double angle)
            : this(center, size.Width, size.Height, angle)
        { }

        #endregion

        #region Properties

        public Point2D Location
        {
            get { return new Point2D(center.X - r1, center.Y - r2); }
            set
            {
                center = new Point2D(value.X + r1, value.Y + r2);
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Center Point of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        [Category("Elements")]
        [Description("The " + nameof(Center) + " location of the " + nameof(Ellipse) + ".")]
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
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the first radius of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        [Category("Elements")]
        [Description("The first radius of the " + nameof(Ellipse) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double R1
        {
            get { return r1; }
            set
            {
                r1 = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the second radius of Ellipse
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        [Category("Elements")]
        [Description("The second radius of the " + nameof(Ellipse) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double R2
        {
            get { return r2; }
            set
            {
                r2 = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the Major radius of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        [Category("Elements")]
        [Description("The larger radius of the " + nameof(Ellipse) + ".")]
        public double MajorRadius
        {
            get { return r1 >= r2 ? r1 : r2; }
        }

        /// <summary>
        /// Gets the Minor radius of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        [Category("Elements")]
        [Description("The smaller radius of the " + nameof(Ellipse) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double MinorRadius
        {
            get { return r1 <= r2 ? r1 : r2; }
        }

        /// <summary>
        /// Gets or sets the Aspect ratio of <see cref="Ellipse"/>. 
        /// </summary>
        /// <remarks></remarks>
        [Category("Properties")]
        [Description("The " + nameof(Aspect) + " ratio of the major and minor axis of the " + nameof(Ellipse) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double Aspect
        {
            get { return r2 / r1; }
            set
            {
                r2 = r1 * value;
                r1 = r2 / value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Angle of the <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [Category("Elements")]
        [Description("The " + nameof(Angle) + " to rotate the " + nameof(Ellipse) + ".")]
        [XmlAttribute]
        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the Focus Radius of the <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Ellipse</remarks>
        [XmlIgnore]
        [Category("Properties")]
        [Description("The focus radius of the " + nameof(Ellipse) + ".")]
        public double FocusRadius
        {
            get { return Sqrt((r1 * r1) - (r2 * r2)); }
        }

        /// <summary>
        /// Gets the <see cref="Eccentricity"/> of the <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Ellipse</remarks>
        [XmlIgnore]
        [Category("Properties")]
        [Description("The " + nameof(Eccentricity) + " of the " + nameof(Ellipse) + ".")]
        public double Eccentricity
        {
            get { return Sqrt(1 - ((r1 / r2) * (r1 / r2))); }
        }

        /// <summary>
        /// Gets the Bounding box of the <see cref="Ellipse"/>.
        /// </summary>
        [XmlIgnore]
        [Category("Properties")]
        [Description("The rectangular bounds of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get
            {
                double phi = Maths.ToRadians(angle);
                double ux = r1 * Cos(phi);
                double uy = r1 * Sin(phi);
                double vx = (r1 * Aspect) * Cos(phi + PI / 2);
                double vy = (r1 * Aspect) * Sin(phi + PI / 2);

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
        /// Gets the <see cref="Perimeter"/> of the <see cref="Ellipse"/>.
        /// </summary>
        /// <returns></returns>
        [XmlIgnore]
        [Category("Properties")]
        [Description("The " + nameof(Perimeter) + " of the " + nameof(Ellipse) + ".")]
        public override double Perimeter
        {
            get
            {
                return this.EllipsePerimeterSykoraRiveraCantrellsParticularlyFruitful();
                //return this.PerimeterAhmadi2006();
            }
        }

        /// <summary>
        /// Gets the <see cref="Area"/> of the <see cref="Ellipse"/>.
        /// </summary>
        [XmlIgnore]
        [Category("Properties")]
        [Description("The " + nameof(Area) + " of the " + nameof(Ellipse) + ".")]
        public override double Area
        {
            get { return PI * r2 * r1; }
        }

        /// <summary>
        /// Gets the size and location of the ellipse, in double-point pixels, relative to the parent canvas.
        /// </summary>
        /// <returns>A System.Drawing.RectangleF in double-point pixels relative to the parent canvas that represents the size and location of the segment.</returns>
        /// <remarks></remarks>
        [XmlIgnore]
        [Category("Properties")]
        [Description("The unrotated rectangular bounds of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public Rectangle2D UnrotatedBounds
        {
            get { return new Rectangle2D(center.X - r1, center.Y - r1, r1, r2); }
        }

        #endregion

        #region Interpolaters

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
        {
            double phi = (2 * PI) * t;

            Rectangle2D unroatatedBounds = UnrotatedBounds;

            double theta = angle;
            Point2D xaxis = new Point2D(Cos(theta), Sin(theta));
            Point2D yaxis = new Point2D(-Sin(theta), Cos(theta));

            // Ellipse equation for an ellipse at origin.
            Point2D ellipsePoint = new Point2D(
                (unroatatedBounds.Width * Cos(phi)),
                (unroatatedBounds.Height * Sin(phi))
                );

            // Apply the rotation transformation and translate to new center.
            return new Point2D(
                Center.X + (ellipsePoint.X * xaxis.X + ellipsePoint.Y * xaxis.Y),
                Center.Y + (ellipsePoint.X * yaxis.X + ellipsePoint.Y * yaxis.Y)
                );
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool Contains(Point2D point)
        {
            return Intersections.EllipseContainsPoint(this, point);
        }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Ellipse"/> struct.
        /// </summary>
        /// <returns></returns>
        [Pure]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Ellipse"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Ellipse"/> struct based on the format string
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
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Ellipse"/> struct based on the format string
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
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Ellipse);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Ellipse)}{{{nameof(Center)}={center},{nameof(R1)}={r1},{nameof(R2)}={r2},{nameof(Angle)}={angle}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
