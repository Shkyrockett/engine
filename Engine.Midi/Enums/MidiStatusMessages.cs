// <copyright file="MidiStatusMessages.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// https://www.midi.org/specifications/item/table-2-expanded-messages-list-status-bytes
// https://www.midi.org/specifications/item/table-1-summary-of-midi-message
// </references>

namespace Engine.Midi
{
    /// <summary>
    /// 
    /// </summary>
    public enum MidiStatusMessages
        : int
    {
        Unknown = -1,

        #region Channel Voice Messages

        /// <summary>
        /// Note Off Status.
        /// </summary>
        /// <remarks>
        /// n8 0kkkkkkk 0vvvvvvv
        /// This message is sent when a note is released (ended). 
        /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the velocity.
        /// </remarks>
        NoteOff = MidiStatus.NoteOff,

        /// <summary>
        /// Note On Status.
        /// </summary>
        /// <remarks>
        /// n9 0kkkkkkk 0vvvvvvv
        /// This message is sent when a note is depressed (start). 
        /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the velocity.
        /// </remarks>
        NoteOn = MidiStatus.NoteOn,

        /// <summary>
        /// Polyphonic Pressure (After-touch) Status.
        /// </summary>
        /// <remarks>
        /// nA 0kkkkkkk 0vvvvvvv
        /// This message is most often sent by pressing down on the key after it "bottoms out". 
        /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the pressure value.
        /// </remarks>
        PolyphonicPressure = MidiStatus.PolyphonicPressure,

        #endregion

        #region Channel Mode Messages

        /// <summary>
        /// Controller/Mode Change Status.
        /// </summary>
        /// <remarks>
        /// nB 0ccccccc 0vvvvvvv
        /// This message is sent when a controller value changes. Controllers include devices such as pedals and levers. Controller numbers 120-127 are reserved as "Channel Mode Messages" (below). (ccccccc) is the controller number (0-119). (vvvvvvv) is the controller value (0-127).
        /// </remarks>
        ControllerChange = MidiStatus.ControllerChange,

        /// <summary>
        /// Program Change Status.
        /// </summary>
        /// <remarks>
        /// nC 0ppppppp
        /// This message sent when the patch number changes. (ppppppp) is the new program number.
        /// </remarks>
        ProgramChange = MidiStatus.ProgramChange,

        /// <summary>
        /// Channel After-touch Pressure Status.
        /// </summary>
        /// <remarks>
        /// nD 0vvvvvvv
        /// This message is most often sent by pressing down on the key after it "bottoms out". This message is different from polyphonic after-touch. Use this message to send the single greatest pressure value (of all the current depressed keys). (vvvvvvv) is the pressure value.
        /// </remarks>
        ChannelPressure = MidiStatus.ChannelPressure,

        /// <summary>
        /// Pitch Wheel Bend Status.
        /// </summary>
        /// <remarks>
        /// nE 0lllllll 0mmmmmmm
        /// 0mmmmmmm This message is sent to indicate a change in the pitch bender (wheel or lever, typically). The pitch bender is measured by a fourteen bit value. Center (no pitch change) is 2000H. Sensitivity is a function of the transmitter. (llllll) are the least significant 7 bits. (mmmmmm) are the most significant 7 bits.
        /// </remarks>
        PitchBend = MidiStatus.PitchBend,

        #endregion

        #region System Common Messages

        /// <summary>
        /// System Exclusive.
        /// </summary>
        /// <remarks>
        /// nF 00 0iiiiiii [0iiiiiii 0iiiiiii] 0ddddddd --- --- 0ddddddd 11110111
        /// This message type allows manufacturers to create their own messages (such as bulk dumps, patch parameters, and other non-spec data) and provides a mechanism for creating additional MIDI Specification messages. The Manufacturer's ID code (assigned by MMA or AMEI) is either 1 byte (0iiiiiii) or 3 bytes (0iiiiiii 0iiiiiii 0iiiiiii). Two of the 1 Byte IDs are reserved for extensions called Universal Exclusive Messages, which are not manufacturer-specific. If a device recognizes the ID code as its own (or as a supported Universal message) it will listen to the rest of the message (0ddddddd). Otherwise, the message will be ignored. (Note: Only Real-Time messages may be interleaved with a System Exclusive.)
        /// </remarks>
        SystemExclusive = MidiStatus.System | MidiSystemMessages.SystemExclusive << 8,

        /// <summary>
        /// MIDI Time Code Quarter Frame.
        /// </summary>
        /// <remarks>
        /// nF 01 0nnndddd
        /// nnn = Message Type dddd = Values
        /// </remarks>
        MidiTimeCode = MidiStatus.System | MidiSystemMessages.MidiTimeCode << 8,

        /// <summary>
        /// Song Position Pointer.
        /// </summary>
        /// <remarks>
        /// nF 02 0lllllll 0mmmmmmm
        /// This is an internal 14 bit register that holds the number of MIDI beats (1 beat= six MIDI clocks) since the start of the song. l is the LSB, m the MSB.
        /// </remarks>
        SongPosition = MidiStatus.System | MidiSystemMessages.SongPosition << 8,

        /// <summary>
        /// Song Select. 
        /// </summary>
        /// <remarks>
        /// nF 03 0sssssss
        /// The Song Select specifies which sequence or song is to be played.
        /// </remarks>
        SongSelect = MidiStatus.System | MidiSystemMessages.SongSelect << 8,

        /// <summary>
        /// Tune Request.
        /// </summary>
        /// <remarks>
        /// nF 06 
        /// Upon receiving a Tune Request, all analog synthesizers should tune their oscillators.
        /// </remarks>
        TuneRequest = MidiStatus.System | MidiSystemMessages.TuneRequest << 8,

        /// <summary>
        /// End of Exclusive.
        /// </summary>
        /// <remarks>
        /// nF 07 
        /// Used to terminate a System Exclusive dump (see above).
        /// </remarks>
        EndOfExclusive = MidiStatus.System | MidiSystemMessages.EndOfExclusive << 8,

        #endregion

        #region Real-time messages

        /// <summary>
        /// Timing Clock.
        /// </summary>
        /// <remarks>
        /// nF 08 
        /// Sent 24 times per quarter note when synchronization is required (see text).
        /// </remarks>
        TimingClock = MidiStatus.System | MidiSystemMessages.TimingClock << 8,

        /// <summary>
        /// Start.
        /// </summary>
        /// <remarks>
        /// nF 0A 
        /// Start the current sequence playing. (This message will be followed with Timing Clocks).
        /// </remarks>
        Start = MidiStatus.System | MidiSystemMessages.Start << 8,

        /// <summary>
        /// Continue.
        /// </summary>
        /// <remarks>
        /// nF 0B 
        /// Continue at the point the sequence was Stopped.
        /// </remarks>
        Continue = MidiStatus.System | MidiSystemMessages.Continue << 8,

        /// <summary>
        /// Stop.
        /// </summary>
        /// <remarks>
        /// nF 0C 
        /// Stop the current sequence.
        /// </remarks>
        Stop = MidiStatus.System | MidiSystemMessages.Stop << 8,

        /// <summary>
        /// Active Sensing.
        /// </summary>
        /// <remarks>
        /// nF 0E 
        /// This message is intended to be sent repeatedly to tell the receiver that a connection is alive. Use of this message is optional. When initially received, the receiver will expect to receive another Active Sensing message each 300ms (max), and if it does not then it will assume that the connection has been terminated. At termination, the receiver will turn off all voices and return to normal (non- active sensing) operation. 
        /// </remarks>
        ActiveSensing = MidiStatus.System | MidiSystemMessages.ActiveSensing << 8,

        #endregion

        #region Meta messages

        /// <summary>
        /// Sequence Number.
        /// </summary>
        /// <remarks>
        /// FF 00 02  SS SS or 00
        /// </remarks>
        SequenceNumber = MidiStatus.Meta | MidiMetaEvents.SequenceNumber << 8,

        /// <summary>
        /// Text event.
        /// </summary>
        /// <remarks>
        /// FF 01 len text
        /// </remarks>
        TextEvent = MidiStatus.Meta | MidiMetaEvents.TextEvent << 8,

        /// <summary>
        /// Copyright Notice.
        /// </summary>
        /// <remarks>
        /// FF 02 len text
        /// </remarks>
        CopyrightNotice = MidiStatus.Meta | MidiMetaEvents.CopyrightNotice << 8,

        /// <summary>
        /// Sequence track name.
        /// </summary>
        /// <remarks>
        /// FF 03 len text
        /// </remarks>
        TrackName = MidiStatus.Meta | MidiMetaEvents.TrackName << 8,

        /// <summary>
        /// Track instrument name.
        /// </summary>
        /// <remarks>
        /// FF 04 len text
        /// </remarks>
        Instrument = MidiStatus.Meta | MidiMetaEvents.Instrument << 8,

        /// <summary>
        /// Lyric Text.
        /// </summary>
        /// <remarks>
        /// FF 05 len text
        /// </remarks>
        LyricText = MidiStatus.Meta | MidiMetaEvents.LyricText << 8,

        /// <summary
        /// >Marker Text.
        /// </summary>
        /// <remarks>
        /// FF 06 len text
        /// </remarks>
        MarkerText = MidiStatus.Meta | MidiMetaEvents.MarkerText << 8,

        /// <summary>
        /// Cue point.
        /// </summary>
        /// <remarks>
        /// FF 07 len text
        /// </remarks>
        CuePoint = MidiStatus.Meta | MidiMetaEvents.CuePoint << 8,

        /// <summary>
        /// Program (patch) name.
        /// </summary>
        /// <remarks>
        /// FF 07 len text
        /// </remarks>
        ProgramName = MidiStatus.Meta | MidiMetaEvents.ProgramName << 8,

        /// <summary>
        /// Device (port) name.
        /// </summary>
        /// <remarks>
        /// FF 09 len text
        /// </remarks>
        DeviceName = MidiStatus.Meta | MidiMetaEvents.DeviceName << 8,

        /// <summary>
        /// MIDI Channel (not official?).
        /// </summary>
        /// <remarks>
        /// FF 20 01  cc
        /// </remarks>
        MIDIChannel = MidiStatus.Meta | MidiMetaEvents.MIDIChannel << 8 | 0x01 << 16,

        /// <summary>
        /// MIDI Port (not official?).
        /// </summary>
        /// <remarks>
        /// FF 21 01  pp
        /// </remarks>
        MIDIPort = MidiStatus.Meta | MidiMetaEvents.MIDIPort << 8 | 0x01 << 16,

        /// <summary>
        /// End of track.
        /// </summary>
        /// <remarks>
        /// FF 2F 00
        /// </remarks>
        EndOfTrack = MidiStatus.Meta | MidiMetaEvents.EndOfTrack << 8,

        /// <summary>
        /// Set tempo.
        /// </summary>
        /// <remarks>
        /// FF 51 03  TT TT TT
        /// </remarks>
        Tempo = MidiStatus.Meta | MidiMetaEvents.Tempo << 8 | 0x03 << 16,

        /// <summary>
        /// SMPTE offset.
        /// </summary>
        /// <remarks>
        /// FF 54 05  HR MN SE FR FF
        /// </remarks>
        SMPTEOffset = MidiStatus.Meta | MidiMetaEvents.SMPTEOffset << 8 | 0x05 << 16,

        /// <summary>
        /// Time signature.
        /// </summary>
        /// <remarks>
        /// FF 58 04  NN DD CC BB
        /// </remarks>
        TimeSignature = MidiStatus.Meta | MidiMetaEvents.TimeSignature << 8 | 0x04 << 16,

        /// <summary>
        /// Key signature.
        /// </summary>
        /// <remarks>
        /// FF 59 02  sf  mi
        ///   sf = -7: 7 flats
        ///   sf = -1: 1 flat
        ///   sf = 0: key of C
        ///   sf = 1: 1 sharp
        ///   sf = 7: 7 sharps
        ///   mi = 0: major key 
        ///   mi = 1: minor key
        ///   
        ///   sf is <see cref="MidiKeySignatures"/>
        ///   mi is <see cref="MidiTonality"/>
        /// </remarks>
        KeySignature = MidiStatus.Meta | MidiMetaEvents.KeySignature << 8 | 0x02 << 16,

        /// <summary>
        /// Sequencer specific proprietary event.
        /// </summary>
        /// <remarks>
        /// FF 7F len data
        /// System exclusive events and meta events cancel any running status which was in effect.
        /// Running status does not apply to and may not be used for these messages.
        /// </remarks>
        SequencerSpecific = MidiStatus.Meta | MidiMetaEvents.SequencerSpecific << 8,

        #endregion
    }
}
