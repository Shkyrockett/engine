// <copyright file="SongSelect.cs" company="Shkyrockett">
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
    /// Song Select.
    /// The Song Select specifies which sequence or song is to be played.
    /// </summary>
    /// <seealso cref="Engine.File.EventStatus" />
    /// <remarks>
    /// <para>nF 03 0sssssss
    /// The Song Select specifies which sequence or song is to be played.</para>
    /// </remarks>
    [ElementName(nameof(SongSelect))]
    public class SongSelect
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongSelect" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SongSelect(IEventStatus status, byte value)
            : this(status, 3, value)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SongSelect" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="length">The length.</param>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal SongSelect(IEventStatus status, int length, byte value)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            (Length, Value) = (length, value);
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public byte Value { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="SongSelect" />.
        /// </returns>
        internal static new SongSelect Read(BinaryReaderExtended reader, IEventStatus status) => new SongSelect(status, reader.ReadVariableLengthInt(), reader.ReadByte());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Song Select";
    }
}
