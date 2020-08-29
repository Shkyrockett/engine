// <copyright file="Matrix3x3D.cs" company="Shkyrockett" >
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
using static Engine.Maths;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The matrix3x3d struct.
    /// </summary>
    /// <seealso cref="IMatrix{T, T}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(Matrix3x3DConverter))]
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public struct Matrix3x3D
        : IMatrix<Matrix3x3D, Vector3D>
    {
        #region Static Fields
        /// <summary>
        /// An Empty <see cref="Matrix3x3D" />.
        /// </summary>
        public static readonly Matrix3x3D Empty = new Matrix3x3D(
            0d, 0d, 0d,
            0d, 0d, 0d,
            0d, 0d, 0d);

        /// <summary>
        /// An Identity <see cref="Matrix3x3D" />.
        /// </summary>
        public static readonly Matrix3x3D Identity = new Matrix3x3D(
            1d, 0d, 0d,
            0d, 1d, 0d,
            0d, 0d, 1d);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix3x3D" /> class.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix3x3D(
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2)
            : this()
        {
            M0x0 = m0x0;
            M0x1 = m0x1;
            M0x2 = m0x2;
            M1x0 = m1x0;
            M1x1 = m1x1;
            M1x2 = m1x2;
            M2x0 = m2x0;
            M2x1 = m2x1;
            M2x2 = m2x2;
        }

        /// <summary>
        /// Create a new Matrix from 2 Vertex2 objects.
        /// </summary>
        /// <param name="xAxis">The x axis.</param>
        /// <param name="yAxis">The y axis.</param>
        /// <param name="zAxis">The z axis.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix3x3D(Vector3D xAxis, Vector3D yAxis, Vector3D zAxis)
            : this(xAxis.I, xAxis.J, xAxis.K, yAxis.I, yAxis.J, yAxis.K, zAxis.I, zAxis.J, zAxis.K)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix3x3D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix3x3D((double, double, double, double, double, double, double, double, double) tuple)
            : this()
        {
            (M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2) = tuple;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Matrix2x2D" /> to a <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}" />.
        /// </summary>
        /// <param name="m0x0">The m0x0.</param>
        /// <param name="m0x1">The m0x1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m1x0">The m1x0.</param>
        /// <param name="m1x1">The m1x1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(
            out double m0x0, out double m0x1, out double m0x2,
            out double m1x0, out double m1x1, out double m1x2,
            out double m2x0, out double m2x1, out double m2x2)
        {
            m0x0 = M0x0;
            m0x1 = M0x1;
            m0x2 = M0x2;
            m1x0 = M1x0;
            m1x1 = M1x1;
            m1x2 = M1x2;
            m2x0 = M2x0;
            m2x1 = M2x1;
            m2x2 = M2x2;
        }
        #endregion Deconstructors

        #region Indexers
        /// <summary>
        /// Gets or sets the <see cref="double"/> with the specified index1.
        /// </summary>
        /// <value>
        /// The <see cref="double"/>.
        /// </value>
        /// <param name="index1">The index1.</param>
        /// <param name="index2">The index2.</param>
        /// <returns></returns>
        public double this[int index1, int index2]
        {
            get
            {
                return index1 switch
                {
                    0 => index2 switch
                    {
                        0 => M0x0,
                        1 => M0x1,
                        2 => M0x2,
                        _ => double.NaN,
                    },
                    1 => index2 switch
                    {
                        0 => M1x0,
                        1 => M1x1,
                        2 => M1x2,
                        _ => double.NaN,
                    },
                    2 => index2 switch
                    {
                        0 => M2x0,
                        1 => M2x1,
                        2 => M2x2,
                        _ => double.NaN,
                    },
                    _ => double.NaN,
                };
            }
            set
            {
                switch (index1)
                {
                    case 0:
                        switch (index2)
                        {
                            case 0: M0x0 = value; break;
                            case 1: M0x1 = value; break;
                            case 2: M0x2 = value; break;
                            default: break;
                        }
                        break;
                    case 1:
                        switch (index2)
                        {
                            case 0: M1x0 = value; break;
                            case 1: M1x1 = value; break;
                            case 2: M1x2 = value; break;
                            default: break;
                        }
                        break;
                    case 2:
                        switch (index2)
                        {
                            case 0: M2x0 = value; break;
                            case 1: M2x1 = value; break;
                            case 2: M2x2 = value; break;
                            default: break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the m0x0.
        /// </summary>
        /// <value>
        /// The M0X0.
        /// </value>
        [DataMember(Name = nameof(M0x0)), XmlAttribute(nameof(M0x0)), SoapAttribute(nameof(M0x0))]
        public double M0x0 { get; set; }

        /// <summary>
        /// Gets or sets the m0x1.
        /// </summary>
        /// <value>
        /// The M0X1.
        /// </value>
        [DataMember(Name = nameof(M0x1)), XmlAttribute(nameof(M0x1)), SoapAttribute(nameof(M0x1))]
        public double M0x1 { get; set; }

        /// <summary>
        /// Gets or sets the m0x2.
        /// </summary>
        /// <value>
        /// The M0X2.
        /// </value>
        [DataMember(Name = nameof(M0x2)), XmlAttribute(nameof(M0x2)), SoapAttribute(nameof(M0x2))]
        public double M0x2 { get; set; }

        /// <summary>
        /// Gets or sets the m1x0.
        /// </summary>
        /// <value>
        /// The M1X0.
        /// </value>
        [DataMember(Name = nameof(M1x0)), XmlAttribute(nameof(M1x0)), SoapAttribute(nameof(M1x0))]
        public double M1x0 { get; set; }

        /// <summary>
        /// Gets or sets the m1x1.
        /// </summary>
        /// <value>
        /// The M1X1.
        /// </value>
        [DataMember(Name = nameof(M1x1)), XmlAttribute(nameof(M1x1)), SoapAttribute(nameof(M1x1))]
        public double M1x1 { get; set; }

        /// <summary>
        /// Gets or sets the m1x2.
        /// </summary>
        /// <value>
        /// The M1X2.
        /// </value>
        [DataMember(Name = nameof(M1x2)), XmlAttribute(nameof(M1x2)), SoapAttribute(nameof(M1x2))]
        public double M1x2 { get; set; }

        /// <summary>
        /// Gets or sets the m2x0.
        /// </summary>
        /// <value>
        /// The M2X0.
        /// </value>
        [DataMember(Name = nameof(M2x0)), XmlAttribute(nameof(M2x0)), SoapAttribute(nameof(M2x0))]
        public double M2x0 { get; set; }

        /// <summary>
        /// Gets or sets the m2x1.
        /// </summary>
        /// <value>
        /// The M2X1.
        /// </value>
        [DataMember(Name = nameof(M2x1)), XmlAttribute(nameof(M2x1)), SoapAttribute(nameof(M2x1))]
        public double M2x1 { get; set; }

        /// <summary>
        /// Gets or sets the m2x2.
        /// </summary>
        /// <value>
        /// The M2X2.
        /// </value>
        [DataMember(Name = nameof(M2x2)), XmlAttribute(nameof(M2x2)), SoapAttribute(nameof(M2x2))]
        public double M2x2 { get; set; }

        /// <summary>
        /// Gets or sets the cx.
        /// </summary>
        /// <value>
        /// The cx.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The First column of the " + nameof(Matrix3x3D))]
        public Vector3D Cx { get { return new Vector3D(M0x0, M1x0, M2x0); } set { (M0x0, M1x0, M2x0) = value; } }

        /// <summary>
        /// Gets or sets the cy.
        /// </summary>
        /// <value>
        /// The cy.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Second column of the " + nameof(Matrix3x3D))]
        public Vector3D Cy { get { return new Vector3D(M0x1, M1x1, M2x1); } set { (M0x1, M1x1, M2x1) = value; } }

        /// <summary>
        /// Gets or sets the cz.
        /// </summary>
        /// <value>
        /// The cz.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Third column of the " + nameof(Matrix3x3D))]
        public Vector3D Cz { get { return new Vector3D(M0x2, M1x2, M2x2); } set { (M0x2, M1x2, M2x2) = value; } }

        /// <summary>
        /// Gets or sets the rx.
        /// </summary>
        /// <value>
        /// The rx.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The First row of the " + nameof(Matrix3x3D))]
        public Vector3D Rx { get { return new Vector3D(M0x0, M0x1, M0x2); } set { (M0x0, M0x1, M0x2) = value; } }

        /// <summary>
        /// Gets or sets the ry.
        /// </summary>
        /// <value>
        /// The ry.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Second row of the " + nameof(Matrix3x3D))]
        public Vector3D Ry { get { return new Vector3D(M1x0, M1x1, M1x2); } set { (M1x0, M1x1, M1x2) = value; } }

        /// <summary>
        /// Gets or sets the rz.
        /// </summary>
        /// <value>
        /// The rz.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Third row of the " + nameof(Matrix3x3D))]
        public Vector3D Rz { get { return new Vector3D(M2x0, M2x1, M2x2); } set { (M2x0, M2x1, M2x2) = value; } }

        /// <summary>
        /// Gets the determinant.
        /// </summary>
        /// <value>
        /// The determinant.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Determinant => MatrixDeterminant(M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2);

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <value>
        /// The transposed.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix3x3D Transposed => TransposeMatrix(M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2);

        /// <summary>
        /// Gets the adjoint.
        /// </summary>
        /// <value>
        /// The adjoint.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix3x3D Adjoint => AdjointMatrix(M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2);

        /// <summary>
        /// Gets the cofactor.
        /// </summary>
        /// <value>
        /// The cofactor.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix3x3D Cofactor => CofactorMatrix(M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2);

        /// <summary>
        /// Gets the inverted.
        /// </summary>
        /// <value>
        /// The inverted.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix3x3D Inverted => InverseMatrix(M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2);

        /// <summary>
        /// Gets a value indicating whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if this instance is identity; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsIdentity => IsMatrixIdentity(M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2);

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Rows => 3;

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Columns => 3;

        /// <summary>
        /// Gets the number of cells in the Matrix.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Count => Rows * Columns;
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
        public static Matrix3x3D operator +(Matrix3x3D value) => Plus(value);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator +(Matrix3x3D augend, Matrix3x3D addend) => Add(augend, addend);

        /// <summary>
        /// Negates all the items in the Matrix.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator -(Matrix3x3D value) => Negate(value);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator -(Matrix3x3D minuend, Matrix3x3D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator *(Matrix3x3D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator *(double multiplicand, Matrix3x3D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Vector3D operator *(Matrix3x3D multiplicand, Vector3D multiplier) => Multiply(multiplicand, multiplier);

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
        public static Vector3D operator *(Vector3D multiplicand, Matrix3x3D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator *(Matrix3x3D multiplicand, Matrix3x3D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator *(Matrix3x3D multiplicand, Matrix2x2D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator *(Matrix2x2D multiplicand, Matrix3x3D multiplier) => Multiply(multiplicand, multiplier);

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
        public static bool operator ==(Matrix3x3D matrix1, Matrix3x3D matrix2) => Equals(matrix1, matrix2);

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
        public static bool operator !=(Matrix3x3D matrix1, Matrix3x3D matrix2) => !Equals(matrix1, matrix2);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Matrix2x2D" /> to <see cref="Matrix3x3D" />.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Matrix3x3D(Matrix2x2D source) => new Matrix3x3D(source.M0x0, source.M0x1, 0d, source.M1x0, source.M1x1, 0d, 0d, 0d, 1);

        /// <summary>
        /// Tuple to <see cref="Matrix3x3D" />.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double, double, double, double, double, double, double, double, double)(Matrix3x3D matrix) => (matrix.M0x0, matrix.M0x1, matrix.M0x2, matrix.M1x0, matrix.M1x1, matrix.M1x2, matrix.M2x0, matrix.M2x1, matrix.M2x2);

        /// <summary>
        /// Tuple to <see cref="Matrix3x3D" />.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Matrix3x3D((double, double, double, double, double, double, double, double, double) tuple) => new Matrix3x3D(tuple);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Plus(Matrix3x3D value) => Operations.Plus(value.M0x0, value.M0x1, value.M0x2, value.M1x0, value.M1x1, value.M1x2, value.M2x0, value.M2x1, value.M2x2);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Add(Matrix3x3D augend, Matrix3x3D addend) => AddMatrix(augend.M0x0, augend.M0x1, augend.M0x2, augend.M1x0, augend.M1x1, augend.M1x2, augend.M2x0, augend.M2x1, augend.M2x2, addend.M0x0, addend.M0x1, addend.M0x2, addend.M1x0, addend.M1x1, addend.M1x2, addend.M2x0, addend.M2x1, addend.M2x2);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Negate(Matrix3x3D value) => Operations.Negate(value.M0x0, value.M0x1, value.M0x2, value.M1x0, value.M1x1, value.M1x2, value.M2x0, value.M2x1, value.M2x2);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Subtract(Matrix3x3D minuend, Matrix3x3D subend) => Subtract3x3x3x3(minuend.M0x0, minuend.M0x1, minuend.M0x2, minuend.M1x0, minuend.M1x1, minuend.M1x2, minuend.M2x0, minuend.M2x1, minuend.M2x2, subend.M0x0, subend.M0x1, subend.M0x2, subend.M1x0, subend.M1x1, subend.M1x2, subend.M2x0, subend.M2x1, subend.M2x2);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Multiply(Matrix3x3D multiplicand, double multiplier) => Scale3x3(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Multiply(double multiplicand, Matrix3x3D multiplier) => Scale3x3(multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplicand);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Multiply(Matrix3x3D multiplicand, Vector3D multiplier) => MultiplyMatrix3x3ByVerticalVector3D(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplier.I, multiplier.J, multiplier.K);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Multiply(Vector3D multiplicand, Matrix3x3D multiplier) => MultiplyHorizontalVector3DByMatrix3x3(multiplicand.I, multiplicand.J, multiplicand.K, multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M2x0, multiplier.M2x1, multiplier.M2x2);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Multiply(Matrix3x3D multiplicand, Matrix3x3D multiplier) => Multiply3x3x3x3(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M2x0, multiplier.M2x1, multiplier.M2x2);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Multiply(Matrix3x3D multiplicand, Matrix2x2D multiplier) => Multiply3x3x2x2(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplier.M0x0, multiplier.M0x1, multiplier.M1x0, multiplier.M1x1);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Multiply(Matrix2x2D multiplicand, Matrix3x3D multiplier) => Multiply2x2x3x3(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M1x0, multiplicand.M1x1, multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M2x0, multiplier.M2x1, multiplier.M2x2);

        /// <summary>
        /// Equals - compares this Matrix with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Matrix3x3D d && Equals(d);

        /// <summary>
        /// Compares two Matrix instances for object equality.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <param name="other">The second Matrix to compare</param>
        /// <returns>
        /// bool - true if the two Matrix instances are exactly equal, false otherwise
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Matrix3x3D other)
            => M0x0 == other.M0x0
            && M0x1 == other.M0x1
            && M0x2 == other.M0x2
            && M1x0 == other.M1x0
            && M1x1 == other.M1x1
            && M1x2 == other.M1x2
            && M2x0 == other.M2x0
            && M2x1 == other.M2x1
            && M2x2 == other.M2x2;

        /// <summary>
        /// Converts to valuetuple.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double, double, double, double, double, double, double, double, double) ToValueTuple() => (M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromValueTuple((double, double, double, double, double, double, double, double, double) tuple) => new Matrix3x3D(tuple);

        /// <summary>
        /// Converts to matrix3x3d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix3x3D ToMatrix3x3D() => new Matrix3x3D(M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2);
        #endregion

        #region Factories
        /// <summary>
        /// The from rotation x.
        /// </summary>
        /// <param name="radianAngle">The radianAngle.</param>
        /// <returns>
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromRotationX(double radianAngle)
        {
            var sin = Sin(radianAngle);
            var cos = Cos(radianAngle);
            return new Matrix3x3D(
                1d, 0d, 0d,
                0d, cos, -sin,
                0d, sin, cos);
        }

        /// <summary>
        /// The from rotation y.
        /// </summary>
        /// <param name="radianAngle">The radianAngle.</param>
        /// <returns>
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromRotationY(double radianAngle)
        {
            var sin = Sin(radianAngle);
            var cos = Cos(radianAngle);
            return new Matrix3x3D(
                cos, 0d, -sin,
                0d, 1d, 0d,
                sin, 0d, cos);
        }

        /// <summary>
        /// The from rotation z.
        /// </summary>
        /// <param name="radianAngle">The radianAngle.</param>
        /// <returns>
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromRotationZ(double radianAngle)
        {
            var sin = Sin(radianAngle);
            var cos = Cos(radianAngle);
            return new Matrix3x3D(
                cos, -sin, 0d,
                sin, cos, 0d,
                0d, 0d, 1d);
        }

        /// <summary>
        /// The from rotation axis using atan.
        /// </summary>
        /// <param name="radianAngle">The radianAngle.</param>
        /// <param name="axis">The axis.</param>
        /// <returns>
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromRotationAxisUsingAtan(double radianAngle, Vector3D axis)
        {
            double zAngle;
            double yAngle;
            if (axis.I == 0)
            {
                if (axis.J == 0)
                {
                    return FromRotationZ(radianAngle);
                }
                else
                {
                    zAngle = HalfPi;
                    yAngle = Atan(axis.K / axis.J);
                }
            }
            else
            {
                zAngle = Atan(axis.J / axis.I);
                yAngle = Atan(axis.K / Sqrt((axis.I * axis.I) + (axis.J * axis.J)));
            }
            return FromRotationZ(-zAngle) *
            FromRotationY(-yAngle) *
            FromRotationX(radianAngle) *
            FromRotationY(yAngle) *
            FromRotationZ(zAngle);
        }

        /// <summary>
        /// The from rotation axis.
        /// </summary>
        /// <param name="radianAngle">The radianAngle.</param>
        /// <param name="axis">The axis.</param>
        /// <returns>
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromRotationAxis(double radianAngle, Vector3D axis)
        {
            var first = FromLookAt(Vector3D.Empty, axis, new Vector3D(axis.K, axis.I, axis.J));
            return first.Inverted * FromRotationZ(radianAngle) * first;
        }

        /// <summary>
        /// The from look at.
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <param name="positiveZAxis">The positiveZAxis.</param>
        /// <param name="onPositiveY">The onPositiveY.</param>
        /// <returns>
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Matrix3x3D FromLookAt(Vector3D origin, Vector3D positiveZAxis, Vector3D onPositiveY)
        {
            var rv = Identity;
            var axis = positiveZAxis - origin;
            rv.Rz = Normalize(axis.I, axis.J, axis.K);
            var translated = onPositiveY - origin;
            rv.Rx = Normalize(CrossProduct(translated.I, translated.J, translated.K, rv.Rz.I, rv.Rz.J, rv.Rz.K));
            rv.Ry = Normalize(CrossProduct(rv.Rz.I, rv.Rz.J, rv.Rz.K, rv.Rx.I, rv.Rx.J, rv.Rx.K));
            return rv;
        }

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromScale(Vector2D scale)
            => new Matrix3x3D(
                scale.I, 0d, 0d,
                0d, scale.J, 0d,
                0d, 0d, 1d);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromScale(Vector3D scale)
            => new Matrix3x3D(
                scale.I, 0d, 0d,
                0d, scale.J, 0d,
                0d, 0d, scale.K);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scaleX">The scale factor in the x dimension</param>
        /// <param name="scaleY">The scale factor in the y dimension</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromScale(double scaleX, double scaleY)
            => new Matrix3x3D(
                scaleX, 0d, 0d,
                0d, scaleY, 0d,
                0d, 0d, 1d);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scaleX">The scale factor in the x dimension</param>
        /// <param name="scaleY">The scale factor in the y dimension</param>
        /// <param name="scaleZ">The scale factor in the z dimension</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromScale(double scaleX, double scaleY, double scaleZ)
            => new Matrix3x3D(
                scaleX, 0d, 0d,
                0d, scaleY, 0d,
                0d, 0d, scaleZ);

        /// <summary>
        /// The from translate2d.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromTranslate2D(Vector2D value)
            => new Matrix3x3D(
                1, 0d, value.I,
                0d, 1, value.J,
                0d, 0d, 1d);

        /// <summary>
        /// The from shear3d.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromShear3D(Vector2D value)
            => new Matrix3x3D(
                1, 0d, value.I,
                0d, 1, value.J,
                0d, 0d, 1);

        /// <summary>
        /// Constructs this Matrix from 3 Euler angles, in degrees.
        /// </summary>
        /// <param name="yaw">The yaw.</param>
        /// <param name="pitch">The pitch.</param>
        /// <param name="roll">The roll.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromEulerAnglesXYZ(double yaw, double pitch, double roll) => FromRotationX(yaw) * (FromRotationY(pitch) * FromRotationZ(roll));

        /// <summary>
        /// Parse a string for a <see cref="Matrix3x3D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Matrix3x3D" /> data</param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix3x3D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [ParseMethod]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Matrix3x3D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Matrix3x3D" /> data</param>
        /// <param name="formatProvider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix3x3D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            var firstToken = tokenizer.NextTokenRequired();

            // The token will already have had whitespace trimmed so we can do a simple string compare.
            var value = firstToken == nameof(Identity) ? Identity : new Matrix3x3D(
                Convert.ToDouble(firstToken, provider),
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
        /// <returns>
        /// The <see cref="IEnumerator{T}" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<IEnumerable<double>> GetEnumerator()
            => new List<List<double>>
            {
                new List<double> { M0x0, M0x1, M0x2 },
                new List<double> { M1x0, M1x1, M1x2 },
                new List<double> { M2x0, M2x1, M2x2 },
            }.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            hash.Add(M1x0);
            hash.Add(M1x1);
            hash.Add(M1x2);
            hash.Add(M2x0);
            hash.Add(M2x1);
            hash.Add(M2x2);
            return hash.ToHashCode();
        }

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x3D" /> struct based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x3D" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="formatProvider">The provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider formatProvider) => ToString("R" /* format string */, formatProvider);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x3D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Matrix3x3D);
            if (IsIdentity) return nameof(Identity);
            var s = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Matrix3x3D)}({nameof(M0x0)}:{M0x0.ToString(format, formatProvider)}{s} {nameof(M0x1)}:{M0x1.ToString(format, formatProvider)}{s} {nameof(M0x2)}:{M0x2.ToString(format, formatProvider)}{s} {nameof(M1x0)}:{M1x0.ToString(format, formatProvider)}{s} {nameof(M1x1)}:{M1x1.ToString(format, formatProvider)}{s} {nameof(M1x2)}:{M1x2.ToString(format, formatProvider)}{s} {nameof(M2x0)}:{M2x0.ToString(format, formatProvider)}{s} {nameof(M2x1)}:{M2x1.ToString(format, formatProvider)}{s} {nameof(M2x2)}:{M2x2.ToString(format, formatProvider)})";
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
