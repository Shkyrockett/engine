// <copyright file="Matrix4x4D.cs" company="Shkyrockett" >
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
using static Engine.Mathematics;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The matrix4x4d struct.
    /// </summary>
    /// <seealso cref="IMatrix{M, V}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(Matrix4x4DConverter))]
    [DebuggerDisplay("{ToString()}")]
    public struct Matrix4x4D
        : IMatrix<Matrix4x4D, Vector4D>
    {
        #region Static Fields
        /// <summary>
        /// An Empty <see cref="Matrix4x4D" />.
        /// </summary>
        public static readonly Matrix4x4D Empty = new Matrix4x4D(
            0d, 0d, 0d, 0d,
            0d, 0d, 0d, 0d,
            0d, 0d, 0d, 0d,
            0d, 0d, 0d, 0d);

        /// <summary>
        /// An Identity <see cref="Matrix4x4D" />.
        /// </summary>
        public static readonly Matrix4x4D Identity = new Matrix4x4D(
            1d, 0d, 0d, 0d,
            0d, 1d, 0d, 0d,
            0d, 0d, 1d, 0d,
            0d, 0d, 0d, 1d);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4x4D" /> class.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix4x4D(
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3)
            : this()
        {
            M0x0 = m0x0;
            M0x1 = m0x1;
            M0x2 = m0x2;
            M0x3 = m0x3;
            M1x0 = m1x0;
            M1x1 = m1x1;
            M1x2 = m1x2;
            M1x3 = m1x3;
            M2x0 = m2x0;
            M2x1 = m2x1;
            M2x2 = m2x2;
            M2x3 = m2x3;
            M3x0 = m3x0;
            M3x1 = m3x1;
            M3x2 = m3x2;
            M3x3 = m3x3;
        }

        /// <summary>
        /// Create a new Matrix from 2 Vector4D objects.
        /// </summary>
        /// <param name="xAxis">The x axis.</param>
        /// <param name="yAxis">The y axis.</param>
        /// <param name="zAxis">The z axis.</param>
        /// <param name="wAxis">The w axis.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix4x4D(Vector4D xAxis, Vector4D yAxis, Vector4D zAxis, Vector4D wAxis)
            : this(xAxis.I, xAxis.J, xAxis.K, xAxis.L,
                  yAxis.I, yAxis.J, yAxis.K, yAxis.L,
                  zAxis.I, zAxis.J, zAxis.K, zAxis.L,
                  wAxis.I, wAxis.J, wAxis.K, wAxis.L)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4x4D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix4x4D((
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3) tuple)
            : this()
        {
            (M0x0, M0x1, M0x2, M0x3,
            M1x0, M1x1, M1x2, M1x3,
            M2x0, M2x1, M2x2, M2x3,
            M3x0, M3x1, M3x2, M3x3) = tuple;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Matrix2x2D" /> to a <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}" />.
        /// </summary>
        /// <param name="m0x0">The m0x0.</param>
        /// <param name="m0x1">The m0x1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m1x0">The m1x0.</param>
        /// <param name="m1x1">The m1x1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(
            out double m0x0, out double m0x1, out double m0x2, out double m0x3,
            out double m1x0, out double m1x1, out double m1x2, out double m1x3,
            out double m2x0, out double m2x1, out double m2x2, out double m2x3,
            out double m3x0, out double m3x1, out double m3x2, out double m3x3)
        {
            m0x0 = M0x0;
            m0x1 = M0x1;
            m0x2 = M0x2;
            m0x3 = M0x3;
            m1x0 = M1x0;
            m1x1 = M1x1;
            m1x2 = M1x2;
            m1x3 = M1x3;
            m2x0 = M2x0;
            m2x1 = M2x1;
            m2x2 = M2x2;
            m2x3 = M2x3;
            m3x0 = M2x0;
            m3x1 = M2x1;
            m3x2 = M2x2;
            m3x3 = M2x3;
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
        /// Gets or sets the m0x2.
        /// </summary>
        /// <value>
        /// The M0X2.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M0x2 { get; set; }

        /// <summary>
        /// Gets or sets the m0x3.
        /// </summary>
        /// <value>
        /// The M0X3.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M0x3 { get; set; }

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
        /// Gets or sets the m1x2.
        /// </summary>
        /// <value>
        /// The M1X2.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M1x2 { get; set; }

        /// <summary>
        /// Gets or sets the m1x3.
        /// </summary>
        /// <value>
        /// The M1X3.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M1x3 { get; set; }

        /// <summary>
        /// Gets or sets the m2x0.
        /// </summary>
        /// <value>
        /// The M2X0.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M2x0 { get; set; }

        /// <summary>
        /// Gets or sets the m2x1.
        /// </summary>
        /// <value>
        /// The M2X1.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M2x1 { get; set; }

        /// <summary>
        /// Gets or sets the m2x2.
        /// </summary>
        /// <value>
        /// The M2X2.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M2x2 { get; set; }

        /// <summary>
        /// Gets or sets the m2x3.
        /// </summary>
        /// <value>
        /// The M2X3.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M2x3 { get; set; }

        /// <summary>
        /// Gets or sets the m3x0.
        /// </summary>
        /// <value>
        /// The M3X0.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M3x0 { get; set; }

        /// <summary>
        /// Gets or sets the m3x1.
        /// </summary>
        /// <value>
        /// The M3X1.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M3x1 { get; set; }

        /// <summary>
        /// Gets or sets the m3x2.
        /// </summary>
        /// <value>
        /// The M3X2.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M3x2 { get; set; }

        /// <summary>
        /// Gets or sets the m3x3.
        /// </summary>
        /// <value>
        /// The M3X3.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M3x3 { get; set; }

        /// <summary>
        /// Gets or sets the cx.
        /// </summary>
        /// <value>
        /// The cx.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The First column of the " + nameof(Matrix4x4D))]
        public Vector4D Cx { get { return new Vector4D(M0x0, M1x0, M2x0, M3x0); } set { (M0x0, M1x0, M2x0, M3x0) = value; } }

        /// <summary>
        /// Gets or sets the cy.
        /// </summary>
        /// <value>
        /// The cy.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Second column of the " + nameof(Matrix4x4D))]
        public Vector4D Cy { get { return new Vector4D(M0x1, M1x1, M2x1, M3x1); } set { (M0x1, M1x1, M2x1, M3x1) = value; } }

        /// <summary>
        /// Gets or sets the cz.
        /// </summary>
        /// <value>
        /// The cz.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Third column of the " + nameof(Matrix4x4D))]
        public Vector4D Cz { get { return new Vector4D(M0x2, M1x2, M2x2, M3x2); } set { (M0x2, M1x2, M2x2, M3x2) = value; } }

        /// <summary>
        /// Gets or sets the cw.
        /// </summary>
        /// <value>
        /// The cw.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Fourth column of the " + nameof(Matrix4x4D))]
        public Vector4D Cw { get { return new Vector4D(M0x3, M1x3, M2x3, M3x3); } set { (M0x3, M1x3, M2x3, M3x3) = value; } }

        /// <summary>
        /// Gets or sets the X Row or row one.
        /// </summary>
        /// <value>
        /// The rx.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The First row of the " + nameof(Matrix4x4D))]
        public Vector4D Rx { get { return new Vector4D(M0x0, M0x1, M0x2, M0x3); } set { (M0x0, M0x1, M0x2, M0x3) = value; } }

        /// <summary>
        /// Gets or sets the Y Row or row two.
        /// </summary>
        /// <value>
        /// The ry.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Second row of the " + nameof(Matrix4x4D))]
        public Vector4D Ry { get { return new Vector4D(M1x0, M1x1, M1x2, M1x3); } set { (M1x0, M1x1, M1x2, M1x3) = value; } }

        /// <summary>
        /// Gets or sets the Z Row or row tree.
        /// </summary>
        /// <value>
        /// The rz.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Third row of the " + nameof(Matrix4x4D))]
        public Vector4D Rz { get { return new Vector4D(M2x0, M2x1, M2x2, M2x3); } set { (M2x0, M2x1, M2x2, M2x3) = value; } }

        /// <summary>
        /// Gets or sets the W Row or row four.
        /// </summary>
        /// <value>
        /// The rw.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Fourth row of the " + nameof(Matrix4x4D))]
        public Vector4D Rw { get { return new Vector4D(M3x0, M3x1, M3x2, M3x3); } set { (M3x0, M3x1, M3x2, M3x3) = value; } }

        /// <summary>
        /// Gets the determinant.
        /// </summary>
        /// <value>
        /// The determinant.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Determinant => MatrixDeterminant(M0x0, M0x1, M0x2, M0x3, M1x0, M1x1, M1x2, M1x3, M2x0, M2x1, M2x2, M2x3, M3x0, M3x1, M3x2, M3x3);

        /// <summary>
        /// Gets the transposed matrix where the rows of the matrix are swapped with the columns.
        /// </summary>
        /// <value>
        /// The transposed.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix4x4D Transposed => TransposeMatrix(M0x0, M0x1, M0x2, M0x3, M1x0, M1x1, M1x2, M1x3, M2x0, M2x1, M2x2, M2x3, M3x0, M3x1, M3x2, M3x3);

        /// <summary>
        /// Gets the adjoint.
        /// </summary>
        /// <value>
        /// The adjoint.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix4x4D Adjoint => AdjointMatrix(M0x0, M0x1, M0x2, M0x3, M1x0, M1x1, M1x2, M1x3, M2x0, M2x1, M2x2, M2x3, M3x0, M3x1, M3x2, M3x3);

        /// <summary>
        /// Gets the cofactor.
        /// </summary>
        /// <value>
        /// The cofactor.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix4x4D Cofactor => CofactorMatrix(M0x0, M0x1, M0x2, M0x3, M1x0, M1x1, M1x2, M1x3, M2x0, M2x1, M2x2, M2x3, M3x0, M3x1, M3x2, M3x3);

        /// <summary>
        /// Gets the inverted.
        /// </summary>
        /// <value>
        /// The inverted.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix4x4D Inverted => InvertMatrix(M0x0, M0x1, M0x2, M0x3, M1x0, M1x1, M1x2, M1x3, M2x0, M2x1, M2x2, M2x3, M3x0, M3x1, M3x2, M3x3);

        /// <summary>
        /// Gets a value indicating whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if this instance is identity; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsIdentity
            => Abs(M0x0 - 1) < Epsilon
                && Abs(M0x1) < Epsilon
                && Abs(M0x2) < Epsilon
                && Abs(M0x3) < Epsilon
                && Abs(M1x0) < Epsilon
                && Abs(M1x1 - 1) < Epsilon
                && Abs(M1x2) < Epsilon
                && Abs(M1x3) < Epsilon
                && Abs(M2x0) < Epsilon
                && Abs(M2x1) < Epsilon
                && Abs(M2x2 - 1) < Epsilon
                && Abs(M2x3) < Epsilon
                && Abs(M3x0) < Epsilon
                && Abs(M3x1) < Epsilon
                && Abs(M3x2) < Epsilon
                && Abs(M3x3 - 1) < Epsilon;
        #endregion Properties

        #region Operators
        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D operator +(Matrix4x4D value) => Plus(value);

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
        public static Matrix4x4D operator +(Matrix4x4D augend, Matrix4x4D addend) => Add(augend, addend);

        /// <summary>
        /// Negates all the items in the Matrix.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D operator -(Matrix4x4D value) => Negate(value);

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
        public static Matrix4x4D operator -(Matrix4x4D minuend, Matrix4x4D subend) => Subtract(minuend, subend);

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
        public static Matrix4x4D operator *(Matrix4x4D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

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
        public static Matrix4x4D operator *(double multiplicand, Matrix4x4D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Matrix4x4D operator *(Matrix4x4D multiplicand, Matrix4x4D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Matrix4x4D operator *(Matrix4x4D multiplicand, Matrix3x3D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Matrix4x4D operator *(Matrix3x3D multiplicand, Matrix4x4D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Matrix4x4D operator *(Matrix4x4D multiplicand, Matrix2x2D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Matrix4x4D operator *(Matrix2x2D multiplicand, Matrix4x4D multiplier) => Multiply(multiplicand, multiplier);

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
        public static bool operator ==(Matrix4x4D matrix1, Matrix4x4D matrix2) => Equals(matrix1, matrix2);

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
        public static bool operator !=(Matrix4x4D matrix1, Matrix4x4D matrix2) => !Equals(matrix1, matrix2);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Matrix3x3D" /> to <see cref="Matrix4x4D" />.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Matrix4x4D(Matrix3x3D source)
            => new Matrix4x4D(
                source.M0x0, source.M0x1, source.M0x2, 0d,
                source.M1x0, source.M1x1, source.M1x2, 0d,
                source.M2x0, source.M2x1, source.M2x2, 0d,
                0d, 0d, 0d, 1);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Matrix2x2D" /> to <see cref="Matrix4x4D" />.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Matrix4x4D(Matrix2x2D source)
            => new Matrix4x4D(
                source.M0x0, source.M0x1, 0d, 0d,
                source.M1x0, source.M1x1, 0d, 0d,
                0d, 0d, 1, 0d,
                0d, 0d, 0d, 1);

        /// <summary>
        /// Tuple to <see cref="Matrix4x4D" />.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double)(Matrix4x4D matrix) => (matrix.M0x0, matrix.M0x1, matrix.M0x2, matrix.M0x3, matrix.M1x0, matrix.M1x1, matrix.M1x2, matrix.M1x3, matrix.M2x0, matrix.M2x1, matrix.M2x2, matrix.M2x3, matrix.M3x0, matrix.M3x1, matrix.M3x2, matrix.M3x3);

        /// <summary>
        /// Tuple to <see cref="Matrix4x4D" />.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Matrix4x4D((double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) tuple) => new Matrix4x4D(tuple);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Plus(Matrix4x4D value) => Operations.Plus(value.M0x0, value.M0x1, value.M0x2, value.M0x3, value.M1x0, value.M1x1, value.M1x2, value.M1x3, value.M2x0, value.M2x1, value.M2x2, value.M2x3, value.M3x0, value.M3x1, value.M3x2, value.M3x3);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Add(Matrix4x4D augend, Matrix4x4D addend) => AddMatrix(augend.M0x0, augend.M0x1, augend.M0x2, augend.M0x3, augend.M1x0, augend.M1x1, augend.M1x2, augend.M1x3, augend.M2x0, augend.M2x1, augend.M2x2, augend.M2x3, augend.M3x0, augend.M3x1, augend.M3x2, augend.M3x3, addend.M0x0, addend.M0x1, addend.M0x2, addend.M0x3, addend.M1x0, addend.M1x1, addend.M1x2, addend.M1x3, addend.M2x0, addend.M2x1, addend.M2x2, addend.M2x3, addend.M3x0, addend.M3x1, addend.M3x2, addend.M3x3);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Negate(Matrix4x4D value) => Operations.Negate(value.M0x0, value.M0x1, value.M0x2, value.M0x3, value.M1x0, value.M1x1, value.M1x2, value.M1x3, value.M2x0, value.M2x1, value.M2x2, value.M2x3, value.M3x0, value.M3x1, value.M3x2, value.M3x3);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Subtract(Matrix4x4D minuend, Matrix4x4D subend) => Subtract4x4x4x4(minuend.M0x0, minuend.M0x1, minuend.M0x2, minuend.M0x3, minuend.M1x0, minuend.M1x1, minuend.M1x2, minuend.M1x3, minuend.M2x0, minuend.M2x1, minuend.M2x2, minuend.M2x3, minuend.M3x0, minuend.M3x1, minuend.M3x2, minuend.M3x3, subend.M0x0, subend.M0x1, subend.M0x2, subend.M0x3, subend.M1x0, subend.M1x1, subend.M1x2, subend.M1x3, subend.M2x0, subend.M2x1, subend.M2x2, subend.M2x3, subend.M3x0, subend.M3x1, subend.M3x2, subend.M3x3);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Multiply(Matrix4x4D multiplicand, double multiplier) => Scale4x4(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M0x3, multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M1x3, multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplicand.M2x3, multiplicand.M3x0, multiplicand.M3x1, multiplicand.M3x2, multiplicand.M3x3, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Multiply(double multiplicand, Matrix4x4D multiplier) => Scale4x4(multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M0x3, multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M1x3, multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplier.M2x3, multiplier.M3x0, multiplier.M3x1, multiplier.M3x2, multiplier.M3x3, multiplicand);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Multiply(Matrix4x4D multiplicand, Matrix4x4D multiplier) => Multiply4x4x4x4(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M0x3, multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M1x3, multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplicand.M2x3, multiplicand.M3x0, multiplicand.M3x1, multiplicand.M3x2, multiplicand.M3x3, multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M0x3, multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M1x3, multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplier.M2x3, multiplier.M3x0, multiplier.M3x1, multiplier.M3x2, multiplier.M3x3);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Multiply(Matrix4x4D multiplicand, Matrix3x3D multiplier) => Multiply4x4x3x3(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M0x3, multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M1x3, multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplicand.M2x3, multiplicand.M3x0, multiplicand.M3x1, multiplicand.M3x2, multiplicand.M3x3, multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M2x0, multiplier.M2x1, multiplier.M2x2);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Multiply(Matrix3x3D multiplicand, Matrix4x4D multiplier) => Multiply3x3x4x4(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M0x3, multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M1x3, multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplier.M2x3, multiplier.M3x0, multiplier.M3x1, multiplier.M3x2, multiplier.M3x3);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Multiply(Matrix4x4D multiplicand, Matrix2x2D multiplier) => Multiply4x4x2x2(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M0x3, multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M1x3, multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplicand.M2x3, multiplicand.M3x0, multiplicand.M3x1, multiplicand.M3x2, multiplicand.M3x3, multiplier.M0x0, multiplier.M0x1, multiplier.M1x0, multiplier.M1x1);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Multiply(Matrix2x2D multiplicand, Matrix4x4D multiplier) => Multiply2x2x4x4(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M1x0, multiplicand.M1x1, multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M0x3, multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M1x3, multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplier.M2x3, multiplier.M3x0, multiplier.M3x1, multiplier.M3x2, multiplier.M3x3);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Matrix4x4D d && Equals(d);

        /// <summary>
        /// Compares two <see cref="Matrix4x4D"/> instances for object equality.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the two Matrix instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='matrix1'>The first Matrix to compare</param>
        /// <param name='matrix2'>The second Matrix to compare</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Matrix4x4D matrix2) => M0x0 == matrix2.M0x0 && M0x1 == matrix2.M0x1 && M0x2 == matrix2.M0x2 && M0x3 == matrix2.M0x3 && M1x0 == matrix2.M1x0 && M1x1 == matrix2.M1x1 && M1x2 == matrix2.M1x2 && M1x3 == matrix2.M1x3 && M2x0 == matrix2.M2x0 && M2x1 == matrix2.M2x1 && M2x2 == matrix2.M2x2 && M2x3 == matrix2.M2x3 && M3x0 == matrix2.M3x0 && M3x1 == matrix2.M3x1 && M3x2 == matrix2.M3x2 && M3x3 == matrix2.M3x3;

        /// <summary>
        /// Converts to valuetuple.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) ToValueTuple() => (M0x0, M0x1, M0x2, M0x3, M1x0, M1x1, M1x2, M1x3, M2x0, M2x1, M2x2, M2x3, M3x0, M3x1, M3x2, M3x3);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D FromValueTuple((double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) tuple) => new Matrix4x4D(tuple);

        /// <summary>
        /// Converts to matrix4x4d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix4x4D ToMatrix4x4D() => new Matrix4x4D(M0x0, M0x1, M0x2, M0x3, M1x0, M1x1, M1x2, M1x3, M2x0, M2x1, M2x2, M2x3, M3x0, M3x1, M3x2, M3x3);
        #endregion

        #region Factories
        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D FromScale(Vector2D scale)
            => new Matrix4x4D(
                scale.I, 0d, 0d, 0d,
                0d, scale.J, 0d, 0d,
                0d, 0d, 1d, 0d,
                0d, 0d, 0d, 1d);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D FromScale(Vector3D scale)
            => new Matrix4x4D(
                scale.I, 0d, 0d, 0d,
                0d, scale.J, 0d, 0d,
                0d, 0d, scale.K, 0d,
                0d, 0d, 0d, 1d);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D FromScale(Vector4D scale)
            => new Matrix4x4D(
                scale.I, 0d, 0d, 0d,
                0d, scale.J, 0d, 0d,
                0d, 0d, scale.K, 0d,
                0d, 0d, 0d, scale.L);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scaleX">The scale factor in the x dimension</param>
        /// <param name="scaleY">The scale factor in the y dimension</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D FromScale(double scaleX, double scaleY)
            => new Matrix4x4D(
                scaleX, 0d, 0d, 0d,
                0d, scaleY, 0d, 0d,
                0d, 0d, 1d, 0d,
                0d, 0d, 0d, 1d);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scaleX">The scale factor in the x dimension</param>
        /// <param name="scaleY">The scale factor in the y dimension</param>
        /// <param name="scaleZ">The scale factor in the z dimension</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D FromScale(double scaleX, double scaleY, double scaleZ)
            => new Matrix4x4D(
                scaleX, 0d, 0d, 0d,
                0d, scaleY, 0d, 0d,
                0d, 0d, scaleZ, 0d,
                0d, 0d, 0d, 1d);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scaleX">The scale factor in the x dimension</param>
        /// <param name="scaleY">The scale factor in the y dimension</param>
        /// <param name="scaleZ">The scale factor in the z dimension</param>
        /// <param name="scaleW">The scale factor in the w dimension</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D FromScale(double scaleX, double scaleY, double scaleZ, double scaleW)
            => new Matrix4x4D(
                scaleX, 0d, 0d, 0d,
                0d, scaleY, 0d, 0d,
                0d, 0d, scaleZ, 0d,
                0d, 0d, 0d, scaleW);

        /// <summary>
        /// Parse a string for a <see cref="Matrix4x4D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Matrix4x4D" /> data</param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix4x4D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [ParseMethod]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Matrix4x4D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Matrix4x4D" /> data</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix4x4D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            var firstToken = tokenizer.NextTokenRequired();

            // The token will already have had whitespace trimmed so we can do a simple string compare.
            var value = firstToken == nameof(Identity) ? Identity : new Matrix4x4D(
                Convert.ToDouble(firstToken, provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
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
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{IEnumerable{double}}"/>.</returns>
        public IEnumerator<IEnumerable<double>> GetEnumerator()
            => new List<List<double>>
            {
                new List<double> { M0x0, M0x1, M0x2, M0x3 },
                new List<double> { M1x0, M1x1, M1x2, M1x3 },
                new List<double> { M2x0, M2x1, M2x2, M2x3 },
                new List<double> { M3x0, M3x1, M3x2, M3x3 },
            }.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Returns the HashCode for this Matrix
        /// </summary>
        /// <returns>
        /// int - the HashCode for this Matrix
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(M0x0);
            hash.Add(M0x1);
            hash.Add(M0x2);
            hash.Add(M0x3);
            hash.Add(M1x0);
            hash.Add(M1x1);
            hash.Add(M1x2);
            hash.Add(M1x3);
            hash.Add(M2x0);
            hash.Add(M2x1);
            hash.Add(M2x2);
            hash.Add(M2x3);
            hash.Add(M3x0);
            hash.Add(M3x1);
            hash.Add(M3x2);
            hash.Add(M3x3);
            return hash.ToHashCode();
        }

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D" /> struct based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Matrix3x2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Matrix3x2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider) => ToString("R" /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Matrix3x2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Matrix4x4D);
            if (IsIdentity) return nameof(Identity);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Matrix4x4D)}({nameof(M0x0)}:{M0x0.ToString(format, provider)}{s} {nameof(M0x1)}:{M0x1.ToString(format, provider)}{s} {nameof(M0x2)}:{M0x2.ToString(format, provider)}{s} {nameof(M0x3)}:{M0x3.ToString(format, provider)}{s} {nameof(M1x0)}:{M1x0.ToString(format, provider)}{s} {nameof(M1x1)}:{M1x1.ToString(format, provider)}{s} {nameof(M1x2)}:{M1x2.ToString(format, provider)}{s} {nameof(M1x3)}:{M1x3.ToString(format, provider)}{s} {nameof(M2x0)}:{M2x0.ToString(format, provider)}{s} {nameof(M2x1)}:{M2x1.ToString(format, provider)}{s} {nameof(M2x2)}:{M2x2.ToString(format, provider)}{s} {nameof(M2x3)}:{M2x3.ToString(format, provider)}{s} {nameof(M3x0)}:{M3x0.ToString(format, provider)}{s} {nameof(M3x1)}:{M3x1.ToString(format, provider)}{s} {nameof(M3x2)}:{M3x2.ToString(format, provider)}{s} {nameof(M3x3)}:{M3x3.ToString(format, provider)})";
        }
        #endregion
    }
}
