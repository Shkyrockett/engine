// <copyright file="LyricText.cs" company="Shkyrockett">
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
    /// Lyric Text.
    /// </summary>
    /// <remarks>
    /// <para>FF 05 len text</para>
    /// </remarks>
    [ElementName(nameof(LyricText))]
    public class LyricText
        : BaseTextEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LyricText"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="text">The text.</param>
        public LyricText(IEventStatus status, string text)
            : base(status, text)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="LyricText"/>.</returns>
        internal static new LyricText Read(BinaryReaderExtended reader, IEventStatus status) => new LyricText(status, reader.ReadASCIIString());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => (Text is string t) && !string.IsNullOrWhiteSpace(t) ? $"Lyric Text: {t}" : "Lyric Text";
    }
}
