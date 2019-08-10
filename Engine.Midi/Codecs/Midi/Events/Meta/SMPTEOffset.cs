// <copyright file="SMPTEOffset.cs" company="Shkyrockett">
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
    /// SMPTE offset.
    /// </summary>
    /// <remarks>
    /// <para>FF 54 05  HR MN SE FR FF</para>
    /// </remarks>
    [ElementName(nameof(SMPTEOffset))]
    [DisplayName("SMPTE Offset")]
    public class SMPTEOffset
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SMPTEOffset"/> class.
        /// </summary>
        /// <param name="hours">The hours.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="seconds">The seconds.</param>
        /// <param name="frames">The frames.</param>
        /// <param name="fractionalFrames">The fractionalFrames.</param>
        /// <param name="status">The status.</param>
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
        /// Gets or sets the hours.
        /// </summary>
        public byte Hours { get; set; }

        /// <summary>
        /// Gets or sets the minutes.
        /// </summary>
        public byte Minutes { get; set; }

        /// <summary>
        /// Gets or sets the seconds.
        /// </summary>
        public byte Seconds { get; set; }

        /// <summary>
        /// Gets or sets the frames.
        /// </summary>
        public byte Frames { get; set; }

        /// <summary>
        /// Gets or sets the fractional frames.
        /// </summary>
        public byte FractionalFrames { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="SMPTEOffset"/>.</returns>
        internal static SMPTEOffset Read(BinaryReaderExtended reader, EventStatus status)
            => new SMPTEOffset(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), status);
    }
}
