// <copyright file="LyricText.cs" company="Shkyrockett">
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
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
    /// <para>FF 05 len text</para>
    /// </remarks>
    [ElementName(nameof(LyricText))]
    [DisplayName("Lyric Text")]
    public class LyricText
        : BaseTextEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LyricText"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="status">The status.</param>
        public LyricText(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="LyricText"/>.</returns>
        internal static LyricText Read(BinaryReaderExtended reader, EventStatus status)
            => new LyricText(reader.ReadASCIIString(), status);
    }
}
