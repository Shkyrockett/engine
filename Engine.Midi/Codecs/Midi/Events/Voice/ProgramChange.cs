// <copyright file="ProgramChange.cs" company="Shkyrockett">
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

using System.Runtime.CompilerServices;

namespace Engine.File
{
    /// <summary>
    /// Program Change Status.
    /// </summary>
    /// <seealso cref="Engine.File.EventStatus" />
    /// <remarks>
    /// <para>nC 0ppppppp
    /// This message sent when the patch number changes. (ppppppp) is the new program number.</para>
    /// </remarks>
    [ElementName(nameof(ProgramChange))]
    public class ProgramChange
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramChange" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="program">The program.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ProgramChange(IEventStatus status, GeneralMidiInstrument program)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            Program = program;
        }

        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public GeneralMidiInstrument Program { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="ProgramChange" />.
        /// </returns>
        internal static new ProgramChange Read(BinaryReaderExtended reader, IEventStatus status) => new ProgramChange(status, (GeneralMidiInstrument)reader.ReadByte());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"Program Change: {Program}";
    }
}
