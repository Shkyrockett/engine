﻿// <copyright file="Rectangle2D.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
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
using static System.Math;

namespace Engine;

/// <summary>
/// The rectangle2d class.
/// </summary>
/// <seealso cref="Engine.Shape2D" />
[DataContract, Serializable]
[GraphicsObject]
[DisplayName(nameof(Rectangle2D))]
[XmlType(TypeName = "rect", Namespace = "http://www.w3.org/2000/svg")]
[DebuggerDisplay("{ToString()}")]
public class Rectangle2D
    : Shape2D
{
    #region Implementations
    /// <summary>
    /// The empty (readonly). Value: new Rectangle2D().
    /// </summary>
    public static readonly Rectangle2D Empty = new(0, 0, 0, 0);

    /// <summary>
    /// The unit (readonly). Value: new Rectangle2D(0, 0, 1, 1).
    /// </summary>
    public static readonly Rectangle2D Unit = new(0, 0, 1, 1);
    #endregion Implementations

    #region Fields
    /// <summary>
    /// The X coordinate location of the rectangle.
    /// </summary>
    private double x;

    /// <summary>
    /// The Y coordinate location of the rectangle.
    /// </summary>
    private double y;

    /// <summary>
    /// The width of the rectangle.
    /// </summary>
    private double width;

    /// <summary>
    /// The height of the rectangle.
    /// </summary>
    private double height;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new default instance of the <see cref="Rectangle2D" /> class.
    /// </summary>
    public Rectangle2D()
        : this(0d, 0d, 0d, 0d)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle2D" /> class with an empty location, with the provided size.
    /// </summary>
    /// <param name="size">The height and width of the <see cref="Rectangle2D" />.</param>
    public Rectangle2D(Size2D size)
        : this(0d, 0d, size.Width, size.Height)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle2D" /> class with an initial location and size.
    /// </summary>
    /// <param name="rectangle">The rectangle to clone.</param>
    public Rectangle2D(Rectangle2D rectangle)
        : this(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle2D" /> class with an initial location and size.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="size">The size.</param>
    public Rectangle2D(Point2D location, Size2D size)
        : this(location.X, location.Y, size.Width, size.Height)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle2D" /> class  with a location and a vector size.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="vector">The vector.</param>
    public Rectangle2D(Point2D point, Vector2D vector)
        : this(point, point + vector)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle2D" /> class with the location and size from a tuple.
    /// </summary>
    /// <param name="tuple1">The tuple1.</param>
    /// <param name="tuple2">The tuple2.</param>
    public Rectangle2D((double, double) tuple1, (double, double) tuple2)
    {
        (x, y) = tuple1;
        (width, height) = tuple2;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle2D" /> class with the location and size from a tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    public Rectangle2D((double, double, double, double) tuple)
        : this()
    {
        (x, y, width, height) = tuple;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle2D" /> class with a location and size.
    /// </summary>
    /// <param name="x">The x coordinate of the upper left corner of the rectangle.</param>
    /// <param name="y">The y coordinate of the upper left corner of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The Height of the rectangle.</param>
    public Rectangle2D(double x, double y, double width, double height)
    {
        //if (width < 0 || height < 0)
        //    throw new ArgumentException("Width and Height cannot be Negative.");
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle2D" /> class with the upper left and lower right corners.
    /// </summary>
    /// <param name="point1">The point1.</param>
    /// <param name="point2">The point2.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Rectangle2D(Point2D point1, Point2D point2)
        : this()
    {
        x = Min(point1.X, point2.X);
        y = Min(point1.Y, point2.Y);

        //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)
        width = Max(Max(point1.X, point2.X) - x, 0);
        height = Max(Max(point1.Y, point2.Y) - y, 0);
    }
    #endregion Constructors

    #region Deconstructors
    /// <summary>
    /// The deconstruct.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="top">The top.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public void Deconstruct(out double left, out double top, out double width, out double height) => (left, top, width, height) = (x, y, this.width, this.height);
    #endregion Deconstructors

    #region Properties
    /// <summary>
    /// Gets or sets the X coordinate location of the rectangle.
    /// </summary>
    /// <value>
    /// The x.
    /// </value>
    [XmlAttribute(nameof(x))]
    [Browsable(true)]
    //[DisplayName(nameof(X))]
    [Category("Elements")]
    [Description("The x coordinate location of the rectangle.")]
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
    /// Gets or sets the Y coordinate location of the rectangle.
    /// </summary>
    /// <value>
    /// The y.
    /// </value>
    [XmlAttribute(nameof(y))]
    [Browsable(true)]
    //[DisplayName(nameof(Y))]
    [Category("Elements")]
    [Description("The y coordinate location of the rectangle.")]
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
    /// Gets or sets the height of the rectangle.
    /// </summary>
    /// <value>
    /// The height.
    /// </value>
    [XmlAttribute("v")]
    [Browsable(true)]
    //[DisplayName(nameof(Height))]
    [Category("Elements")]
    [Description("The height of the rectangle.")]
    [RefreshProperties(RefreshProperties.All)]
    public double Height
    {
        get { return height; }
        set
        {
            height = value;
            ClearCache();
            OnPropertyChanged(nameof(Height));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the width of the rectangle.
    /// </summary>
    /// <value>
    /// The width.
    /// </value>
    [XmlAttribute("h")]
    [Browsable(true)]
    //[DisplayName(nameof(Width))]
    [Category("Elements")]
    [Description("The width of the rectangle.")]
    [RefreshProperties(RefreshProperties.All)]
    public double Width
    {
        get { return width; }
        set
        {
            width = value;
            ClearCache();
            OnPropertyChanged(nameof(Width));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the Aspect ratio of the rectangle.
    /// </summary>
    /// <value>
    /// The aspect.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    //[DisplayName(nameof(Aspect))]
    [Category("Properties")]
    [Description("The " + nameof(Aspect) + " ratio of the height and width of the " + nameof(RotatedRectangle2D) + ".")]
    [RefreshProperties(RefreshProperties.All)]
    public double Aspect
    {
        get { return height / width; }
        set
        {
            height = width * value;
            width = height / value;
            ClearCache();
            OnPropertyChanged(nameof(Aspect));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the angle the rectangle should be rotated.
    /// </summary>
    /// <value>
    /// The location.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[DisplayName(nameof(Location))]
    [Category("Elements")]
    [Description("The top left location of the rectangle.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [TypeConverter(typeof(Point2DConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public Point2D Location
    {
        get { return new Point2D(X, Y); }
        set
        {
            (x, y) = (value.X, value.Y);
            ClearCache();
            OnPropertyChanged(nameof(Location));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the center point X and Y coordinates of the rectangle.
    /// </summary>
    /// <value>
    /// The center.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[DisplayName(nameof(Center))]
    [Category("Elements")]
    [Description("The center location of the rectangle.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [TypeConverter(typeof(Point2DConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public Point2D Center
    {
        get { return new Point2D(X + (width * 0.5d), Y + (height * 0.5d)); }
        set
        {
            (x, y) = (value.X - (width * 0.5d), value.Y - (height * 0.5d));
            ClearCache();
            OnPropertyChanged(nameof(Center));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the height and width of the rectangle.
    /// </summary>
    /// <value>
    /// The size.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[DisplayName(nameof(Size))]
    [Category("Elements")]
    [Description("The height and width of the rectangle.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    //[TypeConverter(typeof(Size2DConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public Size2D Size
    {
        get { return new Size2D(width, height); }
        set
        {
            (width, height) = (value.Width, value.Height);
            ClearCache();
            OnPropertyChanged(nameof(Size));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the top left.
    /// </summary>
    /// <value>
    /// The top left.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[DisplayName(nameof(TopLeft))]
    [Category("Elements")]
    [Description("The top left location of the rectangle.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public Point2D TopLeft
    {
        get { return new Point2D(Left, Top); }
        set
        {
            (x, y, width, height) = (value.X, value.Y, width + x - value.X, height + y - value.Y);
            ClearCache();
            OnPropertyChanged(nameof(TopLeft));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the top right.
    /// </summary>
    /// <value>
    /// The top right.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[DisplayName(nameof(TopRight))]
    [Category("Elements")]
    [Description("The top right location of the rectangle.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public Point2D TopRight
    {
        get { return new Point2D(Right, Top); }
        set
        {
            (y, width, height) = (value.Y, value.X - x, height + y - value.Y);
            ClearCache();
            OnPropertyChanged(nameof(TopRight));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the bottom left.
    /// </summary>
    /// <value>
    /// The bottom left.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[DisplayName(nameof(BottomLeft))]
    [Category("Elements")]
    [Description("The bottom left location of the rectangle.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public Point2D BottomLeft
    {
        get { return new Point2D(Left, Bottom); }
        set
        {
            (Left, Bottom) = (value.X, value.Y);
            ClearCache();
            OnPropertyChanged(nameof(BottomLeft));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the bottom right.
    /// </summary>
    /// <value>
    /// The bottom right.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[DisplayName(nameof(BottomRight))]
    [Category("Elements")]
    [Description("The bottom right location of the rectangle.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public Point2D BottomRight
    {
        get { return new Point2D(Right, Bottom); }
        set
        {
            (Right, Bottom) = (value.X, value.Y);
            ClearCache();
            OnPropertyChanged(nameof(BottomRight));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the left.
    /// </summary>
    /// <value>
    /// The left.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[DisplayName(nameof(Left))]
    [Category("Elements")]
    [Description("The left location of the rectangle.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double Left
    {
        get { return x; }
        set
        {
            width += x - value;
            x = value;
            ClearCache();
            OnPropertyChanged(nameof(Left));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the top.
    /// </summary>
    /// <value>
    /// The top.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[DisplayName(nameof(Top))]
    [Category("Elements")]
    [Description("The top location of the rectangle.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double Top
    {
        get { return y; }
        set
        {
            height += y - value;
            y = value;
            ClearCache();
            OnPropertyChanged(nameof(Top));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the right.
    /// </summary>
    /// <value>
    /// The right.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[DisplayName(nameof(Right))]
    [Category("Elements")]
    [Description("The right location of the rectangle.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double Right
    {
        get { return x + width; }
        set
        {
            width = value - x;
            ClearCache();
            OnPropertyChanged(nameof(Right));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the bottom.
    /// </summary>
    /// <value>
    /// The bottom.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[DisplayName(nameof(Bottom))]
    [Category("Elements")]
    [Description("The bottom location of the rectangle.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RefreshProperties(RefreshProperties.All)]
    public double Bottom
    {
        get { return y + height; }
        set
        {
            height = value - y;
            ClearCache();
            OnPropertyChanged(nameof(Bottom));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets a value indicating whether
    /// </summary>
    /// <value>
    ///   <see langword="true"/> if this instance is empty; otherwise, <see langword="false"/>.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[ReadOnly(true)]
    //[DisplayName(nameof(IsEmpty))]
    [Category("Elements")]
    [Description("A value indicating whether or not the rectangle has height or width.")]
    public bool IsEmpty => (Width <= 0) || (Height <= 0);

    /// <summary>
    /// Gets a value indicating whether the Rectangle2D has area.
    /// </summary>
    /// <value>
    ///   <see langword="true"/> if this instance has area; otherwise, <see langword="false"/>.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[ReadOnly(true)]
    //[DisplayName(nameof(HasArea))]
    [Category("Elements")]
    [Description("A value indicating whether or not the rectangle has height or width.")]
    public bool HasArea => width > 0 && height > 0;

    /// <summary>
    /// Gets the area.
    /// </summary>
    /// <value>
    /// The area.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    //[ReadOnly(true)]
    //[DisplayName(nameof(Area))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Category("Elements")]
    [Description("The area of the rectangle.")]
    public override double Area => height * width;

    /// <summary>
    /// Gets the length of the perimeter of the rectangle.
    /// </summary>
    /// <value>
    /// The perimeter.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(true)]
    //[ReadOnly(true)]
    //[DisplayName(nameof(Perimeter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Category("Elements")]
    [Description("The distance around the rectangle.")]
    public override double Perimeter => (TopLeft.Distance(TopRight) * 2) + (TopLeft.Distance(BottomLeft) * 2);

    /// <summary>
    /// Gets the bounding box of the rectangle.
    /// </summary>
    /// <value>
    /// The bounds.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    //[ReadOnly(true)]
    //[DisplayName(nameof(Bounds))]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [TypeConverter(typeof(Rectangle2DConverter))]
    [RefreshProperties(RefreshProperties.All)]
    [Category("Elements")]
    [Description("bounding box of the rectangle.")]
    public override Rectangle2D Bounds => this;
    #endregion Properties

    #region Operators
    /// <summary>
    /// Tests whether two <see cref="Rectangle2D" /> objects have equal location and size.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator ==(Rectangle2D left, Rectangle2D right) => Equals(left, right);

    /// <summary>
    /// Tests whether two <see cref="Rectangle2D" /> objects differ in location or size.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator !=(Rectangle2D left, Rectangle2D right) => !Equals(left, right);

    /// <summary>
    /// Compares two <see cref="RotatedRectangle2D" />.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Compare(Rectangle2D left, Rectangle2D right) => Equals(left, right);

    /// <summary>
    /// Tests whether <paramref name="left" /> is a <see cref="RotatedRectangle2D" /> with the same location and size of <paramref name="right" />.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Equals(Rectangle2D left, Rectangle2D right) => left?.X == right?.X && left?.Y == right?.Y && left?.Width == right?.Width && left?.Height == right?.Height;

    /// <summary>
    /// Tests whether <paramref name="obj" /> is a <see cref="Rectangle2D" /> with the same location and size of this <see cref="Rectangle2D" />.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object obj) => obj is Rectangle2D && Equals(this, obj as Rectangle2D);

    /// <summary>
    /// Implicit conversion from tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator Rectangle2D((double Left, double Top, double Width, double Height) tuple) => new(tuple);
    #endregion Operators

    #region Factories
    /// <summary>
    /// Creates a new <see cref="Rectangle2D" /> with the specified location and size.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="top">The top.</param>
    /// <param name="right">The right.</param>
    /// <param name="bottom">The bottom.</param>
    /// <returns></returns>
    public static Rectangle2D FromLTRB(double left, double top, double right, double bottom) => new(left, top, right - left, bottom - top);

    /// <summary>
    /// Creates a <see cref="Rectangle2D" /> from a center point and it's size.
    /// </summary>
    /// <param name="center">The center point to create the <see cref="Rectangle2D" /> as a <see cref="Point2D" />.</param>
    /// <param name="size">The height and width of the new <see cref="Rectangle2D" /> as a <see cref="Size" />.</param>
    /// <returns>
    /// Returns a <see cref="Rectangle2D" /> based around a center point and it's size.
    /// </returns>
    public static Rectangle2D RectangleFromCenter(Point2D center, Size2D size) => new(center - size.Inflate(0.5d), size);

    /// <summary>
    /// Creates a rectangle that represents the union between a and b.
    /// </summary>
    /// <param name="a">a.</param>
    /// <param name="b">The b.</param>
    /// <returns></returns>
    public static Rectangle2D Union(Rectangle2D a, Rectangle2D b)
    {
        var left = Min((a?.X).Value, (b?.X).Value);
        var top = Min(a.Y, b.Y);
        var x2 = Max(a.X + a.Width, b.X + b.Width);
        var y2 = Max(a.Y + a.Height, b.Y + b.Height);

        return new Rectangle2D(left, top, x2 - left, y2 - top);
    }

    /// <summary>
    /// Union - Return the result of the union of Rectangle2D and point.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="point">The point.</param>
    /// <returns></returns>
    public static Rectangle2D Union(Rectangle2D rect, Point2D point)
    {
        rect?.UnionMutate(new Rectangle2D(point, point));
        return rect;
    }

    /// <summary>
    /// Creates a rectangle that represents the intersection between a and b. If there is no intersection, null is returned.
    /// </summary>
    /// <param name="a">a.</param>
    /// <param name="b">The b.</param>
    /// <returns></returns>
    public static Rectangle2D Intersect(Rectangle2D a, Rectangle2D b)
    {
        var x1 = Max((a?.X).Value, (b?.X).Value);
        var x2 = Min(a.X + a.Width, b.X + b.Width);
        var y1 = Max(a.Y, b.Y);
        var y2 = Min(a.Y + a.Height, b.Y + b.Height);

        if (x2 >= x1 && y2 >= y1)
        {
            return new Rectangle2D(x1, y1, x2 - x1, y2 - y1);
        }

        return Empty;
    }

    /// <summary>
    /// Offset - return the result of offsetting Rectangle2D by the offset provided
    /// If this is Empty, this method is illegal.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="offsetVector">The offset vector.</param>
    /// <returns></returns>
    public static Rectangle2D Offset(Rectangle2D rect, Vector2D offsetVector)
    {
        rect?.Offset(offsetVector.I, offsetVector.J);
        return rect;
    }

    /// <summary>
    /// Offset - return the result of offsetting Rectangle2D by the offset provided
    /// If this is Empty, this method is illegal.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="offsetX">The offset x.</param>
    /// <param name="offsetY">The offset y.</param>
    /// <returns></returns>
    public static Rectangle2D Offset(Rectangle2D rect, double offsetX, double offsetY)
    {
        rect?.Offset(offsetX, offsetY);
        return rect;
    }

    /// <summary>
    /// Creates a <see cref="Rectangle2D" /> that is inflated by the specified amount.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns></returns>
    public static Rectangle2D Inflate(Rectangle2D rect, float x, float y)
    {
        var r = rect;
        r?.Inflate(x, y);
        return r;
    }

    /// <summary>
    /// Inflate - return the result of inflating Rectangle2D by the size provided, in all directions
    /// If this is Empty, this method is illegal.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="size">The size.</param>
    /// <returns></returns>
    public static Rectangle2D Inflate(Rectangle2D rect, Size2D size)
    {
        rect?.Inflate(size.Width, size.Height);
        return rect;
    }

    /// <summary>
    /// Inflate - return the result of inflating Rectangle2D by the size provided, in all directions
    /// If this is Empty, this method is illegal.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns></returns>
    public static Rectangle2D Inflate(Rectangle2D rect, double width, double height)
    {
        rect?.Inflate(width, height);
        return rect;
    }

    ///// <summary>
    ///// Returns the bounds of the transformed rectangle.
    ///// The Empty Rectangle2D is not affected by this call.
    ///// </summary>
    ///// <param name="rect">The Rectangle2D to transform.</param>
    ///// <param name="matrix">The Matrix by which to transform.</param>
    ///// <returns>
    ///// The Rectangle2D which results from the transformation.
    ///// </returns>
    ///// <exception cref="ArgumentNullException">rect</exception>
    //public static Rectangle2D Transform(Rectangle2D rect, Matrix3x2D matrix)
    //{
    //    if (rect is null)
    //    {
    //        throw new ArgumentNullException(nameof(rect));
    //    }

    //    Matrix3x2D.TransformRect(ref rect, ref matrix);
    //    return rect;
    //}
    #endregion Factories

    #region Mutators
    /// <summary>
    /// Union - Update this rectangle to be the union of this and Rectangle2D.
    /// </summary>
    /// <param name="rect">The rect.</param>
    public void UnionMutate(Rectangle2D rect)
    {
        var left = rect is null ? Left : Min(Left, rect.Left);
        var top = rect is null ? Top : Min(Top, rect.Top);

        if (rect is not null)
        {
            // We need this check so that the math does not result in NaN
            if (double.IsPositiveInfinity(rect.Width) || double.IsPositiveInfinity(Width))
            {
                width = double.PositiveInfinity;
            }
            else
            {
                //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)
                width = Max(Max(Right, rect.Right) - left, 0);
            }

            // We need this check so that the math does not result in NaN
            if (double.IsPositiveInfinity(rect.Height) || double.IsPositiveInfinity(Height))
            {
                height = double.PositiveInfinity;
            }
            else
            {
                //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)
                height = Max(Max(Bottom, rect.Bottom) - top, 0);
            }
        }

        x = left;
        y = top;
        OnPropertyChanged(nameof(Union));
    }

    /// <summary>
    /// Return a rectangle that is a union of this and a supplied Rectangle2D.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <returns></returns>
    public Rectangle2D Union(Rectangle2D rect)
    {
        var left = rect is null ? Left : Min(Left, rect.Left);
        var top = rect is null ? Top : Min(Top, rect.Top);
        var width = this.width;
        var height = this.width;

        if (rect is not null)
        {
            // We need this check so that the math does not result in NaN
            if (double.IsPositiveInfinity(rect.Width) || double.IsPositiveInfinity(Width))
            {
                width = double.PositiveInfinity;
            }
            else
            {
                //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)
                var maxRight = Max(Right, rect.Right);
                width = Max(maxRight - left, 0);
            }

            // We need this check so that the math does not result in NaN
            if (rect is null || double.IsPositiveInfinity(rect.Height) || double.IsPositiveInfinity(Height))
            {
                height = double.PositiveInfinity;
            }
            else
            {
                //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)
                var maxBottom = Max(Bottom, rect.Bottom);
                height = Max(maxBottom - top, 0);
            }
        }

        return new Rectangle2D(left, top, width, height);
    }

    /// <summary>
    /// Union - Update this rectangle to be the union of this and point.
    /// </summary>
    /// <param name="point">The point.</param>
    public void UnionMutate(Point2D point) => UnionMutate(new Rectangle2D(point, point));

    /// <summary>
    /// Return a rectangle that is a union of this and a supplied Point2D.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns></returns>
    public Rectangle2D Union(Point2D point) => Union(new Rectangle2D(point, point));

    /// <summary>
    /// Creates a Rectangle that represents the intersection between this Rectangle and rectangle.
    /// </summary>
    /// <param name="rect">The rect.</param>
    public void Intersect(Rectangle2D rect)
    {
        var result = Intersect(rect, this);

        x = result.X;
        y = result.Y;
        width = result.Width;
        height = result.Height;
        OnPropertyChanged(nameof(Intersect));
    }

    /// <summary>
    /// Adjusts the location of this rectangle by the specified amount.
    /// </summary>
    /// <param name="pos">The position.</param>
    public void Offset(Point2D pos) => Offset(pos.X, pos.Y);

    /// <summary>
    /// Offset - translate the Location by the offset provided.
    /// If this is Empty, this method is illegal.
    /// </summary>
    /// <param name="offsetVector">The offset vector.</param>
    public void Offset(Vector2D offsetVector) => Offset(offsetVector.I, offsetVector.J);

    /// <summary>
    /// Adjusts the location of this rectangle by the specified amount.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <exception cref="InvalidOperationException">Cannot call method.</exception>
    public void Offset(double x, double y)
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Cannot call method.");
        }

        this.x += x;
        this.y += y;
        OnPropertyChanged(nameof(Offset));
    }

    /// <summary>
    /// Inflates this <see cref="Rectangle2D" /> by the specified amount.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    public void Inflate(double x, double y)
    {
        this.x -= x;
        this.y -= y;
        width += 2 * x;
        height += 2 * y;
        OnPropertyChanged(nameof(Inflate));
    }

    /// <summary>
    /// Inflates this <see cref="Rectangle2D" /> by the specified amount.
    /// </summary>
    /// <param name="size">The size.</param>
    public void Inflate(Size2D size) => Inflate(size.Width, size.Height);

    ///// <summary>
    ///// Updates rectangle to be the bounds of the original value transformed
    ///// by the matrix.
    ///// The Empty Rectangle2D is not affected by this call.
    ///// </summary>
    ///// <param name="matrix">Matrix</param>
    //public void Transform(Matrix3x2D matrix) => Matrix3x2D.TransformRect(ref this, ref matrix);
    #endregion Mutators

    #region Methods
    /// <summary>
    /// The contains.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    public override bool Contains(Point2D point) => Intersections.Contains(this, point) != Inclusions.Outside;

    /// <summary>
    /// Determines if the rectangular region represented by <paramref name="rect" /> is entirely contained within the rectangular region represented by  this <see cref="Rectangle2D" /> .
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <returns>
    ///   <see langword="true"/> if [contains] [the specified rect]; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Contains(Rectangle2D rect) => Intersections.Contains(this, rect);

    /// <summary>
    /// Determines if this rectangle interests with another rectangle.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <returns></returns>
    public bool IntersectsWith(Rectangle2D rect) => Intersections.Intersects(this, rect);

    /// <summary>
    /// The intersects.
    /// </summary>
    /// <param name="shape">The shape.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    public bool Intersects(Shape2D shape) => Intersections.Intersects(this, shape);

    /// <summary>
    /// Convert a rectangle to an array of it's corner points.
    /// </summary>
    /// <returns>
    /// An array of points representing the corners of a rectangle.
    /// </returns>
    public List<Point2D> ToPoints() =>
    [
            Location,
            new Point2D(Right, Top),
            new Point2D(Right, Bottom),
            new Point2D(Left, Bottom)
        ];

    /// <summary>
    /// Gets the hash code for this <see cref="Rectangle2D" />.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    /// <summary>
    /// Convert a rectangle to a polygon containing an array of the rectangle's corner points.
    /// </summary>
    /// <returns>
    /// An array of points representing the corners of a rectangle.
    /// </returns>
    public PolygonContour2D ToPolygon() => new(ToPoints());

    /// <summary>
    /// Creates a string representation of this <see cref="Rectangle2D" /> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    public override string ConvertToString(string format, IFormatProvider provider)
    {
        var sep = Tokenizer.GetNumericListSeparator(provider);
        return this is null
            ? nameof(Rectangle2D)
            : $"{nameof(Rectangle2D)}{{{nameof(X)}={x.ToString(format, provider)}{sep}{nameof(Y)}={y.ToString(format, provider)}{sep}{nameof(Width)}={width.ToString(format, provider)}{sep}{nameof(Height)}={height.ToString(format, provider)}}}";
    }
    #endregion Methods
}
