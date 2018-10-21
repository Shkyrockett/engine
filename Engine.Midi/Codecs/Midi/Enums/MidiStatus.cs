// <copyright file="MidiStatus.cs" company="Shkyrockett">
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
    /// The midi status enum.
    /// </summary>
    public enum MidiStatus
        : byte
    {
        /// <summary>
        /// Note Off status message event.
        /// </summary>
        /// <remarks>
        /// n8 0kkkkkkk 0vvvvvvv
        /// This message is sent when a note is released (ended). 
        /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the velocity.
        /// </remarks>
        NoteOff = 0x08,

        /// <summary>
        /// Note On status message event.
        /// </summary>
        /// <remarks>
        /// n9 0kkkkkkk 0vvvvvvv
        /// This message is sent when a note is depressed (start). 
        /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the velocity.
        /// </remarks>
        NoteOn = 0x09,

        /// <summary>
        /// Polyphonic Pressure (Aftertouch) status message event.
        /// </summary>
        /// <remarks>
        /// nA 0kkkkkkk 0vvvvvvv
        /// This message is most often sent by pressing down on the key after it "bottoms out". 
        /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the pressure value.
        /// </remarks>
        PolyphonicPressure = 0x0A,

        /// <summary>
        /// Controller/Mode Change status message event.
        /// </summary>
        /// <remarks>
        /// nB 0ccccccc 0vvvvvvv
        /// This message is sent when a controller value changes. Controllers include devices such as pedals and levers. Controller numbers 120-127 are reserved as "Channel Mode Messages" (below). (ccccccc) is the controller number (0-119). (vvvvvvv) is the controller value (0-127).
        /// </remarks>
        ControllerChange = 0x0B,

        /// <summary>
        /// Program Change status message event.
        /// </summary>
        /// <remarks>
        /// nC 0ppppppp
        /// This message sent when the patch number changes. (ppppppp) is the new program number.
        /// </remarks>
        ProgramChange = 0x0C,

        /// <summary>
        /// Channel After-touch Pressure status message event.
        /// </summary>
        /// <remarks>
        /// nD 0vvvvvvv
        /// This message is most often sent by pressing down on the key after it "bottoms out". This message is different from polyphonic after-touch. Use this message to send the single greatest pressure value (of all the current depressed keys). (vvvvvvv) is the pressure value.
        /// </remarks>
        ChannelPressure = 0x0D,

        /// <summary>
        /// Pitch Bend status message event.
        /// </summary>
        /// <remarks>
        /// nE 0lllllll 0mmmmmmm
        /// 0mmmmmmm This message is sent to indicate a change in the pitch bender (wheel or lever, typically). The pitch bender is measured by a fourteen bit value. Center (no pitch change) is 2000H. Sensitivity is a function of the transmitter. (llllll) are the least significant 7 bits. (mmmmmm) are the most significant 7 bits.
        /// </remarks>
        PitchBend = 0x0E,

        /// <summary>
        /// System status message events.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        System = 0x0F,

        /// <summary>
        /// Meta status message events.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        Meta = 0xFF,
    }
}
