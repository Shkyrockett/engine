// <copyright file="MidiHeader.cs" company="Shkyrockett">
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

using System.Runtime.CompilerServices;

namespace Engine.File;

/// <summary>
/// The header structure of a standard midi file.
/// </summary>
/// <seealso cref="Engine.File.IMediaElement" />
[ElementName(nameof(MidiHeader))]
public class MidiHeader
    : IMediaElement
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="MidiHeader" /> class.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public MidiHeader()
        : this(MidiFileTrackFormat.SingleTrack, 0, new DeltaTime())
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MidiHeader" /> class.
    /// </summary>
    /// <param name="midiFileFormat">The midi file format.</param>
    /// <param name="trackCount">The track count.</param>
    /// <param name="deltaTime">The delta time.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public MidiHeader(string id, int headerSize, MidiFileTrackFormat midiFileFormat, ushort trackCount, DeltaTime deltaTime)
    {
        (ID, HeaderSize, MidiFileFormat, TrackCount, DeltaTime) = (id, headerSize, midiFileFormat, trackCount, deltaTime);
    }
    #endregion

    #region Properties
    /// <summary>
    /// The standard Midi file header identifier.
    /// </summary>
    /// <value>
    /// The identifier. Should always be "MThd" or { 0x4D, 0x54, 0x68, 0x64 }
    /// </value>
    public string ID { get; }

    /// <summary>
    /// The size of the Midi file header in bytes.
    /// </summary>
    /// <value>
    /// The size of the header. Should always be 6.
    /// </value>
    public int HeaderSize { get; internal set; }

    /// <summary>
    /// The format of the Midi file.
    /// </summary>
    /// <value>
    /// The midi file format.
    /// </value>
    public MidiFileTrackFormat MidiFileFormat { get; internal set; }

    /// <summary>
    /// The number of tracks the Midi file contains.
    /// </summary>
    /// <value>
    /// The track count.
    /// </value>
    public ushort TrackCount { get; internal set; }

    /// <summary>
    /// The Delta time of a quarter note.
    /// </summary>
    /// <value>
    /// The delta time.
    /// </value>
    public DeltaTime DeltaTime { get; set; }
    #endregion

    #region Methods
    /// <summary>
    /// Read the header from the stream.
    /// </summary>
    /// <param name="reader">The binary reader for the file.</param>
    /// <param name="chunk">The chunk.</param>
    /// <returns></returns>
    /// <exception cref="InvalidDataException">MThd is missing. This is not a Midi file.</exception>
    internal static MidiHeader Read(BinaryReaderExtended reader, Chunk chunk)
    {
        MidiHeader midiHeader = !string.IsNullOrWhiteSpace(chunk?.Id) && chunk.Id == "MThd"
            ? new MidiHeader(chunk.Id, chunk.Length, (MidiFileTrackFormat)reader.ReadNetworkInt16(), reader.ReadNetworkUInt16(), DeltaTime.Read(reader))
            : throw new InvalidDataException("MThd is missing. This is not a Midi file.");
        // Finish up reading the header by setting the position to the end of the sub stream just in case the length is more than 6 for some reason. Which it should never be but you never know...
        reader.Position = reader.Length;
        return midiHeader;
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => "Midi Header";
    #endregion
}
