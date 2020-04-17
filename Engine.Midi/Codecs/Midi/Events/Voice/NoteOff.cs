﻿// <copyright file="NoteOff.cs" company="Shkyrockett">
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

namespace Engine.File
{
    /// <summary>
    /// Note Off Status.
    /// </summary>
    /// <remarks>
    /// <para>n8 0kkkkkkk 0vvvvvvv
    /// This message is sent when a note is released (ended). 
    /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the velocity.</para>
    /// </remarks>
    [ElementName(nameof(NoteOff))]
    public class NoteOff
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteOff"/> class.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="status">The status.</param>
        public NoteOff(byte note, byte velocity, EventStatus status)
            : base((status?.DeltaTime).Value, status.Status, status.Channel)
        {
            (Note, Velocity) = (note, velocity);
        }

        /// <summary>
        /// Gets or sets the MIDI note (0x0 to 0x7F).
        /// </summary>
        public byte Note { get; set; }

        /// <summary>
        /// Gets or sets the velocity of the note (0x0 to 0x7F).
        /// </summary>
        public byte Velocity { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="NoteOff"/>.</returns>
        internal static NoteOff Read(BinaryReaderExtended reader, EventStatus status) => new NoteOff(reader.ReadByte(), reader.ReadByte(), status);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Note Off";
    }
}
