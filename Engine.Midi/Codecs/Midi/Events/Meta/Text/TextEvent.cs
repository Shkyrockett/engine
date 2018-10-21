// <copyright file="TextEvent.cs" company="Shkyrockett">
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
    /// Text event.
    /// </summary>
    /// <remarks>
    /// FF 01 len text
    /// </remarks>
    [ElementName(nameof(TextEvent))]
    [DisplayName("Text Event")]
    public class TextEvent
        : BaseTextEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextEvent"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="status">The status.</param>
        public TextEvent(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="TextEvent"/>.</returns>
        internal static TextEvent Read(BinaryReaderExtended reader, EventStatus status)
            => new TextEvent(reader.ReadASCIIString(), status);
    }
}
