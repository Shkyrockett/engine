// <copyright file="CuePoint.cs" company="Shkyrockett">
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
    /// Cue point.
    /// </summary>
    /// <remarks>
    /// <para>FF 07 len text</para>
    /// </remarks>
    [ElementName(nameof(CuePoint))]
    public class CuePoint
        : BaseTextEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CuePoint"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="text">The text.</param>
        public CuePoint(IEventStatus status, string text)
            : base(status, text)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="CuePoint"/>.</returns>
        internal static new CuePoint Read(BinaryReaderExtended reader, IEventStatus status) => new CuePoint(status, reader.ReadASCIIString());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => (Text is string t) && !string.IsNullOrWhiteSpace(t) ? $"Cue Point: {t}" : "Cue Point";
    }
}
