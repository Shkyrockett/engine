// <copyright file="Chunk.cs" company="Shkyrockett">
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
    /// Program (patch) name.
    /// </summary>
    /// <remarks>
    /// FF 07 len text
    /// </remarks>
    [ElementName(nameof(ProgramName))]
    [DisplayName("Program Name")]
    public class ProgramName
        : BaseTextEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        public ProgramName(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static ProgramName Read(BinaryReaderExtended reader, EventStatus status)
            => new ProgramName(reader.ReadASCIIString(), status);
    }
}
