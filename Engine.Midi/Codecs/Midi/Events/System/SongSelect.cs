// <copyright file="SongSelect.cs" company="Shkyrockett">
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
    /// Song Select. 
    /// The Song Select specifies which sequence or song is to be played.
    /// </summary>
    /// <remarks>
    /// nF 03 0sssssss
    /// The Song Select specifies which sequence or song is to be played.
    /// </remarks>
    [ElementName(nameof(SongSelect))]
    [DisplayName("Song Select")]
    public class SongSelect
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        public SongSelect(byte value, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static SongSelect Read(BinaryReaderExtended reader, EventStatus status)
            => new SongSelect(reader.ReadByte(), status);
    }
}
