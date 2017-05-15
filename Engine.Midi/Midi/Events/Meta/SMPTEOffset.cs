// <copyright file="SMPTEOffset.cs" company="Shkyrockett">
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

namespace Engine.Midi
{
    /// <summary>
    /// SMPTE offset.
    /// </summary>
    /// <remarks>
    /// FF 54 05  HR MN SE FR FF
    /// </remarks>
    [ElementName(nameof(SMPTEOffset))]
    [DisplayName("SMPTE Offset")]
    public class SMPTEOffset
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="frames"></param>
        /// <param name="fractionalFrames"></param>
        /// <param name="status"></param>
        public SMPTEOffset(byte hours, byte minutes, byte seconds, byte frames, byte fractionalFrames, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Frames = frames;
            FractionalFrames = fractionalFrames;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Hours { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Minutes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Seconds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Frames { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte FractionalFrames { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static SMPTEOffset Read(MidiBinaryReader reader, EventStatus status)
            => new SMPTEOffset(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), status);
    }
}
