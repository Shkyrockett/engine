// <copyright file="PackedNibble.cs" company="Shkyrockett">
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    /// <!-- This class is based on several solutions found at: http://stackoverflow.com/questions/11607848/working-with-nibbles-in-c-sharp -->
    public struct PackedNibble
        : IEquatable<PackedNibble>
    {
        /// <summary>
        /// 
        /// </summary>
        private byte packedValue;// = 0;

        /// <summary>
        /// Creates an instance of the <see cref="PackedNibble"/> class.
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
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        public byte HighNibble
        {
            get { return (byte)(packedValue >> 4); }
            set { packedValue = (byte)((value << 4) | (packedValue & 0x0F)); }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte LowNibble
        {
            get { return (byte)(packedValue & 0x0F); }
            set { packedValue = (byte)((packedValue & 0xF0) | (value & 0x0F)); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(PackedNibble a, PackedNibble b)
            => a.packedValue == b.packedValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(PackedNibble a, PackedNibble b)
            => a.packedValue != b.packedValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is PackedNibble)
                return Equals((PackedNibble)obj);

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PackedNibble other)
            => packedValue == other.packedValue;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"HighNibble: {HighNibble.ToString(CultureInfo.InvariantCulture)} LowNibble: {LowNibble.ToString(CultureInfo.InvariantCulture)}";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => packedValue;
    }
}
