// <copyright file="Circle2D.cs" company="Shkyrockett" >
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Polynomials;
using static System.Math;

namespace Engine;

/// <summary>
/// The circle struct.
/// </summary>
[GraphicsObject]
[DataContract, Serializable]
[DisplayName(nameof(Circle2D))]
[XmlType(TypeName = "circle", Namespace = "shape")]
[TypeConverter(typeof(StructConverter<Circle2D>))]
[DebuggerDisplay("{ToString()}")]
public class Circle2D
    : Shape2D
{
    #region Implementations
    /// <summary>
    /// The empty.
    /// </summary>
    public static Circle2D Empty = new(0d, 0d, 0d);
    #endregion

    #region Private Fields
    /// <summary>
    /// The center x coordinate point of the circle.
    /// </summary>
    private double x;

    /// <summary>
    /// The center y coordinate point of the circle.
    /// </summary>
    private double y;

    /// <summary>
    /// The radius of the circle.
    /// </summary>
    private double radius;
    #endregion Private Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Circle2D"/> class.
    /// </summary>
    public Circle2D()
        : this(0, 0, 0)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Circle2D"/> class.
    /// </summary>
    /// <param name="tuple"></param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Circle2D((double X, double Y, double Radius) tuple)
        : this(tuple.X, tuple.Y, tuple.Radius)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Circle2D"/> class.
    /// </summary>
    /// <param name="circle"></param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Circle2D(Circle2D circle)
        : this(circle.X, circle.Y, circle.Radius)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Circle2D"/> class.
    /// </summary>
    /// <param name="x">The center x coordinate point of the circle.</param>
    /// <param name="y">The center y coordinate point of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Circle2D(double x, double y, double radius)
    //: this()
    {
        this.x = x;
        this.y = y;
        this.radius = radius;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Circle2D"/> class.
    /// </summary>
    /// <param name="center">The center point of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    public Circle2D(Point2D center, double radius)
        : this(center.X, center.Y, radius)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Circle2D"/> class.
    /// </summary>
    /// <param name="bounds">The bounding box of the circle.</param>
    public Circle2D(Rectangle2D bounds)
        : this()
    {
        x = bounds.Center.X;
        y = bounds.Center.Y;
        radius = bounds.Height <= bounds.Width ? bounds.Height * 0.25d : bounds.Width * 0.25d;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Circle2D"/> class.
    /// </summary>
    /// <param name="triangle">The triangle.</param>
    public Circle2D(Triangle2D triangle)
        : this(triangle.A, triangle.B, triangle.C)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Circle2D"/> class.
    /// </summary>
    /// <param name="PointA">The PointA.</param>
    /// <param name="PointB">The PointB.</param>
    /// <param name="PointC">The PointC.</param>
    public Circle2D(Point2D PointA, Point2D PointB, Point2D PointC)
        : this()
    {
        //  Calculate the slopes of the lines.
        var slopeA = Measurements.Slope(PointA, PointB);
        var slopeB = Measurements.Slope(PointC, PointB);
        var f = new Vector2D((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X)),
            (((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X)));

        // Find the center.
        x = f.I - (slopeB * ((f.I - f.J) / (slopeB - slopeA)));
        y = (f.I - f.J) / (slopeB - slopeA);

        // Get the radius.
        radius = Measurements.Distance(x, y, PointA.X, PointA.Y);
    }
    #endregion

    #region Deconstructors
    /// <summary>
    /// Deconstruct this <see cref="Circle2D"/> to a <see cref="ValueTuple{T1, T2, T3}"/>.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="radius"></param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out double x, out double y, out double radius)
    {
        x = this.x;
        y = this.y;
        radius = this.radius;
    }
    #endregion Deconstructors

    #region Properties
    /// <summary>
    /// Gets or sets the center point of the circle.
    /// </summary>
    /// <value>
    /// The center.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    //[DisplayName(nameof(Center))]
    [Category("Elements")]
    [Description("The center location of the circle.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [TypeConverter(typeof(Point2DConverter))]
    [RefreshProperties(RefreshProperties.All)]
    [NotifyParentProperty(true)]
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
    [DataMember(Name = nameof(X)), XmlAttribute(nameof(X)), SoapAttribute(nameof(X))]
    [Browsable(false)]
    [Category("Elements")]
    [Description("The center x coordinate location of the circle.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    [NotifyParentProperty(true)]
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
    [DataMember(Name = nameof(Y)), XmlAttribute(nameof(Y)), SoapAttribute(nameof(Y))]
    [Browsable(false)]
    [Category("Elements")]
    [Description("The center y coordinate location of the circle.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    [NotifyParentProperty(true)]
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
    /// Gets or sets the radius of the circle.
    /// </summary>
    [DataMember(Name = nameof(Radius)), XmlAttribute(nameof(Radius)), SoapAttribute(nameof(Radius))]
    //[DisplayName(nameof(Radius))]
    [Category("Elements")]
    [Description("The radius of the circle.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [RefreshProperties(RefreshProperties.All)]
    [NotifyParentProperty(true)]
    public double Radius
    {
        get { return radius; }
        set
        {
            radius = value;
            ClearCache();
            OnPropertyChanged(nameof(Radius));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets the circumference.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Category("Properties")]
    [Description("The distance around the circle.")]
    public double Circumference => (double)CachingProperty(() => Measurements.CircleCircumference(radius));

    /// <summary>
    /// Gets the perimeter.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Category("Properties")]
    [Description("The distance around the circle.")]
    public override double Perimeter => Circumference;

    /// <summary>
    /// Gets or sets the area.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Category("Properties")]
    [Description("The area of the circle.")]
    [RefreshProperties(RefreshProperties.All)]
    [NotifyParentProperty(true)]
    public override double Area
    {
        get { return Measurements.CircleArea(radius); }
        set
        {
            radius = Sqrt(value / PI);
            ClearCache();
            OnPropertyChanged(nameof(Area));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets the angles of the extreme points of the circle.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Properties")]
    [Description("The angles of the extreme points of the " + nameof(Circle2D) + ".")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(ExpandableCollectionConverter))]
    public List<double> ExtremeAngles => (List<double>)CachingProperty(() => Measurements.CircleExtremeAngles());

    /// <summary>
    /// Get the points of the Cartesian extremes of the circle.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    [Category("Properties")]
    [Description("The locations of the extreme points of the " + nameof(Circle2D) + ".")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(ExpandableCollectionConverter))]
    public List<Point2D> ExtremePoints => (List<Point2D>)CachingProperty(() => Measurements.CircleExtremePoints(x, y, radius));

    /// <summary>
    /// Gets or sets the rectangular boundaries of the circle.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [ReadOnly(true)]
    [Category("Properties")]
    [Description("The rectangular boundaries of the circle.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Rectangle2DConverter))]
    [RefreshProperties(RefreshProperties.All)]
    [NotifyParentProperty(true)]
    public override Rectangle2D Bounds
    {
        get { return Measurements.CircleBounds(x, y, radius); }
        set
        {
            Center = value.Center();
            radius = value.Width <= value.Height ? value.Width : value.Height;
            ClearCache();
            OnPropertyChanged(nameof(Bounds));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets the <see cref="Circle2D"/> curve's conic polynomial representation.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Polynomial<double> ConicSectionCurve
    {
        get
        {
            var curveY = (Polynomial<double>)CachingProperty(() => (Polynomial<double>)CircleConicSectionPolynomial(x, y, radius));
            curveY.IsReadonly = true;
            return curveY;
        }
    }
    #endregion

    #region Operators
    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator ==(Circle2D left, Circle2D right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator !=(Circle2D left, Circle2D right) => !(left == right);

    /// <summary>
    /// Implicit conversion from tuple.
    /// </summary>
    /// <returns></returns>
    /// <param name="tuple"></param>
    public static implicit operator Circle2D((double X, double Y, double Radius) tuple) => new(tuple);
    #endregion

    #region Operator Backing Methods
    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals([AllowNull] object obj) => obj is Circle2D o && Equals(o);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Equals([AllowNull] Circle2D other) => other is Circle2D o && X == o.X && Y == o.Y && Radius == o.Radius;
    #endregion

    #region Interpolators
    /// <summary>
    /// Interpolates the circle.
    /// </summary>
    /// <param name="t">Index of the point to interpolate.</param>
    /// <returns>Returns the interpolated point of the index value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override Point2D Interpolate(double t) => Interpolators.UnitCircle(t, x, y, radius);
    #endregion Interpolators

    #region Static Creation Methods
    /// <summary>
    /// The from center and radius.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="radius">The radius.</param>
    /// <returns>The <see cref="Circle2D"/>.</returns>
    public static Circle2D FromCenterAndRadius(Point2D point, double radius) => new(point, radius);

    /// <summary>
    /// The from three points.
    /// </summary>
    /// <param name="pointA">The pointA.</param>
    /// <param name="pointB">The pointB.</param>
    /// <param name="pointC">The pointC.</param>
    /// <returns>The <see cref="Circle2D"/>.</returns>
    public static Circle2D FromThreePoints(Point2D pointA, Point2D pointB, Point2D pointC) => new(pointA, pointB, pointC);

    /// <summary>
    /// The from triangle.
    /// </summary>
    /// <param name="triangle">The triangle.</param>
    /// <returns>The <see cref="Circle2D"/>.</returns>
    public static Circle2D FromTriangle(Triangle2D triangle) => new(triangle.A, triangle.B, triangle.C);

    /// <summary>
    /// The from rectangle.
    /// </summary>
    /// <param name="rectangle">The rectangle.</param>
    /// <returns>The <see cref="Circle2D"/>.</returns>
    public static Circle2D FromRectangle(Rectangle2D rectangle) => new(rectangle);
    #endregion

    #region Standard Methods
    /// <summary>
    /// Get the hash code.
    /// </summary>
    /// <returns>The <see cref="int"/>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override int GetHashCode() => HashCode.Combine(x, y, radius);

    /// <summary>
    /// Creates a <see cref="string"/> representation of this <see cref="IBoundable"/> interface based on the current culture.
    /// </summary>
    /// <returns>A <see cref="string"/> representation of this instance of the <see cref="IBoundable"/> object.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="Circle2D"/> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(string format, IFormatProvider formatProvider)
    {
        if (this == null)
        {
            return nameof(Circle2D);
        }

        var sep = Tokenizer.GetNumericListSeparator(formatProvider);
        return $"{nameof(Circle2D)}({nameof(X)}: {X.ToString(format, formatProvider)}{sep} {nameof(Y)}: {Y.ToString(format, formatProvider)}{sep} {nameof(Radius)}: {Radius.ToString(format, formatProvider)})";
    }
    #endregion
}

