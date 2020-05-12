// <copyright file="Size4D.cs" company="Shkyrockett" >
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
    /// The size4D struct.
    /// </summary>
    /// <seealso cref="IVector{T}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(Size4DConverter))]
    [DebuggerDisplay("{ToString()}")]
    public struct Size4D
        : IVector<Size4D>
    {
        #region Implementations
        /// <summary>
        /// Represents a <see cref="Size4D" /> that has <see cref="Width" />, <see cref="Height" />, <see cref="Depth" />, and <see cref="Breadth" /> values set to zero.
        /// </summary>
        public static readonly Size4D Empty = new Size4D(0d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Size4D" /> that has <see cref="Width" />, <see cref="Height" />, <see cref="Depth" />, and <see cref="Breadth" /> values set to 1.
        /// </summary>
        public static readonly Size4D Unit = new Size4D(1d, 1d, 1d, 1d);

        /// <summary>
        /// Represents a <see cref="Size4D" /> that has <see cref="Width" />, <see cref="Height" />, <see cref="Depth" />, and <see cref="Breadth" /> values set to NaN.
        /// </summary>
        public static readonly Size4D NaN = new Size4D(double.NaN, double.NaN, double.NaN, double.NaN);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Size4D" /> class.
        /// </summary>
        /// <param name="size">The size.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size4D(Size4D size)
            : this(size.Width, size.Height, size.Depth, size.Breadth)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size4D" /> class.
        /// </summary>
        /// <param name="point">The point.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size4D(Point4D point)
            : this(point.X, point.Y, point.Z, point.W)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size4D" /> class.
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
            (Width, Height, Depth, Breadth) = (width, height, depth, breadth);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size4D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
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
        /// Deconstruct this <see cref="Size4D" /> to a <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="breadth">The breadth.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double width, out double height, out double depth, out double breadth) => (width, height, depth, breadth) = (Width, Height, Depth, Breadth);
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the Width component of a <see cref="Size4D" /> coordinate.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        [DataMember(Name = nameof(Width)), XmlAttribute(nameof(Width)), SoapAttribute(nameof(Width))]
        public double Width { get; internal set; }

        /// <summary>
        /// Gets or sets the Height component of a <see cref="Size4D" /> coordinate.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        [DataMember(Name = nameof(Height)), XmlAttribute(nameof(Height)), SoapAttribute(nameof(Height))]
        public double Height { get; internal set; }

        /// <summary>
        /// Gets or sets the Depth component of a <see cref="Size4D" /> coordinate.
        /// </summary>
        /// <value>
        /// The depth.
        /// </value>
        [DataMember(Name = nameof(Depth)), XmlAttribute(nameof(Depth)), SoapAttribute(nameof(Depth))]
        public double Depth { get; internal set; }

        /// <summary>
        /// Gets or sets the Breadth component of a <see cref="Size4D" /> coordinate.
        /// </summary>
        /// <value>
        /// The breadth.
        /// </value>
        [DataMember(Name = nameof(Breadth)), XmlAttribute(nameof(Breadth)), SoapAttribute(nameof(Breadth))]
        public double Breadth { get; internal set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Size4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator +(Size4D value) => Plus(value);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point4D" /> classes.
        /// </summary>
        /// <param name="augend">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator +(Size4D augend, double addend) => Add(augend, addend);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point4D" /> classes.
        /// </summary>
        /// <param name="augend">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator +(double augend, Size4D addend) => Add(augend, addend);

        /// <summary>
        /// Add two <see cref="Size4D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator +(Size4D augend, Size4D addend) => Add(augend, addend);

        /// <summary>
        /// Add two <see cref="Size4D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator +(Size4D augend, Point4D addend) => Add(augend, addend);

        /// <summary>
        /// Add a <see cref="Point4D" /> and a <see cref="Size4D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator +(Point4D augend, Size4D addend) => Add(augend, addend);

        /// <summary>
        /// Add a <see cref="Size4D" /> to a <see cref="Vector4D" /> class.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(Size4D augend, Vector4D addend) => Add(augend, addend);

        /// <summary>
        /// Add a <see cref="Vector4D" /> and a <see cref="Size4D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(Vector4D augend, Size4D addend) => Add(augend, addend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Size4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator -(Size4D value) => Negate(value);

        /// <summary>
        /// Subtract a <see cref="Size4D" /> from a <see cref="double" /> value.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator -(Size4D minuend, double subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="double" /> value from a <see cref="Size4D" />.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator -(double minuend, Size4D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Size4D" /> from another <see cref="Size4D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator -(Size4D minuend, Size4D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Size4D" /> from a <see cref="Point4D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator -(Size4D minuend, Point4D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Point4D" /> from another <see cref="Size4D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator -(Point4D minuend, Size4D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Size4D" /> from a <see cref="Vector4D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Size4D minuend, Vector4D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Vector4D" /> from another <see cref="Size4D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Vector4D minuend, Size4D subend) => Subtract(minuend, subend);

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
        public static Size4D operator *(Size4D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

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
        public static Size4D operator *(double multiplicand, Size4D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Scale a Size4D
        /// </summary>
        /// <param name="multiplicand">The Point</param>
        /// <param name="multiplier">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator *(Size4D multiplicand, Size4D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Point4D operator *(Size4D multiplicand, Point4D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Point4D operator *(Point4D multiplicand, Size4D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Vector4D operator *(Size4D multiplicand, Vector4D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Vector4D operator *(Vector4D multiplicand, Size4D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Divide a <see cref="Size4D" /> by a <see cref="double" /> value.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator /(Size4D dividend, double divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a <see cref="double" /> by a <see cref="Size4D" /> value.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator /(double dividend, Size4D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a Size4D
        /// </summary>
        /// <param name="dividend">The Point</param>
        /// <param name="divisor">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D operator /(Size4D dividend, Size4D divisor) => Divide(dividend, divisor);

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
        public static Point4D operator /(Size4D dividend, Point4D divisor) => Divide(dividend, divisor);

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
        public static Point4D operator /(Point4D dividend, Size4D divisor) => Divide(dividend, divisor);

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
        public static Vector4D operator /(Size4D dividend, Vector4D divisor) => Divide(dividend, divisor);

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
        public static Vector4D operator /(Vector4D dividend, Size4D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Compares two <see cref="Size4D" /> objects.
        /// The result specifies whether the values of the <see cref="Width" />, <see cref="Height" />, <see cref="Depth" /> and <see cref="Breadth" />
        /// values of the two <see cref="Size4D" /> objects are equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Size4D left, Size4D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Size4D" /> objects.
        /// The result specifies whether the values of the <see cref="Width" />, <see cref="Height" />, <see cref="Depth" /> or <see cref="Breadth" />
        /// values of the two <see cref="Size4D" /> objects are unequal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Size4D left, Size4D right) => !Equals(left, right);

        /// <summary>
        /// Converts the specified <see cref="Vector4D" /> structure to a <see cref="Size4D" /> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector4D" /> to be converted.</param>
        /// <returns>
        /// Size - A Size equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size4D(Vector4D vector) => new Size4D(vector.I, vector.J, vector.K, vector.L);

        /// <summary>
        /// Explicit conversion to Vector.
        /// </summary>
        /// <param name="size">Size - the Size to convert to a Vector</param>
        /// <returns>
        /// Vector - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector4D(Size4D size) => new Vector4D(size.Width, size.Height, size.Depth, size.Breadth);

        /// <summary>
        /// Converts the specified <see cref="Point4D" /> structure to a <see cref="Size4D" /> structure.
        /// </summary>
        /// <param name="point">The <see cref="Point4D" /> to be converted.</param>
        /// <returns>
        /// Size - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size4D(Point4D point) => new Size4D(point.X, point.Y, point.Z, point.W);

        /// <summary>
        /// Converts the specified <see cref="Size4D" /> to a <see cref="Point4D" />.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Point4D(Size4D size) => new Point4D(size.Width, size.Height, size.Depth, size.Breadth);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">Size - the Size to convert to a Vector</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Size4D((double Width, double Height, double Depth, double Breadth) tuple) => new Size4D(tuple);

        /// <summary>
        /// Converts the specified <see cref="Size4D" /> structure to a <see cref="ValueTuple{T1, T2, T3, T4}" /> structure.
        /// </summary>
        /// <param name="size">The <see cref="Size4D" /> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double Width, double Height, double Depth, double Breadth)(Size4D size) => (size.Width, size.Height, size.Depth, size.Breadth);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Plus(Size4D value) => Operations.Plus(value.Width, value.Height, value.Depth, value.Breadth);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Add(Size4D augend, double addend) => Operations.AddVectorUniform(augend.Width, augend.Height, augend.Depth, augend.Breadth, addend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Add(double augend, Size4D addend) => Operations.AddVectorUniform(addend.Width, addend.Height, addend.Depth, addend.Breadth, augend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Add(Size4D augend, Size4D addend) => Operations.AddVectors(augend.Width, augend.Height, augend.Depth, augend.Breadth, addend.Width, addend.Height, addend.Depth, addend.Breadth);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D Add(Size4D augend, Point4D addend) => Operations.AddVectors(augend.Width, augend.Height, augend.Depth, augend.Breadth, addend.X, addend.Y, addend.Z, addend.W);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D Add(Point4D augend, Size4D addend) => Operations.AddVectors(augend.X, augend.Y, augend.Z, augend.W, addend.Width, addend.Height, addend.Depth, addend.Breadth);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Add(Size4D augend, Vector4D addend) => Operations.AddVectors(augend.Width, augend.Height, augend.Depth, augend.Breadth, addend.I, addend.J, addend.K, addend.L);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Add(Vector4D augend, Size4D addend) => Operations.AddVectors(augend.I, augend.J, augend.K, augend.L, addend.Width, addend.Height, addend.Depth, addend.Breadth);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Negate(Size4D value) => Operations.Negate(value.Width, value.Height, value.Depth, value.Breadth);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Subtract(Size4D minuend, double subend) => Operations.SubtractVectorUniform(minuend.Width, minuend.Height, minuend.Depth, minuend.Breadth, subend);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Subtract(double minuend, Size4D subend) => Operations.SubtractVectorUniform(minuend, subend.Width, subend.Height, subend.Depth, subend.Breadth);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Subtract(Size4D minuend, Size4D subend) => Operations.SubtractVector(minuend.Width, minuend.Height, minuend.Depth, minuend.Breadth, subend.Width, subend.Height, subend.Depth, subend.Breadth);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D Subtract(Size4D minuend, Point4D subend) => Operations.SubtractVector(minuend.Width, minuend.Height, minuend.Depth, minuend.Breadth, subend.X, subend.Y, subend.Z, subend.W);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D Subtract(Point4D minuend, Size4D subend) => Operations.SubtractVector(minuend.X, minuend.Y, minuend.Z, minuend.W, subend.Width, subend.Height, subend.Depth, subend.Breadth);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Subtract(Size4D minuend, Vector4D subend) => Operations.SubtractVector(minuend.Width, minuend.Height, minuend.Depth, minuend.Breadth, subend.I, subend.J, subend.K, subend.L);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Subtract(Vector4D minuend, Size4D subend) => Operations.SubtractVector(minuend.I, minuend.J, minuend.K, minuend.L, subend.Width, subend.Height, subend.Depth, subend.Breadth);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Multiply(Size4D multiplicand, double multiplier) => Operations.ScaleVector(multiplicand.Width, multiplicand.Height, multiplicand.Depth, multiplicand.Breadth, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Multiply(double multiplicand, Size4D multiplier) => Operations.ScaleVector(multiplier.Width, multiplier.Height, multiplier.Depth, multiplier.Breadth, multiplicand);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Multiply(Size4D multiplicand, Size4D multiplier) => Operations.ScaleVectorParametric(multiplicand.Width, multiplicand.Height, multiplicand.Depth, multiplicand.Breadth, multiplier.Width, multiplier.Height, multiplier.Depth, multiplier.Breadth);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D Multiply(Size4D multiplicand, Point4D multiplier) => Operations.ScaleVectorParametric(multiplicand.Width, multiplicand.Height, multiplicand.Depth, multiplicand.Breadth, multiplier.X, multiplier.Y, multiplier.Z, multiplier.W);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D Multiply(Point4D multiplicand, Size4D multiplier) => Operations.ScaleVectorParametric(multiplicand.X, multiplicand.Y, multiplicand.Z, multiplicand.W, multiplier.Width, multiplier.Height, multiplier.Depth, multiplier.Breadth);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Multiply(Size4D multiplicand, Vector4D multiplier) => Operations.ScaleVectorParametric(multiplicand.Width, multiplicand.Height, multiplicand.Depth, multiplicand.Breadth, multiplier.I, multiplier.J, multiplier.K, multiplier.L);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Multiply(Vector4D multiplicand, Size4D multiplier) => Operations.ScaleVectorParametric(multiplicand.I, multiplicand.J, multiplicand.K, multiplicand.L, multiplier.Width, multiplier.Height, multiplier.Depth, multiplier.Breadth);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Divide(Size4D dividend, double divisor) => Operations.DivideVectorUniform(dividend.Width, dividend.Height, dividend.Depth, dividend.Breadth, divisor);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Divide(double dividend, Size4D divisor) => Operations.DivideByVectorUniform(dividend, divisor.Width, divisor.Height, divisor.Depth, divisor.Breadth);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D Divide(Size4D dividend, Size4D divisor) => Operations.DivideVectorParametric(dividend.Width, dividend.Height, dividend.Depth, dividend.Breadth, divisor.Width, divisor.Height, divisor.Depth, divisor.Breadth);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D Divide(Size4D dividend, Point4D divisor) => Operations.DivideVectorParametric(dividend.Width, dividend.Height, dividend.Depth, dividend.Breadth, divisor.X, divisor.Y, divisor.Z, divisor.W);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D Divide(Point4D dividend, Size4D divisor) => Operations.DivideVectorParametric(dividend.X, dividend.Y, dividend.Z, dividend.W, divisor.Width, divisor.Height, divisor.Depth, divisor.Breadth);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Divide(Size4D dividend, Vector4D divisor) => Operations.ScaleVectorParametric(dividend.Width, dividend.Height, dividend.Depth, dividend.Breadth, divisor.I, divisor.J, divisor.K, divisor.L);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Divide(Vector4D dividend, Size4D divisor) => Operations.DivideVectorParametric(dividend.I, dividend.J, dividend.K, dividend.L, divisor.Width, divisor.Height, divisor.Depth, divisor.Breadth);

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
        public bool Equals(Size4D other) => (Width == other.Width) & (Height == other.Height) & (Depth == other.Depth) & (Breadth == other.Breadth);

        /// <summary>
        /// Converts to valuetuple.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double Width, double Height, double Depth, double Breadth) ToValueTuple() => (Width, Height, Depth, Breadth);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D FromValueTuple((double Width, double Height, double Depth, double Breadth) tuple) => new Size4D(tuple.Width, tuple.Height, tuple.Depth, tuple.Breadth);

        /// <summary>
        /// Converts to size4d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size4D ToSize4D() => new Size4D(Width, Height, Depth, Breadth);

        /// <summary>
        /// Converts to size4d.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size4D FromSize4D(Size4D size) => new Size4D(size.Width, size.Height, size.Depth, size.Breadth);

        /// <summary>
        /// Converts to vector4d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D ToVector4D() => new Vector4D(Width, Height, Depth, Breadth);

        /// <summary>
        /// Converts to Vector4D.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D FromVector4D(Vector4D vector) => new Vector4D(vector.I, vector.J, vector.K, vector.L);

        /// <summary>
        /// Converts to point4d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point4D ToPoint4D() => new Point4D(Width, Height, Depth, Breadth);

        /// <summary>
        /// Converts to Point4D.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D FromPoint4D(Point4D point) => new Point4D(point.X, point.Y, point.Z, point.W);
        #endregion

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Size4D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Size4D" /> data</param>
        /// <returns>
        /// Returns an instance of the <see cref="Size4D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [ParseMethod]
        public static Size4D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Size4D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Size4D" /> data</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Size4D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
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
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(Width, Height, Depth, Breadth);

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
        /// Creates a string representation of this <see cref="Size4D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Size4D" />.
        /// </returns>
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
