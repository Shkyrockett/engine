// <copyright file="MidiStatusMessages.cs" company="Shkyrockett">
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
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

namespace Engine.File
{
    /// <summary>
    /// The midi status messages enum.
    /// </summary>
    public enum MidiStatusMessage
        : int
    {
        /// <summary>
        /// The Unknown = -1.
        /// </summary>
        Unknown = -1,

        #region Channel Voice Messages
        /// <summary>
        /// Note Off Status.
        /// </summary>
        /// <remarks>
        /// <para>n8 0kkkkkkk 0vvvvvvv
        /// This message is sent when a note is released (ended). 
        /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the velocity.</para>
        /// </remarks>
        NoteOff = MidiStatus.NoteOff,

        /// <summary>
        /// Note On Status.
        /// </summary>
        /// <remarks>
        /// <para>n9 0kkkkkkk 0vvvvvvv
        /// This message is sent when a note is depressed (start). 
        /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the velocity.</para>
        /// </remarks>
        NoteOn = MidiStatus.NoteOn,

        /// <summary>
        /// Polyphonic Pressure (After-touch) Status.
        /// </summary>
        /// <remarks>
        /// <para>nA 0kkkkkkk 0vvvvvvv
        /// This message is most often sent by pressing down on the key after it "bottoms out". 
        /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the pressure value.</para>
        /// </remarks>
        PolyphonicPressure = MidiStatus.PolyphonicPressure,
        #endregion Channel Voice Messages

        #region Channel Mode Messages
        /// <summary>
        /// Controller/Mode Change Status.
        /// </summary>
        /// <remarks>
        /// <para>nB 0ccccccc 0vvvvvvv
        /// This message is sent when a controller value changes. Controllers include devices such as pedals and levers. Controller numbers 120-127 are reserved as "Channel Mode Messages" (below). (ccccccc) is the controller number (0-119). (vvvvvvv) is the controller value (0-127).</para>
        /// </remarks>
        ControllerChange = MidiStatus.ControllerChange,

        /// <summary>
        /// Program Change Status.
        /// </summary>
        /// <remarks>
        /// <para>nC 0ppppppp
        /// This message sent when the patch number changes. (ppppppp) is the new program number.</para>
        /// </remarks>
        ProgramChange = MidiStatus.ProgramChange,

        /// <summary>
        /// Channel After-touch Pressure Status.
        /// </summary>
        /// <remarks>
        /// <para>nD 0vvvvvvv
        /// This message is most often sent by pressing down on the key after it "bottoms out". This message is different from polyphonic after-touch. Use this message to send the single greatest pressure value (of all the current depressed keys). (vvvvvvv) is the pressure value.</para>
        /// </remarks>
        ChannelPressure = MidiStatus.ChannelPressure,

        /// <summary>
        /// Pitch Wheel Bend Status.
        /// </summary>
        /// <remarks>
        /// <para>nE 0lllllll 0mmmmmmm
        /// 0mmmmmmm This message is sent to indicate a change in the pitch bender (wheel or lever, typically). The pitch bender is measured by a fourteen bit value. Center (no pitch change) is 2000H. Sensitivity is a function of the transmitter. (llllll) are the least significant 7 bits. (mmmmmm) are the most significant 7 bits.</para>
        /// </remarks>
        PitchBend = MidiStatus.PitchBend,
        #endregion Channel Mode Messages

        #region System Common Messages
        /// <summary>
        /// System Exclusive.
        /// </summary>
        /// <remarks>
        /// <para>nF 00 0iiiiiii [0iiiiiii 0iiiiiii] 0ddddddd --- --- 0ddddddd 11110111
        /// This message type allows manufacturers to create their own messages (such as bulk dumps, patch parameters, and other non-spec data) and provides a mechanism for creating additional MIDI Specification messages. The Manufacturer's ID code (assigned by MMA or AMEI) is either 1 byte (0iiiiiii) or 3 bytes (0iiiiiii 0iiiiiii 0iiiiiii). Two of the 1 Byte IDs are reserved for extensions called Universal Exclusive Messages, which are not manufacturer-specific. If a device recognizes the ID code as its own (or as a supported Universal message) it will listen to the rest of the message (0ddddddd). Otherwise, the message will be ignored. (Note: Only Real-Time messages may be interleaved with a System Exclusive.)</para>
        /// </remarks>
        SystemExclusive = MidiStatus.System << 4 | (MidiSystemMessage.SystemExclusive),

        /// <summary>
        /// MIDI Time Code Quarter Frame.
        /// </summary>
        /// <remarks>
        /// <para>nF 01 0nnndddd
        /// nnn = Message Type dddd = Values</para>
        /// </remarks>
        MidiTimeCode = MidiStatus.System << 4 | (MidiSystemMessage.MidiTimeCode),

        /// <summary>
        /// Song Position Pointer.
        /// </summary>
        /// <remarks>
        /// <para>nF 02 0lllllll 0mmmmmmm
        /// This is an internal 14 bit register that holds the number of MIDI beats (1 beat= six MIDI clocks) since the start of the song. l is the LSB, m the MSB.</para>
        /// </remarks>
        SongPosition = MidiStatus.System << 4 | (MidiSystemMessage.SongPosition),

        /// <summary>
        /// Song Select. 
        /// </summary>
        /// <remarks>
        /// <para>nF 03 0sssssss
        /// The Song Select specifies which sequence or song is to be played.</para>
        /// </remarks>
        SongSelect = MidiStatus.System << 4 | (MidiSystemMessage.SongSelect),

        /// <summary>
        /// Tune Request.
        /// </summary>
        /// <remarks>
        /// <para>nF 06 
        /// Upon receiving a Tune Request, all analog synthesizers should tune their oscillators.</para>
        /// </remarks>
        TuneRequest = MidiStatus.System << 4 | (MidiSystemMessage.TuneRequest),

        /// <summary>
        /// End of Exclusive.
        /// </summary>
        /// <remarks>
        /// <para>nF 07 
        /// Used to terminate a System Exclusive dump (see above).</para>
        /// </remarks>
        EndOfExclusive = MidiStatus.System << 4 | (MidiSystemMessage.EndOfExclusive),
        #endregion System Common Messages

        #region Real-time Messages
        /// <summary>
        /// Timing Clock.
        /// </summary>
        /// <remarks>
        /// <para>nF 08 
        /// Sent 24 times per quarter note when synchronization is required (see text).</para>
        /// </remarks>
        TimingClock = MidiStatus.System << 4 | (MidiSystemMessage.TimingClock),

        /// <summary>
        /// Start.
        /// </summary>
        /// <remarks>
        /// <para>nF 0A 
        /// Start the current sequence playing. (This message will be followed with Timing Clocks).</para>
        /// </remarks>
        Start = MidiStatus.System << 4 | (MidiSystemMessage.Start),

        /// <summary>
        /// Continue.
        /// </summary>
        /// <remarks>
        /// <para>nF 0B 
        /// Continue at the point the sequence was Stopped.</para>
        /// </remarks>
        Continue = MidiStatus.System << 4 | (MidiSystemMessage.Continue),

        /// <summary>
        /// Stop.
        /// </summary>
        /// <remarks>
        /// <para>nF 0C 
        /// Stop the current sequence.</para>
        /// </remarks>
        Stop = MidiStatus.System << 4 | (MidiSystemMessage.Stop),

        /// <summary>
        /// Active Sensing.
        /// </summary>
        /// <remarks>
        /// <para>nF 0E 
        /// This message is intended to be sent repeatedly to tell the receiver that a connection is alive. Use of this message is optional. When initially received, the receiver will expect to receive another Active Sensing message each 300ms (max), and if it does not then it will assume that the connection has been terminated. At termination, the receiver will turn off all voices and return to normal (non- active sensing) operation.</para> 
        /// </remarks>
        ActiveSensing = MidiStatus.System << 4 | (MidiSystemMessage.ActiveSensing),
        #endregion Real-time Messages

        #region Meta Messages
        /// <summary>
        /// Sequence Number.
        /// </summary>
        /// <remarks>
        /// <para>FF 00 02  SS SS or 00</para>
        /// </remarks>
        SequenceNumber = MidiStatus.Meta | (MidiMetaEvent.SequenceNumber << 8),

        /// <summary>
        /// Text event.
        /// </summary>
        /// <remarks>
        /// <para>FF 01 len text</para>
        /// </remarks>
        TextEvent = MidiStatus.Meta | (MidiMetaEvent.TextEvent << 8),

        /// <summary>
        /// Copyright Notice.
        /// </summary>
        /// <remarks>
        /// <para>FF 02 len text</para>
        /// </remarks>
        CopyrightNotice = MidiStatus.Meta | (MidiMetaEvent.CopyrightNotice << 8),

        /// <summary>
        /// Sequence track name.
        /// </summary>
        /// <remarks>
        /// <para>FF 03 len text</para>
        /// </remarks>
        TrackName = MidiStatus.Meta | (MidiMetaEvent.SequenceOrTrackName << 8),

        /// <summary>
        /// Track instrument name.
        /// </summary>
        /// <remarks>
        /// <para>FF 04 len text</para>
        /// </remarks>
        Instrument = MidiStatus.Meta | (MidiMetaEvent.InstrumentName << 8),

        /// <summary>
        /// Lyric Text.
        /// </summary>
        /// <remarks>
        /// <para>FF 05 len text</para>
        /// </remarks>
        LyricText = MidiStatus.Meta | (MidiMetaEvent.LyricText << 8),

        /// <summary
        /// >Marker Text.
        /// </summary>
        /// <remarks>
        /// <para>FF 06 len text</para>
        /// </remarks>
        MarkerText = MidiStatus.Meta | (MidiMetaEvent.MarkerText << 8),

        /// <summary>
        /// Cue point.
        /// </summary>
        /// <remarks>
        /// <para>FF 07 len text</para>
        /// </remarks>
        CuePoint = MidiStatus.Meta | (MidiMetaEvent.CuePoint << 8),

        /// <summary>
        /// Program (patch) name.
        /// </summary>
        /// <remarks>
        /// <para>FF 07 len text</para>
        /// </remarks>
        ProgramName = MidiStatus.Meta | (MidiMetaEvent.ProgramName << 8),

        /// <summary>
        /// Device (port) name.
        /// </summary>
        /// <remarks>
        /// <para>FF 09 len text</para>
        /// </remarks>
        DeviceName = MidiStatus.Meta | (MidiMetaEvent.DeviceName << 8),

        /// <summary>
        /// The author. Extension.
        /// </summary>
        /// <remarks>
        /// <para>FF 0A len text</para>
        /// </remarks>
        Author = MidiStatus.Meta | (MidiMetaEvent.Author << 8),

        /// <summary>
        /// The track comment. MuseScore extension
        /// </summary>
        /// <remarks>
        /// <para>FF 0F len text</para>
        /// </remarks>
        TrackComment = MidiStatus.Meta | (MidiMetaEvent.TrackComment << 8),

        /// <summary>
        /// The title. MuseScore extension
        /// </summary>
        /// <remarks>
        /// <para>FF 10 len text</para>
        /// </remarks>
        Title = MidiStatus.Meta | (MidiMetaEvent.Title << 8),

        /// <summary>
        /// The subtitle. MuseScore extension
        /// </summary>
        /// <remarks>
        /// <para>FF 11 len text</para>
        /// </remarks>
        Subtitle = MidiStatus.Meta | (MidiMetaEvent.Subtitle << 8),

        /// <summary>
        /// The composer. MuseScore extension
        /// </summary>
        /// <remarks>
        /// <para>FF 12 len text</para>
        /// </remarks>
        Composer = MidiStatus.Meta | (MidiMetaEvent.Composer << 8),

        /// <summary>
        /// The translator. MuseScore extension
        /// </summary>
        /// <remarks>
        /// <para>FF 13 len text</para>
        /// </remarks>
        Translator = MidiStatus.Meta | (MidiMetaEvent.Translator << 8),

        /// <summary>
        /// The poet. MuseScore extension
        /// </summary>
        /// <remarks>
        /// <para>FF 14 len text</para>
        /// </remarks>
        Poet = MidiStatus.Meta | (MidiMetaEvent.Poet << 8),

        /// <summary>
        /// MIDI Channel Prefix (not official?).
        /// </summary>
        /// <remarks>
        /// <para>FF 20 01  cc</para>
        /// </remarks>
        ChannelPrefix = MidiStatus.Meta | (MidiMetaEvent.ChannelPrefix << 8),

        /// <summary>
        /// MIDI Port Prefix (not official?).
        /// </summary>
        /// <remarks>
        /// <para>FF 21 01  pp</para>
        /// </remarks>
        PortPrefix = MidiStatus.Meta | (MidiMetaEvent.PortPrefix << 8),

        /// <summary>
        /// End of track.
        /// </summary>
        /// <remarks>
        /// <para>FF 2F 00</para>
        /// </remarks>
        EndOfTrack = MidiStatus.Meta | (MidiMetaEvent.EndOfTrack << 8),

        /// <summary>
        /// The m live tag
        /// </summary>
        /// <remarks>
        /// <para>FF 4B len tt text</para>
        /// </remarks>
        MLiveTag = MidiStatus.Meta | (MidiMetaEvent.MLiveTag << 8),

        /// <summary>
        /// Set tempo.
        /// </summary>
        /// <remarks>
        /// <para>FF 51 03  TT TT TT</para>
        /// </remarks>
        Tempo = MidiStatus.Meta | (MidiMetaEvent.Tempo << 8),

        /// <summary>
        /// SMPTE offset.
        /// </summary>
        /// <remarks>
        /// <para>FF 54 05  HR MN SE FR FF</para>
        /// </remarks>
        SMPTEOffset = MidiStatus.Meta | (MidiMetaEvent.SMPTEOffset << 8),

        /// <summary>
        /// Time signature.
        /// </summary>
        /// <remarks>
        /// <para>FF 58 04  NN DD CC BB</para>
        /// </remarks>
        TimeSignature = MidiStatus.Meta | (MidiMetaEvent.TimeSignature << 8),

        /// <summary>
        /// Key signature.
        /// </summary>
        /// <remarks>
        /// <para>FF 59 02  sf  mi
        ///   sf = -7: 7 flats
        ///   sf = -1: 1 flat
        ///   sf = 0: key of C
        ///   sf = 1: 1 sharp
        ///   sf = 7: 7 sharps
        ///   mi = 0: major key 
        ///   mi = 1: minor key
        ///   
        ///   sf is <see cref="MidiKeySignature"/>
        ///   mi is <see cref="MidiTonality"/></para>
        /// </remarks>
        KeySignature = MidiStatus.Meta | (MidiMetaEvent.KeySignature << 8),

        /// <summary>
        /// Sequencer specific proprietary event.
        /// </summary>
        /// <remarks>
        /// <para>FF 7F len data
        /// System exclusive events and meta events cancel any running status which was in effect.
        /// Running status does not apply to and may not be used for these messages.</para>
        /// </remarks>
        SequencerSpecific = MidiStatus.Meta | (MidiMetaEvent.SequencerSpecific << 8),
        #endregion Meta Messages
    }
}
