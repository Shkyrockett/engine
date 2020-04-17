// <copyright file="Orientation3D.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The orientation struct.
    /// </summary>
    /// <seealso cref="Engine.IVector{Engine.Orientation3D}" />
    /// <seealso cref="System.IEquatable{Engine.Orientation3D}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Orientation3D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Orientation3D
        : IVector<Orientation3D>, IEquatable<Orientation3D>
    {
        #region Implementations
        /// <summary>
        /// The empty
        /// </summary>
        public static readonly Orientation3D Empty = new Orientation3D(0d, 0d, 0d);

        /// <summary>
        /// The na n
        /// </summary>
        public static readonly Orientation3D NaN = new Orientation3D(double.NaN, double.NaN, double.NaN);
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Orientation3D" /> struct.
        /// </summary>
        /// <param name="orientation">The orientation.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Orientation3D(Orientation3D orientation)
            : this(orientation.Roll, orientation.Pitch, orientation.Yaw)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Orientation3D" /> struct from a tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Orientation3D((double Roll, double Pitch, double Yaw) tuple)
            : this()
        {
            (Roll, Pitch, Yaw) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Orientation3D" /> class.
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
        /// Deconstruct this <see cref="Orientation3D" /> to a <see cref="ValueTuple{T1, T2, T3}" />.
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
        /// <value>
        /// The roll.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Roll { get; set; }

        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
        /// <value>
        /// The pitch.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Pitch { get; set; }

        /// <summary>
        /// Gets or sets the yaw.
        /// </summary>
        /// <value>
        /// The yaw.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Yaw { get; set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Orientation3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D operator +(Orientation3D value) => Plus(value);

        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="augend">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The <see cref="Orientation3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D operator +(double augend, Orientation3D addend) => Add(augend, addend);

        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="augend">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D operator +(Orientation3D augend, double addend) => Add(augend, addend);

        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="augend">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The <see cref="Orientation3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D operator +(Orientation3D augend, Orientation3D addend) => Add(augend, addend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Orientation3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D operator -(Orientation3D value) => Negate(value);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="minuend">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The <see cref="Orientation3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D operator -(Orientation3D minuend, double subend) => Subtract(minuend, subend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="minuend">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D operator -(double minuend, Orientation3D subend) => Subtract(minuend, subend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="minuend">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The <see cref="Orientation3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D operator -(Orientation3D minuend, Orientation3D subend) => Subtract(minuend, subend);

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="multiplicand">The value.</param>
        /// <param name="multiplier">The factor.</param>
        /// <returns>
        /// The <see cref="Orientation3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D operator *(Orientation3D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="multiplicand">The factor.</param>
        /// <param name="multiplier">The value.</param>
        /// <returns>
        /// The <see cref="Orientation3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D operator *(double multiplicand, Orientation3D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// The operator /.
        /// </summary>
        /// <param name="dividend">The divisor.</param>
        /// <param name="divisor">The dividend.</param>
        /// <returns>
        /// The <see cref="Orientation3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D operator /(Orientation3D dividend, double divisor) => Divide(dividend, divisor);

        /// <summary>
        /// The operator /.
        /// </summary>
        /// <param name="dividend">The divisor.</param>
        /// <param name="divisor">The dividend.</param>
        /// <returns>
        /// The <see cref="Orientation3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D operator /(double dividend, Orientation3D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Compares two <see cref="Orientation3D" /> objects.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Orientation3D left, Orientation3D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Orientation3D" /> objects.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Orientation3D left, Orientation3D right) => !Equals(left, right);

        /// <summary>
        /// Tuple to <see cref="Orientation3D" /> struct.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Orientation3D((double Roll, double Pitch, double Yaw) tuple) => FromValueTuple(tuple);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D Plus(Orientation3D value) => Operations.Plus(value.Roll, value.Pitch, value.Yaw);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D Add(double augend, Orientation3D addend) => AddVectorUniform(addend.Roll, addend.Pitch, addend.Yaw, augend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D Add(Orientation3D augend, double addend) => AddVectorUniform(augend.Roll, augend.Pitch, augend.Yaw, addend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D Add(Orientation3D augend, Orientation3D addend) => AddVectors(augend.Roll, augend.Pitch, augend.Yaw, addend.Roll, addend.Pitch, addend.Yaw);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D Negate(Orientation3D value) => Operations.Negate(value.Roll, value.Pitch, value.Yaw);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D Subtract(Orientation3D minuend, double subend) => SubtractVectorUniform(minuend.Roll, minuend.Pitch, minuend.Yaw, subend);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D Subtract(double minuend, Orientation3D subend) => SubtractFromMinuend(minuend, subend.Roll, subend.Pitch, subend.Yaw);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D Subtract(Orientation3D minuend, Orientation3D subend) => SubtractVector(minuend.Roll, minuend.Pitch, minuend.Yaw, subend.Roll, subend.Pitch, subend.Yaw);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D Multiply(Orientation3D multiplicand, double multiplier) => ScaleVector(multiplicand.Roll, multiplicand.Pitch, multiplicand.Yaw, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D Multiply(double multiplicand, Orientation3D multiplier) => ScaleVector(multiplier.Roll, multiplier.Pitch, multiplier.Yaw, multiplicand);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D Divide(Orientation3D dividend, double divisor) => DivideVectorUniform(dividend.Roll, dividend.Pitch, dividend.Yaw, divisor);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D Divide(double dividend, Orientation3D divisor) => DivideByVectorUniform(dividend, divisor.Roll, divisor.Pitch, divisor.Yaw);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Orientation3D && Equals(this, (Orientation3D)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] Orientation3D value) => (Roll == value.Roll) && (Pitch == value.Pitch) && (Yaw == value.Yaw);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D FromValueTuple((double Roll, double Pitch, double Yaw) tuple) => new Orientation3D(tuple);
        #endregion Public Methods

        #region Standard Methods
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(Roll, Pitch, Yaw);

        /// <summary>
        /// Creates a string representation of this <see cref="Orientation3D" /> struct based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Orientation3D" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider) => ToString("R" /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Orientation3D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Orientation3D);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Orientation3D)}({nameof(Roll)}:{Roll.ToString(format, provider)}{s} {nameof(Pitch)}:{Pitch.ToString(format, provider)}{s} {nameof(Yaw)}:{Yaw.ToString(format, provider)})";
        }
        #endregion
    }
}
