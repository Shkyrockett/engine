// <copyright file="SongPosition.cs" company="Shkyrockett">
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
    /// Song Position Pointer.
    /// The number of MIDI beats(1 beat = six MIDI clocks) since the start of the song.
    /// </summary>
    /// <remarks>
    /// <para>nF 02 0lllllll 0mmmmmmm
    /// This is an internal 14 bit register that holds the number of MIDI beats (1 beat = six MIDI clocks)
    /// since the start of the song. l is the LSB, m the MSB.</para>
    /// </remarks>
    [ElementName(nameof(SongPosition))]
    [DisplayName("Song Position")]
    public class SongPosition
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongPosition"/> class.
        /// </summary>
        /// <param name="beats">The beats.</param>
        /// <param name="status">The status.</param>
        public SongPosition(short beats, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Beats = beats;
        }

        /// <summary>
        /// Gets or sets the beats.
        /// </summary>
        public short Beats { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="SongPosition"/>.</returns>
        internal static SongPosition Read(BinaryReaderExtended reader, EventStatus status)
            => new SongPosition(reader.ReadNetworkInt14(), status);
    }
}
