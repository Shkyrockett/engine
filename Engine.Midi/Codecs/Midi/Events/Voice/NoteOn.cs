// <copyright file="NoteOn.cs" company="Shkyrockett">
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
    /// Note On Status.
    /// </summary>
    /// <seealso cref="Engine.File.EventStatus" />
    /// <remarks>
    /// <para>n9 0kkkkkkk 0vvvvvvv
    /// This message is sent when a note is depressed (start).
    /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the velocity.</para>
    /// </remarks>
    [ElementName(nameof(NoteOn))]
    public class NoteOn
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteOn" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="note">The note.</param>
        /// <param name="velocity">The velocity.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NoteOn(IEventStatus status, byte note, byte velocity)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            (Note, Velocity) = (note, velocity);
        }

        /// <summary>
        /// Gets or sets the MIDI note (0x0 to 0x7F).
        /// </summary>
        /// <value>
        /// The note.
        /// </value>
        public byte Note { get; set; }

        /// <summary>
        /// Gets or sets the velocity of the note (0x0 to 0x7F).
        /// </summary>
        /// <value>
        /// The velocity.
        /// </value>
        public byte Velocity { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="NoteOn" />.
        /// </returns>
        internal static new NoteOn Read(BinaryReaderExtended reader, IEventStatus status) => new NoteOn(status, reader.ReadByte(), reader.ReadByte());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Note On";
    }
}
