// <copyright file="MIDIPort.cs" company="Shkyrockett">
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
    /// MIDI Port (not official?).
    /// </summary>
    /// <remarks>
    /// <para>FF 21 01  pp</para>
    /// </remarks>
    [ElementName(nameof(PortPrefix))]
    public class PortPrefix
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PortPrefix"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="status">The status.</param>
        public PortPrefix(byte port, EventStatus status)
            : base((status?.DeltaTime).Value, status.Status, status.Channel)
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
        /// <returns>The <see cref="PortPrefix"/>.</returns>
        internal static PortPrefix Read(BinaryReaderExtended reader, EventStatus status) => new PortPrefix(reader.ReadByte(), status);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"Port Prefix: {Port}";
    }
}
