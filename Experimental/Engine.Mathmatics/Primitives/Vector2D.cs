// <copyright file="Vector2D.cs" company="Shkyrockett" >
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
    /// The vector2d struct. Represents a vector in 2D coordinate space (double precision floating-point coordinates).
    /// </summary>
    [ComVisible(true)]
    [DataContract, Serializable]
    [TypeConverter(typeof(Vector2DConverter))]
    //[TypeConverter(typeof(StructConverter<Vector2D>))]
    [DebuggerDisplay("{nameof(Vector2D)}({nameof(I)}: {I ?? double.NaN}, {nameof(J)}: {J ?? double.NaN})")]
    public struct Vector2D
        : IVector<Vector2D>
    {
        #region Static Fields
        /// <summary>
        /// Represents a <see cref="Vector2D"/> that has <see cref="I"/>, and <see cref="J"/> values set to zero.
        /// </summary>
        public static readonly Vector2D Empty = new Vector2D(0d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector2D"/> that has <see cref="I"/>, and <see cref="J"/> values set to 1.
        /// </summary>
        public static readonly Vector2D Unit = new Vector2D(1d, 1d);

        /// <summary>
        /// Represents a <see cref="Vector2D"/> that has <see cref="I"/>, and <see cref="J"/> values set to NaN.
        /// </summary>
        public static readonly Vector2D NaN = new Vector2D(double.NaN, double.NaN);

        /// <summary>
        /// Represents a <see cref="Vector2D"/> that has <see cref="I"/> to 1, and <see cref="J"/> set to 0.
        /// </summary>
        public static readonly Vector2D XAxis = new Vector2D(1d, 0d);

        /// <summary>
        /// Represents a <see cref="Vector2D"/> that has <see cref="I"/> to 0, and <see cref="J"/> set to 1.
        /// </summary>
        public static readonly Vector2D YAxis = new Vector2D(0d, 1d);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="vector2D">A <see cref="Vector2D"/> class to clone.</param>
        public Vector2D(Vector2D vector2D)
            : this(vector2D.I, vector2D.J)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="i">The <see cref="I"/> component of the <see cref="Vector2D"/> class.</param>
        /// <param name="j">The <see cref="J"/> component of the <see cref="Vector2D"/> class.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D(double i, double j)
            : this()
        {
            I = i;
            J = j;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D((double X, double Y) tuple)
            : this()
        {
            (I, J) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D((double X, double Y) a, (double X, double Y) b)
            : this(a.X, a.Y, b.X, b.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D(Vector2D a, Vector2D b) :
            this(a.I, a.J, b.I, b.J)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="aI">The aI.</param>
        /// <param name="aJ">The aJ.</param>
        /// <param name="bI">The bI.</param>
        /// <param name="bJ">The bJ.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D(double aI, double aJ, double bI, double bJ)
            : this()
        {
            (var i, var j) = (bI - aI, bJ - aJ);
            var d = Sqrt((i * i) + (j * j));
            if (d is 0d)
            {
                // ToDo: Figure out what to do when d is 0;
                (I, J) = (i * 1d / d, j * 1d / d);
            }
            else
            {
                (I, J) = (i * 1d / d, j * 1d / d);
            }
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Vector2D"/> to a <see cref="ValueTuple{T1, T2}"/>.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double i, out double j)
        {
            i = I;
            j = J;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the I or first component of a 2D Vector.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double I { get; set; }

        /// <summary>
        /// Gets or sets the j or second Component of a 2D Vector.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double J { get; set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Vector2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(Vector2D value) => Operations.UnaryAdd2D(value.I, value.J);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(Vector2D value, double addend) => Operations.Add2D(value.I, value.J, addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(double value, Vector2D addend) => Operations.Add2D(addend.I, addend.J, value);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(Vector2D value, Vector2D addend) => Operations.Add2D(value.I, value.J, addend.I, addend.J);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Vector2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(Vector2D value) => Operations.UnaryNegate2D(value.I, value.J);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(Vector2D value, double subend) => Operations.SubtractSubtrahend2D(value.I, value.J, subend);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(double value, Vector2D subend) => Operations.SubtractFromMinuend2D(value, subend.I, subend.J);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(Vector2D value, Vector2D subend) => Operations.Subtract2D(value.I, value.J, subend.I, subend.J);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator *(Vector2D value, double factor) => Operations.Scale2D(value.I, value.J, factor);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="factor">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator *(double factor, Vector2D value) => Operations.Scale2D(value.I, value.J, factor);

        /// <summary>
        /// Divide a Vector2D
        /// </summary>
        /// <param name="divisor">The Vector2D</param>
        /// <param name="dividend">The dividend</param>
        /// <returns>A Vector2D divided by the divisor</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator /(Vector2D divisor, double dividend) => Operations.DivideByDividend2D(divisor.I, divisor.J, dividend);

        /// <summary>
        /// Divide a Vector2D
        /// </summary>
        /// <param name="divisor">The Vector2D</param>
        /// <param name="dividend">The divisor</param>
        /// <returns>A Vector2D divided by the divisor</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator /(double divisor, Vector2D dividend) => Operations.DivideDivisor2D(divisor, dividend.I, dividend.I);

        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector2D a, Vector2D b) => Equals(a, b);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector2D a, Vector2D b) => !Equals(a, b);

        /// <summary>
        /// Converts the specified <see cref="Vector2D"/> structure to a <see cref="ValueTuple{T1, T2}"/> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2D"/> to be converted.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double I, double J)(Vector2D vector) => (vector.I, vector.J);

        /// <summary>
        /// Tuple to <see cref="Vector2D"/>.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2D((double X, double Y) tuple) => new Vector2D(tuple);
        #endregion Operators

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Vector2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Vector2D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [ParseMethod]
        public static Vector2D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Vector2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Vector2D"/> data </param>
        /// <param name="provider"></param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Vector2D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            var firstToken = tokenizer.NextTokenRequired();

            var value = new Vector2D(
                Convert.ToDouble(firstToken, provider),
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
        public override int GetHashCode() => HashCode.Combine(I, J);
		
        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => base.Equals(obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector2D other) => I == other.I && J == other.J;


        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Vector2D"/>.
        /// </summary>
        /// <returns>A string representation of this <see cref="Vector2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => base.ToString();

        /// <summary>
        /// Creates a string representation of this <see cref="Vector2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The <see cref="CultureInfo"/> provider.</param>
        /// <returns>A string representation of this <see cref="Vector2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Vector2D);
            var s = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Vector2D)}({nameof(I)}: {I.ToString(format, formatProvider)}{s} {nameof(J)}: {J.ToString(format, formatProvider)})";
        }
        #endregion Public Methods
    }
}
