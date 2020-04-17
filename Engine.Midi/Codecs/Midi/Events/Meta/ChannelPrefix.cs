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
        /// <param name="channel">The channel.</param>
        /// <param name="status">The status.</param>
        public ChannelPrefix(byte channel, EventStatus status)
            : base((status?.DeltaTime).Value, status.Status, status.Channel)
        {
            MidiChannel = channel;
        }

        /// <summary>
        /// Gets or sets the midi channel.
        /// </summary>
        public byte MidiChannel { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="ChannelPrefix"/>.</returns>
        internal static ChannelPrefix Read(BinaryReaderExtended reader, EventStatus status) => new ChannelPrefix(reader.ReadByte(), status);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"Channel Prefix: {MidiChannel}";
    }
}
