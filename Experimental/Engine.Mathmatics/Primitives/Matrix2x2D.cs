// <copyright file="Matrix2x2D.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections;
using System.Collections.Generic;
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
    /// The <see cref="Matrix2x2D" /> struct.
    /// </summary>
    /// <seealso cref="Engine.IMatrix{Engine.Matrix2x2D, Engine.Vector2D}" />
    /// <seealso cref="IMatrix{T, T}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(Matrix2x2DConverter))]
    [DebuggerDisplay("{ToString()}")]
    public struct Matrix2x2D
        : IMatrix<Matrix2x2D, Vector2D>
    {
        #region Static Fields
        /// <summary>
        /// An Empty <see cref="Matrix2x2D" />.
        /// </summary>
        public static readonly Matrix2x2D Empty = new Matrix2x2D(
            0d, 0d,
            0d, 0d);

        /// <summary>
        /// An Identity <see cref="Matrix2x2D" />.
        /// </summary>
        public static readonly Matrix2x2D Identity = new Matrix2x2D(
            1d, 0d,
            0d, 1d);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix2x2D"/> class.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix2x2D(double m0x0, double m0x1, double m1x0, double m1x1)
            : this()
        {
            M0x0 = m0x0;
            M0x1 = m0x1;
            M1x0 = m1x0;
            M1x1 = m1x1;
        }

        /// <summary>
        /// Create a new Matrix from 2 Vertex2 objects.
        /// </summary>
        /// <param name="xAxis">The x axis.</param>
        /// <param name="yAxis">The y axis.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix2x2D(Vector2D xAxis, Vector2D yAxis)
            : this(xAxis.I, xAxis.J, yAxis.I, yAxis.J)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix2x2D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix2x2D((
            double M1x1, double M1x2,
            double M2x1, double M2x2) tuple)
            : this()
        {
            (M0x0, M0x1, M1x0, M1x1) = tuple;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Matrix2x2D" /> to a <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </summary>
        /// <param name="m0x0">The m0x0.</param>
        /// <param name="m0x1">The m0x1.</param>
        /// <param name="m1x0">The m1x0.</param>
        /// <param name="m1x1">The m1x1.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(
            out double m0x0, out double m0x1,
            out double m1x0, out double m1x1)
        {
            m0x0 = M0x0;
            m0x1 = M0x1;
            m1x0 = M1x0;
            m1x1 = M1x1;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the m0x0.
        /// </summary>
        /// <value>
        /// The M0X0.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M0x0 { get; set; }

        /// <summary>
        /// Gets or sets the m0x1.
        /// </summary>
        /// <value>
        /// The M0X1.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M0x1 { get; set; }

        /// <summary>
        /// Gets or sets the m1x0.
        /// </summary>
        /// <value>
        /// The M1X0.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M1x0 { get; set; }

        /// <summary>
        /// Gets or sets the m1x1.
        /// </summary>
        /// <value>
        /// The M1X1.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M1x1 { get; set; }

        /// <summary>
        /// Gets or sets the cx.
        /// </summary>
        /// <value>
        /// The cx.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The First column of the " + nameof(Matrix2x2D))]
        public Vector2D Cx { get { return new Vector2D(M0x0, M1x0); } set { (M0x0, M1x0) = value; } }

        /// <summary>
        /// Gets or sets the cy.
        /// </summary>
        /// <value>
        /// The cy.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Second column of the " + nameof(Matrix2x2D))]
        public Vector2D Cy { get { return new Vector2D(M0x1, M1x1); } set { (M0x1, M1x1) = value; } }

        /// <summary>
        /// Gets or sets the rx.
        /// </summary>
        /// <value>
        /// The rx.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The First row of the " + nameof(Matrix2x2D))]
        public Vector2D Rx { get { return new Vector2D(M0x0, M0x1); } set { (M0x0, M0x1) = value; } }

        /// <summary>
        /// Gets or sets the ry.
        /// </summary>
        /// <value>
        /// The ry.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Second row of the " + nameof(Matrix2x2D))]
        public Vector2D Ry { get { return new Vector2D(M1x0, M1x1); } set { (M1x0, M1x1) = value; } }

        /// <summary>
        /// Gets the determinant.
        /// </summary>
        /// <value>
        /// The determinant.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Determinant => MatrixDeterminant(M0x0, M0x1, M1x0, M1x1);

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <value>
        /// The transposed.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix2x2D Transposed => TransposeMatrix(M0x0, M0x1, M1x0, M1x1);

        /// <summary>
        /// Gets the adjoint.
        /// </summary>
        /// <value>
        /// The adjoint.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix2x2D Adjoint => AdjointMatrix(M0x0, M0x1, M1x0, M1x1);

        /// <summary>
        /// Gets the cofactor.
        /// </summary>
        /// <value>
        /// The cofactor.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix2x2D Cofactor => CofactorMatrix(M0x0, M0x1, M1x0, M1x1);

        /// <summary>
        /// Gets the inverted.
        /// </summary>
        /// <value>
        /// The inverted.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix2x2D Inverted => InvertMatrix(M0x0, M0x1, M1x0, M1x1);

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if this instance is identity; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsIdentity => IsMatrixIdentity(M0x0, M0x1, M1x0, M1x1);
        #endregion Properties

        #region Operators
        /// <summary>
        /// Unary Add all the items in the Matrix.
        /// </summary>
        /// <param name="value">The matrix.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D operator +(Matrix2x2D value) => Plus(value);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D operator +(Matrix2x2D augend, Matrix2x2D addend) => Add(augend, addend);

        /// <summary>
        /// Negates all the items in the Matrix.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D operator -(Matrix2x2D value) => Negate(value);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subend"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D operator -(Matrix2x2D minuend, Matrix2x2D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="multiplicand"></param>
        /// <param name="multiplier"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D operator *(Matrix2x2D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="multiplicand"></param>
        /// <param name="multiplier"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D operator *(double multiplicand, Matrix2x2D multiplier) => Multiply(multiplier, multiplicand);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="multiplicand"></param>
        /// <param name="multiplier"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D operator *(Matrix2x2D multiplicand, Matrix2x2D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Compares two Matrix instances for exact equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <param name="matrix1">The first Matrix to compare</param>
        /// <param name="matrix2">The second Matrix to compare</param>
        /// <returns>
        /// bool - true if the two Matrix instances are exactly equal, false otherwise
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Matrix2x2D matrix1, Matrix2x2D matrix2) => Equals(matrix1, matrix2);

        /// <summary>
        /// Compares two Matrix instances for exact inequality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <param name="matrix1">The first Matrix to compare</param>
        /// <param name="matrix2">The second Matrix to compare</param>
        /// <returns>
        /// bool - true if the two Matrix instances are exactly unequal, false otherwise
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Matrix2x2D matrix1, Matrix2x2D matrix2) => !Equals(matrix1, matrix2);

        /// <summary>
        ///   <see cref="Matrix2x2D" /> to <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double, double, double, double)(Matrix2x2D matrix) => (matrix.M0x0, matrix.M0x1, matrix.M1x0, matrix.M1x1);

        /// <summary>
        ///   <see cref="ValueTuple{T1, T2, T3, T4}" /> to <see cref="Matrix2x2D" />.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Matrix2x2D((double, double, double, double) tuple) => FromValueTuple(tuple);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Plus(Matrix2x2D value) => UnaryAdd(value.M0x0, value.M0x1, value.M1x0, value.M1x1);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Add(Matrix2x2D augend, Matrix2x2D addend) => AddMatrix(augend.M0x0, augend.M0x1, augend.M1x0, augend.M1x1, addend.M0x0, addend.M0x1, addend.M1x0, addend.M1x1);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Negate(Matrix2x2D value) => Operations.Negate(value.M0x0, value.M0x1, value.M1x0, value.M1x1);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Subtract(Matrix2x2D minuend, Matrix2x2D subend) => Subtract2x2x2x2(minuend.M0x0, minuend.M0x1, minuend.M1x0, minuend.M1x1, subend.M0x0, subend.M0x1, subend.M1x0, subend.M1x1);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Multiply(Matrix2x2D multiplicand, double multiplier) => Scale2x2(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M1x0, multiplicand.M1x1, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Multiply(double multiplicand, Matrix2x2D multiplier) => Scale2x2(multiplier.M0x0, multiplier.M0x1, multiplier.M1x0, multiplier.M1x1, multiplicand);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Multiply(Matrix2x2D multiplicand, Matrix2x2D multiplier) => Multiply2x2x2x2(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M1x0, multiplicand.M1x1, multiplier.M0x0, multiplier.M0x1, multiplier.M1x0, multiplier.M1x1);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => base.Equals(obj);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Matrix2x2D other) => M0x0 == other.M0x0 && M0x1 == other.M0x1 && M1x0 == other.M1x0 && M1x1 == other.M1x1;

        /// <summary>
        /// Converts to valuetuple.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double, double, double, double) ToValueTuple() => (M0x0, M0x1, M1x0, M1x1);

        /// <summary>
        /// Converts to matrix2x2d.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D FromValueTuple((double, double, double, double) tuple) => new Matrix2x2D(tuple);
        #endregion

        #region Factories
        /// <summary>
        /// The from rotation.
        /// </summary>
        /// <param name="radianAngle">The radianAngle.</param>
        /// <returns>
        /// The <see cref="Matrix2x2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D FromRotation(double radianAngle)
        {
            var sin = Sin(radianAngle);
            var cos = Cos(radianAngle);
            return new Matrix2x2D(cos, sin, -sin, cos);
        }

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D FromScale(Vector2D scale) => new Matrix2x2D(scale.I, 0d, 0d, scale.J);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scaleX">The scale factor in the x dimension</param>
        /// <param name="scaleY">The scale factor in the y dimension</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D FromScale(double scaleX, double scaleY) => new Matrix2x2D(scaleX, 0d, 0d, scaleY);

        /// <summary>
        /// Creates a skew transform
        /// </summary>
        /// <param name='skewX'>The skew angle in the x dimension in degrees</param>
        /// <param name='skewY'>The skew angle in the y dimension in degrees</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D FromSkewRadians(double skewX, double skewY) => new Matrix2x2D(1d, Tan(skewY), Tan(skewX), 1.0f);

        /// <summary>
        /// Parse a string for a <see cref="Matrix2x2D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Matrix2x2D" /> data</param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix2x2D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [ParseMethod]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Matrix3x2D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Matrix3x2D" /> data</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix3x2D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            var firstToken = tokenizer.NextTokenRequired();

            // The token will already have had whitespace trimmed so we can do a simple string compare.
            var value = firstToken == nameof(Identity) ? Identity : new Matrix2x2D(
                Convert.ToDouble(firstToken, provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider)
                );

            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }

        /// <summary>
        /// The to matrix3x3d.
        /// </summary>
        /// <returns>
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix3x3D ToMatrix3x3D()
        {
            var result = Matrix3x3D.Identity;
            result.M0x0 = M0x0; result.M0x1 = M0x1;
            result.M1x0 = M1x0; result.M1x1 = M1x1;
            return result;
        }
        #endregion Factories

        #region Standard Methods
        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator{T}" />.
        /// </returns>
        public IEnumerator<IEnumerable<double>> GetEnumerator()
            => new List<List<double>>
            {
                new List<double> { M0x0, M0x1 },
                new List<double> { M1x0, M1x1 },
            }.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Returns the HashCode for this <see cref="Matrix2x2D" />
        /// </summary>
        /// <returns>
        /// The <see cref="int" /> HashCode for this <see cref="Matrix2x2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(M0x0, M0x1, M1x0, M1x1);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix2x2D" /> struct based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix2x2D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Matrix4x4D);
            if (IsIdentity) return nameof(Identity);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Matrix2x2D)}({nameof(M0x0)}:{M0x0.ToString(format, provider)}{s} {nameof(M0x1)}:{M0x1.ToString(format, provider)}{s} {nameof(M1x0)}:{M1x0.ToString(format, provider)}{s} {nameof(M1x1)}:{M1x1.ToString(format, provider)})";
        }
        #endregion
    }
}
