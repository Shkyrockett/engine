﻿// <copyright file="Ellipse.cs" >
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
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
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
        : Shape, IClosedShape
    {
        #region Fields

        /// <summary>
        /// The center x coordinate point of the <see cref="Ellipse"/>.
        /// </summary>
        private double x;

        /// <summary>
        /// The center y coordinate point of the <see cref="Ellipse"/>.
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

        #endregion

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        public Ellipse()
            : this(0, 0, 0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse"/> class.
        /// </summary>
        /// <param name="x">Center Point x coordinate of <see cref="Ellipse"/>.</param>
        /// <param name="y">Center Point x coordinate of <see cref="Ellipse"/>.</param>
        /// <param name="r1">Major radius of <see cref="Ellipse"/>.</param>
        /// <param name="r2">Minor radius of <see cref="Ellipse"/>.</param>
        /// <param name="angle">Angle of <see cref="Ellipse"/>.</param>
        /// <remarks></remarks>
        public Ellipse(double x, double y, double r1, double r2, double angle)
        {
            this.x = x;
            this.y = y;
            this.r1 = r1;
            this.r2 = r2;
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
            x = center.X;
            y = center.Y;
            r1 = a;
            r2 = b;
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
        [Description("The " + nameof(Center) + " location of the " + nameof(Ellipse) + ".")]
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
        /// Gets the angles of the extreme points of the rotated ellipse.
        /// </summary>
        [Pure]
        [XmlIgnore]
        [Category("Properties")]
        [Description("The angles of the extreme points of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<double> ExtremeAngles
        {
            get
            {
                // Get the ellipse rotation transform.
                double cosT = Cos(angle);
                double sinT = Sin(angle);

                // Calculate the radii of the angle of rotation.
                double a = r1 * cosT;
                double b = r2 * sinT;
                double c = r1 * sinT;
                double d = r2 * cosT;

                // Ellipse equation for an ellipse at origin.
                double u1 = r1 * Cos(Atan2(d, c));
                double v1 = -(r2 * Sin(Atan2(d, c)));
                double u2 = r1 * Cos(Atan2(-b, a));
                double v2 = -(r2 * Sin(Atan2(-b, a)));

                return new List<double>
                {
                    Atan2(u1 * sinT - v1 * cosT, u1 * cosT + v1 * sinT),
                    Atan2(u2 * sinT - v2 * cosT, u2 * cosT + v2 * sinT),
                    Atan2(u2 * sinT - v2 * cosT, u2 * cosT + v2 * sinT) + PI,
                    Atan2(u1 * sinT - v1 * cosT, u1 * cosT + v1 * sinT) + PI
                };
            }
        }

        /// <summary>
        /// Get the points of the Cartesian extremes of a rotated ellipse.
        /// </summary>
        /// <remarks>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
        [Pure]
        [XmlIgnore]
        [Category("Properties")]
        [Description("The locations of the extreme points of the " + nameof(Ellipse) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<Point2D> ExtremePoints
        {
            get
            {
                double a = r1 * Cos(angle);
                double c = r1 * Sin(angle);
                double d = r2 * Cos(angle);
                double b = r2 * Sin(angle);

                // Find the angles of the Cartesian extremes.
                double a1 = Atan2(-b, a);
                double a2 = Atan2(-b, a) + PI;
                double a3 = Atan2(d, c);
                double a4 = Atan2(d, c) + PI;

                // Return the points of Cartesian extreme of the rotated ellipse.
                return new List<Point2D> {
                    Interpolaters.Ellipse(x, y, r1, r2, angle, a1),
                    Interpolaters.Ellipse(x, y, r1, r2, angle, a2),
                    Interpolaters.Ellipse(x, y, r1, r2, angle, a3),
                    Interpolaters.Ellipse(x, y, r1, r2, angle, a4)
                };
            }
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
            get { return Boundings.Ellipse(x, y, r1, r2, angle); }
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
            => Perimeters.EllipsePerimeter(r1, r2);

        /// <summary>
        /// Gets the <see cref="Area"/> of the <see cref="Ellipse"/>.
        /// </summary>
        [XmlIgnore]
        [Category("Properties")]
        [Description("The " + nameof(Area) + " of the " + nameof(Ellipse) + ".")]
        public override double Area
        {
            get { return Areas.Ellipse(r1, r2); }
            set
            {
                // ToDo: Figure out the correct formula.
                double a = Aspect;
                r1 = value * a / PI;
                r2 = value * a / PI;
                update?.Invoke();
            }
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
            => Boundings.Ellipse(x, y, r1, r2);

        #endregion

        #region Interpolaters

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Interpolaters.Ellipse(x, y, r1, r2, angle, t);

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
            IFormattable formatable = $"{nameof(Ellipse)}{{{nameof(Center)}={Center},{nameof(R1)}={r1},{nameof(R2)}={r2},{nameof(Angle)}={angle}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
