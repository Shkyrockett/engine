// <copyright file="SequencerSpecific.cs" company="Shkyrockett">
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
    /// Sequencer specific proprietary event.
    /// </summary>
    /// <remarks>
    /// FF 7F len data
    /// System exclusive events and meta events cancel any running status which was in effect.
    /// Running status does not apply to and may not be used for these messages.
    /// </remarks>
    [ElementName(nameof(SequencerSpecific))]
    [DisplayName("Sequencer Specific")]
    public class SequencerSpecific
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="status"></param>
        public SequencerSpecific(byte[] data, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Data = data;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static SequencerSpecific Read(BinaryReaderExtended reader, EventStatus status)
            => new SequencerSpecific(reader.ReadVariableLengthBytes(), status);
    }
}
