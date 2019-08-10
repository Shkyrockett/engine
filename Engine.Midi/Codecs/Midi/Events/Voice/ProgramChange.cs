// <copyright file="ProgramChange.cs" company="Shkyrockett">
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
    /// Program Change Status.
    /// </summary>
    /// <remarks>
    /// <para>nC 0ppppppp
    /// This message sent when the patch number changes. (ppppppp) is the new program number.</para>
    /// </remarks>
    [ElementName(nameof(ProgramChange))]
    [DisplayName("Program Change")]
    public class ProgramChange
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramChange"/> class.
        /// </summary>
        /// <param name="program">The program.</param>
        /// <param name="status">The status.</param>
        public ProgramChange(byte program, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Program = program;
        }

        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        public byte Program { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="ProgramChange"/>.</returns>
        internal static ProgramChange Read(BinaryReaderExtended reader, EventStatus status)
            => new ProgramChange(reader.ReadByte(), status);
    }
}
