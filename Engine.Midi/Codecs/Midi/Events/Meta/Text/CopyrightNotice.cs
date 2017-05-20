// <copyright file="CopyrightNotice.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        public CopyrightNotice(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static CopyrightNotice Read(BinaryReaderExtended reader, EventStatus status)
            => new CopyrightNotice(reader.ReadASCIIString(), status);
    }
}
