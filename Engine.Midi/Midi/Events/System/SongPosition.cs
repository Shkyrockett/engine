// <copyright file="SongPosition.cs" company="Shkyrockett">
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
    /// Song Position Pointer.
    /// The number of MIDI beats(1 beat = six MIDI clocks) since the start of the song.
    /// </summary>
    /// <remarks>
    /// nF 02 0lllllll 0mmmmmmm
    /// This is an internal 14 bit register that holds the number of MIDI beats (1 beat = six MIDI clocks)
    /// since the start of the song. l is the LSB, m the MSB.
    /// </remarks>
    [ElementName(nameof(SongPosition))]
    [DisplayName("Song Position")]
    public class SongPosition
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="beats"></param>
        /// <param name="status"></param>
        public SongPosition(short beats, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Beats = beats;
        }

        /// <summary>
        /// 
        /// </summary>
        public short Beats { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static SongPosition Read(MidiBinaryReader reader, EventStatus status)
            => new SongPosition(reader.ReadNetworkInt14(), status);
    }
}
