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
    public class Point2D
    {
        #region Static Implementations
        /// <summary>
        /// A Unit <see cref="Point2D"/>.
        /// </summary>
        public static readonly Point2D AUnit = new Point2D(1, 1);

        /// <summary>
        /// An Empty <see cref="Point2D"/>.
        /// </summary>
        public static readonly Point2D Empty = new Point2D();
        #endregion

        #region Private Fields

        /// <summary>
        /// X component of a <see cref="Point2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        private double x;

        /// <summary>
        /// Y component of a <see cref="Point2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        private double y;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Point2D"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Point2D()
            : this(0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <remarks></remarks>
        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region Properties
        /// <summary>
        /// X component of a <see cref="Point2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Y component of a <see cref="Point2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point2D"/> is empty.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsEmpty
        {
            get { return x == 0 && y == 0; }
        }
        
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
            return new Point2D(p1.x * p2.x - p1.y * p2.y, p1.x * p2.y + p1.y * p2.x);
        }

        #region Operators
        /// <summary>
        /// Unary addition operator.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Point2D operator +(Point2D p)
        {
            return new Point2D(+p.X, +p.Y);
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
        /// Operator Point + Vector
        /// </summary>
        /// <param name="point"> The Point to be added to the Vector </param>
        /// <param name="vector"> The Vector to be added to the Point </param>
        /// <returns>
        /// Point - The result of the addition
        /// </returns>
        public static Point2D operator +(Point2D point, Vector2D vector)
        {
            return new Point2D(point.x + vector.I, point.y + vector.J);
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
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Point2D)) return false;
            Point2D comp = (Point2D)obj;
            return comp.X == X && comp.Y == Y && comp.GetType().Equals(GetType());
        }

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
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //if (this == null) return nameof(Point2D);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4}}}", nameof(Point2D), nameof(X), x, nameof(Y), y);
        }
    }
}
