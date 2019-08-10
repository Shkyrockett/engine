// <copyright file="Seconds.cs" company="Shkyrockett" >
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

namespace Engine.Physics
{
    /// <summary>
    /// The seconds struct.
    /// </summary>
    public struct Seconds
        : ITime, IEquatable<Seconds>
    {
        /// <summary>
        /// The minute (const). Value: 1d / 60d.
        /// </summary>
        public const double Minute = 1d / 60d;

        /// <summary>
        /// The hour (const). Value: 1d / 3600d.
        /// </summary>
        public const double Hour = 1d / 3600d;

        /// <summary>
        /// The day (const). Value: 1d / 86400d.
        /// </summary>
        public const double Day = 1d / 86400d;

        /// <summary>
        /// The year (const). Value: 1d / (365.25d * Day).
        /// </summary>
        public const double Year = 1d / (365.25d * Day);

        /// <summary>
        /// Initializes a new instance of the <see cref="Seconds"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Seconds(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Value { get; set; }

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
        /// Gets or sets the years.
        /// </summary>
        public double Years
        {
            get { return Value * Year; }
            set { Value = value / Year; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name
            => nameof(Seconds);

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abbreviation
            => "s";

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Double"/> to <see cref="Seconds"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Seconds(double value)
            => new Seconds(value);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Seconds left, Seconds right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Seconds left, Seconds right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is Seconds seconds && Equals(seconds);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Seconds other) => Value == other.Value;

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
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
            => $"{Value} s";
    }
}
