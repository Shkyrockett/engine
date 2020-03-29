// <copyright file="Rectangle2D.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The rectangle2d struct.
    /// </summary>
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="Engine.IPropertyCaching" />
    /// <seealso cref="System.IEquatable{Engine.Rectangle2D}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Rectangle2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Rectangle2D
        : IClosedShape, IPropertyCaching, IEquatable<Rectangle2D>
    {
        #region Implementations
        /// <summary>
        /// The empty (readonly). Value: new Rectangle2D().
        /// </summary>
        public static readonly Rectangle2D Empty = new Rectangle2D(0, 0, 0, 0);

        /// <summary>
        /// The unit (readonly). Value: new Rectangle2D(0, 0, 1, 1).
        /// </summary>
        public static readonly Rectangle2D Unit = new Rectangle2D(0, 0, 1, 1);
        #endregion Implementations

        #region Event Delegates
        /// <summary>
        /// The property changed event of the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The property changing event of the <see cref="PropertyChangingEventHandler"/>.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;
        #endregion

        #region Constructors
        ///// <summary>
        ///// Initializes a new default instance of the <see cref="Rectangle2D"/> class.
        ///// </summary>
        //public Rectangle2D()
        //    : this(0, 0, 0, 0)
        //{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D" /> class with an initial location and size.
        /// </summary>
        /// <param name="rectangle">The rectangle to clone.</param>
        public Rectangle2D(Rectangle2D rectangle)
            : this(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D" /> class with an empty location, with the provided size.
        /// </summary>
        /// <param name="size">The height and width of the <see cref="Rectangle2D" />.</param>
        public Rectangle2D(Size2D size)
            : this(0, 0, size.Width, size.Height)
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
        /// <param name="tuple">The tuple.</param>
        public Rectangle2D((double, double, double, double) tuple)
            : this()
        {
            (X, Y, Width, Height) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D" /> class with a location and size.
        /// </summary>
        /// <param name="x">The x coordinate of the upper left corner of the rectangle.</param>
        /// <param name="y">The y coordinate of the upper left corner of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The Height of the rectangle.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Rectangle2D(double x, double y, double width, double height)
            : this()
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D" /> class with the upper left and lower right corners.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Rectangle2D(Point2D point1, Point2D point2)
            : this()
        {
            X = Min(point1.X, point2.X);
            Y = Min(point1.Y, point2.Y);

            //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)
            Width = Max(Max(point1.X, point2.X) - X, 0);
            Height = Max(Max(point1.Y, point2.Y) - Y, 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D" /> class with the location and size from a tuple.
        /// </summary>
        /// <param name="tuple1">The tuple1.</param>
        /// <param name="tuple2">The tuple2.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Rectangle2D((double, double) tuple1, (double, double) tuple2)
            : this()
        {
            (X, Y) = tuple1;
            (Width, Height) = tuple2;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Rectangle2D"/> to a <see cref="ValueTuple{T1, T2, T3, T4}"/>.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double left, out double top, out double width, out double height)
        {
            left = X;
            top = Y;
            width = Width;
            height = Height;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the X coordinate location of the rectangle.
        /// </summary>
        [Browsable(true)]
        [DisplayName(nameof(X))]
        [Category("Elements")]
        [Description("The " + nameof(X) + " coordinate location of the " + nameof(Rectangle2D) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        [DataMember(Name = nameof(X)), XmlAttribute(nameof(X)), SoapAttribute(nameof(X))]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate location of the rectangle.
        /// </summary>
        [Browsable(true)]
        [DisplayName(nameof(Y))]
        [Category("Elements")]
        [Description("The " + nameof(Y) + " coordinate location of the " + nameof(Rectangle2D) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        [DataMember(Name = nameof(Y)), XmlAttribute(nameof(Y)), SoapAttribute(nameof(Y))]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the width of the rectangle.
        /// </summary>
        [Browsable(true)]
        [DisplayName(nameof(Width))]
        [Category("Elements")]
        [Description("The " + nameof(Width) + " of the " + nameof(Rectangle2D) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        [DataMember(Name = nameof(Width)), XmlAttribute(nameof(Width)), SoapAttribute(nameof(Width))]
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the rectangle.
        /// </summary>
        [Browsable(true)]
        [DisplayName(nameof(Height))]
        [Category("Elements")]
        [Description("The " + nameof(Height) + " of the " + nameof(Rectangle2D) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        [DataMember(Name = nameof(Height)), XmlAttribute(nameof(Height)), SoapAttribute(nameof(Height))]
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the location of the rectangle.
        /// </summary>
        [Browsable(false)]
        [DisplayName(nameof(Location))]
        [Category("Elements")]
        [Description("The top left " + nameof(Location) + " of the " + nameof(Rectangle2D) + ".")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Point2D Location { get { return new Point2D(X, Y); } set { (X, Y) = value; } }

        /// <summary>
        /// Gets or sets the height and width of the rectangle.
        /// </summary>
        [Browsable(false)]
        [DisplayName(nameof(Size))]
        [Category("Elements")]
        [Description("The " + nameof(Size) + " in " + nameof(Height) + " and " + nameof(Width) + " of the " + nameof(Rectangle2D) + ".")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        //[TypeConverter(typeof(Size2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Size2D Size { get { return new Size2D(Width, Height); } set { (Width, Height) = value; } }

        /// <summary>
        /// Gets or sets the center point X and Y coordinates of the rectangle.
        /// </summary>
        [Browsable(false)]
        [DisplayName(nameof(Center))]
        [Category("Elements")]
        [Description("The " + nameof(Center) + " location of the " + nameof(Rectangle2D) + ".")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Point2D Center { get { return new Point2D(X + (Width * 0.5d), Y + (Height * 0.5d)); } set { (X, Y) = (value.X - (Width * 0.5d), value.Y - (Height * 0.5d)); } }

        /// <summary>
        /// Gets or sets the Aspect ratio of the rectangle.
        /// </summary>
        [Browsable(true)]
        [DisplayName(nameof(Aspect))]
        [Category("Properties")]
        [Description("The " + nameof(Aspect) + " ratio of the height and width of the " + nameof(Rectangle2D) + ".")]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Aspect { get { return Height / Width; } set { (Height, Width) = (Width * value, Height / value); } }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        [Browsable(false)]
        [DisplayName(nameof(Left))]
        [Category("Elements")]
        [Description("The left location of the " + nameof(Rectangle2D) + ".")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Left { get { return X; } set { Width += X - value; X = value; } }

        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        [Browsable(false)]
        [DisplayName(nameof(Top))]
        [Category("Elements")]
        [Description("The top location of the " + nameof(Rectangle2D) + ".")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Top { get { return Y; } set { Height += Y - value; Y = value; } }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        [Browsable(false)]
        [DisplayName(nameof(Right))]
        [Category("Elements")]
        [Description("The right location of the " + nameof(Rectangle2D) + ".")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Right { get { return X + Width; } set { Width = value - X; } }

        /// <summary>
        /// Gets or sets the bottom.
        /// </summary>
        [Browsable(false)]
        //[DisplayName(nameof(Bottom))]
        [Category("Elements")]
        [Description("The bottom location of the " + nameof(Rectangle2D) + ".")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Bottom { get { return Y + Height; } set { Height = value - Y; } }

        /// <summary>
        /// Gets or sets the top left point.
        /// </summary>
        [Browsable(false)]
        [DisplayName(nameof(TopLeft))]
        [Category("Elements")]
        [Description("The top left location of the " + nameof(Rectangle2D) + ".")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D TopLeft { get { return new Point2D(Left, Top); } set { (Left, Top) = value; } }

        /// <summary>
        /// Gets or sets the top right point.
        /// </summary>
        [Browsable(false)]
        [DisplayName(nameof(TopRight))]
        [Category("Elements")]
        [Description("The top right location of the " + nameof(Rectangle2D) + ".")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D TopRight { get { return new Point2D(Right, Top); } set { (Right, Top) = value; } }

        /// <summary>
        /// Gets or sets the bottom left point.
        /// </summary>
        [Browsable(false)]
        [DisplayName(nameof(BottomLeft))]
        [Category("Elements")]
        [Description("The bottom left location of the " + nameof(Rectangle2D) + ".")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D BottomLeft { get { return new Point2D(Left, Bottom); } set { (Left, Bottom) = value; } }

        /// <summary>
        /// Gets or sets the bottom right point.
        /// </summary>
        [Browsable(false)]
        [DisplayName(nameof(BottomRight))]
        [Category("Elements")]
        [Description("The bottom right location of the " + nameof(Rectangle2D) + ".")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D BottomRight { get { return new Point2D(Right, Bottom); } set { (Right, Bottom) = value; } }

        /// <summary>
        /// Gets the bounding box of the rectangle.
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName(nameof(Bounds))]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Elements")]
        [Description("bounding box of the " + nameof(Rectangle2D) + ".")]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Rectangle2D Bounds => this;

        /// <summary>
        /// Gets the length of the perimeter of the rectangle.
        /// </summary>
        [Browsable(true)]
        [ReadOnly(true)]
        [DisplayName(nameof(Perimeter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Category("Elements")]
        [Description("The distance around the " + nameof(Rectangle2D) + ".")]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Perimeter => (Measurements.Distance(TopLeft, TopRight) * 2) + (Measurements.Distance(TopLeft, BottomLeft) * 2);

        /// <summary>
        /// Gets the area.
        /// </summary>
        [Browsable(true)]
        [ReadOnly(true)]
        [DisplayName(nameof(Area))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Category("Elements")]
        [Description("The " + nameof(Area) + " of the " + nameof(Rectangle2D) + ".")]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Area => Height * Width;

        /// <summary>
        /// Gets a value indicating whether the Rectangle2D has area.
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName(nameof(HasArea))]
        [Category("Elements")]
        [Description("A value indicating whether or not the " + nameof(Rectangle2D) + " has " + nameof(Height) + " or " + nameof(Width) + ".")]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool HasArea => Width > 0 && Height > 0;

        /// <summary>
        /// Gets a value indicating whether the Rectangle2D is empty.
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName(nameof(IsEmpty))]
        [Category("Elements")]
        [Description("A value indicating whether or not the " + nameof(Rectangle2D) + " has height or width.")]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsEmpty => (Width <= 0) || (Height <= 0);

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        [Browsable(false)]
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Dictionary<object, object> IPropertyCaching.PropertyCache { get; set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// Tests whether two <see cref="Rectangle2D"/> objects have equal location and size.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Rectangle2D left, Rectangle2D right) => Equals(left, right);

        /// <summary>
        /// Tests whether two <see cref="Rectangle2D"/> objects differ in location or size.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Rectangle2D left, Rectangle2D right) => !Equals(left, right);

        /// <summary>
        /// Implicit conversion from <see cref="ValueTuple{T1, T2, T3, T4}"/> to <see cref="Rectangle2D"/>.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Rectangle2D((double Left, double Top, double Width, double Height) tuple) => FromValueTuple(tuple);

        /// <summary>
        /// Implicit conversion from <see cref="Rectangle2D"/> to <see cref="ValueTuple{T1, T2, T3, T4}"/>.
        /// </summary>
        /// <returns></returns>
        /// <param name="rectangle"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double Left, double Top, double Width, double Height)(Rectangle2D rectangle) => (rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Tests whether <paramref name="obj" /> is a <see cref="Rectangle2D" /> with the same location and size of this <see cref="Rectangle2D" />.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Rectangle2D d && Equals(d);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] Rectangle2D other) => X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;

        /// <summary>
        /// Creates a new <see cref="Rectangle2D" /> from a <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D FromValueTuple((double X, double Y, double Width, double Height) tuple) => new Rectangle2D(tuple);

        /// <summary>
        /// Convert a rectangle to an array of it's corner points.
        /// </summary>
        /// <returns>An array of points representing the corners of a rectangle.</returns>
        public List<Point2D> ToPoints()
            => new List<Point2D>
            {
                Location,
                new Point2D(Right, Top),
                new Point2D(Right, Bottom),
                new Point2D(Left, Bottom)
            };
        #endregion

        #region Factories
        /// <summary>
        /// Creates a new <see cref="Rectangle2D"/> with the specified location and size.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="right">The right.</param>
        /// <param name="bottom">The bottom.</param>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D FromLTRB(double left, double top, double right, double bottom) => new Rectangle2D(left, top, right - left, bottom - top);

        /// <summary>
        /// Creates a <see cref="Rectangle2D"/> from a center point and it's size.
        /// </summary>
        /// <param name="center">The center point to create the <see cref="Rectangle2D"/> as a <see cref="Point2D"/>.</param>
        /// <param name="size">The height and width of the new <see cref="Rectangle2D"/> as a <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Rectangle2D"/> based around a center point and it's size.</returns>
        public static Rectangle2D RectangleFromCenter(Point2D center, Size2D size)
            => new Rectangle2D(center - (size * 0.5d), size);

        /// <summary>
        /// Creates a rectangle that represents the union between a and b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Rectangle2D Union(Rectangle2D a, Rectangle2D b)
        {
            var left = Min(a.X, b.X);
            var top = Min(a.Y, b.Y);
            var x2 = Max(a.X + a.Width, b.X + b.Width);
            var y2 = Max(a.Y + a.Height, b.Y + b.Height);

            return new Rectangle2D(left, top, x2 - left, y2 - top);
        }

        /// <summary>
        /// Union - Return the result of the union of Rectangle2D and point.
        /// </summary>
        public static Rectangle2D Union(Rectangle2D rect, Point2D point)
        {
            rect.UnionMutate(new Rectangle2D(point, point));
            return rect;
        }

        /// <summary>
        /// Creates a rectangle that represents the intersection between a and b. If there is no intersection, null is returned.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Rectangle2D Intersect(Rectangle2D a, Rectangle2D b)
        {
            var x1 = Max(a.X, b.X);
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
        public static Rectangle2D Offset(Rectangle2D rect, Vector2D offsetVector)
        {
            rect.Offset(offsetVector.I, offsetVector.J);
            return rect;
        }

        /// <summary>
        /// Offset - return the result of offsetting Rectangle2D by the offset provided
        /// If this is Empty, this method is illegal.
        /// </summary>
        public static Rectangle2D Offset(Rectangle2D rect, double offsetX, double offsetY)
        {
            rect.Offset(offsetX, offsetY);
            return rect;
        }

        /// <summary>
        /// Creates a <see cref="Rectangle2D"/> that is inflated by the specified amount.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Rectangle2D Inflate(Rectangle2D rect, float x, float y)
        {
            var r = rect;
            r.Inflate(x, y);
            return r;
        }

        /// <summary>
        /// Inflate - return the result of inflating Rectangle2D by the size provided, in all directions
        /// If this is Empty, this method is illegal.
        /// </summary>
        public static Rectangle2D Inflate(Rectangle2D rect, Size2D size)
        {
            rect.Inflate(size.Width, size.Height);
            return rect;
        }

        /// <summary>
        /// Inflate - return the result of inflating Rectangle2D by the size provided, in all directions
        /// If this is Empty, this method is illegal.
        /// </summary>
        public static Rectangle2D Inflate(Rectangle2D rect, double width, double height)
        {
            rect.Inflate(width, height);
            return rect;
        }

        ///// <summary>
        ///// Returns the bounds of the transformed rectangle.
        ///// The Empty Rectangle2D is not affected by this call.
        ///// </summary>
        ///// <returns>
        ///// The Rectangle2D which results from the transformation.
        ///// </returns>
        ///// <param name="rect"> The Rectangle2D to transform. </param>
        ///// <param name="matrix"> The Matrix by which to transform. </param>
        //public static Rectangle2D Transform(Rectangle2D rect, Matrix3x2D matrix)
        //{
        //    Matrix3x2D.TransformRect(ref rect, ref matrix);
        //    return rect;
        //}
        #endregion Factories

        #region Mutators
        /// <summary>
        /// Union - Update this rectangle to be the union of this and Rectangle2D.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public void UnionMutate(Rectangle2D rect)
        {
            var left = /*rect is null ? Left :*/ Min(Left, rect.Left);
            var top = /*rect is null ? Top :*/ Min(Top, rect.Top);

            //if (!(rect is null))
            //{
            // We need this check so that the math does not result in NaN
            if (double.IsPositiveInfinity(rect.Width) || double.IsPositiveInfinity(Width))
            {
                Width = double.PositiveInfinity;
            }
            else
            {
                //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)
                Width = Max(Max(Right, rect.Right) - left, 0);
            }

            // We need this check so that the math does not result in NaN
            if (double.IsPositiveInfinity(rect.Height) || double.IsPositiveInfinity(Height))
            {
                Height = double.PositiveInfinity;
            }
            else
            {
                //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)
                Height = Max(Max(Bottom, rect.Bottom) - top, 0);
            }
            //}

            X = left;
            Y = top;
            OnPropertyChanged(nameof(Union));
        }

        /// <summary>
        /// Return a rectangle that is a union of this and a supplied Rectangle2D.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public Rectangle2D Union(Rectangle2D rect)
        {
            var left = Min(Left, rect!.Left);
            var top = Min(Top, rect!.Top);
            double width;
            double height;

            // We need this check so that the math does not result in NaN
            if (double.IsPositiveInfinity(rect!.Width) || double.IsPositiveInfinity(Width))
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
            if (/*rect is null ||*/ double.IsPositiveInfinity(rect!.Height) || double.IsPositiveInfinity(Height))
            {
                height = double.PositiveInfinity;
            }
            else
            {
                //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)
                var maxBottom = Max(Bottom, rect.Bottom);
                height = Max(maxBottom - top, 0);
            }

            return new Rectangle2D(left, top, width, height);
        }

        /// <summary>
        /// Union - Update this rectangle to be the union of this and point.
        /// </summary>
        public void UnionMutate(Point2D point) => UnionMutate(new Rectangle2D(point, point));

        /// <summary>
        /// Return a rectangle that is a union of this and a supplied Point2D.
        /// </summary>
        public Rectangle2D Union(Point2D point) => Union(new Rectangle2D(point, point));

        /// <summary>
        /// Creates a Rectangle that represents the intersection between this Rectangle and rectangle.
        /// </summary>
        /// <param name="rect"></param>
        public void Intersect(Rectangle2D rect)
        {
            var result = Intersect(rect, this);

            X = result.X;
            Y = result.Y;
            Width = result.Width;
            Height = result.Height;
            OnPropertyChanged(nameof(Intersect));
        }

        /// <summary>
        /// Adjusts the location of this rectangle by the specified amount.
        /// </summary>
        /// <param name="pos"></param>
        public void Offset(Point2D pos) => Offset(pos.X, pos.Y);

        /// <summary>
        /// Offset - translate the Location by the offset provided.
        /// If this is Empty, this method is illegal.
        /// </summary>
        /// <param name="offsetVector"></param>
        public void Offset(Vector2D offsetVector) => Offset(offsetVector.I, offsetVector.J);

        /// <summary>
        /// Adjusts the location of this rectangle by the specified amount.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Offset(double x, double y)
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Cannot call method.");
            }

            X += x;
            Y += y;
            OnPropertyChanged(nameof(Offset));
        }

        /// <summary>
        /// Inflates this <see cref="Rectangle2D"/> by the specified amount.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Inflate(double x, double y)
        {
            X -= x;
            Y -= y;
            Width += 2 * x;
            Height += 2 * y;
            OnPropertyChanged(nameof(Inflate));
        }

        /// <summary>
        /// Inflates this <see cref="Rectangle2D"/> by the specified amount.
        /// </summary>
        /// <param name="size"></param>
        public void Inflate(Size2D size) => Inflate(size.Width, size.Height);

        ///// <summary>
        ///// Updates rectangle to be the bounds of the original value transformed
        ///// by the matrix.
        ///// The Empty Rectangle2D is not affected by this call.
        ///// </summary>
        ///// <param name="matrix"> Matrix </param>
        //public void Transform(Matrix3x2D matrix)
        //    => Matrix3x2D.TransformRect(ref this, ref matrix);
        #endregion Mutators

        #region Methods
        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Contains(Point2D point) => Intersections.Contains(this, point) != Inclusions.Outside;

        /// <summary>
        /// Determines if the rectangular region represented by <paramref name="rect"/> is entirely contained within the rectangular region represented by  this <see cref="Rectangle2D"/> .
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool Contains(Rectangle2D rect) => Intersections.Contains(this, rect);

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool IntersectsWith(Rectangle2D rect) => Intersections.Intersects(this, rect);

        /// <summary>
        /// The intersects.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public bool Intersects(IShape shape) => Intersections.Intersects(this, shape);
        #endregion

        #region Standard Methods
        /// <summary>
        /// Raises the property changing event.
        /// </summary>
        /// <param name="name">The name.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPropertyChanging([CallerMemberName] string name = "") => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="name">The name.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Gets the hash code for this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

        /// <summary>
        /// Creates a <see cref="string"/> representation of this <see cref="IShape"/> interface based on the current culture.
        /// </summary>
        /// <returns>A <see cref="string"/> representation of this instance of the <see cref="IShape"/> object.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Rectangle2D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null)
            {
                return nameof(Rectangle2D);
            }

            var sep = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Rectangle2D)}({nameof(X)}: {X.ToString(format, formatProvider)}{sep} {nameof(Y)}: {Y.ToString(format, formatProvider)}{sep} {nameof(Width)}: {Width.ToString(format, formatProvider)}{sep} {nameof(Height)}: {Height.ToString(format, formatProvider)})";
        }

        public (double Left, double Top, double Width, double Height) ToValueTuple() => throw new NotImplementedException();
        #endregion Methods
    }
}
