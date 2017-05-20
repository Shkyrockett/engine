// <copyright file="MIDIChannel.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
        /// 
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="status"></param>
        public MIDIChannel(byte channel, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            MidiChannel = channel;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte MidiChannel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static MIDIChannel Read(BinaryReaderExtended reader, EventStatus status)
            => new MIDIChannel(reader.ReadByte(), status);
    }
}
