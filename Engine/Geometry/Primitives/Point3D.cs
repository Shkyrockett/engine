using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using static System.Math;
using static Engine.Geometry.Maths;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [DisplayName(nameof(Point3D))]
    //[TypeConverter(typeof(Point2DConverter))]
    public class Point3D
        : IFormattable
    {
        #region Implementations

        /// <summary>
        /// An Empty <see cref="Point3D"/>.
        /// </summary>
        public static readonly Point3D Empty = new Point3D();

        /// <summary>
        /// A Unit <see cref="Point3D"/>.
        /// </summary>
        public static readonly Point3D Unit = new Point3D(1, 1, 1);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new default instance of the <see cref="Point3D"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Point3D()
            : this(0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new  instance of the <see cref="Point3D"/> class.
        /// </summary>
        /// <param name="point"></param>
        /// <remarks></remarks>
        public Point3D(Point3D point)
            : this(point.X, point.Y, point.Z)
        {
        }

        /// <summary>
        /// Initializes a new  instance of the <see cref="Point3D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        /// <remarks></remarks>
        public Point3D(Tuple<double, double, double> tuple)
            : this(tuple.Item1, tuple.Item2, tuple.Item3)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="x">The x component of the Point.</param>
        /// <param name="y">The y component of the Point.</param>
        /// <param name="z">The z component of the Point.</param>
        /// <remarks></remarks>
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Properties

        /// <summary>
        /// X component of a <see cref="Point3D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double X { get; set; }

        /// <summary>
        /// Y component of a <see cref="Point3D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double Y { get; set; }

        /// <summary>
        /// Z component of a <see cref="Point3D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double Z { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point3D"/> is empty.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsEmpty
            => Abs(X) < DoubleEpsilon
            && Abs(Y) < DoubleEpsilon
            && Abs(Z) < DoubleEpsilon;

        #endregion

        #region Operators

        /// <summary>
        /// Unary addition operator.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point3D operator +(Point3D value) => new Point3D(+value.X, +value.Y, +value.Z);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point3D"/> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point3D operator +(Point3D value, double addend) => new Point3D(value.X + addend, value.Y + addend, value.Z + addend);

        /// <summary>
        /// Add two <see cref="Point3D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point3D operator +(Point3D value, Point3D addend) => new Point3D(value.X + addend.X, value.Y + addend.Y, value.Z + addend.Z);

        /// <summary>
        /// Unary subtraction operator.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point3D operator -(Point3D value) => new Point3D(-value.X, -value.Y, -value.Z);

        /// <summary>
        /// Subtract a <see cref="Point3D"/> from a <see cref="double"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point3D operator -(Point3D value, double subend) => new Point3D(value.X - subend, value.Y - subend, value.Z - subend);

        /// <summary>
        /// Subtract a <see cref="Point2D"/> from another <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point3D operator -(Point3D value, Point3D subend) => new Point3D(value.X - subend.X, value.Y - subend.Y, value.Z - subend.Z);

        /// <summary>
        /// Scale a point
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Point3D operator *(double value, Point3D factor) => new Point3D(value * factor.X, value * factor.Y, value * factor.Z);

        /// <summary>
        /// Scale a point.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Point3D operator *(Point3D value, double factor) => new Point3D(value.X * factor, value.Y * factor, value.Z * factor);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point3D"/> classes.
        /// </summary>
        /// <param name="divisor">The original value</param>
        /// <param name="dividend">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point3D operator /(Point3D divisor, double dividend) => new Point3D(divisor.X / dividend, divisor.Y / dividend, divisor.Z / dividend);

        /// <summary>
        /// Compares two <see cref="Point3D"/> objects. 
        /// The result specifies whether the values of the <see cref="X"/> and <see cref="Y"/> 
        /// values of the two <see cref="Point3D"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Point3D left, Point3D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Point3D"/> objects. 
        /// The result specifies whether the values of the <see cref="X"/> or <see cref="Y"/> 
        /// values of the two <see cref="Point3D"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Point3D left, Point3D right) => !Equals(left, right);

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Point3D a, Point3D b) => Equals(a, b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Point3D a, Point3D b) => (a?.X == b?.X) & (a?.Y == b?.Y) & (a?.Z == b?.Z);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Point3D && Equals(this, obj as Point3D);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Point3D value) => Equals(this, value);

        #endregion

        #region Factories

        /// <summary>
        /// Create a Random <see cref="Point3D"/>.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        public static Point3D Random()
            => new Point3D((2 * Maths.RandomNumberGenerator.NextDouble()) - 1, (2 * Maths.RandomNumberGenerator.NextDouble()) - 1, (2 * Maths.RandomNumberGenerator.NextDouble()) - 1);

        /// <summary>
        /// Parse a string for a <see cref="Point3D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Point3D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Point3D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [Pure]
        public static Point3D Parse(string source)
        {
            var tokenizer = new Tokenizer(source, CultureInfo.InvariantCulture);
            var value = new Point3D(
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture),
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture),
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture)
                );
            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Pure]
        public override int GetHashCode() => X.GetHashCode() ^
       Y.GetHashCode() ^
       Z.GetHashCode();

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Point3D"/> class.
        /// </summary>
        /// <returns></returns>
        [Pure]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Point3D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Point3D"/> class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Point3D"/> class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Point3D);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Point3D)}{{{nameof(X)}={X}{sep}{nameof(Y)}={Y}{sep}{nameof(Z)}={Z}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
