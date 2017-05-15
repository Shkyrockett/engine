// <copyright file="ExpandedNibble.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// This class is based on several solutions found at: http://stackoverflow.com/questions/11607848/working-with-nibbles-in-c-sharp
// </references>

using System;
using System.Globalization;

namespace Engine.Midi
{
    /// <summary>
    /// 
    /// </summary>
    /// <!-- This class is based on several solutions found at: http://stackoverflow.com/questions/11607848/working-with-nibbles-in-c-sharp -->
    public class ExpandedNibble
        : IEquatable<ExpandedNibble>
    {
        /// <summary>
        /// Creates an instance of the <see cref="ExpandedNibble"/> class.
        /// </summary>
        /// <param name="highNibble">Initial value for the y component.</param>
        /// <param name="lowNibble">Initial value for the x component.</param>
        public ExpandedNibble(byte highNibble, byte lowNibble)
        {
            HighNibble = highNibble;
            LowNibble = lowNibble;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte HighNibble { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public byte LowNibble { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        private byte PackedValue
        {
            get { return (byte)((HighNibble & 0xF0 << 4) | (LowNibble & 0x0F)); }
            set
            {
                HighNibble = (byte)(PackedValue >> 4);
                LowNibble = (byte)(PackedValue & 0x0F);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(ExpandedNibble a, ExpandedNibble b)
            => a.PackedValue == b.PackedValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(ExpandedNibble a, ExpandedNibble b)
            => a.PackedValue != b.PackedValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is ExpandedNibble)
                return Equals((ExpandedNibble)obj);

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ExpandedNibble other)
            => PackedValue == other.PackedValue;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => PackedValue.ToString("HighNibble: " + HighNibble + " LowNibble: " + LowNibble, CultureInfo.InvariantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => PackedValue;
    }
}
