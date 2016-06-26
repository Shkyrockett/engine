// <copyright file="EllipseArc.cs" >
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
    /// <remarks>
    /// http://www.vbforums.com/showthread.php?686351-RESOLVED-Elliptical-orbit
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Ellipse Arc")]
    public class EllipseArc
        : Shape
    {
        #region Fields

        /// <summary>
        /// The center x coordinate point of the <see cref="EllipseArc"/>.
        /// </summary>
        private double x;

        /// <summary>
        /// The center y coordinate point of the <see cref="EllipseArc"/>.
        /// </summary>
        private double y;

        /// <summary>
        /// Major Radius of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        private double r1;

        /// <summary>
        /// Minor Radius of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        private double r2;

        /// <summary>
        /// Angle of <see cref="Ellipse"/>. 
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
        private double sweepAngle;

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public EllipseArc()
            : this(0, 0, 0, 0, 0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipseArc"/> class.
        /// </summary>
        /// <param name="x">Center Point x coordinate of <see cref="EllipseArc"/>.</param>
        /// <param name="y">Center Point x coordinate of <see cref="EllipseArc"/>.</param>
        /// <param name="r1">Major radius of <see cref="EllipseArc"/>.</param>
        /// <param name="r2">Minor radius of <see cref="EllipseArc"/>.</param>
        /// <param name="angle">Angle of <see cref="EllipseArc"/>.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <remarks></remarks>
        public EllipseArc(double x, double y, double r1, double r2, double angle, double startAngle, double sweepAngle)
        {
            this.x = x;
            this.y = y;
            this.r1 = r1;
            this.r2 = r2;
            this.angle = angle;
            this.startAngle = startAngle;
            this.sweepAngle = sweepAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipseArc"/> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="EllipseArc"/>.</param>
        /// <param name="a">Major radius of <see cref="EllipseArc"/>.</param>
        /// <param name="b">Minor radius of <see cref="EllipseArc"/>.</param>
        /// <param name="angle">Angle of <see cref="EllipseArc"/>.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <remarks></remarks>
        public EllipseArc(Point2D center, double a, double b, double angle, double startAngle, double sweepAngle)
        {
            x = center.X;
            y = center.Y;
            r1 = a;
            r2 = b;
            this.angle = angle;
            this.startAngle = startAngle;
            this.sweepAngle = sweepAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipseArc"/> class.
        /// </summary>
        /// <param name="center">Center Point of <see cref="EllipseArc"/>.</param>
        /// <param name="size">Major and Minor radii of <see cref="EllipseArc"/>.</param>
        /// <param name="angle">Angle of <see cref="EllipseArc"/>.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <remarks></remarks>
        public EllipseArc(Point2D center, Size2D size, double angle, double startAngle, double sweepAngle)
            : this(center.X, center.Y, size.Width, size.Height, angle, startAngle, sweepAngle)
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

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Point2D Location
        {
            get { return new Point2D(x - r1, y - r2); }
            set
            {
                x = value.X + r1;
                y = value.Y + r2;
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Center Point of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        [Category("Elements")]
        [Description("The " + nameof(Center) + " location of the " + nameof(EllipseArc) + ".")]
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
        /// Gets or sets the X coordinate location of the center of the circle.
        /// </summary>
        /// <remarks></remarks>
        [Category("Elements")]
        [Description("The center x coordinate location of the circle.")]
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
        /// Gets or sets the Y coordinate location of the center of the circle.
        /// </summary>
        [Category("Elements")]
        [Description("The center y coordinate location of the circle.")]
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
            => r1 >= r2 ? r1 : r2;

        /// <summary>
        /// Gets the Minor radius of <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        [Category("Elements")]
        [Description("The smaller radius of the " + nameof(Ellipse) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        public double MinorRadius
            => r1 <= r2 ? r1 : r2;

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
        [GeometryAngle]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
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
                update?.Invoke();
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
        /// Gets the Focus Radius of the <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Ellipse</remarks>
        [XmlIgnore]
        [Category("Properties")]
        [Description("The focus radius of the " + nameof(Ellipse) + ".")]
        public double FocusRadius
            => Sqrt((r1 * r1) - (r2 * r2));

        /// <summary>
        /// Gets the <see cref="Eccentricity"/> of the <see cref="Ellipse"/>.
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Ellipse</remarks>
        [XmlIgnore]
        [Category("Properties")]
        [Description("The " + nameof(Eccentricity) + " of the " + nameof(Ellipse) + ".")]
        public double Eccentricity
            => Sqrt(1 - ((r1 / r2) * (r1 / r2)));

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
                double phi = angle;
                double ux = r1 * Cos(phi);
                double uy = r1 * Sin(phi);
                double vx = (r1 * Aspect) * Cos(phi + PI / 2);
                double vy = (r1 * Aspect) * Sin(phi + PI / 2);

                double bbox_halfwidth = Sqrt(ux * ux + vx * vx);
                double bbox_halfheight = Sqrt(uy * uy + vy * vy);

                return Rectangle2D.FromLTRB(
                    (x - bbox_halfwidth),
                    (y - bbox_halfheight),
                    (x + bbox_halfwidth),
                    (y + bbox_halfheight)
                    );
            }
            set
            {
                Rectangle2D bounds1 = Bounds;
                double aspect = Aspect;
                Rectangle2D bounds2 = value;
                Vector2D locDif = bounds2.Location - bounds1.Location;
                Size2D scaleDif = bounds2.Size - bounds1.Size;
                Center += locDif;
                if (aspect > 1)
                {
                    r1 = r1 / bounds1.Width * bounds2.Width;
                    r2 = r2 / bounds1.Height * bounds2.Height;
                }
                else
                {
                    r2 = r2 / bounds1.Width * bounds2.Width;
                    r1 = r1 / bounds1.Height * bounds2.Height;
                }
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
                double minor = (MajorRadius * Aspect);
                return ((Sqrt(0.5 * ((minor * minor) + (MajorRadius * MajorRadius)))) * (PI * 2));

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
        /// Gets the <see cref="Area"/> of the <see cref="Ellipse"/>.
        /// </summary>
        [XmlIgnore]
        [Category("Properties")]
        [Description("The " + nameof(Area) + " of the " + nameof(Ellipse) + ".")]
        public override double Area
            => PI * r2 * r1;

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
            => new Rectangle2D(x - r1, y - r1, r1 * 2, r2 * 2);

        #endregion

        #region Interpolaters

        /// <summary>
        /// http://www.vbforums.com/showthread.php?686351-RESOLVED-Elliptical-orbit
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Interpolaters.EllipticArc(x, y, r1, r2, angle, startAngle, sweepAngle, t);

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
        internal override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null)
                return nameof(Ellipse);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(EllipseArc)}{{{nameof(Center)}={Center},{nameof(R1)}={r1},{nameof(R2)}={r2},{nameof(Angle)}={angle},{nameof(StartAngle)}={startAngle},{SweepAngle}={sweepAngle}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
