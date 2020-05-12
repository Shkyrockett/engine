// <copyright file="Size5D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The size5D struct.
    /// </summary>
    /// <seealso cref="IVector{T}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(Size5DConverter))]
    [DebuggerDisplay("{ToString()}")]
    public struct Size5D
        : IVector<Size5D>
    {
        #region Implementations
        /// <summary>
        /// Represents a <see cref="Size5D" /> that has <see cref="Width" />, <see cref="Height" />, <see cref="Depth" />, <see cref="Breadth" />, and <see cref="Length" /> values set to zero.
        /// </summary>
        public static readonly Size5D Empty = new Size5D(0d, 0d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Size5D" /> that has <see cref="Width" />, <see cref="Height" />, <see cref="Depth" />, <see cref="Breadth" />, and <see cref="Length" /> values set to 1.
        /// </summary>
        public static readonly Size5D Unit = new Size5D(1d, 1d, 1d, 1d, 1d);

        /// <summary>
        /// Represents a <see cref="Size5D" /> that has <see cref="Width" />, <see cref="Height" />, <see cref="Depth" />, <see cref="Breadth" />, and <see cref="Length" /> values set to NaN.
        /// </summary>
        public static readonly Size5D NaN = new Size5D(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Size5D" /> class.
        /// </summary>
        /// <param name="size">The size.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size5D(Size5D size)
            : this(size.Width, size.Height, size.Depth, size.Breadth, size.Length)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size5D" /> class.
        /// </summary>
        /// <param name="point">The point.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size5D(Point5D point)
            : this(point.X, point.Y, point.Z, point.W, point.V)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size5D" /> class.
        /// </summary>
        /// <param name="width">The Width component of the Size.</param>
        /// <param name="height">The Height component of the Size.</param>
        /// <param name="depth">The Depth component of the Size.</param>
        /// <param name="breadth">The Breadth component of the Size.</param>
        /// <param name="length">The length.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size5D(double width, double height, double depth, double breadth, double length)
            : this()
        {
            (Width, Height, Depth, Breadth, Length) = (width, height, depth, breadth, length);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size5D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size5D((double Width, double Height, double Depth, double Breadth, double Length) tuple)
            : this()
        {
            (Width, Height, Depth, Breadth, Length) = tuple;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Size5D" /> to a <see cref="ValueTuple{T1, T2, T3, T4, T5}" />.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="breadth">The breadth.</param>
        /// <param name="length">The length.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double width, out double height, out double depth, out double breadth, out double length) => (width, height, depth, breadth, length) = (Width, Height, Depth, Breadth, Length);
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the Width component of a <see cref="Size5D" /> coordinate.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        [DataMember(Name = nameof(Width)), XmlAttribute(nameof(Width)), SoapAttribute(nameof(Width))]
        public double Width { get; internal set; }

        /// <summary>
        /// Gets or sets the Height component of a <see cref="Size5D" /> coordinate.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        [DataMember(Name = nameof(Height)), XmlAttribute(nameof(Height)), SoapAttribute(nameof(Height))]
        public double Height { get; internal set; }

        /// <summary>
        /// Gets or sets the Depth component of a <see cref="Size5D" /> coordinate.
        /// </summary>
        /// <value>
        /// The depth.
        /// </value>
        [DataMember(Name = nameof(Depth)), XmlAttribute(nameof(Depth)), SoapAttribute(nameof(Depth))]
        public double Depth { get; internal set; }

        /// <summary>
        /// Gets or sets the Breadth component of a <see cref="Size5D" /> coordinate.
        /// </summary>
        /// <value>
        /// The breadth.
        /// </value>
        [DataMember(Name = nameof(Breadth)), XmlAttribute(nameof(Breadth)), SoapAttribute(nameof(Breadth))]
        public double Breadth { get; internal set; }

        /// <summary>
        /// Gets or sets the Length component of a <see cref="Size5D" /> coordinate.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        [DataMember(Name = nameof(Length)), XmlAttribute(nameof(Length)), SoapAttribute(nameof(Length))]
        public double Length { get; internal set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Size5D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator +(Size5D value) => Plus(value);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point5D" /> classes.
        /// </summary>
        /// <param name="augend">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator +(Size5D augend, double addend) => Add(augend, addend);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point5D" /> classes.
        /// </summary>
        /// <param name="augend">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator +(double augend, Size5D addend) => Add(augend, addend);

        /// <summary>
        /// Add two <see cref="Size5D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator +(Size5D augend, Size5D addend) => Add(augend, addend);

        /// <summary>
        /// Add two <see cref="Size5D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator +(Size5D augend, Point5D addend) => Add(augend, addend);

        /// <summary>
        /// Add a <see cref="Point5D" /> and a <see cref="Size5D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator +(Point5D augend, Size5D addend) => Add(augend, addend);

        /// <summary>
        /// Add a <see cref="Size5D" /> to a <see cref="Vector5D" /> class.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator +(Size5D augend, Vector5D addend) => Add(augend, addend);

        /// <summary>
        /// Add a <see cref="Vector5D" /> and a <see cref="Size5D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator +(Vector5D augend, Size5D addend) => Add(augend, addend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Size5D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator -(Size5D value) => Negate(value);

        /// <summary>
        /// Subtract a <see cref="Size5D" /> from a <see cref="double" /> value.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator -(Size5D minuend, double subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="double" /> value from a <see cref="Size5D" />.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator -(double minuend, Size5D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Size5D" /> from another <see cref="Size5D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator -(Size5D minuend, Size5D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Size5D" /> from a <see cref="Point5D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator -(Size5D minuend, Point5D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Point5D" /> from another <see cref="Size5D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator -(Point5D minuend, Size5D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Size5D" /> from a <see cref="Vector5D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator -(Size5D minuend, Vector5D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Vector5D" /> from another <see cref="Size5D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator -(Vector5D minuend, Size5D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Scale a Size.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator *(Size5D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Scale a point
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator *(double multiplicand, Size5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Scale a Size5D
        /// </summary>
        /// <param name="multiplicand">The Point</param>
        /// <param name="multiplier">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator *(Size5D multiplicand, Size5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Scale a Point
        /// </summary>
        /// <param name="multiplicand">The Point</param>
        /// <param name="multiplier">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator *(Size5D multiplicand, Point5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Scale a Point
        /// </summary>
        /// <param name="multiplicand">The Point</param>
        /// <param name="multiplier">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator *(Point5D multiplicand, Size5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplicand">The Point</param>
        /// <param name="multiplier">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator *(Size5D multiplicand, Vector5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplicand">The Point</param>
        /// <param name="multiplier">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator *(Vector5D multiplicand, Size5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Divide a <see cref="Size5D" /> by a <see cref="double" /> value.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator /(Size5D dividend, double divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a <see cref="double" /> by a <see cref="Size5D" /> value.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator /(double dividend, Size5D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a Size5D
        /// </summary>
        /// <param name="dividend">The Point</param>
        /// <param name="divisor">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D operator /(Size5D dividend, Size5D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a Point
        /// </summary>
        /// <param name="dividend">The Point</param>
        /// <param name="divisor">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator /(Size5D dividend, Point5D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a Point
        /// </summary>
        /// <param name="dividend">The Point</param>
        /// <param name="divisor">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator /(Point5D dividend, Size5D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a Vector
        /// </summary>
        /// <param name="dividend">The Point</param>
        /// <param name="divisor">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator /(Size5D dividend, Vector5D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a Vector
        /// </summary>
        /// <param name="dividend">The Point</param>
        /// <param name="divisor">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator /(Vector5D dividend, Size5D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Compares two <see cref="Size5D" /> objects.
        /// The result specifies whether the values of the <see cref="Width" />, <see cref="Height" />, <see cref="Depth" /> and <see cref="Breadth" />
        /// values of the two <see cref="Size5D" /> objects are equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Size5D left, Size5D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Size5D" /> objects.
        /// The result specifies whether the values of the <see cref="Width" />, <see cref="Height" />, <see cref="Depth" /> or <see cref="Breadth" />
        /// values of the two <see cref="Size5D" /> objects are unequal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Size5D left, Size5D right) => !Equals(left, right);

        /// <summary>
        /// Converts the specified <see cref="Vector5D" /> structure to a <see cref="Size5D" /> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector5D" /> to be converted.</param>
        /// <returns>
        /// Size - A Size equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size5D(Vector5D vector) => new Size5D(vector.I, vector.J, vector.K, vector.L, vector.M);

        /// <summary>
        /// Explicit conversion to Vector.
        /// </summary>
        /// <param name="size">Size - the Size to convert to a Vector</param>
        /// <returns>
        /// Vector - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector5D(Size5D size) => new Vector5D(size.Width, size.Height, size.Depth, size.Breadth, size.Length);

        /// <summary>
        /// Converts the specified <see cref="Point5D" /> structure to a <see cref="Size5D" /> structure.
        /// </summary>
        /// <param name="point">The <see cref="Point5D" /> to be converted.</param>
        /// <returns>
        /// Size - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size5D(Point5D point) => new Size5D(point.X, point.Y, point.Z, point.W, point.V);

        /// <summary>
        /// Converts the specified <see cref="Size5D" /> to a <see cref="Point5D" />.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Point5D(Size5D size) => new Point5D(size.Width, size.Height, size.Depth, size.Breadth, size.Length);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">Size - the Size to convert to a Vector</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Size5D((double Width, double Height, double Depth, double Breadth, double Length) tuple) => new Size5D(tuple);

        /// <summary>
        /// Converts the specified <see cref="Size5D" /> structure to a <see cref="ValueTuple{T1, T2, T3, T4, T5}" /> structure.
        /// </summary>
        /// <param name="size">The <see cref="Size5D" /> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double Width, double Height, double Depth, double Breadth, double Length)(Size5D size) => (size.Width, size.Height, size.Depth, size.Breadth, size.Length);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Plus(Size5D value) => Operations.Plus(value.Width, value.Height, value.Depth, value.Breadth, value.Length);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Add(Size5D augend, double addend) => Operations.AddVectorUniform(augend.Width, augend.Height, augend.Depth, augend.Breadth, augend.Length, addend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Add(double augend, Size5D addend) => Operations.AddVectorUniform(addend.Width, addend.Height, addend.Depth, addend.Breadth, addend.Length, augend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Add(Size5D augend, Size5D addend) => Operations.AddVectors(augend.Width, augend.Height, augend.Depth, augend.Breadth, augend.Length, addend.Width, addend.Height, addend.Depth, addend.Breadth, addend.Length);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Add(Size5D augend, Point5D addend) => Operations.AddVectors(augend.Width, augend.Height, augend.Depth, augend.Breadth, augend.Length, addend.X, addend.Y, addend.Z, addend.W, addend.V);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Add(Point5D augend, Size5D addend) => Operations.AddVectors(augend.X, augend.Y, augend.Z, augend.W, augend.V, addend.Width, addend.Height, addend.Depth, addend.Breadth, addend.Length);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Add(Size5D augend, Vector5D addend) => Operations.AddVectors(augend.Width, augend.Height, augend.Depth, augend.Breadth, augend.Length, addend.I, addend.J, addend.K, addend.L, addend.M);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Add(Vector5D augend, Size5D addend) => Operations.AddVectors(augend.I, augend.J, augend.K, augend.L, augend.M, addend.Width, addend.Height, addend.Depth, addend.Breadth, addend.Length);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Negate(Size5D value) => Operations.Negate(value.Width, value.Height, value.Depth, value.Breadth, value.Length);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Subtract(Size5D minuend, double subend) => Operations.SubtractVectorUniform(minuend.Width, minuend.Height, minuend.Depth, minuend.Breadth, minuend.Length, subend);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Subtract(double minuend, Size5D subend) => Operations.SubtractVectorUniform(minuend, subend.Width, subend.Height, subend.Depth, subend.Breadth, subend.Length);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Subtract(Size5D minuend, Size5D subend) => Operations.SubtractVector(minuend.Width, minuend.Height, minuend.Depth, minuend.Breadth, minuend.Length, subend.Width, subend.Height, subend.Depth, subend.Breadth, subend.Length);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Subtract(Size5D minuend, Point5D subend) => Operations.SubtractVector(minuend.Width, minuend.Height, minuend.Depth, minuend.Breadth, minuend.Length, subend.X, subend.Y, subend.Z, subend.W, subend.V);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Subtract(Point5D minuend, Size5D subend) => Operations.SubtractVector(minuend.X, minuend.Y, minuend.Z, minuend.W, minuend.V, subend.Width, subend.Height, subend.Depth, subend.Breadth, subend.Length);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Subtract(Size5D minuend, Vector5D subend) => Operations.SubtractVector(minuend.Width, minuend.Height, minuend.Depth, minuend.Breadth, minuend.Length, subend.I, subend.J, subend.K, subend.L, subend.M);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Subtract(Vector5D minuend, Size5D subend) => Operations.SubtractVector(minuend.I, minuend.J, minuend.K, minuend.L, minuend.M, subend.Width, subend.Height, subend.Depth, subend.Breadth, subend.Length);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Multiply(Size5D multiplicand, double multiplier) => Operations.ScaleVector(multiplicand.Width, multiplicand.Height, multiplicand.Depth, multiplicand.Breadth, multiplicand.Length, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Multiply(double multiplicand, Size5D multiplier) => Operations.ScaleVector(multiplier.Width, multiplier.Height, multiplier.Depth, multiplier.Breadth, multiplier.Length, multiplicand);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Multiply(Size5D multiplicand, Size5D multiplier) => Operations.ScaleVectorParametric(multiplicand.Width, multiplicand.Height, multiplicand.Depth, multiplicand.Breadth, multiplicand.Length, multiplier.Width, multiplier.Height, multiplier.Depth, multiplier.Breadth, multiplier.Length);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Multiply(Size5D multiplicand, Point5D multiplier) => Operations.ScaleVectorParametric(multiplicand.Width, multiplicand.Height, multiplicand.Depth, multiplicand.Breadth, multiplicand.Length, multiplier.X, multiplier.Y, multiplier.Z, multiplier.W, multiplier.V);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Multiply(Point5D multiplicand, Size5D multiplier) => Operations.ScaleVectorParametric(multiplicand.X, multiplicand.Y, multiplicand.Z, multiplicand.W, multiplicand.V, multiplier.Width, multiplier.Height, multiplier.Depth, multiplier.Breadth, multiplier.Length);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Multiply(Size5D multiplicand, Vector5D multiplier) => Operations.ScaleVectorParametric(multiplicand.Width, multiplicand.Height, multiplicand.Depth, multiplicand.Breadth, multiplicand.Length, multiplier.I, multiplier.J, multiplier.K, multiplier.L, multiplier.M);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Multiply(Vector5D multiplicand, Size5D multiplier) => Operations.ScaleVectorParametric(multiplicand.I, multiplicand.J, multiplicand.K, multiplicand.L, multiplicand.M, multiplier.Width, multiplier.Height, multiplier.Depth, multiplier.Breadth, multiplier.Length);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Divide(Size5D dividend, double divisor) => Operations.DivideVectorUniform(dividend.Width, dividend.Height, dividend.Depth, dividend.Breadth, dividend.Length, divisor);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Divide(double dividend, Size5D divisor) => Operations.DivideByVectorUniform(dividend, divisor.Width, divisor.Height, divisor.Depth, divisor.Breadth, divisor.Length);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D Divide(Size5D dividend, Size5D divisor) => Operations.DivideVectorParametric(dividend.Width, dividend.Height, dividend.Depth, dividend.Breadth, dividend.Length, divisor.Width, divisor.Height, divisor.Depth, divisor.Breadth, divisor.Length);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Divide(Size5D dividend, Point5D divisor) => Operations.DivideVectorParametric(dividend.Width, dividend.Height, dividend.Depth, dividend.Breadth, dividend.Length, divisor.X, divisor.Y, divisor.Z, divisor.W, divisor.V);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Divide(Point5D dividend, Size5D divisor) => Operations.DivideVectorParametric(dividend.X, dividend.Y, dividend.Z, dividend.W, dividend.V, divisor.Width, divisor.Height, divisor.Depth, divisor.Breadth, divisor.Length);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Divide(Size5D dividend, Vector5D divisor) => Operations.ScaleVectorParametric(dividend.Width, dividend.Height, dividend.Depth, dividend.Breadth, dividend.Length, divisor.I, divisor.J, divisor.K, divisor.L, divisor.M);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Divide(Vector5D dividend, Size5D divisor) => Operations.DivideVectorParametric(dividend.I, dividend.J, dividend.K, dividend.L, dividend.M, divisor.Width, divisor.Height, divisor.Depth, divisor.Breadth, divisor.Length);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Point2D d && Equals(d);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Size5D other) => (Width == other.Width) && (Height == other.Height) && (Depth == other.Depth) && (Breadth == other.Breadth) && (Length == other.Length);

        /// <summary>
        /// Converts to valuetuple.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double Width, double Height, double Depth, double Breadth, double Length) ToValueTuple() => (Width, Height, Depth, Breadth, Length);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D FromValueTuple((double Width, double Height, double Depth, double Breadth, double Length) tuple) => new Size5D(tuple.Width, tuple.Height, tuple.Depth, tuple.Breadth, tuple.Length);

        /// <summary>
        /// Converts to size5D.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size5D ToSize5D() => new Size5D(Width, Height, Depth, Breadth, Length);

        /// <summary>
        /// Converts to size5D.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size5D FromSize5D(Size5D size) => new Size5D(size.Width, size.Height, size.Depth, size.Breadth, size.Length);

        /// <summary>
        /// Converts to vector5D.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D ToVector5D() => new Vector5D(Width, Height, Depth, Breadth, Length);

        /// <summary>
        /// Converts to Vector5D.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D FromVector5D(Vector5D vector) => new Vector5D(vector.I, vector.J, vector.K, vector.L, vector.M);

        /// <summary>
        /// Converts to point5D.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point5D ToPoint5D() => new Point5D(Width, Height, Depth, Breadth, Length);

        /// <summary>
        /// Converts to Point5D.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D FromPoint5D(Point5D point) => new Point5D(point.X, point.Y, point.Z, point.W, point.V);
        #endregion

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Size5D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Size5D" /> data</param>
        /// <returns>
        /// Returns an instance of the <see cref="Size5D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [ParseMethod]
        public static Size5D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Size5D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Size5D" /> data</param>
        /// <param name="formatProvider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Size5D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        public static Size5D Parse(string source, IFormatProvider formatProvider)
        {
            var tokenizer = new Tokenizer(source, formatProvider);
            var firstToken = tokenizer.NextTokenRequired();

            // The token will already have had whitespace trimmed so we can do a simple string compare.
            var value = firstToken == nameof(Empty) ? Empty : new Size5D(
                Convert.ToDouble(firstToken, formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider)
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
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(Width, Height, Depth, Breadth, Length);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Size5D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Size5D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Size5D);
            var s = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Size5D)}({nameof(Width)}:{Width.ToString(format, formatProvider)}{s} {nameof(Height)}:{Height.ToString(format, formatProvider)}{s} {nameof(Depth)}:{Depth.ToString(format, formatProvider)}{s} {nameof(Breadth)}:{Breadth.ToString(format, formatProvider)}{s} {nameof(Length)}:{Length.ToString(format, formatProvider)})";
        }
        #endregion Methods
    }
}
