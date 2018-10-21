﻿// <copyright file="CuePoint.cs" company="Shkyrockett">
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
    /// Cue point.
    /// </summary>
    /// <remarks>
    /// FF 07 len text
    /// </remarks>
    [ElementName(nameof(CuePoint))]
    [DisplayName("Cue Point")]
    public class CuePoint
        : BaseTextEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CuePoint"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="status">The status.</param>
        public CuePoint(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="CuePoint"/>.</returns>
        internal static CuePoint Read(BinaryReaderExtended reader, EventStatus status)
            => new CuePoint(reader.ReadASCIIString(), status);
    }
}
