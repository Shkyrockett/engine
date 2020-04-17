// <copyright file="MidiTrack.cs" company="Shkyrockett">
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
using System.Linq;

namespace Engine.File
{
    /// <summary>
    /// The midi track class.
    /// </summary>
    [ElementName(nameof(MidiTrack))]
    public class MidiTrack
        : IMediaElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MidiTrack"/> class.
        /// </summary>
        public MidiTrack()
            : this(new List<EventStatus>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MidiTrack"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        public MidiTrack(params EventStatus[] events)
            : this(new List<EventStatus>(events))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MidiTrack"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        public MidiTrack(List<EventStatus> events)
            : this("MTrk", 6, events)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MidiTrack"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="trackHeaderLength">Length of the track header.</param>
        /// <param name="events">The events.</param>
        public MidiTrack(string id, int trackHeaderLength, List<EventStatus> events)
        {
            (ID, TrackHeaderLength, Events) = (id, trackHeaderLength, events);
        }
        #endregion

        #region Indexers
        /// <summary>
        /// Gets or sets the <see cref="EventStatus"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="EventStatus"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public EventStatus this[int index]
        {
            get { return Events[index]; }
            set { Events[index] = value; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the ID.
        /// </summary>
        public string ID { get; } // = "MTrk"; // new byte[] { 0x4D, 0x54, 0x72, 0x6B };

        /// <summary>
        /// Gets or sets the track header length.
        /// </summary>
        public int TrackHeaderLength { get; set; } // = 6;

        /// <summary>
        /// Gets the items.
        /// </summary>
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<EventStatus> Events { get; }
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
            var events = new List<EventStatus>();
            var status = new EventStatus();
            var sysExContinue = false;
            byte[] sysExData = null;

            var ps = MidiStatusMessage.Unknown;

            var st = (reader.BaseStream as SubStream).BaseStream; // For debug reference.

            while (reader.BaseStream.Position < reader.Length)
            {
                var deltaTime = (uint)reader.Read7BitEncodedInt();
                var test = reader.PeekByte();
                if (test == 0)
                {
                    break; // throw new System.Exception("Invalid file.");
                }

                if ((test & 0x80) != 0)
                {
                    status = EventStatus.Read(reader, deltaTime);
                }
                else
                {
                    reader.ReadByte();
                }

                switch (status.Status)
                {
                    case MidiStatusMessage.NoteOff:
                        events.Add(NoteOff.Read(reader, status));
                        break;
                    case MidiStatusMessage.NoteOn:
                        events.Add(NoteOn.Read(reader, status));
                        break;
                    case MidiStatusMessage.PolyphonicPressure:
                        events.Add(PolyphonicPressure.Read(reader, status));
                        break;
                    case MidiStatusMessage.ControllerChange:
                        events.Add(ControllerChange.Read(reader, status));
                        break;
                    case MidiStatusMessage.ProgramChange:
                        events.Add(ProgramChange.Read(reader, status));
                        break;
                    case MidiStatusMessage.ChannelPressure:
                        events.Add(ChannelPressure.Read(reader, status));
                        break;
                    case MidiStatusMessage.PitchBend:
                        events.Add(PitchBend.Read(reader, status));
                        break;
                    case MidiStatusMessage.SystemExclusive:
                        events.Add(SystemExclusive.Read(reader, status, ref sysExContinue, ref sysExData));
                        break;
                    case MidiStatusMessage.MidiTimeCode:
                        events.Add(MidiTimeCode.Read(reader, status));
                        break;
                    case MidiStatusMessage.SongPosition:
                        events.Add(SongPosition.Read(reader, status));
                        break;
                    case MidiStatusMessage.SongSelect:
                        events.Add(SongSelect.Read(reader, status));
                        break;
                    case MidiStatusMessage.TuneRequest:
                        events.Add(TuneRequest.Read(reader, status));
                        break;
                    case MidiStatusMessage.EndOfExclusive:
                        events.Add(EndOfExclusive.Read(reader, status));
                        break;
                    case MidiStatusMessage.TimingClock:
                        events.Add(TimingClock.Read(reader, status));
                        break;
                    case MidiStatusMessage.Start:
                        events.Add(Start.Read(reader, status));
                        break;
                    case MidiStatusMessage.Continue:
                        events.Add(Continue.Read(reader, status));
                        break;
                    case MidiStatusMessage.Stop:
                        events.Add(Stop.Read(reader, status));
                        break;
                    case MidiStatusMessage.ActiveSensing:
                        events.Add(ActiveSensing.Read(reader, status));
                        break;
                    case MidiStatusMessage.SequenceNumber:
                        events.Add(SequenceNumber.Read(reader, status));
                        break;
                    case MidiStatusMessage.TextEvent:
                        events.Add(TextEvent.Read(reader, status));
                        break;
                    case MidiStatusMessage.CopyrightNotice:
                        events.Add(CopyrightNotice.Read(reader, status));
                        break;
                    case MidiStatusMessage.TrackName:
                        events.Add(TrackName.Read(reader, status));
                        break;
                    case MidiStatusMessage.Instrument:
                        events.Add(Instrument.Read(reader, status));
                        break;
                    case MidiStatusMessage.LyricText:
                        events.Add(LyricText.Read(reader, status));
                        break;
                    case MidiStatusMessage.MarkerText:
                        events.Add(MarkerText.Read(reader, status));
                        break;
                    case MidiStatusMessage.CuePoint:
                        events.Add(CuePoint.Read(reader, status));
                        break;
                    case MidiStatusMessage.ProgramName:
                        events.Add(ProgramName.Read(reader, status));
                        break;
                    case MidiStatusMessage.DeviceName:
                        events.Add(DeviceName.Read(reader, status));
                        break;
                    case MidiStatusMessage.ChannelPrefix:
                        events.Add(ChannelPrefix.Read(reader, status));
                        break;
                    case MidiStatusMessage.PortPrefix:
                        events.Add(PortPrefix.Read(reader, status));
                        break;
                    case MidiStatusMessage.EndOfTrack:
                        events.Add(EndOfTrack.Read(reader, status));
                        break;
                    case MidiStatusMessage.Tempo:
                        events.Add(Tempo.Read(reader, status));
                        break;
                    case MidiStatusMessage.SMPTEOffset:
                        events.Add(SMPTEOffset.Read(reader, status));
                        break;
                    case MidiStatusMessage.TimeSignature:
                        events.Add(TimeSignature.Read(reader, status));
                        break;
                    case MidiStatusMessage.KeySignature:
                        events.Add(KeySignature.Read(reader, status));
                        break;
                    case MidiStatusMessage.SequencerSpecific:
                        events.Add(SequencerSpecific.Read(reader, status));
                        break;
                    case MidiStatusMessage.Unknown:
                    default: break;
                }

                ps = status.Status;
            }

            return new MidiTrack(chunk.Id, chunk.Length, events);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{(Events?.FirstOrDefault(t => t is TrackName) is TrackName m ? $"Midi Track: {m.Text}" : "Midi Track")}";
        #endregion
    }
}
