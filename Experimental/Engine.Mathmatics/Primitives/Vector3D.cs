// <copyright file="Vector3D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;
using static Engine.Mathematics;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The vector3d struct. Represents a vector in 3D coordinate space (double precision floating-point coordinates).
    /// </summary>
    [ComVisible(true)]
    [DataContract, Serializable]
    [TypeConverter(typeof(Vector3DConverter))]
    //[TypeConverter(typeof(StructConverter<Vector3D>))]
    [DebuggerDisplay("{nameof(Vector3D)}({nameof(I)}: {I ?? double.NaN}, {nameof(J)}: {J ?? double.NaN}, {nameof(K)}: {K ?? double.NaN})")]
    public struct Vector3D
        : IVector<Vector3D>
    {
        #region Static Fields
        /// <summary>
        /// Represents a <see cref="Vector3D"/> that has <see cref="I"/>, <see cref="J"/>, and <see cref="K"/> values set to zero.
        /// </summary>
        public static readonly Vector3D Empty = new Vector3D(0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector3D"/> that has <see cref="I"/>, <see cref="J"/>, and <see cref="K"/> values set to 1.
        /// </summary>
        public static readonly Vector3D Unit = new Vector3D(1d, 1d, 1d);

        /// <summary>
        /// Represents a <see cref="Vector3D"/> that has <see cref="I"/>, <see cref="J"/>, and <see cref="K"/> values set to NaN.
        /// </summary>
        public static readonly Vector3D NaN = new Vector3D(double.NaN, double.NaN, double.NaN);

        /// <summary>
        /// Represents a <see cref="Vector3D"/> that has <see cref="I"/> set to 1, <see cref="J"/> set to 0, and <see cref="K"/> set to 0.
        /// </summary>
        public static readonly Vector3D XAxis = new Vector3D(1d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector3D"/> that has <see cref="I"/> set to 0, <see cref="J"/> set to 1, and <see cref="K"/> set to 0.
        /// </summary>
        public static readonly Vector3D YAxis = new Vector3D(0d, 1d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector3D"/> that has <see cref="I"/> set to 0, <see cref="J"/> set to 0, and <see cref="K"/> set to 1.
        /// </summary>
        public static readonly Vector3D ZAxis = new Vector3D(0d, 0d, 1d);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="vector3D">A <see cref="Vector3D"/> class to clone.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3D(Vector3D vector3D)
            : this(vector3D.I, vector3D.J, vector3D.K)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="i">The <see cref="I"/> component of the <see cref="Vector3D"/> class.</param>
        /// <param name="j">The <see cref="J"/> component of the <see cref="Vector3D"/> class.</param>
        /// <param name="k">The <see cref="K"/> component of the <see cref="Vector3D"/> class.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3D(double i, double j, double k)
            : this()
        {
            I = i;
            J = j;
            K = k;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3D((double X, double Y, double Z) tuple)
            : this()
        {
            (I, J, K) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> struct.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3D((double X, double Y, double Z) a, (double X, double Y, double Z) b)
            : this(a.X, a.Y, a.Z, b.X, b.Y, b.Z)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3D(Vector3D a, Vector3D b) :
            this(a.I, a.J, a.K, b.I, b.J, b.K)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="aI">The aI.</param>
        /// <param name="aJ">The aJ.</param>
        /// <param name="aK">The aK.</param>
        /// <param name="bI">The bI.</param>
        /// <param name="bJ">The bJ.</param>
        /// <param name="bK">The bK.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3D(double aI, double aJ, double aK, double bI, double bJ, double bK)
            : this()
        {
            (var i, var j, var k) = (bI - aI, bJ - aJ, bK - aK);
            var d = Sqrt((i * i) + (j * j) + (k * k));
            I = i * 1d / d;
            J = j * 1d / d;
            K = k * 1d / d;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Vector3D"/> to a <see cref="ValueTuple{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double i, out double j, out double k)
        {
            i = I;
            j = J;
            k = K;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the I or first component of a 3D Vector
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double I { get; set; }

        /// <summary>
        /// Gets or sets the j or second component of a 3D Vector
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double J { get; set; }

        /// <summary>
        /// Gets or sets the k or third component of a 3D Vector
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double K { get; set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Vector3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator +(Vector3D value) => Operations.UnaryAdd3D(value.I, value.J, value.K);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator +(Vector3D value, double addend) => Operations.Add3D(value.I, value.J, value.K, addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator +(double value, Vector3D addend) => Operations.Add3D(addend.I, addend.J, addend.K, value);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator +(Vector3D value, Vector3D addend) => Operations.Add3D(value.I, value.J, value.K, addend.I, addend.J, addend.K);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Vector3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator -(Vector3D value) => Operations.UnaryNegate3D(value.I, value.J, value.K);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator -(Vector3D value, double subend) => Operations.SubtractSubtrahend3D(value.I, value.J, value.K, subend);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator -(double value, Vector3D subend) => Operations.SubtractFromMinuend3D(value, subend.I, subend.J, subend.K);

        /// <summary>
        /// Subtract Vectors
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator -(Vector3D value, Vector3D subend) => Operations.Subtract3D(value.I, value.J, value.K, subend.I, subend.J, subend.K);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Vector Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator *(Vector3D value, double factor) => Operations.Scale3D(value.I, value.J, value.K, factor);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="factor">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Vector Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator *(double factor, Vector3D value) => Operations.Scale3D(value.I, value.J, value.K, factor);

        /// <summary>
        /// Divide a <see cref="Vector3D"/>
        /// </summary>
        /// <param name="divisor">The <see cref="Vector3D"/></param>
        /// <param name="divedend">The divisor</param>
        /// <returns>A <see cref="Vector3D"/> divided by the divisor</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator /(Vector3D divisor, double dividend) => Operations.DivideByDividend3D(divisor.I, divisor.J, divisor.K, dividend);

        /// <summary>
        /// Divide a <see cref="Vector3D"/>
        /// </summary>
        /// <param name="divisor">The <see cref="Vector3D"/></param>
        /// <param name="dividend">The divisor</param>
        /// <returns>A <see cref="Vector3D"/> divided by the divisor</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator /(double divisor, Vector3D dividend) => Operations.DivideDivisor3D(divisor, dividend.I, dividend.I, dividend.K);

        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector3D a, Vector3D b) => Equals(a, b);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector3D a, Vector3D b) => !Equals(a, b);

        /// <summary>
        /// Converts the specified <see cref="Vector3D"/> structure to a <see cref="ValueTuple{T1, T2, T3}"/> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3D"/> to be converted.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double I, double J, double K) (Vector3D vector) => (vector.I, vector.J, vector.K);

        /// <summary>
        /// Point to Vector3D
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3D((double X, double Y, double Z) tuple) => new Vector3D(tuple);
        #endregion Operators

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Vector3D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Vector3D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector3D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [ParseMethod]
        public static Vector3D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Vector3D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Vector3D"/> data </param>
        /// <param name="provider"></param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector3D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Vector3D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            var firstToken = tokenizer.NextTokenRequired();

            var value = new Vector3D(
                Convert.ToDouble(firstToken, provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider)
                );

            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }
        #endregion Factories

        #region Public Methods
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed <see cref="int"/> hash code.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(I, J, K);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3D other) => I == other.I && J == other.J && K == other.K;

        public override bool Equals(object obj) => base.Equals(obj);

        public override string ToString() => base.ToString();

        /// <summary>
        /// Creates a string representation of this <see cref="Vector3D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The <see cref="CultureInfo"/> provider.</param>
        /// <returns>A string representation of this <see cref="Vector3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Vector3D);
            var s = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Vector3D)}({nameof(I)}: {I.ToString(format, formatProvider)}{s} {nameof(J)}: {J.ToString(format, formatProvider)}{s} {nameof(K)}: {K.ToString(format, formatProvider)})";
        }
        #endregion Public Methods
    }
}
