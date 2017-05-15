// <copyright file="MIDIPort.cs" company="Shkyrockett">
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

namespace Engine.Midi
{
    /// <summary>
    /// MIDI Port (not official?).
    /// </summary>
    /// <remarks>
    /// FF 21 01  pp
    /// </remarks>
    [ElementName(nameof(MIDIPort))]
    [DisplayName("MIDI Port")]
    public class MIDIPort
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        /// <param name="status"></param>
        public MIDIPort(byte port, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Port = port;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Port { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static MIDIPort Read(MidiBinaryReader reader, EventStatus status)
            => new MIDIPort(reader.ReadByte(), status);
    }
}
