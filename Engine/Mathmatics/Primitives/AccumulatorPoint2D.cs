// <copyright file="AccumulatorPoint2D.cs" company="Shkyrockett" >
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
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using static System.Math;
using static Engine.Maths;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// The accumulator point2d struct.
    /// </summary>
    [DataContract, Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    //[DebuggerDisplay("X: {X}, Y: {Y}, TotalDistance: {TotalDistance}, Previous: {Previous}")]
    public struct AccumulatorPoint2D
        : IVector<AccumulatorPoint2D>
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
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AccumulatorPoint2D"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="totalDistance">The totalDistance.</param>
        /// <param name="theta">The theta.</param>
        /// <param name="previous">The previous.</param>
        public AccumulatorPoint2D(double x, double y, double totalDistance = 0, double theta = 0, int previous = 0)
        {
            X = x;
            Y = y;
            TotalDistance = totalDistance;
            Theta = theta;
            Previous = previous;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccumulatorPoint2D"/> class.
        /// </summary>
        /// <param name="accumulatorPoint2D">The accumulatorPoint2D.</param>
        public AccumulatorPoint2D(AccumulatorPoint2D accumulatorPoint2D)
            : this(accumulatorPoint2D.X, accumulatorPoint2D.Y, accumulatorPoint2D.TotalDistance, accumulatorPoint2D.Previous)
        { }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// X component of a <see cref="AccumulatorPoint2D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double X { get; set; }

        /// <summary>
        /// Y component of a <see cref="AccumulatorPoint2D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the Theta index value.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Theta { get; set; }

        /// <summary>
        /// Gets or sets the total distance.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the previous index.
        /// </summary>
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
        #endregion Properties

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
        #endregion Operators

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
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(AccumulatorPoint2D a, AccumulatorPoint2D b)
            => Equals(a, b);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(AccumulatorPoint2D a, AccumulatorPoint2D b)
            => a.X == b.X & a.Y == b.Y & a.Previous == b.Previous & a.TotalDistance == b.TotalDistance;

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is AccumulatorPoint2D && Equals(this, (AccumulatorPoint2D)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(AccumulatorPoint2D value)
            => Equals(this, value);

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetHashCode()
            => X.GetHashCode()
            ^ Y.GetHashCode();

        /// <summary>
        /// The to point.
        /// </summary>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public Point2D ToPoint()
            => new Point2D(X, Y);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="AccumulatorPoint2D"/> struct.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="AccumulatorPoint2D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider)
            => ConvertToString(string.Empty /* format string */, provider);

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
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format /* format string */, provider /* format provider */);

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
        private string ConvertToString(string format, IFormatProvider provider)
        {
            //if (this is null) return nameof(AccumulatorPoint2D);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Point2D)}{{{nameof(X)}={X}{sep}{nameof(Y)}={Y}{sep}{nameof(TotalDistance)}={TotalDistance}{sep}{nameof(Previous)}={Previous}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
