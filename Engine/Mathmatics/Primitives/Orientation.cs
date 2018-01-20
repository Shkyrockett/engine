// <copyright file="Orientation.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// The orientation struct.
    /// </summary>
    [DataContract, Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DebuggerDisplay("Roll: {Roll}, Pitch: {Pitch}, Yaw: {Yaw}")]
    public struct Orientation
        : IVector<Vector3D>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Orientation"/> struct from a tuple.
        /// </summary>
        /// <param name="tuple"></param>
        public Orientation((double Roll, double Pitch, double Yaw) tuple)
        {
            (Roll, Pitch, Yaw) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Orientation"/> class.
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <param name="pitch">The pitch.</param>
        /// <param name="yaw">The yaw.</param>
        public Orientation(double roll, double pitch, double yaw)
        {
            Roll = roll;
            Pitch = pitch;
            Yaw = yaw;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the roll.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Roll { get; set; }

        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Pitch { get; set; }

        /// <summary>
        /// Gets or sets the yaw.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Yaw { get; set; }

        #endregion

        #region Operators

        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Orientation"/>.</returns>
        public static Orientation operator +(Orientation value)
            => (+value.Roll, +value.Pitch, +value.Yaw);

        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="Orientation"/>.</returns>
        public static Orientation operator +(Orientation value, double addend)
            => Add3D(value.Roll, value.Pitch, value.Yaw, addend);

        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="Orientation"/>.</returns>
        public static Orientation operator +(Orientation value, Orientation addend)
            => Add3D(value.Roll, value.Pitch, value.Yaw, addend.Roll, addend.Pitch, addend.Yaw);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Orientation"/>.</returns>
        public static Orientation operator -(Orientation value)
            => (-value.Roll, -value.Pitch, -value.Yaw);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>The <see cref="Orientation"/>.</returns>
        public static Orientation operator -(Orientation value, double subend)
            => Subtract3D(value.Roll, value.Pitch, value.Yaw, subend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>The <see cref="Orientation"/>.</returns>
        public static Orientation operator -(Orientation value, Orientation subend)
            => Subtract3D(value.Roll, value.Pitch, value.Yaw, subend.Roll, subend.Pitch, subend.Yaw);

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="factor">The factor.</param>
        /// <returns>The <see cref="Orientation"/>.</returns>
        public static Orientation operator *(Orientation value, double factor)
            => Scale3D(value.Roll, value.Pitch, value.Yaw, factor);

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="factor">The factor.</param>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Orientation"/>.</returns>
        public static Orientation operator *(double factor, Orientation value)
            => Scale3D(value.Roll, value.Pitch, value.Yaw, factor);

        /// <summary>
        /// The operator /.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="divedend">The divedend.</param>
        /// <returns>The <see cref="Orientation"/>.</returns>
        public static Orientation operator /(Orientation divisor, double divedend)
            => Divide3D1D(divisor.Roll, divisor.Pitch, divisor.Yaw, divedend);

        /// <summary>
        /// The operator /.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns>The <see cref="Orientation"/>.</returns>
        public static Orientation operator /(double divisor, Orientation dividend)
            => Divide1D3D(divisor, dividend.Roll, dividend.Pitch, dividend.Yaw);

        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator ==(Orientation a, Orientation b)
            => Equals(a, b);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
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
            => Roll.GetHashCode()
            ^ Pitch.GetHashCode()
            ^ Yaw.GetHashCode();

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Vector3D && Equals(this, (Vector3D)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3D value)
            => Equals(this, value);

        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Vector3D a, Vector3D b)
            => Equals(a, b);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
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
            // Capture the culture's list separator character.
            var sep = Tokenizer.GetNumericListSeparator(provider);

            // Create the string representation of the struct.
            return $"{nameof(Orientation)}({nameof(Roll)}={Roll.ToString(format, provider)}{sep}{nameof(Pitch)}={Pitch.ToString(format, provider)}{sep}{nameof(Yaw)}={Yaw.ToString(format, provider)})";
        }

        #endregion
    }
}
