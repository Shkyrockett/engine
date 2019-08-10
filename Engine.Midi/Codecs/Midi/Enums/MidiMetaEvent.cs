// <copyright file="MidiMetaEvents.cs" company="Shkyrockett">
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
    /// Midi Meta Events.
    /// </summary>
    /// <remarks> 
    /// <para>Specifies non-MIDI information useful to this format or to sequencers, with this syntax: FF [type] [length] [bytes]
    /// All meta-events begin with FF, then have an event type byte (which is always less than 128), and then have the length
    /// of the data stored as a variable-length quantity, and then the data itself. If there is no data, the length is 0.
    /// As with chunks, future meta-events may be designed which may not be known to existing programs, so programs must 
    /// properly ignore meta-events which they do not recognize, and indeed should expect to see them. 
    /// Programs must never ignore the length of a meta-event which they do not recognize, and they shouldn't be surprised
    /// if it's bigger than expected. If so, they must ignore everything past what they know about.
    /// However, they must not add anything of their own to the end of the meta-event.</para>
    /// </remarks>
    public enum MidiMetaEvent
        : byte // format: FF ## Len Data
    {
        /// <summary>
        /// No Event set.
        /// </summary>
        None,

        /// <summary>
        /// Sequence Number.
        /// </summary>
        /// <remarks>
        /// <para>FF 00 02  ss ss or 00</para>
        /// </remarks>
        SequenceNumber = 0x00,

        /// <summary>
        /// Text event.
        /// </summary>
        /// <remarks>
        /// <para>FF 01 len text</para>
        /// </remarks>
        TextEvent = 0x01,

        /// <summary>
        /// Copyright Notice.
        /// </summary>
        /// <remarks>
        /// <para>FF 02 len text</para>
        /// </remarks>
        CopyrightNotice = 0x02,

        /// <summary>
        /// Sequence track name.
        /// </summary>
        /// <remarks>
        /// <para>FF 03 len text</para>
        /// </remarks>
        SequenceOrTrackName = 0x03,

        /// <summary>
        /// Track instrument name.
        /// </summary>
        /// <remarks>
        /// <para>FF 04 len text</para>
        /// </remarks>
        InstrumentName = 0x04,

        /// <summary>
        /// Lyric Text.
        /// </summary>
        /// <remarks>
        /// <para>FF 05 len text</para>
        /// </remarks>
        LyricText = 0x05,

        /// <summary
        /// >Marker Text.
        /// </summary>
        /// <remarks>
        /// <para>FF 06 len text</para>
        /// </remarks>
        MarkerText = 0x06,

        /// <summary>
        /// Cue point.
        /// </summary>
        /// <remarks>
        /// <para>FF 07 len text</para>
        /// </remarks>
        CuePoint = 0x07,

        /// <summary>
        /// Program (patch) name.
        /// </summary>
        /// <remarks>
        /// <para>FF 07 len text</para>
        /// </remarks>
        ProgramName = 0x08,

        /// <summary>
        /// Device (port) name.
        /// </summary>
        /// <remarks>
        /// <para>FF 09 len text</para>
        /// </remarks>
        DeviceName = 0x09,

        /// <summary>
        /// MIDI Channel (not official?).
        /// </summary>
        /// <remarks>
        /// <para>FF 20 01  cc</para>
        /// </remarks>
        MIDIChannel = 0x20,

        /// <summary>
        /// MIDI Port (not official?).
        /// </summary>
        /// <remarks>
        /// <para>FF 21 01  pp</para>
        /// </remarks>
        MIDIPort = 0x21,

        /// <summary>
        /// End of track.
        /// </summary>
        /// <remarks>
        /// <para>FF 2F 00</para>
        /// </remarks>
        EndOfTrack = 0x2F,

        /// <summary>
        /// Set Tempo, in microseconds per MIDI quarter-note.
        /// </summary>
        /// <remarks>
        /// <para>FF 51 03  tt tt tt</para>
        /// </remarks>
        Tempo = 0x51,

        /// <summary>
        /// SMPTE offset specification.
        /// </summary>
        /// <remarks>
        /// <para>FF 54 05  hr mn se fr ff</para>
        /// </remarks>
        SMPTEOffset = 0x54,

        /// <summary>
        /// Time signature.
        /// </summary>
        /// <remarks>
        /// <para>FF 58 04  nn dd cc bb</para>
        /// </remarks>
        TimeSignature = 0x58,

        /// <summary>
        /// Key signature.
        /// </summary>
        /// <remarks>
        /// <para>FF 59 02  sf mi
        ///   sf = -7: 7 flats
        ///   sf = -1: 1 flat
        ///   sf = 0: key of C
        ///   sf = 1: 1 sharp
        ///   sf = 7: 7 sharps
        ///   mi = 0: major key 
        ///   mi = 1: minor key</para>
        /// </remarks>
        KeySignature = 0x59,

        /// <summary>
        /// Sequencer specific proprietary meta-event.
        /// </summary>
        /// <remarks>
        /// <para>FF 7F len data
        /// Sysex events and meta events cancel any running status which was in effect.
        /// Running status does not apply to and may not be used for these messages.</para>
        /// </remarks>
        SequencerSpecific = 0x7F,
    }
}
