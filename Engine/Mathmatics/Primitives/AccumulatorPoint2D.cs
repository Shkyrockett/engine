// <copyright file="AccumulatorPoint2D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using static System.Math;
using static Engine.Maths;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract, Serializable]
    public class AccumulatorPoint2D
        : IFormattable
    {
        #region Implementations

        /// <summary>
        /// An Empty <see cref="AccumulatorPoint2D"/>.
        /// </summary>
        public static readonly AccumulatorPoint2D Empty = new AccumulatorPoint2D();

        /// <summary>
        /// A Unit <see cref="AccumulatorPoint2D"/>.
        /// </summary>
        public static readonly AccumulatorPoint2D Unit = new AccumulatorPoint2D(1, 1);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccumulatorPoint2D"/> class.
        /// </summary>
        /// <remarks></remarks>
        public AccumulatorPoint2D()
            : this(0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccumulatorPoint2D"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <remarks></remarks>
        public AccumulatorPoint2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// X component of a <see cref="AccumulatorPoint2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [DataMember, XmlAttribute, SoapAttribute]
        public double X { get; set; }

        /// <summary>
        /// Y component of a <see cref="AccumulatorPoint2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double TotalDistance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Previous { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point2D"/> is empty.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public bool IsEmpty
            => Abs(X) < Epsilon
            && Abs(Y) < Epsilon;

        #endregion

        #region Operators

        /// <summary>
        /// Compares two <see cref="AccumulatorPoint2D"/> objects. 
        /// The result specifies whether the values of the <see cref="X"/> and <see cref="Y"/> 
        /// values of the two <see cref="AccumulatorPoint2D"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(AccumulatorPoint2D left, AccumulatorPoint2D right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="AccumulatorPoint2D"/> objects. 
        /// The result specifies whether the values of the <see cref="X"/> or <see cref="Y"/> 
        /// values of the two <see cref="AccumulatorPoint2D"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(AccumulatorPoint2D left, AccumulatorPoint2D right)
            => !Equals(left, right);

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(AccumulatorPoint2D a, AccumulatorPoint2D b)
            => Equals(a, b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(AccumulatorPoint2D a, AccumulatorPoint2D b)
            => a?.X == b?.X & a?.Y == b?.Y & a?.Previous == b?.Previous & a?.TotalDistance == b?.TotalDistance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is AccumulatorPoint2D && Equals(this, obj as AccumulatorPoint2D);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(AccumulatorPoint2D value)
            => Equals(this, value);

        /// <summary>
        /// Explicit conversion to Point2D.
        /// </summary>
        /// <returns>
        /// </returns>
        /// <param name="point"></param>
        public static explicit operator Point2D(AccumulatorPoint2D point)
            => new Point2D(point.X, point.Y);

        /// <summary>
        /// Implicit conversion to ItPoint2D.
        /// </summary>
        /// <returns>
        /// </returns>
        /// <param name="point"></param>
        public static implicit operator AccumulatorPoint2D(Point2D point)
            => new AccumulatorPoint2D(point.X, point.Y);

        #endregion

        #region Factories

        /// <summary>
        /// Parse a string for a <see cref="AccumulatorPoint2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="AccumulatorPoint2D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="AccumulatorPoint2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Point2D Parse(string source)
        {
            var tokenizer = new Tokenizer(source, CultureInfo.InvariantCulture);
            var value = new Point2D(
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture),
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture));
            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }

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

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => X.GetHashCode()
            ^ Y.GetHashCode();

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="AccumulatorPoint2D"/> struct.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="AccumulatorPoint2D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="AccumulatorPoint2D"/> struct based on the format string
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
        /// Creates a string representation of this <see cref="AccumulatorPoint2D"/> struct based on the format string
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
            if (this == null) return nameof(AccumulatorPoint2D);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Point2D)}{{{nameof(X)}={X}{sep}{nameof(Y)}={Y}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
