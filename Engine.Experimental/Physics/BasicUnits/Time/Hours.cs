// <copyright file="Hours.cs" company="Shkyrockett" >
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
using System.Diagnostics.CodeAnalysis;

namespace Engine
{
    /// <summary>
    /// The hours struct.
    /// </summary>
    /// <seealso cref="ITime" />
    /// <seealso cref="IEquatable{T}" />
    public struct Hours
        : ITime, IEquatable<Hours>
    {
        /// <summary>
        /// The second (const). Value: 3600d.
        /// </summary>
        public const double Second = 3600d;

        /// <summary>
        /// The minute (const). Value: 60d.
        /// </summary>
        public const double Minute = 60d;

        /// <summary>
        /// The hour (const). Value: 1d.
        /// </summary>
        public const double Hour = 1d;

        /// <summary>
        /// The day (const). Value: 24d.
        /// </summary>
        public const double Day = 24d;

        /// <summary>
        /// The year (const). Value: 365.25d * Day.
        /// </summary>
        public const double Year = 365.25d * Day;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hours" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Hours(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the seconds.
        /// </summary>
        /// <value>
        /// The seconds.
        /// </value>
        public double Seconds
        {
            get { return Value * Second; }
            set { Value = value / Second; }
        }

        /// <summary>
        /// Gets or sets the minutes.
        /// </summary>
        /// <value>
        /// The minutes.
        /// </value>
        public double Minutes
        {
            get { return Value * Minute; }
            set { Value = value / Minute; }
        }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        /// <value>
        /// The days.
        /// </value>
        public double Days
        {
            get { return Value * Day; }
            set { Value = value / Day; }
        }

        /// <summary>
        /// Gets or sets the years.
        /// </summary>
        /// <value>
        /// The years.
        /// </value>
        public double Years
        {
            get { return Value * Year; }
            set { Value = value / Year; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name
            => nameof(Hours);

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        /// <value>
        /// The abbreviation.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abbreviation
            => "h";

        /// <summary>
        /// Performs an implicit conversion from <see cref="double"/> to <see cref="Hours"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Hours(double value)
            => new Hours(value);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Hours left, Hours right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Hours left, Hours right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is Hours hours && Equals(hours);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(Hours other) => Value == other.Value;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => -1937169414 + Value.GetHashCode();

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
            => $"{Value} h";
    }
}
