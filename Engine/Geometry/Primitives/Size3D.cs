// <copyright file="Size3D.cs" company="Shkyrockett" >
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

namespace Engine
{
    /// <summary>
    /// The size3D struct.
    /// </summary>
    [ComVisible(true)]
    [DataContract, Serializable]
    //[TypeConverter(typeof(Size3DConverter))]
    [TypeConverter(typeof(StructConverter<Size3D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Size3D
        : IVector<Size3D>
    {
        #region Implementations
        /// <summary>
        /// Represents a <see cref="Size3D"/> that has <see cref="Width"/>, <see cref="Height"/>, and <see cref="Depth"/> values set to zero.
        /// </summary>
        public static readonly Size3D Empty = new Size3D(0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Size3D"/> that has <see cref="Width"/>, <see cref="Height"/>, and <see cref="Depth"/> values set to 1.
        /// </summary>
        public static readonly Size3D Unit = new Size3D(1d, 1d, 1d);

        /// <summary>
        /// Represents a <see cref="Size3D"/> that has <see cref="Width"/>, <see cref="Height"/>, and <see cref="Depth"/> values set to NaN.
        /// </summary>
        public static readonly Size3D NaN = new Size3D(double.NaN, double.NaN, double.NaN);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Size3D"/> class.
        /// </summary>
        /// <param name="size"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size3D(Size3D size)
            : this(size.Width, size.Height, size.Depth)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size3D"/> class.
        /// </summary>
        /// <param name="point"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size3D(Point3D point)
            : this(point.X, point.Y, point.Z)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size3D"/> class.
        /// </summary>
        /// <param name="width">The Width component of the Size.</param>
        /// <param name="height">The Height component of the Size.</param>
        /// <param name="depth">The Depth component of the Size.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size3D(double width, double height, double depth)
            : this()
        {
            Width = width;
            Height = height;
            Depth = depth;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size3D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size3D((double Width, double Height, double Depth) tuple)
            : this()
        {
            (Width, Height, Depth) = tuple;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Size3D"/> to a <see cref="ValueTuple{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="depth">The depth.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double width, out double height, out double depth)
        {
            width = Width;
            height = Height;
            depth = Depth;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the Width component of a <see cref="Size3D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the Height component of a <see cref="Size3D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the Depth component of a <see cref="Size3D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Depth { get; set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Size3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator +(Size3D value) => Operations.UnaryAdd3D(value.Width, value.Height, value.Depth);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point3D"/> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator +(Size3D value, double addend) => Operations.Add3D(value.Width, value.Height, value.Depth, addend);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point3D"/> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator +(double value, Size3D addend) => Operations.Add3D(addend.Width, addend.Height, addend.Depth, value);

        /// <summary>
        /// Add two <see cref="Size3D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator +(Size3D value, Size3D addend) => Operations.Add3D(value.Width, value.Height, value.Depth, addend.Width, addend.Height, addend.Depth);

        /// <summary>
        /// Add two <see cref="Size3D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator +(Size3D value, Point3D addend) => Operations.Add3D(value.Width, value.Height, value.Depth, addend.X, addend.Y, addend.Z);

        /// <summary>
        /// Add a <see cref="Point3D"/> and a <see cref="Size3D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator +(Point3D value, Size3D addend) => Operations.Add3D(value.X, value.Y, value.Z, addend.Width, addend.Height, addend.Depth);

        /// <summary>
        /// Add a <see cref="Size3D"/> to a <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator +(Size3D value, Vector3D addend) => Operations.Add3D(value.Width, value.Height, value.Depth, addend.I, addend.J, addend.K);

        /// <summary>
        /// Add a <see cref="Vector3D"/> and a <see cref="Size3D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator +(Vector3D value, Size3D addend) => Operations.Add3D(value.I, value.J, value.K, addend.Width, addend.Height, addend.Depth);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Size3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator -(Size3D value) => Operations.UnaryNegate3D(value.Width, value.Height, value.Depth);

        /// <summary>
        /// Subtract a <see cref="Size3D"/> from a <see cref="double"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator -(Size3D value, double subend) => Operations.SubtractSubtrahend3D(value.Width, value.Height, value.Depth, subend);

        /// <summary>
        /// Subtract a <see cref="double"/> value from a <see cref="Size3D"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator -(double value, Size3D subend) => Operations.SubtractSubtrahend3D(value, subend.Width, subend.Height, subend.Depth);

        /// <summary>
        /// Subtract a <see cref="Size3D"/> from another <see cref="Size3D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator -(Size3D value, Size3D subend) => Operations.Subtract3D(value.Width, value.Height, value.Depth, subend.Width, subend.Height, subend.Depth);

        /// <summary>
        /// Subtract a <see cref="Size3D"/> from a <see cref="Point3D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator -(Size3D value, Point3D subend) => Operations.Subtract3D(value.Width, value.Height, value.Depth, subend.X, subend.Y, subend.Z);

        /// <summary>
        /// Subtract a <see cref="Point3D"/> from another <see cref="Size3D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator -(Point3D value, Size3D subend) => Operations.Subtract3D(value.X, value.Y, value.Z, subend.Width, subend.Height, subend.Depth);

        /// <summary>
        /// Subtract a <see cref="Size3D"/> from a <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator -(Size3D value, Vector3D subend) => Operations.Subtract3D(value.Width, value.Height, value.Depth, subend.I, subend.J, subend.K);

        /// <summary>
        /// Subtract a <see cref="Vector3D"/> from another <see cref="Size3D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator -(Vector3D value, Size3D subend) => Operations.Subtract3D(value.I, value.J, value.K, subend.Width, subend.Height, subend.Depth);

        /// <summary>
        /// Scale a point
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator *(double value, Size3D factor) => Operations.Scale3D(factor.Width, factor.Height, factor.Depth, value);

        /// <summary>
        /// Scale a point.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator *(Size3D value, double factor) => Operations.Scale3D(value.Width, value.Height, value.Depth, factor);

        /// <summary>
        /// Scale a Size3D
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator *(Size3D value, Size3D factor) => Operations.ParametricScale3D(value.Width, value.Height, value.Depth, factor.Width, factor.Height, factor.Depth);

        /// <summary>
        /// Scale a Point
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator *(Point3D value, Size3D factor) => Operations.ParametricScale3D(value.X, value.Y, value.Z, factor.Width, factor.Height, factor.Depth);

        /// <summary>
        /// Scale a Point
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator *(Size3D value, Point3D factor) => Operations.ParametricScale3D(value.Width, value.Height, value.Depth, factor.X, factor.Y, factor.Z);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator *(Vector3D value, Size3D factor) => Operations.ParametricScale3D(value.I, value.J, value.K, factor.Width, factor.Height, factor.Depth);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator *(Size3D value, Vector3D factor) => Operations.ParametricScale3D(value.Width, value.Height, value.Depth, factor.I, factor.J, factor.K);

        /// <summary>
        /// Divide a <see cref="Size3D"/> by a <see cref="double"/> value.
        /// </summary>
        /// <param name="divisor"></param>
        /// <param name="dividend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator /(Size3D divisor, double dividend) => Operations.DivideByDividend3D(divisor.Width, divisor.Height, divisor.Depth, dividend);

        /// <summary>
        /// Divide a <see cref="double"/> by a <see cref="Size3D"/> value.
        /// </summary>
        /// <param name="divisor"></param>
        /// <param name="dividend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator /(double divisor, Size3D dividend) => Operations.DivideDivisor3D(divisor, dividend.Width, dividend.Height, dividend.Depth);

        /// <summary>
        /// Divide a Size3D
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator /(Size3D value, Size3D factor) => Operations.ParametricDivide3D(value.Width, value.Height, value.Depth, factor.Width, factor.Height, factor.Depth);

        /// <summary>
        /// Divide a Point
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator /(Point3D value, Size3D factor) => Operations.ParametricDivide3D(value.X, value.Y, value.Z, factor.Width, factor.Height, factor.Depth);

        /// <summary>
        /// Divide a Point
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator /(Size3D value, Point3D factor) => Operations.ParametricDivide3D(value.Width, value.Height, value.Depth, factor.X, factor.Y, factor.Z);

        /// <summary>
        /// Divide a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator /(Vector3D value, Size3D factor) => Operations.ParametricDivide3D(value.I, value.J, value.K, factor.Width, factor.Height, factor.Depth);

        /// <summary>
        /// Divide a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator /(Size3D value, Vector3D factor) => Operations.ParametricScale3D(value.Width, value.Height, value.Depth, factor.I, factor.J, factor.K);

        /// <summary>
        /// Compares two <see cref="Size3D"/> objects.
        /// The result specifies whether the values of the <see cref="Width"/>, <see cref="Height"/> and <see cref="Depth"/>
        /// values of the two <see cref="Size3D"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Size3D left, Size3D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Size3D"/> objects.
        /// The result specifies whether the values of the <see cref="Width"/>, <see cref="Height"/> or <see cref="Depth"/>
        /// values of the two <see cref="Size3D"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Size3D left, Size3D right) => !Equals(left, right);

        /// <summary>
        /// Converts the specified <see cref="Vector3D"/> structure to a <see cref="Size3D"/> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3D"/> to be converted.</param>
        /// <returns>
        /// Size - A Size equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size3D(Vector3D vector) => new Size3D(vector.I, vector.J, vector.K);

        /// <summary>
        /// Explicit conversion to Vector.
        /// </summary>
        /// <param name="size"> Size - the Size to convert to a Vector </param>
        /// <returns>
        /// Vector - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector3D(Size3D size) => new Vector3D(size.Width, size.Height, size.Depth);

        /// <summary>
        /// Converts the specified <see cref="Point3D"/> structure to a <see cref="Size3D"/> structure.
        /// </summary>
        /// <param name="point">The <see cref="Point3D"/> to be converted.</param>
        /// <returns>
        /// Size - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size3D(Point3D point) => new Size3D(point.X, point.Y, point.Z);

        /// <summary>
        /// Converts the specified <see cref="Size3D"/> to a <see cref="Point3D"/>.
        /// </summary>
        /// <param name="size"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Point3D(Size3D size) => new Point3D(size.Width, size.Height, size.Depth);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"> Size - the Size to convert to a Vector </param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Size3D((double Width, double Height, double Depth) tuple) => new Size3D(tuple);

        /// <summary>
        /// Converts the specified <see cref="Size3D"/> structure to a <see cref="ValueTuple{T1, T2, T3}"/> structure.
        /// </summary>
        /// <param name="size">The <see cref="Size3D"/> to be converted.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double Width, double Height, double Depth) (Size3D size) => (size.Width, size.Height, size.Depth);
        #endregion Operators

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Size3D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Size3D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Size3D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [ParseMethod]
        public static Size3D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Size3D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Size3D"/> data </param>
        /// <param name="provider"></param>
        /// <returns>
        /// Returns an instance of the <see cref="Size3D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Size3D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            var firstToken = tokenizer.NextTokenRequired();

            // The token will already have had whitespace trimmed so we can do a simple string compare.
            var value = firstToken == nameof(Empty) ? Empty : new Size3D(
                Convert.ToDouble(firstToken, provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
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
        public override int GetHashCode() => Width.GetHashCode() ^ Height.GetHashCode() ^ Depth.GetHashCode();

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Size3D other) => (Width == other.Width) && (Height == other.Height) && (Depth == other.Depth);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => base.Equals(obj);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => base.ToString();

        /// <summary>
        /// Creates a string representation of this <see cref="Size3D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The <see cref="CultureInfo"/> provider.</param>
        /// <returns>A string representation of this <see cref="Size3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Size3D);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Size3D)}({nameof(Width)}:{Width.ToString(format, provider)}{s} {nameof(Height)}:{Height.ToString(format, provider)}{s} {nameof(Depth)}:{Depth.ToString(format, provider)})";
        }
        #endregion Methods
    }
}
