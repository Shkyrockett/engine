// <copyright file="SongPosition.cs" company="Shkyrockett">
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
    /// Song Position Pointer.
    /// The number of MIDI beats(1 beat = six MIDI clocks) since the start of the song.
    /// </summary>
    /// <remarks>
    /// <para>nF 02 0lllllll 0mmmmmmm
    /// This is an internal 14 bit register that holds the number of MIDI beats (1 beat = six MIDI clocks)
    /// since the start of the song. l is the LSB, m the MSB.</para>
    /// </remarks>
    [ElementName(nameof(SongPosition))]
    public class SongPosition
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongPosition"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="beats">The beats.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SongPosition(IEventStatus status, short beats)
            : this(status, 2, beats)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SongPosition" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="length">The length.</param>
        /// <param name="beats">The beats.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal SongPosition(IEventStatus status, int length, short beats)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            (Length, Beats) = (length, beats);
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public object Length { get; }

        /// <summary>
        /// Gets or sets the beats.
        /// </summary>
        /// <value>
        /// The beats.
        /// </value>
        public short Beats { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="SongPosition" />.
        /// </returns>
        internal static new SongPosition Read(BinaryReaderExtended reader, IEventStatus status) => new SongPosition(status, reader.ReadVariableLengthInt(), reader.ReadNetworkInt14());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Song Position";
    }
}
