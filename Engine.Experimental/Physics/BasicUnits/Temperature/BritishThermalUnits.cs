// <copyright file="BritishThermalUnits.cs" company="Shkyrockett" >
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
    /// The British thermal units struct.
    /// </summary>
    public struct BritishThermalUnits
        : ITemperature, IEquatable<BritishThermalUnits>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BritishThermalUnits"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public BritishThermalUnits(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name
            => "British Thermal Units";

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abbreviation
            => "Btu";

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Double"/> to <see cref="BritishThermalUnits"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator BritishThermalUnits(double value)
            => new BritishThermalUnits(value);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(BritishThermalUnits left, BritishThermalUnits right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(BritishThermalUnits left, BritishThermalUnits right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is BritishThermalUnits units && Equals(units);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(BritishThermalUnits other) => Value == other.Value;

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
            => $"{Value} Btu";
    }
}
