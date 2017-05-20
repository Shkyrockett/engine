// <copyright file="Midi.cs" company="Shkyrockett">
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

using System.Collections.Generic;
using System.IO;

namespace Engine.File
{
    /// <summary>
    /// Standard Midi File media container.
    /// </summary>
    [Expandable]
    [DisplayName("Midi File")]
    public class Midi
        : IMediaContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Midi"/> class.
        /// </summary>
        public Midi()
        {
            Header = new MidiHeader();
            Tracks = new List<MidiTrack>();
        }

        /// <summary>
        /// Gets or sets the Midi file header.
        /// </summary>
        public MidiHeader Header { get; set; }

        /// <summary>
        /// Gets or sets the Midi file tracks.
        /// </summary>
        [ExpandableList]
        public List<MidiTrack> Tracks { get; set; }

        /// <summary>
        /// Loads a Midi file from a stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Midi Load(Stream stream)
        {
            using (var reader = new BinaryReaderExtended(stream))
                return Read(reader);
        }

        /// <summary>
        /// Reads a Midi file from a Midi file stream using a Midi file Reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static Midi Read(BinaryReaderExtended reader)
        {
            Midi midi = null;
            MidiHeader header = null;

            var headerChunk = Chunk.Read(reader);

            //using (
            var headerReader = new BinaryReaderExtended(headerChunk.SubStream);//)
            {
                header = MidiHeader.Read(headerReader, headerChunk);
            }

            var tracks = new List<MidiTrack>(header.TrackCount);

            for (var i = 0; i < header.TrackCount; i++)
            {
                var trackChunk = Chunk.Read(reader);
                var trackStart = reader.Position;
                var trackEnd = trackStart + trackChunk.Length;

                //using (
                var trackReader = new BinaryReaderExtended(trackChunk.SubStream);//)
                {
                    tracks.Add(MidiTrack.Read(trackReader, trackChunk));
                }

                if (reader.Position != trackEnd)
                    reader.Position = trackEnd;
            }

            midi = new Midi()
            {
                Header = header,
                Tracks = tracks
            };

            return midi;
        }
    }
}
