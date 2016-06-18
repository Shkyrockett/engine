﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Rectangle2D")]
    public class Rectangle2D
        : Shape, IClosedShape
    {
        #region Static Implementations

        /// <summary>
        /// 
        /// </summary>
        public static readonly Rectangle2D Empty = new Rectangle2D();

        /// <summary>
        /// 
        /// </summary>
        public static readonly Rectangle2D Unit = new Rectangle2D(0, 0, 1, 1);

        #endregion

        #region Private Fields

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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new default instance of the <see cref="Rectangle2D"/> class.
        /// </summary>
        public Rectangle2D()
            : this(0, 0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D"/> class with an empty location, with the provided size.
        /// </summary>
        /// <param name="size">The height and width of the <see cref="Rectangle2D"/>.</param>
        public Rectangle2D(Size2D size)
            : this(0, 0, size.Width, size.Height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D"/> class with an initial location and size.
        /// </summary>
        /// <param name="rectangle">The rectangle to clone.</param>
        public Rectangle2D(Rectangle2D rectangle)
            : this(rectangle.X, rectangle.Y, rectangle.Width, rectangle.height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D"/> class with an initial location and size.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        public Rectangle2D(Point2D location, Size2D size)
            : this(location.X, location.Y, size.Width, size.Height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D"/> class  with a location and a vector size.
        /// </summary>
        public Rectangle2D(Point2D point, Vector2D vector)
            : this(point, point + vector)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D"/> class with the location and size from a tuple.
        /// </summary>
        /// <param name="tuple"></param>
        public Rectangle2D(Tuple<double, double, double, double> tuple)
            : this(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D"/> class with a location and size.
        /// </summary>
        /// <param name="x">The x coordinate of the upper left corner of the rectangle.</param>
        /// <param name="y">The y coordinate of the upper left corner of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The Height of the rectangle.</param>
        public Rectangle2D(double x, double y, double width, double height)
        {
            if (width < 0 || height < 0) throw new ArgumentException("Width and Height cannot be Negative.");

            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D"/> class with the upper left and lower right corners.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        public Rectangle2D(Point2D point1, Point2D point2)
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
        /// 
        /// </summary>
        [XmlAttribute]
        [Browsable(false)]
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
        /// 
        /// </summary>
        [XmlAttribute]
        [Browsable(false)]
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
        [XmlAttribute]
        [Browsable(false)]
        public double Height
        {
            get { return height; }
            set
            {
                height = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        [Browsable(false)]
        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Location
        {
            get { return new Point2D(X, Y); }
            set
            {
                x = value.X;
                y = value.Y;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(Size2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Size2D Size
        {
            get { return new Size2D(width, height); }
            set
            {
                width = value.Width;
                height = value.Height;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Point2D TopLeft
        {
            get { return new Point2D(Left, Top); }
            set
            {
                Left = value.X;
                Top = value.Y;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Point2D TopRight
        {
            get { return new Point2D(Top, Right); }
            set
            {
                Right = value.X;
                Top = value.Y;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Point2D BottomLeft
        {
            get { return new Point2D(Left, Bottom); }
            set
            {
                Left = value.X;
                Bottom = value.Y;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Point2D BottomRight
        {
            get { return new Point2D(Right, Bottom); }
            set
            {
                Right = value.X;
                Bottom = value.Y;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public double Left
        {
            get { return x; }
            set
            {
                width += x - value;
                x = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public double Top
        {
            get { return y; }
            set
            {
                height += y - value;
                y = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public double Right
        {
            get { return x + width; }
            set
            {
                width = value - x;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public double Bottom
        {
            get { return y + height; }
            set
            {
                height = value - y;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public bool IsEmpty => (Width <= 0) || (Height <= 0);

        /// <summary>
        /// Returns true if this Rectangle2D has area.
        /// </summary>
        [XmlIgnore]
        public bool HasArea => width > 0 && height > 0;

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [XmlIgnore]
        public override double Perimeter => (TopLeft.Distance(TopRight) * 2) + (TopLeft.Distance(BottomLeft) * 2);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public override Rectangle2D Bounds => this;

        #endregion

        #region Operators

        /// <summary>
        /// Tests whether two <see cref="Rectangle2D"/> objects have equal location and size.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Rectangle2D left, Rectangle2D right) => Equals(left, right);

        /// <summary>
        /// Tests whether two <see cref="RectangleF"/> objects differ in location or size.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Rectangle2D left, Rectangle2D right) => !Equals(left, right);

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Rectangle2D left, Rectangle2D right) => Equals(left, right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Rectangle2D left, Rectangle2D right) => (left?.X == right?.X && left?.Y == right?.Y && left?.Width == right?.Width && left?.Height == right?.Height);

        /// <summary>
        /// Tests whether <paramref name="obj"/> is a <see cref="Rectangle2D"/> with the same location and size of this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is Rectangle2D && Equals(this, obj as Rectangle2D);

        #endregion

        #region Factories

        /// <summary>
        /// Creates a new <see cref="Rectangle2D"/> with the specified location and size.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        [Pure]
        public static Rectangle2D FromLTRB(double left, double top, double right, double bottom) => new Rectangle2D(left, top, right - left, bottom - top);

        /// <summary>
        /// Creates a <see cref="Rectangle"/> from a center point and it's size.
        /// </summary>
        /// <param name="center">The center point to create the <see cref="Rectangle"/> as a <see cref="Point"/>.</param>
        /// <param name="size">The height and width of the new <see cref="Rectangle"/> as a <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Rectangle"/> based around a center point and it's size.</returns>
        [Pure]
        public static Rectangle2D RectangleFromCenter(Point2D center, Size2D size) => new Rectangle2D(center - size.Inflate(0.5d), size);

        /// <summary>
        /// Creates a rectangle that represents the union between a and b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [Pure]
        public static Rectangle2D Union(Rectangle2D a, Rectangle2D b)
        {
            double left = Min(a.X, b.X);
            double top = Min(a.Y, b.Y);
            double x2 = Max(a.X + a.Width, b.X + b.Width);
            double y2 = Max(a.Y + a.Height, b.Y + b.Height);

            return new Rectangle2D(left, top, x2 - left, y2 - top);
        }

        /// <summary>
        /// Union - Return the result of the union of Rectangle2D and point.
        /// </summary>
        [Pure]
        public static Rectangle2D Union(Rectangle2D rect, Point2D point)
        {
            rect.Union(new Rectangle2D(point, point));
            return rect;
        }

        /// <summary>
        /// Creates a rectangle that represents the intersection between a and b. If there is no intersection, null is returned.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [Pure]
        public static Rectangle2D Intersect(Rectangle2D a, Rectangle2D b)
        {
            double x1 = Max(a.X, b.X);
            double x2 = Min(a.X + a.Width, b.X + b.Width);
            double y1 = Max(a.Y, b.Y);
            double y2 = Min(a.Y + a.Height, b.Y + b.Height);

            if (x2 >= x1 && y2 >= y1)
                return new Rectangle2D(x1, y1, x2 - x1, y2 - y1);

            return Empty;
        }

        /// <summary>
        /// Offset - return the result of offsetting Rectangle2D by the offset provided
        /// If this is Empty, this method is illegal.
        /// </summary>
        [Pure]
        public static Rectangle2D Offset(Rectangle2D rect, Vector2D offsetVector)
        {
            rect.Offset(offsetVector.I, offsetVector.J);
            return rect;
        }

        /// <summary>
        /// Offset - return the result of offsetting Rectangle2D by the offset provided
        /// If this is Empty, this method is illegal.
        /// </summary>
        [Pure]
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
        [Pure]
        public static Rectangle2D Inflate(Rectangle2D rect, float x, float y)
        {
            Rectangle2D r = rect;
            r.Inflate(x, y);
            return r;
        }

        /// <summary>
        /// Inflate - return the result of inflating Rectangle2D by the size provided, in all directions
        /// If this is Empty, this method is illegal.
        /// </summary>
        [Pure]
        public static Rectangle2D Inflate(Rectangle2D rect, Size size)
        {
            rect.Inflate(size.Width, size.Height);
            return rect;
        }

        /// <summary>
        /// Inflate - return the result of inflating Rectangle2D by the size provided, in all directions
        /// If this is Empty, this method is illegal.
        /// </summary>
        [Pure]
        public static Rectangle2D Inflate(Rectangle2D rect, double width, double height)
        {
            rect.Inflate(width, height);
            return rect;
        }

        /// <summary>
        /// Returns the bounds of the transformed rectangle.
        /// The Empty Rectangle2D is not affected by this call.
        /// </summary>
        /// <returns>
        /// The Rectangle2D which results from the transformation.
        /// </returns>
        /// <param name="rect"> The Rectangle2D to transform. </param>
        /// <param name="matrix"> The Matrix by which to transform. </param>
        [Pure]
        public static Rectangle2D Transform(Rectangle2D rect, Matrix2D matrix)
        {
            Matrix2D.TransformRect(ref rect, ref matrix);
            return rect;
        }

        #endregion

        #region Mutators

        /// <summary>
        /// Union - Update this rectangle to be the union of this and Rectangle2D.
        /// </summary>
        public void Union(Rectangle2D rect)
        {
            double left = Min(Left, rect.Left);
            double top = Min(Top, rect.Top);

            // We need this check so that the math does not result in NaN
            if ((double.IsPositiveInfinity(rect.Width)) || (double.IsPositiveInfinity(Width)))
            {
                width = double.PositiveInfinity;
            }
            else
            {
                //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)                    
                double maxRight = Max(Right, rect.Right);
                width = Max(maxRight - left, 0);
            }

            // We need this check so that the math does not result in NaN
            if ((double.IsPositiveInfinity(rect.Height)) || (double.IsPositiveInfinity(Height)))
            {
                height = double.PositiveInfinity;
            }
            else
            {
                //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)
                double maxBottom = Max(Bottom, rect.Bottom);
                height = Max(maxBottom - top, 0);
            }

            x = left;
            y = top;
        }

        /// <summary>
        /// Union - Update this rectangle to be the union of this and point.
        /// </summary>
        public void Union(Point2D point)
        {
            Union(new Rectangle2D(point, point));
        }

        /// <summary>
        /// Creates a Rectangle that represents the intersection between this Rectangle and rect.
        /// </summary>
        /// <param name="rect"></param>
        public void Intersect(Rectangle2D rect)
        {
            Rectangle2D result = Intersect(rect, this);

            x = result.X;
            y = result.Y;
            width = result.Width;
            height = result.Height;
        }

        /// <summary>
        /// Adjusts the location of this rectangle by the specified amount.
        /// </summary>
        /// <param name="pos"></param>
        public void Offset(Point2D pos)
        {
            Offset(pos.X, pos.Y);
        }

        /// <summary>
        /// Offset - translate the Location by the offset provided.
        /// If this is Empty, this method is illegal.
        /// </summary>
        /// <param name="offsetVector"></param>
        public void Offset(Vector2D offsetVector)
        {
            Offset(offsetVector.I, offsetVector.J);
        }

        /// <summary>
        /// Adjusts the location of this rectangle by the specified amount.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Offset(double x, double y)
        {
            if (IsEmpty) throw new InvalidOperationException("Cannot call method.");
            this.x += x;
            this.y += y;
        }

        /// <summary>
        /// Inflates this <see cref="Rectangle2D"/> by the specified amount.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Inflate(double x, double y)
        {
            this.x -= x;
            this.y -= y;
            width += 2 * x;
            height += 2 * y;
        }

        /// <summary>
        /// Inflates this <see cref="Rectangle2D"/> by the specified amount.
        /// </summary>
        /// <param name="size"></param>
        public void Inflate(Size2D size)
        {
            Inflate(size.Width, size.Height);
        }

        ///// <summary>
        ///// Updates rectangle to be the bounds of the original value transformed
        ///// by the matrix.
        ///// The Empty Rectangle2D is not affected by this call.        
        ///// </summary>
        ///// <param name="matrix"> Matrix </param>
        //public void Transform(Matrix2D matrix)
        //{
        //    Matrix2D.TransformRect(ref this, ref matrix);
        //}

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool Contains(Point2D point) => Intersections.Contains(this, point) != Inclusion.Outside;

        /// <summary>
        /// Determines if the rectangular region represented by <paramref name="rect"/> is entirely contained within the rectangular region represented by  this <see cref="Rectangle2D"/> .
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        [Pure]
        public bool Contains(Rectangle2D rect) => Intersections.Contains(this, rect);

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        [Pure]
        public bool IntersectsWith(Rectangle2D rect) => Intersections.RectangleRectangle(this, rect);

        /// <summary>
        /// Convert a rectangle to an array of it's corner points.
        /// </summary>
        /// <returns>An array of points representing the corners of a rectangle.</returns>
        public List<Point2D> ToPoints() => new List<Point2D>
            {
                Location,
                new Point2D(Right, Top),
                new Point2D(Right, Bottom),
                new Point2D(Left, Bottom)
            };

        /// <summary>
        /// Gets the hash code for this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => unchecked((int)((uint)X ^
(((uint)Y << 13) | ((uint)Y >> 19)) ^
(((uint)Width << 26) | ((uint)Width >> 6)) ^
(((uint)Height << 7) | ((uint)Height >> 25))));

        /// <summary>
        /// Convert a rectangle to a polygon containing an array of the rectangle's corner points.
        /// </summary>
        /// <returns>An array of points representing the corners of a rectangle.</returns>
        public Polygon ToPolygon() => new Polygon(ToPoints());

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
        internal override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Rectangle2D);
            //return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4}}}", nameof(Size2D), nameof(Width), Width, nameof(Height), Height);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Rectangle2D)}{{{nameof(X)}={x},{nameof(Y)}={y},{nameof(Width)}={width},{nameof(Height)}={height}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
