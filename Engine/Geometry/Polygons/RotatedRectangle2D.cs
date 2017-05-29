// <copyright file="RotatedRectangle2D.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
//using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(RotatedRectangle2D))]
    public class RotatedRectangle2D
        : Shape
    {
        #region Implementations

        /// <summary>
        /// 
        /// </summary>
        public static readonly RotatedRectangle2D Empty = new RotatedRectangle2D();

        /// <summary>
        /// 
        /// </summary>
        public static readonly RotatedRectangle2D Unit = new RotatedRectangle2D(0, 0, 1, 1, 0);

        #endregion

        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private double x;

        /// <summary>
        /// 
        /// </summary>
        private double y;

        /// <summary>
        /// 
        /// </summary>
        private double width;

        /// <summary>
        /// 
        /// </summary>
        private double height;

        /// <summary>
        /// 
        /// </summary>
        private double angle;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new default instance of the <see cref="RotatedRectangle2D"/> class.
        /// </summary>
        public RotatedRectangle2D()
            : this(0, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotatedRectangle2D"/> class with an empty location, with the provided size.
        /// </summary>
        /// <param name="size">The height and width of the <see cref="RotatedRectangle2D"/>.</param>
        public RotatedRectangle2D(Size2D size)
            : this(0, 0, size.Width, size.Height, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotatedRectangle2D"/> class with an initial location and size.
        /// </summary>
        /// <param name="rectangle">The rectangle to clone.</param>
        public RotatedRectangle2D(RotatedRectangle2D rectangle)
            : this(rectangle.X, rectangle.Y, rectangle.Width, rectangle.height, rectangle.angle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotatedRectangle2D"/> class with an initial location and size.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        public RotatedRectangle2D(Point2D location, Size2D size)
            : this(location.X, location.Y, size.Width, size.Height, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotatedRectangle2D"/> class with an initial location and size.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="angle"></param>
        public RotatedRectangle2D(Point2D location, Size2D size, double angle)
            : this(location.X, location.Y, size.Width, size.Height, angle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotatedRectangle2D"/> class with the location and size from a tuple.
        /// </summary>
        /// <param name="tuple"></param>
        public RotatedRectangle2D((double, double, double, double) tuple)
            : this(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotatedRectangle2D"/> class with the location and size from a tuple.
        /// </summary>
        /// <param name="tuple"></param>
        public RotatedRectangle2D((double, double, double, double, double) tuple)
            : this(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotatedRectangle2D"/> class with a location and size.
        /// </summary>
        /// <param name="x">The x coordinate of the upper left corner of the rectangle.</param>
        /// <param name="y">The y coordinate of the upper left corner of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="angle"></param>
        /// <param name="height">The Height of the rectangle.</param>
        public RotatedRectangle2D(double x, double y, double width, double height, double angle)
        {
            if (width < 0 || height < 0)
                throw new ArgumentException("Width and Height cannot be Negative.");

            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.angle = angle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotatedRectangle2D"/> class with the upper left and lower right corners.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        public RotatedRectangle2D(Point2D point1, Point2D point2)
        {
            x = Min(point1.X, point2.X);
            y = Min(point1.Y, point2.Y);

            //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)
            width = Max(Max(point1.X, point2.X) - x, 0);
            height = Max(Max(point1.Y, point2.Y) - y, 0);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the X coordinate location of the rectangle.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
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
                OnPropertyChanged(nameof(X));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate location of the rectangle.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
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
                OnPropertyChanged(nameof(Y));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the height of the rectangle.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
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
                OnPropertyChanged(nameof(Height));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the width of the rectangle.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
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
                OnPropertyChanged(nameof(Width));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Aspect ratio of the rectangle. 
        /// </summary>
        /// <remarks></remarks>
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
                OnPropertyChanged(nameof(Aspect));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the angle the rectangle should be rotated.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [Browsable(true)]
        //[DisplayName(nameof(Aspect))]
        [Category("Properties")]
        [Description("The " + nameof(Angle) + " of rotation of the " + nameof(RotatedRectangle2D) + ".")]
        [GeometryAngleRadians]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                OnPropertyChanged(nameof(Angle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the center point X and Y coordinates of the rectangle.
        /// </summary>
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
            get { return new Point2D(X, Y); }
            set
            {
                x = value.X;
                y = value.Y;
                OnPropertyChanged(nameof(Center));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the height and width of the rectangle.
        /// </summary>
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
                width = value.Width;
                height = value.Height;
                OnPropertyChanged(nameof(Size));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the rectangle is empty. Meaning it has no height or width.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsEmpty
            => (Width <= 0) || (Height <= 0);

        /// <summary>
        /// Gets a value indicating whether the Rectangle2D has area.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        //[ReadOnly(true)]
        //[DisplayName(nameof(HasArea))]
        [Category("Elements")]
        [Description("A value indicating whether or not the rectangle has height or width.")]
        public bool HasArea
            => width > 0 && height > 0;

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        //[ReadOnly(true)]
        //[DisplayName(nameof(Area))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Category("Elements")]
        [Description("The area of the rectangle.")]
        public override double Area
            => height * width;

        /// <summary>
        /// Gets the length of the perimeter of the rectangle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        //[ReadOnly(true)]
        //[DisplayName(nameof(Perimeter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Category("Elements")]
        [Description("The distance around the rectangle.")]
        public override double Perimeter
            => (Measurements.Distance(0, 0, width, 0) * 2) + (Measurements.Distance(0, 0, 0, height) * 2);

        /// <summary>
        /// Gets the bounding box of the rectangle.
        /// </summary>
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
        public override Rectangle2D Bounds
            => Measurements.RotatedRectangleBounds(width, height, Center, angle);

        #endregion

        #region Operators

        /// <summary>
        /// Tests whether two <see cref="RotatedRectangle2D"/> objects have equal location, size, an angle.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(RotatedRectangle2D left, RotatedRectangle2D right)
            => Equals(left, right);

        /// <summary>
        /// Tests whether two <see cref="RotatedRectangle2D"/> objects differ in location, size or angle.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(RotatedRectangle2D left, RotatedRectangle2D right)
            => !Equals(left, right);

        /// <summary>
        /// Compares two <see cref="RotatedRectangle2D"/>.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(RotatedRectangle2D left, RotatedRectangle2D right)
            => Equals(left, right);

        /// <summary>
        /// Tests whether <paramref name="left"/> is a <see cref="RotatedRectangle2D"/> with the same location and size of <paramref name="right"/>.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(RotatedRectangle2D left, RotatedRectangle2D right)
            => (left?.X == right?.X
            && left?.Y == right?.Y
            && left?.Width == right?.Width
            && left?.Height == right?.Height
            && Abs(left.Angle - right.Angle) < Maths.Epsilon);

        /// <summary>
        /// Tests whether <paramref name="obj"/> is a <see cref="RotatedRectangle2D"/> with the same location and size of this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
            => obj is RotatedRectangle2D && Equals(this, obj as RotatedRectangle2D);

        #endregion

        #region Factories

        /// <summary>
        /// Creates a new <see cref="RotatedRectangle2D"/> with the specified location and size.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static RotatedRectangle2D FromLTRBA(double left, double top, double right, double bottom, double angle)
            => new RotatedRectangle2D(left, top, right - left, bottom - top, angle);

        /// <summary>
        /// Creates a <see cref="Rectangle"/> from a center point and it's size.
        /// </summary>
        /// <param name="center">The center point to create the <see cref="Rectangle"/> as a <see cref="Point"/>.</param>
        /// <param name="size">The height and width of the new <see cref="Rectangle"/> as a <see cref="Size"/>.</param>
        /// <param name="angle"></param>
        /// <returns>Returns a <see cref="Rectangle"/> based around a center point and it's size.</returns>
        public static RotatedRectangle2D RectangleFromCenter(Point2D center, Size2D size, double angle)
            => new RotatedRectangle2D(center - size.Inflate(0.5d), size, angle);

        ///// <summary>
        ///// Creates a rectangle that represents the union between a and b.
        ///// </summary>
        ///// <param name="a"></param>
        ///// <param name="b"></param>
        ///// <returns></returns>
        //public static RotatedRectangle2D Union(RotatedRectangle2D a, RotatedRectangle2D b)
        //{
        //    double left = Min(a.X, b.X);
        //    double top = Min(a.Y, b.Y);
        //    double x2 = Max(a.X + a.Width, b.X + b.Width);
        //    double y2 = Max(a.Y + a.Height, b.Y + b.Height);

        //    return new RotatedRectangle2D(left, top, x2 - left, y2 - top);
        //}

        ///// <summary>
        ///// Union - Return the result of the union of Rectangle2D and point.
        ///// </summary>
        //public static RotatedRectangle2D Union(RotatedRectangle2D rect, RotatedRectangle2D point)
        //{
        //    rect.Union(new RotatedRectangle2D(point, point));
        //    return rect;
        //}

        ///// <summary>
        ///// Creates a rectangle that represents the intersection between a and b. If there is no intersection, null is returned.
        ///// </summary>
        ///// <param name="a"></param>
        ///// <param name="b"></param>
        ///// <returns></returns>
        //public static RotatedRectangle2D Intersect(RotatedRectangle2D a, RotatedRectangle2D b)
        //{
        //    double x1 = Max(a.X, b.X);
        //    double x2 = Min(a.X + a.Width, b.X + b.Width);
        //    double y1 = Max(a.Y, b.Y);
        //    double y2 = Min(a.Y + a.Height, b.Y + b.Height);

        //    if (x2 >= x1 && y2 >= y1)
        //    {
        //        return new RotatedRectangle2D(x1, y1, x2 - x1, y2 - y1);
        //    }

        //    return Empty;
        //}

        ///// <summary>
        ///// Offset - return the result of offsetting Rectangle2D by the offset provided.
        ///// If this is Empty, this method is illegal.
        ///// </summary>
        //public static RotatedRectangle2D Offset(RotatedRectangle2D rect, Vector2D offsetVector)
        //{
        //    rect.Offset(offsetVector.I, offsetVector.J);
        //    return rect;
        //}

        ///// <summary>
        ///// Offset - return the result of offsetting Rectangle2D by the offset provided
        ///// If this is Empty, this method is illegal.
        ///// </summary>
        //public static RotatedRectangle2D Offset(RotatedRectangle2D rect, double offsetX, double offsetY)
        //{
        //    rect.Offset(offsetX, offsetY);
        //    return rect;
        //}

        ///// <summary>
        ///// Creates a <see cref="RotatedRectangle2D"/> that is inflated by the specified amount.
        ///// </summary>
        ///// <param name="rect"></param>
        ///// <param name="x"></param>
        ///// <param name="y"></param>
        ///// <returns></returns>
        //public static RotatedRectangle2D Inflate(RotatedRectangle2D rect, float x, float y)
        //{
        //    RotatedRectangle2D r = rect;
        //    r.Inflate(x, y);
        //    return r;
        //}

        ///// <summary>
        ///// Inflate - return the result of inflating Rectangle2D by the size provided, in all directions
        ///// If this is Empty, this method is illegal.
        ///// </summary>
        //public static RotatedRectangle2D Inflate(RotatedRectangle2D rect, Size size)
        //{
        //    rect.Inflate(size.Width, size.Height);
        //    return rect;
        //}

        ///// <summary>
        ///// Inflate - return the result of inflating Rectangle2D by the size provided, in all directions
        ///// If this is Empty, this method is illegal.
        ///// </summary>
        //public static RotatedRectangle2D Inflate(RotatedRectangle2D rect, double width, double height)
        //{
        //    rect.Inflate(width, height);
        //    return rect;
        //}

        ///// <summary>
        ///// Returns the bounds of the transformed rectangle.
        ///// The Empty Rectangle2D is not affected by this call.
        ///// </summary>
        ///// <returns>
        ///// The Rectangle2D which results from the transformation.
        ///// </returns>
        ///// <param name="rect"> The Rectangle2D to transform. </param>
        ///// <param name="matrix"> The Matrix by which to transform. </param>
        //public static RotatedRectangle2D Transform(RotatedRectangle2D rect, Matrix2x3D matrix)
        //{
        //    Matrix2x3D.TransformRect(ref rect, ref matrix);
        //    return rect;
        //}

        #endregion

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(RotatedRectangle2D)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(RotatedRectangle2D)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(RotatedRectangle2D)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(RotatedRectangle2D)} has been deserialized.");
        //}

        //#endregion

        #region Mutators

        ///// <summary>
        ///// Union - Update this rectangle to be the union of this and Rectangle2D.
        ///// </summary>
        //public void Union(RotatedRectangle2D rect)
        //{
        //    double left = Min(Left, rect.Left);
        //    double top = Min(Top, rect.Top);

        //    // We need this check so that the math does not result in NaN
        //    if ((rect.Width == double.PositiveInfinity) || (Width == double.PositiveInfinity))
        //    {
        //        width = double.PositiveInfinity;
        //    }
        //    else
        //    {
        //        //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)                    
        //        double maxRight = Max(Right, rect.Right);
        //        width = Max(maxRight - left, 0);
        //    }

        //    // We need this check so that the math does not result in NaN
        //    if ((rect.Height == double.PositiveInfinity) || (Height == double.PositiveInfinity))
        //    {
        //        height = double.PositiveInfinity;
        //    }
        //    else
        //    {
        //        //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)
        //        double maxBottom = Max(Bottom, rect.Bottom);
        //        height = Max(maxBottom - top, 0);
        //    }

        //    x = left;
        //    y = top;
        //}

        ///// <summary>
        ///// Union - Update this rectangle to be the union of this and point.
        ///// </summary>
        //public void Union(Point2D point)
        //{
        //    Union(new RotatedRectangle2D(point, point));
        //}

        ///// <summary>
        ///// Creates a Rectangle that represents the intersection between this Rectangle and rect.
        ///// </summary>
        ///// <param name="rect"></param>
        //public void Intersect(RotatedRectangle2D rect)
        //{
        //    RotatedRectangle2D result = Intersect(rect, this);

        //    x = result.X;
        //    y = result.Y;
        //    width = result.Width;
        //    height = result.Height;
        //}

        ///// <summary>
        ///// Adjusts the location of this rectangle by the specified amount.
        ///// </summary>
        ///// <param name="pos"></param>
        //public void Offset(Point2D pos)
        //{
        //    Offset(pos.X, pos.Y);
        //}

        ///// <summary>
        ///// Offset - translate the Location by the offset provided.
        ///// If this is Empty, this method is illegal.
        ///// </summary>
        ///// <param name="offsetVector"></param>
        //public void Offset(Vector2D offsetVector)
        //{
        //    Offset(offsetVector.I, offsetVector.J);
        //}

        ///// <summary>
        ///// Adjusts the location of this rectangle by the specified amount.
        ///// </summary>
        ///// <param name="x"></param>
        ///// <param name="y"></param>
        //public void Offset(double x, double y)
        //{
        //    if (IsEmpty) throw new InvalidOperationException("Cannot call method.");
        //    this.x += x;
        //    this.y += y;
        //}

        ///// <summary>
        ///// Inflates this <see cref="Rectangle2D"/> by the specified amount.
        ///// </summary>
        ///// <param name="x"></param>
        ///// <param name="y"></param>
        //public void Inflate(double x, double y)
        //{
        //    this.x -= x;
        //    this.y -= y;
        //    width += 2 * x;
        //    height += 2 * y;
        //}

        ///// <summary>
        ///// Inflates this <see cref="RotatedRectangle2D"/> by the specified amount.
        ///// </summary>
        ///// <param name="size"></param>
        //public void Inflate(Size2D size)
        //{
        //    Inflate(size.Width, size.Height);
        //}

        ///// <summary>
        ///// Updates rectangle to be the bounds of the original value transformed
        ///// by the matrix.
        ///// The Empty Rectangle2D is not affected by this call.        
        ///// </summary>
        ///// <param name="matrix"> Matrix </param>
        //public void Transform(Matrix2x3D matrix)
        //{
        //    Matrix2x3D.TransformRect(ref this, ref matrix);
        //}

        #endregion

        #region Methods

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="point"></param>
        ///// <returns></returns>
        //public override bool Contains(Point2D point)
        //{
        //    return Intersections.Contains(this, point);
        //}

        /// <summary>
        /// Determines if the rectangular region represented by <paramref name="rect"/> is entirely contained within the rectangular region represented by  this <see cref="Rectangle2D"/> .
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool Contains(Rectangle2D rect)
            => Bounds.Contains(rect);

        ///// <summary>
        ///// Determines if this rectangle interests with another rectangle.
        ///// </summary>
        ///// <param name="rect"></param>
        ///// <returns></returns>
        //public bool IntersectsWith(Rectangle2D rect)
        //{
        //    return Intersections.RectangleIntersectsRectangle(this, rect);
        //}

        /// <summary>
        /// Convert a rectangle to an array of it's corner points.
        /// </summary>
        /// <returns>An array of points representing the corners of a rectangle.</returns>
        public List<Point2D> ToPoints()
            => Interpolators.RotatedRectangle(x - (width * 0.5d), y - (height * 0.5d), width, height, Center.X, Center.Y, angle);

        /// <summary>
        /// Gets the hash code for this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => unchecked((int)((uint)X
            ^ (((uint)Y << 13) | ((uint)Y >> 19))
            ^ (((uint)Width << 26) | ((uint)Width >> 6))
            ^ (((uint)Height << 7) | ((uint)Height >> 25))));

        /// <summary>
        /// Convert a rectangle to a polygon containing an array of the rectangle's corner points.
        /// </summary>
        /// <returns>An array of points representing the corners of a rectangle.</returns>
        public Contour ToPolygon()
            => new Contour(ToPoints());

        /// <summary>
        /// Creates a string representation of this <see cref="Rectangle2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null)
                return nameof(RotatedRectangle2D);
            //return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4}}}", nameof(Size2D), nameof(Width), Width, nameof(Height), Height);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(RotatedRectangle2D)}{{{nameof(X)}={x},{nameof(Y)}={y},{nameof(Width)}={width},{nameof(Height)}={height},{nameof(Angle)}={angle}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
