// <copyright file="MidiTimeCode.cs" company="Shkyrockett">
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
    /// MIDI Time Code Quarter Frame.
    /// </summary>
    /// <remarks>
    /// <para>nF 01 0nnndddd
    /// nnn = Message Type dddd = Values</para>
    /// </remarks>
    [ElementName(nameof(MidiTimeCode))]
    public class MidiTimeCode
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MidiTimeCode"/> class.
        /// </summary>
        /// <param name="timeCode">The timeCode.</param>
        /// <param name="status">The status.</param>
        public MidiTimeCode(byte timeCode, EventStatus status)
            : base((status?.DeltaTime).Value, status.Status, status.Channel)
        {
            TimeCode = timeCode;
        }

        /// <summary>
        /// Gets or sets the time code.
        /// </summary>
        public byte TimeCode { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="MidiTimeCode"/>.</returns>
        internal static MidiTimeCode Read(BinaryReaderExtended reader, EventStatus status) => new MidiTimeCode(reader.ReadByte(), status);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Midi Time Code";
    }
}
