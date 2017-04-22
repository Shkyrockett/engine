// <copyright file="MidiHeader.cs" company="Shkyrockett">
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
    /// The header structure of a standard midi file.
    /// </summary>
    [ElementName(nameof(MidiHeader))]
    [DisplayName("Midi File Header")]
    public class MidiHeader
        : IMidiElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MidiHeader"/> class.
        /// </summary>
        public MidiHeader()
        {
            MidiFileFormat = MidiFileTrackFormats.SingleTrack;
            TrackCount = 0;
            DeltaTime = new DeltaTime();
        }

        /// <summary>
        /// The standard Midi file header identifier.
        /// </summary>
        //new List<byte> { 0x4D, 0x54, 0x68, 0x64 };
        public string ID { get; } = "MThd";

        /// <summary>
        /// The size of the Midi file header in bytes.
        /// </summary>
        public int HeaderSize { get; set; } = 6;

        /// <summary>
        /// The format of the Midi file.
        /// </summary>
        public MidiFileTrackFormats MidiFileFormat { get; set; }

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
        /// <returns></returns>
        internal static MidiHeader Read(MidiBinaryReader reader, Chunk chunk)
        {
            //string id = reader.ReadASCIIBytes(4);
            if (string.IsNullOrWhiteSpace(chunk.Id)) return null;

            var header = new MidiHeader()
            {
                HeaderSize = chunk.Length, //reader.ReadNetworkInt32(),
                MidiFileFormat = (MidiFileTrackFormats)reader.ReadNetworkInt16(),
                TrackCount = reader.ReadNetworkUInt16(),
                DeltaTime = DeltaTime.Read(reader)
            };

            if (chunk.Id != header.ID) return null;

            return header;
        }
    }
}
