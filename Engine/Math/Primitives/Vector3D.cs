﻿// <copyright file="Vector3D.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
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
using System.Xml.Serialization;
using static System.Math;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// Represents a vector in 3D coordinate space (double precision floating-point coordinates).
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [TypeConverter(typeof(StructConverter<Vector3D>))]
    public struct Vector3D
        : IVector<Vector3D>
    {
        #region Static Fields

        /// <summary>
        /// An Empty <see cref="Vector3D"/>.
        /// </summary>
        public static readonly Vector3D Empty = new Vector3D(0, 0, 0);

        /// <summary>
        /// A Unit <see cref="Vector3D"/>.
        /// </summary>
        public static readonly Vector3D Unit = new Vector3D(1, 1, 1);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector3D XAxis = new Vector3D(1, 0, 0);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector3D YAxis = new Vector3D(0, 1, 0);

        /// <summary>
        /// 
        /// </summary>
        public static readonly Vector3D ZAxis = new Vector3D(0, 0, 1);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="vector3D">A <see cref="Vector3D"/> class to clone.</param>
        public Vector3D(Vector3D vector3D)
            : this(vector3D.I, vector3D.J, vector3D.K)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        /// <remarks></remarks>
        public Vector3D((double X, double Y, double Z) tuple)
            => (I, J, K) = tuple;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="i">The <see cref="I"/> component of the <see cref="Vector3D"/> class.</param>
        /// <param name="j">The <see cref="J"/> component of the <see cref="Vector3D"/> class.</param>
        /// <param name="k">The <see cref="K"/> component of the <see cref="Vector3D"/> class.</param>
        /// <remarks></remarks>
        public Vector3D(double i, double j, double k)
        {
            I = i;
            J = j;
            K = k;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="aI"></param>
        /// <param name="aJ"></param>
        /// <param name="aK"></param>
        /// <param name="bI"></param>
        /// <param name="bJ"></param>
        /// <param name="bK"></param>
        /// <remarks></remarks>
        public Vector3D(double aI, double aJ, double aK, double bI, double bJ, double bK)
            : this(new Vector3D(aI, aJ, aK), new Vector3D(bI, bJ, bK))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <remarks></remarks>
        public Vector3D(Vector3D a, Vector3D b)
            : this(new Vector3D(a.I - b.I, a.J - b.J, a.K - b.K).Unit())
        { }

        #endregion

        #region Properties

        /// <summary>
        /// First Point of a 3D Vector
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double I { get; set; }

        /// <summary>
        /// Second Component of a 3D Vector
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double J { get; set; }

        /// <summary>
        /// Third Component of a 3D Vector
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double K { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Vector3D"/> is empty.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public bool IsEmpty
            => Abs(I) < Epsilon
            && Abs(J) < Epsilon
            && Abs(K) < Epsilon;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double Magnitude
            => Sqrt(I * I + J * J + K * K);

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector3D operator +(Vector3D value)
            => new Vector3D(+value.I, +value.J, +value.K);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector3D operator +(Vector3D value, double addend)
            => value.Add(addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point3D operator +(Vector3D value, Point3D addend)
            => value.Add(addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector3D operator +(Vector3D value, Vector3D addend)
            => value.Add(addend);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector3D operator -(Vector3D value)
            => new Vector3D(-value.I, -value.J, -value.K);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector3D operator -(Vector3D value, double subend)
            => value.Subtract(subend);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point3D operator -(Vector3D value, Point3D subend)
            => value.Subtract(subend);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector3D operator -(Vector3D value, Vector3D subend)
            => value.Subtract(subend);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector3D operator *(Vector3D value, double factor)
            => new Vector3D(value.I * factor, value.J * factor, value.K * factor);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="factor">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector3D operator *(double factor, Vector3D value)
            => new Vector3D(value.I * factor, value.J * factor, value.K * factor);

        /// <summary>
        /// Divide a Vector3D
        /// </summary>
        /// <param name="divisor">The Vector3D</param>
        /// <param name="divedend">The divisor</param>
        /// <returns>A Vector3D divided by the divisor</returns>
        /// <remarks></remarks>
        public static Vector3D operator /(Vector3D divisor, double divedend)
            => new Vector3D(divisor.I / divedend, divisor.J / divedend, divisor.K / divedend);

        /// <summary>
        /// Divide a Vector3D
        /// </summary>
        /// <param name="divisor">The Vector3D</param>
        /// <param name="dividend">The divisor</param>
        /// <returns>A Vector3D divided by the divisor</returns>
        /// <remarks></remarks>
        public static Vector3D operator /(double divisor, Vector3D dividend)
            => new Vector3D(divisor / dividend.I, divisor / dividend.I, divisor / dividend.K);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Vector3D a, Vector3D b)
            => Equals(a, b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Vector3D a, Vector3D b)
            => !Equals(a, b);

        /// <summary>
        /// Point to Vector3D
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3D((double X, double Y, double Z) tuple)
            => new Vector3D(tuple);

        /// <summary>
        /// Point to Vector3D
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3D(Point3D value)
            => new Vector3D(value.X, value.Y, value.Z);

        #endregion

        #region Factories

        /// <summary>
        /// Create a Random <see cref="Vector3D"/>.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector3D Random()
            => new Vector3D(
                (2 * RandomNumberGenerator.NextDouble()) - 1,
                (2 * RandomNumberGenerator.NextDouble()) - 1,
                (2 * RandomNumberGenerator.NextDouble()) - 1);

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
            var value = new Vector3D(
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider)
                );
            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => I.GetHashCode()
            ^ J.GetHashCode()
            ^ K.GetHashCode();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Vector3D && Equals(this, (Vector3D)obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3D value)
            => Equals(this, value);

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Vector3D a, Vector3D b)
            => Equals(a, b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Vector3D a, Vector3D b)
            => a.I == b.I & a.J == b.J & a.K == b.K;

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Vector3D"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => ConvertToString(null, CultureInfo.InvariantCulture);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector3D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector3D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            // Capture the culture's list ceparator character.
            char sep = Tokenizer.GetNumericListSeparator(provider);

            // Create the string representation of the struct.
            return $"{nameof(Vector3D)}({nameof(I)}={I.ToString(format, provider)}{sep}{nameof(J)}={J.ToString(format, provider)}{sep}{nameof(K)}={K.ToString(format, provider)})";
        }

        #endregion
    }
}
