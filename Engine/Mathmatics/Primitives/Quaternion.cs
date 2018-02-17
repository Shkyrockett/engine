// <copyright file="QuaternionD.cs" company="Shkyrockett" >
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
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract, Serializable]
    [ComVisible(true)]
    [TypeConverter(typeof(StructConverter<QuaternionD>))]
    public struct QuaternionD
        : IEquatable<QuaternionD>, IFormattable
    {
        #region Static Fields
        /// <summary>
        /// The empty.
        /// </summary>
        public static QuaternionD Empty = new QuaternionD(0, 0, 0, 0);

        /// <summary>
        /// The zero.
        /// </summary>
        public static QuaternionD Zero = Empty;

        /// <summary>
        /// The identity.
        /// </summary>
        public static QuaternionD Identity = new QuaternionD(0, 0, 0, 1);
        #endregion Static Fields

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuple"></param>
        public QuaternionD((double X, double Y, double Z, double W) tuple)
        {
            (X, Y, Z, W) = tuple;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public QuaternionD(double x, double y, double z, double w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scalar"></param>
        public QuaternionD(Vector3D vector, double scalar)
            : this(vector.I, vector.J, vector.K, scalar)
        { }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the X value of this Quaternion. 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y value of this Quaternion. 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the Z value of this Quaternion. 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Z { get; set; }

        /// <summary>
        /// Gets or sets the W value of this Quaternion. 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double W { get; set; }

        /// <summary>
        /// Gets the squared 'length' of this quaternion.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Normal
            => Measurements.QuaternionNormal( X, Y, Z, W);

        /// <summary>
        /// Gets the squared 'length' of this quaternion.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double LengthSquared
            => Measurements.QuaternionNormal( X, Y, Z, W);

        /// <summary>
        /// Gets the 'length' of this quaternion.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Length
            => Measurements.QuaternionMagnitude( X, Y, Z, W);

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Pitch
        {
            set
            {
                var euler = this.ToEulerAngles();
                FromEulerAngles(euler.Roll, value, euler.Yaw);
            }
            get
            {

                var test = X * Y + Z * W;
                if (Abs(test) > 0.499d) // singularitY at north and south pole
                    return 0d;
                return Atan2(2d * X * W - 2d * Y * Z, 1d - 2d * X * X - 2d * Z * Z);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Yaw
        {
            set
            {
                var euler = this.ToEulerAngles();
                FromEulerAngles(euler.Roll, euler.Pitch, value);
            }
            get
            {
                var test = X * Y + Z * W;
                if (Abs(test) > 0.499d) // singularitY at north and south pole
                    return Sign(test) * 2d * Atan2(X, W);
                return Atan2(2d * Y * W - 2d * X * Z, 1d - 2d * Y * Y - 2d * Z * Z);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Roll
        {
            set
            {
                var euler = this.ToEulerAngles();
                FromEulerAngles(value, euler.Pitch, euler.Yaw);
            }
            get
            {
                var test = X * Y + Z * W;
                if (Abs(test) > 0.499d) // singularitY at north and south pole
                    return Sign(test) * Right;
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
                var fTX = 2.0f * X;
                var fTY = 2.0f * Y;
                var fTZ = 2.0f * Z;
                var fTWY = fTY * W;
                var fTWZ = fTZ * W;
                var fTXY = fTY * X;
                var fTXZ = fTZ * X;
                var fTYY = fTY * Y;
                var fTZZ = fTZ * Z;
                return new Vector3D(1.0d - (fTYY + fTZZ), fTXY + fTWZ, fTXZ - fTWY);
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
                var fTX = 2.0f * X;
                var fTY = 2.0f * Y;
                var fTZ = 2.0f * Z;
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
                var fTX = 2.0f * X;
                var fTY = 2.0f * Y;
                var fTZ = 2.0f * Z;
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
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static QuaternionD operator +(QuaternionD value)
            => new QuaternionD(+value.X, +value.Y, +value.Z, +value.W);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static QuaternionD operator +(QuaternionD value, double addend)
            => value.Add(addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static QuaternionD operator +(QuaternionD value, QuaternionD addend)
            => value.Add(addend);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static QuaternionD operator -(QuaternionD value)
            => new QuaternionD(-value.X, -value.Y, -value.Z, -value.W);

        /// <summary>
        /// Subtract
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static QuaternionD operator -(QuaternionD value, double subend)
            => value.Subtract(subend);

        /// <summary>
        /// Subtract
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static QuaternionD operator -(QuaternionD value, QuaternionD subend)
            => value.Subtract(subend);

        /// <summary>
        /// Scale
        /// </summary>
        /// <param name="value">The Point</param>
        /// <param name="scalar">The Multiplier</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static QuaternionD operator *(QuaternionD value, double scalar)
            => value.Scale(scalar);

        /// <summary>
        /// Scale
        /// </summary>
        /// <param name="scalar">The Multiplier</param>
        /// <param name="value">The Point</param>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static QuaternionD operator *(double scalar, QuaternionD value)
            => value.Scale(scalar);

        /// <summary>
        /// Multiply
        /// </summary>
        /// <returns>A Point Multiplied by the Multiplier</returns>
        /// <remarks></remarks>
        public static QuaternionD operator *(QuaternionD value, QuaternionD scalar)
            => value.Multiply(scalar);

        /// <summary>
        /// Divide
        /// </summary>
        /// <param name="divisor"></param>
        /// <param name="dividend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static QuaternionD operator /(QuaternionD divisor, QuaternionD dividend)
            => divisor.Divide(dividend);

        /// <summary>
        /// Compares two <see cref="QuaternionD"/> instances for exact equality.
        /// </summary>
        /// <param name="a">The first <see cref="QuaternionD"/> to compare</param>
        /// <param name="b">The second <see cref="QuaternionD"/> to compare</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="QuaternionD"/> instances are exactly equal.
        /// The return value is true if they are equal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        public static bool operator ==(QuaternionD a, QuaternionD b)
            => Equals(a, b);

        /// <summary>
        /// Compares two <see cref="QuaternionD"/> instances for exact inequality.
        /// </summary>
        /// <param name="a">The first <see cref="QuaternionD"/> to compare</param>
        /// <param name="b">The second <see cref="QuaternionD"/> to compare</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="QuaternionD"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        public static bool operator !=(QuaternionD a, QuaternionD b)
            => !Equals(a, b);

        /// <summary>
        /// Tupple to <see cref="QuaternionD"/>.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator QuaternionD((double X, double Y, double Z, double W) tuple)
            => new QuaternionD(tuple);
        #endregion Operators

        #region Factories
        /// <summary>
        /// set this quaternion's values from the rotation matrix built from the Axi.
        /// </summary>
        /// <param name="XAxis"></param>
        /// <param name="YAxis"></param>
        /// <param name="ZAxis"></param>
        public static QuaternionD FromAxis(Vector3D XAxis, Vector3D YAxis, Vector3D ZAxis)
            => FromRotationMatrix(new Matrix3x3D(
                XAxis.I, YAxis.I, ZAxis.I,
                XAxis.J, YAxis.J, ZAxis.J,
                XAxis.K, YAxis.K, ZAxis.K));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static QuaternionD FromAxisAngle(Vector3D axis, double angle)
        {
            var halfAngle = angle * 0.5d;
            var sin = Sin(halfAngle);
            var cos = Cos(halfAngle);
            return new QuaternionD(axis.I * sin, axis.J * sin, axis.K * sin, cos);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static QuaternionD FromRotationMatrix(Matrix3x3D matrix)
        {
            var trace = (matrix.M0x0 + matrix.M1x1) + matrix.M2x2;
            var quaternion = new QuaternionD();
            if (trace > 0d)
            {
                var root = Sqrt(trace + 1d);
                quaternion.W = root * 0.5d;
                root = 0.5d / root;
                quaternion.X = (matrix.M1x2 - matrix.M2x1) * root;
                quaternion.Y = (matrix.M2x0 - matrix.M0x2) * root;
                quaternion.Z = (matrix.M0x1 - matrix.M1x0) * root;
                return quaternion;
            }
            if ((matrix.M0x0 >= matrix.M1x1) && (matrix.M0x0 >= matrix.M2x2))
            {
                var root = Sqrt(((1f + matrix.M0x0) - matrix.M1x1) - matrix.M2x2);
                var w = 0.5d / root;
                quaternion.X = 0.5d * root;
                quaternion.Y = (matrix.M0x1 + matrix.M1x0) * w;
                quaternion.Z = (matrix.M0x2 + matrix.M2x0) * w;
                quaternion.W = (matrix.M1x2 - matrix.M2x1) * w;
                return quaternion;
            }
            if (matrix.M1x1 > matrix.M2x2)
            {
                var root = Sqrt(((1f + matrix.M1x1) - matrix.M0x0) - matrix.M2x2);
                var w = 0.5d / root;
                quaternion.X = (matrix.M1x0 + matrix.M0x1) * w;
                quaternion.Y = 0.5d * root;
                quaternion.Z = (matrix.M2x1 + matrix.M1x2) * w;
                quaternion.W = (matrix.M2x0 - matrix.M0x2) * w;
                return quaternion;
            }
            var sqrt = Sqrt(((1f + matrix.M2x2) - matrix.M0x0) - matrix.M1x1);
            var ww = 0.5d / sqrt;
            quaternion.X = (matrix.M2x0 + matrix.M0x2) * ww;
            quaternion.Y = (matrix.M2x1 + matrix.M1x2) * ww;
            quaternion.Z = 0.5d * sqrt;
            quaternion.W = (matrix.M0x1 - matrix.M1x0) * ww;
            return quaternion;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yaw"></param>
        /// <param name="pitch"></param>
        /// <param name="roll"></param>
        /// <returns></returns>
        public static QuaternionD FromEulerAngles(double roll, double pitch, double yaw)
        {
            var halfRoll = roll * 0.5d;
            var rollSin = Sin(halfRoll);
            var rollCos = Cos(halfRoll);
            var halfPitch = pitch * 0.5d;
            var pitchSin = Sin(halfPitch);
            var pitchCos = Cos(halfPitch);
            var halfYaw = yaw * 0.5d;
            var yawSin = Sin(halfYaw);
            var yawCos = Cos(halfYaw);
            return new QuaternionD(
                ((yawCos * pitchSin) * rollCos) + ((yawSin * pitchCos) * rollSin),
                ((yawSin * pitchCos) * rollCos) - ((yawCos * pitchSin) * rollSin),
                ((yawCos * pitchCos) * rollSin) - ((yawSin * pitchSin) * rollCos),
                ((yawCos * pitchCos) * rollCos) + ((yawSin * pitchSin) * rollSin));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [ParseMethod]
        public static QuaternionD Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static QuaternionD Parse(string source, IFormatProvider provider)
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
                    return new QuaternionD(
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

        #region Methods
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => X.GetHashCode()
            ^ Y.GetHashCode()
            ^ Z.GetHashCode()
            ^ W.GetHashCode();

        /// <summary>
        /// Compares two <see cref="QuaternionD"/> structs.
        /// </summary>
        /// <param name="a">The object to comare.</param>
        /// <param name="b">The object to compare against.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(QuaternionD a, QuaternionD b)
            => Equals(a, b);

        /// <summary>
        /// Compares two <see cref="QuaternionD"/> instances for exact equality.
        /// </summary>
        /// <param name="a">The first <see cref="QuaternionD"/> to compare</param>
        /// <param name="b">The second <see cref="QuaternionD"/> to compare</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="QuaternionD"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(QuaternionD a, QuaternionD b)
            => a.X == b.X
             & a.Y == b.Y
             & a.Z == b.Z
             & a.W == b.W;

        /// <summary>
        /// Compares this <see cref="QuaternionD"/> with the passed in object.
        /// </summary>
        /// <param name="obj">The object to compare to this <see cref="QuaternionD"/> to.</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="QuaternionD"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// In this equality Double.NaN is equal to itself, unlike in numeric equality.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is QuaternionD && Equals(this, (QuaternionD)obj);

        /// <summary>
        /// Compares this <see cref="QuaternionD"/> with the passed in <see cref="QuaternionD"/>.
        /// </summary>
        /// <param name="value">The <see cref="QuaternionD"/> to compare to this <see cref="QuaternionD"/> to.</param>
        /// <returns>
        /// A boolean value indicating whether the two <see cref="QuaternionD"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// In this equality Double.NaN is equal to itself, unlike in numeric equality.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(QuaternionD value)
            => Equals(this, value);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="QuaternionD"/>.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="QuaternionD"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="QuaternionD"/> class based on the format string
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
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="QuaternionD"/> class based on the format string
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
        public string ConvertToString(string format, IFormatProvider provider)
        {
            var sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(QuaternionD)}{{{nameof(X)}={X.ToString(format, provider)}{sep}{nameof(Y)}={Y.ToString(format, provider)}{sep}{nameof(Z)}={Z.ToString(format, provider)}{sep}{nameof(W)}={W.ToString(format, provider)}}}";
        }
        #endregion Methods
    }
}
