// <copyright file="MIDIChannel.cs" company="Shkyrockett">
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
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
    /// FF 20 01  cc
    /// </remarks>
    [ElementName(nameof(MIDIChannel))]
    [DisplayName("MIDI Channel")]
    public class MIDIChannel
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MIDIChannel"/> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="status">The status.</param>
        public MIDIChannel(byte channel, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
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
        /// <returns>The <see cref="MIDIChannel"/>.</returns>
        internal static MIDIChannel Read(BinaryReaderExtended reader, EventStatus status)
            => new MIDIChannel(reader.ReadByte(), status);
    }
}
