// <copyright file="Vector4D.cs" company="Shkyrockett" >
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
using static Engine.Mathematics;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The <see cref="Vector4D" /> struct. Represents a vector in 4D coordinate space (double precision floating-point coordinates).
    /// </summary>
    /// <seealso cref="IVector{T}" />
    [ComVisible(true)]
    [DataContract, Serializable]
    //[TypeConverter(typeof(Vector4DConverter))]
    [TypeConverter(typeof(StructConverter<Vector4D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Vector4D
        : IVector<Vector4D>
    {
        #region Static Fields
        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" />, <see cref="J" />, <see cref="K" />, and <see cref="L" /> values set to zero.
        /// </summary>
        public static readonly Vector4D Empty = new Vector4D(0d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" />, <see cref="J" />, <see cref="K" />, and <see cref="L" /> values set to 1.
        /// </summary>
        public static readonly Vector4D Unit = new Vector4D(1d, 1d, 1d, 1d);

        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" />, <see cref="J" />, <see cref="K" />, and <see cref="L" /> values set to NaN.
        /// </summary>
        public static readonly Vector4D NaN = new Vector4D(double.NaN, double.NaN, double.NaN, double.NaN);

        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" /> set to 1, <see cref="J" /> set to 0, <see cref="K" /> set to 0, and <see cref="L" /> set to 0.
        /// </summary>
        public static readonly Vector4D XAxis = new Vector4D(1d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" /> set to 0, <see cref="J" /> set to 1, <see cref="K" /> set to 0, and <see cref="L" /> set to 0.
        /// </summary>
        public static readonly Vector4D YAxis = new Vector4D(0d, 1d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" /> set to 0, <see cref="J" /> set to 0, <see cref="K" /> set to 1, and <see cref="L" /> set to 0.
        /// </summary>
        public static readonly Vector4D ZAxis = new Vector4D(0d, 0d, 1d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector4D" /> that has <see cref="I" /> set to 0, <see cref="J" /> set to 0, <see cref="K" /> set to 0, and <see cref="L" /> set to 1.
        /// </summary>
        public static readonly Vector4D WAxis = new Vector4D(0d, 0d, 0d, 1d);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> class as a copy of the one provided.
        /// </summary>
        /// <param name="vector4D">A <see cref="Vector4D" /> class to clone.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D(Vector4D vector4D)
            : this(vector4D.I, vector4D.J, vector4D.K, vector4D.L)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> class.
        /// </summary>
        /// <param name="i">The <see cref="I" /> component of the <see cref="Vector4D" /> class.</param>
        /// <param name="j">The <see cref="J" /> component of the <see cref="Vector4D" /> class.</param>
        /// <param name="k">The <see cref="K" /> component of the <see cref="Vector4D" /> class.</param>
        /// <param name="l">The <see cref="L" /> component of the <see cref="Vector4D" /> class.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D(double i, double j, double k, double l)
            : this()
        {
            I = i;
            J = j;
            K = k;
            L = l;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> class.
        /// </summary>
        /// <param name="tuple">The X, Y, Z and W values in tuple form.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D((double X, double Y, double Z, double W) tuple)
            : this()
        {
            (I, J, K, L) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> struct.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D((double X, double Y, double Z, double W) a, (double X, double Y, double Z, double W) b)
            : this(a.X, a.Y, a.Z, a.W, b.X, b.Y, b.Z, b.W)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> class.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D(Vector4D a, Vector4D b)
            : this(a.I, a.J, a.K, a.L, b.I, b.J, b.K, b.L)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D" /> class.
        /// </summary>
        /// <param name="aI">The aI.</param>
        /// <param name="aJ">The aJ.</param>
        /// <param name="aK">The aK.</param>
        /// <param name="aL">The aL.</param>
        /// <param name="bI">The bI.</param>
        /// <param name="bJ">The bJ.</param>
        /// <param name="bK">The bK.</param>
        /// <param name="bL">The bL.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D(double aI, double aJ, double aK, double aL, double bI, double bJ, double bK, double bL)
            : this()
        {
            // This creates a normalized vector. It is debatable that it is what we actually want. We may only want the first line.

            // Find the new vector.
            (var i, var j, var k, var l) = (bI - aI, bJ - aJ, bK - aK, bL - aL);

            // Get the length of the vector.
            var d = Sqrt((i * i) + (j * j) + (k * k) + (l * l));

            // Calculate the normalized vector.
            (I, J, K, L) = d == 0 ? (i, j, k, l) : (i * 1d / d, j * 1d / d, k * 1d / d, l * 1d / d);
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Vector4D" /> to a <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double i, out double j, out double k, out double l)
        {
            i = I;
            j = J;
            k = K;
            l = L;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the I or first Component of a 4D Vector
        /// </summary>
        /// <value>
        /// The i.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double I { get; set; }

        /// <summary>
        /// Gets or sets the j or second Component of a 4D Vector
        /// </summary>
        /// <value>
        /// The j.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double J { get; set; }

        /// <summary>
        /// Gets or sets the k or third Component of a 4D Vector
        /// </summary>
        /// <value>
        /// The k.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double K { get; set; }

        /// <summary>
        /// Gets or sets the l or fourth Component of a 4D Vector
        /// </summary>
        /// <value>
        /// The l.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double L { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Vector4D" /> is empty.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if this instance is empty; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public bool IsEmpty
            => Abs(I) < Epsilon
            && Abs(J) < Epsilon
            && Abs(K) < Epsilon
            && Abs(L) < Epsilon;

        /// <summary>
        /// Gets the magnitude.
        /// </summary>
        /// <value>
        /// The magnitude.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double Magnitude
            => Measurements.VectorMagnitude(I, J, K, L);

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double Length
            => Measurements.VectorMagnitude(I, J, K, L);

        /// <summary>
        /// Gets the length squared.
        /// </summary>
        /// <value>
        /// The length squared.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double LengthSquared
            => Measurements.VectorMagnitudeSquared(I, J, K, L);
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Vector4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(Vector4D value) => UnaryAdd4D(value.I, value.J, value.K, value.L);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(Vector4D value, double addend) => Add4D(value.I, value.J, value.K, value.L, addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(double value, Vector4D addend) => Add4D(addend.I, addend.J, addend.K, addend.L, value);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(Vector4D value, Vector4D addend) => Add4D(value.I, value.J, value.K, value.L, addend.I, addend.J, addend.K, addend.L);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Vector4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Vector4D value) => UnaryNegate4D(value.I, value.J, value.K, value.L);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Vector4D value, double subend) => SubtractSubtrahend4D(value.I, value.J, value.K, value.L, subend);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(double value, Vector4D subend) => SubtractFromMinuend4D(value, subend.I, subend.J, subend.K, subend.L);

        /// <summary>
        /// Subtract Vectors
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Vector4D value, Vector4D subend) => Subtract4D(value.I, value.J, value.K, value.L, subend.I, subend.J, subend.K, subend.L);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>
        /// A Vector Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator *(Vector4D value, double factor) => Scale4D(value.I, value.J, value.K, value.L, factor);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="factor">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>
        /// A Vector Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator *(double factor, Vector4D value) => Scale4D(value.I, value.J, value.K, value.L, factor);

        /// <summary>
        /// Divide a <see cref="Vector4D" />
        /// </summary>
        /// <param name="divisor">The <see cref="Vector4D" /></param>
        /// <param name="dividend">The dividend.</param>
        /// <returns>
        /// A <see cref="Vector4D" /> divided by the divisor
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator /(Vector4D divisor, double dividend) => DivideByDividend4D(divisor.I, divisor.J, divisor.K, divisor.L, dividend);

        /// <summary>
        /// Divide a <see cref="Vector4D" />
        /// </summary>
        /// <param name="divisor">The <see cref="Vector4D" /></param>
        /// <param name="dividend">The divisor</param>
        /// <returns>
        /// A <see cref="Vector4D" /> divided by the divisor
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator /(double divisor, Vector4D dividend) => DivideDivisor4D(divisor, dividend.I, dividend.I, dividend.K, dividend.L);

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
        public static bool operator ==(Vector4D a, Vector4D b) => Equals(a, b);

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
        public static bool operator !=(Vector4D a, Vector4D b) => !Equals(a, b);

        /// <summary>
        /// Converts the specified <see cref="Vector4D" /> structure to a <see cref="ValueTuple{T1, T2, T3, T4}" /> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector4D" /> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double I, double J, double K, double L)(Vector4D vector) => (vector.I, vector.J, vector.K, vector.L);

        /// <summary>
        /// Tuple to Vector4D
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector4D((double X, double Y, double Z, double W) value) => new Vector4D(value);
        #endregion Operators

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Vector4D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Vector4D" /> data</param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector4D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [ParseMethod]
        public static Vector4D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Vector4D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Vector4D" /> data</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector4D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        public static Vector4D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            var firstToken = tokenizer.NextTokenRequired();

            var value = new Vector4D(
                Convert.ToDouble(firstToken, provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
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
        /// Get the hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => I.GetHashCode() ^ J.GetHashCode() ^ K.GetHashCode() ^ L.GetHashCode();

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Vector4D && Equals(this, (Vector4D)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Vector4D a, Vector4D b) => (a.I == b.I) & (a.J == b.J) & (a.K == b.K) & (a.L == b.L);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector4D other) => Equals(this, other);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Vector4D" /> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Vector4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector4D" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">The <see cref="CultureInfo" /> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Vector4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider) => ToString("R" /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector4D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The <see cref="CultureInfo" /> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Vector4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Vector4D);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Vector4D)}({nameof(I)}: {I.ToString(format, provider)}{s} {nameof(J)}: {J.ToString(format, provider)}{s} {nameof(K)}: {K.ToString(format, provider)}{s} {nameof(L)}: {L.ToString(format, provider)})";
        }

        /// <summary>
        /// Pluses the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static Vector4D Plus(Vector4D item) => +item;

        /// <summary>
        /// Adds the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector4D Add(Vector4D left, Vector4D right) => left + right;

        /// <summary>
        /// Adds the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector4D Add(double left, Vector4D right) => left + right;

        /// <summary>
        /// Adds the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector4D Add(Vector4D left, double right) => left + right;

        /// <summary>
        /// Negates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static Vector4D Negate(Vector4D item) => -item;

        /// <summary>
        /// Subtracts the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector4D Subtract(Vector4D left, Vector4D right) => left - right;

        /// <summary>
        /// Subtracts the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector4D Subtract(double left, Vector4D right) => left - right;

        /// <summary>
        /// Subtracts the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector4D Subtract(Vector4D left, double right) => left - right;

        /// <summary>
        /// Multiplies the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector4D Multiply(double left, Vector4D right) => left * right;

        /// <summary>
        /// Multiplies the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector4D Multiply(Vector4D left, double right) => left * right;

        /// <summary>
        /// Divides the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector4D Divide(double left, Vector4D right) => left / right;

        /// <summary>
        /// Divides the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector4D Divide(Vector4D left, double right) => left / right;

        /// <summary>
        /// To this instance.
        /// </summary>
        /// <returns></returns>
        public (double I, double J, double K, double L) To() => (I, J, K, L);
        #endregion Public Methods
    }
}
