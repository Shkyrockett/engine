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
    [DisplayName(nameof(Point2D))]
    [TypeConverter(typeof(Point2DConverter))]
    public class Point2D
        : IFormattable
    {
        #region Static Implementations

        /// <summary>
        /// An Empty <see cref="Point2D"/>.
        /// </summary>
        public static readonly Point2D Empty = new Point2D();

        /// <summary>
        /// A Unit <see cref="Point2D"/>.
        /// </summary>
        public static readonly Point2D Unit = new Point2D(1, 1);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new default instance of the <see cref="Point2D"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Point2D()
            : this(0, 0)
        { }

        /// <summary>
        /// Initializes a new  instance of the <see cref="Point2D"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Point2D(Point2D point)
            : this(point.X, point.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <remarks></remarks>
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// X component of a <see cref="Point2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double X { get; set; }

        /// <summary>
        /// Y component of a <see cref="Point2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double Y { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point2D"/> is empty.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsEmpty => X == 0 && Y == 0;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/1476497/multiply-two-point-objects</remarks>
        public static Point2D ComplexProduct(Point2D p1, Point2D p2)
        {
            return new Point2D(p1.X * p2.X - p1.Y * p2.Y, p1.X * p2.Y + p1.Y * p2.X);
        }

        #region Operators

        /// <summary>
        /// Unary addition operator.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point2D operator +(Point2D value)
        {
            return new Point2D(+value.X, +value.Y);
        }

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D"/> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D operator +(Point2D value, double addend)
        {
            return value.Add(addend);
        }

        /// <summary>
        /// Add two <see cref="Point2D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D operator +(Point2D value, Point2D addend)
        {
            return value.Add(addend);
        }

        /// <summary>
        /// Add two <see cref="Point2D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D operator +(Point2D value, Size2D addend)
        {
            return value.Add(addend);
        }

        /// <summary>
        /// Operator Point + Vector
        /// </summary>
        /// <param name="point"> The Point to be added to the Vector </param>
        /// <param name="vector"> The Vector to be added to the Point </param>
        /// <returns>
        /// Point - The result of the addition
        /// </returns>
        public static Point2D operator +(Point2D point, Vector2D vector)
        {
            return new Point2D(point.X + vector.I, point.Y + vector.J);
        }

        /// <summary>
        /// Unary subtraction operator.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point2D operator -(Point2D value)
        {
            return new Point2D(-value.X, -value.Y);
        }

        /// <summary>
        /// Subtract a <see cref="Point2D"/> from a <see cref="double"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D operator -(Point2D value, double subend)
        {
            return value.Subtract(subend);
        }

        /// <summary>
        /// Subtract a <see cref="Point2D"/> from another <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D operator -(Point2D value, Point2D subend)
        {
            return value.Subtract(subend);
        }

        /// <summary>
        /// Subtract a <see cref="Point2D"/> from another <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D operator -(Point2D value, Size2D subend)
        {
            return value.Subtract(subend);
        }

        /// <summary>
        /// Subtract a <see cref="Point2D"/> from another <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D operator -(Point2D value, Vector2D subend)
        {
            return value.Subtract(subend);
        }

        /// <summary>
        /// Scale a point
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point2D operator *(double value, Point2D factor)
        {
            return new Point2D(value * factor.X, value * factor.Y);
        }

        /// <summary>
        /// Scale a point.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Point2D operator *(Point2D value, double factor)
        {
            return new Point2D(value.X * factor, value.Y * factor);
        }

        /// <summary>
        /// Multiply a point by a matrix.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Point2D operator *(Point2D value, Matrix2D matrix)
        {
            return matrix.Transform(value);
        }

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D"/> classes.
        /// </summary>
        /// <param name="divisor">The original value</param>
        /// <param name="dividend">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D operator /(Point2D divisor, double dividend)
        {
            return new Point2D(divisor.X / dividend, divisor.Y / dividend);
        }

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool Compare(Point2D a, Point2D b)
        {
            return Equals(a, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Equals(Point2D a, Point2D b)
        {
            return (a.X == b.X) & (a.Y == b.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Point2D && Equals(this, (Point2D)obj);
        }

        /// <summary>
        /// Compares two <see cref="Point2D"/> objects. 
        /// The result specifies whether the values of the <see cref="X"/> and <see cref="Y"/> 
        /// values of the two <see cref="Point2D"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Point2D left, Point2D right)
        {
            if (left == null || right == null) return false;
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>
        /// Compares two <see cref="Point2D"/> objects. 
        /// The result specifies whether the values of the <see cref="X"/> or <see cref="Y"/> 
        /// values of the two <see cref="Point2D"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Point2D left, Point2D right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Explicit conversion to Size.  Note that since Size cannot contain negative values,
        /// the resulting size will contains the absolute values of X and Y
        /// </summary>
        /// <returns>
        /// Size - A Size equal to this Point
        /// </returns>
        /// <param name="point"> Point - the Point to convert to a Size </param>
        public static explicit operator Size2D(Point2D point)
        {
            return new Size2D(Math.Abs(point.X), Math.Abs(point.Y));
        }

        /// <summary>
        /// Explicit conversion to Vector
        /// </summary>
        /// <returns>
        /// Vector - A Vector equal to this Point
        /// </returns>
        /// <param name="point"> Point - the Point to convert to a Vector </param>
        public static explicit operator Vector2D(Point2D point)
        {
            return new Vector2D(point.X, point.Y);
        }

        /// <summary>
        /// Explicit conversion to Vector
        /// </summary>
        /// <returns>
        /// Vector - A Vector equal to this Point
        /// </returns>
        /// <param name="point"> Point - the Point to convert to a Vector </param>
        public static explicit operator Point2D(Vector2D point)
        {
            return new Point2D(point.I, point.J);
        }

        #endregion

        /// <summary>
        /// Create a Random <see cref="Point2D"/>.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D Random() => new Point2D((2 * Maths.RandomNumberGenerator.NextDouble()) - 1, (2 * Maths.RandomNumberGenerator.NextDouble()) - 1);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^
                   Y.GetHashCode();
        }

        /// <summary>
        /// Parse - returns an instance converted from the provided string using
        /// the culture "en-US"
        /// <param name="source"> string with Point data </param>
        /// </summary>
        public static Point2D Parse(string source)
        {
            TokenizerHelper th = new TokenizerHelper(source, CultureInfo.InvariantCulture);

            Point2D value;

            String firstToken = th.NextTokenRequired();

            value = new Point2D(
                Convert.ToDouble(firstToken, CultureInfo.InvariantCulture),
                Convert.ToDouble(th.NextTokenRequired(), CultureInfo.InvariantCulture));

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
            return string.Format(provider, "{0}{{{1}={2:" + format + "},{3}={4:" + format + "}}}", nameof(Point2D), nameof(X), X, nameof(Y), Y);
        }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Point2D"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{nameof(Point2D)}({nameof(X)}={X},{nameof(Y)}={Y})";
    }
}
