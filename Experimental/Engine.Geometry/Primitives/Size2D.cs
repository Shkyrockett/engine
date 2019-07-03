﻿// <copyright file="Size2D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;
using static Engine.Mathematics;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The size2d struct.
    /// </summary>
    [ComVisible(true)]
    [DataContract, Serializable]
    [TypeConverter(typeof(Size2DConverter))]
    //[TypeConverter(typeof(StructConverter<Size2D>))]
    [DebuggerDisplay("{nameof(Size2D)}({nameof(Width)}: {Width ?? double.NaN}, {nameof(Height)}: {Height ?? double.NaN})")]
    public struct Size2D
        : IVector<Size2D>
    {
        #region Implementations
        /// <summary>
        /// Represents a <see cref="Size2D"/> that has <see cref="Width"/>, and <see cref="Height"/> values set to zero.
        /// </summary>
        public static readonly Size2D Empty = new Size2D(0d, 0d);

        /// <summary>
        /// Represents a <see cref="Size2D"/> that has <see cref="Width"/>, and <see cref="Height"/> values set to 1.
        /// </summary>
        public static readonly Size2D Unit = new Size2D(1d, 1d);

        /// <summary>
        /// Represents a <see cref="Size2D"/> that has <see cref="Width"/>, and <see cref="Height"/> values set to NaN.
        /// </summary>
        public static readonly Size2D NaN = new Size2D(double.NaN, double.NaN);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="size"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size2D(Size2D size)
            : this(size.Width, size.Height)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="point"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size2D(Point2D point)
            : this(point.X, point.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="width">The Width component of the Size.</param>
        /// <param name="height">The Height component of the Size.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size2D(double width, double height)
            : this()
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size2D((double Width, double Height) tuple)
            : this()
        {
            (Width, Height) = tuple;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Size2D"/> to a <see cref="ValueTuple{T1, T2}"/>.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double width, out double height)
        {
            width = Width;
            height = Height;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the Width component of a <see cref="Size2D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the Height component of a <see cref="Size2D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Height { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point2D"/> is empty.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public bool IsEmpty
            => Abs(Width) < Epsilon
            && Abs(Height) < Epsilon;
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator +(Size2D value) => Operations.UnaryAdd2D(value.Width, value.Height);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D"/> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator +(Size2D value, double addend) => Operations.Add2D(value.Width, value.Height, addend);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D"/> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator +(double value, Size2D addend) => Operations.Add2D(addend.Width, addend.Height, value);

        /// <summary>
        /// Add two <see cref="Size2D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator +(Size2D value, Size2D addend) => Operations.Add2D(value.Width, value.Height, addend.Width, addend.Height);

        /// <summary>
        /// Add two <see cref="Size2D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator +(Size2D value, Point2D addend) => Operations.Add2D(value.Width, value.Height, addend.X, addend.Y);

        /// <summary>
        /// Add a <see cref="Point2D"/> and a <see cref="Size2D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator +(Point2D value, Size2D addend) => Operations.Add2D(value.X, value.Y, addend.Width, addend.Height);

        /// <summary>
        /// Add a <see cref="Size2D"/> to a <see cref="Vector2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(Size2D value, Vector2D addend) => Operations.Add2D(value.Width, value.Height, addend.I, addend.J);

        /// <summary>
        /// Add a <see cref="Vector2D"/> and a <see cref="Size2D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(Vector2D value, Size2D addend) => Operations.Add2D(value.I, value.J, addend.Width, addend.Height);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator -(Size2D value) => Operations.UnaryNegate2D(value.Width, value.Height);

        /// <summary>
        /// Subtract a <see cref="Size2D"/> from a <see cref="double"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator -(Size2D value, double subend) => Operations.SubtractSubtrahend2D(value.Width, value.Height, subend);

        /// <summary>
        /// Subtract a <see cref="double"/> value from a <see cref="Size2D"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator -(double value, Size2D subend) => Operations.SubtractSubtrahend2D(value, subend.Width, subend.Height);

        /// <summary>
        /// Subtract a <see cref="Size2D"/> from another <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator -(Size2D value, Size2D subend) => Operations.Subtract2D(value.Width, value.Height, subend.Width, subend.Height);

        /// <summary>
        /// Subtract a <see cref="Size2D"/> from a <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator -(Size2D value, Point2D subend) => Operations.Subtract2D(value.Width, value.Height, subend.X, subend.Y);

        /// <summary>
        /// Subtract a <see cref="Point2D"/> from another <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator -(Point2D value, Size2D subend) => Operations.Subtract2D(value.X, value.Y, subend.Width, subend.Height);

        /// <summary>
        /// Subtract a <see cref="Size2D"/> from a <see cref="Vector2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(Size2D value, Vector2D subend) => Operations.Subtract2D(value.Width, value.Height, subend.I, subend.J);

        /// <summary>
        /// Subtract a <see cref="Vector2D"/> from another <see cref="Size2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(Vector2D value, Size2D subend) => Operations.Subtract2D(value.I, value.J, subend.Width, subend.Height);

        /// <summary>
        /// Scale a point
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator *(double value, Size2D factor) => Operations.Scale2D(factor.Width, factor.Height, value);

        /// <summary>
        /// Scale a point.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator *(Size2D value, double factor) => Operations.Scale2D(value.Width, value.Height, factor);

        /// <summary>
        /// Scale a Size2D
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator *(Size2D value, Size2D factor) => Operations.ParametricScale2D(value.Width, value.Height, factor.Width, factor.Height);

        /// <summary>
        /// Scale a Point
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator *(Point2D value, Size2D factor) => Operations.ParametricScale2D(value.X, value.Y, factor.Width, factor.Height);

        /// <summary>
        /// Scale a Point
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator *(Size2D value, Point2D factor) => Operations.ParametricScale2D(value.Width, value.Height, factor.X, factor.Y);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator *(Vector2D value, Size2D factor) => Operations.ParametricScale2D(value.I, value.J, factor.Width, factor.Height);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator *(Size2D value, Vector2D factor) => Operations.ParametricScale2D(value.Width, value.Height, factor.I, factor.J);

        /// <summary>
        /// Divide a <see cref="Size2D"/> by a <see cref="double"/> value.
        /// </summary>
        /// <param name="divisor"></param>
        /// <param name="dividend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator /(Size2D divisor, double dividend) => Operations.DivideByDividend2D(divisor.Width, divisor.Height, dividend);

        /// <summary>
        /// Divide a <see cref="double"/> by a <see cref="Size2D"/> value.
        /// </summary>
        /// <param name="divisor"></param>
        /// <param name="dividend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator /(double divisor, Size2D dividend) => Operations.DivideDivisor2D(divisor, dividend.Width, dividend.Height);

        /// <summary>
        /// Divide a Size2D
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator /(Size2D value, Size2D factor) => Operations.ParametricDivide2D(value.Width, value.Height, factor.Width, factor.Height);

        /// <summary>
        /// Divide a Point
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator /(Point2D value, Size2D factor) => Operations.ParametricDivide2D(value.X, value.Y, factor.Width, factor.Height);

        /// <summary>
        /// Divide a Point
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator /(Size2D value, Point2D factor) => Operations.ParametricDivide2D(value.Width, value.Height, factor.X, factor.Y);

        /// <summary>
        /// Divide a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator /(Vector2D value, Size2D factor) => Operations.ParametricDivide2D(value.I, value.J, factor.Width, factor.Height);

        /// <summary>
        /// Divide a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator /(Size2D value, Vector2D factor) => Operations.ParametricScale2D(value.Width, value.Height, factor.I, factor.J);

        /// <summary>
        /// Compares two <see cref="Size2D"/> objects.
        /// The result specifies whether the values of the <see cref="Width"/> and <see cref="Height"/>
        /// values of the two <see cref="Size2D"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Size2D left, Size2D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Size2D"/> objects.
        /// The result specifies whether the values of the <see cref="Width"/> or <see cref="Height"/>
        /// values of the two <see cref="Size2D"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Size2D left, Size2D right) => !Equals(left, right);

        /// <summary>
        /// Explicit conversion to Size2D.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="vector">The <see cref="Vector2D"/> to be converted.</param>
        /// <returns>
        /// Size - A Size equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size2D(Vector2D size) => new Size2D(size.I, size.J);

        /// <summary>
        /// Explicit conversion to Vector.
        /// </summary>
        /// <param name="size"> Size - the Size to convert to a Vector </param>
        /// <returns>
        /// Vector - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector2D(Size2D size) => new Vector2D(size.Width, size.Height);

        /// <summary>
        /// Converts the specified <see cref="Point2D"/> to a <see cref="Size2D"/>.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="point">The <see cref="Point2D"/> to be converted.</param>
        /// <returns>
        /// Size - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size2D(Point2D size) => new Size2D(size.X, size.Y);

        /// <summary>
        /// Converts the specified <see cref="Size2D"/> to a <see cref="Point2D"/>.
        /// </summary>
        /// <param name="size"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Point2D(Size2D size) => new Point2D(size.Width, size.Height);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"> Size - the Size to convert to a Vector </param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Size2D((double Width, double Height) tuple) => new Size2D(tuple);

        /// <summary>
        /// Converts the specified <see cref="Point2D"/> structure to a <see cref="ValueTuple{T1, T2}"/> structure.
        /// </summary>
        /// <param name="point">The <see cref="Point2D"/> to be converted.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double Width, double Height)(Size2D point) => (point.Width, point.Height);
        #endregion Operators

        #region Factories
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
            var firstToken = tokenizer.NextTokenRequired();

            // The token will already have had whitespace trimmed so we can do a simple string compare.
            var value = firstToken == nameof(Empty) ? Empty : new Size2D(
                Convert.ToDouble(firstToken, provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider)
                );

            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }
        #endregion Factories

        #region Methods
        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(Width, Height);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Size2D other) => (Width == other.Width) && (Height == other.Height);
        public override bool Equals(object obj) => base.Equals(obj);
        public override string ToString() => base.ToString();

        /// <summary>
        /// Creates a string representation of this <see cref="Size2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The <see cref="CultureInfo"/> provider.</param>
        /// <returns>A string representation of this <see cref="Size2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Size2D);
            var s = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Size2D)}({nameof(Width)}: {Width.ToString(format, formatProvider)}{s} {nameof(Height)}: {Height.ToString(format, formatProvider)})";
        }
        #endregion Methods
    }
}
