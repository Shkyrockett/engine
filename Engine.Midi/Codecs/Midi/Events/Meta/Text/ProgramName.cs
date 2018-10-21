// <copyright file="Chunk.cs" company="Shkyrockett">
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
        /// Initializes a new instance of the <see cref="ProgramName"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="status">The status.</param>
        public ProgramName(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="ProgramName"/>.</returns>
        internal static ProgramName Read(BinaryReaderExtended reader, EventStatus status)
            => new ProgramName(reader.ReadASCIIString(), status);
    }
}
