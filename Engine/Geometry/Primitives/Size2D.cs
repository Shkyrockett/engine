using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [DisplayName(nameof(Size2D))]
    [TypeConverter(typeof(Size2DConverter))]
    public struct Size2D
        : IFormattable
    {
        #region Static Implementations

        /// <summary>
        /// An Empty <see cref="Size2D"/>.
        /// </summary>
        public static readonly Size2D Empty = new Size2D();

        /// <summary>
        /// A Unit <see cref="Size2D"/>.
        /// </summary>
        public static readonly Size2D Unit = new Size2D(1, 1);

        #endregion

        #region Constructors

        ///// <summary>
        ///// Initializes a new default instance of the <see cref="Size2D"/> class.
        ///// </summary>
        ///// <remarks></remarks>
        //public Size2D()
        //    : this(0, 0)
        //{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="size"></param>
        /// <remarks></remarks>
        public Size2D(Size2D size)
            : this(size.Width, size.Height)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="point"></param>
        /// <remarks></remarks>
        public Size2D(Point2D point)
            : this(point.X, point.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        /// <remarks></remarks>
        public Size2D(Tuple<double, double> tuple)
            : this(tuple.Item1, tuple.Item2)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="width">The Width component of the Size.</param>
        /// <param name="height">The Height component of the Size.</param>
        /// <remarks></remarks>
        public Size2D(double width, double height)
        {
            // If negative sizes are prohibited, then it would be impossible to inflate a rectangle in the negative direction to shrink it.
            //if (width < 0 || height < 0) throw new ArgumentException("Width and Height cannot be Negative.");
            Width = width;
            Height = height;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Width component of a <see cref="Size2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double Width { get; set; }

        /// <summary>
        /// Height component of a <see cref="Size2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double Height { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point2D"/> is empty.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsEmpty => Width == 0 && Height == 0;

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Size2D operator +(Size2D value)
        {
            return new Size2D(+value.Width, +value.Height);
        }

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D"/> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator +(Size2D value, double addend)
        {
            return value.Add(addend);
        }

        /// <summary>
        /// Add two <see cref="Size2D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator +(Size2D value, Point2D addend)
        {
            return value.Add(addend);
        }

        /// <summary>
        /// Add two <see cref="Size2D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator +(Size2D value, Size2D addend)
        {
            return value.Add(addend);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Size2D operator -(Size2D value)
        {
            return new Size2D(-value.Width, -value.Height);
        }

        /// <summary>
        /// Subtract a <see cref="Size2D"/> from a <see cref="double"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator -(Size2D value, double subend)
        {
            return value.Subtract(subend);
        }

        /// <summary>
        /// Subtract a <see cref="Size2D"/> from another <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator -(Size2D value, Point2D subend)
        {
            return value.Subtract(subend);
        }

        /// <summary>
        /// Subtract a <see cref="Size2D"/> from another <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator -(Size2D value, Size2D subend)
        {
            return value.Subtract(subend);
        }

        /// <summary>
        /// Divide a <see cref="Size2D"/> by a <see cref="double"/> value.
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator /(Size2D dividend, double divisor)
        {
            return new Size2D(dividend.Width / divisor, dividend.Height / divisor);
        }

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool Compare(Size2D a, Size2D b)
        {
            return Equals(a, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Equals(Size2D a, Size2D b)
        {
            return (a.Width == b.Width) & (a.Height == b.Height);
        }

        /// <summary>
        /// Tests to see whether the specified object is a <see cref="Size2D"/>
        /// with the same dimensions as this <see cref="Size2D"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Size2D && Equals(this, (Size2D)obj);
        }

        /// <summary>
        /// Compares two <see cref="Size2D"/> objects. 
        /// The result specifies whether the values of the <see cref="Width"/> and <see cref="Height"/> 
        /// values of the two <see cref="Size2D"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Size2D left, Size2D right)
        {
            return left.Width == right.Width && left.Height == right.Height;
        }

        /// <summary>
        /// Compares two <see cref="Size2D"/> objects. 
        /// The result specifies whether the values of the <see cref="Width"/> or <see cref="Height"/> 
        /// values of the two <see cref="Size2D"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Size2D left, Size2D right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Explicit conversion to Vector.
        /// </summary>
        /// <returns>
        /// Vector - A Vector equal to this Size
        /// </returns>
        /// <param name="size"> Size - the Size to convert to a Vector </param>
        public static explicit operator Vector2D(Size2D size)
        {
            return new Vector2D(size.Width, size.Height);
        }

        /// <summary>
        /// Converts the specified <see cref="Size2D"/> to a
        ///    <see cref="Point2D"/>.
        /// </summary>
        /// <param name="size"></param>
        public static explicit operator Point2D(Size2D size)
        {
            return new Point2D(size.Width, size.Height);
        }

        #endregion

        /// <summary>
        /// Create a Random <see cref="Size2D"/>.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D Random() => new Size2D((2 * Maths.RandomNumberGenerator.NextDouble()) - 1, (2 * Maths.RandomNumberGenerator.NextDouble()) - 1);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Width.GetHashCode() ^ Height.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Point2D ToPoint2D()
        {
            return (Point2D)this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Size2D Truncate()
        {
            return new Size2D((int)Width, (int)Height);
        }

        /// <summary>
        /// Parse - returns an instance converted from the provided string using
        /// the culture "en-US"
        /// <param name="source"> string with Size data </param>
        /// </summary>
        public static Size2D Parse(string source)
        {
            TokenizerHelper th = new TokenizerHelper(source, CultureInfo.InvariantCulture);

            Size2D value;

            string firstToken = th.NextTokenRequired();

            // The token will already have had whitespace trimmed so we can do a
            // simple string compare.
            if (firstToken == "Empty")
            {
                value = Empty;
            }
            else
            {
                value = new Size2D(
                    Convert.ToDouble(firstToken, CultureInfo.InvariantCulture),
                    Convert.ToDouble(th.NextTokenRequired(), CultureInfo.InvariantCulture));
            }

            // There should be no more tokens in this string.
            th.LastTokenRequired();

            return value;
        }

        /// <summary>
        /// Creates a string representation of this object based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        string IFormattable.ToString(string format, IFormatProvider provider) => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this object based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            //if (this == null) return nameof(Size2D);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4}}}", nameof(Size2D), nameof(Width), Width, nameof(Height), Height);
        }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Size2D"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{nameof(Size2D)}({nameof(Width)}={Width},{nameof(Height)}={Height})";
    }
}
