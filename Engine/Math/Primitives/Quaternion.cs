// <copyright file="QuaternionD.cs" company="Shkyrockett" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
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
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    [ComVisible(true)]
    [TypeConverter(typeof(StructConverter<QuaternionD>))]
    public struct QuaternionD
        : IEquatable<QuaternionD>, IFormattable
    {
        #region Static Fields

        /// <summary>
        /// 
        /// </summary>
        public static QuaternionD Empty = new QuaternionD(0, 0, 0, 0);

        /// <summary>
        /// 
        /// </summary>
        public static QuaternionD Zero = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static QuaternionD Identity = new QuaternionD(0, 0, 0, 1);

        #endregion

        #region Feilds

        /// <summary>
        /// 
        /// </summary>
        private double x;

        /// <summary>
        /// 
        /// </summary>
        private double y;

        /// <summary>
        /// 
        /// </summary>
        private double z;

        /// <summary>
        /// 
        /// </summary>
        private double w;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuple"></param>
        public QuaternionD((double X, double Y, double Z, double W) tuple)
            => (x, y, z, w) = tuple;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public QuaternionD(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scalar"></param>
        public QuaternionD(Vector3D vector, double scalar)
            : this(vector.I, vector.J, vector.K, scalar)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [XmlAttribute]
        public double X { get => x; set => x = value; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [XmlAttribute]
        public double Y { get => y; set => y = value; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [XmlAttribute]
        public double Z { get => z; set => z = value; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [XmlAttribute]
        public double W { get => w; set => w = value; }

        /// <summary>
        /// Squared 'length' of this quaternion.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public double Normal
            => X * X + Y * Y + Z * Z + W * W;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public double Pitch
        {
            set
            {
                var euler = this.ToEulerAngles();
                FromEulerAngles(euler.Roll, value, euler.Yaw);
            }
            get
            {

                double test = X * Y + Z * W;
                if (Abs(test) > 0.499d) // singularitY at north and south pole
                    return 0d;
                return Atan2(2d * X * W - 2d * Y * Z, 1d - 2d * X * X - 2d * Z * Z);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public double Yaw
        {
            set
            {
                var euler = this.ToEulerAngles();
                FromEulerAngles(euler.Roll, euler.Pitch, value);
            }
            get
            {
                double test = X * Y + Z * W;
                if (Abs(test) > 0.499d) // singularitY at north and south pole
                    return Sign(test) * 2d * Atan2(X, W);
                return Atan2(2d * Y * W - 2d * X * Z, 1d - 2d * Y * Y - 2d * Z * Z);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public double Roll
        {
            set
            {
                var euler = this.ToEulerAngles();
                FromEulerAngles(value, euler.Pitch, euler.Yaw);
            }
            get
            {
                double test = X * Y + Z * W;
                if (Abs(test) > 0.499d) // singularitY at north and south pole
                    return Sign(test) * HalfPi;
                return Asin(2d * test);
            }
        }

        /// <summary>
        /// Local X-aXis portion of this rotation.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Vector3D XAxis
        {
            get
            {
                double fTX = 2.0f * X;
                double fTY = 2.0f * Y;
                double fTZ = 2.0f * Z;
                double fTWY = fTY * W;
                double fTWZ = fTZ * W;
                double fTXY = fTY * X;
                double fTXZ = fTZ * X;
                double fTYY = fTY * Y;
                double fTZZ = fTZ * Z;
                return new Vector3D(1.0f - (fTYY + fTZZ), fTXY + fTWZ, fTXZ - fTWY);
            }
        }

        /// <summary>
        /// Local Y-aXis portion of this rotation.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Vector3D YAxis
        {
            get
            {
                double fTX = 2.0f * X;
                double fTY = 2.0f * Y;
                double fTZ = 2.0f * Z;
                double fTWX = fTX * W;
                double fTWZ = fTZ * W;
                double fTXX = fTX * X;
                double fTXY = fTY * X;
                double fTYZ = fTZ * Y;
                double fTZZ = fTZ * Z;
                return new Vector3D(fTXY - fTWZ, 1d - (fTXX + fTZZ), fTYZ + fTWX);
            }
        }

        /// <summary>
        /// Local Z-aXis portion of this rotation.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Vector3D ZAxis
        {
            get
            {
                double fTX = 2.0f * X;
                double fTY = 2.0f * Y;
                double fTZ = 2.0f * Z;
                double fTWX = fTX * W;
                double fTWY = fTY * W;
                double fTXX = fTX * X;
                double fTXZ = fTZ * X;
                double fTYY = fTY * Y;
                double fTYZ = fTZ * Y;
                return new Vector3D(fTXZ + fTWY, fTYZ - fTWX, 1d - (fTXX + fTYY));
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static QuaternionD operator +(QuaternionD value)
            => new QuaternionD(+value.x, +value.y, +value.z, +value.w);

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
            => new QuaternionD(-value.x, -value.y, -value.z, -value.w);

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
        /// A boolian value indicating whether the two <see cref="QuaternionD"/> instances are exactly equal.
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
        /// A boolian value indicating whether the two <see cref="QuaternionD"/> instances are exactly unequal.
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

        #endregion

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
            double halfAngle = angle * 0.5d;
            double sin = Sin(halfAngle);
            double cos = Cos(halfAngle);
            return new QuaternionD(axis.I * sin, axis.J * sin, axis.K * sin, cos);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static QuaternionD FromRotationMatrix(Matrix3x3D matrix)
        {
            double trace = (matrix.M0x0 + matrix.M1x1) + matrix.M2x2;
            QuaternionD quaternion = new QuaternionD();
            if (trace > 0d)
            {
                double root = Sqrt(trace + 1d);
                quaternion.W = root * 0.5d;
                root = 0.5d / root;
                quaternion.X = (matrix.M1x2 - matrix.M2x1) * root;
                quaternion.Y = (matrix.M2x0 - matrix.M0x2) * root;
                quaternion.Z = (matrix.M0x1 - matrix.M1x0) * root;
                return quaternion;
            }
            if ((matrix.M0x0 >= matrix.M1x1) && (matrix.M0x0 >= matrix.M2x2))
            {
                double root = Sqrt(((1f + matrix.M0x0) - matrix.M1x1) - matrix.M2x2);
                double w = 0.5d / root;
                quaternion.X = 0.5d * root;
                quaternion.Y = (matrix.M0x1 + matrix.M1x0) * w;
                quaternion.Z = (matrix.M0x2 + matrix.M2x0) * w;
                quaternion.W = (matrix.M1x2 - matrix.M2x1) * w;
                return quaternion;
            }
            if (matrix.M1x1 > matrix.M2x2)
            {
                double root = Sqrt(((1f + matrix.M1x1) - matrix.M0x0) - matrix.M2x2);
                double w = 0.5d / root;
                quaternion.X = (matrix.M1x0 + matrix.M0x1) * w;
                quaternion.Y = 0.5d * root;
                quaternion.Z = (matrix.M2x1 + matrix.M1x2) * w;
                quaternion.W = (matrix.M2x0 - matrix.M0x2) * w;
                return quaternion;
            }
            double sqrt = Sqrt(((1f + matrix.M2x2) - matrix.M0x0) - matrix.M1x1);
            double ww = 0.5d / sqrt;
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
            double halfRoll = roll * 0.5d;
            double rollSin = Sin(halfRoll);
            double rollCos = Cos(halfRoll);
            double halfPitch = pitch * 0.5d;
            double pitchSin = Sin(halfPitch);
            double pitchCos = Cos(halfPitch);
            double halfYaw = yaw * 0.5d;
            double yawSin = Sin(halfYaw);
            double yawCos = Cos(halfYaw);
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
            char sep = Tokenizer.GetNumericListSeparator(provider);
            string[] vals = source.Replace("Quaternion", string.Empty).Trim(' ', '{', '(', '[', '<', '}', ')', ']', '>').Split(sep);

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

        #endregion

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
        /// A boolian value indicating whether the two <see cref="QuaternionD"/> instances are exactly unequal.
        /// The return value is true if they are unequal, false otherwise.
        /// </returns>
        /// <remarks>
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(QuaternionD a, QuaternionD b)
            => a.x == b.x
             & a.y == b.y
             & a.z == b.z
             & a.w == b.w;

        /// <summary>
        /// Compares this <see cref="QuaternionD"/> with the passed in object.
        /// </summary>
        /// <param name="obj">The object to compare to this <see cref="QuaternionD"/> to.</param>
        /// <returns>
        /// A boolian value indicating whether the two <see cref="QuaternionD"/> instances are exactly unequal.
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
        /// A boolian value indicating whether the two <see cref="QuaternionD"/> instances are exactly unequal.
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
            char sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(QuaternionD)}{{{nameof(X)}={X.ToString(format, provider)}{sep}{nameof(Y)}={Y.ToString(format, provider)}{sep}{nameof(Z)}={Z.ToString(format, provider)}{sep}{nameof(W)}={W.ToString(format, provider)}}}";
        }

        #endregion
    }
}
