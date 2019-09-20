// <copyright file="Years.cs" company="Shkyrockett" >
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

namespace Engine
{
    /// <summary>
    /// The years struct.
    /// </summary>
    public struct Years
        : ITime, IEquatable<Years>
    {
        /// <summary>
        /// The second (const). Value: 31557600d.
        /// </summary>
        public const double Second = 31557600d;

        /// <summary>
        /// The minute (const). Value: 525960d.
        /// </summary>
        public const double Minute = 525960d;

        /// <summary>
        /// The hour (const). Value: 8766d.
        /// </summary>
        public const double Hour = 8766d;

        /// <summary>
        /// The day (const). Value: 365.25d.
        /// </summary>
        public const double Day = 365.25d;

        /// <summary>
        /// The year (const). Value: 1d.
        /// </summary>
        public const double Year = 1d;

        /// <summary>
        /// Initializes a new instance of the <see cref="Years"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Years(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the seconds.
        /// </summary>
        public double Seconds
        {
            get { return Value * Second; }
            set { Value = value / Second; }
        }

        /// <summary>
        /// Gets or sets the minutes.
        /// </summary>
        public double Minutes
        {
            get { return Value * Minute; }
            set { Value = value / Minute; }
        }

        /// <summary>
        /// Gets or sets the hours.
        /// </summary>
        public double Hours
        {
            get { return Value * Hour; }
            set { Value = value / Hour; }
        }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        public double Days
        {
            get { return Value * Day; }
            set { Value = value / Day; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name
            => nameof(Years);

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abbreviation
            => "years";

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Double"/> to <see cref="Years"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Years(double value)
            => new Years(value);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Years left, Years right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Years left, Years right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is Years years && Equals(years);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Years other) => Value == other.Value;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => -1937169414 + Value.GetHashCode();

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => $"{Value} years";
    }
}
