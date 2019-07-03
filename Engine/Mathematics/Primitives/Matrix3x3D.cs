// <copyright file="Matrix3x3D.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
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
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Mathematics;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The matrix3x3d struct.
    /// </summary>
    [ComVisible(true)]
    [DataContract, Serializable]
    //[TypeConverter(typeof(Matrix3x3DConverter))]
    [TypeConverter(typeof(StructConverter<Matrix3x3D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Matrix3x3D
        : IMatrix<Matrix3x3D, Vector3D>
    {
        #region Static Fields
        /// <summary>
        /// An Empty <see cref="Matrix3x3D"/>.
        /// </summary>
        public static readonly Matrix3x3D Empty = new Matrix3x3D(
            0d, 0d, 0d,
            0d, 0d, 0d,
            0d, 0d, 0d);

        /// <summary>
        /// An Identity <see cref="Matrix3x3D"/>.
        /// </summary>
        public static readonly Matrix3x3D Identity = new Matrix3x3D(
            1d, 0d, 0d,
            0d, 1d, 0d,
            0d, 0d, 1d);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix3x3D"/> class.
        /// </summary>
        /// <param name="m0x0"></param>
        /// <param name="m0x1"></param>
        /// <param name="m0x2"></param>
        /// <param name="m1x0"></param>
        /// <param name="m1x1"></param>
        /// <param name="m1x2"></param>
        /// <param name="m2x0"></param>
        /// <param name="m2x1"></param>
        /// <param name="m2x2"></param>
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
        /// <param name="xAxis"></param>
        /// <param name="yAxis"></param>
        /// <param name="zAxis"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix3x3D(Vector3D xAxis, Vector3D yAxis, Vector3D zAxis)
            : this(xAxis.I, xAxis.J, xAxis.K, yAxis.I, yAxis.J, yAxis.K, zAxis.I, zAxis.J, zAxis.K)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix3x3D"/> class.
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
        /// Deconstruct this <see cref="Matrix2x2D"/> to a <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}"/>.
        /// </summary>
        /// <param name="m0x0">The m0x0.</param>
        /// <param name="m0x1">The m0x1.</param>
        /// <param name="m0x2"></param>
        /// <param name="m1x0">The m1x0.</param>
        /// <param name="m1x1">The m1x1.</param>
        /// <param name="m1x2"></param>
        /// <param name="m2x0"></param>
        /// <param name="m2x1"></param>
        /// <param name="m2x2"></param>
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

        #region Properties
        /// <summary>
        /// Gets or sets the m0x0.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M0x0 { get; set; }

        /// <summary>
        /// Gets or sets the m0x1.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M0x1 { get; set; }

        /// <summary>
        /// Gets or sets the m0x2.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M0x2 { get; set; }

        /// <summary>
        /// Gets or sets the m1x0.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M1x0 { get; set; }

        /// <summary>
        /// Gets or sets the m1x1.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M1x1 { get; set; }

        /// <summary>
        /// Gets or sets the m1x2.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M1x2 { get; set; }

        /// <summary>
        /// Gets or sets the m2x0.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M2x0 { get; set; }

        /// <summary>
        /// Gets or sets the m2x1.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M2x1 { get; set; }

        /// <summary>
        /// Gets or sets the m2x2.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double M2x2 { get; set; }

        /// <summary>
        /// Gets or sets the cx.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The First column of the " + nameof(Matrix3x3D))]
        public Vector3D Cx { get { return new Vector3D(M0x0, M1x0, M2x0); } set { (M0x0, M1x0, M2x0) = value; } }

        /// <summary>
        /// Gets or sets the cy.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Second column of the " + nameof(Matrix3x3D))]
        public Vector3D Cy { get { return new Vector3D(M0x1, M1x1, M2x1); } set { (M0x1, M1x1, M2x1) = value; } }

        /// <summary>
        /// Gets or sets the cz.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Third column of the " + nameof(Matrix3x3D))]
        public Vector3D Cz { get { return new Vector3D(M0x2, M1x2, M2x2); } set { (M0x2, M1x2, M2x2) = value; } }

        /// <summary>
        /// Gets or sets the rx.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The First row of the " + nameof(Matrix3x3D))]
        public Vector3D Rx { get { return new Vector3D(M0x0, M0x1, M0x2); } set { (M0x0, M0x1, M0x2) = value; } }

        /// <summary>
        /// Gets or sets the ry.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Second row of the " + nameof(Matrix3x3D))]
        public Vector3D Ry { get { return new Vector3D(M1x0, M1x1, M1x2); } set { (M1x0, M1x1, M1x2) = value; } }

        /// <summary>
        /// Gets or sets the rz.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Third row of the " + nameof(Matrix3x3D))]
        public Vector3D Rz { get { return new Vector3D(M2x0, M2x1, M2x2); } set { (M2x0, M2x1, M2x2) = value; } }

        /// <summary>
        /// Gets the determinant.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Determinant => Determinant(M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2);

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix3x3D Transposed => Transpose(M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2);

        /// <summary>
        /// Gets the adjoint.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix3x3D Adjoint => Adjoint(M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2);

        /// <summary>
        /// Gets the cofactor.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix3x3D Cofactor => Cofactor(M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2);

        /// <summary>
        /// Gets the inverted.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix3x3D Inverted => Invert(M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2);

        /// <summary>
        /// Gets a value indicating whether or not a given transform is an identity transform matrix.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsIdentity => Abs(M0x0 - 1) < Epsilon && Abs(M0x1) < Epsilon && Abs(M0x2) < Epsilon && Abs(M1x0) < Epsilon && Abs(M1x1 - 1) < Epsilon && Abs(M1x2) < Epsilon && Abs(M2x0) < Epsilon && Abs(M2x1) < Epsilon && Abs(M2x2 - 1) < Epsilon;
        #endregion Properties

        #region Operators
        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator +(Matrix3x3D left, Matrix3x3D right) => Add3x3x3x3(left.M0x0, left.M0x1, left.M0x2, left.M1x0, left.M1x1, left.M1x2, left.M2x0, left.M2x1, left.M2x2, right.M0x0, right.M0x1, right.M0x2, right.M1x0, right.M1x1, right.M1x2, right.M2x0, right.M2x1, right.M2x2);

        /// <summary>
        /// Negates all the items in the Matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator -(Matrix3x3D matrix) => Negate(matrix.M0x0, matrix.M0x1, matrix.M0x2, matrix.M1x0, matrix.M1x1, matrix.M1x2, matrix.M2x0, matrix.M2x1, matrix.M2x2);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator -(Matrix3x3D left, Matrix3x3D right) => Subtract3x3x3x3(left.M0x0, left.M0x1, left.M0x2, left.M1x0, left.M1x1, left.M1x2, left.M2x0, left.M2x1, left.M2x2, right.M0x0, right.M0x1, right.M0x2, right.M1x0, right.M1x1, right.M1x2, right.M2x0, right.M2x1, right.M2x2);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator *(Matrix3x3D matrix, double scalar) => Scale3x3(matrix.M0x0, matrix.M0x1, matrix.M0x2, matrix.M1x0, matrix.M1x1, matrix.M1x2, matrix.M2x0, matrix.M2x1, matrix.M2x2, scalar);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator *(double scalar, Matrix3x3D matrix) => Scale3x3(matrix.M0x0, matrix.M0x1, matrix.M0x2, matrix.M1x0, matrix.M1x1, matrix.M1x2, matrix.M2x0, matrix.M2x1, matrix.M2x2, scalar);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator *(Matrix2x2D left, Matrix3x3D right) => Multiply2x2x3x3(left.M0x0, left.M0x1, left.M1x0, left.M1x1, right.M0x0, right.M0x1, right.M0x2, right.M1x0, right.M1x1, right.M1x2, right.M2x0, right.M2x1, right.M2x2);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator *(Matrix3x3D left, Matrix2x2D right) => Multiply3x3x2x2(left.M0x0, left.M0x1, left.M0x2, left.M1x0, left.M1x1, left.M1x2, left.M2x0, left.M2x1, left.M2x2, right.M0x0, right.M0x1, right.M1x0, right.M1x1);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D operator *(Matrix3x3D left, Matrix3x3D right) => Multiply3x3x3x3(left.M0x0, left.M0x1, left.M0x2, left.M1x0, left.M1x1, left.M1x2, left.M2x0, left.M2x1, left.M2x2, right.M0x0, right.M0x1, right.M0x2, right.M1x0, right.M1x1, right.M1x2, right.M2x0, right.M2x1, right.M2x2);

        /// <summary>
        /// Compares two Matrix instances for exact equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two Matrix instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='matrix1'>The first Matrix to compare</param>
        /// <param name='matrix2'>The second Matrix to compare</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Matrix3x3D matrix1, Matrix3x3D matrix2) => Equals(matrix1, matrix2);

        /// <summary>
        /// Compares two Matrix instances for exact inequality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two Matrix instances are exactly unequal, false otherwise
        /// </returns>
        /// <param name='matrix1'>The first Matrix to compare</param>
        /// <param name='matrix2'>The second Matrix to compare</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Matrix3x3D matrix1, Matrix3x3D matrix2) => !Equals(matrix1, matrix2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Matrix3x3D(Matrix2x2D source) => new Matrix3x3D(source.M0x0, source.M0x1, 0, source.M1x0, source.M1x1, 0, 0, 0, 1);

        /// <summary>
        /// Tuple to <see cref="Matrix3x3D"/>.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double, double, double, double, double, double, double, double, double)(Matrix3x3D matrix) => (matrix.M0x0, matrix.M0x1, matrix.M0x2, matrix.M1x0, matrix.M1x1, matrix.M1x2, matrix.M2x0, matrix.M2x1, matrix.M2x2);

        /// <summary>
        /// Tuple to <see cref="Matrix3x3D"/>.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Matrix3x3D((double, double, double, double, double, double, double, double, double) tuple) => new Matrix3x3D(tuple);
        #endregion Operators

        #region Factories
        /// <summary>
        /// The from rotation x.
        /// </summary>
        /// <param name="radianAngle">The radianAngle.</param>
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromRotationX(double radianAngle)
        {
            var sin = Sin(radianAngle);
            var cos = Cos(radianAngle);
            return new Matrix3x3D(
                1, 0, 0,
                0, cos, -sin,
                0, sin, cos);
        }

        /// <summary>
        /// The from rotation y.
        /// </summary>
        /// <param name="radianAngle">The radianAngle.</param>
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromRotationY(double radianAngle)
        {
            var sin = Sin(radianAngle);
            var cos = Cos(radianAngle);
            return new Matrix3x3D(
                cos, 0, -sin,
                0, 1, 0,
                sin, 0, cos);
        }

        /// <summary>
        /// The from rotation z.
        /// </summary>
        /// <param name="radianAngle">The radianAngle.</param>
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromRotationZ(double radianAngle)
        {
            var sin = Sin(radianAngle);
            var cos = Cos(radianAngle);
            return new Matrix3x3D(
                cos, -sin, 0,
                sin, cos, 0,
                0, 0, 1);
        }

        /// <summary>
        /// The from rotation axis using atan.
        /// </summary>
        /// <param name="radianAngle">The radianAngle.</param>
        /// <param name="axis">The axis.</param>
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
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
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
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
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Matrix3x3D FromLookAt(Vector3D origin, Vector3D positiveZAxis, Vector3D onPositiveY)
        {
            var rv = Identity;
            var axis = positiveZAxis - origin;
            rv.Rz = Normalize3D(axis.I, axis.J, axis.K);
            var translated = onPositiveY - origin;
            rv.Rx = Normalize3D(CrossProduct(translated.I, translated.J, translated.K, rv.Rz.I, rv.Rz.J, rv.Rz.K));
            rv.Ry = Normalize3D(CrossProduct(rv.Rz.I, rv.Rz.J, rv.Rz.K, rv.Rx.I, rv.Rx.J, rv.Rx.K));
            return rv;
        }

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromScale(Vector2D scale)
            => new Matrix3x3D(
                scale.I, 0, 0,
                0, scale.J, 0,
                0, 0, 1);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromScale(Vector3D scale)
            => new Matrix3x3D(
                scale.I, 0, 0,
                0, scale.J, 0,
                0, 0, scale.K);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromScale(double scaleX, double scaleY)
            => new Matrix3x3D(
                scaleX, 0, 0,
                0, scaleY, 0,
                0, 0, 1);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        /// <param name='scaleZ'>The scale factor in the z dimension</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromScale(double scaleX, double scaleY, double scaleZ)
            => new Matrix3x3D(
                scaleX, 0, 0,
                0, scaleY, 0,
                0, 0, scaleZ);

        /// <summary>
        /// The from translate2d.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromTranslate2D(Vector2D value)
            => new Matrix3x3D(
                1, 0, value.I,
                0, 1, value.J,
                0, 0, 1);

        /// <summary>
        /// The from shear3d.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromShear3D(Vector2D value)
            => new Matrix3x3D(
                1, 0, value.I,
                0, 1, value.J,
                0, 0, 1);

        /// <summary>
        /// Constructs this Matrix from 3 Euler angles, in degrees.
        /// </summary>
        /// <param name="yaw"></param>
        /// <param name="pitch"></param>
        /// <param name="roll"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D FromEulerAnglesXYZ(double yaw, double pitch, double roll) => FromRotationX(yaw) * (FromRotationY(pitch) * FromRotationZ(roll));

        /// <summary>
        /// Parse a string for a <see cref="Matrix3x3D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Matrix3x3D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix3x3D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [ParseMethod]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Matrix3x2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Matrix3x2D"/> data </param>
        /// <param name="provider"></param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix3x2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
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

        #region Methods
        /// <summary>
        /// Returns the HashCode for this Matrix
        /// </summary>
        /// <returns>
        /// int - the HashCode for this Matrix
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => new { M0x0, M0x1, M0x2, M1x0, M1x1, M1x2, M2x0, M2x1, M2x2 }.GetHashCode();

        /// <returns></returns>
        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="T:IEnumerator{IEnumerable{double}}"/>.</returns>
        public IEnumerator<IEnumerable<double>> GetEnumerator()
            => new List<List<double>>
            {
                new List<double> { M0x0, M0x1, M0x2 },
                new List<double> { M1x0, M1x1, M1x2 },
                new List<double> { M2x0, M2x1, M2x2 },
            }.GetEnumerator();

        /// <returns></returns>
        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Compares two Matrix instances for object equality.  In this equality
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
        public static bool Equals(Matrix3x3D matrix1, Matrix3x3D matrix2)
            => matrix1.M0x0.Equals(matrix2.M0x0)
                && matrix1.M0x1.Equals(matrix2.M0x1)
                && matrix1.M0x2.Equals(matrix2.M0x2)
                && matrix1.M1x0.Equals(matrix2.M1x0)
                && matrix1.M1x1.Equals(matrix2.M1x1)
                && matrix1.M1x2.Equals(matrix2.M1x2)
                && matrix1.M2x0.Equals(matrix2.M2x0)
                && matrix1.M2x1.Equals(matrix2.M2x1)
                && matrix1.M2x2.Equals(matrix2.M2x2);

        /// <summary>
        /// Equals - compares this Matrix with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the object is an instance of Matrix and if it's equal to "this".
        /// </returns>
        /// <param name='obj'>The object to compare to "this"</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Matrix3x3D && Equals(this, (Matrix3x3D)obj);

        /// <summary>
        /// Equals - compares this Matrix with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if "value" is equal to "this".
        /// </returns>
        /// <param name='value'>The Matrix to compare to "this"</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Matrix3x3D value) => Equals(this, value);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D"/> struct based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider) => ToString("R" /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider) => ConvertToString(format /* format string */, provider /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Matrix3x3D);
            if (IsIdentity) return nameof(Identity);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Matrix3x3D)}({nameof(M0x0)}:{M0x0.ToString(format, provider)}{s} {nameof(M0x1)}:{M0x1.ToString(format, provider)}{s} {nameof(M0x2)}:{M0x2.ToString(format, provider)}{s} {nameof(M1x0)}:{M1x0.ToString(format, provider)}{s} {nameof(M1x1)}:{M1x1.ToString(format, provider)}{s} {nameof(M1x2)}:{M1x2.ToString(format, provider)}{s} {nameof(M2x0)}:{M2x0.ToString(format, provider)}{s} {nameof(M2x1)}:{M2x1.ToString(format, provider)}{s} {nameof(M2x2)}:{M2x2.ToString(format, provider)})";
        }
        #endregion Methods
    }
}
