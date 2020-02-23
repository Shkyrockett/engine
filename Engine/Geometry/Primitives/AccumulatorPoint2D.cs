// <copyright file="AccumulatorPoint2D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
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
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The <see cref="AccumulatorPoint2D"/> struct.
    /// </summary>
    [DataContract, Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DebuggerDisplay("{ToString()}")]
    public struct AccumulatorPoint2D
        : IVector<AccumulatorPoint2D>
    {
        #region Implementations
        /// <summary>
        /// Represents a <see cref="AccumulatorPoint2D"/> that has <see cref="X"/>, and <see cref="Y"/> values set to zero.
        /// </summary>
        public static readonly AccumulatorPoint2D Empty = new AccumulatorPoint2D(0d, 0d);

        /// <summary>
        /// A Unit <see cref="AccumulatorPoint2D"/>.
        /// </summary>
        public static readonly AccumulatorPoint2D Unit = new AccumulatorPoint2D(1d, 1d);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AccumulatorPoint2D"/> class.
        /// </summary>
        /// <param name="accumulatorPoint2D">The accumulatorPoint2D.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AccumulatorPoint2D(AccumulatorPoint2D accumulatorPoint2D)
            : this(accumulatorPoint2D.X, accumulatorPoint2D.Y, accumulatorPoint2D.Theta, accumulatorPoint2D.TotalDistance, accumulatorPoint2D.PreviousIndex)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccumulatorPoint2D"/> class.
        /// </summary>
        /// <param name="point">The point.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AccumulatorPoint2D(Point2D point)
            : this(point.X, point.Y, 0d, 0d, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccumulatorPoint2D"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AccumulatorPoint2D(double x, double y)
            : this(x, y, 0d, 0d, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccumulatorPoint2D"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="theta">The theta.</param>
        /// <param name="totalDistance">The totalDistance.</param>
        /// <param name="previousIndex">The previous index.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AccumulatorPoint2D(double x, double y, double theta = 0, double totalDistance = 0, int previousIndex = 0)
            : this()
        {
            X = x;
            Y = y;
            Theta = theta;
            TotalDistance = totalDistance;
            PreviousIndex = previousIndex;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="AccumulatorPoint2D"/> to a <see cref="ValueTuple{T1, T2}"/>.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double x, out double y)
        {
            x = X;
            y = Y;
        }

        /// <summary>
        /// Deconstruct this <see cref="AccumulatorPoint2D"/> to a <see cref="ValueTuple{T1, T2, T3, T4}"/>.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="theta">The theta.</param>
        /// <param name="totalDistance">The totalDistance.</param>
        /// <param name="previousIndex">The previousIndex.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double x, out double y, out double theta, out double totalDistance, out int previousIndex)
        {
            x = X;
            y = Y;
            theta = Theta;
            totalDistance = TotalDistance;
            previousIndex = PreviousIndex;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the X component of a <see cref="AccumulatorPoint2D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y component of a <see cref="AccumulatorPoint2D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the Theta index value.
        /// </summary>
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Theta { get; set; }

        /// <summary>
        /// Gets or sets the total distance.
        /// </summary>
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the previous index.
        /// </summary>
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int PreviousIndex { get; set; }

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
        /// The result specifies whether the values of the <see cref="X"/>, and <see cref="Y"/>
        /// values of the two <see cref="AccumulatorPoint2D"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(AccumulatorPoint2D left, AccumulatorPoint2D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="AccumulatorPoint2D"/> objects.
        /// The result specifies whether the values of the <see cref="X"/>, or <see cref="Y"/>
        /// values of the two <see cref="AccumulatorPoint2D"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(AccumulatorPoint2D left, AccumulatorPoint2D right) => !Equals(left, right);

        /// <summary>
        /// Implicit conversion to ItPoint2D.
        /// </summary>
        /// <returns>
        /// </returns>
        /// <param name="point"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator AccumulatorPoint2D(Point2D point) => new AccumulatorPoint2D(point.X, point.Y);

        /// <summary>
        /// Converts the specified <see cref="AccumulatorPoint2D"/> structure to a <see cref="Point2D"/> structure.
        /// </summary>
        /// <param name="point">The <see cref="AccumulatorPoint2D"/> to be converted.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Point2D(AccumulatorPoint2D point) => new Point2D(point.X, point.Y);

        /// <summary>
        /// Converts the specified <see cref="AccumulatorPoint2D"/> structure to a <see cref="ValueTuple{T1, T2}"/> structure.
        /// </summary>
        /// <param name="point">The <see cref="AccumulatorPoint2D"/> to be converted.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double X, double Y)(AccumulatorPoint2D point) => (point.X, point.Y);
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

        #region Methods
        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(AccumulatorPoint2D a, AccumulatorPoint2D b) => Equals(a, b);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(AccumulatorPoint2D a, AccumulatorPoint2D b) => (a.X == b.X) & (a.Y == b.Y);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is AccumulatorPoint2D && Equals(this, (AccumulatorPoint2D)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(AccumulatorPoint2D value) => Equals(this, value);

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

        /// <summary>
        /// The to point.
        /// </summary>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2D ToPoint() => new Point2D(X, Y);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="AccumulatorPoint2D"/> struct.
        /// </summary>
        /// <returns>A string representation of this <see cref="AccumulatorPoint2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="AccumulatorPoint2D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">The <see cref="CultureInfo"/> provider.</param>
        /// <returns>A string representation of this <see cref="AccumulatorPoint2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider) => ToString("R" /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="AccumulatorPoint2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The <see cref="CultureInfo"/> provider.</param>
        /// <returns>A string representation of this <see cref="AccumulatorPoint2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(AccumulatorPoint2D);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(AccumulatorPoint2D)}({nameof(X)}:{X.ToString(format, provider)}{s} {nameof(Y)}:{Y.ToString(format, provider)}{s} {nameof(Theta)}:{Theta.ToString(format, provider)}{s} {nameof(TotalDistance)}:{TotalDistance.ToString(format, provider)}{s} {nameof(PreviousIndex)}:{PreviousIndex.ToString(format, provider)})";
        }
        #endregion Methods
    }
}
