// <copyright file="SequencerSpecific.cs" company="Shkyrockett">
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

using System.ComponentModel;

namespace Engine.File
{
    /// <summary>
    /// Sequencer specific proprietary event.
    /// </summary>
    /// <remarks>
    /// <para>FF 7F len data
    /// System exclusive events and meta events cancel any running status which was in effect.
    /// Running status does not apply to and may not be used for these messages.</para>
    /// </remarks>
    [ElementName(nameof(SequencerSpecific))]
    [DisplayName("Sequencer Specific")]
    public class SequencerSpecific
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequencerSpecific"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="status">The status.</param>
        public SequencerSpecific(byte[] data, EventStatus status)
            : base((status?.DeltaTime).Value, status.Status, status.Channel)
        {
            Data = data;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public byte[] Data { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="SequencerSpecific"/>.</returns>
        internal static SequencerSpecific Read(BinaryReaderExtended reader, EventStatus status)
            => new SequencerSpecific(reader.ReadVariableLengthBytes(), status);
    }
}
