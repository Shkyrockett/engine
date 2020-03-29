// <copyright file="Size3D.cs" company="Shkyrockett" >
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
    /// The size3D struct.
    /// </summary>
    /// <seealso cref="IVector{T}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(Size3DConverter))]
    [DebuggerDisplay("{ToString()}")]
    public struct Size3D
        : IVector<Size3D>
    {
        #region Implementations
        /// <summary>
        /// Represents a <see cref="Size3D" /> that has <see cref="Width" />, <see cref="Height" />, and <see cref="Depth" /> values set to zero.
        /// </summary>
        public static readonly Size3D Empty = new Size3D(0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Size3D" /> that has <see cref="Width" />, <see cref="Height" />, and <see cref="Depth" /> values set to 1.
        /// </summary>
        public static readonly Size3D Unit = new Size3D(1d, 1d, 1d);

        /// <summary>
        /// Represents a <see cref="Size3D" /> that has <see cref="Width" />, <see cref="Height" />, and <see cref="Depth" /> values set to NaN.
        /// </summary>
        public static readonly Size3D NaN = new Size3D(double.NaN, double.NaN, double.NaN);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Size3D" /> class.
        /// </summary>
        /// <param name="size">The size.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size3D(Size3D size)
            : this(size.Width, size.Height, size.Depth)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size3D" /> class.
        /// </summary>
        /// <param name="point">The point.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size3D(Point3D point)
            : this(point.X, point.Y, point.Z)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size3D" /> class.
        /// </summary>
        /// <param name="width">The Width component of the Size.</param>
        /// <param name="height">The Height component of the Size.</param>
        /// <param name="depth">The Depth component of the Size.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size3D(double width, double height, double depth)
            : this()
        {
            (Width, Height, Depth) = (width, height, depth);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size3D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
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
        /// Deconstruct this <see cref="Size3D" /> to a <see cref="ValueTuple{T1, T2, T3}" />.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="depth">The depth.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double width, out double height, out double depth) => (width, height, depth) = (Width, Height, Depth);
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the Width component of a <see cref="Size3D" /> coordinate.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the Height component of a <see cref="Size3D" /> coordinate.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the Depth component of a <see cref="Size3D" /> coordinate.
        /// </summary>
        /// <value>
        /// The depth.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Depth { get; set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Size3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator +(Size3D value) => Plus(value);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point3D" /> classes.
        /// </summary>
        /// <param name="augend">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator +(Size3D augend, double addend) => Add(augend, addend);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point3D" /> classes.
        /// </summary>
        /// <param name="augend">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator +(double augend, Size3D addend) => Add(augend, addend);

        /// <summary>
        /// Add two <see cref="Size3D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator +(Size3D augend, Size3D addend) => Add(augend, addend);

        /// <summary>
        /// Add two <see cref="Size3D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator +(Size3D augend, Point3D addend) => Add(augend, addend);

        /// <summary>
        /// Add a <see cref="Point3D" /> and a <see cref="Size3D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator +(Point3D augend, Size3D addend) => Add(augend, addend);

        /// <summary>
        /// Add a <see cref="Size3D" /> to a <see cref="Vector3D" /> class.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator +(Size3D augend, Vector3D addend) => Add(augend, addend);

        /// <summary>
        /// Add a <see cref="Vector3D" /> and a <see cref="Size3D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator +(Vector3D augend, Size3D addend) => Add(augend, addend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Size3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator -(Size3D value) => Negate(value);

        /// <summary>
        /// Subtract a <see cref="Size3D" /> from a <see cref="double" /> value.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator -(Size3D minuend, double subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="double" /> value from a <see cref="Size3D" />.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator -(double minuend, Size3D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Size3D" /> from another <see cref="Size3D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator -(Size3D minuend, Size3D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Size3D" /> from a <see cref="Point3D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator -(Size3D minuend, Point3D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Point3D" /> from another <see cref="Size3D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator -(Point3D minuend, Size3D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Size3D" /> from a <see cref="Vector3D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator -(Size3D minuend, Vector3D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Vector3D" /> from another <see cref="Size3D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator -(Vector3D minuend, Size3D subend) => Subtract(minuend, subend);

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
        public static Size3D operator *(Size3D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

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
        public static Size3D operator *(double multiplicand, Size3D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Scale a Size3D
        /// </summary>
        /// <param name="multiplicand">The Point</param>
        /// <param name="multiplier">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator *(Size3D multiplicand, Size3D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Point3D operator *(Size3D multiplicand, Point3D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Point3D operator *(Point3D multiplicand, Size3D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Vector3D operator *(Size3D multiplicand, Vector3D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Vector3D operator *(Vector3D multiplicand, Size3D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Divide a <see cref="Size3D" /> by a <see cref="double" /> value.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator /(Size3D dividend, double divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a <see cref="double" /> by a <see cref="Size3D" /> value.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator /(double dividend, Size3D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a Size3D
        /// </summary>
        /// <param name="dividend">The Point</param>
        /// <param name="divisor">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D operator /(Size3D dividend, Size3D divisor) => Divide(dividend, divisor);

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
        public static Point3D operator /(Size3D dividend, Point3D divisor) => Divide(dividend, divisor);

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
        public static Point3D operator /(Point3D dividend, Size3D divisor) => Divide(dividend, divisor);

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
        public static Vector3D operator /(Size3D dividend, Vector3D divisor) => Divide(dividend, divisor);

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
        public static Vector3D operator /(Vector3D dividend, Size3D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Compares two <see cref="Size3D" /> objects.
        /// The result specifies whether the values of the <see cref="Width" />, <see cref="Height" /> and <see cref="Depth" />
        /// values of the two <see cref="Size3D" /> objects are equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Size3D left, Size3D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Size3D" /> objects.
        /// The result specifies whether the values of the <see cref="Width" />, <see cref="Height" /> or <see cref="Depth" />
        /// values of the two <see cref="Size3D" /> objects are unequal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Size3D left, Size3D right) => !Equals(left, right);

        /// <summary>
        /// Converts the specified <see cref="Vector3D" /> structure to a <see cref="Size3D" /> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3D" /> to be converted.</param>
        /// <returns>
        /// Size - A Size equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size3D(Vector3D vector) => new Size3D(vector.I, vector.J, vector.K);

        /// <summary>
        /// Explicit conversion to Vector.
        /// </summary>
        /// <param name="size">Size - the Size to convert to a Vector</param>
        /// <returns>
        /// Vector - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector3D(Size3D size) => new Vector3D(size.Width, size.Height, size.Depth);

        /// <summary>
        /// Converts the specified <see cref="Point3D" /> structure to a <see cref="Size3D" /> structure.
        /// </summary>
        /// <param name="point">The <see cref="Point3D" /> to be converted.</param>
        /// <returns>
        /// Size - A Vector equal to this Size
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size3D(Point3D point) => new Size3D(point.X, point.Y, point.Z);

        /// <summary>
        /// Converts the specified <see cref="Size3D" /> to a <see cref="Point3D" />.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Point3D(Size3D size) => new Point3D(size.Width, size.Height, size.Depth);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">Size - the Size to convert to a Vector</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Size3D((double Width, double Height, double Depth) tuple) => new Size3D(tuple);

        /// <summary>
        /// Converts the specified <see cref="Size3D" /> structure to a <see cref="ValueTuple{T1, T2, T3}" /> structure.
        /// </summary>
        /// <param name="size">The <see cref="Size3D" /> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double Width, double Height, double Depth)(Size3D size) => (size.Width, size.Height, size.Depth);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Plus(Size3D value) => Operations.UnaryAdd(value.Width, value.Height, value.Depth);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Add(Size3D augend, double addend) => Operations.AddVectorUniform(augend.Width, augend.Height, augend.Depth, addend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Add(double augend, Size3D addend) => Operations.AddVectorUniform(addend.Width, addend.Height, addend.Depth, augend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Add(Size3D augend, Size3D addend) => Operations.AddVectors(augend.Width, augend.Height, augend.Depth, addend.Width, addend.Height, addend.Depth);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Add(Size3D augend, Point3D addend) => Operations.AddVectors(augend.Width, augend.Height, augend.Depth, addend.X, addend.Y, addend.Z);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Add(Point3D augend, Size3D addend) => Operations.AddVectors(augend.X, augend.Y, augend.Z, addend.Width, addend.Height, addend.Depth);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Add(Size3D augend, Vector3D addend) => Operations.AddVectors(augend.Width, augend.Height, augend.Depth, addend.I, addend.J, addend.K);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Add(Vector3D augend, Size3D addend) => Operations.AddVectors(augend.I, augend.J, augend.K, addend.Width, addend.Height, addend.Depth);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Negate(Size3D value) => Operations.NegateVector(value.Width, value.Height, value.Depth);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Subtract(Size3D minuend, double subend) => Operations.SubtractVectorUniform(minuend.Width, minuend.Height, minuend.Depth, subend);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Subtract(double minuend, Size3D subend) => Operations.SubtractVectorUniform(minuend, subend.Width, subend.Height, subend.Depth);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Subtract(Size3D minuend, Size3D subend) => Operations.SubtractVector(minuend.Width, minuend.Height, minuend.Depth, subend.Width, subend.Height, subend.Depth);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Subtract(Size3D minuend, Point3D subend) => Operations.SubtractVector(minuend.Width, minuend.Height, minuend.Depth, subend.X, subend.Y, subend.Z);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Subtract(Point3D minuend, Size3D subend) => Operations.SubtractVector(minuend.X, minuend.Y, minuend.Z, subend.Width, subend.Height, subend.Depth);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Subtract(Size3D minuend, Vector3D subend) => Operations.SubtractVector(minuend.Width, minuend.Height, minuend.Depth, subend.I, subend.J, subend.K);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Subtract(Vector3D minuend, Size3D subend) => Operations.SubtractVector(minuend.I, minuend.J, minuend.K, subend.Width, subend.Height, subend.Depth);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Multiply(Size3D multiplicand, double multiplier) => Operations.ScaleVector(multiplicand.Width, multiplicand.Height, multiplicand.Depth, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Multiply(double multiplicand, Size3D multiplier) => Operations.ScaleVector(multiplier.Width, multiplier.Height, multiplier.Depth, multiplicand);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Multiply(Size3D multiplicand, Size3D multiplier) => Operations.ScaleVectorParametric(multiplicand.Width, multiplicand.Height, multiplicand.Depth, multiplier.Width, multiplier.Height, multiplier.Depth);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Multiply(Size3D multiplicand, Point3D multiplier) => Operations.ScaleVectorParametric(multiplicand.Width, multiplicand.Height, multiplicand.Depth, multiplier.X, multiplier.Y, multiplier.Z);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Multiply(Point3D multiplicand, Size3D multiplier) => Operations.ScaleVectorParametric(multiplicand.X, multiplicand.Y, multiplicand.Z, multiplier.Width, multiplier.Height, multiplier.Depth);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Multiply(Size3D multiplicand, Vector3D multiplier) => Operations.ScaleVectorParametric(multiplicand.Width, multiplicand.Height, multiplicand.Depth, multiplier.I, multiplier.J, multiplier.K);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Multiply(Vector3D multiplicand, Size3D multiplier) => Operations.ScaleVectorParametric(multiplicand.I, multiplicand.J, multiplicand.K, multiplier.Width, multiplier.Height, multiplier.Depth);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Divide(Size3D dividend, double divisor) => Operations.DivideVectorUniform(dividend.Width, dividend.Height, dividend.Depth, divisor);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Divide(double dividend, Size3D divisor) => Operations.DivideByVectorUniform(dividend, divisor.Width, divisor.Height, divisor.Depth);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D Divide(Size3D dividend, Size3D divisor) => Operations.DivideVectorParametric(dividend.Width, dividend.Height, dividend.Depth, divisor.Width, divisor.Height, divisor.Depth);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Divide(Size3D dividend, Point3D divisor) => Operations.DivideVectorParametric(dividend.Width, dividend.Height, dividend.Depth, divisor.X, divisor.Y, divisor.Z);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Divide(Point3D dividend, Size3D divisor) => Operations.DivideVectorParametric(dividend.X, dividend.Y, dividend.Z, divisor.Width, divisor.Height, divisor.Depth);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Divide(Size3D dividend, Vector3D divisor) => Operations.ScaleVectorParametric(dividend.Width, dividend.Height, dividend.Depth, divisor.I, divisor.J, divisor.K);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Divide(Vector3D dividend, Size3D divisor) => Operations.DivideVectorParametric(dividend.I, dividend.J, dividend.K, divisor.Width, divisor.Height, divisor.Depth);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
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
        public bool Equals(Size3D other) => (Width == other.Width) && (Height == other.Height) && (Depth == other.Depth);

        /// <summary>
        /// Converts to valuetuple.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double Width, double Height, double Depth) ToValueTuple() => (Width, Height, Depth);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D FromValueTuple((double Width, double Height, double Depth) tuple) => new Size3D(tuple.Width, tuple.Height, tuple.Depth);

        /// <summary>
        /// Converts to size3d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Size3D ToSize3D() => new Size3D(Width, Height, Depth);

        /// <summary>
        /// Converts to size3d.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size3D FromSize3D(Size3D size) => new Size3D(size.Width, size.Height, size.Depth);

        /// <summary>
        /// Converts to vector3d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3D ToVector3D() => new Vector3D(Width, Height, Depth);

        /// <summary>
        /// Converts to Vector3D.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D FromVector3D(Vector3D vector) => new Vector3D(vector.I, vector.J, vector.K);

        /// <summary>
        /// Converts to point3d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point3D ToPoint3D() => new Point3D(Width, Height, Depth);

        /// <summary>
        /// Converts to Point3D.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D FromPoint3D(Point3D point) => new Point3D(point.X, point.Y, point.Z);
        #endregion

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Size3D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Size3D" /> data</param>
        /// <returns>
        /// Returns an instance of the <see cref="Size3D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [ParseMethod]
        public static Size3D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Size3D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Size3D" /> data</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Size3D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
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

        #region Standard Methods
        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(Width, Height, Depth);

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
        /// Creates a string representation of this <see cref="Size3D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Size3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Size3D);
            var s = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Size3D)}({nameof(Width)}:{Width.ToString(format, formatProvider)}{s} {nameof(Height)}:{Height.ToString(format, formatProvider)}{s} {nameof(Depth)}:{Depth.ToString(format, formatProvider)})";
        }
        #endregion Methods
    }
}
