// <copyright file="Size2D.cs" company="Shkyrockett" >
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
using static Engine.Maths;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The size2d struct.
    /// </summary>
    /// <seealso cref="IVector{T}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(Size2DConverter))]
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public struct Size2D
        : IVector<Size2D>
    {
        #region Implementations
        /// <summary>
        /// Represents a <see cref="Size2D" /> that has <see cref="Width" />, and <see cref="Height" /> values set to zero.
        /// </summary>
        public static readonly Size2D Empty = new Size2D(0d, 0d);

        /// <summary>
        /// Represents a <see cref="Size2D" /> that has <see cref="Width" />, and <see cref="Height" /> values set to 1.
        /// </summary>
        public static readonly Size2D Unit = new Size2D(1d, 1d);

        /// <summary>
        /// Represents a <see cref="Size2D" /> that has <see cref="Width" />, and <see cref="Height" /> values set to NaN.
        /// </summary>
        public static readonly Size2D NaN = new Size2D(double.NaN, double.NaN);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D" /> class.
        /// </summary>
        /// <param name="size">The size.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size2D(Size2D size)
            : this(size.Width, size.Height)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D" /> class.
        /// </summary>
        /// <param name="point">The point.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size2D(Point2D point)
            : this(point.X, point.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D" /> class.
        /// </summary>
        /// <param name="width">The Width component of the Size.</param>
        /// <param name="height">The Height component of the Size.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size2D(double width, double height)
            : this()
        {
            (Width, Height) = (width, height);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size2D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
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
        /// Deconstruct this <see cref="Size2D" /> to a <see cref="ValueTuple{T1, T2}" />.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double width, out double height) => (width, height) = (Width, Height);
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the Width component of a <see cref="Size2D" /> coordinate.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        [DataMember(Name = nameof(Width)), XmlAttribute(nameof(Width)), SoapAttribute(nameof(Width))]
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the Height component of a <see cref="Size2D" /> coordinate.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        [DataMember(Name = nameof(Height)), XmlAttribute(nameof(Height)), SoapAttribute(nameof(Height))]
        public double Height { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point2D" /> is empty.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if this instance is empty; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public bool IsEmpty
            => Abs(Width) < double.Epsilon
            && Abs(Height) < double.Epsilon;

        /// <summary>
        /// Gets the number of components in the Vector.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Count => 2;
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Size2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator +(Size2D value) => Plus(value);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D" /> classes.
        /// </summary>
        /// <param name="augend">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator +(Size2D augend, double addend) => Add(augend, addend);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D" /> classes.
        /// </summary>
        /// <param name="augend">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator +(double augend, Size2D addend) => Add(augend, addend);

        /// <summary>
        /// Add two <see cref="Size2D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator +(Size2D augend, Size2D addend) => Add(augend, addend);

        /// <summary>
        /// Add two <see cref="Size2D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator +(Size2D augend, Point2D addend) => Add(augend, addend);

        /// <summary>
        /// Add a <see cref="Point2D" /> and a <see cref="Size2D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator +(Point2D augend, Size2D addend) => Add(augend, addend);

        /// <summary>
        /// Add a <see cref="Size2D" /> to a <see cref="Vector2D" /> class.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(Size2D augend, Vector2D addend) => Add(augend, addend);

        /// <summary>
        /// Add a <see cref="Vector2D" /> and a <see cref="Size2D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(Vector2D augend, Size2D addend) => Add(augend, addend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Size2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator -(Size2D value) => Negate(value);

        /// <summary>
        /// Subtract a <see cref="Size2D" /> from a <see cref="double" /> value.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator -(Size2D minuend, double subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="double" /> value from a <see cref="Size2D" />.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator -(double minuend, Size2D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Size2D" /> from another <see cref="Size2D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator -(Size2D minuend, Size2D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Size2D" /> from a <see cref="Point2D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator -(Size2D minuend, Point2D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Point2D" /> from another <see cref="Size2D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator -(Point2D minuend, Size2D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Size2D" /> from a <see cref="Vector2D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(Size2D minuend, Vector2D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Vector2D" /> from another <see cref="Size2D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(Vector2D minuend, Size2D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Scale a point.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator *(Size2D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

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
        public static Size2D operator *(double multiplicand, Size2D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Scale a Size2D
        /// </summary>
        /// <param name="multiplicand">The Point</param>
        /// <param name="multiplier">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator *(Size2D multiplicand, Size2D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Point2D operator *(Size2D multiplicand, Point2D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Point2D operator *(Point2D multiplicand, Size2D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Vector2D operator *(Size2D multiplicand, Vector2D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Vector2D operator *(Vector2D multiplicand, Size2D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Divide a <see cref="Size2D" /> by a <see cref="double" /> value.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator /(Size2D dividend, double divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a <see cref="double" /> by a <see cref="Size2D" /> value.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator /(double dividend, Size2D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a Size2D
        /// </summary>
        /// <param name="dividend">The Point</param>
        /// <param name="divisor">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D operator /(Size2D dividend, Size2D divisor) => Divide(dividend, divisor);

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
        public static Point2D operator /(Size2D dividend, Point2D divisor) => Divide(dividend, divisor);

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
        public static Point2D operator /(Point2D dividend, Size2D divisor) => Divide(dividend, divisor);

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
        public static Vector2D operator /(Size2D dividend, Vector2D divisor) => Divide(dividend, divisor);

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
        public static Vector2D operator /(Vector2D dividend, Size2D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Compares two <see cref="Size2D" /> objects.
        /// The result specifies whether the values of the <see cref="Width" /> and <see cref="Height" />
        /// values of the two <see cref="Size2D" /> objects are equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Size2D left, Size2D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Size2D" /> objects.
        /// The result specifies whether the values of the <see cref="Width" /> or <see cref="Height" />
        /// values of the two <see cref="Size2D" /> objects are unequal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Size2D left, Size2D right) => !Equals(left, right);

        /// <summary>
        /// Explicit conversion to Size2D.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>
        /// Size - A Size equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size2D(Vector2D size) => new Size2D(size.I, size.J);

        /// <summary>
        /// Explicit conversion to Vector.
        /// </summary>
        /// <param name="size">Size - the Size to convert to a Vector</param>
        /// <returns>
        /// Vector - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector2D(Size2D size) => new Vector2D(size.Width, size.Height);

        /// <summary>
        /// Converts the specified <see cref="Point2D" /> to a <see cref="Size2D" />.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>
        /// Size - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size2D(Point2D size) => new Size2D(size.X, size.Y);

        /// <summary>
        /// Converts the specified <see cref="Size2D" /> to a <see cref="Point2D" />.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Point2D(Size2D size) => new Point2D(size.Width, size.Height);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">Size - the Size to convert to a Vector</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Size2D((double Width, double Height) tuple) => new Size2D(tuple);

        /// <summary>
        /// Converts the specified <see cref="Point2D" /> structure to a <see cref="ValueTuple{T1, T2}" /> structure.
        /// </summary>
        /// <param name="point">The <see cref="Point2D" /> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double Width, double Height)(Size2D point) => (point.Width, point.Height);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Plus(Size2D value) => Operations.Plus(value.Width, value.Height);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Add(Size2D augend, double addend) => AddVectorUniform(augend.Width, augend.Height, addend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Add(double augend, Size2D addend) => AddVectorUniform(addend.Width, addend.Height, augend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Add(Size2D augend, Size2D addend) => AddVectors(augend.Width, augend.Height, addend.Width, addend.Height);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Add(Size2D augend, Point2D addend) => AddVectors(augend.Width, augend.Height, addend.X, addend.Y);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Add(Point2D augend, Size2D addend) => AddVectors(augend.X, augend.Y, addend.Width, addend.Height);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(Size2D augend, Vector2D addend) => AddVectors(augend.Width, augend.Height, addend.I, addend.J);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Add(Vector2D augend, Size2D addend) => AddVectors(augend.I, augend.J, addend.Width, addend.Height);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Negate(Size2D value) => Operations.Negate(value.Width, value.Height);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Subtract(Size2D minuend, double subend) => SubtractVectorUniform(minuend.Width, minuend.Height, subend);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Subtract(double minuend, Size2D subend) => SubtractVectorUniform(minuend, subend.Width, subend.Height);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Subtract(Size2D minuend, Size2D subend) => SubtractVector(minuend.Width, minuend.Height, subend.Width, subend.Height);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Subtract(Size2D minuend, Point2D subend) => SubtractVector(minuend.Width, minuend.Height, subend.X, subend.Y);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Subtract(Point2D minuend, Size2D subend) => SubtractVector(minuend.X, minuend.Y, subend.Width, subend.Height);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Subtract(Size2D minuend, Vector2D subend) => SubtractVector(minuend.Width, minuend.Height, subend.I, subend.J);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Subtract(Vector2D minuend, Size2D subend) => SubtractVector(minuend.I, minuend.J, subend.Width, subend.Height);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Multiply(Size2D multiplicand, double multiplier) => ScaleVector(multiplicand.Width, multiplicand.Height, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Multiply(double multiplicand, Size2D multiplier) => ScaleVector(multiplier.Width, multiplier.Height, multiplicand);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Multiply(Size2D multiplicand, Size2D multiplier) => ScaleVectorParametric(multiplicand.Width, multiplicand.Height, multiplier.Width, multiplier.Height);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Multiply(Size2D multiplicand, Point2D multiplier) => ScaleVectorParametric(multiplicand.Width, multiplicand.Height, multiplier.X, multiplier.Y);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Multiply(Point2D multiplicand, Size2D multiplier) => ScaleVectorParametric(multiplicand.X, multiplicand.Y, multiplier.Width, multiplier.Height);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Multiply(Size2D multiplicand, Vector2D multiplier) => ScaleVectorParametric(multiplicand.Width, multiplicand.Height, multiplier.I, multiplier.J);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Multiply(Vector2D multiplicand, Size2D multiplier) => ScaleVectorParametric(multiplicand.I, multiplicand.J, multiplier.Width, multiplier.Height);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Divide(Size2D dividend, double divisor) => DivideVectorUniform(dividend.Width, dividend.Height, divisor);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Divide(double dividend, Size2D divisor) => DivideByVectorUniform(dividend, divisor.Width, divisor.Height);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Divide(Size2D dividend, Size2D divisor) => DivideVectorParametric(dividend.Width, dividend.Height, divisor.Width, divisor.Height);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Divide(Size2D dividend, Point2D divisor) => DivideVectorParametric(dividend.Width, dividend.Height, divisor.X, divisor.Y);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Divide(Point2D dividend, Size2D divisor) => DivideVectorParametric(dividend.X, dividend.Y, divisor.Width, divisor.Height);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Divide(Size2D dividend, Vector2D divisor) => ScaleVectorParametric(dividend.Width, dividend.Height, divisor.I, divisor.J);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D Divide(Vector2D dividend, Size2D divisor) => DivideVectorParametric(dividend.I, dividend.J, divisor.Width, divisor.Height);

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
        public bool Equals(Size2D other) => (Width == other.Width) && (Height == other.Height);

        /// <summary>
        /// Converts to valuetuple.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double Width, double Height) ToValueTuple() => (Width, Height);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D FromValueTuple((double Width, double Height) tuple) => new Size2D(tuple.Width, tuple.Height);

        /// <summary>
        /// Converts to size2d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size2D ToSize2D() => new Size2D(Width, Height);

        /// <summary>
        /// Converts to size2d.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D FromSize2D(Size2D size) => new Size2D(size.Width, size.Height);

        /// <summary>
        /// Converts to vector2d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D ToVector2D() => new Vector2D(Width, Height);

        /// <summary>
        /// Converts to Vector2D.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D FromVector2D(Vector2D vector) => new Vector2D(vector.I, vector.J);

        /// <summary>
        /// Converts to point2d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2D ToPoint2D() => new Point2D(Width, Height);

        /// <summary>
        /// Converts to Point2D.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D FromPoint2D(Point2D point) => new Point2D(point.X, point.Y);
        #endregion

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Size2D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Size2D" /> data</param>
        /// <returns>
        /// Returns an instance of the <see cref="Size2D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [ParseMethod]
        public static Size2D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Size2D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Size2D" /> data</param>
        /// <param name="formatProvider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Size2D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        public static Size2D Parse(string source, IFormatProvider formatProvider)
        {
            var tokenizer = new Tokenizer(source, formatProvider);
            var firstToken = tokenizer.NextTokenRequired();

            // The token will already have had whitespace trimmed so we can do a simple string compare.
            var value = firstToken == nameof(Empty) ? Empty : new Size2D(
                Convert.ToDouble(firstToken, formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider)
                );

            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }

        /// <summary>
        /// The truncate.
        /// </summary>
        /// <returns>
        /// The <see cref="Size2D" />.
        /// </returns>
        public Size2D Truncate() => new Size2D((int)Width, (int)Height);
        #endregion Factories

        #region Standard Methods
        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(Width, Height);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Size2D" /> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Size2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Size2D" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="formatProvider">The <see cref="CultureInfo" /> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Size2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider formatProvider) => ToString("R" /* format string */, formatProvider);

        /// <summary>
        /// Creates a string representation of this <see cref="Size2D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The <see cref="CultureInfo"/> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Size2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Size2D);
            var s = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Size2D)}({nameof(Width)}: {Width.ToString(format, formatProvider)}{s} {nameof(Height)}: {Height.ToString(format, formatProvider)})";
        }

        /// <summary>
        /// Gets the debugger display.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string GetDebuggerDisplay() => ToString();
        #endregion
    }
}
