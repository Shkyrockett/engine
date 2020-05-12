// <copyright file="Composer.cs" company="Shkyrockett">
//     Copyright © 2020 Shkyrockett. All rights reserved.
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
    /// Sequence composer.
    /// </summary>
    /// <remarks>
    /// <para>FF 12 len text</para>
    /// <para>https://github.com/musescore/MuseScore/blob/master/miditools/midievent.h</para>
    /// </remarks>
    [ElementName(nameof(Composer))]
    public class Composer
        : BaseTextEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Composer"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="text">The text.</param>
        public Composer(IEventStatus status, string text)
            : base(status, text)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="Composer"/>.</returns>
        internal static new Composer Read(BinaryReaderExtended reader, IEventStatus status) => new Composer(status, reader.ReadASCIIString());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => (Text is string t) && !string.IsNullOrWhiteSpace(t) ? $"Composer: {t}" : "Composer";
    }
}
