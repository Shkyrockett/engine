// <copyright file="Matrix4x4D.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Math;
using static Engine.Maths;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [TypeConverter(typeof(StructConverter<Matrix4x4D>))]
    public partial struct Matrix4x4D
        : IMatrix<Matrix4x4D, Vector4D>
    {
        #region Static Fields

        /// <summary>
        /// An Empty <see cref="Matrix4x4D"/>.
        /// </summary>
        public static readonly Matrix4x4D Empty = new Matrix4x4D(
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0);

        /// <summary>
        /// An Identity <see cref="Matrix4x4D"/>.
        /// </summary>
        public static readonly Matrix4x4D Identity = new Matrix4x4D(
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

        #endregion

        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private double m0x0;

        /// <summary>
        /// 
        /// </summary>
        private double m0x1;

        /// <summary>
        /// 
        /// </summary>
        private double m0x2;

        /// <summary>
        /// 
        /// </summary>
        private double m0x3;

        /// <summary>
        /// 
        /// </summary>
        private double m1x0;

        /// <summary>
        /// 
        /// </summary>
        private double m1x1;

        /// <summary>
        /// 
        /// </summary>
        private double m1x2;

        /// <summary>
        /// 
        /// </summary>
        private double m1x3;

        /// <summary>
        /// 
        /// </summary>
        private double m2x0;

        /// <summary>
        /// 
        /// </summary>
        private double m2x1;

        /// <summary>
        /// 
        /// </summary>
        private double m2x2;

        /// <summary>
        /// 
        /// </summary>
        private double m2x3;

        /// <summary>
        /// 
        /// </summary>
        private double m3x0;

        /// <summary>
        /// 
        /// </summary>
        private double m3x1;

        /// <summary>
        /// 
        /// </summary>
        private double m3x2;

        /// <summary>
        /// 
        /// </summary>
        private double m3x3;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuple"></param>
        public Matrix4x4D((double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) tuple)
        {
            (m0x0, m0x1, m0x2, m0x3,
                           m1x0, m1x1, m1x2, m1x3,
                           m2x0, m2x1, m2x2, m2x3,
                           m3x0, m3x1, m3x2, m3x3) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4x4D"/> class.
        /// </summary>
        /// <param name="m0x0"></param>
        /// <param name="m0x1"></param>
        /// <param name="m0x2"></param>
        /// <param name="m0x3"></param>
        /// <param name="m1x0"></param>
        /// <param name="m1x1"></param>
        /// <param name="m1x2"></param>
        /// <param name="m1x3"></param>
        /// <param name="m2x0"></param>
        /// <param name="m2x1"></param>
        /// <param name="m2x2"></param>
        /// <param name="m2x3"></param>
        /// <param name="m3x0"></param>
        /// <param name="m3x1"></param>
        /// <param name="m3x2"></param>
        /// <param name="m3x3"></param>
        public Matrix4x4D(
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3)
        {
            this.m0x0 = m0x0;
            this.m0x1 = m0x1;
            this.m0x2 = m0x2;
            this.m0x3 = m0x3;
            this.m1x0 = m1x0;
            this.m1x1 = m1x1;
            this.m1x2 = m1x2;
            this.m1x3 = m1x3;
            this.m2x0 = m2x0;
            this.m2x1 = m2x1;
            this.m2x2 = m2x2;
            this.m2x3 = m2x3;
            this.m3x0 = m3x0;
            this.m3x1 = m3x1;
            this.m3x2 = m3x2;
            this.m3x3 = m3x3;
        }

        /// <summary>
        /// Create a new Matrix from 2 Vector4D objects.
        /// </summary>
        /// <param name="xAxis"></param>
        /// <param name="yAxis"></param>
        /// <param name="zAxis"></param>
        /// <param name="wAxis"></param>
        public Matrix4x4D(Vector4D xAxis, Vector4D yAxis, Vector4D zAxis, Vector4D wAxis)
            : this(xAxis.I, xAxis.J, xAxis.K, xAxis.L,
                  yAxis.I, yAxis.J, yAxis.K, yAxis.L,
                  zAxis.I, zAxis.J, zAxis.K, zAxis.L,
                  wAxis.I, wAxis.J, wAxis.K, wAxis.L)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public double M0x0 {
            get
            {
                return m0x0;
            }

            set
            {
                m0x0 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M0x1 {
            get
            {
                return m0x1;
            }

            set
            {
                m0x1 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M0x2 {
            get
            {
                return m0x2;
            }

            set
            {
                m0x2 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M0x3 {
            get
            {
                return m0x3;
            }

            set
            {
                m0x3 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M1x0 {
            get
            {
                return m1x0;
            }

            set
            {
                m1x0 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M1x1 {
            get
            {
                return m1x1;
            }

            set
            {
                m1x1 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M1x2 {
            get
            {
                return m1x2;
            }

            set
            {
                m1x2 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M1x3 {
            get
            {
                return m1x3;
            }

            set
            {
                m1x3 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M2x0 {
            get
            {
                return m2x0;
            }

            set
            {
                m2x0 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M2x1 {
            get
            {
                return m2x1;
            }

            set
            {
                m2x1 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M2x2 {
            get
            {
                return m2x2;
            }

            set
            {
                m2x2 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M2x3 {
            get
            {
                return m2x3;
            }

            set
            {
                m2x3 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M3x0 {
            get
            {
                return m3x0;
            }

            set
            {
                m3x0 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M3x1 {
            get
            {
                return m3x1;
            }

            set
            {
                m3x1 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M3x2 {
            get
            {
                return m3x2;
            }

            set
            {
                m3x2 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double M3x3 {
            get
            {
                return m3x3;
            }

            set
            {
                m3x3 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Vector4D Cx
        {
            get
            {
                return new Vector4D(m0x0, m1x0, m2x0, m3x0);
            }

            set
            {
                m0x0 = value.I;
                m1x0 = value.J;
                m2x0 = value.K;
                m3x0 = value.L;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Vector4D Cy
        {
            get
            {
                return new Vector4D(m0x1, m1x1, m2x1, m3x1);
            }

            set
            {
                m0x1 = value.I;
                m1x1 = value.J;
                m2x1 = value.K;
                m3x1 = value.L;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Vector4D Cz
        {
            get
            {
                return new Vector4D(m0x2, m1x2, m2x2, m3x2);
            }

            set
            {
                m0x2 = value.I;
                m1x2 = value.J;
                m2x2 = value.K;
                m3x2 = value.L;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Vector4D Cw
        {
            get
            {
                return new Vector4D(m0x3, m1x3, m2x3, m3x3);
            }

            set
            {
                m0x3 = value.I;
                m1x3 = value.J;
                m2x3 = value.K;
                m3x3 = value.L;
            }
        }

        /// <summary>
        /// The X Row or row zero.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Description("The First row of the " + nameof(Matrix4x4D))]
        public Vector4D Rx
        {
            get
            {
                return new Vector4D(m0x0, m0x1, m0x2, m0x3);
            }

            set
            {
                m0x0 = value.I;
                m0x1 = value.J;
                m0x2 = value.K;
                m0x3 = value.L;
            }
        }

        /// <summary>
        /// The Y Row or row one.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Description("The Second row of the " + nameof(Matrix4x4D))]
        public Vector4D Ry
        {
            get
            {
                return new Vector4D(m1x0, m1x1, m1x2, m1x3);
            }

            set
            {
                m1x0 = value.I;
                m1x1 = value.J;
                m1x2 = value.K;
                m1x3 = value.L;
            }
        }

        /// <summary>
        /// The Z Row or row one.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Description("The Third row of the " + nameof(Matrix4x4D))]
        public Vector4D Rz
        {
            get
            {
                return new Vector4D(m2x0, m2x1, m2x2, m2x3);
            }

            set
            {
                m2x0 = value.I;
                m2x1 = value.J;
                m2x2 = value.K;
                m2x3 = value.L;
            }
        }

        /// <summary>
        /// The W Row or row one.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Description("The Third row of the " + nameof(Matrix4x4D))]
        public Vector4D Rw
        {
            get
            {
                return new Vector4D(m3x0, m3x1, m3x2, m3x3);
            }

            set
            {
                m3x0 = value.I;
                m3x1 = value.J;
                m3x2 = value.K;
                m3x3 = value.L;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public double Determinant
            => Determinant(m0x0, m0x1, M0x2, M0x3, m1x0, m1x1, m1x2, m1x3, m2x0, m2x1, m2x2, m2x3, m3x0, m3x1, m3x2, m3x3);

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <returns>A transposed Matrix.</returns>
        [XmlIgnore, SoapIgnore]
        public Matrix4x4D Transposed
            => Primitives.Transpose(this);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Matrix4x4D Adjoint
            => Primitives.Adjoint(this);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Matrix4x4D Cofactor
            => Primitives.Cofactor(this);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Matrix4x4D Inverted
            => Primitives.Invert(this);

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public bool IsIdentity
            => (Abs(m0x0 - 1) < Epsilon
                && Abs(m0x1) < Epsilon
                && Abs(m0x2) < Epsilon
                && Abs(m0x3) < Epsilon
                && Abs(m1x0) < Epsilon
                && Abs(m1x1 - 1) < Epsilon
                && Abs(m1x2) < Epsilon
                && Abs(m1x3) < Epsilon
                && Abs(m2x0) < Epsilon
                && Abs(m2x1) < Epsilon
                && Abs(m2x2 - 1) < Epsilon
                && Abs(m2x3) < Epsilon
                && Abs(m3x0) < Epsilon
                && Abs(m3x1) < Epsilon
                && Abs(m3x2) < Epsilon
                && Abs(m3x3 - 1) < Epsilon);

        #endregion

        #region Operators

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix4x4D operator +(Matrix4x4D left, Matrix4x4D right)
            => left.Add(right);

        /// <summary>
        /// Negates all the items in the Matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix4x4D operator -(Matrix4x4D matrix)
            => matrix.Negate();

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix4x4D operator -(Matrix4x4D left, Matrix4x4D right)
            => left.Subtract(right);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix4x4D operator *(Matrix4x4D matrix, double scalar)
            => matrix.Scale(scalar);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix4x4D operator *(double scalar, Matrix4x4D matrix)
            => matrix.Scale(scalar);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix4x4D operator *(Matrix2x2D left, Matrix4x4D right)
            => left.Multiply(right);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix4x4D operator *(Matrix3x3D left, Matrix4x4D right)
            => left.Multiply(right);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix4x4D operator *(Matrix4x4D left, Matrix2x2D right)
            => left.Multiply(right);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix4x4D operator *(Matrix4x4D left, Matrix3x3D right)
            => left.Multiply(right);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix4x4D operator *(Matrix4x4D left, Matrix4x4D right)
            => left.Multiply(right);

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
        public static bool operator ==(Matrix4x4D matrix1, Matrix4x4D matrix2)
            => Equals(matrix1, matrix2);

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
        public static bool operator !=(Matrix4x4D matrix1, Matrix4x4D matrix2)
            => !Equals(matrix1, matrix2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        [DebuggerStepThrough]
        public static explicit operator Matrix4x4D(Matrix3x3D source)
            => new Matrix4x4D(
                source.M0x0, source.M0x1, source.M0x2, 0,
                source.M1x0, source.M1x1, source.M1x2, 0,
                source.M2x0, source.M2x1, source.M2x2, 0,
                0, 0, 0, 1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public static explicit operator Matrix4x4D(Matrix2x2D source)
            => new Matrix4x4D(
                source.M0x0, source.M0x1, 0, 0,
                source.M1x0, source.M1x1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        /// <summary>
        /// Tupple to <see cref="Matrix4x4D"/>.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Matrix4x4D((double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) tuple)
            => new Matrix4x4D(tuple);

        #endregion

        #region Factories

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Matrix4x4D FromScale(Vector2D scale)
            => new Matrix4x4D(
                scale.I, 0, 0, 0,
                0, scale.J, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Matrix4x4D FromScale(Vector3D scale)
            => new Matrix4x4D(
                scale.I, 0, 0, 0,
                0, scale.J, 0, 0,
                0, 0, scale.K, 0,
                0, 0, 0, 0);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Matrix4x4D FromScale(Vector4D scale)
            => new Matrix4x4D(
                scale.I, 0, 0, 0,
                0, scale.J, 0, 0,
                0, 0, scale.K, 0,
                0, 0, 0, scale.L);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        public static Matrix4x4D FromScale(double scaleX, double scaleY)
            => new Matrix4x4D(
                scaleX, 0, 0, 0,
                0, scaleY, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        /// <param name='scaleZ'>The scale factor in the z dimension</param>
        public static Matrix4x4D FromScale(double scaleX, double scaleY, double scaleZ)
            => new Matrix4x4D(
                scaleX, 0, 0, 0,
                0, scaleY, 0, 0,
                0, 0, scaleZ, 0,
                0, 0, 0, 0);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        /// <param name='scaleZ'>The scale factor in the z dimension</param>
        /// <param name="scaleW">The scale factor in the w dimension</param>
        public static Matrix4x4D FromScale(double scaleX, double scaleY, double scaleZ, double scaleW)
            => new Matrix4x4D(
                scaleX, 0, 0, 0,
                0, scaleY, 0, 0,
                0, 0, scaleZ, 0,
                0, 0, 0, scaleW);

        /// <summary>
        /// Parse a string for a <see cref="Matrix4x4D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Matrix4x4D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix4x4D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [ParseMethod]
        public static Matrix4x4D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Matrix4x4D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Matrix4x4D"/> data </param>
        /// <param name="provider"></param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix4x4D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Matrix4x4D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            Matrix4x4D value;
            string firstToken = tokenizer.NextTokenRequired();
            // The token will already have had whitespace trimmed so we can do a
            // simple string compare.
            if (firstToken == "Identity")
            {
                value = Identity;
            }
            else
            {
                value = new Matrix4x4D(
                    firstToken.ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider));
            }
            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }

        #endregion

        #region Serialization

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing()]
        private void OnSerializing(StreamingContext context)
        {
            // Assert("This value went into the data file during serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerialized()]
        private void OnSerialized(StreamingContext context)
        {
            // Assert("This value was reset after serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing()]
        private void OnDeserializing(StreamingContext context)
        {
            // Assert("This value was set during deserialization");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized()]
        private void OnDeserialized(StreamingContext context)
        {
            // Assert("This value was set after deserialization.");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the HashCode for this Matrix
        /// </summary>
        /// <returns>
        /// int - the HashCode for this Matrix
        /// </returns>
        public override int GetHashCode()
            => m0x0.GetHashCode()
            ^ m0x1.GetHashCode()
            ^ m0x2.GetHashCode()
            ^ m0x3.GetHashCode()
            ^ m1x0.GetHashCode()
            ^ m1x1.GetHashCode()
            ^ m1x2.GetHashCode()
            ^ m1x3.GetHashCode()
            ^ m2x0.GetHashCode()
            ^ m2x1.GetHashCode()
            ^ m2x2.GetHashCode()
            ^ m2x3.GetHashCode()
            ^ m3x0.GetHashCode()
            ^ m3x1.GetHashCode()
            ^ m3x2.GetHashCode()
            ^ m3x3.GetHashCode();

        /// <summary>
        /// Compares two Matrix2D
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Matrix4x4D a, Matrix4x4D b)
            => Equals(a, b);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Matrix4x4D matrix1, Matrix4x4D matrix2)
            => matrix1.M0x0.Equals(matrix2.M0x0)
                && matrix1.M0x1.Equals(matrix2.M0x1)
                && matrix1.M0x2.Equals(matrix2.M0x2)
                && matrix1.M0x3.Equals(matrix2.M0x3)
                && matrix1.M1x0.Equals(matrix2.M1x0)
                && matrix1.M1x1.Equals(matrix2.M1x1)
                && matrix1.M1x2.Equals(matrix2.M1x2)
                && matrix1.M1x3.Equals(matrix2.M1x3)
                && matrix1.M2x0.Equals(matrix2.M2x0)
                && matrix1.M2x1.Equals(matrix2.M2x1)
                && matrix1.M2x2.Equals(matrix2.M2x2)
                && matrix1.M2x3.Equals(matrix2.M2x3)
                && matrix1.M3x0.Equals(matrix2.M3x0)
                && matrix1.M3x1.Equals(matrix2.M3x1)
                && matrix1.M3x2.Equals(matrix2.M3x2)
                && matrix1.M3x3.Equals(matrix2.M3x3);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Matrix4x4D && Equals(this, (Matrix4x4D)obj);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Matrix4x4D value)
            => Equals(this, value);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D"/> struct based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (IsIdentity) return "Identity";
            // Helper to get the numeric list separator for a given culture.
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Matrix4x4D)}{{{nameof(M0x0)}={m0x0}{sep}{nameof(M0x1)}={m0x1}{sep}{nameof(M0x2)}={m0x2}{sep}{nameof(M0x3)}={m0x3}{sep}{nameof(M1x0)}={m1x0}{sep}{nameof(M1x1)}={m1x1}{sep}{nameof(M1x2)}={m1x2}{sep}{nameof(M1x3)}={m1x3}{sep}{nameof(M2x0)}={m2x0}{sep}{nameof(M2x1)}={m2x1}{sep}{nameof(M2x2)}={m2x2}{sep}{nameof(M2x3)}={m2x3}{sep}{nameof(M3x0)}={m3x0}{sep}{nameof(M3x1)}={m3x1}{sep}{nameof(M3x2)}={m3x2}{sep}{nameof(M3x3)}={m3x3}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
