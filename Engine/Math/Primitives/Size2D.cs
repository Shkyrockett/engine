// <copyright file="Size2D.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using static System.Math;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [TypeConverter(typeof(StructConverter<Size2D>))]
    public struct Size2D
        : IVector<Size2D>
    {
        #region Implementations

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
        public Size2D((double X, double Y) tuple)
            => (Width, Height) = tuple;

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
        [XmlAttribute, SoapAttribute]
        public double Width { get; set; }

        /// <summary>
        /// Height component of a <see cref="Size2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute, SoapAttribute]
        public double Height { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point2D"/> is empty.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public bool IsEmpty
            => Abs(Width) < Epsilon
            && Abs(Height) < Epsilon;

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Size2D operator +(Size2D value)
            => new Size2D(+value.Width, +value.Height);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D"/> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator +(Size2D value, double addend)
            => value.Add(addend);

        /// <summary>
        /// Add two <see cref="Size2D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator +(Size2D value, Point2D addend)
            => value.Add(addend);

        /// <summary>
        /// Add two <see cref="Size2D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator +(Size2D value, Size2D addend)
            => value.Add(addend);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Size2D operator -(Size2D value)
            => new Size2D(-value.Width, -value.Height);

        /// <summary>
        /// Subtract a <see cref="Size2D"/> from a <see cref="double"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator -(Size2D value, double subend)
            => value.Subtract(subend);

        /// <summary>
        /// Subtract a <see cref="Size2D"/> from another <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator -(Size2D value, Point2D subend)
            => value.Subtract(subend);

        /// <summary>
        /// Subtract a <see cref="Size2D"/> from another <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator -(Size2D value, Size2D subend)
            => value.Subtract(subend);

        /// <summary>
        /// Divide a <see cref="Size2D"/> by a <see cref="double"/> value.
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D operator /(Size2D dividend, double divisor)
            => new Size2D(dividend.Width / divisor, dividend.Height / divisor);

        /// <summary>
        /// Compares two <see cref="Size2D"/> objects. 
        /// The result specifies whether the values of the <see cref="Width"/> and <see cref="Height"/> 
        /// values of the two <see cref="Size2D"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Size2D left, Size2D right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Size2D"/> objects. 
        /// The result specifies whether the values of the <see cref="Width"/> or <see cref="Height"/> 
        /// values of the two <see cref="Size2D"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Size2D left, Size2D right)
            => !Equals(left, right);

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Size2D a, Size2D b)
            => Equals(a, b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Size2D a, Size2D b)
            => (a.Width == b.Width) & (a.Height == b.Height);

        /// <summary>
        /// Tests to see whether the specified object is a <see cref="Size2D"/>
        /// with the same dimensions as this <see cref="Size2D"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Size2D && Equals(this, (Size2D)obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Size2D value)
            => Equals(this, value);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"> Size - the Size to convert to a Vector </param>
        public static implicit operator Size2D((double X, double Y) tuple)
            => new Size2D(tuple);

        /// <summary>
        /// Explicit conversion to Vector.
        /// </summary>
        /// <returns>
        /// Vector - A Vector equal to this Size
        /// </returns>
        /// <param name="size"> Size - the Size to convert to a Vector </param>
        public static explicit operator Vector2D(Size2D size)
            => new Vector2D(size.Width, size.Height);

        /// <summary>
        /// Converts the specified <see cref="Size2D"/> to a
        ///    <see cref="Point2D"/>.
        /// </summary>
        /// <param name="size"></param>
        public static explicit operator Point2D(Size2D size)
            => new Point2D(size.Width, size.Height);

        #endregion

        #region Factories

        /// <summary>
        /// Create a Random <see cref="Size2D"/>.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size2D Random()
            => new Size2D((2 * RandomNumberGenerator.NextDouble()) - 1, (2 * RandomNumberGenerator.NextDouble()) - 1);

        /// <summary>
        /// Parse a string for a <see cref="Size2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Size2D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Size2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [ParseMethod]
        public static Size2D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Size2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Size2D"/> data </param>
        /// <param name="provider"></param>
        /// <returns>
        /// Returns an instance of the <see cref="Size2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Size2D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            Size2D value;
            string firstToken = tokenizer.NextTokenRequired();

            // The token will already have had whitespace trimmed so we can do a
            // simple string compare.
            if (firstToken == "Empty")
            {
                value = Empty;
            }
            else
            {
                value = new Size2D(
                    Convert.ToDouble(firstToken, provider),
                    Convert.ToDouble(tokenizer.NextTokenRequired(), provider));
            }

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
        public override int GetHashCode() => Width.GetHashCode()
    ^ Height.GetHashCode();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Point2D ToPoint2D() => (Point2D)this;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Size2D Truncate() => new Size2D((int)Width, (int)Height);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Size2D"/> struct.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Size2D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Size2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Size2D"/> struct based on the format string
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
            if (this == null) return nameof(Size2D);
            //return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4}}}", nameof(Size2D), nameof(Width), Width, nameof(Height), Height);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Size2D)}({nameof(Width)}={Width}{sep}{nameof(Height)}={Height})";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
