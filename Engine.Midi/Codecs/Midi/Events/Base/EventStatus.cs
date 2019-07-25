// <copyright file="EventStatus.cs" company="Shkyrockett">
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
    /// The event status class.
    /// </summary>
    [Expandable]
    public class EventStatus
        : IMidiElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventStatus"/> class.
        /// </summary>
        public EventStatus()
        {
            DeltaTime = 0;
            Status = MidiStatusMessage.Unknown;
            Channel = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStatus"/> class.
        /// </summary>
        /// <param name="deltaTime">The deltaTime.</param>
        /// <param name="status">The status.</param>
        /// <param name="channel">The channel.</param>
        public EventStatus(uint deltaTime, MidiStatusMessage status, byte channel)
        {
            DeltaTime = deltaTime;
            Status = status;
            Channel = channel;
        }

        /// <summary>
        /// Gets or sets the delta time.
        /// </summary>
        public uint DeltaTime { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public MidiStatusMessage Status { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        public byte Channel { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="deltaTime">The deltaTime.</param>
        /// <returns>The <see cref="EventStatus"/>.</returns>
        internal static EventStatus Read(BinaryReaderExtended reader, uint deltaTime)
        {
            var cursor = reader.ReadByte();
            byte channel = 0;
            int status = cursor;
            if (cursor != 0xff)
            {
                status &= 0x0F; // >> 4;
                channel = (byte)(cursor & (0x0F >> 4));
            }

            if (cursor >= 0x0F)
            {
                cursor = reader.ReadByte();
                status |= cursor << 8;

                //// This next extra shift was a dumb attempt to try to be clever. Turns out messages are at most two bytes.
                //// Though this might be useful in SysEx? But likely not.
                //if (cursor >= 0x0F)
                //{
                //    cursor = reader.ReadByte();
                //    status |= cursor << 16;
                //}
            }

            return new EventStatus(deltaTime, (MidiStatusMessage)status, channel);
        }
    }
}
