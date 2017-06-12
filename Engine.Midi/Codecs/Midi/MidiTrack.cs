// <copyright file="MidiTrack.cs" company="Shkyrockett">
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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

namespace Engine.File
{
    /// <summary>
    /// 
    /// </summary>
    [ElementName("MidiTrack")]
    [DisplayName("Midi Track")]
    public class MidiTrack
        : IMidiElement
    {
        /// <summary>
        /// 
        /// </summary>
        private List<EventStatus> events = new List<EventStatus>();

        /// <summary>
        /// 
        /// </summary>
        public MidiTrack()
        {
            TrackHeaderLength = 6;
            events = new List<EventStatus>();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Event this[int index]
        //{
        //    get { return events[index]; }
        //    set { events[index] = value; }
        //}

        /// <summary>
        /// 
        /// </summary>
        //new List<byte> { 0x4D, 0x54, 0x72, 0x6B };
        public string ID { get; } = "MTrk";

        /// <summary>
        /// 
        /// </summary>
        public int TrackHeaderLength { get; set; } = 6;

        /// <summary>
        /// 
        /// </summary>
        public List<EventStatus> Items
            => events;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="chunk"></param>
        /// <returns></returns>
        internal static MidiTrack Read(BinaryReaderExtended reader, Chunk chunk)
        {
            var events = new List<EventStatus>();
            var status = new EventStatus();
            var sysExContinue = false;
            byte[] sysExData = null;

            MidiStatusMessages ps = MidiStatusMessages.Unknown;

            while (reader.BaseStream.Position < reader.Length)
            {
                var deltaTime = (uint)reader.Read7BitEncodedInt();
                var test = reader.PeekByte();
                if (test == 0)
                    break; // throw new System.Exception("Invalid file.");
                if ((test & 0x80) != 0)
                    status = EventStatus.Read(reader, deltaTime);
                else
                    reader.ReadByte();

                switch (status.Status)
                {
                    case MidiStatusMessages.NoteOff:
                        events.Add(NoteOff.Read(reader, status));
                        break;
                    case MidiStatusMessages.NoteOn:
                        events.Add(NoteOn.Read(reader, status));
                        break;
                    case MidiStatusMessages.PolyphonicPressure:
                        events.Add(PolyphonicPressure.Read(reader, status));
                        break;
                    case MidiStatusMessages.ControllerChange:
                        events.Add(ControllerChange.Read(reader, status));
                        break;
                    case MidiStatusMessages.ProgramChange:
                        events.Add(ProgramChange.Read(reader, status));
                        break;
                    case MidiStatusMessages.ChannelPressure:
                        events.Add(ChannelPressure.Read(reader, status));
                        break;
                    case MidiStatusMessages.PitchBend:
                        events.Add(PitchBend.Read(reader, status));
                        break;
                    case MidiStatusMessages.SystemExclusive:
                        events.Add(SystemExclusive.Read(reader, status, ref sysExContinue, ref sysExData));
                        break;
                    case MidiStatusMessages.MidiTimeCode:
                        events.Add(MidiTimeCode.Read(reader, status));
                        break;
                    case MidiStatusMessages.SongPosition:
                        events.Add(SongPosition.Read(reader, status));
                        break;
                    case MidiStatusMessages.SongSelect:
                        events.Add(SongSelect.Read(reader, status));
                        break;
                    case MidiStatusMessages.TuneRequest:
                        events.Add(TuneRequest.Read(reader, status));
                        break;
                    case MidiStatusMessages.EndOfExclusive:
                        events.Add(EndOfExclusive.Read(reader, status));
                        break;
                    case MidiStatusMessages.TimingClock:
                        events.Add(TimingClock.Read(reader, status));
                        break;
                    case MidiStatusMessages.Start:
                        events.Add(Start.Read(reader, status));
                        break;
                    case MidiStatusMessages.Continue:
                        events.Add(Continue.Read(reader, status));
                        break;
                    case MidiStatusMessages.Stop:
                        events.Add(Stop.Read(reader, status));
                        break;
                    case MidiStatusMessages.ActiveSensing:
                        events.Add(ActiveSensing.Read(reader, status));
                        break;
                    case MidiStatusMessages.SequenceNumber:
                        events.Add(SequenceNumber.Read(reader, status));
                        break;
                    case MidiStatusMessages.TextEvent:
                        events.Add(TextEvent.Read(reader, status));
                        break;
                    case MidiStatusMessages.CopyrightNotice:
                        events.Add(CopyrightNotice.Read(reader, status));
                        break;
                    case MidiStatusMessages.TrackName:
                        events.Add(TrackName.Read(reader, status));
                        break;
                    case MidiStatusMessages.Instrument:
                        events.Add(Instrument.Read(reader, status));
                        break;
                    case MidiStatusMessages.LyricText:
                        events.Add(LyricText.Read(reader, status));
                        break;
                    case MidiStatusMessages.MarkerText:
                        events.Add(MarkerText.Read(reader, status));
                        break;
                    case MidiStatusMessages.CuePoint:
                        events.Add(CuePoint.Read(reader, status));
                        break;
                    case MidiStatusMessages.ProgramName:
                        events.Add(ProgramName.Read(reader, status));
                        break;
                    case MidiStatusMessages.DeviceName:
                        events.Add(DeviceName.Read(reader, status));
                        break;
                    case MidiStatusMessages.MIDIChannel:
                        events.Add(MIDIChannel.Read(reader, status));
                        break;
                    case MidiStatusMessages.MIDIPort:
                        events.Add(MIDIPort.Read(reader, status));
                        break;
                    case MidiStatusMessages.EndOfTrack:
                        events.Add(EndOfTrack.Read(reader, status));
                        break;
                    case MidiStatusMessages.Tempo:
                        events.Add(Tempo.Read(reader, status));
                        break;
                    case MidiStatusMessages.SMPTEOffset:
                        events.Add(SMPTEOffset.Read(reader, status));
                        break;
                    case MidiStatusMessages.TimeSignature:
                        events.Add(TimeSignature.Read(reader, status));
                        break;
                    case MidiStatusMessages.KeySignature:
                        events.Add(KeySignature.Read(reader, status));
                        break;
                    case MidiStatusMessages.SequencerSpecific:
                        events.Add(SequencerSpecific.Read(reader, status));
                        break;
                    case MidiStatusMessages.Unknown:
                    default:
                        break;
                }

                ps = status.Status;
            }

            return new MidiTrack()
            {
                TrackHeaderLength = chunk.Length,
                events = events
            };
        }
    }
}
