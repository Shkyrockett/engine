// <copyright file="SMPTEOffset.cs" company="Shkyrockett">
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

using System.Runtime.CompilerServices;

namespace Engine.File
{
    /// <summary>
    /// SMPTE offset.
    /// </summary>
    /// <seealso cref="Engine.File.EventStatus" />
    /// <remarks>
    /// <para>FF 54 05  HR MN SE FR FF</para>
    /// </remarks>
    [ElementName(nameof(SMPTEOffset))]
    public class SMPTEOffset
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SMPTEOffset" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="hours">The hours.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="seconds">The seconds.</param>
        /// <param name="frames">The frames.</param>
        /// <param name="fractionalFrames">The fractional frames.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SMPTEOffset(IEventStatus status, byte hours, byte minutes, byte seconds, byte frames, byte fractionalFrames)
            : this(status, 5, hours, minutes, seconds, frames, fractionalFrames)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SMPTEOffset" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="length">The length.</param>
        /// <param name="hours">The hours.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="seconds">The seconds.</param>
        /// <param name="frames">The frames.</param>
        /// <param name="fractionalFrames">The fractionalFrames.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal SMPTEOffset(IEventStatus status, int length, byte hours, byte minutes, byte seconds, byte frames, byte fractionalFrames)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            (Length, Hours, Minutes, Seconds, Frames, FractionalFrames) = (length, hours, minutes, seconds, frames, fractionalFrames);
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; }

        /// <summary>
        /// Gets or sets the hours.
        /// </summary>
        /// <value>
        /// The hours.
        /// </value>
        public byte Hours { get; set; }

        /// <summary>
        /// Gets or sets the minutes.
        /// </summary>
        /// <value>
        /// The minutes.
        /// </value>
        public byte Minutes { get; set; }

        /// <summary>
        /// Gets or sets the seconds.
        /// </summary>
        /// <value>
        /// The seconds.
        /// </value>
        public byte Seconds { get; set; }

        /// <summary>
        /// Gets or sets the frames.
        /// </summary>
        /// <value>
        /// The frames.
        /// </value>
        public byte Frames { get; set; }

        /// <summary>
        /// Gets or sets the fractional frames.
        /// </summary>
        /// <value>
        /// The fractional frames.
        /// </value>
        public byte FractionalFrames { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="SMPTEOffset" />.
        /// </returns>
        internal static new SMPTEOffset Read(BinaryReaderExtended reader, IEventStatus status) => new SMPTEOffset(status, reader.ReadVariableLengthInt(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => "SMPTE Offset";
    }
}
