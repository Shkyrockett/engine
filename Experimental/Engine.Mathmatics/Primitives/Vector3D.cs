// <copyright file="Vector3D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The vector3d struct. Represents a vector in 3D coordinate space (double precision floating-point coordinates).
    /// </summary>
    [DataContract, Serializable]
    [TypeConverter(typeof(Vector3DConverter))]
    [DebuggerDisplay("{ToString()}")]
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
        public static Vector3D operator +(Vector3D value) => Plus(value);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator +(Vector3D augend, double addend) => Add(augend, addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator +(double augend, Vector3D addend) => Add(augend, addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator +(Vector3D augend, Vector3D addend) => Add(augend, addend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Vector3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator -(Vector3D value) => Negate(value);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator -(Vector3D minuend, double subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator -(double minuend, Vector3D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract Vectors
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator -(Vector3D minuend, Vector3D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplicand">The Point</param>
        /// <param name="multiplier">The Multiplier</param>
        /// <returns>A Vector Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator *(Vector3D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="multiplicand">The Multiplier</param>
        /// <param name="multiplier">The Point</param>
        /// <returns>A Vector Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator *(double multiplicand, Vector3D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Divide a <see cref="Vector3D"/>
        /// </summary>
        /// <param name="dividend">The <see cref="Vector3D"/></param>
        /// <param name="divedend">The divisor</param>
        /// <returns>A <see cref="Vector3D"/> divided by the divisor</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator /(Vector3D dividend, double divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a <see cref="Vector3D"/>
        /// </summary>
        /// <param name="dividend">The <see cref="Vector3D"/></param>
        /// <param name="divisor">The divisor</param>
        /// <returns>A <see cref="Vector3D"/> divided by the divisor</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator /(double dividend, Vector3D divisor) => Divide(dividend, divisor);

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
        public static implicit operator (double I, double J, double K)(Vector3D vector) => (vector.I, vector.J, vector.K);

        /// <summary>
        /// Point to Vector3D
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3D((double X, double Y, double Z) tuple) => new Vector3D(tuple);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Plus(Vector3D value) => Operations.UnaryAdd(value.I, value.J, value.K);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Add(Vector3D augend, double addend) => Operations.AddVectorUniform(augend.I, augend.J, augend.K, addend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Add(double augend, Vector3D addend) => Operations.AddVectorUniform(addend.I, addend.J, addend.K, augend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Add(Vector3D augend, Vector3D addend) => Operations.AddVectors(augend.I, augend.J, augend.K, addend.I, addend.J, addend.K);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Negate(Vector3D value) => Operations.NegateVector(value.I, value.J, value.K);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Subtract(Vector3D minuend, double subend) => Operations.SubtractVectorUniform(minuend.I, minuend.J, minuend.K, subend);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Subtract(double minuend, Vector3D subend) => Operations.SubtractFromMinuend(minuend, subend.I, subend.J, subend.K);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Subtract(Vector3D minuend, Vector3D subend) => Operations.SubtractVector(minuend.I, minuend.J, minuend.K, subend.I, subend.J, subend.K);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Multiply(Vector3D multiplicand, double multiplier) => Operations.ScaleVector(multiplicand.I, multiplicand.J, multiplicand.K, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Multiply(double multiplicand, Vector3D multiplier) => Operations.ScaleVector(multiplier.I, multiplier.J, multiplier.K, multiplicand);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Divide(Vector3D dividend, double divisor) => Operations.DivideVectorUniform(dividend.I, dividend.J, dividend.K, divisor);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D Divide(double dividend, Vector3D divisor) => Operations.DivideByVectorUniform(dividend, divisor.I, divisor.I, divisor.K);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Vector3D d && Equals(d);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3D other) => I == other.I && J == other.J && K == other.K;

        /// <summary>
        /// Converts to valuetuple.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double I, double J, double K) ToValueTuple() => (I, J, K);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D FromValueTuple((double X, double Y, double Z) tuple) => new Vector3D(tuple);

        /// <summary>
        /// Converts to vector3d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3D ToVector3D() => new Vector3D(I, J, K);
        #endregion

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

        #region Standard Methods
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed <see cref="int" /> hash code.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(I, J, K);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector3D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The <see cref="CultureInfo" /> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Vector3D" />.
        /// </returns>
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
