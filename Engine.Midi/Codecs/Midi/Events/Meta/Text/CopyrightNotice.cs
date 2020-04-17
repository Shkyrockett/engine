﻿// <copyright file="CopyrightNotice.cs" company="Shkyrockett">
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
    /// Copyright Notice.
    /// </summary>
    /// <remarks>
    /// <para>FF 02 len text</para>
    /// </remarks>
    [ElementName(nameof(CopyrightNotice))]
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
        internal static CopyrightNotice Read(BinaryReaderExtended reader, EventStatus status) => new CopyrightNotice(reader.ReadASCIIString(), status);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => (Text is string t) && !string.IsNullOrWhiteSpace(t) ? $"Copyright Notice: {t}" : "Copyright Notice";
    }
}
