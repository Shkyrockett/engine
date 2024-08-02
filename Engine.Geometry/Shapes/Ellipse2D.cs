// <copyright file="Ellipse2D.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Polynomials;
using static System.Math;

namespace Engine;

/// <summary>
/// http://math.stackexchange.com/questions/426150/what-is-the-general-equation-of-the-ellipse-that-is-not-in-the-origin-and-rotate
/// </summary>
[DataContract, Serializable]
[GraphicsObject]
[DisplayName(nameof(Ellipse2D))]
[XmlType(TypeName = "ellipse", Namespace = "http://www.w3.org/2000/svg")]
[DebuggerDisplay("{ToString()}")]
public class Ellipse2D
    : Shape2D
{
    #region Fields
    /// <summary>
    /// The center x coordinate point of the <see cref="Ellipse2D"/>.
    /// </summary>
    private double x;

    /// <summary>
    /// The center y coordinate point of the <see cref="Ellipse2D"/>.
    /// </summary>
    private double y;

    /// <summary>
    /// Major Radius of <see cref="Ellipse2D"/>.
    /// </summary>
    private double radiusA;

    /// <summary>
    /// Minor Radius of <see cref="Ellipse2D"/>.
    /// </summary>
    private double radiusB;

    /// <summary>
    /// Angle of <see cref="Ellipse2D"/>.
    /// </summary>
    private double angle;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Ellipse2D"/> class.
    /// </summary>
    public Ellipse2D()
        : this(0, 0, 0, 0, 0)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ellipse2D"/> class.
    /// </summary>
    /// <param name="tuple"></param>
    public Ellipse2D((double X, double Y, double RX, double RY, double Angle) tuple)
        : this(tuple.X, tuple.Y, tuple.RX, tuple.RY, tuple.Angle)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ellipse2D"/> class.
    /// </summary>
    /// <param name="x">Center Point x coordinate of <see cref="Ellipse2D"/>.</param>
    /// <param name="y">Center Point x coordinate of <see cref="Ellipse2D"/>.</param>
    /// <param name="rX">Major radius of <see cref="Ellipse2D"/>.</param>
    /// <param name="rY">Minor radius of <see cref="Ellipse2D"/>.</param>
    /// <param name="angle">Angle of <see cref="Ellipse2D"/>.</param>
    public Ellipse2D(double x, double y, double rX, double rY, double angle = 0)
    {
        this.x = x;
        this.y = y;
        radiusA = rX;
        radiusB = rY;
        this.angle = angle;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ellipse2D"/> class.
    /// </summary>
    /// <param name="center">Center Point of <see cref="Ellipse2D"/>.</param>
    /// <param name="a">Major radius of <see cref="Ellipse2D"/>.</param>
    /// <param name="b">Minor radius of <see cref="Ellipse2D"/>.</param>
    /// <param name="angle">Angle of <see cref="Ellipse2D"/>.</param>
    public Ellipse2D(Point2D center, double a, double b, double angle = 0)
    {
        x = center.X;
        y = center.Y;
        radiusA = a;
        radiusB = b;
        this.angle = angle;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ellipse2D"/> class.
    /// </summary>
    /// <param name="center">Center Point of <see cref="Ellipse2D"/>.</param>
    /// <param name="size">Major and Minor radii of <see cref="Ellipse2D"/>.</param>
    /// <param name="angle">Angle of <see cref="Ellipse2D"/>.</param>
    public Ellipse2D(Point2D center, Size2D size, double angle = 0)
        : this(center, size.Width, size.Height, angle)
    { }
    #endregion Constructors

    #region Deconstructors
    /// <summary>
    /// Deconstruct this <see cref="Ellipse2D"/> to a Tuple.
    /// </summary>
    /// <param name="cx"></param>
    /// <param name="cy"></param>
    /// <param name="rX"></param>
    /// <param name="rY"></param>
    /// <param name="angle"></param>
    public void Deconstruct(out double cx, out double cy, out double rX, out double rY, out double angle)
    {
        cx = x;
        cy = x;
        rX = radiusA;
        rY = radiusB;
        angle = this.angle;
    }
    #endregion Deconstructors

    #region Properties
    /// <summary>
    /// Gets or sets the <see cref="Location"/> of the <see cref="Ellipse2D"/>
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    public Point2D Location
    {
        get { return new Point2D(x - radiusA, y - radiusB); }
        set
        {
            x = value.X + radiusA;
            y = value.Y + radiusB;
            ClearCache();
            OnPropertyChanged(nameof(Location));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the Center Point of <see cref="Ellipse2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Elements")]
    [Description("The " + nameof(Center) + " location of the " + nameof(Ellipse2D) + ".")]
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
            ClearCache();
            OnPropertyChanged(nameof(Center));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the X coordinate location of the center of the circle.
    /// </summary>
    [XmlAttribute(nameof(x))]
    [Browsable(true)]
    [Category("Elements")]
    [Description("The center x coordinate location of the circle.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double X
    {
        get { return x; }
        set
        {
            x = value;
            ClearCache();
            OnPropertyChanged(nameof(X));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the Y coordinate location of the center of the circle.
    /// </summary>
    [XmlAttribute(nameof(y))]
    [Browsable(true)]
    [Category("Elements")]
    [Description("The center y coordinate location of the circle.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double Y
    {
        get { return y; }
        set
        {
            y = value;
            ClearCache();
            OnPropertyChanged(nameof(Y));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the first radius of <see cref="Ellipse2D"/>.
    /// </summary>
    [XmlAttribute("rx")]
    [Browsable(true)]
    [Category("Elements")]
    [Description("The first radius of the " + nameof(Ellipse2D) + ".")]
    [RefreshProperties(RefreshProperties.All)]
    public double RadiusA
    {
        get { return radiusA; }
        set
        {
            radiusA = value;
            ClearCache();
            OnPropertyChanged(nameof(RadiusA));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the second radius of Ellipse
    /// </summary>
    [XmlAttribute("ry")]
    [Browsable(true)]
    [Category("Elements")]
    [Description("The second radius of the " + nameof(Ellipse2D) + ".")]
    [RefreshProperties(RefreshProperties.All)]
    public double RadiusB
    {
        get { return radiusB; }
        set
        {
            radiusB = value;
            ClearCache();
            OnPropertyChanged(nameof(RadiusB));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets the Major radius of <see cref="Ellipse2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Elements")]
    [Description("The larger radius of the " + nameof(Ellipse2D) + ".")]
    public double MajorRadius => radiusA >= radiusB ? radiusA : radiusB;

    /// <summary>
    /// Gets the Minor radius of <see cref="Ellipse2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Elements")]
    [Description("The smaller radius of the " + nameof(Ellipse2D) + ".")]
    [RefreshProperties(RefreshProperties.All)]
    public double MinorRadius => radiusA <= radiusB ? radiusA : radiusB;

    /// <summary>
    /// Gets or sets the Aspect ratio of <see cref="Ellipse2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Properties")]
    [Description("The " + nameof(Aspect) + " ratio of the major and minor axis of the " + nameof(Ellipse2D) + ".")]
    [RefreshProperties(RefreshProperties.All)]
    public double Aspect
    {
        get { return Measurements.Aspect(radiusA, radiusB); }
        set
        {
            radiusB = radiusA * value;
            radiusA = radiusB / value;
            ClearCache();
            OnPropertyChanged(nameof(Aspect));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the Angle of the <see cref="Ellipse2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [GeometryAngleRadians]
    [Category("Elements")]
    [Description("The " + nameof(Angle) + " to rotate the " + nameof(Ellipse2D) + ".")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [TypeConverter(typeof(AngleConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public double Angle
    {
        get { return angle; }
        set
        {
            angle = value;
            ClearCache();
            OnPropertyChanged(nameof(Angle));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the Angle of the <see cref="Ellipse2D"/> in Degrees.
    /// </summary>
    [XmlAttribute(nameof(angle))]
    [Browsable(false)]
    [GeometryAngleDegrees]
    [Category("Elements")]
    [Description("The " + nameof(Angle) + " to rotate the " + nameof(Ellipse2D) + " in Degrees.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double AngleDegrees
    {
        get { return angle.RadiansToDegrees(); }
        set
        {
            angle = value.DegreesToRadians();
            ClearCache();
            OnPropertyChanged(nameof(AngleDegrees));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets the cos angle.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public double CosAngle => (double)CachingProperty(() => Cos(angle));

    /// <summary>
    /// Gets the sin angle.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public double SinAngle => (double)CachingProperty(() => Sin(angle));

    /// <summary>
    /// Gets the Focus Radius of the <see cref="Ellipse2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Properties")]
    [Description("The focus radius of the " + nameof(Ellipse2D) + ".")]
    public double FocusRadius => (double)CachingProperty(() => Measurements.EllipseFocusRadius(radiusA, radiusB));

    /// <summary>
    /// Gets the <see cref="Eccentricity"/> of the <see cref="Ellipse2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Properties")]
    [Description("The " + nameof(Eccentricity) + " of the " + nameof(Ellipse2D) + ".")]
    public double Eccentricity => (double)CachingProperty(() => Measurements.Eccentricity(radiusA, radiusB));

    /// <summary>
    /// Gets the <see cref="Perimeter"/> of the <see cref="Ellipse2D"/>.
    /// </summary>
    /// <returns></returns>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Properties")]
    [Description("The " + nameof(Perimeter) + " of the " + nameof(Ellipse2D) + ".")]
    public override double Perimeter => (double)CachingProperty(() => Measurements.EllipsePerimeter(radiusA, radiusB));

    /// <summary>
    /// Gets the <see cref="Area"/> of the <see cref="Ellipse2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Properties")]
    [Description("The " + nameof(Area) + " of the " + nameof(Ellipse2D) + ".")]
    public override double Area
    {
        get { return Measurements.EllipseArea(radiusA, radiusB); }
        set
        {
            // ToDo: Figure out the correct formula.
            var a = Aspect;
            radiusA = value * a / PI;
            radiusB = value * a / PI;
            ClearCache();
            OnPropertyChanged(nameof(Area));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets the angles of the extreme points of the rotated ellipse.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Properties")]
    [Description("The angles of the extreme points of the " + nameof(Ellipse2D) + ".")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(ExpandableCollectionConverter))]
    public List<double> ExtremeAngles => (List<double>)CachingProperty(() => Measurements.EllipseExtremeAngles(radiusA, radiusB, angle));

    /// <summary>
    /// Get the points of the Cartesian extremes of a rotated ellipse.
    /// </summary>
    /// <acknowledgment>
    /// Based roughly on the principles found at:
    /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
    /// </acknowledgment>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Properties")]
    [Description("The locations of the extreme points of the " + nameof(Ellipse2D) + ".")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(ExpandableCollectionConverter))]
    public List<Point2D> ExtremePoints => (List<Point2D>)CachingProperty(() => Measurements.EllipseExtremePoints(x, y, radiusA, radiusB, angle));

    /// <summary>
    /// Gets an sets the Bounding box of the <see cref="Ellipse2D"/>.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Properties")]
    [Description("The rectangular bounds of the " + nameof(Ellipse2D) + ".")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Rectangle2DConverter))]
    public override Rectangle2D Bounds
    {
        get { return Measurements.EllipseBounds(x, y, radiusA, radiusB, angle); }
        set
        {
            var bounds1 = Bounds;
            var aspect = Aspect;
            var bounds2 = value;

            var locDif = bounds1 is null
                ? bounds2 is null ? Vector2D.Empty : (Vector2D)bounds2.Location
                : bounds2 is null ? (Vector2D)bounds1.Location : bounds2.Location - bounds1.Location;
            //var scaleDif = bounds2.Size - bounds1.Size;
            Center += locDif;
            if (aspect > 1)
            {
                radiusA = radiusA / bounds1.Width * (bounds2?.Width).Value;
                radiusB = radiusB / bounds1.Height * bounds2.Height;
            }
            else
            {
                radiusB = radiusB / bounds1.Width * (bounds2?.Width).Value;
                radiusA = radiusA / bounds1.Height * bounds2.Height;
            }
            ClearCache();
            OnPropertyChanged(nameof(Bounds));
        }
    }

    /// <summary>
    /// Gets the size and location of the ellipse, in double-point pixels, relative to the parent canvas.
    /// </summary>
    /// <returns>A System.Drawing.RectangleF in double-point pixels relative to the parent canvas that represents the size and location of the segment.</returns>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Properties")]
    [Description("The orthogonal rectangular bounds of the " + nameof(Ellipse2D) + ".")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Rectangle2DConverter))]
    public Rectangle2D OrthogonalBounds => (Rectangle2D)CachingProperty(() => Measurements.EllipseBounds(x, y, radiusA, radiusB));

    /// <summary>
    /// Gets the <see cref="Ellipse2D"/> curve's conic polynomial representation.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Polynomial<double> ConicSectionCurve
    {
        get
        {
            var curveY = (Polynomial<double>)CachingProperty(() => (Polynomial<double>)EllipseConicSectionPolynomial(x, y, radiusA, radiusB, CosAngle, SinAngle));
            curveY.IsReadonly = true;
            return curveY;
        }
    }
    #endregion Properties

    #region Operators
    /// <summary>
    /// Implicit conversion from tuple.
    /// </summary>
    /// <returns></returns>
    /// <param name="tuple"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Ellipse2D((double X, double Y, double RX, double RY, double Angle) tuple) => new(tuple);
    #endregion Operators

    #region Interpolators
    /// <summary>
    /// The interpolate.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override Point2D Interpolate(double t) => Interpolators.Ellipse(t, x, y, radiusA, radiusB, angle);
    #endregion Interpolators

    #region Methods
    /// <summary>
    /// Creates a new <see cref="Ellipse2D"/> from a <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Ellipse2D FromValueTuple((double X, double Y, double RX, double RY, double Angle) tuple) => new(tuple);

    /// <summary>
    /// The contains.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Contains(Point2D point) => Intersections.Contains(this, point) != Inclusions.Outside;

    /// <summary>
    /// Creates a string representation of this <see cref="Ellipse2D"/> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ConvertToString(string format, IFormatProvider provider)
    {
        if (this is null)
        {
            return nameof(Ellipse2D);
        }

        var sep = Tokenizer.GetNumericListSeparator(provider);
        IFormattable formatable = $"{nameof(Ellipse2D)}{{{nameof(Center)}={Center}{sep}{nameof(RadiusA)}={radiusA}{sep}{nameof(RadiusB)}={radiusB}{sep}{nameof(Angle)}={angle}}}";
        return formatable.ToString(format, provider);
    }
    #endregion Methods
}
