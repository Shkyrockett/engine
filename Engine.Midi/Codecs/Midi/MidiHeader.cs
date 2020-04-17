// <copyright file="MidiHeader.cs" company="Shkyrockett">
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
    /// The header structure of a standard midi file.
    /// </summary>
    [ElementName(nameof(MidiHeader))]
    public class MidiHeader
        : IMediaElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MidiHeader"/> class.
        /// </summary>
        public MidiHeader()
            : this(MidiFileTrackFormat.SingleTrack, 0, new DeltaTime())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MidiHeader"/> class.
        /// </summary>
        /// <param name="headerSize">Size of the header.</param>
        /// <param name="midiFileFormat">The midi file format.</param>
        /// <param name="trackCount">The track count.</param>
        /// <param name="deltaTime">The delta time.</param>
        public MidiHeader(MidiFileTrackFormat midiFileFormat, ushort trackCount, DeltaTime deltaTime)
           : this("MThd", 6, midiFileFormat, trackCount, deltaTime)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MidiHeader" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="headerSize">Size of the header.</param>
        /// <param name="midiFileFormat">The midi file format.</param>
        /// <param name="trackCount">The track count.</param>
        /// <param name="deltaTime">The delta time.</param>
        public MidiHeader(string id, int headerSize, MidiFileTrackFormat midiFileFormat, ushort trackCount, DeltaTime deltaTime)
        {
            (ID, HeaderSize, MidiFileFormat, TrackCount, DeltaTime) = (id, headerSize, midiFileFormat, trackCount, deltaTime);
        }

        /// <summary>
        /// The standard Midi file header identifier.
        /// </summary>
        public string ID { get; } // = "MThd"; //new byte[] { 0x4D, 0x54, 0x68, 0x64 };

        /// <summary>
        /// The size of the Midi file header in bytes.
        /// </summary>
        public int HeaderSize { get; set; } // = 6;

        /// <summary>
        /// The format of the Midi file.
        /// </summary>
        public MidiFileTrackFormat MidiFileFormat { get; set; }

        /// <summary>
        /// The number of tracks the Midi file contains.
        /// </summary>
        public ushort TrackCount { get; set; }

        /// <summary>
        /// The Delta time of a quarter note.
        /// </summary>
        public DeltaTime DeltaTime { get; set; }

        /// <summary>
        /// Read the header from the stream.
        /// </summary>
        /// <param name="reader">The binary reader for the file.</param>
        /// <param name="chunk"></param>
        /// <returns></returns>
        internal static MidiHeader Read(BinaryReaderExtended reader, Chunk chunk)
            => string.IsNullOrWhiteSpace(chunk.Id) || chunk.Id != "MThd" ? null
            : new MidiHeader(chunk.Id, chunk.Length, (MidiFileTrackFormat)reader.ReadNetworkInt16(), reader.ReadNetworkUInt16(), DeltaTime.Read(reader));

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Midi Header";
    }
}
