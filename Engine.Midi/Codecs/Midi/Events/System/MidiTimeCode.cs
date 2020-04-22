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

using System.Runtime.CompilerServices;

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
        /// Initializes a new instance of the <see cref="MidiTimeCode" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="timeCode">The time code.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MidiTimeCode(IEventStatus status, byte timeCode)
            : this(status, 1, timeCode)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MidiTimeCode" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="length">The length.</param>
        /// <param name="timeCode">The timeCode.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal MidiTimeCode(IEventStatus status, int length, byte timeCode)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            (Length, TimeCode) = (length, timeCode);
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; }

        /// <summary>
        /// Gets or sets the time code.
        /// </summary>
        /// <value>
        /// The time code.
        /// </value>
        public byte TimeCode { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="MidiTimeCode" />.
        /// </returns>
        internal static new MidiTimeCode Read(BinaryReaderExtended reader, IEventStatus status) => new MidiTimeCode(status, reader.ReadVariableLengthInt(), reader.ReadByte());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Midi Time Code";
    }
}
