// <copyright file="Range.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Numeric range class.
    /// </summary>
    /// <seealso cref="IEnumerable{T}" />
    /// <seealso cref="IEquatable{T}" />
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DebuggerDisplay("{ToString()}")]
    public struct NumericRange
        : IEnumerable<double>, IEquatable<NumericRange>
    {
        #region Implementations
        #endregion Implementations

        #region Factories
        /// <summary>
        /// Initializes a new instance of the <see cref="NumericRange" /> struct for iteration of a range from 0 to 1.
        /// </summary>
        /// <param name="count">The number of steps to have between 0 and 1.</param>
        /// <returns>
        /// A new <see cref="NumericRange" /> struct set up for 0 to 1 iteration.
        /// </returns>
        public static NumericRange IteratorRange(double count)
            => new NumericRange(0d, 1d, 0d, 1d, 1d / count, Overflow.Clamp);

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericRange" /> struct for iteration from the start for a designated length.
        /// </summary>
        /// <param name="start">The start value of the <see cref="NumericRange" />.</param>
        /// <param name="length">The length of the <see cref="NumericRange" />.</param>
        /// <param name="count">The number of steps to have between the start and end.</param>
        /// <returns>
        /// A new <see cref="NumericRange" /> struct set up for iteration from the start for a designated length.
        /// </returns>
        public static NumericRange StartLengthCountRange(double start, double length, int count)
            => new NumericRange(start, start + length, start, start + length, length / count, Overflow.Clamp);

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericRange" /> struct for iteration in Radians.
        /// </summary>
        /// <param name="start">The start angle in radians.</param>
        /// <param name="end">The end angle in radians.</param>
        /// <param name="count">The number of steps to have between the start and end.</param>
        /// <returns>
        /// A new <see cref="NumericRange" /> struct set up for iterating radians.
        /// </returns>
        public static NumericRange RadiansRange(double start, double end, int count)
            => new NumericRange(start, end, -PI, PI, (end - start) / count, Overflow.Wrap);

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericRange" /> struct for iteration in Degrees.
        /// </summary>
        /// <param name="start">The start angle in degrees.</param>
        /// <param name="end">The end angle in degrees.</param>
        /// <returns>
        /// A new <see cref="NumericRange" /> struct set up for iterating degrees.
        /// </returns>
        public static NumericRange DegreesRange(double start, double end)
            => new NumericRange(start, end, 0d, 360d, 1d, Overflow.Wrap);
        #endregion Factories

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NumericRange" /> struct;
        /// </summary>
        /// <param name="min">Minimum value of the <see cref="NumericRange" />.</param>
        /// <param name="max">Maximum value of the <see cref="NumericRange" />.</param>
        /// <param name="unitMin">Minimum value of a periodic unit that wraps back to the maximum value.</param>
        /// <param name="unitMax">Maximum value of a periodic unit that wraps back to the minimum value.</param>
        /// <param name="step">Amount to advance for each iteration</param>
        /// <param name="overflow">Overflow rules.</param>
        public NumericRange(double min, double max, double unitMin, double unitMax, double step, Overflow overflow = Overflow.Clamp)
        {
            Min = min;
            Max = max;
            UnitMin = unitMin;
            UnitMax = unitMax;
            Step = step;
            Overflow = overflow;
        }
        #endregion Constructors

        /// <summary>
        /// Provides access to the enumeration of the linear interpolation from the <see cref="Min" /> value to the <see cref="Max" /> value.
        /// </summary>
        /// <value>
        /// The <see cref="double" />.
        /// </value>
        /// <param name="index">The position between the min and max to interpolate.</param>
        /// <returns>
        /// The linear interpolation of a value between the <see cref="Min" /> and <see cref="Max" /> values.
        /// </returns>
        public double this[double index]
        {
            get
            {
                switch (Overflow)
                {
                    case Overflow.Clamp:
                        return Interpolators.Linear(Operations.Clamp(index, UnitMin, UnitMax), Min, Max);
                    case Overflow.Wrap:
                        return Interpolators.Linear(Operations.Wrap(index, UnitMin, UnitMax), Min, Max);
                    default:
                        return Interpolators.Linear(index, Min, Max);
                }
            }
        }

        #region Properties
        /// <summary>
        /// Gets the minimum value of the <see cref="NumericRange" />.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Min { get; }

        /// <summary>
        /// Gets the maximum value of the <see cref="NumericRange" />.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Max { get; }

        /// <summary>
        /// Gets the minimum value of a periodic unit that wraps back to the <see cref="UnitMax" /> value.
        /// </summary>
        /// <value>
        /// The unit minimum.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double UnitMin { get; }

        /// <summary>
        /// Gets the Maximum value of a periodic unit that wraps back to the <see cref="UnitMin" /> value.
        /// </summary>
        /// <value>
        /// The unit maximum.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double UnitMax { get; }

        /// <summary>
        /// Gets the amount to advance for each iteration.
        /// </summary>
        /// <value>
        /// The step.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Step { get; }

        /// <summary>
        /// Gets a value indicating whether to wrap the return value or clamp it between max and min.
        /// </summary>
        /// <value>
        /// The overflow.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public Overflow Overflow { get; }
        #endregion Properties

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(NumericRange left, NumericRange right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(NumericRange left, NumericRange right) => !(left == right);

        #region Methods
        /// <summary>
        /// Enumerate all values at the <see cref="Step" /> interval between the <see cref="Max" /> and <see cref="Min" /> values.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = Min; i < Max; i += Step)
            {
                yield return this[i];
            }
        }

        /// <summary>
        /// Enumerate all values at the <see cref="Step" /> interval between the <see cref="Max" /> and <see cref="Min" /> values.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            for (var i = Min; i < Max; i += Step)
            {
                yield return this[i];
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        public override bool Equals(object obj) => obj is NumericRange range && Equals(range);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(NumericRange other) => Min == other.Min && Max == other.Max && UnitMin == other.UnitMin && UnitMax == other.UnitMax && Step == other.Step && Overflow == other.Overflow;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 885038066;
            hashCode = hashCode * -1521134295 + Min.GetHashCode();
            hashCode = hashCode * -1521134295 + Max.GetHashCode();
            hashCode = hashCode * -1521134295 + UnitMin.GetHashCode();
            hashCode = hashCode * -1521134295 + UnitMax.GetHashCode();
            hashCode = hashCode * -1521134295 + Step.GetHashCode();
            hashCode = hashCode * -1521134295 + Overflow.GetHashCode();
            return hashCode;
        }
        #endregion Methods
    }
}
