// <copyright file="Orientation3D.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Mathematics;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The orientation struct.
    /// </summary>
    [DataContract, Serializable]
    //[TypeConverter(typeof(ExpandableObjectConverter))]
    [TypeConverter(typeof(StructConverter<Orientation3D>))]
    [DebuggerDisplay("{nameof(Quaternion4D)}({nameof(Roll)}: {Roll ?? double.NaN}, {nameof(Pitch)}: {Pitch ?? double.NaN}, {nameof(Yaw)}: {Yaw ?? double.NaN})")]
    public struct Orientation3D
        : IVector<Orientation3D>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Orientation3D"/> struct.
        /// </summary>
        /// <param name="orientation">The orientation.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Orientation3D(Orientation3D orientation)
            : this(orientation.Roll, orientation.Pitch, orientation.Yaw)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Orientation3D"/> struct from a tuple.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Orientation3D((double Roll, double Pitch, double Yaw) tuple)
            : this()
        {
            (Roll, Pitch, Yaw) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Orientation3D"/> class.
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <param name="pitch">The pitch.</param>
        /// <param name="yaw">The yaw.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Orientation3D(double roll, double pitch, double yaw)
            : this()
        {
            Roll = roll;
            Pitch = pitch;
            Yaw = yaw;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Orientation3D"/> to a <see cref="ValueTuple{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <param name="pitch">The pitch.</param>
        /// <param name="yaw">The yaw.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstructor(out double roll, out double pitch, out double yaw)
        {
            roll = Roll;
            pitch = Pitch;
            yaw = Yaw;
        }
        #endregion Deconstructors

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
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        public static Orientation3D operator +(Orientation3D value) => UnaryAdd3D(value.Roll, value.Pitch, value.Yaw);

        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        public static Orientation3D operator +(Orientation3D value, double addend) => Add3D(value.Roll, value.Pitch, value.Yaw, addend);

        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        public static Orientation3D operator +(Orientation3D value, Orientation3D addend) => Add3D(value.Roll, value.Pitch, value.Yaw, addend.Roll, addend.Pitch, addend.Yaw);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        public static Orientation3D operator -(Orientation3D value) => UnaryNegate3D(value.Roll, value.Pitch, value.Yaw);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        public static Orientation3D operator -(Orientation3D value, double subend) => SubtractSubtrahend3D(value.Roll, value.Pitch, value.Yaw, subend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        public static Orientation3D operator -(double value, Orientation3D subend) => SubtractFromMinuend3D(value, subend.Roll, subend.Pitch, subend.Yaw);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        public static Orientation3D operator -(Orientation3D value, Orientation3D subend) => Subtract3D(value.Roll, value.Pitch, value.Yaw, subend.Roll, subend.Pitch, subend.Yaw);

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="factor">The factor.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        public static Orientation3D operator *(Orientation3D value, double factor) => Scale3D(value.Roll, value.Pitch, value.Yaw, factor);

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="factor">The factor.</param>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        public static Orientation3D operator *(double factor, Orientation3D value) => Scale3D(value.Roll, value.Pitch, value.Yaw, factor);

        /// <summary>
        /// The operator /.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        public static Orientation3D operator /(Orientation3D divisor, double dividend) => DivideByDividend3D(divisor.Roll, divisor.Pitch, divisor.Yaw, dividend);

        /// <summary>
        /// The operator /.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        public static Orientation3D operator /(double divisor, Orientation3D dividend) => DivideDivisor3D(divisor, dividend.Roll, dividend.Pitch, dividend.Yaw);

        /// <summary>
        /// Compares two <see cref="Orientation3D"/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Orientation3D left, Orientation3D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Orientation3D"/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Orientation3D left, Orientation3D right) => !Equals(left, right);

        /// <summary>
        /// Tuple to <see cref="Orientation3D"/> struct.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Orientation3D((double Roll, double Pitch, double Yaw) tuple) => new Orientation3D(tuple);
        #endregion Operators

        #region Public Methods
        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Orientation3D a, Orientation3D b) => (a.Roll == b.Roll) && (a.Pitch == b.Pitch) && (a.Yaw == b.Yaw);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Orientation3D && Equals(this, (Orientation3D)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Orientation3D value) => Equals(this, value);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(Roll, Pitch, Yaw);

        /// <summary>
        /// Creates a string representation of this <see cref="Orientation3D"/> struct based on the current culture.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => base.ToString();

        /// <summary>
        /// Creates a string representation of this <see cref="Orientation3D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Orientation3D);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Orientation3D)}=[{nameof(Roll)}:{Roll.ToString(format, provider)}{s} {nameof(Pitch)}:{Pitch.ToString(format, provider)}{s} {nameof(Yaw)}:{Yaw.ToString(format, provider)}]";
        }
        #endregion Public Methods
    }
}
