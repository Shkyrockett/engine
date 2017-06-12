﻿// <copyright file="TextEvent.cs" company="Shkyrockett">
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        public TextEvent(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static TextEvent Read(BinaryReaderExtended reader, EventStatus status)
            => new TextEvent(reader.ReadASCIIString(), status);
    }
}
