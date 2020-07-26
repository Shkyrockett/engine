// <copyright file="SystemExclusive.cs" company="Shkyrockett">
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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine.File
{
    /// <summary>
    /// System Exclusive.
    /// </summary>
    /// <remarks>
    /// <para>nF 00 0iiiiiii [0iiiiiii 0iiiiiii] 0ddddddd --- --- 0ddddddd 11110111
    /// This message type allows manufacturers to create their own messages
    /// (such as bulk dumps, patch parameters, and other non-spec data)
    /// and provides a mechanism for creating additional MIDI Specification messages.
    /// The Manufacturer's ID code (assigned by MMA or AMEI) is either 1 byte (0iiiiiii)
    /// or 3 bytes (0iiiiiii 0iiiiiii 0iiiiiii). Two of the 1 Byte IDs are reserved for
    /// extensions called Universal Exclusive Messages, which are not manufacturer-specific.
    /// If a device recognizes the ID code as its own (or as a supported Universal message)
    /// it will listen to the rest of the message (0ddddddd). Otherwise, the message will be ignored.
    /// (Note: Only Real-Time messages may be interleaved with a System Exclusive.)</para>
    /// </remarks>
    /// <seealso cref="Engine.File.EventStatus" />
    [ElementName(nameof(SystemExclusive))]
    [DisplayName("System Exclusive")]
    public class SystemExclusive
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemExclusive" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="exclusive">The exclusive.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SystemExclusive(IEventStatus status, byte[] exclusive)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            Exclusive = exclusive;
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length => Exclusive.Length;

        /// <summary>
        /// Gets or sets the exclusive.
        /// </summary>
        /// <value>
        /// The exclusive.
        /// </value>
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public byte[] Exclusive { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <param name="sysExContinue">A flag indicating whether we're in a multi-segment system exclusive message.</param>
        /// <param name="sysExData">system exclusive data up to this point from a multi-segment message.</param>
        /// <returns>
        /// The <see cref="SystemExclusive" />.
        /// </returns>
        internal static SystemExclusive Read(BinaryReaderExtended reader, IEventStatus status, ref bool sysExContinue, ref byte[] sysExData)
        {
            var buffer = reader.ReadVariableLengthBytes();
            if (sysExData is null)
            {
                sysExData = Array.Empty<byte>();
            }

            if (buffer.Length == 0)
            {
                return null;
            }

            if (buffer[0] == 0xF7)
            {
                // If this is single-segment message, process the whole thing
                sysExData = buffer;
                return new SystemExclusive(status, sysExData);
            }
            else
            {
                // It's multi-segment, so add the new data to the previously acquired data
                // Add to previously acquired sys ex data
                var newSysExData = new byte[sysExData.Length + buffer.Length];
                if (sysExData is not null)
                {
                    sysExData.CopyTo(newSysExData, 0);
                }

                Array.Copy(buffer, 0, newSysExData, sysExData.Length, buffer.Length);
                sysExData = newSysExData;
                sysExContinue = true;
                return new SystemExclusive(status, sysExData);
            }
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => "System Exclusive";
    }
}
