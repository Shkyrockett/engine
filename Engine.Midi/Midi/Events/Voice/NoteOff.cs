// <copyright file="NoteOff.cs" company="Shkyrockett">
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
    /// Note Off Status.
    /// </summary>
    /// <remarks>
    /// n8 0kkkkkkk 0vvvvvvv
    /// This message is sent when a note is released (ended). 
    /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the velocity.
    /// </remarks>
    [ElementName(nameof(NoteOff))]
    [DisplayName("Note Off")]
    public class NoteOff
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="velocity"></param>
        public NoteOff(byte note, byte velocity, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Note = note;
            Velocity = velocity;
        }

        /// <summary>
        /// Gets or sets the MIDI note (0x0 to 0x7F).
        /// </summary>
        public byte Note { get; set; }

        /// <summary>
        /// Gets or sets the velocity of the note (0x0 to 0x7F).
        /// </summary>
        public byte Velocity { get; set; }

        ///
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static NoteOff Read(MidiBinaryReader reader, EventStatus status)
            => new NoteOff(reader.ReadByte(), reader.ReadByte(), status);
    }
}
