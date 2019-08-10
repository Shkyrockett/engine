// <copyright file="MIDIPort.cs" company="Shkyrockett">
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
    /// MIDI Port (not official?).
    /// </summary>
    /// <remarks>
    /// <para>FF 21 01  pp</para>
    /// </remarks>
    [ElementName(nameof(MIDIPort))]
    [DisplayName("MIDI Port")]
    public class MIDIPort
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MIDIPort"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="status">The status.</param>
        public MIDIPort(byte port, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Port = port;
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public byte Port { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="MIDIPort"/>.</returns>
        internal static MIDIPort Read(BinaryReaderExtended reader, EventStatus status)
            => new MIDIPort(reader.ReadByte(), status);
    }
}
