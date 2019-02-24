// <copyright file="Matrix2x2D.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
    /// The <see cref="Matrix2x2D"/> struct.
    /// </summary>
    [DataContract, Serializable]
    [ComVisible(true)]
    [TypeConverter(typeof(StructConverter<Matrix2x2D>))]
    public partial struct Matrix2x2D
        : IMatrix<Matrix2x2D, Vector2D>
    {
        #region Static Fields
        /// <summary>
        /// An Empty <see cref="Matrix2x2D"/>.
        /// </summary>
        public static readonly Matrix2x2D Empty = new Matrix2x2D(
            0d, 0d,
            0d, 0d);

        /// <summary>
        /// An Identity <see cref="Matrix2x2D"/>.
        /// </summary>
        public static readonly Matrix2x2D Identity = new Matrix2x2D(
            1d, 0d,
            0d, 1d);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix2x2D"/> class.
        /// </summary>
        /// <param name="m0x0"></param>
        /// <param name="m0x1"></param>
        /// <param name="m1x0"></param>
        /// <param name="m1x1"></param>
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
        /// <param name="xAxis"></param>
        /// <param name="yAxis"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix2x2D(Vector2D xAxis, Vector2D yAxis)
            : this(xAxis.I, xAxis.J, yAxis.I, yAxis.J)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix2x2D"/> class.
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

        /// <summary>
        /// Deconstruct this <see cref="Matrix2x2D"/> to a <see cref="ValueTuple{T1, T2, T3, T4}"/>.
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

        #region Properties
        /// <summary>
        /// Gets or sets the m0x0.
        /// </summary>
        public double M0x0 { get; set; }

        /// <summary>
        /// Gets or sets the m0x1.
        /// </summary>
        public double M0x1 { get; set; }

        /// <summary>
        /// Gets or sets the m1x0.
        /// </summary>
        public double M1x0 { get; set; }

        /// <summary>
        /// Gets or sets the m1x1.
        /// </summary>
        public double M1x1 { get; set; }

        /// <summary>
        /// Gets or sets the cx.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The First column of the " + nameof(Matrix2x2D))]
        public Vector2D Cx { get { return new Vector2D(M0x0, M1x0); } set { (M0x0, M1x0) = value; } }

        /// <summary>
        /// Gets or sets the cy.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Second column of the " + nameof(Matrix2x2D))]
        public Vector2D Cy { get { return new Vector2D(M0x1, M1x1); } set { (M0x1, M1x1) = value; } }

        /// <summary>
        /// Gets or sets the rx.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The First row of the " + nameof(Matrix2x2D))]
        public Vector2D Rx { get { return new Vector2D(M0x0, M0x1); } set { (M0x0, M0x1) = value; } }

        /// <summary>
        /// Gets or sets the ry.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Description("The Second row of the " + nameof(Matrix2x2D))]
        public Vector2D Ry { get { return new Vector2D(M1x0, M1x1); } set { (M1x0, M1x1) = value; } }
        /// <summary>
        /// Gets the determinant.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Determinant => Determinant(M0x0, M0x1, M1x0, M1x1);

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <returns>A transposed Matrix.</returns>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix2x2D Transposed => Primitives.Transpose(this);

        /// <summary>
        /// Gets the adjoint.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix2x2D Adjoint => Primitives.Adjoint(this);

        /// <summary>
        /// Gets the cofactor.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix2x2D Cofactor => Primitives.Cofactor(this);

        /// <summary>
        /// Gets the inverted.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Matrix2x2D Inverted => Primitives.Invert(this);

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsIdentity => Abs(M0x0 - 1) < Epsilon && Abs(M0x1) < Epsilon && Abs(M1x0) < Epsilon && Abs(M1x1 - 1) < Epsilon;
        #endregion Properties

        #region Operators
        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix2x2D operator +(Matrix2x2D left, Matrix2x2D right) => left.Add(right);

        /// <summary>
        /// Negates all the items in the Matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix2x2D operator -(Matrix2x2D matrix) => matrix.Negate();

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix2x2D operator -(Matrix2x2D left, Matrix2x2D right) => left.Subtract(right);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix2x2D operator *(Matrix2x2D matrix, double scalar) => matrix.Scale(scalar);

        /// <summary>
        /// Multiplies all the items in the Matrix3 by a scalar value.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix2x2D operator *(double scalar, Matrix2x2D matrix) => matrix.Scale(scalar);

        /// <summary>
        /// Multiply (concatenate) two Matrix3 instances together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Matrix2x2D operator *(Matrix2x2D left, Matrix2x2D right) => left.Multiply(right);

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
        public static bool operator ==(Matrix2x2D matrix1, Matrix2x2D matrix2) => Equals(matrix1, matrix2);

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
        public static bool operator !=(Matrix2x2D matrix1, Matrix2x2D matrix2) => !Equals(matrix1, matrix2);

        /// <summary>
        /// Tupple to <see cref="Matrix2x2D"/>.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static implicit operator Matrix2x2D((double, double, double, double) tuple) => new Matrix2x2D(tuple);
        #endregion Operators

        #region Factories
        /// <summary>
        /// The from rotation.
        /// </summary>
        /// <param name="radianAngle">The radianAngle.</param>
        /// <returns>The <see cref="Matrix2x2D"/>.</returns>
        public static Matrix2x2D FromRotation(double radianAngle)
        {
            var sin = Sin(radianAngle);
            var cos = Cos(radianAngle);
            return new Matrix2x2D(cos, sin, -sin, cos);
        }

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Matrix2x2D FromScale(Vector2D scale) => new Matrix2x2D(scale.I, 0, 0, scale.J);

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        public static Matrix2x2D FromScale(double scaleX, double scaleY) => new Matrix2x2D(scaleX, 0, 0, scaleY);

        /// <summary>
        /// Creates a skew transform
        /// </summary>
        /// <param name='skewX'>The skew angle in the x dimension in degrees</param>
        /// <param name='skewY'>The skew angle in the y dimension in degrees</param>
        public static Matrix2x2D FromSkewRadians(double skewX, double skewY) => new Matrix2x2D(1d, Tan(skewY), Tan(skewX), 1.0f);

        /// <summary>
        /// Parse a string for a <see cref="Matrix2x2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Matrix2x2D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix2x2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [ParseMethod]
        public static Matrix2x2D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Matrix3x2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Matrix3x2D"/> data </param>
        /// <param name="provider"></param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix3x2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Matrix2x2D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            Matrix2x2D value;
            var firstToken = tokenizer.NextTokenRequired();
            // The token will already have had whitespace trimmed so we can do a
            // simple string compare.
            value = firstToken == nameof(Identity) ? Identity : new Matrix2x2D(
                    firstToken.ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider),
                    tokenizer.NextTokenRequired().ParseFloat(provider));
            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }
        #endregion Factories

        #region Methods
        /// <summary>
        /// Returns the HashCode for this <see cref="Matrix2x2D"/>
        /// </summary>
        /// <returns>
        /// The <see cref="int"/> HashCode for this <see cref="Matrix2x2D"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => M0x0.GetHashCode() ^ M0x1.GetHashCode() ^ M1x0.GetHashCode() ^ M1x1.GetHashCode();

        /// <summary>
        /// Compares two Matrix2x3D
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Matrix2x2D a, Matrix2x2D b) => Equals(a, b);

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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Matrix2x2D && Equals(this, (Matrix2x2D)obj);

        /// <summary>
        /// Equals - compares this <see cref="Matrix2x2D"/> with the passed in object.  In this equality
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
        public bool Equals(Matrix2x2D value) => Equals(this, value);

        /// <summary>
        /// The to matrix3x3d.
        /// </summary>
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
        public Matrix3x3D ToMatrix3x3D()
        {
            var result = Matrix3x3D.Identity;
            result.M0x0 = M0x0; result.M0x1 = M0x1;
            result.M1x0 = M1x0; result.M1x1 = M1x1;
            return result;
        }

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix2x2D"/> struct based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix2x2D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider) => ToString("R" /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix2x2D"/> struct based on the format string
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
        private string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Matrix4x4D);
            if (IsIdentity)return nameof(Identity);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Matrix2x2D)}=[{nameof(M0x0)}:{M0x0.ToString(format, provider)}{s} {nameof(M0x1)}:{M0x1.ToString(format, provider)}{s} {nameof(M1x0)}:{M1x0.ToString(format, provider)}{s} {nameof(M1x1)}:{M1x1.ToString(format, provider)}]";
        }

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="T:IEnumerator{IEnumerable{double}}"/>.</returns>
        public IEnumerator<IEnumerable<double>> GetEnumerator()
            => new List<List<double>>
            {
                new List<double> { M0x0, M0x1 },
                new List<double> { M1x0, M1x1 },
            }.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion Methods
    }
}
