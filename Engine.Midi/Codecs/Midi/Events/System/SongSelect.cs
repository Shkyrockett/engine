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

namespace Engine.File
{
    /// <summary>
    /// Song Select. 
    /// The Song Select specifies which sequence or song is to be played.
    /// </summary>
    /// <remarks>
    /// <para>nF 03 0sssssss
    /// The Song Select specifies which sequence or song is to be played.</para>
    /// </remarks>
    [ElementName(nameof(SongSelect))]
    public class SongSelect
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongSelect"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="status">The status.</param>
        public SongSelect(byte value, EventStatus status)
            : base((status?.DeltaTime).Value, status.Status, status.Channel)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public byte Value { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="SongSelect"/>.</returns>
        internal static SongSelect Read(BinaryReaderExtended reader, EventStatus status) => new SongSelect(reader.ReadByte(), status);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Song Select";
    }
}
