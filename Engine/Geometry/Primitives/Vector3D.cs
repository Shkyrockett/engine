// <copyright file="Vector3D.cs" >
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

namespace Engine.Geometry
{
    /// <summary>
    /// Represents a vector in 3D coordinate space (double precision floating-point coordinates).
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [DisplayName(nameof(Vector3D))]
    //[TypeConverter(typeof(Vector3DConverter))]
    public class Vector3D
         : IFormattable
    {
        #region Static Implementations

        /// <summary>
        /// An Empty <see cref="Vector3D"/>.
        /// </summary>
        public static readonly Vector3D Empty = new Vector3D();

        /// <summary>
        /// A Unit <see cref="Vector3D"/>.
        /// </summary>
        public static readonly Vector3D Unit = new Vector3D(1, 1, 1);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new default instance of the <see cref="Vector3D"/>class.
        /// </summary>
        /// <remarks></remarks>
        public Vector3D()
            : this(0, 0, 0)
        { }

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
        public Vector3D(Tuple<double, double, double> tuple)
            : this(tuple.Item1, tuple.Item2, tuple.Item3)
        { }

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
            : this(new Point3D(aI, aJ, aK), new Point3D(bI, bJ, bK))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <remarks></remarks>
        public Vector3D(Point3D a, Point3D b)
            : this(new Vector3D(a.X-b.X, a.Y-b.Y,a.Z-b.Z).Unit())
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
        [XmlIgnore]
        [Browsable(false)]
        public bool IsEmpty => I == 0d && J == 0d && K == 0d;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public double Magnitude => Sqrt(I * I + J * J + K * K);

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector3D operator +(Vector3D value)
        {
            return new Vector3D(+value.I, +value.J, +value.K);
        }

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector3D operator +(Vector3D value, double addend)
        {
            return value.Add(addend);
        }

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point3D operator +(Vector3D value, Point3D addend)
        {
            return value.Add(addend);
        }

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector3D operator +(Vector3D value, Vector3D addend)
        {
            return value.Add(addend);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector3D operator -(Vector3D value)
        {
            return new Vector3D(-value.I, -value.J, -value.K);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector3D operator -(Vector3D value, double subend)
        {
            return value.Subtract(subend);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point3D operator -(Vector3D value, Point3D subend)
        {
            return value.Subtract(subend);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector3D operator -(Vector3D value, Vector3D subend)
        {
            return value.Subtract(subend);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector3D operator *(Vector3D value, double factor)
        {
            return new Vector3D(value.I * factor, value.J * factor, value.K * factor);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="factor">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector3D operator *(double factor, Vector3D value)
        {
            return new Vector3D(value.I * factor, value.J * factor, value.K * factor);
        }

        /// <summary>
        /// Divide a Vector3D
        /// </summary>
        /// <param name="divisor">The Vector3D</param>
        /// <param name="divedend">The divisor</param>
        /// <returns>A Vector3D divided by the divisor</returns>
        /// <remarks></remarks>
        public static Vector3D operator /(Vector3D divisor, double divedend)
        {
            return new Vector3D(divisor.I / divedend, divisor.J / divedend, divisor.K / divedend);
        }

        /// <summary>
        /// Divide a Vector3D
        /// </summary>
        /// <param name="divisor">The Vector3D</param>
        /// <param name="dividend">The divisor</param>
        /// <returns>A Vector3D divided by the divisor</returns>
        /// <remarks></remarks>
        public static Vector3D operator /(double divisor, Vector3D dividend)
        {
            return new Vector3D(divisor / dividend.I, divisor / dividend.I, divisor / dividend.K);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Vector3D a, Vector3D b)
        {
            return Equals(a, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Vector3D a, Vector3D b)
        {
            return !Equals(a, b);
        }

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Vector3D a, Vector3D b)
        {
            return Equals(a, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Vector3D a, Vector3D b)
        {
            return a?.I == b?.I & a?.J == b?.J & a?.K == b?.K;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj is Vector3D && Equals(this, (Vector3D)obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3D value)
        {
            return Equals(this, value);
        }

        /// <summary>
        /// Point to Vector3D
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Vector3D(Point3D value)
        {
            return new Vector3D(value.X, value.Y, value.Z);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a Random <see cref="Vector3D"/>.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector3D Random()
            => new Vector3D(
                (2 * Maths.RandomNumberGenerator.NextDouble()) - 1,
                (2 * Maths.RandomNumberGenerator.NextDouble()) - 1,
                (2 * Maths.RandomNumberGenerator.NextDouble()) - 1);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return I.GetHashCode()
                ^ J.GetHashCode()
                ^ K.GetHashCode();
        }

        /// <summary>
        /// Parse a string for a <see cref="Vector3D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Vector3D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector3D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Vector3D Parse(string source)
        {
            Tokenizer tokenizer = new Tokenizer(source, CultureInfo.InvariantCulture);
            Vector3D value = new Vector3D(
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture),
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture),
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture)
                );
            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }

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
            //return string.Format(provider, "{0}{{{1}={2:" + format + "},{3}={4:" + format + "}}}", nameof(Vector3D), nameof(I), I, nameof(J), J);
            IFormattable formatable = $"{nameof(Vector3D)}({nameof(I)}={I},{nameof(J)}={J})";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
