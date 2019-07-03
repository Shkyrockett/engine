// <copyright file="QuaternionD.cs" company="Shkyrockett" >
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
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;
using static Engine.Mathematics;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The <see cref="Quaternion4D"/> struct.
    /// </summary>
    [DataContract, Serializable]
    [ComVisible(true)]
    [TypeConverter(typeof(Quaternion4DConverter))]
    //[TypeConverter(typeof(StructConverter<Quaternion4D>))]
    [DebuggerDisplay("{nameof(Quaternion4D)}({nameof(X)}: {X ?? double.NaN}, {nameof(Y)}: {Y ?? double.NaN}, {nameof(Z)}: {Z ?? double.NaN}, {nameof(W)}: {W ?? double.NaN})")]
    public struct Quaternion4D
        : IVector<Quaternion4D>
    {
        #region Static Fields
        /// <summary>
        /// Represents a <see cref="Quaternion4D"/> that has <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, and <see cref="W"/> values set to zero.
        /// </summary>
        public static readonly Quaternion4D Empty = new Quaternion4D(0d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Quaternion4D"/> that has <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, and <see cref="W"/> values set to zero.
        /// </summary>
        public static readonly Quaternion4D Zero = new Quaternion4D(0d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Quaternion4D"/> that has <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, and <see cref="W"/> values set to NaN.
        /// </summary>
        public static readonly Quaternion4D NaN = new Quaternion4D(double.NaN, double.NaN, double.NaN, double.NaN);

        /// <summary>
        /// Represents a <see cref="Quaternion4D"/> that has <see cref="X"/> set to 0, <see cref="Y"/> set to 0, <see cref="Z"/> set to 0, and <see cref="W"/> set to 1.
        /// </summary>
        public static readonly Quaternion4D Identity = new Quaternion4D(0d, 0d, 0d, 1d);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion4D"/> class.
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
        /// Initializes a new instance of the <see cref="Quaternion4D"/> class.
        /// </summary>
        /// <param name="x">The <paramref name="x"/>.</param>
        /// <param name="y">The <paramref name="y"/>.</param>
        /// <param name="z">The <paramref name="z"/>.</param>
        /// <param name="w">The <paramref name="w"/>.</param>
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
        /// Initializes a new instance of the <see cref="Quaternion4D"/> class.
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
        /// Deconstruct this <see cref="Quaternion4D"/> to a <see cref="ValueTuple{T1, T2, T3, T4}"/>.
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
        /// Gets or sets the <see cref="X"/> value of this Quaternion. 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Y"/> value of this Quaternion. 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Z"/> value of this Quaternion. 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Z { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="W"/> value of this Quaternion. 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double W { get; set; }

        /// <summary>
        /// Gets the squared 'length' of this quaternion.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Normal
            => Measurements.QuaternionNormal(X, Y, Z, W);

        /// <summary>
        /// Gets the squared 'length' of this quaternion.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double LengthSquared
            => Measurements.QuaternionNormal(X, Y, Z, W);

        /// <summary>
        /// Gets the 'length' of this quaternion.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Length
            => Measurements.QuaternionMagnitude(X, Y, Z, W);

        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
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
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator +(Quaternion4D value) => UnaryAdd4D(value.X, value.Y, value.Z, value.W);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator +(Quaternion4D value, double addend) => Add4D(value.X, value.Y, value.Z, value.W, addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator +(double value, Quaternion4D addend) => Add4D(addend.X, addend.Y, addend.Z, addend.W, value);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator +(Quaternion4D value, Quaternion4D addend) => Add4D(value.X, value.Y, value.Z, value.W, addend.X, addend.Y, addend.Z, addend.W);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator -(Quaternion4D value) => UnaryNegate4D(value.X, value.Y, value.Z, value.W);

        /// <summary>
        /// Subtract
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator -(Quaternion4D value, double subend) => SubtractSubtrahend4D(value.X, value.Y, value.Z, value.W, subend);

        /// <summary>
        /// Subtract
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator -(double value, Quaternion4D subend) => SubtractFromMinuend4D(value, subend.X, subend.Y, subend.Z, subend.W);

        /// <summary>
        /// Subtract
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator -(Quaternion4D value, Quaternion4D subend) => Subtract4D(value.X, value.Y, value.Z, value.W, subend.X, subend.Y, subend.Z, subend.W);

        /// <summary>
        /// Scale
        /// </summary>
        /// <param name="factor">The Point</param>
        /// <param name="scalar">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator *(Quaternion4D factor, double scalar) => Scale4D(factor.X, factor.Y, factor.Z, factor.W, scalar);

        /// <summary>
        /// Scale
        /// </summary>
        /// <param name="scalar">The Multiplier</param>
        /// <param name="factor">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator *(double scalar, Quaternion4D factor) => Scale4D(factor.X, factor.Y, factor.Z, factor.W, scalar);

        /// <summary>
        /// Multiply
        /// </summary>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator *(Quaternion4D value, Quaternion4D scalar) => MultiplyQuaternions(value.X, value.Y, value.Z, value.W, scalar.X, scalar.Y, scalar.Z, scalar.W);

        /// <summary>
        /// Divide
        /// </summary>
        /// <param name="divisor">The divisor</param>
        /// <param name="dividend">The dividend</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator /(Quaternion4D divisor, double dividend) => DivideByDividend4D(divisor.X, divisor.Y, divisor.Z, divisor.W, dividend);

        /// <summary>
        /// Divide
        /// </summary>
        /// <param name="divisor">The divisor</param>
        /// <param name="dividend">The dividend</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator /(double divisor, Quaternion4D dividend) => DivideDivisor4D(divisor, dividend.X, dividend.Y, dividend.Z, dividend.W);

        /// <summary>
        /// Divide
        /// </summary>
        /// <param name="divisor"></param>
        /// <param name="dividend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D operator /(Quaternion4D divisor, Quaternion4D dividend) => DivideQuaternions(divisor.X, divisor.Y, divisor.Z, divisor.W, dividend.X, dividend.Y, dividend.Z, dividend.W);

        /// <summary>
        /// Compares two <see cref="Quaternion4D"/> instances for exact equality.
        /// The result specifies whether the values of the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, and <see cref="W"/>
        /// values of the two <see cref="Quaternion4D"/> objects are equal.
        /// </summary>
        /// <param name="left">The first <see cref="Quaternion4D"/> to compare</param>
        /// <param name="right">The second <see cref="Quaternion4D"/> to compare</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="Quaternion4D"/> instances are exactly equal.
        /// The return value is true if they are equal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Quaternion4D left, Quaternion4D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Quaternion4D"/> instances for exact inequality.
        /// The result specifies whether the values of the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, or <see cref="W"/>
        /// values of the two <see cref="Quaternion4D"/> objects are unequal.
        /// </summary>
        /// <param name="left">The first <see cref="Quaternion4D"/> to compare.</param>
        /// <param name="right">The second <see cref="Quaternion4D"/> to compare.</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="Quaternion4D"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Quaternion4D left, Quaternion4D right) => !Equals(left, right);

        /// <summary>
        /// Converts the specified <see cref="Quaternion4D"/> structure to a <see cref="ValueTuple{T1, T2, T3, T4}"/> structure.
        /// </summary>
        /// <param name="quaternion">The <see cref="Quaternion4D"/> to be converted.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double X, double Y, double Z, double W)(Quaternion4D quaternion) => (quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

        /// <summary>
        /// Tuple to <see cref="Quaternion4D"/>.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Quaternion4D((double X, double Y, double Z, double W) tuple) => new Quaternion4D(tuple);
        #endregion Operators

        #region Factories
        /// <summary>
        /// set this quaternion's values from the rotation matrix built from the Axi.
        /// </summary>
        /// <param name="XAxis"></param>
        /// <param name="YAxis"></param>
        /// <param name="ZAxis"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D FromAxis(Vector3D XAxis, Vector3D YAxis, Vector3D ZAxis)
            => QuaternionFromRotationMatrix(
                XAxis.I, YAxis.I, ZAxis.I,
                XAxis.J, YAxis.J, ZAxis.J,
                XAxis.K, YAxis.K, ZAxis.K);

        /// <summary>
        /// The from axis angle.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D FromAxisAngle(Vector3D axis, double angle) => QuaternionFromAxisAngle(axis.I, axis.J, axis.K, angle);

        /// <summary>
        /// The from Euler angles.
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <param name="pitch">The pitch.</param>
        /// <param name="yaw">The yaw.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D FromEulerAngles(double roll, double pitch, double yaw) => QuaternionFromEulerAngles(roll, pitch, yaw);

        /// <summary>
        /// Parse.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [ParseMethod]
        public static Quaternion4D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="FormatException">The parts of the vectors must be decimal numbers</exception>
        public static Quaternion4D Parse(string source, IFormatProvider provider)
        {
            var sep = Tokenizer.GetNumericListSeparator(provider);
            var vals = source.Replace("Quaternion", string.Empty).Trim(' ', '{', '(', '[', '<', '}', ')', ']', '>').Split(sep);

            if (vals.Length != 4)
            {
                throw new FormatException($"Cannot parse the text '{source}' because it does not have 4 parts separated by commas in the form (x,y,z,w) with optional parenthesis.");
            }
            else
            {
                try
                {
                    return new Quaternion4D(
                        double.Parse(vals[0].Trim()),
                        double.Parse(vals[1].Trim()),
                        double.Parse(vals[2].Trim()),
                        double.Parse(vals[3].Trim()));
                }
                catch (Exception ex)
                {
                    throw new FormatException("The parts of the vectors must be decimal numbers", ex);
                }
            }
        }
        #endregion Factories

        #region Methods
        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);

        /// <summary>
        /// Compares two <see cref="Quaternion4D"/> instances for exact equality.
        /// </summary>
        /// <param name="a">The first <see cref="Quaternion4D"/> to compare.</param>
        /// <param name="b">The second <see cref="Quaternion4D"/> to compare.</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="Quaternion4D"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Quaternion4D a, Quaternion4D b) => (a.X == b.X) && (a.Y == b.Y) && (a.Z == b.Z) && (a.W == b.W);

        /// <summary>
        /// Compares this <see cref="Quaternion4D"/> with the passed in object.
        /// </summary>
        /// <param name="obj">The object to compare to this <see cref="Quaternion4D"/> to.</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="Quaternion4D"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// In this equality Double.NaN is equal to itself, unlike in numeric equality.
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Quaternion4D && Equals(this, (Quaternion4D)obj);

        /// <summary>
        /// Compares this <see cref="Quaternion4D"/> with the passed in <see cref="Quaternion4D"/>.
        /// </summary>
        /// <param name="value">The <see cref="Quaternion4D"/> to compare to this <see cref="Quaternion4D"/> to.</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="Quaternion4D"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// In this equality Double.NaN is equal to itself, unlike in numeric equality.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Quaternion4D value) => Equals(this, value);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Quaternion4D"/> struct.
        /// </summary>
        /// <returns>A string representation of this <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => base.ToString();

        /// <summary>
        /// Creates a string representation of this <see cref="Quaternion4D"/> class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The <see cref="CultureInfo"/> provider.</param>
        /// <returns>A string representation of this <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Quaternion4D);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Quaternion4D)}=[{nameof(X)}:{X.ToString(format, provider)}{s} {nameof(Y)}:{Y.ToString(format, provider)}{s} {nameof(Z)}:{Z.ToString(format, provider)}{s} {nameof(W)}:{W.ToString(format, provider)}]";
        }
        #endregion Methods
    }
}
