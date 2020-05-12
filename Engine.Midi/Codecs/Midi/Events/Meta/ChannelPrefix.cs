// <copyright file="MIDIChannel.cs" company="Shkyrockett">
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

using System.Runtime.CompilerServices;

namespace Engine.File
{
    /// <summary>
    /// MIDI Channel (not official?).
    /// </summary>
    /// <remarks>
    /// <para>FF 20 01  cc</para>
    /// </remarks>
    [ElementName(nameof(ChannelPrefix))]
    public class ChannelPrefix
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelPrefix"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="midiChannel">The midi channel.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ChannelPrefix(IEventStatus status, byte midiChannel)
            : this(status, 1, midiChannel)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelPrefix" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="length">The length.</param>
        /// <param name="channel">The channel.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ChannelPrefix(IEventStatus status, int length, byte channel)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            Length = length;
            MidiChannel = channel;
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; }

        /// <summary>
        /// Gets or sets the midi channel.
        /// </summary>
        /// <value>
        /// The midi channel.
        /// </value>
        public byte MidiChannel { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="ChannelPrefix" />.
        /// </returns>
        internal static new ChannelPrefix Read(BinaryReaderExtended reader, IEventStatus status) => new ChannelPrefix(status, reader.ReadVariableLengthInt(), reader.ReadByte());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"Channel Prefix: {MidiChannel}";
    }
}
