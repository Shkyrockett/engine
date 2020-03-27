// <copyright file="QuaternionD.cs" company="Shkyrockett" >
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
using static Engine.Mathematics;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The <see cref="Quaternion4D" /> struct.
    /// </summary>
    /// <seealso cref="IVector{T}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(Quaternion4DConverter))]
    [DebuggerDisplay("{ToString()}")]
    public struct Quaternion4D
        : IVector<Quaternion4D>
    {
        #region Implementations
        /// <summary>
        /// Represents a <see cref="Quaternion4D" /> that has <see cref="X" />, <see cref="Y" />, <see cref="Z" />, and <see cref="W" /> values set to zero.
        /// </summary>
        public static readonly Quaternion4D Empty = new Quaternion4D(0d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Quaternion4D" /> that has <see cref="X" />, <see cref="Y" />, <see cref="Z" />, and <see cref="W" /> values set to zero.
        /// </summary>
        public static readonly Quaternion4D Zero = new Quaternion4D(0d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Quaternion4D" /> that has <see cref="X" />, <see cref="Y" />, <see cref="Z" />, and <see cref="W" /> values set to NaN.
        /// </summary>
        public static readonly Quaternion4D NaN = new Quaternion4D(double.NaN, double.NaN, double.NaN, double.NaN);

        /// <summary>
        /// Represents a <see cref="Quaternion4D" /> that has <see cref="X" /> set to 0, <see cref="Y" /> set to 0, <see cref="Z" /> set to 0, and <see cref="W" /> set to 1.
        /// </summary>
        public static readonly Quaternion4D Identity = new Quaternion4D(0d, 0d, 0d, 1d);
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion4D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion4D((double X, double Y, double Z, double W) tuple)
            : this()
        {
            (X, Y, Z, W) = tuple;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion4D" /> class.
        /// </summary>
        /// <param name="x">The <paramref name="x" />.</param>
        /// <param name="y">The <paramref name="y" />.</param>
        /// <param name="z">The <paramref name="z" />.</param>
        /// <param name="w">The <paramref name="w" />.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion4D(double x, double y, double z, double w)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion4D" /> class.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion4D(Vector3D vector, double scalar)
            : this(vector.I, vector.J, vector.K, scalar)
        { }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Quaternion4D" /> to a <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="w">The w.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double x, out double y, out double z, out double w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="X" /> value of this Quaternion.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Y" /> value of this Quaternion.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Z" /> value of this Quaternion.
        /// </summary>
        /// <value>
        /// The z.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Z { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="W" /> value of this Quaternion.
        /// </summary>
        /// <value>
        /// The w.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double W { get; set; }

        /// <summary>
        /// Gets the squared 'length' of this quaternion.
        /// </summary>
        /// <value>
        /// The normal.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Normal => Measurements.QuaternionNormal(X, Y, Z, W);

        /// <summary>
        /// Gets the squared 'length' of this quaternion.
        /// </summary>
        /// <value>
        /// The length squared.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double LengthSquared => Measurements.QuaternionNormal(X, Y, Z, W);

        /// <summary>
        /// Gets the 'length' of this quaternion.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Length => Measurements.QuaternionMagnitude(X, Y, Z, W);

        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
        /// <value>
        /// The pitch.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Pitch
        {
            set
            {
                var euler = QuaternionToEulerAngles(X, Y, Z, W);
                FromEulerAngles(euler.Roll, value, euler.Yaw);
            }
            get
            {
                var test = (X * Y) + (Z * W);
                if (Abs(test) > 0.499d) // singularitY at north and south pole
                {
                    return 0d;
                }

                return Atan2((2d * X * W) - (2d * Y * Z), 1d - (2d * X * X) - (2d * Z * Z));
            }
        }

        /// <summary>
        /// Gets or sets the yaw.
        /// </summary>
        /// <value>
        /// The yaw.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Yaw
        {
            set
            {
                var euler = QuaternionToEulerAngles(X, Y, Z, W);
                FromEulerAngles(euler.Roll, euler.Pitch, value);
            }
            get
            {
                var test = (X * Y) + (Z * W);
                if (Abs(test) > 0.499d) // singularitY at north and south pole
                {
                    return Sign(test) * 2d * Atan2(X, W);
                }

                return Atan2((2d * Y * W) - (2d * X * Z), 1d - (2d * Y * Y) - (2d * Z * Z));
            }
        }

        /// <summary>
        /// Gets or sets the roll.
        /// </summary>
        /// <value>
        /// The roll.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Roll
        {
            set
            {
                var euler = QuaternionToEulerAngles(X, Y, Z, W);
                FromEulerAngles(value, euler.Pitch, euler.Yaw);
            }
            get
            {
                var test = (X * Y) + (Z * W);
                if (Abs(test) > 0.499d) // singularitY at north and south pole
                {
                    return Sign(test) * HalfPi;
                }

                return Asin(2d * test);
            }
        }

        /// <summary>
        /// Local X-aXis portion of this rotation.
        /// </summary>
        /// <value>
        /// The x axis.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Vector3D XAxis
        {
            get
            {
                //var fTX = 2d * X;
                var fTY = 2d * Y;
                var fTZ = 2d * Z;
                var fTWY = fTY * W;
                var fTWZ = fTZ * W;
                var fTXY = fTY * X;
                var fTXZ = fTZ * X;
                var fTYY = fTY * Y;
                var fTZZ = fTZ * Z;
                return new Vector3D(1d - (fTYY + fTZZ), fTXY + fTWZ, fTXZ - fTWY);
            }
        }

        /// <summary>
        /// Local Y-aXis portion of this rotation.
        /// </summary>
        /// <value>
        /// The y axis.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Vector3D YAxis
        {
            get
            {
                var fTX = 2d * X;
                var fTY = 2d * Y;
                var fTZ = 2d * Z;
                var fTWX = fTX * W;
                var fTWZ = fTZ * W;
                var fTXX = fTX * X;
                var fTXY = fTY * X;
                var fTYZ = fTZ * Y;
                var fTZZ = fTZ * Z;
                return new Vector3D(fTXY - fTWZ, 1d - (fTXX + fTZZ), fTYZ + fTWX);
            }
        }

        /// <summary>
        /// Local Z-aXis portion of this rotation.
        /// </summary>
        /// <value>
        /// The z axis.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Vector3D ZAxis
        {
            get
            {
                var fTX = 2d * X;
                var fTY = 2d * Y;
                var fTZ = 2d * Z;
                var fTWX = fTX * W;
                var fTWY = fTY * W;
                var fTXX = fTX * X;
                var fTXZ = fTZ * X;
                var fTYY = fTY * Y;
                var fTYZ = fTZ * Y;
                return new Vector3D(fTXZ + fTWY, fTYZ - fTWX, 1d - (fTXX + fTYY));
            }
        }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Quaternion4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator +(Quaternion4D value) => Plus(value);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator +(Quaternion4D augend, double addend) => Add(augend, addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator +(double augend, Quaternion4D addend) => Add(augend, addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="augend"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator +(Quaternion4D augend, Quaternion4D addend) => Add(augend, addend);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Quaternion4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator -(Quaternion4D value) => Negate(value);

        /// <summary>
        /// Subtract
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subend"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator -(Quaternion4D minuend, double subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subend"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator -(double minuend, Quaternion4D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract
        /// </summary>
        /// <param name="minuend"></param>
        /// <param name="subend"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator -(Quaternion4D minuend, Quaternion4D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Scale
        /// </summary>
        /// <param name="multiplicand">The Point</param>
        /// <param name="multiplier">The Multiplier</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator *(Quaternion4D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Scale
        /// </summary>
        /// <param name="multiplicand">The Multiplier</param>
        /// <param name="multiplier">The Point</param>
        /// <returns>
        /// A Point Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator *(double multiplicand, Quaternion4D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Multiply
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// A Quaternion Multiplied by the Multiplier
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator *(Quaternion4D multiplicand, Quaternion4D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Divide
        /// </summary>
        /// <param name="dividend">The divisor</param>
        /// <param name="divisor">The dividend</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator /(Quaternion4D dividend, double divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide
        /// </summary>
        /// <param name="dividend">The divisor</param>
        /// <param name="divisor">The dividend</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator /(double dividend, Quaternion4D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator /(Quaternion4D dividend, Quaternion4D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Quaternion4D left, Quaternion4D right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="Quaternion4D" /> instances for exact inequality.
        /// The result specifies whether the values of the <see cref="X" />, <see cref="Y" />, <see cref="Z" />, or <see cref="W" />
        /// values of the two <see cref="Quaternion4D" /> objects are unequal.
        /// </summary>
        /// <param name="left">The first <see cref="Quaternion4D" /> to compare.</param>
        /// <param name="right">The second <see cref="Quaternion4D" /> to compare.</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="Quaternion4D" /> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// <para>Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.</para>
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Quaternion4D left, Quaternion4D right) => !left.Equals(right);

        /// <summary>
        /// Converts the specified <see cref="Quaternion4D" /> structure to a <see cref="ValueTuple{T1, T2, T3, T4}" /> structure.
        /// </summary>
        /// <param name="quaternion">The <see cref="Quaternion4D" /> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double X, double Y, double Z, double W)(Quaternion4D quaternion) => (quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

        /// <summary>
        /// Tuple to <see cref="Quaternion4D" />.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Quaternion4D((double X, double Y, double Z, double W) tuple) => new Quaternion4D(tuple);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Plus(Quaternion4D value) => UnaryAdd(value.X, value.Y, value.Z, value.W);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Add(Quaternion4D augend, double addend) => AddVectorUniform(augend.X, augend.Y, augend.Z, augend.W, addend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Add(double augend, Quaternion4D addend) => AddVectorUniform(addend.X, addend.Y, addend.Z, addend.W, augend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Add(Quaternion4D augend, Quaternion4D addend) => AddVectors(augend.X, augend.Y, augend.Z, augend.W, addend.X, addend.Y, addend.Z, addend.W);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Negate(Quaternion4D value) => NegateVector(value.X, value.Y, value.Z, value.W);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Subtract(Quaternion4D minuend, double subend) => SubtractVectorUniform(minuend.X, minuend.Y, minuend.Z, minuend.W, subend);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Subtract(double minuend, Quaternion4D subend) => SubtractFromMinuend(minuend, subend.X, subend.Y, subend.Z, subend.W);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Subtract(Quaternion4D minuend, Quaternion4D subend) => SubtractVector(minuend.X, minuend.Y, minuend.Z, minuend.W, subend.X, subend.Y, subend.Z, subend.W);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Multiply(Quaternion4D multiplicand, double multiplier) => ScaleVector(multiplicand.X, multiplicand.Y, multiplicand.Z, multiplicand.W, multiplier);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Multiply(double multiplicand, Quaternion4D multiplier) => ScaleVector(multiplier.X, multiplier.Y, multiplier.Z, multiplier.W, multiplicand);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Multiply(Quaternion4D multiplicand, Quaternion4D multiplier) => MultiplyQuaternions(multiplicand.X, multiplicand.Y, multiplicand.Z, multiplicand.W, multiplier.X, multiplier.Y, multiplier.Z, multiplier.W);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Divide(Quaternion4D dividend, double divisor) => DivideVectorUniform(dividend.X, dividend.Y, dividend.Z, dividend.W, divisor);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Divide(double dividend, Quaternion4D divisor) => DivideByVectorUniform(dividend, divisor.X, divisor.Y, divisor.Z, divisor.W);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Divide(Quaternion4D dividend, Quaternion4D divisor) => DivideQuaternions(dividend.X, dividend.Y, dividend.Z, dividend.W, divisor.X, divisor.Y, divisor.Z, divisor.W);

        /// <summary>
        /// Compares this <see cref="Quaternion4D" /> with the passed in object.
        /// </summary>
        /// <param name="obj">The object to compare to this <see cref="Quaternion4D" /> to.</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="Quaternion4D" /> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// <para>Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// In this equality Double.NaN is equal to itself, unlike in numeric equality.</para>
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Quaternion4D d && Equals(d);

        /// <summary>
        /// Compares this <see cref="Quaternion4D" /> with the passed in <see cref="Quaternion4D" />.
        /// </summary>
        /// <param name="value">The <see cref="Quaternion4D" /> to compare to this <see cref="Quaternion4D" /> to.</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="Quaternion4D" /> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// <para>Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// In this equality Double.NaN is equal to itself, unlike in numeric equality.</para>
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Quaternion4D value) => (X == value.X) && (Y == value.Y) && (Z == value.Z) && (W == value.W);

        /// <summary>
        /// Converts to valuetuple.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double X, double Y, double Z, double W) ToValueTuple() => (X, Y, Z, W);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D FromValueTuple((double X, double Y, double Z, double W) tuple) => new Quaternion4D(tuple);

        /// <summary>
        /// Converts to quaternion4d.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion4D ToQuaternion4D() => new Quaternion4D(X, Y, Z, W);
        #endregion

        #region Factories
        /// <summary>
        /// set this quaternion's values from the rotation matrix built from the Axi.
        /// </summary>
        /// <param name="XAxis">The x axis.</param>
        /// <param name="YAxis">The y axis.</param>
        /// <param name="ZAxis">The z axis.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D FromAxis(Vector3D XAxis, Vector3D YAxis, Vector3D ZAxis) => QuaternionFromRotationMatrix(XAxis.I, YAxis.I, ZAxis.I, XAxis.J, YAxis.J, ZAxis.J, XAxis.K, YAxis.K, ZAxis.K);

        /// <summary>
        /// The from axis angle.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>
        /// The <see cref="Quaternion4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D FromAxisAngle(Vector3D axis, double angle) => QuaternionFromAxisAngle(axis.I, axis.J, axis.K, angle);

        /// <summary>
        /// The from Euler angles.
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <param name="pitch">The pitch.</param>
        /// <param name="yaw">The yaw.</param>
        /// <returns>
        /// The <see cref="Quaternion4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D FromEulerAngles(double roll, double pitch, double yaw) => QuaternionFromEulerAngles(roll, pitch, yaw);
        #endregion

        #region Factories
        /// <summary>
        /// Parse.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The <see cref="Quaternion4D" />.
        /// </returns>
        [ParseMethod]
        public static Quaternion4D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// The <see cref="Quaternion4D" />.
        /// </returns>
        /// <exception cref="FormatException">
        /// Cannot parse the text '{source}' because it does not have 4 parts separated by commas in the form (x,y,z,w) with optional parenthesis.
        /// or
        /// The parts of the vectors must be decimal numbers
        /// </exception>
        public static Quaternion4D Parse(string source, IFormatProvider provider)
        {
            var sep = Tokenizer.GetNumericListSeparator(provider);
            var values = source?.Replace("Quaternion", string.Empty, StringComparison.OrdinalIgnoreCase).Trim(' ', '{', '(', '[', '<', '}', ')', ']', '>').Split(sep);

            if (values.Length != 4)
            {
                throw new FormatException($"Cannot parse the text '{source}' because it does not have 4 parts separated by commas in the form (x,y,z,w) with optional parenthesis.");
            }
            else
            {
                try
                {
                    return new Quaternion4D(
                        double.Parse(values[0].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture),
                        double.Parse(values[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture),
                        double.Parse(values[2].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture),
                        double.Parse(values[3].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture));
                }
                catch (Exception ex)
                {
                    throw new FormatException("The parts of the vectors must be decimal numbers", ex);
                }
            }
        }
        #endregion

        #region Standard Methods
        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Quaternion4D" /> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Quaternion4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Quaternion4D" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">The <see cref="CultureInfo" /> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Quaternion4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider) => ToString("R" /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Quaternion4D" /> class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The <see cref="CultureInfo" /> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Quaternion4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Quaternion4D);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Quaternion4D)}=[{nameof(X)}:{X.ToString(format, provider)}{s} {nameof(Y)}:{Y.ToString(format, provider)}{s} {nameof(Z)}:{Z.ToString(format, provider)}{s} {nameof(W)}:{W.ToString(format, provider)}]";
        }
        #endregion
    }
}
