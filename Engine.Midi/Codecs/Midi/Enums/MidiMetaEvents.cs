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
    /// Specifies non-MIDI information useful to this format or to sequencers, with this syntax: FF [type] [length] [bytes]
    /// All meta-events begin with FF, then have an event type byte (which is always less than 128), and then have the length
    /// of the data stored as a variable-length quantity, and then the data itself. If there is no data, the length is 0.
    /// As with chunks, future meta-events may be designed which may not be known to existing programs, so programs must 
    /// properly ignore meta-events which they do not recognize, and indeed should expect to see them. 
    /// Programs must never ignore the length of a meta-event which they do not recognize, and they shouldn't be surprised
    /// if it's bigger than expected. If so, they must ignore everything past what they know about.
    /// However, they must not add anything of their own to the end of the meta-event.
    /// </remarks>
    public enum MidiMetaEvents
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
        /// FF 00 02  ss ss or 00
        /// </remarks>
        SequenceNumber = 0x00,

        /// <summary>
        /// Text event.
        /// </summary>
        /// <remarks>
        /// FF 01 len text
        /// </remarks>
        TextEvent = 0x01,

        /// <summary>
        /// Copyright Notice.
        /// </summary>
        /// <remarks>
        /// FF 02 len text
        /// </remarks>
        CopyrightNotice = 0x02,

        /// <summary>
        /// Sequence track name.
        /// </summary>
        /// <remarks>
        /// FF 03 len text
        /// </remarks>
        SequenceOrTrackName = 0x03,

        /// <summary>
        /// Track instrument name.
        /// </summary>
        /// <remarks>
        /// FF 04 len text
        /// </remarks>
        InstrumentName = 0x04,

        /// <summary>
        /// Lyric Text.
        /// </summary>
        /// <remarks>
        /// FF 05 len text
        /// </remarks>
        LyricText = 0x05,

        /// <summary
        /// >Marker Text.
        /// </summary>
        /// <remarks>
        /// FF 06 len text
        /// </remarks>
        MarkerText = 0x06,

        /// <summary>
        /// Cue point.
        /// </summary>
        /// <remarks>
        /// FF 07 len text
        /// </remarks>
        CuePoint = 0x07,

        /// <summary>
        /// Program (patch) name.
        /// </summary>
        /// <remarks>
        /// FF 07 len text
        /// </remarks>
        ProgramName = 0x08,

        /// <summary>
        /// Device (port) name.
        /// </summary>
        /// <remarks>
        /// FF 09 len text
        /// </remarks>
        DeviceName = 0x09,

        /// <summary>
        /// MIDI Channel (not official?).
        /// </summary>
        /// <remarks>
        /// FF 20 01  cc
        /// </remarks>
        MIDIChannel = 0x20,

        /// <summary>
        /// MIDI Port (not official?).
        /// </summary>
        /// <remarks>
        /// FF 21 01  pp
        /// </remarks>
        MIDIPort = 0x21,

        /// <summary>
        /// End of track.
        /// </summary>
        /// <remarks>
        /// FF 2F 00
        /// </remarks>
        EndOfTrack = 0x2F,

        /// <summary>
        /// Set Tempo, in microseconds per MIDI quarter-note.
        /// </summary>
        /// <remarks>
        /// FF 51 03  tt tt tt
        /// </remarks>
        Tempo = 0x51,

        /// <summary>
        /// SMPTE offset specification.
        /// </summary>
        /// <remarks>
        /// FF 54 05  hr mn se fr ff
        /// </remarks>
        SMPTEOffset = 0x54,

        /// <summary>
        /// Time signature.
        /// </summary>
        /// <remarks>
        /// FF 58 04  nn dd cc bb
        /// </remarks>
        TimeSignature = 0x58,

        /// <summary>
        /// Key signature.
        /// </summary>
        /// <remarks>
        /// FF 59 02  sf mi
        ///   sf = -7: 7 flats
        ///   sf = -1: 1 flat
        ///   sf = 0: key of C
        ///   sf = 1: 1 sharp
        ///   sf = 7: 7 sharps
        ///   mi = 0: major key 
        ///   mi = 1: minor key
        /// </remarks>
        KeySignature = 0x59,

        /// <summary>
        /// Sequencer specific proprietary meta-event.
        /// </summary>
        /// <remarks>
        /// FF 7F len data
        /// Sysex events and meta events cancel any running status which was in effect.
        /// Running status does not apply to and may not be used for these messages.
        /// </remarks>
        SequencerSpecific = 0x7F,
    }
}
