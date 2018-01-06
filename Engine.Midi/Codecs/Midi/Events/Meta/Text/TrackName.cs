// <copyright file="TrackName.cs" company="Shkyrockett">
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
    /// Sequence track name.
    /// </summary>
    /// <remarks>
    /// FF 03 len text
    /// </remarks>
    [ElementName(nameof(TrackName))]
    [DisplayName("Track Name")]
    public class TrackName
        : BaseTextEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        public TrackName(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static TrackName Read(BinaryReaderExtended reader, EventStatus status)
            => new TrackName(reader.ReadASCIIString(), status);
    }
}
