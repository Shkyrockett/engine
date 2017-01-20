// <copyright file="Matrix2x2D.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
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

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [TypeConverter(typeof(StructConverter<Matrix2x2D>))]
    public partial struct Matrix2x2D
        : IMatrix<Matrix2x2D, Vector2D>
    {
        #region Static Fields

        /// <summary>
        /// An Empty <see cref="Matrix2x2D"/>.
        /// </summary>
        public static readonly Matrix2x2D Empty = new Matrix2x2D(0, 0, 0, 0);

        /// <summary>
        /// An Identity <see cref="Matrix2x2D"/>.
        /// </summary>
        public static readonly Matrix2x2D Identity = new Matrix2x2D(1, 0, 0, 1);

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
        private double m1x0;

        /// <summary>
        /// 
        /// </summary>
        private double m1x1;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuple"></param>
        public Matrix2x2D((double M1x1, double M1x2, double M2x1, double M2x2) tuple)
            => (m0x0, m0x1, m1x0, m1x1) = tuple;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix2x3D"/> class of the form:<br/>
        /// / m11, m12, 0 \<br/>
        /// | m21, m22, 0 |<br/>
        /// \ offsetX, offsetY, 1 /<br/>
        /// </summary>
        public Matrix2x2D(double m0x0, double m0x1, double m1x0, double m1x1)
        {
            this.m0x0 = m0x0;
            this.m0x1 = m0x1;
            this.m1x0 = m1x0;
            this.m1x1 = m1x1;
        }

        /// <summary>
        /// Create a new Matrix from 2 Vertex2 objects.
        /// </summary>
        /// <param name="xAxis"></param>
        /// <param name="yAxis"></param>
        public Matrix2x2D(Vector2D xAxis, Vector2D yAxis)
            : this(xAxis.I, xAxis.J, yAxis.I, yAxis.J)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// M11
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
        /// M12
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
        /// M22
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
        /// M22
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
        [XmlIgnore, SoapIgnore]
        public Vector2D Cx
        {
            get
            {
                return new Vector2D(m0x0, m1x0);
            }

            set
            {
                m0x0 = value.I;
                m1x0 = value.J;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Vector2D Cy
        {
            get
            {
                return new Vector2D(m0x1, m1x1);
            }

            set
            {
                m0x1 = value.I;
                m1x1 = value.J;
            }
        }

        /// <summary>
        /// The X Row or row zero.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Description("The First row of the Matrix2x2")]
        public Vector2D Rx
        {
            get
            {
                return new Vector2D(m0x0, m0x1);
            }

            set
            {
                m0x0 = value.I;
                m0x1 = value.J;
            }
        }

        /// <summary>
        /// The Y Row or row one.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Description("The Second row of the Matrix2x2")]
        public Vector2D Ry
        {
            get
            {
                return new Vector2D(m1x0, m1x1);
            }

            set
            {
                m1x0 = value.I;
                m1x1 = value.J;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public double Determinant
            => Determinant(m0x0, m0x1, m1x0, m1x1);

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <returns>A transposed Matrix.</returns>
        [XmlIgnore, SoapIgnore]
        public Matrix2x2D Transposed
            => Primitives.Transpose(this);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Matrix2x2D Adjoint
            => Primitives.Adjoint(this);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Matrix2x2D Cofactor
            => Primitives.Cofactor(this);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Matrix2x2D Inverted
            => Primitives.Invert(this);

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public bool IsIdentity
            => (Abs(m0x0 - 1) < Epsilon
                && Abs(m0x1) < Epsilon
                && Abs(m1x0) < Epsilon
                && Abs(m1x1 - 1) < Epsilon);

        #endregion

        #region Operators

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix2x2D operator +(Matrix2x2D left, Matrix2x2D right)
            => left.Add(right);

        /// <summary>
        /// Negates all the items in the Matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix2x2D operator -(Matrix2x2D matrix)
            => matrix.Negate();

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix2x2D operator -(Matrix2x2D left, Matrix2x2D right)
            => left.Subtract(right);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix2x2D operator *(Matrix2x2D matrix, double scalar)
            => matrix.Scale(scalar);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix2x2D operator *(double scalar, Matrix2x2D matrix)
            => matrix.Scale(scalar);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix2x2D operator *(Matrix2x2D left, Matrix2x2D right)
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
        public static bool operator ==(Matrix2x2D matrix1, Matrix2x2D matrix2)
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
        public static bool operator !=(Matrix2x2D matrix1, Matrix2x2D matrix2)
            => !Equals(matrix1, matrix2);

        /// <summary>
        /// Tupple to <see cref="Matrix2x2D"/>.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Matrix2x2D((double, double, double, double) tuple)
            => new Matrix2x2D(tuple);

        #endregion

        #region Factories

        /// <summary>
        /// 
        /// </summary>
        /// <param name="radianAngle"></param>
        /// <returns></returns>
        public static Matrix2x2D FromRotation(double radianAngle)
        {
            double sin = Sin(radianAngle);
            double cos = Cos(radianAngle);
            return new Matrix2x2D(cos, sin, -sin, cos);
        }

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Matrix2x2D FromScale(Vector2D scale)
            => new Matrix2x2D(scale.I, 0, 0, scale.J);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        public static Matrix2x2D FromScale(double scaleX, double scaleY)
            => new Matrix2x2D(scaleX, 0, 0, scaleY);

        /// <summary>
        /// Creates a skew transform
        /// </summary>
        /// <param name='skewX'>The skew angle in the x dimension in degrees</param>
        /// <param name='skewY'>The skew angle in the y dimension in degrees</param>
        public static Matrix2x2D FromSkewRadians(double skewX, double skewY)
            => new Matrix2x2D(1.0f, Tan(skewY), Tan(skewX), 1.0f);

        /// <summary>
        /// Parse a string for a <see cref="Matrix2x2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Matrix2x2D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix2x2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [ParseMethod]
        public static Matrix2x2D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Matrix2x3D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Matrix2x3D"/> data </param>
        /// <param name="provider"></param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix2x3D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Matrix2x2D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            Matrix2x2D value;
            string firstToken = tokenizer.NextTokenRequired();
            // The token will already have had whitespace trimmed so we can do a
            // simple string compare.
            if (firstToken == "Identity")
            {
                value = Identity;
            }
            else
            {
                value = new Matrix2x2D(
                    firstToken.ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider));
            }
            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
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
            ^ m1x0.GetHashCode()
            ^ m1x1.GetHashCode();

        /// <summary>
        /// Compares two Matrix2x3D
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Matrix2x2D a, Matrix2x2D b)
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
        public static bool Equals(Matrix2x2D matrix1, Matrix2x2D matrix2)
            => matrix1.M0x0.Equals(matrix2.M0x0)
                   && matrix1.M0x1.Equals(matrix2.M0x1)
                   && matrix1.M1x0.Equals(matrix2.M1x0)
                   && matrix1.M1x1.Equals(matrix2.M1x1);

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
            => obj is Matrix2x2D && Equals(this, (Matrix2x2D)obj);

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
        public bool Equals(Matrix2x2D value)
            => Equals(this, value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Matrix3x3D ToMatrix3x3D()
        {
            Matrix3x3D result = Matrix3x3D.Identity;
            result.M0x0 = m0x0; result.M0x1 = m0x1;
            result.M1x0 = m1x0; result.M1x1 = m1x1;
            return result;
        }

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix2x3D"/> struct based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix2x3D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix2x3D"/> struct based on the format string
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
        /// Creates a string representation of this <see cref="Matrix2x3D"/> struct based on the format string
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
            IFormattable formatable = $"{nameof(Matrix2x2D)}{{{nameof(M0x0)}={m0x0}{sep}{nameof(M0x1)}={m0x1}{sep}{nameof(M1x0)}={m1x0}{sep}{nameof(M1x1)}={m1x1}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
