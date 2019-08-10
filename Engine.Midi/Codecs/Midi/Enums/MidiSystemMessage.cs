// <copyright file="MidiSystemMessages.cs" company="Shkyrockett">
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <author id="shkyrockett">Shkyrockett</author>
// <notes></notes>
// <references>
// </references>

namespace Engine.File
{
    /// <summary>
    /// The midi system messages enum.
    /// </summary>
    public enum MidiSystemMessage
        : byte
    {
        #region Common Messages
        /// <summary>
        /// System Exclusive.
        /// </summary>
        /// <remarks>
        /// <para>nF 00 0iiiiiii [0iiiiiii 0iiiiiii] 0ddddddd --- --- 0ddddddd 11110111
        /// This message type allows manufacturers to create their own messages (such as bulk dumps, patch parameters, and other non-spec data) and provides a mechanism for creating additional MIDI Specification messages. The Manufacturer's ID code (assigned by MMA or AMEI) is either 1 byte (0iiiiiii) or 3 bytes (0iiiiiii 0iiiiiii 0iiiiiii). Two of the 1 Byte IDs are reserved for extensions called Universal Exclusive Messages, which are not manufacturer-specific. If a device recognizes the ID code as its own (or as a supported Universal message) it will listen to the rest of the message (0ddddddd). Otherwise, the message will be ignored. (Note: Only Real-Time messages may be interleaved with a System Exclusive.)</para>
        /// </remarks>
        SystemExclusive = 0x00,

        /// <summary>
        /// MIDI Time Code Quarter Frame.
        /// </summary>
        /// <remarks>
        /// <para>nF 01 0nnndddd
        /// nnn = Message Type dddd = Values</para>
        /// </remarks>
        MidiTimeCode = 0x01,

        /// <summary>
        /// Song Position Pointer.
        /// </summary>
        /// <remarks>
        /// <para>nF 02 0lllllll 0mmmmmmm
        /// This is an internal 14 bit register that holds the number of MIDI beats (1 beat= six MIDI clocks) since the start of the song. l is the LSB, m the MSB.</para>
        /// </remarks>
        SongPosition = 0x02,

        /// <summary>
        /// Song Select. 
        /// </summary>
        /// <remarks>
        /// <para>nF 03 0sssssss
        /// The Song Select specifies which sequence or song is to be played.</para>
        /// </remarks>
        SongSelect = 0x03,

        // Reserved = 0x04,
        // Reserved = 0x05,

        /// <summary>
        /// Tune Request.
        /// </summary>
        /// <remarks>
        /// <para>nF 06 
        /// Upon receiving a Tune Request, all analog synthesizers should tune their oscillators.</para>
        /// </remarks>
        TuneRequest = 0x06,

        /// <summary>
        /// End of Exclusive.
        /// </summary>
        /// <remarks>
        /// <para>nF 07 
        /// Used to terminate a System Exclusive dump (see above).</para>
        /// </remarks>
        EndOfExclusive = 0x07,
        #endregion Common Messages

        #region Real-time Messages
        /// <summary>
        /// Timing Clock.
        /// </summary>
        /// <remarks>
        /// <para>nF 08 
        /// Sent 24 times per quarter note when synchronization is required (see text).</para>
        /// </remarks>
        TimingClock = 0x08,

        // Reserved = 0x09,

        /// <summary>
        /// Start.
        /// </summary>
        /// <remarks>
        /// <para>nF 0A 
        /// Start the current sequence playing. (This message will be followed with Timing Clocks).</para>
        /// </remarks>
        Start = 0x0A,

        /// <summary>
        /// Continue.
        /// </summary>
        /// <remarks>
        /// <para>nF 0B 
        /// Continue at the point the sequence was Stopped.</para>
        /// </remarks>
        Continue = 0x0B,

        /// <summary>
        /// Stop.
        /// </summary>
        /// <remarks>
        /// <para>nF 0C 
        /// Stop the current sequence.</para>
        /// </remarks>
        Stop = 0x0C,

        // Reserved = 0x0D,

        /// <summary>
        /// Active Sensing.
        /// </summary>
        /// <remarks>
        /// <para>nF 0E 
        /// This message is intended to be sent repeatedly to tell the receiver that a connection is alive. Use of this message is optional. When initially received, the receiver will expect to receive another Active Sensing message each 300ms (max), and if it does not then it will assume that the connection has been terminated. At termination, the receiver will turn off all voices and return to normal (non- active sensing) operation.</para> 
        /// </remarks>
        ActiveSensing = 0x0E,

        /// <summary>
        /// Reset.
        /// </summary>
        /// <remarks>
        /// <para>nF 0F 
        /// Reset all receivers in the system to power-up status. This should be used sparingly, preferably under manual control. In particular, it should not be sent on power-up.</para>
        /// </remarks>
        Reset = 0x0F,
        #endregion Real-time Messages
    }
}
