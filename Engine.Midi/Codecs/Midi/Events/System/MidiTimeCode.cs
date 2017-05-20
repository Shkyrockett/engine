// <copyright file="MidiTimeCode.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
    /// MIDI Time Code Quarter Frame.
    /// </summary>
    /// <remarks>
    /// nF 01 0nnndddd
    /// nnn = Message Type dddd = Values
    /// </remarks>
    [ElementName(nameof(MidiTimeCode))]
    [DisplayName("Midi Time Code")]
    public class MidiTimeCode
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeCode"></param>
        /// <param name="status"></param>
        public MidiTimeCode(byte timeCode, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            TimeCode = timeCode;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte TimeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static MidiTimeCode Read(BinaryReaderExtended reader, EventStatus status)
            => new MidiTimeCode(reader.ReadByte(), status);
    }
}
