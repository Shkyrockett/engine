// <copyright file="Vector5D.cs" company="Shkyrockett" >
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
    /// The <see cref="Vector5D"/> struct. Represents a vector in 5D coordinate space (double precision floating-point coordinates).
    /// </summary>
    [DataContract, Serializable]
    [TypeConverter(typeof(Vector5DConverter))]
    [DebuggerDisplay("{ToString()}")]
    public struct Vector5D
        : IVector<Vector5D>
    {
        #region Static Fields
        /// <summary>
        /// Represents a <see cref="Vector5D"/> that has <see cref="I"/>, <see cref="J"/>, <see cref="K"/>, <see cref="L"/>, and <see cref="M"/> values set to zero.
        /// </summary>
        public static readonly Vector5D Empty = new Vector5D(0d, 0d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector5D"/> that has <see cref="I"/>, <see cref="J"/>, <see cref="K"/>, <see cref="L"/>, and <see cref="M"/> values set to 1.
        /// </summary>
        public static readonly Vector5D Unit = new Vector5D(1d, 1d, 1d, 1d, 1d);

        /// <summary>
        /// Represents a <see cref="Vector5D"/> that has <see cref="I"/>, <see cref="J"/>, <see cref="K"/>, <see cref="L"/>, and <see cref="M"/> values set to NaN.
        /// </summary>
        public static readonly Vector5D NaN = new Vector5D(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);

        /// <summary>
        /// Represents a <see cref="Vector5D"/> that has <see cref="I"/> set to 1, <see cref="J"/> set to 0, <see cref="K"/> set to 0, <see cref="L"/> set to 0, and <see cref="M"/> set to 0.
        /// </summary>
        public static readonly Vector5D XAxis = new Vector5D(1d, 0d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector5D"/> that has <see cref="I"/> set to 0, <see cref="J"/> set to 1, <see cref="K"/> set to 0, <see cref="L"/> set to 0, and <see cref="M"/> set to 0.
        /// </summary>
        public static readonly Vector5D YAxis = new Vector5D(0d, 1d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector5D"/> that has <see cref="I"/> set to 0, <see cref="J"/> set to 0, <see cref="K"/> set to 1, <see cref="L"/> set to 1, and <see cref="M"/> set to 0.
        /// </summary>
        public static readonly Vector5D ZAxis = new Vector5D(0d, 0d, 1d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector5D"/> that has <see cref="I"/> set to 0, <see cref="J"/> set to 0, <see cref="K"/> set to 0, <see cref="L"/> set to 1, and <see cref="M"/> set to 0.
        /// </summary>
        public static readonly Vector5D WAxis = new Vector5D(0d, 0d, 0d, 1d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector5D"/> that has <see cref="I"/> set to 0, <see cref="J"/> set to 0, <see cref="K"/> set to 0, <see cref="L"/> set to 0, and <see cref="M"/> set to 1.
        /// </summary>
        public static readonly Vector5D VAxis = new Vector5D(0d, 0d, 0d, 0d, 1d);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector5D" /> class as a copy of the one provided.
        /// </summary>
        /// <param name="vector5D">A <see cref="Vector5D" /> class to clone.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D(Vector5D vector5D)
            : this(vector5D.I, vector5D.J, vector5D.K, vector5D.L, vector5D.M)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector5D" /> class.
        /// </summary>
        /// <param name="i">The <see cref="I" /> component of the <see cref="Vector5D" /> class.</param>
        /// <param name="j">The <see cref="J" /> component of the <see cref="Vector5D" /> class.</param>
        /// <param name="k">The <see cref="K" /> component of the <see cref="Vector5D" /> class.</param>
        /// <param name="l">The <see cref="L" /> component of the <see cref="Vector5D" /> class.</param>
        /// <param name="m">The m.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D(double i, double j, double k, double l, double m)
            : this()
        {
            (I, J, K, L, M) = (i, j, k, l, m); ;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector5D" /> class.
        /// </summary>
        /// <param name="tuple">The X, Y, Z and W values in tuple form.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D((double X, double Y, double Z, double W, double V) tuple)
            : this()
        {
            (I, J, K, L, M) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector5D" /> struct.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D((double X, double Y, double Z, double W, double V) a, (double X, double Y, double Z, double W, double V) b)
            : this(a.X, a.Y, a.Z, a.W, a.V, b.X, b.Y, b.Z, b.W, b.V)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector5D" /> class.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D(Vector5D a, Vector5D b)
            : this(a.I, a.J, a.K, a.L, a.M, b.I, b.J, b.K, b.L, b.M)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector5D" /> class.
        /// </summary>
        /// <param name="aI">The aI.</param>
        /// <param name="aJ">The aJ.</param>
        /// <param name="aK">The aK.</param>
        /// <param name="aL">The aL.</param>
        /// <param name="aM">a m.</param>
        /// <param name="bI">The bI.</param>
        /// <param name="bJ">The bJ.</param>
        /// <param name="bK">The bK.</param>
        /// <param name="bL">The bL.</param>
        /// <param name="bM">The b m.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D(double aI, double aJ, double aK, double aL, double aM, double bI, double bJ, double bK, double bL, double bM)
            : this()
        {
            // This creates a normalized vector. It is debatable that it is what we actually want. We may only want the first line.

            // Find the new vector.
            (var i, var j, var k, var l, var m) = (bI - aI, bJ - aJ, bK - aK, bL - aL, bM - aM);

            // Get the length of the vector.
            var d = Sqrt((i * i) + (j * j) + (k * k) + (l * l) + (m * m));

            // Calculate the normalized vector.
            (I, J, K, L, M) = d == 0d ? (i, j, k, l, m) : (i * 1d / d, j * 1d / d, k * 1d / d, l * 1d / d, m * 1d / d);
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Vector5D" /> to a <see cref="ValueTuple{T1, T2, T3, T4, T5}" />.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double i, out double j, out double k, out double l, out double m) => (i, j, k, l, m) = (I, J, K, L, M);
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the I or first Component of a 5D Vector
        /// </summary>
        /// <value>
        /// The i.
        /// </value>
        [DataMember(Name = nameof(I)), XmlAttribute(nameof(I)), SoapAttribute(nameof(I))]
        public double I { get; set; }

        /// <summary>
        /// Gets or sets the j or second Component of a 5D Vector
        /// </summary>
        /// <value>
        /// The j.
        /// </value>
        [DataMember(Name = nameof(J)), XmlAttribute(nameof(J)), SoapAttribute(nameof(J))]
        public double J { get; set; }

        /// <summary>
        /// Gets or sets the k or third Component of a 5D Vector
        /// </summary>
        /// <value>
        /// The k.
        /// </value>
        [DataMember(Name = nameof(K)), XmlAttribute(nameof(K)), SoapAttribute(nameof(K))]
        public double K { get; set; }

        /// <summary>
        /// Gets or sets the l or fourth Component of a 5D Vector
        /// </summary>
        /// <value>
        /// The l.
        /// </value>
        [DataMember(Name = nameof(L)), XmlAttribute(nameof(L)), SoapAttribute(nameof(L))]
        public double L { get; set; }

        /// <summary>
        /// Gets or sets the m or fifth Component of a 5D Vector
        /// </summary>
        /// <value>
        /// The m.
        /// </value>
        [DataMember(Name = nameof(M)), XmlAttribute(nameof(M)), SoapAttribute(nameof(M))]
        public double M { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Vector4D" /> is empty.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if this instance is empty; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public bool IsEmpty => IsEmptyVector(I, J, K, L, M);

        /// <summary>
        /// Gets the magnitude.
        /// </summary>
        /// <value>
        /// The magnitude.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double Magnitude => VectorMagnitude(I, J, K, L, M);

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double Length => VectorMagnitude(I, J, K, L, M);

        /// <summary>
        /// Gets the length squared.
        /// </summary>
        /// <value>
        /// The length squared.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double LengthSquared => VectorMagnitudeSquared(I, J, K, L, M);
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Vector5D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator +(Vector5D value) => Plus(value);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator +(Vector5D augend, double addend) => Add(augend, addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator +(double augend, Vector5D addend) => Add(augend, addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator +(Vector5D augend, Vector5D addend) => Add(augend, addend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Vector5D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator -(Vector5D value) => Negate(value);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator -(Vector5D minuend, double subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subend"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator -(double minuend, Vector5D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract Vectors
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subend"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator -(Vector5D minuend, Vector5D subend) => Subtract(minuend, subend);

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
        public static Vector5D operator *(Vector5D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

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
        public static Vector5D operator *(double multiplicand, Vector5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator *(Vector5D multiplicand, Matrix5x5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator *(Matrix5x5D multiplicand, Vector5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Divide a <see cref="Vector5D" />
        /// </summary>
        /// <param name="dividend">The <see cref="Vector5D" /></param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>
        /// A <see cref="Vector5D" /> divided by the divisor
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator /(Vector5D dividend, double divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a <see cref="Vector5D" />
        /// </summary>
        /// <param name="dividend">The <see cref="Vector5D" /></param>
        /// <param name="divisor">The divisor</param>
        /// <returns>
        /// A <see cref="Vector5D" /> divided by the divisor
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator /(double dividend, Vector5D divisor) => Divide(dividend, divisor);

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
        public static bool operator ==(Vector5D a, Vector5D b) => Equals(a, b);

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
        public static bool operator !=(Vector5D a, Vector5D b) => !Equals(a, b);

        /// <summary>
        /// Converts the specified <see cref="Vector5D"/> structure to a <see cref="ValueTuple{T1, T2, T3, T4}"/> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector5D"/> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double I, double J, double K, double L, double M)(Vector5D vector) => (vector.I, vector.J, vector.K, vector.L, vector.M);

        /// <summary>
        /// Tuple to Vector5D
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector5D((double X, double Y, double Z, double W, double V) tuple) => new Vector5D(tuple);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses this instance.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D Plus() => Operations.Plus(I, J, K, L, M);

        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Plus(Vector5D value) => Operations.Plus(value.I, value.J, value.K, value.L, value.M);

        /// <summary>
        /// Adds the specified addend.
        /// </summary>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D Add(double addend) => AddVectorUniform(I, J, K, L, M, addend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Add(Vector5D augend, double addend) => AddVectorUniform(augend.I, augend.J, augend.K, augend.L, augend.M, addend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Add(double augend, Vector5D addend) => AddVectorUniform(addend.I, addend.J, addend.K, addend.L, addend.M, augend);

        /// <summary>
        /// Adds the specified addend.
        /// </summary>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D Add(Vector5D addend) => AddVectors(I, J, K, L, M, addend.I, addend.J, addend.K, addend.L, addend.M);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Add(Vector5D augend, Vector5D addend) => AddVectors(augend.I, augend.J, augend.K, augend.L, augend.M, addend.I, addend.J, addend.K, addend.L, addend.M);

        /// <summary>
        /// Negates this instance.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D Negate() => Operations.Negate(I, J, K, L, M);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Negate(Vector5D value) => Operations.Negate(value.I, value.J, value.K, value.L, value.M);

        /// <summary>
        /// Subtracts the specified subend.
        /// </summary>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D Subtract(double subend) => SubtractVectorUniform(I, J, K, L, M, subend);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Subtract(Vector5D minuend, double subend) => SubtractVectorUniform(minuend.I, minuend.J, minuend.K, minuend.L, minuend.M, subend);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Subtract(double minuend, Vector5D subend) => SubtractFromMinuend(minuend, subend.I, subend.J, subend.K, subend.L, subend.M);

        /// <summary>
        /// Subtracts the specified subend.
        /// </summary>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D Subtract(Vector5D subend) => SubtractVector(I, J, K, L, M, subend.I, subend.J, subend.K, subend.L, subend.M);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Subtract(Vector5D minuend, Vector5D subend) => SubtractVector(minuend.I, minuend.J, minuend.K, minuend.L, minuend.M, subend.I, subend.J, subend.K, subend.L, subend.M);

        /// <summary>
        /// Multiplies the specified multiplier.
        /// </summary>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D Multiply(double multiplier) => ScaleVector(I, J, K, L, M, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Multiply(Vector5D multiplicand, double multiplier) => ScaleVector(multiplicand.I, multiplicand.J, multiplicand.K, multiplicand.L, multiplicand.M, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Multiply(double multiplicand, Vector5D multiplier) => ScaleVector(multiplier.I, multiplier.J, multiplier.K, multiplier.L, multiplier.M, multiplicand);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Multiply(Vector5D multiplicand, Matrix5x5D multiplier) => MultiplyVector5DMatrix5x5(multiplicand.I, multiplicand.J, multiplicand.K, multiplicand.L, multiplicand.M, multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M0x3, multiplier.M0x4, multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M1x3, multiplier.M1x4, multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplier.M2x3, multiplier.M2x4, multiplier.M3x0, multiplier.M3x1, multiplier.M3x2, multiplier.M3x3, multiplier.M3x4, multiplier.M4x0, multiplier.M4x1, multiplier.M4x2, multiplier.M4x3, multiplier.M4x4);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Multiply(Matrix5x5D multiplicand, Vector5D multiplier) => MultiplyVector5DMatrix5x5(multiplier.I, multiplier.J, multiplier.K, multiplier.L, multiplier.M, multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M0x3, multiplicand.M0x4, multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M1x3, multiplicand.M1x4, multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplicand.M2x3, multiplicand.M2x4, multiplicand.M3x0, multiplicand.M3x1, multiplicand.M3x2, multiplicand.M3x3, multiplicand.M3x4, multiplicand.M4x0, multiplicand.M4x1, multiplicand.M4x2, multiplicand.M4x3, multiplicand.M4x4);

        /// <summary>
        /// Divides the specified divisor.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D Divide(double divisor) => DivideVectorUniform(I, J, K, L, M, divisor);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Divide(Vector5D dividend, double divisor) => DivideVectorUniform(dividend.I, dividend.J, dividend.K, dividend.L, dividend.M, divisor);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Divide(double dividend, Vector5D divisor) => DivideByVectorUniform(dividend, divisor.I, divisor.I, divisor.K, divisor.L, divisor.M);

        /// <summary>
        /// Dots the product.
        /// </summary>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double DotProduct(Vector5D right) => Operations.DotProduct(I, J, K, L, M, right.I, right.J, right.K, right.L, right.M);

        /// <summary>
        /// Dots the product.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(Vector5D left, Vector5D right) => Operations.DotProduct(left.I, left.J, left.K, left.L, left.M, right.I, right.J, right.K, right.L, right.M);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Vector5D d && Equals(d);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] Vector5D other) => I == other.I && J == other.J && K == other.K && L == other.L && L == other.M;

        /// <summary>
        /// Converts to valuetuple.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double I, double J, double K, double L, double M) ToValueTuple() => (I, J, K, L, M);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D FromValueTuple((double X, double Y, double Z, double W, double V) tuple) => new Vector5D(tuple);

        /// <summary>
        /// Converts to vector5D.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D ToVector5D() => new Vector5D(I, J, K, L, M);
        #endregion

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Vector5D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Vector5D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector5D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [ParseMethod]
        public static Vector5D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Vector5D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Vector5D"/> data </param>
        /// <param name="formatProvider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector5D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Vector5D Parse(string source, IFormatProvider formatProvider)
        {
            var tokenizer = new Tokenizer(source, formatProvider);
            var firstToken = tokenizer.NextTokenRequired();

            var value = new Vector5D(
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

        #region Standard Methods
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(I, J, K, L, M);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Vector5D"/> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Vector5D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector5D" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="formatProvider">The <see cref="CultureInfo" /> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Vector5D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider formatProvider) => ToString("R" /* format string */, formatProvider);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector5D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The <see cref="CultureInfo"/> provider.</param>
        /// <returns>A string representation of this <see cref="Vector5D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Vector5D);
            var s = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Vector5D)}({nameof(I)}: {I.ToString(format, formatProvider)}{s} {nameof(J)}: {J.ToString(format, formatProvider)}{s} {nameof(K)}: {K.ToString(format, formatProvider)}{s} {nameof(L)}: {L.ToString(format, formatProvider)}{s} {nameof(M)}: {M.ToString(format, formatProvider)})";
        }
        #endregion 
    }
}
