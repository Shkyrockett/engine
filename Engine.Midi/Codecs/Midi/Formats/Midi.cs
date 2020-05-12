// <copyright file="Midi.cs" company="Shkyrockett">
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

using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Engine.File
{
    /// <summary>
    /// Standard Midi File media container.
    /// </summary>
    [Expandable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Midi
        : IMediaContainer
    {
        #region Constants
        /// <summary>
        /// The registered codec
        /// </summary>
        public static readonly bool CodecRegistered = RegisterMediaCodecs();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Midi"/> class.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Midi()
            : this(new List<MidiTrack>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Midi"/> class.
        /// </summary>
        /// <param name="tracks">The tracks.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Midi(params MidiTrack[] tracks)
            : this(new List<MidiTrack>(tracks))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Midi"/> class.
        /// </summary>
        /// <param name="tracks">The tracks.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Midi(List<MidiTrack> tracks)
            : this(new MidiHeader(), tracks)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Midi"/> class.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="tracks">The tracks.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Midi(MidiHeader header, List<MidiTrack> tracks)
        {
            (Header, Tracks) = (header, tracks);
        }
        #endregion

        #region Indexers
        /// <summary>
        /// Gets or sets the <see cref="MidiTrack"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="MidiTrack"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public MidiTrack this[int index]
        {
            get { return Tracks[index]; }
            set { Tracks[index] = value; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Midi file header.
        /// </summary>
        public MidiHeader Header { get; set; }

        /// <summary>
        /// Gets or sets the Midi file tracks.
        /// </summary>
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<MidiTrack> Tracks { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Registers the media extensions.
        /// </summary>
        public static bool RegisterMediaCodecs()
        {
            if (!MediaFile.RegisteredTypes.Contains(typeof(Midi))) MediaFile.RegisteredTypes.Add(typeof(Midi));
            if (!MediaFile.RegisteredExtensions.ContainsKey(".KAR")) MediaFile.RegisteredExtensions.Add(".KAR", s => Load(s));
            if (!MediaFile.RegisteredExtensions.ContainsKey(".MID")) MediaFile.RegisteredExtensions.Add(".MID", s => Load(s));
            if (!MediaFile.RegisteredExtensions.ContainsKey(".MIDI")) MediaFile.RegisteredExtensions.Add(".MIDI", s => Load(s));
            if (!MediaFile.RegisteredExtensions.ContainsKey(".SMF")) MediaFile.RegisteredExtensions.Add(".SMF", s => Load(s));
            return true;
        }

        /// <summary>
        /// Loads a Midi file from a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static Midi Load(Stream stream)
        {
            using var reader = new BinaryReaderExtended(stream, Encoding.Default, true);
            return Read(reader);
        }

        /// <summary>
        /// Reads a Midi file from a Midi file stream using a Midi file Reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static Midi Read(BinaryReaderExtended reader)
        {
            var headerChunk = Chunk.Read(reader);
            using var headerReader = new BinaryReaderExtended(headerChunk.SubStream, Encoding.Default, true);
            var header = MidiHeader.Read(headerReader, headerChunk);

            var tracks = new List<MidiTrack>(header.TrackCount);
            for (var i = 0; i < header.TrackCount; i++)
            {
                var trackChunk = Chunk.Read(reader);
                var trackStart = reader.Position;
                var trackEnd = trackStart + trackChunk.Length;

                using var trackReader = new BinaryReaderExtended(trackChunk.SubStream, Encoding.Default, true);
                tracks.Add(MidiTrack.Read(trackReader, trackChunk));

                if (reader.Position != trackEnd)
                {
                    reader.Position = trackEnd;
                }
            }

            return new Midi(header, tracks);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Midi File";
        #endregion
    }
}
