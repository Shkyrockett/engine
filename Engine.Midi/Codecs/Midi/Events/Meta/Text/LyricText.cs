// <copyright file="LyricText.cs" company="Shkyrockett">
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

namespace Engine.File
{
    /// <summary>
    /// Lyric Text.
    /// </summary>
    /// <remarks>
    /// FF 05 len text
    /// </remarks>
    [ElementName(nameof(LyricText))]
    [DisplayName("Lyric Text")]
    public class LyricText
        : BaseTextEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        public LyricText(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static LyricText Read(BinaryReaderExtended reader, EventStatus status)
            => new LyricText(reader.ReadASCIIString(), status);
    }
}
