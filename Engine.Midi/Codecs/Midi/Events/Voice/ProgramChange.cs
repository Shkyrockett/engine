// <copyright file="ProgramChange.cs" company="Shkyrockett">
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
    /// Program Change Status.
    /// </summary>
    /// <remarks>
    /// nC 0ppppppp
    /// This message sent when the patch number changes. (ppppppp) is the new program number.
    /// </remarks>
    [ElementName(nameof(ProgramChange))]
    [DisplayName("Program Change")]
    public class ProgramChange
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="program"></param>
        /// <param name="status"></param>
        public ProgramChange(byte program, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Program = program;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Program { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static ProgramChange Read(BinaryReaderExtended reader, EventStatus status)
            => new ProgramChange(reader.ReadByte(), status);
    }
}
