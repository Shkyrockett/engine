// <copyright file="CopyrightNotice.cs" company="Shkyrockett">
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
    /// Copyright Notice.
    /// </summary>
    /// <remarks>
    /// FF 02 len text
    /// </remarks>
    [ElementName(nameof(CopyrightNotice))]
    [DisplayName("Copyright Notice")]
    public class CopyrightNotice
        : BaseTextEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CopyrightNotice"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="status">The status.</param>
        public CopyrightNotice(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="CopyrightNotice"/>.</returns>
        internal static CopyrightNotice Read(BinaryReaderExtended reader, EventStatus status)
            => new CopyrightNotice(reader.ReadASCIIString(), status);
    }
}
