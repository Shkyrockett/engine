﻿// <copyright file="EventStatus.cs" company="Shkyrockett">
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

namespace Engine.File
{
    /// <summary>
    /// 
    /// </summary>
    [Expandable]
    public class EventStatus
        : IMidiElement
    {
        /// <summary>
        /// 
        /// </summary>
        public EventStatus()
        {
            DeltaTime = 0;
            Status = MidiStatusMessages.Unknown;
            Channel = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="status"></param>
        /// <param name="channel"></param>
        public EventStatus(uint deltaTime, MidiStatusMessages status, byte channel)
        {
            DeltaTime = deltaTime;
            Status = status;
            Channel = channel;
        }

        /// <summary>
        /// 
        /// </summary>
        public uint DeltaTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MidiStatusMessages Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Channel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        internal static EventStatus Read(BinaryReaderExtended reader, uint deltaTime)
        {
            var cursor = reader.ReadByte();
            byte channel = 0;
            int status = cursor;
            if (cursor != 0xff)
            {
                status &= 0x0F; // >> 4;
                channel = (byte)(cursor & 0x0F >> 4);
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

            return new EventStatus(deltaTime, (MidiStatusMessages)status, channel);
        }
    }
}