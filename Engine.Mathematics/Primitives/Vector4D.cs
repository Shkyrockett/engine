// <copyright file="Vector4D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
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
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The <see cref="Vector4D" /> struct. Represents a vector in 4D coordinate space (double precision floating-point coordinates).
    /// </summary>
    /// <seealso cref="IVector{T}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(Vector4DConverter))]
    [DebuggerDisplay("{ToString()}")]
    public struct Vector4D
        : IVector<Vector4D>
    {
        #region Static Fields
        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" />, <see cref="J" />, <see cref="K" />, and <see cref="L" /> values set to zero.
        /// </summary>
        public static readonly Vector4D Empty = new Vector4D(0d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" />, <see cref="J" />, <see cref="K" />, and <see cref="L" /> values set to 1.
        /// </summary>
        public static readonly Vector4D Unit = new Vector4D(1d, 1d, 1d, 1d);

        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" />, <see cref="J" />, <see cref="K" />, and <see cref="L" /> values set to NaN.
        /// </summary>
        public static readonly Vector4D NaN = new Vector4D(double.NaN, double.NaN, double.NaN, double.NaN);

        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" /> set to 1, <see cref="J" /> set to 0, <see cref="K" /> set to 0, and <see cref="L" /> set to 0.
        /// </summary>
        public static readonly Vector4D XAxis = new Vector4D(1d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" /> set to 0, <see cref="J" /> set to 1, <see cref="K" /> set to 0, and <see cref="L" /> set to 0.
        /// </summary>
        public static readonly Vector4D YAxis = new Vector4D(0d, 1d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" /> set to 0, <see cref="J" /> set to 0, <see cref="K" /> set to 1, and <see cref="L" /> set to 0.
        /// </summary>
        public static readonly Vector4D ZAxis = new Vector4D(0d, 0d, 1d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" /> set to 0, <see cref="J" /> set to 0, <see cref="K" /> set to 0, and <see cref="L" /> set to 1.
        /// </summary>
        public static readonly Vector4D WAxis = new Vector4D(0d, 0d, 0d, 1d);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> class as a copy of the one provided.
        /// </summary>
        /// <param name="vector4D">A <see cref="Vector4D" /> class to clone.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D(Vector4D vector4D)
            : this(vector4D.I, vector4D.J, vector4D.K, vector4D.L)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> class.
        /// </summary>
        /// <param name="i">The <see cref="I" /> component of the <see cref="Vector4D" /> class.</param>
        /// <param name="j">The <see cref="J" /> component of the <see cref="Vector4D" /> class.</param>
        /// <param name="k">The <see cref="K" /> component of the <see cref="Vector4D" /> class.</param>
        /// <param name="l">The <see cref="L" /> component of the <see cref="Vector4D" /> class.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D(double i, double j, double k, double l)
            : this()
        {
            (I, J, K, L) = (i, j, k, l);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> class.
        /// </summary>
        /// <param name="tuple">The X, Y, Z and W values in tuple form.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D((double X, double Y, double Z, double W) tuple)
            : this()
        {
            (I, J, K, L) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> struct.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D((double X, double Y, double Z, double W) a, (double X, double Y, double Z, double W) b)
            : this(a.X, a.Y, a.Z, a.W, b.X, b.Y, b.Z, b.W)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> class.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D(Vector4D a, Vector4D b)
            : this(a.I, a.J, a.K, a.L, b.I, b.J, b.K, b.L)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> class.
        /// </summary>
        /// <param name="aI">The aI.</param>
        /// <param name="aJ">The aJ.</param>
        /// <param name="aK">The aK.</param>
        /// <param name="aL">The aL.</param>
        /// <param name="bI">The bI.</param>
        /// <param name="bJ">The bJ.</param>
        /// <param name="bK">The bK.</param>
        /// <param name="bL">The bL.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D(double aI, double aJ, double aK, double aL, double bI, double bJ, double bK, double bL)
            : this()
        {
            // This creates a normalized vector. It is debatable that it is what we actually want. We may only want the first line.

            // Find the new vector.
            (var i, var j, var k, var l) = (bI - aI, bJ - aJ, bK - aK, bL - aL);

            // Get the length of the vector.
            var d = Sqrt((i * i) + (j * j) + (k * k) + (l * l));

            // Calculate the normalized vector.
            (I, J, K, L) = d == 0d ? (i, j, k, l) : (i * 1d / d, j * 1d / d, k * 1d / d, l * 1d / d);
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Vector4D" /> to a <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double i, out double j, out double k, out double l) => (i, j, k, l) = (I, J, K, L);
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the I or first Component of a 4D Vector
        /// </summary>
        /// <value>
        /// The i.
        /// </value>
        [DataMember(Name = nameof(I)), XmlAttribute(nameof(I)), SoapAttribute(nameof(I))]
        public double I { get; set; }

        /// <summary>
        /// Gets or sets the j or second Component of a 4D Vector
        /// </summary>
        /// <value>
        /// The j.
        /// </value>
        [DataMember(Name = nameof(J)), XmlAttribute(nameof(J)), SoapAttribute(nameof(J))]
        public double J { get; set; }

        /// <summary>
        /// Gets or sets the k or third Component of a 4D Vector
        /// </summary>
        /// <value>
        /// The k.
        /// </value>
        [DataMember(Name = nameof(K)), XmlAttribute(nameof(K)), SoapAttribute(nameof(K))]
        public double K { get; set; }

        /// <summary>
        /// Gets or sets the l or fourth Component of a 4D Vector
        /// </summary>
        /// <value>
        /// The l.
        /// </value>
        [DataMember(Name = nameof(L)), XmlAttribute(nameof(L)), SoapAttribute(nameof(L))]
        public double L { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Vector4D" /> is empty.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if this instance is empty; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public bool IsEmpty => IsEmptyVector(I, J, K, L);

        /// <summary>
        /// Gets the magnitude.
        /// </summary>
        /// <value>
        /// The magnitude.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double Magnitude => VectorMagnitude(I, J, K, L);

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double Length => VectorMagnitude(I, J, K, L);

        /// <summary>
        /// Gets the length squared.
        /// </summary>
        /// <value>
        /// The length squared.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double LengthSquared => VectorMagnitudeSquared(I, J, K, L);
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Vector4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(Vector4D value) => Plus(value);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(Vector4D augend, double addend) => Add(augend, addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(double augend, Vector4D addend) => Add(augend, addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(Vector4D augend, Vector4D addend) => Add(augend, addend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Vector4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Vector4D value) => Negate(value);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Vector4D minuend, double subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(double minuend, Vector4D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract Vectors
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Vector4D minuend, Vector4D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplicand">The Point</param>
        /// <param name="multiplier">The Multiplier</param>
        /// <returns>
        /// A Vector Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator *(Vector4D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplicand">The Multiplier</param>
        /// <param name="multiplier">The Point</param>
        /// <returns>
        /// A Vector Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator *(double multiplicand, Vector4D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Divide a <see cref="Vector4D" />
        /// </summary>
        /// <param name="dividend">The <see cref="Vector4D" /></param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>
        /// A <see cref="Vector4D" /> divided by the divisor
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator /(Vector4D dividend, double divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a <see cref="Vector4D" />
        /// </summary>
        /// <param name="dividend">The <see cref="Vector4D" /></param>
        /// <param name="divisor">The divisor</param>
        /// <returns>
        /// A <see cref="Vector4D" /> divided by the divisor
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator /(double dividend, Vector4D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector4D a, Vector4D b) => Equals(a, b);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector4D a, Vector4D b) => !Equals(a, b);

        /// <summary>
        /// Converts the specified <see cref="Vector4D" /> structure to a <see cref="ValueTuple{T1, T2, T3, T4}" /> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector4D" /> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double I, double J, double K, double L)(Vector4D vector) => (vector.I, vector.J, vector.K, vector.L);

        /// <summary>
        /// Tuple to Vector4D
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector4D((double X, double Y, double Z, double W) tuple) => new Vector4D(tuple);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D Plus() => Operations.Plus(I, J, K, L);

        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Plus(Vector4D value) => Operations.Plus(value.I, value.J, value.K, value.L);

        /// <summary>
        /// Adds the specified addend.
        /// </summary>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D Add(double addend) => AddVectorUniform(I, J, K, L, addend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Add(Vector4D augend, double addend) => AddVectorUniform(augend.I, augend.J, augend.K, augend.L, addend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Add(double augend, Vector4D addend) => AddVectorUniform(addend.I, addend.J, addend.K, addend.L, augend);

        /// <summary>
        /// Adds the specified addend.
        /// </summary>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D Add(Vector4D addend) => AddVectors(I, J, K, L, addend.I, addend.J, addend.K, addend.L);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Add(Vector4D augend, Vector4D addend) => AddVectors(augend.I, augend.J, augend.K, augend.L, addend.I, addend.J, addend.K, addend.L);

        /// <summary>
        /// Negates this instance.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D Negate() => Operations.Negate(I, J, K, L);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Negate(Vector4D value) => Operations.Negate(value.I, value.J, value.K, value.L);

        /// <summary>
        /// Subtracts the specified subend.
        /// </summary>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D Subtract(double subend) => SubtractVectorUniform(I, J, K, L, subend);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Subtract(Vector4D minuend, double subend) => SubtractVectorUniform(minuend.I, minuend.J, minuend.K, minuend.L, subend);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Subtract(double minuend, Vector4D subend) => SubtractFromMinuend(minuend, subend.I, subend.J, subend.K, subend.L);

        /// <summary>
        /// Subtracts the specified subend.
        /// </summary>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D Subtract(Vector4D subend) => SubtractVector(I, J, K, L, subend.I, subend.J, subend.K, subend.L);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Subtract(Vector4D minuend, Vector4D subend) => SubtractVector(minuend.I, minuend.J, minuend.K, minuend.L, subend.I, subend.J, subend.K, subend.L);

        /// <summary>
        /// Multiplies the specified multiplier.
        /// </summary>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D Multiply(double multiplier) => ScaleVector(I, J, K, L, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Multiply(Vector4D multiplicand, double multiplier) => ScaleVector(multiplicand.I, multiplicand.J, multiplicand.K, multiplicand.L, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Multiply(double multiplicand, Vector4D multiplier) => ScaleVector(multiplier.I, multiplier.J, multiplier.K, multiplier.L, multiplicand);

        /// <summary>
        /// Divides the specified divisor.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D Divide(double divisor) => DivideVectorUniform(I, J, K, L, divisor);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Divide(Vector4D dividend, double divisor) => DivideVectorUniform(dividend.I, dividend.J, dividend.K, dividend.L, divisor);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Divide(double dividend, Vector4D divisor) => DivideByVectorUniform(dividend, divisor.I, divisor.I, divisor.K, divisor.L);

        /// <summary>
        /// Dots the product.
        /// </summary>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double DotProduct(Vector4D right) => Operations.DotProduct(I, J, K, L, right.I, right.J, right.K, right.L);

        /// <summary>
        /// Dots the product.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(Vector4D left, Vector4D right) => Operations.DotProduct(left.I, left.J, left.K, left.L, right.I, right.J, right.K, right.L);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Vector4D d && Equals(d);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] Vector4D other) => I == other.I && J == other.J && K == other.K && L == other.L;

        /// <summary>
        /// Converts to valuetuple.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double I, double J, double K, double L) ToValueTuple() => (I, J, K, L);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D FromValueTuple((double X, double Y, double Z, double W) tuple) => new Vector4D(tuple);

        /// <summary>
        /// Converts to vector4d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D ToVector4D() => new Vector4D(I, J, K, L);
        #endregion

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Vector4D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Vector4D" /> data</param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector4D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [ParseMethod]
        public static Vector4D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Vector4D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Vector4D" /> data</param>
        /// <param name="formatProvider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector4D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        public static Vector4D Parse(string source, IFormatProvider formatProvider)
        {
            var tokenizer = new Tokenizer(source, formatProvider);
            var firstToken = tokenizer.NextTokenRequired();

            var value = new Vector4D(
                Convert.ToDouble(firstToken, formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider)
                );

            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }
        #endregion Factories

        #region Standard Methods
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(I, J, K, L);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Vector4D" /> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Vector4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector4D" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="formatProvider">The <see cref="CultureInfo" /> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Vector4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider formatProvider) => ToString("R" /* format string */, formatProvider);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector4D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The <see cref="CultureInfo"/> provider.</param>
        /// <returns>A string representation of this <see cref="Vector4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Vector4D);
            var s = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Vector4D)}({nameof(I)}: {I.ToString(format, formatProvider)}{s} {nameof(J)}: {J.ToString(format, formatProvider)}{s} {nameof(K)}: {K.ToString(format, formatProvider)}{s} {nameof(L)}: {L.ToString(format, formatProvider)})";
        }
        #endregion 
    }
}
