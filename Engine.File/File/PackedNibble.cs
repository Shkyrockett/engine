// <copyright file="PackedNibble.cs" company="Shkyrockett">
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

using System;
using System.Globalization;

namespace Engine.File
{
    /// <summary>
    /// The packed nibble struct.
    /// </summary>
    /// <seealso cref="System.IEquatable{T}" />
    /// <!-- This class is based on several solutions found at: http://stackoverflow.com/questions/11607848/working-with-nibbles-in-c-sharp -->
    public struct PackedNibble
        : IEquatable<PackedNibble>
    {
        /// <summary>
        /// The packed value.
        /// </summary>
        private byte packedValue;// = 0;

        /// <summary>
        /// Creates an instance of the <see cref="PackedNibble" /> class.
        /// </summary>
        /// <param name="highNibble">Initial value for the y component.</param>
        /// <param name="lowNibble">Initial value for the x component.</param>
        public PackedNibble(byte highNibble, byte lowNibble)
        {
            packedValue = 0;
            //packedValue = (byte)((highNibble & 0xF0 << 4) | (lowNibble & 0x0F));
            packedValue = (byte)((packedValue & 0xF0) | (lowNibble & 0x0F));
            packedValue = (byte)((packedValue & 0x0F) | (highNibble << 4));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackedNibble" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public PackedNibble(byte value)
        {
            packedValue = value;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public PackedNibble()
        //{
        //    packedValue = 0;
        //}

        /// <summary>
        /// Gets or sets the high nibble.
        /// </summary>
        /// <value>
        /// The high nibble.
        /// </value>
        public byte HighNibble
        {
            get { return (byte)(packedValue >> 4); }
            set { packedValue = (byte)((value << 4) | (packedValue & 0x0F)); }
        }

        /// <summary>
        /// Gets or sets the low nibble.
        /// </summary>
        /// <value>
        /// The low nibble.
        /// </value>
        public byte LowNibble
        {
            get { return (byte)(packedValue & 0x0F); }
            set { packedValue = (byte)((packedValue & 0xF0) | (value & 0x0F)); }
        }

        /// <summary>
        /// Packs the nibbles.
        /// </summary>
        /// <param name="lowNibble">The low nibble.</param>
        /// <param name="highNibble">The high nibble.</param>
        /// <returns></returns>
        public static byte PackNibbles(byte lowNibble, byte highNibble)
        {
            byte packedValue = 0;
            packedValue = (byte)((packedValue & 0xF0) | (lowNibble & 0x0F));
            packedValue = (byte)((packedValue & 0x0F) | (highNibble << 0x04));
            return packedValue;
        }

        /// <summary>
        /// Unpacks the nibbles.
        /// </summary>
        /// <param name="packedValue">The packed value.</param>
        /// <returns></returns>
        public static (byte LowNibble, byte HighNibble) UnpackNibbles(byte packedValue) => ((byte)(packedValue & 0x0F), (byte)(packedValue >> 0x04));

        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool operator ==(PackedNibble a, PackedNibble b) => a.packedValue == b.packedValue;

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool operator !=(PackedNibble a, PackedNibble b) => a.packedValue != b.packedValue;

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is PackedNibble nibble)
            {
                return Equals(nibble);
            }

            return false;
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public bool Equals(PackedNibble other) => packedValue == other.packedValue;

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public override string ToString() => $"HighNibble: {HighNibble.ToString(CultureInfo.InvariantCulture)} LowNibble: {LowNibble.ToString(CultureInfo.InvariantCulture)}";

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        public override int GetHashCode() => packedValue;
    }
}
