using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Size2D
    {
        #region Static Implementations
        /// <summary>
        /// A Unit <see cref="Size2D"/>.
        /// </summary>
        public static readonly Size2D AUnit = new Size2D(1, 1);

        /// <summary>
        /// An Empty <see cref="Size2D"/>.
        /// </summary>
        public static readonly Size2D Empty = new Size2D();
        #endregion

        #region Private Fields
        /// <summary>
        /// Width component of a <see cref="Size2D"/> class.
        /// </summary>
        /// <remarks></remarks>
        private double width;

        /// <summary>
        /// Height component of a <see cref="Size2D"/> class.
        /// </summary>
        /// <remarks></remarks>
        private double height;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Size2D()
            : this(0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Size2D(Point2D point)
            : this(point.X, point.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <remarks></remarks>
        public Size2D(double width, double height)
        {
            // If negative sizes are prohibited, then it would be impossible to inflate a rectangle in the negative direction to shrink it.
            //if (width < 0 || height < 0) throw new ArgumentException("Width and Height cannot be Negative.");
            this.width = width;
            this.height = height;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Width component of a <see cref="Size2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Height component of a <see cref="Size2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point2D"/> is empty.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsEmpty
        {
            get { return width == 0 && height == 0; }
        }

        #endregion

        #region Operators
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
            return new Vector2D(size.width, size.height);
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
        /// Tests to see whether the specified object is a <see cref="Size2D"/>
        /// with the same dimensions as this <see cref="Size2D"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Size2D)) return false;
            Size2D comp = (Size2D)obj;
            return (comp.Width == Width) && (comp.Height == Height) && (comp.GetType().Equals(GetType()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Width.GetHashCode() ^
                       Height.GetHashCode();
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
            return new Size2D((int)width,(int)height);
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

            String firstToken = th.NextTokenRequired();

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
        /// Creates a human-readable string that represents this <see cref="Size2D"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //if (this == null) return nameof(Size2D);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4}}}", nameof(Size2D), nameof(Width), width, nameof(Height), height);
        }
    }
}
