// <copyright file="Orientation.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public struct Orientation
        : IVector<Vector3D>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Orientation"/> struct from a tuple.
        /// </summary>
        /// <param name="tuple"></param>
        public Orientation((double Roll, double Pitch, double Yaw) tuple)
            => (Roll, Pitch, Yaw) = tuple;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roll"></param>
        /// <param name="pitch"></param>
        /// <param name="yaw"></param>
        public Orientation(double roll, double pitch, double yaw)
        {
            Roll = roll;
            Pitch = pitch;
            Yaw = yaw;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
        public double Roll { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
        public double Pitch { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
        public double Yaw { get; set; }

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Orientation operator +(Orientation value)
            => (+value.Roll, +value.Pitch, +value.Yaw);

        /// <summary>
        /// Add <see cref="Orientation"/>s.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Orientation operator +(Orientation value, double addend)
            => Add3D(value.Roll, value.Pitch, value.Yaw, addend);

        /// <summary>
        /// Add <see cref="Orientation"/>s.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Orientation operator +(Orientation value, Orientation addend)
            => Add3D(value.Roll, value.Pitch, value.Yaw, addend.Roll, addend.Pitch, addend.Yaw);

        /// <summary>
        /// Negate an <see cref="Orientation"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Orientation operator -(Orientation value)
            => (-value.Roll, -value.Pitch, -value.Yaw);

        /// <summary>
        /// Subtract <see cref="Orientation"/>s.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Orientation operator -(Orientation value, double subend)
            => Subtract3D(value.Roll, value.Pitch, value.Yaw, subend);

        /// <summary>
        /// Subtract <see cref="Orientation"/>s.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Orientation operator -(Orientation value, Orientation subend)
            => Subtract3D(value.Roll, value.Pitch, value.Yaw, subend.Roll, subend.Pitch, subend.Yaw);

        /// <summary>
        /// Scale an <see cref="Orientation"/>s.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Orientation operator *(Orientation value, double factor)
            => Scale3D(value.Roll, value.Pitch, value.Yaw, factor);

        /// <summary>
        /// Scale an <see cref="Orientation"/>s.
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Orientation operator *(double factor, Orientation value)
            => Scale3D(value.Roll, value.Pitch, value.Yaw, factor);

        /// <summary>
        /// Divide a <see cref="Orientation"/>.
        /// </summary>
        /// <param name="divisor"></param>
        /// <param name="divedend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Orientation operator /(Orientation divisor, double divedend)
            => Divide3D1D(divisor.Roll, divisor.Pitch, divisor.Yaw, divedend);

        /// <summary>
        /// Divide a <see cref="Orientation"/>.
        /// </summary>
        /// <param name="divisor"></param>
        /// <param name="dividend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Orientation operator /(double divisor, Orientation dividend)
            => Divide1D3D(divisor, dividend.Roll, dividend.Pitch, dividend.Yaw);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Orientation a, Orientation b)
            => Equals(a, b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Orientation a, Orientation b)
            => !Equals(a, b);

        /// <summary>
        /// Tupple to convert to a <see cref="Orientation"/> struct.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Orientation((double Roll, double Pitch, double Yaw) tuple)
            => new Orientation(tuple);

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => Roll.GetHashCode()
            ^ Pitch.GetHashCode()
            ^ Yaw.GetHashCode();

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
            return $"{nameof(Orientation)}({nameof(Roll)}={Roll.ToString(format, provider)}{sep}{nameof(Pitch)}={Pitch.ToString(format, provider)}{sep}{nameof(Yaw)}={Yaw.ToString(format, provider)})";
        }

        #endregion
    }
}
