// <copyright file="Vector4D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
using System.Xml.Serialization;
using static System.Math;
using static Engine.Maths;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// Represents a vector in 4D coordinate space (double precision floating-point coordinates).
    /// </summary>
    [DataContract, Serializable]
    [ComVisible(true)]
    [TypeConverter(typeof(StructConverter<Vector4D>))]
    //[DebuggerDisplay("I: {I}, J: {J}, K: {K}, L: {L}")]
    public struct Vector4D
        : IVector<Vector4D>
    {
        #region Static Fields
        /// <summary>
        /// An Empty <see cref="Vector4D"/>.
        /// </summary>
        public static readonly Vector4D Empty = new Vector4D(0, 0, 0, 0);

        /// <summary>
        /// A Unit <see cref="Vector4D"/>.
        /// </summary>
        public static readonly Vector4D Unit = new Vector4D(1, 1, 1, 1);

        /// <summary>
        /// The x axis (readonly). Value: new Vector4D(1, 0, 0, 0).
        /// </summary>
        public static readonly Vector4D XAxis = new Vector4D(1, 0, 0, 0);

        /// <summary>
        /// The y axis (readonly). Value: new Vector4D(0, 1, 0, 0).
        /// </summary>
        public static readonly Vector4D YAxis = new Vector4D(0, 1, 0, 0);

        /// <summary>
        /// The z axis (readonly). Value: new Vector4D(0, 0, 1, 0).
        /// </summary>
        public static readonly Vector4D ZAxis = new Vector4D(0, 0, 1, 0);

        /// <summary>
        /// The w axis (readonly). Value: new Vector4D(0, 0, 0, 1).
        /// </summary>
        public static readonly Vector4D WAxis = new Vector4D(0, 0, 0, 1);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D"/> class as a copy of the one provided.
        /// </summary>
        /// <param name="vector4D">A <see cref="Vector4D"/> class to clone.</param>
        public Vector4D(Vector4D vector4D)
            : this(vector4D.I, vector4D.J, vector4D.K, vector4D.L)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D"/> class.
        /// </summary>
        /// <param name="tuple">The X, Y, Z and W values in tupple form.</param>
        public Vector4D((double X, double Y, double Z, double W) tuple)
        {
            (I, J, K, L) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D"/> class.
        /// </summary>
        /// <param name="i">The <see cref="I"/> component of the <see cref="Vector4D"/> class.</param>
        /// <param name="j">The <see cref="J"/> component of the <see cref="Vector4D"/> class.</param>
        /// <param name="k">The <see cref="K"/> component of the <see cref="Vector4D"/> class.</param>
        /// <param name="l">The <see cref="L"/> component of the <see cref="Vector4D"/> class.</param>
        public Vector4D(double i, double j, double k, double l)
        {
            I = i;
            J = j;
            K = k;
            L = l;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D"/> class.
        /// </summary>
        /// <param name="aI"></param>
        /// <param name="aJ"></param>
        /// <param name="aK"></param>
        /// <param name="aL"></param>
        /// <param name="bI"></param>
        /// <param name="bJ"></param>
        /// <param name="bK"></param>
        /// <param name="bL"></param>
        public Vector4D(double aI, double aJ, double aK, double aL, double bI, double bJ, double bK, double bL)
            : this(new Vector4D(aI, aJ, aK, aL), new Vector4D(bI, bJ, bK, bL))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public Vector4D(Vector4D a, Vector4D b)
            : this(new Vector4D(a.I - b.I, a.J - b.J, a.K - b.K, a.L - b.L).Unit())
        { }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// First Point of a 4D Vector
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double I { get; set; }

        /// <summary>
        /// Second Component of a 4D Vector
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double J { get; set; }

        /// <summary>
        /// Third Component of a 4D Vector
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double K { get; set; }

        /// <summary>
        /// Fourth Component of a 4D Vector
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double L { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Vector4D"/> is empty.
        /// </summary>
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
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double Magnitude
            => Measurements.VectorMagnitude(I, J, K, L);

        /// <summary>
        /// Gets the length.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double Length
            => Measurements.VectorMagnitude(I, J, K, L);

        /// <summary>
        /// Gets the length squared.
        /// </summary>
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
        /// <returns>The <see cref="Vector4D"/>.</returns>
        public static Vector4D operator +(Vector4D value)
            => new Vector4D(+value.I, +value.J, +value.K, +value.L);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        public static Vector4D operator +(Vector4D value, double addend)
            => value.Add(addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        public static Vector4D operator +(Vector4D value, Vector4D addend)
            => value.Add(addend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Vector4D"/>.</returns>
        public static Vector4D operator -(Vector4D value)
            => new Vector4D(-value.I, -value.J, -value.K, -value.L);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        public static Vector4D operator -(Vector4D value, double subend)
            => value.Subtract(subend);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        public static Vector4D operator -(Vector4D value, Vector4D subend)
            => value.Subtract(subend);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        public static Vector4D operator *(Vector4D value, double factor)
            => value.Scale(factor);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="factor">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        public static Vector4D operator *(double factor, Vector4D value)
            => value.Scale(factor);

        /// <summary>
        /// Divide a Vector4D
        /// </summary>
        /// <param name="divisor">The Vector4D</param>
        /// <param name="divedend">The divisor</param>
        /// <returns>A Vector4D divided by the divisor</returns>
        public static Vector4D operator /(Vector4D divisor, double divedend)
            => Divide4D1D(divisor.I, divisor.J, divisor.K, divisor.L, divedend);

        /// <summary>
        /// Divide a Vector4D
        /// </summary>
        /// <param name="divisor">The Vector4D</param>
        /// <param name="dividend">The divisor</param>
        /// <returns>A Vector4D divided by the divisor</returns>
        public static Vector4D operator /(double divisor, Vector4D dividend)
            => new Vector4D(divisor / dividend.I, divisor / dividend.I, divisor / dividend.K, divisor / dividend.L);

        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator ==(Vector4D a, Vector4D b)
            => Equals(a, b);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator !=(Vector4D a, Vector4D b)
            => !Equals(a, b);

        /// <summary>
        /// Tuple to Vector4D
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static implicit operator Vector4D((double X, double Y, double Z, double W) value)
            => new Vector4D(value);
        #endregion Operators

        #region Factories
        /// <summary>
        /// Create a Random <see cref="Vector4D"/>.
        /// </summary>
        /// <returns></returns>
        public static Vector4D Random()
            => new Vector4D(
                (2 * RandomNumberGenerator.NextDouble()) - 1,
                (2 * RandomNumberGenerator.NextDouble()) - 1,
                (2 * RandomNumberGenerator.NextDouble()) - 1,
                (2 * RandomNumberGenerator.NextDouble()) - 1);

        /// <summary>
        /// Parse a string for a <see cref="Vector4D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Vector4D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector4D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [ParseMethod]
        public static Vector4D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Vector4D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Vector4D"/> data </param>
        /// <param name="provider"></param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector4D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Vector4D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            var value = new Vector4D(
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

        #region Public Methods
        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetHashCode()
            => I.GetHashCode()
            ^ J.GetHashCode()
            ^ K.GetHashCode()
            ^ L.GetHashCode();

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Vector4D a, Vector4D b)
            => Equals(a, b);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Vector4D a, Vector4D b)
            => a.I == b.I & a.J == b.J & a.K == b.K & a.L == b.L;

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Vector4D && Equals(this, (Vector4D)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector4D value)
            => Equals(this, value);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Vector4D"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => ConvertToString(null, CultureInfo.InvariantCulture);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector4D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector4D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        private string ConvertToString(string format, IFormatProvider provider)
        {
            // If the object hasn't been initialized yet, for example reading from reflection, return its name.
            //#pragma warning disable RECS0065 // Expression is always 'true' or always 'false'
            //            if (this is null) return nameof(Vector4D);
            //#pragma warning restore RECS0065 // Expression is always 'true' or always 'false'

            // Capture the culture's list ceparator character.
            var sep = Tokenizer.GetNumericListSeparator(provider);

            // Create the string representation of the struct.
            return $"{nameof(Vector4D)}({nameof(I)}={I.ToString(format, provider)}{sep}{nameof(J)}={J.ToString(format, provider)}{sep}{nameof(K)}={K.ToString(format, provider)}{sep}{nameof(L)}={L.ToString(format, provider)})";
        }
        #endregion Public Methods
    }
}
