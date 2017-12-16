// <copyright file="Matrix3x3D.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
using System.Collections;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    ///
    /// </summary>
    [DataContract, Serializable]
    [ComVisible(true)]
    [TypeConverter(typeof(StructConverter<Matrix3x3D>))]
    public partial struct Matrix3x3D
        : IMatrix<Matrix3x3D, Vector3D>
    {
        #region Static Fields

        /// <summary>
        /// An Empty <see cref="Matrix3x3D"/>.
        /// </summary>
        public static readonly Matrix3x3D Empty = new Matrix3x3D(
            0, 0, 0,
            0, 0, 0,
            0, 0, 0);

        /// <summary>
        /// An Identity <see cref="Matrix3x3D"/>.
        /// </summary>
        public static readonly Matrix3x3D Identity = new Matrix3x3D(
            1, 0, 0,
            0, 1, 0,
            0, 0, 1);

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
        private double m2x0;

        /// <summary>
        ///
        /// </summary>
        private double m2x1;

        /// <summary>
        ///
        /// </summary>
        private double m2x2;

        #endregion

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="tuple"></param>
        public Matrix3x3D((double, double, double, double, double, double, double, double, double) tuple)
        {
            (m0x0, m0x1, m0x2, m1x0, m1x1, m1x2, m2x0, m2x1, m2x2) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix3x2D"/> class of the form:<br/>
        /// / m11, m12, 0 \<br/>
        /// | m21, m22, 0 |<br/>
        /// \ offsetX, offsetY, 1 /<br/>
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
        public Matrix3x3D(
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2)
        {
            this.m0x0 = m0x0;
            this.m0x1 = m0x1;
            this.m0x2 = m0x2;
            this.m1x0 = m1x0;
            this.m1x1 = m1x1;
            this.m1x2 = m1x2;
            this.m2x0 = m2x0;
            this.m2x1 = m2x1;
            this.m2x2 = m2x2;
        }

        /// <summary>
        /// Create a new Matrix from 2 Vertex2 objects.
        /// </summary>
        /// <param name="xAxis"></param>
        /// <param name="yAxis"></param>
        /// <param name="zAxis"></param>
        public Matrix3x3D(Vector3D xAxis, Vector3D yAxis, Vector3D zAxis)
            : this(xAxis.I, xAxis.J, xAxis.K, yAxis.I, yAxis.J, yAxis.K, zAxis.I, zAxis.J, zAxis.K)
        { }

        #endregion

        #region Properties

        /// <summary>
        ///
        /// </summary>
        public double M0x0 { get { return m0x0; } set { m0x0 = value; } }

        /// <summary>
        ///
        /// </summary>
        public double M0x1 { get { return m0x1; } set { m0x1 = value; } }

        /// <summary>
        ///
        /// </summary>
        public double M0x2 { get { return m0x2; } set { m0x2 = value; } }

        /// <summary>
        ///
        /// </summary>
        public double M1x0 { get { return m1x0; } set { m1x0 = value; } }

        /// <summary>
        ///
        /// </summary>
        public double M1x1 { get { return m1x1; } set { m1x1 = value; } }

        /// <summary>
        ///
        /// </summary>
        public double M1x2 { get { return m1x2; } set { m1x2 = value; } }

        /// <summary>
        ///
        /// </summary>
        public double M2x0 { get { return m2x0; } set { m2x0 = value; } }

        /// <summary>
        ///
        /// </summary>
        public double M2x1 { get { return m2x1; } set { m2x1 = value; } }

        /// <summary>
        ///
        /// </summary>
        public double M2x2 { get { return m2x2; } set { m2x2 = value; } }

        /// <summary>
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Vector3D Cx
        {
            get { return new Vector3D(m0x0, m1x0, m2x0); }
            set
            {
                m0x0 = value.I;
                m1x0 = value.J;
                m2x0 = value.K;
            }
        }

        /// <summary>
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Vector3D Cy
        {
            get { return new Vector3D(m0x1, m1x1, m2x1); }
            set
            {
                m0x1 = value.I;
                m1x1 = value.J;
                m2x1 = value.K;
            }
        }

        /// <summary>
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Vector3D Cz
        {
            get { return new Vector3D(m0x2, m1x2, m2x2); }
            set
            {
                m0x2 = value.I;
                m1x2 = value.J;
                m2x2 = value.K;
            }
        }

        /// <summary>
        /// The X Row or row zero.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The First row of the " + nameof(Matrix3x3D))]
        public Vector3D Rx
        {
            get { return new Vector3D(m0x0, m0x1, m0x2); }
            set
            {
                m0x0 = value.I;
                m0x1 = value.J;
                m0x2 = value.K;
            }
        }

        /// <summary>
        /// The Y Row or row one.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Second row of the " + nameof(Matrix3x3D))]
        public Vector3D Ry
        {
            get { return new Vector3D(m1x0, m1x1, M1x2); }
            set
            {
                m1x0 = value.I;
                m1x1 = value.J;
                m1x2 = value.K;
            }
        }

        /// <summary>
        /// The Z Row or row one.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Third row of the " + nameof(Matrix3x3D))]
        public Vector3D Rz
        {
            get            {                return new Vector3D(m2x0, m2x1, M2x2);            }
            set
            {
                m2x0 = value.I;
                m2x1 = value.J;
                m2x2 = value.K;
            }
        }

        /// <summary>
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Determinant
            => Determinant(m0x0, m0x1, M0x2, m1x0, m1x1, m1x2, m2x0, m2x1, m2x2);

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <returns>A transposed Matrix.</returns>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix3x3D Transposed
            => Primitives.Transpose(this);

        /// <summary>
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix3x3D Adjoint
            => Primitives.Adjoint(this);

        /// <summary>
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix3x3D Cofactor
            => Primitives.Cofactor(this);

        /// <summary>
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix3x3D Inverted
            => Primitives.Invert(this);

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsIdentity
            => (Abs(m0x0 - 1) < Epsilon
                && Abs(m0x1) < Epsilon
                && Abs(m0x2) < Epsilon
                && Abs(m1x0) < Epsilon
                && Abs(m1x1 - 1) < Epsilon
                && Abs(m1x2) < Epsilon
                && Abs(m2x0) < Epsilon
                && Abs(m2x1) < Epsilon
                && Abs(m2x2 - 1) < Epsilon);

        #endregion

        #region Operators

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix3x3D operator +(Matrix3x3D left, Matrix3x3D right)
            => left.Add(right);

        /// <summary>
        /// Negates all the items in the Matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix3x3D operator -(Matrix3x3D matrix)
            => matrix.Negate();

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix3x3D operator -(Matrix3x3D left, Matrix3x3D right)
            => left.Subtract(right);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix3x3D operator *(Matrix3x3D matrix, double scalar)
            => matrix.Scale(scalar);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix3x3D operator *(double scalar, Matrix3x3D matrix)
            => matrix.Scale(scalar);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix3x3D operator *(Matrix2x2D left, Matrix3x3D right)
            => left.Multiply(right);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix3x3D operator *(Matrix3x3D left, Matrix2x2D right)
            => left.Multiply(right);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix3x3D operator *(Matrix3x3D left, Matrix3x3D right)
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
        public static bool operator ==(Matrix3x3D matrix1, Matrix3x3D matrix2)
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
        public static bool operator !=(Matrix3x3D matrix1, Matrix3x3D matrix2)
            => !Equals(matrix1, matrix2);

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        public static explicit operator Matrix3x3D(Matrix2x2D source)
            => new Matrix3x3D(
                source.M0x0, source.M0x1, 0,
                source.M1x0, source.M1x1, 0,
                0, 0, 1);

        /// <summary>
        /// Tupple to <see cref="Matrix3x3D"/>.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Matrix3x3D((double, double, double, double, double, double, double, double, double) tuple)
            => new Matrix3x3D(tuple);

        #endregion

        #region Factories

        /// <summary>
        ///
        /// </summary>
        /// <param name="radianAngle"></param>
        /// <returns></returns>
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
        ///
        /// </summary>
        /// <param name="radianAngle"></param>
        /// <returns></returns>
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
        ///
        /// </summary>
        /// <param name="radianAngle"></param>
        /// <returns></returns>
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
        ///
        /// </summary>
        /// <param name="radianAngle"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
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
                    zAngle = Right;
                    yAngle = Atan(axis.K / axis.J);
                }
            }
            else
            {
                zAngle = Atan(axis.J / axis.I);
                yAngle = Atan(axis.K / Sqrt(axis.I * axis.I + axis.J * axis.J));
            }
            return FromRotationZ(-zAngle) *
            FromRotationY(-yAngle) *
            FromRotationX(radianAngle) *
            FromRotationY(yAngle) *
            FromRotationZ(zAngle);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="radianAngle"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static Matrix3x3D FromRotationAxis(double radianAngle, Vector3D axis)
        {
            var first = FromLookAt(Vector3D.Empty, axis, new Vector3D(axis.K, axis.I, axis.J));
            return first.Inverted * FromRotationZ(radianAngle) * first;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="positiveZAxis"></param>
        /// <param name="onPositiveY"></param>
        /// <returns></returns>
        internal static Matrix3x3D FromLookAt(Vector3D origin, Vector3D positiveZAxis, Vector3D onPositiveY)
        {
            var rv = Identity;
            rv.Rz = Primitives.Normalize(positiveZAxis - origin);
            var translated = onPositiveY - origin;
            rv.Rx = Primitives.Normalize(CrossProduct(translated.I, translated.J, translated.K, rv.Rz.I, rv.Rz.J, rv.Rz.K));
            rv.Ry = Primitives.Normalize(CrossProduct(rv.Rz.I, rv.Rz.J, rv.Rz.K, rv.Rx.I, rv.Rx.J, rv.Rx.K));
            return rv;
        }

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
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
        public static Matrix3x3D FromScale(double scaleX, double scaleY, double scaleZ)
            => new Matrix3x3D(
                scaleX, 0, 0,
                0, scaleY, 0,
                0, 0, scaleZ);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Matrix3x3D FromTranslate2D(Vector2D value)
            => new Matrix3x3D(
                1, 0, value.I,
                0, 1, value.J,
                0, 0, 1);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Matrix3x3D FromShear3D(Vector2D value)
            => new Matrix3x3D(
                1, 0, value.I,
                0, 1, value.J,
                0, 0, 1);

        /// <summary>
        /// Constructs this Matrix from 3 euler angles, in degrees.
        /// </summary>
        /// <param name="yaw"></param>
        /// <param name="pitch"></param>
        /// <param name="roll"></param>
        public static Matrix3x3D FromEulerAnglesXYZ(double yaw, double pitch, double roll)
            => FromRotationX(yaw) * (FromRotationY(pitch) * FromRotationZ(roll));

        /// <summary>
        /// Parse a string for a <see cref="Matrix3x3D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Matrix3x3D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix3x3D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [ParseMethod]
        public static Matrix3x3D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Matrix3x2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Matrix3x2D"/> data </param>
        /// <param name="provider"></param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix3x2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Matrix3x3D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            Matrix3x3D value;
            var firstToken = tokenizer.NextTokenRequired();
            // The token will already have had whitespace trimmed so we can do a
            // simple string compare.
            value = firstToken == "Identity" ? Identity : new Matrix3x3D(
                    firstToken.ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider));
            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }

        #endregion

        //#region Serialization

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    // Assert("This value went into the data file during serialization.");
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    // Assert("This value was reset after serialization.");
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    // Assert("This value was set during deserialization");
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    // Assert("This value was set after deserialization.");
        //}

        //#endregion

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
            ^ m1x0.GetHashCode()
            ^ m1x1.GetHashCode()
            ^ m1x2.GetHashCode()
            ^ m2x0.GetHashCode()
            ^ m2x1.GetHashCode()
            ^ m2x2.GetHashCode();

        /// <summary>
        /// Compares two Matrix2D
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Matrix3x3D a, Matrix3x3D b)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Matrix3x3D && Equals(this, (Matrix3x3D)obj);

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
        public bool Equals(Matrix3x3D value)
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
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Matrix3x3D)}{{{nameof(M0x0)}={m0x0}{sep}{nameof(M0x1)}={m0x1}{sep}{nameof(M0x2)}={m0x2}{sep}{nameof(M1x0)}={m1x0}{sep}{nameof(M1x1)}={m1x1}{sep}{nameof(M1x2)}={m1x2}{sep}{nameof(M2x0)}={m2x0}{sep}{nameof(M2x1)}={m2x1}{sep}{nameof(M2x2)}={m2x2}}}";
            return formatable.ToString(format, provider);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IEnumerable<double>> GetEnumerator()
            => new List<List<double>>
            {
                new List<double> { m0x0, m0x1, m0x2 },
                new List<double> { m1x0, m1x1, m1x2 },
                new List<double> { m2x0, m2x1, m2x2 },
            }.GetEnumerator();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        #endregion
    }
}
