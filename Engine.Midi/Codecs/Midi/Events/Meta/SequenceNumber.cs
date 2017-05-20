// <copyright file="SequenceNumber.cs" company="Shkyrockett">
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
    /// Sequence Number.
    /// </summary>
    /// <remarks>
    /// FF 00 02  SS SS or 00
    /// </remarks>
    [ElementName(nameof(SequenceNumber))]
    [DisplayName("Sequence Number")]
    public class SequenceNumber
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="status"></param>
        public SequenceNumber(short value, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public short Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static SequenceNumber Read(BinaryReaderExtended reader, EventStatus status)
            => new SequenceNumber(reader.ReadNetworkInt16(), status);
    }
}
