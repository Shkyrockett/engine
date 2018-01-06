// <copyright file="SystemExclusive.cs" company="Shkyrockett">
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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

namespace Engine.File
{
    /// <summary>
    /// System Exclusive.
    /// </summary>
    /// <remarks>
    /// nF 00 0iiiiiii [0iiiiiii 0iiiiiii] 0ddddddd --- --- 0ddddddd 11110111
    /// This message type allows manufacturers to create their own messages 
    /// (such as bulk dumps, patch parameters, and other non-spec data)
    /// and provides a mechanism for creating additional MIDI Specification messages.
    /// The Manufacturer's ID code (assigned by MMA or AMEI) is either 1 byte (0iiiiiii)
    /// or 3 bytes (0iiiiiii 0iiiiiii 0iiiiiii). Two of the 1 Byte IDs are reserved for
    /// extensions called Universal Exclusive Messages, which are not manufacturer-specific.
    /// If a device recognizes the ID code as its own (or as a supported Universal message)
    /// it will listen to the rest of the message (0ddddddd). Otherwise, the message will be ignored.
    /// (Note: Only Real-Time messages may be interleaved with a System Exclusive.)
    /// </remarks>
    [ElementName(nameof(SystemExclusive))]
    [DisplayName("System Exclusive")]
    public class SystemExclusive
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exclusive"></param>
        /// <param name="status"></param>
        public SystemExclusive(byte[] exclusive, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Exclusive = exclusive;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Exclusive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <param name="sysExContinue">A flag indicating whether we're in a multi-segment system exclusive message.</param>
        /// <param name="sysExData">system exclusive data up to this point from a multi-segment message.</param>
        /// <returns></returns>
        internal static SystemExclusive Read(BinaryReaderExtended reader, EventStatus status, ref bool sysExContinue, ref byte[] sysExData)
        {
            var buffer = reader.ReadVariableLengthBytes();
            if (sysExData == null) sysExData = new byte[0];
            if (buffer.Length == 0)
            {
                return null;
            }

            if (buffer[0] == 0xF7)
            {
                // If this is single-segment message, process the whole thing
                sysExData = buffer;
                return new SystemExclusive(sysExData, status);
            }
            else
            {
                // It's multi-segment, so add the new data to the previously acquired data
                // Add to previously acquired sys ex data
                var newSysExData = new byte[sysExData.Length + buffer.Length];
                if (sysExData != null) sysExData.CopyTo(newSysExData, 0);
                Array.Copy(buffer, 0, newSysExData, sysExData.Length, buffer.Length);
                sysExData = newSysExData;
                sysExContinue = true;
                return new SystemExclusive(sysExData, status);
            }
        }
    }
}
