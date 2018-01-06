﻿// <copyright file="Instrument.cs" company="Shkyrockett">
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
    /// Track instrument name.
    /// </summary>
    /// <remarks>
    /// FF 04 len text
    /// </remarks>
    [ElementName(nameof(Instrument))]
    [DisplayName(nameof(Instrument))]
    public class Instrument
        : BaseTextEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        public Instrument(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static Instrument Read(BinaryReaderExtended reader, EventStatus status)
            => new Instrument(reader.ReadASCIIString(), status);
    }
}
