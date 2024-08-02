// <copyright file="MidiTrack.cs" company="Shkyrockett">
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

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine.File;

/// <summary>
/// The midi track class.
/// </summary>
[ElementName(nameof(MidiTrack))]
public class MidiTrack
    : IMediaElement
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="MidiTrack" /> class.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public MidiTrack()
        : this(new List<IEventStatus>())
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MidiTrack" /> class.
    /// </summary>
    /// <param name="events">The events.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public MidiTrack(params IEventStatus[] events)
        : this(new List<IEventStatus>(events))
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MidiTrack" /> class.
    /// </summary>
    /// <param name="events">The events.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public MidiTrack(List<IEventStatus> events)
        : this("MTrk", 6, events)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MidiTrack" /> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="trackHeaderLength">Length of the track header.</param>
    /// <param name="events">The events.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public MidiTrack(string id, int trackHeaderLength, List<IEventStatus> events)
    {
        (ID, TrackHeaderLength, Events) = (id, trackHeaderLength, events);
    }
    #endregion

    #region Indexers
    /// <summary>
    /// Gets or sets the <see cref="EventStatus" /> at the specified index.
    /// </summary>
    /// <value>
    /// The <see cref="EventStatus" />.
    /// </value>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    public IEventStatus this[int index]
    {
        get { return Events[index]; }
        set { Events[index] = value; }
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the ID.
    /// </summary>
    /// <value>
    /// The identifier. Should always be "MTrk" or { 0x4D, 0x54, 0x72, 0x6B };
    /// </value>
    public string ID { get; }

    /// <summary>
    /// Gets or sets the track header length.
    /// </summary>
    /// <value>
    /// The length of the track header. Should always be 6.
    /// </value>
    public int TrackHeaderLength { get; set; }

    /// <summary>
    /// Gets the items.
    /// </summary>
    /// <value>
    /// The events.
    /// </value>
    [TypeConverter(typeof(ExpandableCollectionConverter))]
    public List<IEventStatus> Events { get; }
    #endregion

    #region Methods
    /// <summary>
    /// Read.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="chunk">The chunk.</param>
    /// <returns>The <see cref="MidiTrack"/>.</returns>
    internal static MidiTrack Read(BinaryReaderExtended reader, Chunk chunk)
    {
        var events = new List<IEventStatus>();
        var sysExContinue = false;
        byte[] sysExData = null;

        var status = EventStatus.Empty as IEventStatus;
        while (reader.BaseStream.Position < reader.Length)
        {
            status = EventStatus.Read(reader, status);

            status = status.Message switch
            {
                MidiStatusMessage.Unknown => status,
                MidiStatusMessage.NoteOff => NoteOff.Read(reader, status),
                MidiStatusMessage.NoteOn => NoteOn.Read(reader, status),
                MidiStatusMessage.PolyphonicPressure => PolyphonicPressure.Read(reader, status),
                MidiStatusMessage.ControllerChange => ControllerChange.Read(reader, status),
                MidiStatusMessage.ProgramChange => ProgramChange.Read(reader, status),
                MidiStatusMessage.ChannelPressure => ChannelPressure.Read(reader, status),
                MidiStatusMessage.PitchBend => PitchBend.Read(reader, status),
                MidiStatusMessage.SystemExclusive => SystemExclusive.Read(reader, status, ref sysExContinue, ref sysExData),
                MidiStatusMessage.MidiTimeCode => MidiTimeCode.Read(reader, status),
                MidiStatusMessage.SongPosition => SongPosition.Read(reader, status),
                MidiStatusMessage.SongSelect => SongSelect.Read(reader, status),
                MidiStatusMessage.TuneRequest => TuneRequest.Read(reader, status),
                MidiStatusMessage.EndOfExclusive => EndOfExclusive.Read(reader, status),
                MidiStatusMessage.TimingClock => TimingClock.Read(reader, status),
                MidiStatusMessage.Start => MidiStart.Read(reader, status),
                MidiStatusMessage.Continue => MidiContinue.Read(reader, status),
                MidiStatusMessage.Stop => MidiStop.Read(reader, status),
                MidiStatusMessage.ActiveSensing => ActiveSensing.Read(reader, status),
                MidiStatusMessage.SequenceNumber => SequenceNumber.Read(reader, status),
                MidiStatusMessage.TextEvent => TextEvent.Read(reader, status),
                MidiStatusMessage.CopyrightNotice => CopyrightNotice.Read(reader, status),
                MidiStatusMessage.TrackName => TrackName.Read(reader, status),
                MidiStatusMessage.Instrument => Instrument.Read(reader, status),
                MidiStatusMessage.LyricText => LyricText.Read(reader, status),
                MidiStatusMessage.MarkerText => MarkerText.Read(reader, status),
                MidiStatusMessage.CuePoint => CuePoint.Read(reader, status),
                MidiStatusMessage.ProgramName => ProgramName.Read(reader, status),
                MidiStatusMessage.DeviceName => DeviceName.Read(reader, status),
                MidiStatusMessage.Author => Author.Read(reader, status),
                MidiStatusMessage.TrackComment => TrackComment.Read(reader, status),
                MidiStatusMessage.Title => Title.Read(reader, status),
                MidiStatusMessage.Subtitle => Subtitle.Read(reader, status),
                MidiStatusMessage.Composer => Composer.Read(reader, status),
                MidiStatusMessage.Translator => Translator.Read(reader, status),
                MidiStatusMessage.Poet => Poet.Read(reader, status),
                MidiStatusMessage.ChannelPrefix => ChannelPrefix.Read(reader, status),
                MidiStatusMessage.PortPrefix => PortPrefix.Read(reader, status),
                MidiStatusMessage.EndOfTrack => EndOfTrack.Read(reader, status),
                MidiStatusMessage.MLiveTag => MLiveTag.Read(reader, status),
                MidiStatusMessage.Tempo => Tempo.Read(reader, status),
                MidiStatusMessage.SMPTEOffset => SMPTEOffset.Read(reader, status),
                MidiStatusMessage.TimeSignature => TimeSignature.Read(reader, status),
                MidiStatusMessage.KeySignature => KeySignature.Read(reader, status),
                MidiStatusMessage.SequencerSpecific => SequencerSpecific.Read(reader, status),
                var m when ((int)m & 0xFFFF) == 0xFF => BaseTextEvent.Read(reader, status),
                _ => status,
            };
            events.Add(status);
        }

        // Finish up reading the track by setting the position to the end of the sub stream just in case everything hasn't been read.
        reader.Position = reader.Length;
        return new MidiTrack(chunk.Id, chunk.Length, events);
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => $"{(Events?.FirstOrDefault(t => t is TrackName) is TrackName m ? $"Midi Track: {m.Text}" : "Midi Track")}";
    #endregion
}
