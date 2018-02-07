// <copyright file="Vector2D.cs" company="Shkyrockett" >
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
    /// Represents a vector in 2D coordinate space (double precision floating-point coordinates).
    /// </summary>
    [DataContract, Serializable]
    [ComVisible(true)]
    [TypeConverter(typeof(StructConverter<Vector2D>))]
    [DebuggerDisplay("I: {I}, J: {J}")]
    public struct Vector2D
        : IVector<Vector2D>
    {
        #region Static Fields
        /// <summary>
        /// An Empty <see cref="Vector2D"/>.
        /// </summary>
        public static readonly Vector2D Empty = new Vector2D();

        /// <summary>
        /// A Unit <see cref="Vector2D"/>.
        /// </summary>
        public static readonly Vector2D Unit = new Vector2D(1, 1);

        /// <summary>
        /// Vector2D(1,0)
        /// </summary>
        public static readonly Vector2D XAxis = new Vector2D(1, 0);

        /// <summary>
        /// Vector2D(0,1)
        /// </summary>
        public static readonly Vector2D YAxis = new Vector2D(0, 1);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="tuple"></param>
        /// <remarks></remarks>
        public Vector2D((double X, double Y) tuple)
        {
            (I, J) = tuple;
        }

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
        /// <remarks></remarks>
        public Vector2D(double i, double j)
        {
            I = i;
            J = j;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
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
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <remarks></remarks>
        public Vector2D(Point2D a, Point2D b)
            : this(a.Delta(b).Unit())
        { }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// First Point of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        [DataMember, XmlAttribute, SoapAttribute]
        public double I { get; set; }

        /// <summary>
        /// Second Component of a 2D Vector
        /// </summary>
        /// <remarks></remarks>
        [DataMember, XmlAttribute, SoapAttribute]
        public double J { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Vector2D"/> is empty.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public bool IsEmpty
            => Abs(I) < Epsilon
            && Abs(J) < Epsilon;

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double Magnitude
            => Measurements.VectorMagnitude(I, J);

        /// <summary>
        /// Length Property - the length of this Vector
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double Length
            => Measurements.VectorMagnitude(I, J);

        /// <summary>
        /// LengthSquared Property - the squared length of this Vector
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public double LengthSquared
            => Measurements.VectorMagnitudeSquared(I, J);
        #endregion Properties

        #region Operators
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2D operator +(Vector2D value)
            => new Vector2D(+value.I, +value.J);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D operator +(Vector2D value, double addend)
            => value.Add(addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D operator +(Vector2D value, Point2D addend)
            => value.Add(addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D operator +(Vector2D value, Vector2D addend)
            => value.Add(addend);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2D operator -(Vector2D value)
            => new Vector2D(-value.I, -value.J);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D operator -(Vector2D value, double subend)
            => value.Subtract(subend);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D operator -(Vector2D value, Point2D subend)
            => value.Subtract(subend);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D operator -(Vector2D value, Vector2D subend)
            => value.Subtract(subend);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector2D operator *(Vector2D value, double factor)
            => new Vector2D(value.I * factor, value.J * factor);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="factor">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector2D operator *(Vector2D value, Size2D factor)
            => new Vector2D(value.I * factor.Width, value.J * factor.Height);

        /// <summary>
        /// Scale a Vector
        /// </summary>
        /// <param name="factor">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static Vector2D operator *(double factor, Vector2D value)
            => new Vector2D(value.I * factor, value.J * factor);

        /// <summary>
        /// Divide a Vector2D
        /// </summary>
        /// <param name="divisor">The Vector2D</param>
        /// <param name="divedend">The divisor</param>
        /// <returns>A Vector2D divided by the divisor</returns>
        /// <remarks></remarks>
        public static Vector2D operator /(Vector2D divisor, double divedend)
            => new Vector2D(divisor.I / divedend, divisor.J / divedend);

        /// <summary>
        /// Divide a Vector2D
        /// </summary>
        /// <param name="divisor">The Vector2D</param>
        /// <param name="dividend">The divisor</param>
        /// <returns>A Vector2D divided by the divisor</returns>
        /// <remarks></remarks>
        public static Vector2D operator /(double divisor, Vector2D dividend)
            => new Vector2D(divisor / dividend.I, divisor / dividend.I);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool operator ==(Vector2D a, Vector2D b)
            => Equals(a, b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Vector2D a, Vector2D b)
            => !Equals(a, b);

        /// <summary>
        /// Tuple to <see cref="Vector2D"/>.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Vector2D((double X, double Y) tuple)
            => new Vector2D(tuple);

        ///// <summary>
        ///// <see cref="PointF"/> to <see cref="Vector2D"/>
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //[DebuggerStepThrough]
        //public static implicit operator Vector2D(PointF value)
        //    => new Vector2D(value.X, value.Y);

        ///// <summary>
        ///// Vector2D to Point
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //[DebuggerStepThrough]
        //public static implicit operator Vector2D(Point value)
        //    => new Vector2D(value.X, value.Y);

        ///// <summary>
        ///// Vector2D to Point
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //[DebuggerStepThrough]
        //public static implicit operator Vector2D(Point2D value)
        //    => new Vector2D(value.X, value.Y);

        ///// <summary>
        ///// PointF to Vector2D
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //[DebuggerStepThrough]
        //public static explicit operator PointF(Vector2D value)
        //    => new PointF((int)value.I, (int)value.I);

        ///// <summary>
        ///// Point to Vector2D
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //[DebuggerStepThrough]
        //public static explicit operator Point(Vector2D value)
        //    => new Point((int)value.I, (int)value.I);
        #endregion Operators

        #region Factories
        /// <summary>
        /// Create a Random <see cref="Vector2D"/>.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Vector2D Random()
            => new Vector2D(
                (2 * RandomNumberGenerator.NextDouble()) - 1,
                (2 * RandomNumberGenerator.NextDouble()) - 1);

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
            var value = new Vector2D(
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider));
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
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
            => unchecked(I.GetHashCode() ^ J.GetHashCode());

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Vector2D a, Vector2D b)
            => Equals(a, b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Vector2D && Equals(this, (Vector2D)obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector2D value)
            => Equals(this, value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Vector2D a, Vector2D b)
            => a.I == b.I & a.J == b.J;

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Vector2D"/>.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector2D"/> struct based on the IFormatProvider
        /// passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A string representation of this instance as specified by provider.
        /// </returns>
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Vector2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
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
        public string ConvertToString(string format, IFormatProvider provider)
        {
            //            // If the object hasn't been initialized yet, for example reading from reflection, return its name.
            //#pragma warning disable RECS0065 // Expression is always 'true' or always 'false'
            //            if (this == null) return nameof(Vector2D);
            //#pragma warning restore RECS0065 // Expression is always 'true' or always 'false'

            // Capture the culture's list separator character.
            var sep = Tokenizer.GetNumericListSeparator(provider);

            // Create the string representation of the struct.
            return $"{nameof(Vector2D)}({nameof(I)}={I.ToString(format, provider)}{sep}{nameof(J)}={J.ToString(format, provider)})";
        }
        #endregion Public Methods
    }
}
