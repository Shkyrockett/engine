// <copyright file="SequenceNumber.cs" company="Shkyrockett">
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
    /// Sequence Number.
    /// </summary>
    /// <remarks>
    /// <para>FF 00 02  SS SS or 00</para>
    /// </remarks>
    [ElementName(nameof(SequenceNumber))]
    [DisplayName("Sequence Number")]
    public class SequenceNumber
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceNumber"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="status">The status.</param>
        public SequenceNumber(short value, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public short Value { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="SequenceNumber"/>.</returns>
        internal static SequenceNumber Read(BinaryReaderExtended reader, EventStatus status)
            => new SequenceNumber(reader.ReadNetworkInt16(), status);
    }
}
