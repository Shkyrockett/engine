// <copyright file="Poet.cs" company="Shkyrockett">
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
    /// Sequence poet.
    /// </summary>
    /// <remarks>
    /// <para>FF 14 len text</para>
    /// <para>https://github.com/musescore/MuseScore/blob/master/miditools/midievent.h</para>
    /// </remarks>
    [ElementName(nameof(Poet))]
    public class Poet
        : BaseTextEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Poet"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="text">The text.</param>
        public Poet(IEventStatus status, string text)
            : base(status, text)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="Poet"/>.</returns>
        internal static new Poet Read(BinaryReaderExtended reader, IEventStatus status) => new Poet(status, reader.ReadASCIIString());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => (Text is string t) && !string.IsNullOrWhiteSpace(t) ? $"Poet: {t}" : "Poet";
    }
}
