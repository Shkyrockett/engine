// <copyright file="Vector2D.cs" >
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
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// Represents a vector in 2D coordinate space (double precision floating-point coordinates).
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [DisplayName(nameof(Vector2D))]
    [TypeConverter(typeof(Vector2DConverter))]
    public struct Vector2D
         : IFormattable
    {
        #region Static Implementations

        /// <summary>
        /// An Empty <see cref="Vector2D"/>.
        /// </summary>
        public static readonly Vector2D Empty = new Vector2D();

        /// <summary>
        /// A Unit <see cref="Vector2D"/>.
        /// </summary>
        public static readonly Vector2D Unit = new Vector2D(1, 1);

        #endregion

        #region Constructors

        ///// <summary>
        ///// Initializes a new default instance of the <see cref="Vector2D"/>class.
        ///// </summary>
        ///// <remarks></remarks>
        //public Vector2D()
        //    : this(0, 0)
        //{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> class.
        /// </summary>
        /// <param name="vector2D">A <see cref="Vector2D"/> class to clone.</param>
        public Vector2D(Vector2D vector2D)
            : this(vector2D.I, vector2D.J)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        /// <remarks></remarks>
        public Vector2D(Tuple<double, double> tuple)
            : this(tuple.Item1, tuple.Item2)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> class.
        /// </summary>
        /// <param name="i">The <see cref="I"/> component of the <see cref="Vector2D"/> class.</param>
        /// <param name="j">The <see cref="J"/> component of the <see cref="Vector2D"/> class.</param>
        /// <remarks></remarks>
        public Vector2D(double i, double j)
        {
            I = i;
            J = j;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> class.
        /// </summary>
        /// <param name="aI"></param>
        /// <param name="aJ"></param>
        /// <param name="bI"></param>
        /// <param name="bJ"></param>
        /// <remarks></remarks>
        public Vector2D(double aI, double aJ, double bI, double bJ)
            : this(new Point2D(aI, aJ), new Point2D(bI, bJ))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <remarks></remarks>
        public Vector2D(Point2D a, Point2D b)
            : this(a.Delta(b).Unit())
        { }

        #endregion

        #region Properties

        /// <summary>
        /// First Point of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double I { get; set; }

        /// <summary>
        /// Second Component of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double J { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Vector2D"/> is empty.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsEmpty => I == 0f && J == 0f;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public double Magnitude => Sqrt(I * I + J * J);

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2D operator +(Vector2D value)
        {
            return new Vector2D(+value.I, +value.J);
        }

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D operator +(Vector2D value, double addend)
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
        public static Point2D operator +(Vector2D value, Point2D addend)
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
        public static Vector2D operator +(Vector2D value, Vector2D addend)
        {
            return value.Add(addend);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2D operator -(Vector2D value)
        {
            return new Vector2D(-value.I, -value.J);
        }

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D operator -(Vector2D value, double subend)
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
        public static Point2D operator -(Vector2D value, Point2D subend)
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
        public static Vector2D operator -(Vector2D value, Vector2D subend)
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
        public static Vector2D operator *(Vector2D value, double factor)
        {
            return new Vector2D(value.I * factor, value.J * factor);
        }

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="factor">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector2D operator *(double factor, Vector2D value)
        {
            return new Vector2D(value.I * factor, value.J * factor);
        }

        /// <summary>
        /// Divide a Vector2D
        /// </summary>
        /// <param name="divisor">The Vector2D</param>
        /// <param name="divedend">The divisor</param>
        /// <returns>A Vector2D divided by the divisor</returns>
        /// <remarks></remarks>
        public static Vector2D operator /(Vector2D divisor, double divedend)
        {
            return new Vector2D(divisor.I / divedend, divisor.J / divedend);
        }

        /// <summary>
        /// Divide a Vector2D
        /// </summary>
        /// <param name="divisor">The Vector2D</param>
        /// <param name="dividend">The divisor</param>
        /// <returns>A Vector2D divided by the divisor</returns>
        /// <remarks></remarks>
        public static Vector2D operator /(double divisor, Vector2D dividend)
        {
            return new Vector2D(divisor / dividend.I, divisor / dividend.I);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Vector2D a, Vector2D b)
        {
            return Equals(a, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Vector2D a, Vector2D b)
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
        public static bool Compare(Vector2D a, Vector2D b)
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
        public static bool Equals(Vector2D a, Vector2D b)
        {
            return a.I == b.I & a.J == b.J;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj is Vector2D && Equals(this, (Vector2D)obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector2D value)
        {
            return Equals(this, value);
        }

        /// <summary>
        /// Vector2D to PointF
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Vector2D(PointF value)
        {
            return new Vector2D(value.X, value.Y);
        }

        /// <summary>
        /// Vector2D to Point
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Vector2D(Point value)
        {
            return new Vector2D(value.X, value.Y);
        }

        /// <summary>
        /// Vector2D to Point
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Vector2D(Point2D value)
        {
            return new Vector2D(value.X, value.Y);
        }

        /// <summary>
        /// PointF to Vector2D
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static explicit operator PointF(Vector2D value)
        {
            return new PointF((int)value.I, (int)value.I);
        }

        /// <summary>
        /// Point to Vector2D
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static explicit operator Point(Vector2D value)
        {
            return new Point((int)value.I, (int)value.I);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a Random <see cref="Vector2D"/>.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D Random() => new Vector2D((2 * Maths.RandomNumberGenerator.NextDouble()) - 1, (2 * Maths.RandomNumberGenerator.NextDouble()) - 1);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return I.GetHashCode() ^ J.GetHashCode();
        }

        /// <summary>
        /// Parse a string for a <see cref="Vector2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Vector2D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Vector2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Vector2D Parse(string source)
        {
            Tokenizer tokenizer = new Tokenizer(source, CultureInfo.InvariantCulture);
            Vector2D value = new Vector2D(
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture),
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture));
            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Vector2D"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => ConvertToString(null, CultureInfo.InvariantCulture);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector2D"/> struct based on the format string
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
        /// Creates a string representation of this <see cref="Vector2D"/> struct based on the format string
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
            //return string.Format(provider, "{0}{{{1}={2:" + format + "},{3}={4:" + format + "}}}", nameof(Vector2D), nameof(I), I, nameof(J), J);
            IFormattable formatable = $"{nameof(Vector2D)}({nameof(I)}={I},{nameof(J)}={J})";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
