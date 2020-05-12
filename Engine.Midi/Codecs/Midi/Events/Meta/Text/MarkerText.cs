// <copyright file="MarkerText.cs" company="Shkyrockett">
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
    /// <summary
    /// >Marker Text.
    /// </summary>
    /// <remarks>
    /// <para>FF 06 len text</para>
    /// </remarks>
    [ElementName(nameof(MarkerText))]
    public class MarkerText
        : BaseTextEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkerText"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="text">The text.</param>
        public MarkerText(IEventStatus status, string text)
            : base(status, text)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="MarkerText"/>.</returns>
        internal static new MarkerText Read(BinaryReaderExtended reader, IEventStatus status) => new MarkerText(status, reader.ReadASCIIString());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => (Text is string t) && !string.IsNullOrWhiteSpace(t) ? $"Marker Text: {t}" : "Marker Text";
    }
}
