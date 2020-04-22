// <copyright file="EventStatus.cs" company="Shkyrockett">
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
    /// The event status class.
    /// </summary>
    /// <seealso cref="Engine.File.IMediaElement" />
    [Expandable]
    public class EventStatus
        : IEventStatus
    {
        #region Implementations
        /// <summary>
        /// The empty
        /// </summary>
        public static readonly EventStatus Empty = new EventStatus(0, MidiStatusMessage.Unknown, 1);
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EventStatus" /> class.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EventStatus()
            : this(0, MidiStatusMessage.Unknown, 1)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStatus" /> class.
        /// </summary>
        /// <param name="deltaTime">The deltaTime.</param>
        /// <param name="status">The status.</param>
        /// <param name="channel">The channel.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EventStatus(uint deltaTime, MidiStatusMessage status, byte channel)
        {
            (DeltaTime, Message, Channel) = (deltaTime, status, channel);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the delta time.
        /// </summary>
        /// <value>
        /// The delta time.
        /// </value>
        public uint DeltaTime { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public MidiStatusMessage Message { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public byte Channel { get; set; }
        #endregion

        /// <summary>
        /// Reads the specified Event.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="prevEvent">The previous event.</param>
        /// <returns>
        /// The <see cref="EventStatus" />.
        /// </returns>
        internal static IEventStatus Read(BinaryReaderExtended reader, IEventStatus prevEvent)
        {
            var deltaTime = (uint)reader.ReadVariableLengthInt();
            int channel = 1;
            var cursor = reader.ReadByte();
            int message;
            if ((cursor & 0x80) == 0)
            {
                // Continue messages.
                message = (int)prevEvent.Message;
                channel = prevEvent.Channel;
                reader.Position--;
            }
            else if ((cursor & 0xF0) == 0xF0)
            {
                // System messages.
                message = cursor;
                if (cursor == 0xFF)
                {
                    // Meta messages.
                    cursor = reader.ReadByte();
                    message |= cursor << 8;
                }
            }
            else
            {
                // Regular messages. 
                message = (cursor & 0xF0) >> 4;
                channel = (byte)((cursor & 0x0F) + 1);
            }

            return new EventStatus(deltaTime, (MidiStatusMessage)message, (byte)channel);
        }
    }
}
