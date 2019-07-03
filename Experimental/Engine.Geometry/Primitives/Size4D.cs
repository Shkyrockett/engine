﻿// <copyright file="Size4D.cs" company="Shkyrockett" >
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
    /// The size4D struct.
    /// </summary>
    [ComVisible(true)]
    [DataContract, Serializable]
    [TypeConverter(typeof(Size4DConverter))]
    //[TypeConverter(typeof(StructConverter<Size4D>))]
    [DebuggerDisplay("{nameof(Size4D)}({nameof(Width)}: {Width ?? double.NaN}, {nameof(Height)}: {Height ?? double.NaN}, {nameof(Depth)}: {Depth ?? double.NaN}, {nameof(Breadth)}: {Breadth ?? double.NaN})")]
    public struct Size4D
        : IVector<Size4D>
    {
        #region Implementations
        /// <summary>
        /// Represents a <see cref="Size4D"/> that has <see cref="Width"/>, <see cref="Height"/>, <see cref="Depth"/>, and <see cref="Breadth"/> values set to zero.
        /// </summary>
        public static readonly Size4D Empty = new Size4D(0d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Size4D"/> that has <see cref="Width"/>, <see cref="Height"/>, <see cref="Depth"/>, and <see cref="Breadth"/> values set to 1.
        /// </summary>
        public static readonly Size4D Unit = new Size4D(1d, 1d, 1d, 1d);

        /// <summary>
        /// Represents a <see cref="Size4D"/> that has <see cref="Width"/>, <see cref="Height"/>, <see cref="Depth"/>, and <see cref="Breadth"/> values set to NaN.
        /// </summary>
        public static readonly Size4D NaN = new Size4D(double.NaN, double.NaN, double.NaN, double.NaN);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Size4D"/> class.
        /// </summary>
        /// <param name="size"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size4D(Size4D size)
            : this(size.Width, size.Height, size.Depth, size.Breadth)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size4D"/> class.
        /// </summary>
        /// <param name="point"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size4D(Point4D point)
            : this(point.X, point.Y, point.Z, point.W)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size4D"/> class.
        /// </summary>
        /// <param name="width">The Width component of the Size.</param>
        /// <param name="height">The Height component of the Size.</param>
        /// <param name="depth">The Depth component of the Size.</param>
        /// <param name="breadth">The Breadth component of the Size.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size4D(double width, double height, double depth, double breadth)
            : this()
        {
            Width = width;
            Height = height;
            Depth = depth;
            Breadth = breadth;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size4D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size4D((double Width, double Height, double Depth, double Breadth) tuple)
            : this()
        {
            (Width, Height, Depth, Breadth) = tuple;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Size4D"/> to a <see cref="ValueTuple{T1, T2, T3, T4}"/>.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="breadth">The breadth.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double width, out double height, out double depth, out double breadth)
        {
            width = Width;
            height = Height;
            depth = Depth;
            breadth = Breadth;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the Width component of a <see cref="Size4D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Width { get; internal set; }

        /// <summary>
        /// Gets or sets the Height component of a <see cref="Size4D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Height { get; internal set; }

        /// <summary>
        /// Gets or sets the Depth component of a <see cref="Size4D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Depth { get; internal set; }

        /// <summary>
        /// Gets or sets the Breadth component of a <see cref="Size4D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Breadth { get; internal set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Size4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator +(Size4D value) => Operations.UnaryAdd4D(value.Width, value.Height, value.Depth, value.Breadth);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point4D"/> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator +(Size4D value, double addend) => Operations.Add4D(value.Width, value.Height, value.Depth, value.Breadth, addend);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point4D"/> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator +(double value, Size4D addend) => Operations.Add4D(addend.Width, addend.Height, addend.Depth, addend.Breadth, value);

        /// <summary>
        /// Add two <see cref="Size4D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator +(Size4D value, Size4D addend) => Operations.Add4D(value.Width, value.Height, value.Depth, value.Breadth, addend.Width, addend.Height, addend.Depth, addend.Breadth);

        /// <summary>
        /// Add two <see cref="Size4D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator +(Size4D value, Point4D addend) => Operations.Add4D(value.Width, value.Height, value.Depth, value.Breadth, addend.X, addend.Y, addend.Z, addend.W);

        /// <summary>
        /// Add a <see cref="Point4D"/> and a <see cref="Size4D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator +(Point4D value, Size4D addend) => Operations.Add4D(value.X, value.Y, value.Z, value.W, addend.Width, addend.Height, addend.Depth, addend.Breadth);

        /// <summary>
        /// Add a <see cref="Size4D"/> to a <see cref="Vector4D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(Size4D value, Vector4D addend) => Operations.Add4D(value.Width, value.Height, value.Depth, value.Breadth, addend.I, addend.J, addend.K, addend.L);

        /// <summary>
        /// Add a <see cref="Vector4D"/> and a <see cref="Size4D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(Vector4D value, Size4D addend) => Operations.Add4D(value.I, value.J, value.K, value.L, addend.Width, addend.Height, addend.Depth, addend.Breadth);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Size4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator -(Size4D value) => Operations.UnaryNegate4D(value.Width, value.Height, value.Depth, value.Breadth);

        /// <summary>
        /// Subtract a <see cref="Size4D"/> from a <see cref="double"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator -(Size4D value, double subend) => Operations.SubtractSubtrahend4D(value.Width, value.Height, value.Depth, value.Breadth, subend);

        /// <summary>
        /// Subtract a <see cref="double"/> value from a <see cref="Size4D"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator -(double value, Size4D subend) => Operations.SubtractSubtrahend4D(value, subend.Width, subend.Height, subend.Depth, subend.Breadth);

        /// <summary>
        /// Subtract a <see cref="Size4D"/> from another <see cref="Size4D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator -(Size4D value, Size4D subend) => Operations.Subtract4D(value.Width, value.Height, value.Depth, value.Breadth, subend.Width, subend.Height, subend.Depth, subend.Breadth);

        /// <summary>
        /// Subtract a <see cref="Size4D"/> from a <see cref="Point4D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator -(Size4D value, Point4D subend) => Operations.Subtract4D(value.Width, value.Height, value.Depth, value.Breadth, subend.X, subend.Y, subend.Z, subend.W);

        /// <summary>
        /// Subtract a <see cref="Point4D"/> from another <see cref="Size4D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator -(Point4D value, Size4D subend) => Operations.Subtract4D(value.X, value.Y, value.Z, value.W, subend.Width, subend.Height, subend.Depth, subend.Breadth);

        /// <summary>
        /// Subtract a <see cref="Size4D"/> from a <see cref="Vector4D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Size4D value, Vector4D subend) => Operations.Subtract4D(value.Width, value.Height, value.Depth, value.Breadth, subend.I, subend.J, subend.K, subend.L);

        /// <summary>
        /// Subtract a <see cref="Vector4D"/> from another <see cref="Size4D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Vector4D value, Size4D subend) => Operations.Subtract4D(value.I, value.J, value.K, value.L, subend.Width, subend.Height, subend.Depth, subend.Breadth);

        /// <summary>
        /// Scale a point
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator *(double value, Size4D factor) => Operations.Scale4D(factor.Width, factor.Height, factor.Depth, factor.Breadth, value);

        /// <summary>
        /// Scale a Size.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator *(Size4D value, double factor) => Operations.Scale4D(value.Width, value.Height, value.Depth, value.Breadth, factor);

        /// <summary>
        /// Scale a Size4D
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator *(Size4D value, Size4D factor) => Operations.ParametricScale4D(value.Width, value.Height, value.Depth, value.Breadth, factor.Width, factor.Height, factor.Depth, factor.Breadth);

        /// <summary>
        /// Scale a Point
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator *(Point4D value, Size4D factor) => Operations.ParametricScale4D(value.X, value.Y, value.Z, value.W, factor.Width, factor.Height, factor.Depth, factor.Breadth);

        /// <summary>
        /// Scale a Point
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator *(Size4D value, Point4D factor) => Operations.ParametricScale4D(value.Width, value.Height, value.Depth, value.Breadth, factor.X, factor.Y, factor.Z, factor.W);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator *(Vector4D value, Size4D factor) => Operations.ParametricScale4D(value.I, value.J, value.K, value.L, factor.Width, factor.Height, factor.Depth, factor.Breadth);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator *(Size4D value, Vector4D factor) => Operations.ParametricScale4D(value.Width, value.Height, value.Depth, value.Breadth, factor.I, factor.J, factor.K, factor.L);

        /// <summary>
        /// Divide a <see cref="Size4D"/> by a <see cref="double"/> value.
        /// </summary>
        /// <param name="divisor"></param>
        /// <param name="dividend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator /(Size4D divisor, double dividend) => Operations.DivideByDividend4D(divisor.Width, divisor.Height, divisor.Depth, divisor.Breadth, dividend);

        /// <summary>
        /// Divide a <see cref="double"/> by a <see cref="Size4D"/> value.
        /// </summary>
        /// <param name="divisor"></param>
        /// <param name="dividend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator /(double divisor, Size4D dividend) => Operations.DivideDivisor4D(divisor, dividend.Width, dividend.Height, dividend.Depth, dividend.Breadth);

        /// <summary>
        /// Divide a Size4D
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator /(Size4D value, Size4D factor) => Operations.ParametricDivide4D(value.Width, value.Height, value.Depth, value.Breadth, factor.Width, factor.Height, factor.Depth, factor.Breadth);

        /// <summary>
        /// Divide a Point
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator /(Point4D value, Size4D factor) => Operations.ParametricDivide4D(value.X, value.Y, value.Z, value.W, factor.Width, factor.Height, factor.Depth, factor.Breadth);

        /// <summary>
        /// Divide a Point
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator /(Size4D value, Point4D factor) => Operations.ParametricDivide4D(value.Width, value.Height, value.Depth, value.Breadth, factor.X, factor.Y, factor.Z, factor.W);

        /// <summary>
        /// Divide a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator /(Vector4D value, Size4D factor) => Operations.ParametricDivide4D(value.I, value.J, value.K, value.L, factor.Width, factor.Height, factor.Depth, factor.Breadth);

        /// <summary>
        /// Divide a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator /(Size4D value, Vector4D factor) => Operations.ParametricScale4D(value.Width, value.Height, value.Depth, value.Breadth, factor.I, factor.J, factor.K, factor.L);

        /// <summary>
        /// Compares two <see cref="Size4D"/> objects.
        /// The result specifies whether the values of the <see cref="Width"/>, <see cref="Height"/>, <see cref="Depth"/> and <see cref="Breadth"/>
        /// values of the two <see cref="Size4D"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Size4D left, Size4D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Size4D"/> objects.
        /// The result specifies whether the values of the <see cref="Width"/>, <see cref="Height"/>, <see cref="Depth"/> or <see cref="Breadth"/>
        /// values of the two <see cref="Size4D"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Size4D left, Size4D right) => !Equals(left, right);

        /// <summary>
        /// Converts the specified <see cref="Vector4D"/> structure to a <see cref="Size4D"/> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector4D"/> to be converted.</param>
        /// <returns>
        /// Size - A Size equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size4D(Vector4D vector) => new Size4D(vector.I, vector.J, vector.K, vector.L);

        /// <summary>
        /// Explicit conversion to Vector.
        /// </summary>
        /// <param name="size"> Size - the Size to convert to a Vector </param>
        /// <returns>
        /// Vector - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector4D(Size4D size) => new Vector4D(size.Width, size.Height, size.Depth, size.Breadth);

        /// <summary>
        /// Converts the specified <see cref="Point4D"/> structure to a <see cref="Size4D"/> structure.
        /// </summary>
        /// <param name="point">The <see cref="Point4D"/> to be converted.</param>
        /// <returns>
        /// Size - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size4D(Point4D point) => new Size4D(point.X, point.Y, point.Z, point.W);

        /// <summary>
        /// Converts the specified <see cref="Size4D"/> to a <see cref="Point4D"/>.
        /// </summary>
        /// <param name="size"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Point4D(Size4D size) => new Point4D(size.Width, size.Height, size.Depth, size.Breadth);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"> Size - the Size to convert to a Vector </param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Size4D((double Width, double Height, double Depth, double Breadth) tuple) => new Size4D(tuple);

        /// <summary>
        /// Converts the specified <see cref="Size4D"/> structure to a <see cref="ValueTuple{T1, T2, T3, T4}"/> structure.
        /// </summary>
        /// <param name="size">The <see cref="Size4D"/> to be converted.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double Width, double Height, double Depth, double Breadth) (Size4D size) => (size.Width, size.Height, size.Depth, size.Breadth);
        #endregion Operators

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Size4D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Size4D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Size4D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [ParseMethod]
        public static Size4D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Size4D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Size4D"/> data </param>
        /// <param name="provider"></param>
        /// <returns>
        /// Returns an instance of the <see cref="Size4D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Size4D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            var firstToken = tokenizer.NextTokenRequired();

            // The token will already have had whitespace trimmed so we can do a simple string compare.
            var value = firstToken == nameof(Empty) ? Empty : new Size4D(
                Convert.ToDouble(firstToken, provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
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
        public override int GetHashCode() => HashCode.Combine(Width, Height, Depth, Breadth);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Size4D other) => (Width == other.Width) & (Height == other.Height) & (Depth == other.Depth) & (Breadth == other.Breadth);
        public override bool Equals(object obj) => base.Equals(obj);
        public override string ToString() => base.ToString();

        /// <summary>
        /// Creates a string representation of this <see cref="Size4D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The <see cref="CultureInfo"/> provider.</param>
        /// <returns>A string representation of this <see cref="Size4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Size4D);
            var s = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Size4D)}({nameof(Width)}:{Width.ToString(format, formatProvider)}{s} {nameof(Height)}:{Height.ToString(format, formatProvider)}{s} {nameof(Depth)}:{Depth.ToString(format, formatProvider)}{s} {nameof(Breadth)}:{Breadth.ToString(format, formatProvider)})";
        }
        #endregion Methods
    }
}
